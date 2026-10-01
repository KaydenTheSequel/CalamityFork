using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SepulcherSoul : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.Opacity = 0f;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 120;
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
			base.Projectile.localAI[0] = 1f;
		}
		Vector2 idealVelocity = Vector2.Zero;
		idealVelocity.X = (float)(Math.Sin(Time / 27f + (float)base.Projectile.identity * 1.1f) + (double)(float)Math.Cos(Math.E * (double)(Time / 27f + (float)base.Projectile.identity * 1.1f))) * 4f;
		idealVelocity.Y = MathHelper.SmoothStep(-3f, -9f, (float)Math.Sin(Time / 23f + (float)base.Projectile.identity * 1.1f) * 0.5f + 0.5f);
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, 0.075f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 15f, Time, clamped: true) * Utils.GetLerpValue(0f, 25f, base.Projectile.timeLeft, clamped: true);
		Time++;
	}
}
