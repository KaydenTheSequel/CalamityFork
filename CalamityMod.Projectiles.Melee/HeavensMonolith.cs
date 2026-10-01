using System;
using System.Collections.Generic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class HeavensMonolith : ModProjectile, ILocalizedModType, IModType
{
	public const float BaseWidth = 90f;

	public const float BaseHeight = 420f;

	public CalamityUtils.CurveSegment StaySmall = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0f, 0.2f, 0f);

	public CalamityUtils.CurveSegment GoBig = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineOut, 0.25f, 0.2f, 1f);

	public CalamityUtils.CurveSegment GoNormal = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0.4f, 1.2f, -0.2f);

	public CalamityUtils.CurveSegment StayNormal = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.5f, 1f, 0f);

	public CalamityUtils.CurveSegment Anticipate = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0f, 0f, 0.15f);

	public CalamityUtils.CurveSegment Overextend = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineOut, 0.2f, 0.15f, 1f);

	public CalamityUtils.CurveSegment Unextend = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0.25f, 1.15f, -0.15f);

	public CalamityUtils.CurveSegment Hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpOut, 0.7f, 1f, -0.1f);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMonolith";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC Target => Main.npc[(int)base.Projectile.ai[2]];

	public float Timer => (100f - (float)base.Projectile.timeLeft) / 100f;

	public ref float Variant => ref base.Projectile.ai[0];

	public ref float Scale => ref base.Projectile.ai[1];

	internal float Width()
	{
		return CalamityUtils.PiecewiseAnimation(Timer, StaySmall, GoBig, GoNormal, StayNormal) * 90f;
	}

	internal float Height()
	{
		return CalamityUtils.PiecewiseAnimation(Timer, Anticipate, Overextend, Unextend, Hold) * 420f;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 102;
		base.Projectile.scale = Scale;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 20;
		base.Projectile.hide = true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * Height(), Width(), ref collisionPoint);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 100)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			base.Projectile.velocity = Vector2.Zero;
			for (int i = 0; i < 20; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(7.5f, 20f), Color.White, Main.rand.NextBool() ? Color.MediumTurquoise : Color.DarkOrange, 0.1f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 3f));
			}
		}
		if (!Target.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = Target.Center;
		if (base.Projectile.timeLeft == 80)
		{
			Vector2 particleDirection = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
			SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalDryadTouch, base.Projectile.Center);
			for (int j = 0; j < 8; j++)
			{
				Vector2 hitPositionDisplace = particleDirection.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(0f, 10f);
				Vector2 flyDirection = particleDirection.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)) * Main.rand.NextFloat(5f, 15f);
				GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(base.Projectile.Center + hitPositionDisplace, flyDirection, Color.Lerp(Color.DarkOrange, Color.MediumTurquoise, Main.rand.NextFloat()), new Color(130, 130, 130), Main.rand.NextFloat(2.8f, 3.6f), 165 - Main.rand.Next(30), 0.1f));
			}
			for (int k = 0; k < 6; k++)
			{
				Vector2 hitPositionDisplace2 = particleDirection.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(10f, 30f);
				Vector2 flyDirection2 = particleDirection.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f));
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(base.Projectile.Center + hitPositionDisplace2 * 3f, flyDirection2 * Main.rand.NextFloat(3f, 6f), Color.Lerp(Color.DarkSlateBlue, Color.LightSlateGray, Main.rand.NextFloat()), 1f + Main.rand.NextFloat(0f, 2.4f), 30 + Main.rand.Next(50), 0.1f));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMonolith", (AssetRequestMode)2).Value;
		float drawAngle = base.Projectile.rotation;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector((int)Variant * 94, 0, 94, 420);
		Vector2 drawScale = new Vector2(Width() / 90f, Height() / 420f) * Scale;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition - (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 26f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)frame.Width / 2f, (float)frame.Height);
		float opacity = MathHelper.Clamp(1f - (Timer - 0.85f) / 0.15f, 0f, 1f);
		Main.EntitySpriteDraw(value, drawPosition, frame, lightColor * opacity, drawAngle, drawOrigin, drawScale, (SpriteEffects)0);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMonolith_Glow", (AssetRequestMode)2).Value;
		float drawAngle = base.Projectile.rotation;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector((int)Variant * 94, 0, 94, 420);
		Vector2 drawScale = new Vector2(Width() / 90f, Height() / 420f) * Scale;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition - (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 26f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)frame.Width / 2f, (float)frame.Height);
		float opacity = MathHelper.Clamp(1f - (Timer - 0.85f) / 0.15f, 0f, 1f);
		Main.EntitySpriteDraw(value, drawPosition, frame, Color.White * opacity, drawAngle, drawOrigin, drawScale, (SpriteEffects)0);
	}
}
