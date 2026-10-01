using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class Trilobite : ModNPC
{
	public const float MinSpeedLungePrompt = 0.5f;

	public Player Target => Main.player[base.NPC.target];

	public ref float SpikeShootCountdown => ref base.NPC.ai[0];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.TrailCacheLength[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 36;
		base.NPC.height = 38;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.damage = 45;
		base.NPC.lifeMax = 300;
		base.NPC.defense = 15;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.damage = 80;
			base.NPC.lifeMax = 4200;
			base.NPC.defense = 30;
		}
		base.NPC.knockBackResist = 0.2f;
		base.NPC.value = Item.buyPrice(0, 0, 4);
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit42;
		base.NPC.DeathSound = SoundID.NPCDeath27;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<TrilobiteBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Trilobite")
		});
	}

	public override void AI()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		if (!base.NPC.wet)
		{
			GetStuckOnLand();
			return;
		}
		if (((Vector2)(ref base.NPC.velocity)).Length() < 0.5f)
		{
			base.NPC.TargetClosest();
			float lungeSpeed = (DownedBossSystem.downedPolterghast ? 18.5f : 15f);
			base.NPC.velocity = base.NPC.SafeDirectionTo(Target.Center, -Vector2.UnitY) * lungeSpeed;
			base.NPC.velocity.X *= 1.6f;
			base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
			base.NPC.netUpdate = true;
			return;
		}
		if (Math.Abs(base.NPC.velocity.X) < 20f)
		{
			base.NPC.velocity.X += (float)base.NPC.direction * 0.02f;
		}
		base.NPC.rotation = base.NPC.velocity.X * 0.4f;
		if (Math.Abs(base.NPC.velocity.Y) < 0.9f)
		{
			base.NPC.velocity.X *= 0.96f;
		}
		else if (Main.netMode != 1 && Math.Abs(base.NPC.velocity.X) < 3.5f)
		{
			float speedX = 18f;
			float speedY = 9f;
			if (DownedBossSystem.downedPolterghast)
			{
				speedX = 22f;
				speedY = 11f;
			}
			base.NPC.velocity = base.NPC.SafeDirectionTo(Target.Center, -Vector2.UnitY) * new Vector2(speedX, speedY);
			base.NPC.netUpdate = true;
		}
		base.NPC.velocity.Y *= 0.98f;
	}

	public void GetStuckOnLand()
	{
		if (SpikeShootCountdown > 0f)
		{
			SpikeShootCountdown--;
		}
		base.NPC.rotation += base.NPC.velocity.X * 0.1f;
		if (base.NPC.velocity.Y == 0f)
		{
			base.NPC.velocity.X *= 0.99f;
			if (Math.Abs(base.NPC.velocity.X) < 0.01f)
			{
				base.NPC.velocity.X = 0f;
			}
		}
		if (base.NPC.velocity.Y > 13f)
		{
			base.NPC.velocity.Y = 13f;
			base.NPC.netUpdate = true;
		}
		else
		{
			base.NPC.velocity.Y += 0.3f;
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(() => DownedBossSystem.downedPolterghast);
		mainRule.Add(ModContent.ItemType<CorrodedFossil>(), 15, 1, 3, !DownedBossSystem.downedPolterghast);
		mainRule.AddFail(ModContent.ItemType<CorrodedFossil>(), 3, 1, 3, DownedBossSystem.downedPolterghast);
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.NPC.velocity)).Length() > 0.5f)
		{
			CalamityGlobalNPC.DrawAfterimage(base.NPC, spriteBatch, drawColor, Color.Transparent, null, null, directioning: true);
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		if (SpikeShootCountdown <= 0f)
		{
			SoundEngine.PlaySound(in SoundID.NPCDeath11, base.NPC.Center);
			int projDamage = ((!DownedBossSystem.downedPolterghast) ? ((!DownedBossSystem.downedAquaticScourge) ? (Main.masterMode ? 14 : (Main.expertMode ? 17 : 21)) : (Main.masterMode ? 19 : (Main.expertMode ? 23 : 29))) : (Main.masterMode ? 23 : (Main.expertMode ? 28 : 35)));
			Vector2 spikeVelocity = -base.NPC.velocity.RotatedByRandom(0.18000000715255737);
			if (Main.zenithWorld)
			{
				spikeVelocity = -projectile.velocity;
				((Vector2)(ref spikeVelocity)).Normalize();
				spikeVelocity *= (float)(DownedBossSystem.downedPolterghast ? 8 : 5);
			}
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Main.rand.NextVector2Unit() * base.NPC.Size * 0.7f, spikeVelocity, ModContent.ProjectileType<TrilobiteSpike>(), projDamage, 3f);
			SpikeShootCountdown = Main.rand.Next(50, 65);
			base.NPC.netUpdate = true;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 4.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= Main.npcFrameCount[base.Type] * frameHeight)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("TrilobiteGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("TrilobiteGore2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("TrilobiteGore3").Type, base.NPC.scale);
			}
			for (int i = 0; i < 30; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		}
	}
}
