using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.PlagueEnemies;

public class PestilentSlime : ModNPC
{
	public float spikeTimer = 60f;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 1;
		base.NPC.damage = 55;
		base.NPC.width = 40;
		base.NPC.height = 30;
		base.NPC.defense = 35;
		base.NPC.lifeMax = 600;
		base.NPC.knockBackResist = 0.5f;
		base.AnimationType = 81;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.alpha = 60;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<PestilentSlimeBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.PestilentSlime")
		});
	}

	public override void AI()
	{
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		if (spikeTimer > 0f)
		{
			spikeTimer--;
		}
		if (base.NPC.wet || Main.player[base.NPC.target].npcTypeNoAggro[base.Type])
		{
			return;
		}
		Vector2 slimePosition = default(Vector2);
		((Vector2)(ref slimePosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
		float targetXDist = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - slimePosition.X;
		float targetYDist = Main.player[base.NPC.target].position.Y - slimePosition.Y;
		float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
		if (Main.expertMode && targetDistance < 120f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && base.NPC.velocity.Y == 0f)
		{
			base.NPC.ai[0] = -40f;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			}
			if (spikeTimer != 0f)
			{
				return;
			}
			int damage = (Main.masterMode ? 16 : (Main.expertMode ? 19 : 25));
			SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
			if (Main.netMode != 1)
			{
				Vector2 spikeVelocity = default(Vector2);
				for (int n = 0; n < 5; n++)
				{
					((Vector2)(ref spikeVelocity))._002Ector((float)(n - 2), -4f);
					spikeVelocity.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
					spikeVelocity.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
					((Vector2)(ref spikeVelocity)).Normalize();
					spikeVelocity *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), slimePosition.X, slimePosition.Y, spikeVelocity.X, spikeVelocity.Y, ModContent.ProjectileType<PlagueStingerGoliathV2>(), damage, 0f, Main.myPlayer);
					spikeTimer = 30f;
				}
			}
		}
		else
		{
			if (!(targetDistance < 360f) || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) || base.NPC.velocity.Y != 0f)
			{
				return;
			}
			base.NPC.ai[0] = -40f;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			}
			if (spikeTimer == 0f)
			{
				int damage2 = (Main.masterMode ? 14 : (Main.expertMode ? 17 : 22));
				SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
				targetYDist = Main.player[base.NPC.target].position.Y - slimePosition.Y - (float)Main.rand.Next(0, 200);
				targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
				targetDistance = 6.5f / targetDistance;
				targetXDist *= targetDistance;
				targetYDist *= targetDistance;
				spikeTimer = 50f;
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), slimePosition.X, slimePosition.Y, targetXDist, targetYDist, ModContent.ProjectileType<PlagueStingerGoliathV2>(), damage2, 0f, Main.myPlayer);
				}
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !NPC.downedGolemBoss || spawnInfo.Player.Calamity().ZoneSunkenSea)
		{
			return 0f;
		}
		return SpawnCondition.HardmodeJungle.Chance * 0.09f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.PlagueBoomSound, base.NPC.Center);
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<PlagueCellCanister>(), 1, 1, 2);
		npcLoot.Add(ItemDropRule.NormalvsExpert(209, 4, 2));
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Plague>(), 180);
		}
	}
}
