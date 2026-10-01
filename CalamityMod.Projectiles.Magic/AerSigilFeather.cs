using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AerSigilFeather : ModProjectile, ILocalizedModType, IModType
{
	private float extraRotation;

	private float intervalModifier = 1f;

	private bool randomized;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 151;
		base.Projectile.height = 205;
		base.Projectile.scale = 0.15f;
		base.Projectile.friendly = false;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 170;
	}

	public override void AI()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 50)
		{
			base.Projectile.alpha = (int)(255f - 5.1f * (float)base.Projectile.timeLeft);
		}
		float blendProgress = (170f - (float)base.Projectile.timeLeft) / 70f;
		blendProgress = Math.Min(blendProgress, 1f);
		if (!randomized)
		{
			extraRotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			intervalModifier = Main.rand.NextFloat(0.815f, 1.225f);
			randomized = true;
		}
		Vector2 newFeatherVelocity = Vector2.Zero;
		newFeatherVelocity.Y = base.Projectile.velocity.Y * 0.96f + 0.04f;
		newFeatherVelocity.X = 1.4f * (float)Math.Cos((float)base.Projectile.timeLeft * 0.035f * intervalModifier);
		float newFeatherRotation = extraRotation + (float)Math.Sin((float)base.Projectile.timeLeft * 0.035f);
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, newFeatherVelocity, blendProgress);
		base.Projectile.rotation = MathHelper.Lerp(base.Projectile.rotation, newFeatherRotation, blendProgress);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		lightColor = Color.LightGoldenrodYellow * 0.5f;
		return true;
	}
}
