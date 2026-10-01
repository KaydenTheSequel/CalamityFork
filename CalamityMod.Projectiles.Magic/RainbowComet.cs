using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RainbowComet : ModProjectile, ILocalizedModType, IModType
{
	public const float FadeinTime = 40f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 72;
		base.Projectile.height = 72;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 20f, Time, clamped: true);
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(Math.Sin(Time / 30f) * 0.012500000186264515);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 4 == 3)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Texture2D cometTexture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(cometTexture, base.Projectile.Center + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition, cometTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame), Color.White * base.Projectile.Opacity, base.Projectile.rotation, cometTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 18; i++)
			{
				Vector2 velocity = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(9f, 20f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<PartySparkle>(), base.Projectile.damage, 1f, base.Projectile.owner);
			}
			for (int j = 0; j < 7; j++)
			{
				Vector2 velocity2 = -base.Projectile.velocity.SafeNormalize(-Vector2.UnitY);
				velocity2 = velocity2.RotatedBy(MathHelper.Lerp(-1.1f, 1.1f, (float)j / 7f));
				velocity2 *= Main.rand.NextFloat(7f, 15f);
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, ModContent.ProjectileType<RainbowRocket>(), base.Projectile.damage * 3, 1f, base.Projectile.owner).ai[1] = j;
			}
		}
		if (!Main.dedServ)
		{
			for (int k = 0; k < 80; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 263);
				dust.color = CalamityUtils.MulticolorLerp((Main.rand.NextFloat(0.4f) + Main.GlobalTimeWrappedHourly * 0.4f) % 0.999f, RainbowPartyCannon.ColorSet);
				dust.scale = Main.rand.NextFloat(0.6f, 0.9f);
				dust.velocity = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(12f, 16f);
				dust.noGravity = true;
			}
			SoundStyle style = CommonCalamitySounds.LargeWeaponFireSound with
			{
				Volume = 0.45f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}
}
