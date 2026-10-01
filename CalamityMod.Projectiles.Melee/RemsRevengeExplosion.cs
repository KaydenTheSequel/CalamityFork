using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class RemsRevengeExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 80);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 20;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerStomp", 2);
			style.Pitch = -0.75f;
			style.PitchVariance = 0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(Color.DarkRed, Color.Red, Utils.GetLerpValue(0f, 5f, i, clamped: true)), "CalamityMod/Particles/DetailedExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.5f + 0.03f * (float)i, (int)(20f - (float)i * 1.5f), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int j = 0; j < 3; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(Color.DarkRed, Color.Red, Utils.GetLerpValue(0f, 3f, j, clamped: true)), "CalamityMod/Projectiles/FireProj", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 2f + 0.22f * (float)j, (int)(20f - (float)j * 2f), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.RosyBrown, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.1f, 2f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int p = 0; p < 16; p++)
			{
				Vector2 velocity = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(8f, 12f);
				float scale = Main.rand.NextFloat(0.6f, 2f);
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, velocity, 30, scale, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed));
			}
		}
		base.Projectile.scale = MathHelper.Lerp(0f, 1f, CalamityUtils.PiecewiseAnimation(Time / 20f, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4)));
		Time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Laceration>(), 60);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (Owner.Center.X < target.Center.X).ToDirectionInt();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * base.Projectile.scale, targetHitbox);
	}
}
