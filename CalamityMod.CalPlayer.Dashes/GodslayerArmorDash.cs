using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Cooldowns;
using CalamityMod.Enums;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public class GodslayerArmorDash : PlayerDashEffect
{
	public SlotId GSDashSlot;

	public static readonly SoundStyle Impact = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerDeathImpact")
	{
		Volume = 0.5f
	};

	public int Time;

	public float Size = 2.2f;

	public bool SoundOnce = true;

	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.ShieldSlam;

	public override bool IsOmnidirectional => true;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		return 32f;
	}

	public override void OnDashEffects(Player player)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		Time = 0;
		Size = 2.2f;
		GSDashSlot = SoundEngine.PlaySound(in DevourerofGodsHead.DeathAnimationSound, player.Center);
		SoundOnce = true;
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(player.Center, Vector2.Zero, Color.Orchid, new Vector2(2f, 2f), Main.rand.NextFloat(12f, 25f), 0.1f, 12f, 18));
		for (int i = 0; i <= 15; i++)
		{
			Dust.NewDustPerfect(player.position, 181, -player.velocity.RotatedByRandom(MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.3f, 0.9f), 0, default(Color), Main.rand.NextFloat(3.1f, 3.9f)).noGravity = false;
		}
	}

	public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(GSDashSlot, out ActiveSound Dashsound) && Dashsound.IsPlaying)
		{
			Dashsound.Position = player.Center;
		}
		Time++;
		Size -= 0.01f;
		float progress = MathHelper.Clamp((float)Time / 75f, 0f, 1f);
		float ease = 1f - (float)Math.Pow(1f - progress, 0.20000000298023224);
		float finalSpeed = MathHelper.Lerp(32f, 11f, ease);
		Vector2 dashVel = Main.MouseWorld - player.Center;
		Vector2 targetDirection = player.velocity.ToRotation().AngleTowards(dashVel.ToRotation(), 0.175f).ToRotationVector2();
		player.velocity = targetDirection * finalSpeed;
		player.maxFallSpeed = 50f;
		if (Time < 75)
		{
			GeneralParticleHandler.SpawnParticle(new Jaws(player.Center + player.velocity * 0.5f, player.velocity, Color.Fuchsia, new Vector2(0.8f, 1f), player.velocity.ToRotation() + (float)Math.PI / 2f, Size, Size, 2));
			GeneralParticleHandler.SpawnParticle(new Jaws(player.Center + player.velocity * 0.45f, player.velocity, Color.Aqua, new Vector2(0.8f, 1f), player.velocity.ToRotation() + (float)Math.PI / 2f, Size - 0.3f, Size - 0.3f, 2));
		}
		float radiusFactor = MathHelper.Lerp(0f, 1f, Utils.GetLerpValue(2f, 2.5f, Time, clamped: true));
		for (int i = 0; i < 3; i++)
		{
			float offsetRotationAngle = player.velocity.ToRotation() + (float)Time / 20f;
			float radius = (30f + (float)Math.Cos((float)Time / 3f) * 24f) * radiusFactor;
			Dust dust = Dust.NewDustPerfect(player.Center + player.velocity * 0.8f + offsetRotationAngle.ToRotationVector2().RotatedBy((float)i / 5f * ((float)Math.PI * 2f)) * radius, Main.rand.NextBool(5) ? 181 : 295);
			dust.noGravity = true;
			dust.velocity = player.velocity * 0.5f;
			dust.scale = Main.rand.NextFloat(2.7f, 3f);
			Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) + player.velocity * 0.5f, Main.rand.NextBool(14) ? 180 : 295, -player.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(2.7f, 3.9f)).noGravity = true;
		}
		if (Time % 2 == 0)
		{
			float sparkscale = Size * 1.3f;
			Vector2 SparkVelocity1 = player.velocity.RotatedBy(player.direction * -4) * 0.08f - player.velocity / 2f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(player.Center + player.velocity.RotatedBy(2f * (float)player.direction) * 1.2f, SparkVelocity1, affectedByGravity: false, Main.rand.Next(11, 13), sparkscale, Main.rand.NextBool(3) ? Color.Aqua : Color.Fuchsia));
			Vector2 SparkVelocity2 = player.velocity.RotatedBy(player.direction * 4) * 0.08f - player.velocity / 2f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(player.Center + player.velocity.RotatedBy(-2f * (float)player.direction) * 1.2f, SparkVelocity2, affectedByGravity: false, Main.rand.Next(11, 13), sparkscale, Main.rand.NextBool(3) ? Color.Aqua : Color.Fuchsia));
		}
		if (Time > 75 && Time < 78)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(player.Center - player.velocity * 0.52f, player.velocity / 1.5f, Color.Fuchsia, new Vector2(1f, 2f), player.velocity.ToRotation(), 0.82f, 0.32f, 60));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(player.Center - player.velocity * 0.4f, player.velocity / 1.5f * 0.9f, Color.Aqua, new Vector2(0.8f, 1.5f), player.velocity.ToRotation(), 0.58f, 0.28f, 50));
			Time = 95;
		}
		dashSpeed = finalSpeed;
		runSpeedDecelerationFactor = 0.8f;
		player.AddCooldown(GodSlayerDash.ID, GodSlayerChestplate.DashCooldown);
		player.Calamity().godSlayerDashHotKeyPressed = false;
	}

	public override void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (SoundOnce)
		{
			SoundEngine.PlaySound(in DevourerofGodsHead.DeathExplosionSound, player.Center);
			SoundOnce = false;
		}
		for (int i = 0; i <= 25; i++)
		{
			Dust.NewDustPerfect(player.position, Main.rand.NextBool(3) ? 226 : 272, player.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.5f), 0, default(Color), Main.rand.NextFloat(2.1f, 2.9f)).noGravity = false;
		}
		int hitDirection = player.direction;
		if (player.velocity.X != 0f)
		{
			hitDirection = Math.Sign(player.velocity.X);
		}
		hitContext.HitDirection = hitDirection;
		hitContext.PlayerImmunityFrames = 12;
		hitContext.damageClass = player.GetBestClass();
		hitContext.BaseDamage = GodSlayerChestplate.DashDamage;
		hitContext.BaseKnockback = GodSlayerChestplate.DashKnockback;
		hitContext.PlayerImmunityFrames = GodSlayerChestplate.DashIFrames;
		npc.AddBuff(ModContent.BuffType<GodSlayerInferno>(), GodSlayerChestplate.DashGodSlayerInfernoDuration);
	}
}
