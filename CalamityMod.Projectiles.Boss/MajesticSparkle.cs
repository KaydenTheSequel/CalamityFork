using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class MajesticSparkle : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 90;

	public const int FadeinTime = 18;

	public const int FadeoutTime = 18;

	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float ColorSpectrumHue => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 72;
		base.Projectile.height = 72;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 90;
		base.Projectile.scale = 0.001f;
	}

	public override void AI()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 1f)
		{
			base.Projectile.scale = Main.rand.NextFloat(0.3f, 0.75f);
			base.Projectile.ExpandHitboxBy((int)(base.Projectile.scale * 72f));
			ColorSpectrumHue = Main.rand.NextFloat(0f, 0.9999f);
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			base.Projectile.netUpdate = true;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.96f;
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp((float)Math.PI / 2f, 0.085f);
		ColorSpectrumHue = (ColorSpectrumHue + 0.0037f) % 0.999f;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 18f, Time, clamped: true) * Utils.GetLerpValue(90f, 72f, Time, clamped: true);
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(Math.Sin(Time / 30f) * 0.012500000186264515);
		Time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
		Color sparkleColor = Main.hslToRgb(ColorSpectrumHue, 1f, 0.5f) * base.Projectile.Opacity * 0.5f;
		((Color)(ref sparkleColor)).A = 0;
		sparkleColor *= MathHelper.Lerp(1f, 1.5f, Utils.GetLerpValue(30f, 60f, Time, clamped: true));
		Color orthogonalSparkleColor = Color.Lerp(sparkleColor, Color.White, 0.5f) * 0.5f;
		Vector2 origin = value.Size() * 0.5f;
		Vector2 sparkleScale = new Vector2(0.3f, 1f) * base.Projectile.Opacity * base.Projectile.scale;
		Vector2 orthogonalsparkleScale = new Vector2(0.3f, 2f) * base.Projectile.Opacity * base.Projectile.scale;
		Main.EntitySpriteDraw(value, drawPosition, null, sparkleColor, (float)Math.PI / 2f + base.Projectile.rotation, origin, orthogonalsparkleScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPosition, null, sparkleColor, base.Projectile.rotation, origin, sparkleScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPosition, null, orthogonalSparkleColor, (float)Math.PI / 2f + base.Projectile.rotation, origin, orthogonalsparkleScale * 0.6f, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPosition, null, orthogonalSparkleColor, base.Projectile.rotation, origin, sparkleScale * 0.6f, (SpriteEffects)0);
		return false;
	}
}
