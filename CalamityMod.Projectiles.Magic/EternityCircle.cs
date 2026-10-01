using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class EternityCircle : ModProjectile, ILocalizedModType, IModType
{
	public const float TargetOffsetRadius = 490f;

	public const float SinusoidalOffsetAngleIncrement = 0.54f;

	public static readonly float SinusoidalPositionAngleIncrement = MathHelper.ToRadians(3.5f);

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public int TargetNPCIndex
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float SinusoidalPositionAngle
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public float SinusoidalOffsetAngle
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public int HeldBookIndex
	{
		get
		{
			return (int)base.Projectile.localAI[1];
		}
		set
		{
			base.Projectile.localAI[1] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 310;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		NPC target = Main.npc[TargetNPCIndex];
		if (HeldBookIndex >= Main.projectile.Length || SinusoidalOffsetAngle < 0f)
		{
			base.Projectile.Kill();
			return;
		}
		if (!Main.projectile[HeldBookIndex].active)
		{
			base.Projectile.Kill();
			return;
		}
		if (!target.active || target.dontTakeDamage)
		{
			base.Projectile.active = false;
		}
		if (SinusoidalOffsetAngle == 0f)
		{
			SinusoidalOffsetAngle = Main.rand.NextFloat((float)Math.PI * 2f);
		}
		base.Projectile.position = target.Center + SinusoidalPositionAngle.ToRotationVector2() * 490f;
		SinusoidalPositionAngle += SinusoidalPositionAngleIncrement;
		SinusoidalOffsetAngle += 0.54f;
		float pulse = (float)Math.Sin(SinusoidalOffsetAngle);
		float radius = 8f;
		Vector2 offset = Vector2.UnitY * pulse * radius;
		Dust.NewDustPerfect(base.Projectile.Center + offset, 132, Vector2.Zero).noGravity = true;
		Dust.NewDustPerfect(base.Projectile.Center - offset, 133, Vector2.Zero).noGravity = true;
	}
}
