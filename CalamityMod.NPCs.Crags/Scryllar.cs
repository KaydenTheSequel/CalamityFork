using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Summon;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crags;

public class Scryllar : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 10f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 50;
		base.NPC.width = 80;
		base.NPC.height = 80;
		base.NPC.defense = 18;
		base.NPC.lifeMax = 160;
		base.NPC.alpha = 100;
		base.NPC.knockBackResist = 0.7f;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.HitSound = SoundID.NPCHit49;
		base.NPC.DeathSound = SoundID.NPCDeath51;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.lavaImmune = true;
		if (DownedBossSystem.downedProvidence)
		{
			base.NPC.damage = 90;
			base.NPC.defense = 30;
			base.NPC.lifeMax = 2500;
		}
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ScryllarBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Scryllar")
		});
	}

	public override void AI()
	{
		base.NPC.rotation = base.NPC.velocity.X * 0.04f;
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		bool hoverDownDistCheck = false;
		if (base.NPC.justHit)
		{
			base.NPC.ai[2] = 0f;
		}
		if (base.NPC.ai[2] >= 0f)
		{
			int hoverDistance = 16;
			bool changeDirectionX = false;
			bool changeDirectionY = false;
			if (base.NPC.position.X > base.NPC.ai[0] - (float)hoverDistance && base.NPC.position.X < base.NPC.ai[0] + (float)hoverDistance)
			{
				changeDirectionX = true;
			}
			else if ((base.NPC.velocity.X < 0f && base.NPC.direction > 0) || (base.NPC.velocity.X > 0f && base.NPC.direction < 0))
			{
				changeDirectionX = true;
			}
			hoverDistance += 24;
			if (base.NPC.position.Y > base.NPC.ai[1] - (float)hoverDistance && base.NPC.position.Y < base.NPC.ai[1] + (float)hoverDistance)
			{
				changeDirectionY = true;
			}
			if (changeDirectionX & changeDirectionY)
			{
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 30f && hoverDistance == 16)
				{
					hoverDownDistCheck = true;
				}
				if (base.NPC.ai[2] >= 60f)
				{
					base.NPC.ai[2] = -200f;
					base.NPC.direction *= -1;
					base.NPC.velocity.X = base.NPC.velocity.X * -1f;
					base.NPC.collideX = false;
				}
			}
			else
			{
				base.NPC.ai[0] = base.NPC.position.X;
				base.NPC.ai[1] = base.NPC.position.Y;
				base.NPC.ai[2] = 0f;
			}
			base.NPC.TargetClosest();
		}
		else
		{
			base.NPC.TargetClosest();
			base.NPC.ai[2] += 2f;
		}
		int npcTileX = (int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f) + base.NPC.direction * 2;
		int npcTileY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f);
		bool hoverDownwards = true;
		int tileCheckLoopAmt = 3;
		for (int loopInc1 = npcTileY; loopInc1 < npcTileY + tileCheckLoopAmt; loopInc1++)
		{
			if ((Main.tile[npcTileX, loopInc1].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, loopInc1].TileType]) || Main.tile[npcTileX, loopInc1].LiquidAmount > 0)
			{
				hoverDownwards = false;
				break;
			}
		}
		if (Main.player[base.NPC.target].npcTypeNoAggro[base.Type])
		{
			bool inTileNoAggro = false;
			for (int i = npcTileY; i < npcTileY + tileCheckLoopAmt - 2; i++)
			{
				if ((Main.tile[npcTileX, i].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, i].TileType]) || Main.tile[npcTileX, i].LiquidAmount > 0)
				{
					inTileNoAggro = true;
					break;
				}
			}
			base.NPC.directionY = (!inTileNoAggro).ToDirectionInt();
		}
		if (hoverDownDistCheck)
		{
			hoverDownwards = true;
		}
		if (hoverDownwards)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.1f;
			if (base.NPC.velocity.Y > 3f)
			{
				base.NPC.velocity.Y = 3f;
			}
		}
		else
		{
			if (base.NPC.directionY < 0 && base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.1f;
			}
			if (base.NPC.velocity.Y < -4f)
			{
				base.NPC.velocity.Y = -4f;
			}
		}
		if (base.NPC.collideX)
		{
			base.NPC.velocity.X = base.NPC.oldVelocity.X * -0.4f;
			if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 1f)
			{
				base.NPC.velocity.X = 1f;
			}
			if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -1f)
			{
				base.NPC.velocity.X = -1f;
			}
		}
		if (base.NPC.collideY)
		{
			base.NPC.velocity.Y = base.NPC.oldVelocity.Y * -0.25f;
			if (base.NPC.velocity.Y > 0f && base.NPC.velocity.Y < 1f)
			{
				base.NPC.velocity.Y = 1f;
			}
			if (base.NPC.velocity.Y < 0f && base.NPC.velocity.Y > -1f)
			{
				base.NPC.velocity.Y = -1f;
			}
		}
		float maxXVelocity = 4f;
		if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f - maxXVelocity)
		{
			base.NPC.velocity.X = base.NPC.velocity.X - 0.1f;
			if (base.NPC.velocity.X > maxXVelocity)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - 0.1f;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + 0.05f;
			}
			if (base.NPC.velocity.X < 0f - maxXVelocity)
			{
				base.NPC.velocity.X = 0f - maxXVelocity;
			}
		}
		else if (base.NPC.direction == 1 && base.NPC.velocity.X < maxXVelocity)
		{
			base.NPC.velocity.X = base.NPC.velocity.X + 0.1f;
			if (base.NPC.velocity.X < 0f - maxXVelocity)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + 0.1f;
			}
			else if (base.NPC.velocity.X < 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - 0.05f;
			}
			if (base.NPC.velocity.X > maxXVelocity)
			{
				base.NPC.velocity.X = maxXVelocity;
			}
		}
		maxXVelocity = 1.5f;
		if (base.NPC.directionY == -1 && base.NPC.velocity.Y > 0f - maxXVelocity)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y - 0.04f;
			if (base.NPC.velocity.Y > maxXVelocity)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.05f;
			}
			else if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.03f;
			}
			if (base.NPC.velocity.Y < 0f - maxXVelocity)
			{
				base.NPC.velocity.Y = 0f - maxXVelocity;
			}
		}
		else if (base.NPC.directionY == 1 && base.NPC.velocity.Y < maxXVelocity)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.04f;
			if (base.NPC.velocity.Y < 0f - maxXVelocity)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.05f;
			}
			else if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.03f;
			}
			if (base.NPC.velocity.Y > maxXVelocity)
			{
				base.NPC.velocity.Y = maxXVelocity;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.Calamity().ZoneCalamity)
		{
			return 0f;
		}
		return 0.25f;
	}

	public static void DefineScryllarLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.Hardmode());
		LeadingConditionRule postProv = npcLoot.DefineConditionalDropSet(DropHelper.PostProv());
		mainRule.Add(ModContent.ItemType<EssenceofHavoc>(), 2);
		postProv.Add(ModContent.ItemType<Bloodstone>(), 4);
		postProv.Add(ModContent.ItemType<GuidelightofOblivion>(), 20);
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		DefineScryllarLoot(npcLoot);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Scryllar").Type, base.NPC.scale);
			}
		}
	}
}
