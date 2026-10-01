using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PartySparkle : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 90;

	public const int FadeinTime = 18;

	public const int FadeoutTime = 18;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float ColorSpectrumHue
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

	public override void SetDefaults()
	{
		base.Projectile.width = 72;
		base.Projectile.height = 72;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 90;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.scale = 0.001f;
	}

	public override void AI()
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 1f)
		{
			base.Projectile.scale = Main.rand.NextFloat(0.4f, 1.1f);
			base.Projectile.ExpandHitboxBy((int)(72f * base.Projectile.scale));
			ColorSpectrumHue = Main.rand.NextFloat(0f, 0.9999f);
			base.Projectile.netUpdate = true;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		Time++;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.96f;
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp((float)Math.PI / 2f, 0.085f);
		ColorSpectrumHue = (ColorSpectrumHue + 0.0037f) % 0.999f;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 18f, Time, clamped: true) * Utils.GetLerpValue(90f, 72f, Time, clamped: true);
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(Math.Sin(Time / 30f) * 0.012500000186264515);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Color sparkleColor = CalamityUtils.MulticolorLerp(ColorSpectrumHue, RainbowPartyCannon.ColorSet) * base.Projectile.Opacity * 0.5f;
		((Color)(ref sparkleColor)).A = 0;
		sparkleColor *= MathHelper.Lerp(1f, 1.5f, Utils.GetLerpValue(30f, 60f, Time, clamped: true));
		Color orthogonalsparkleColor = Color.Lerp(sparkleColor, Color.White, 0.5f) * 0.5f;
		Vector2 origin = value.Size() / 2f;
		Vector2 sparkleScale = new Vector2(0.3f, 1f) * base.Projectile.Opacity * base.Projectile.scale;
		Vector2 orthogonalsparkleScale = new Vector2(0.3f, 2f) * base.Projectile.Opacity * base.Projectile.scale;
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, null, sparkleColor, (float)Math.PI / 2f + base.Projectile.rotation, origin, orthogonalsparkleScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, null, sparkleColor, base.Projectile.rotation, origin, sparkleScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, null, orthogonalsparkleColor, (float)Math.PI / 2f + base.Projectile.rotation, origin, orthogonalsparkleScale * 0.6f, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, null, orthogonalsparkleColor, base.Projectile.rotation, origin, sparkleScale * 0.6f, (SpriteEffects)0);
		return false;
	}
}
