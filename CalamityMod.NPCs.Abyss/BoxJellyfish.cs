using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class BoxJellyfish : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y += 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 44;
		base.NPC.width = 30;
		base.NPC.height = 33;
		base.NPC.defense = 8;
		base.NPC.lifeMax = 100;
		base.NPC.alpha = 20;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = SoundID.NPCHit25;
		base.NPC.DeathSound = SoundID.NPCDeath28;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BoxJellyfishBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer1Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BoxJellyfish")
		});
	}

	public override void AI()
	{
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (!base.NPC.wet)
		{
			base.NPC.rotation += base.NPC.velocity.X * 0.1f;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.98f;
				if ((double)base.NPC.velocity.X > -0.01 && (double)base.NPC.velocity.X < 0.01)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.2f;
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
			base.NPC.ai[0] = 1f;
			return;
		}
		if (base.NPC.collideX)
		{
			base.NPC.velocity.X = base.NPC.velocity.X * -1f;
			base.NPC.direction *= -1;
		}
		if (base.NPC.collideY)
		{
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
		bool canAttack = false;
		base.NPC.TargetClosest(faceTarget: false);
		if (Main.player[base.NPC.target].wet && !Main.player[base.NPC.target].dead && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
		{
			canAttack = true;
		}
		if (canAttack)
		{
			base.NPC.localAI[2] = 1f;
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + 1.57f;
			NPC nPC = base.NPC;
			nPC.velocity *= 0.975f;
			float lungeThreshold = 0.8f;
			if (base.NPC.velocity.X > 0f - lungeThreshold && base.NPC.velocity.X < lungeThreshold && base.NPC.velocity.Y > 0f - lungeThreshold && base.NPC.velocity.Y < lungeThreshold)
			{
				base.NPC.TargetClosest();
				float num = (CalamityWorld.death ? 12f : (CalamityWorld.revenge ? 10f : 8f));
				Vector2 npcPosition = base.NPC.Center;
				float targetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - npcPosition.X;
				float targetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - npcPosition.Y;
				float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
				targetDistance = num / targetDistance;
				targetX *= targetDistance;
				targetY *= targetDistance;
				base.NPC.velocity.X = targetX;
				base.NPC.velocity.Y = targetY;
			}
			return;
		}
		base.NPC.localAI[2] = 0f;
		base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.02f;
		base.NPC.rotation = base.NPC.velocity.X * 0.4f;
		if (base.NPC.velocity.X < -1f || base.NPC.velocity.X > 1f)
		{
			base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
			if (base.NPC.velocity.Y < -1f)
			{
				base.NPC.ai[0] = 1f;
			}
		}
		else
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
			if (base.NPC.velocity.Y > 1f)
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
		else
		{
			base.NPC.ai[0] = 1f;
		}
		if ((double)base.NPC.velocity.Y > 1.2 || (double)base.NPC.velocity.Y < -1.2)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y * 0.99f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneSulphur)
		{
			return 0f;
		}
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer1 && spawnInfo.Water)
		{
			return SpawnCondition.CaveJellyfish.Chance * 2.5f;
		}
		return SpawnCondition.OceanMonster.Chance * 0.1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(70, 120);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(1303, 30);
		npcLoot.DefineConditionalDropSet(() => NPC.downedBoss3).Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AbyssShocker>(), 50, 40));
	}
}
