using System;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class NovaChargedShot : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle ChargeImpact = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeImpact")
	{
		Volume = 0.3f
	};

	public int Time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 20;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 24;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(-3f, 3f), ModContent.DustType<SquashDust>());
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(0.9f, 1.3f);
		dust.color = ArcNovaDiffuser.mainColor;
		dust.fadeIn = -0.4f;
		if (Time >= 120)
		{
			return;
		}
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), scale: Main.rand.NextFloat(0.6f, 0.75f), color: Main.rand.NextBool(3) ? Color.Chartreuse : ArcNovaDiffuser.mainColor, velocity: base.Projectile.velocity * 0.2f, texture: "CalamityMod/Particles/BloomCircle", affectedByGravity: false, lifetime: 50, stretch: new Vector2(0.2f, 1.4f), useAddativeBlend: true, glowCenter: true, extraRotation: 0f, fadeIn: false, affectedByLight: false, shrinkSpeed: 0.1f));
		}
		if (Time % 2 == 0)
		{
			for (int i = -1; i <= 1; i += 2)
			{
				float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.45f / (float)Math.PI);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 28f * (float)i, ModContent.DustType<SquashDust>(), base.Projectile.velocity * Main.rand.NextFloat(0.6f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.8f, 0.85f));
				dust2.noGravity = true;
				dust2.color = (Main.rand.NextBool(3) ? Color.Lime : ArcNovaDiffuser.mainColor);
				dust2.fadeIn = -0.55f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Color mainColor = ArcNovaDiffuser.mainColor;
		((Color)(ref mainColor)).A = 0;
		return mainColor;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 19; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), Utils.RotatedByRandom(new Vector2(0f, -18f), MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.8f, 2.3f);
			dust.color = ArcNovaDiffuser.mainColor;
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), Utils.RotatedByRandom(new Vector2(0f, -7f), MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust2.noGravity = false;
			dust2.scale = Main.rand.NextFloat(0.8f, 2.3f);
			dust2.color = Color.Lime;
		}
		SoundEngine.PlaySound(in ChargeImpact, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArcNovaDiffuser.mainColor, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.2f, 1.3f, 16, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lime, "CalamityMod/Particles/BloomCircle", new Vector2(1f, 1f), 0f, 0.8f, 0.1f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int j = 0; j < 2; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 16, 0.95f, ArcNovaDiffuser.mainColor, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0f, 1f, 0.9f));
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
		float critDamage = Math.Min(Main.player[base.Projectile.owner].GetTotalCritChance(base.Projectile.DamageType) * 0.01f, 1f);
		modifiers.SourceDamage *= 1f + critDamage;
	}

	public override bool? CanDamage()
	{
		return base.CanDamage();
	}
}
