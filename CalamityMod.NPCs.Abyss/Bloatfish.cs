using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class Bloatfish : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.lavaImmune = true;
		base.NPC.damage = 5;
		base.NPC.width = 74;
		base.NPC.height = 94;
		base.NPC.defense = 100;
		base.NPC.lifeMax = 7200;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 30);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0.9f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BloatfishBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer4Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Bloatfish")
		});
	}

	public override void AI()
	{
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		base.NPC.noGravity = true;
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.wet)
		{
			base.NPC.TargetClosest(faceTarget: false);
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * -1f;
				base.NPC.direction *= -1;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.collideY)
			{
				base.NPC.netUpdate = true;
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
					base.NPC.directionY = -1;
					base.NPC.ai[0] = -1f;
				}
				else if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
					base.NPC.directionY = 1;
					base.NPC.ai[0] = 1f;
				}
			}
			base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.1f;
			if (base.NPC.velocity.X < -0.2f || base.NPC.velocity.X > 0.2f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
			}
			if (base.NPC.ai[0] == -1f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
				if ((double)base.NPC.velocity.Y < -0.3)
				{
					base.NPC.ai[0] = 1f;
				}
			}
			else
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
				if ((double)base.NPC.velocity.Y > 0.3)
				{
					base.NPC.ai[0] = -1f;
				}
			}
			int npcTileX = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
			int npcTileY = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
			if (Main.tile[npcTileX, npcTileY - 1].LiquidAmount > 128)
			{
				if (Main.tile[npcTileX, npcTileY + 1].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
				else if (Main.tile[npcTileX, npcTileY + 2].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
			}
			if ((double)base.NPC.velocity.Y > 0.4 || (double)base.NPC.velocity.Y < -0.4)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y * 0.95f;
			}
		}
		else
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.94f;
				if ((double)base.NPC.velocity.X > -0.2 && (double)base.NPC.velocity.X < 0.2)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.3f;
			if (base.NPC.velocity.Y > 3f)
			{
				base.NPC.velocity.Y = 3f;
			}
			base.NPC.ai[0] = 1f;
		}
		base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f;
		if ((double)base.NPC.rotation < -0.2)
		{
			base.NPC.rotation = -0.2f;
		}
		if ((double)base.NPC.rotation > 0.2)
		{
			base.NPC.rotation = 0.2f;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if ((projectile.penetrate == -1 || projectile.penetrate > 1) && !projectile.minion)
		{
			projectile.penetrate = 1;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<CrushDepth>(), 300);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (!base.NPC.wet && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer4 && spawnInfo.Water && NPC.CountNPCS(ModContent.NPCType<Bloatfish>()) < 3)
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.5f;
			}
			return 4.5f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<Voidstone>(), 1, 10, 20);
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 5, 7, 7, 10));
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bloatfish").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bloatfish2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bloatfish3").Type, base.NPC.scale);
			}
		}
		if (base.NPC.scale < 2f || Main.zenithWorld)
		{
			base.NPC.scale += 0.05f;
		}
	}
}
