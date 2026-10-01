using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Environment;

public class Lilyglow : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/ExtraTextures/TinyGreyscaleCircle";

	public ref float Direction => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.width = 360;
		base.Projectile.height = 360;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = 1;
		base.Projectile.scale = Main.rand?.NextFloat(0.03f, 0.055f) ?? 0.04f;
		Projectile projectile = base.Projectile;
		projectile.Size /= base.Projectile.scale;
	}

	public override void AI()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 120f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(600f, 540f, base.Projectile.timeLeft, clamped: true);
		if ((float)base.Projectile.timeLeft < 60f)
		{
			base.Projectile.scale *= 0.98f;
		}
		if (Collision.SolidCollision(base.Projectile.Center, 1, 1))
		{
			base.Projectile.timeLeft -= 8;
		}
		base.Projectile.Size = Vector2.One * 360f;
		base.Projectile.hide = base.Projectile.Opacity < 0.15f;
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy((float)Math.Cos((float)base.Projectile.timeLeft / 33f + (float)base.Projectile.identity) * 0.0283f);
		Point left = (base.Projectile.Center - Vector2.UnitX * 24f).ToTileCoordinates();
		Point right = (base.Projectile.Center + Vector2.UnitX * 24f).ToTileCoordinates();
		Point p = (base.Projectile.Center - Vector2.UnitY * 24f).ToTileCoordinates();
		Point bottom = (base.Projectile.Center + Vector2.UnitY * 24f).ToTileCoordinates();
		Color alpha = base.Projectile.GetAlpha(Color.White);
		DelegateMethods.v3_1 = ((Color)(ref alpha)).ToVector3();
		Utils.PlotLine(left, right, DelegateMethods.CastLight);
		Utils.PlotLine(p, bottom, DelegateMethods.CastLight);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		float fadeToPink = (float)Math.Pow((float)base.Projectile.identity % 8f / 8f, 0.93);
		Color c = Color.Lerp(Color.Yellow, new Color(255, 110, 105), fadeToPink) * base.Projectile.Opacity;
		((Color)(ref c)).A = (byte)base.Projectile.alpha;
		return c;
	}
}
