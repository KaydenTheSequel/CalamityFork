using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class StormMarkHostile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 900;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		Color newColor3 = default(Color);
		((Color)(ref newColor3))._002Ector(255, 255, 255);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = -1;
			SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.Center);
		}
		if (base.Projectile.localAI[1] < 30f)
		{
			int totalDust = ((base.Projectile.ai[0] == 0f) ? 1 : 2);
			Vector2 dustMovement = default(Vector2);
			Vector2 dustMovement2 = default(Vector2);
			for (int i = 0; i < totalDust; i++)
			{
				float lerpvalue = -0.5f;
				float lerpvalue2 = 0.9f;
				float randomLerp = Main.rand.NextFloat();
				((Vector2)(ref dustMovement))._002Ector(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(lerpvalue, lerpvalue2, randomLerp));
				dustMovement.X *= MathHelper.Lerp(2.2f, 0.6f, randomLerp);
				dustMovement.X *= -1f;
				((Vector2)(ref dustMovement2))._002Ector(2f, 10f);
				Vector2 position4 = base.Projectile.Center + new Vector2(60f, (base.Projectile.ai[0] != 0f) ? 800f : 200f) * dustMovement * 0.5f + dustMovement2;
				Dust stormy = Main.dust[Dust.NewDust(position4, 0, 0, 16, 0f, 0f, 0, default(Color), 0.5f)];
				stormy.position = position4;
				stormy.customData = base.Projectile.Center + dustMovement2;
				stormy.fadeIn = 1f;
				stormy.scale = 0.3f;
				if (dustMovement.X > -1.2f)
				{
					stormy.velocity.X = 1f + Main.rand.NextFloat();
				}
				stormy.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
			}
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 0.8f;
			base.Projectile.direction = 1;
			Point projCenter = base.Projectile.Center.ToTileCoordinates();
			base.Projectile.Center = new Vector2((float)(projCenter.X * 16 + 8), (float)(projCenter.Y * 16 + 8));
		}
		base.Projectile.rotation = base.Projectile.localAI[1] / 40f * ((float)Math.PI * 2f) * (float)base.Projectile.direction;
		if (base.Projectile.localAI[1] < 33f)
		{
			if (base.Projectile.alpha > 0)
			{
				base.Projectile.alpha -= 8;
			}
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		if (base.Projectile.localAI[1] > 103f)
		{
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha += 16;
			}
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.alpha = 255;
			}
		}
		if (base.Projectile.alpha == 0)
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref newColor3)).ToVector3() * 0.5f);
		}
		for (int j = 0; j < 2; j++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 dustVel = Vector2.UnitY.RotatedBy((float)j * (float)Math.PI).RotatedBy(base.Projectile.rotation);
				Dust obj = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 16, 0f, 0f, 225, newColor3)];
				obj.noGravity = true;
				obj.noLight = true;
				obj.scale = base.Projectile.Opacity * base.Projectile.localAI[0];
				obj.position = base.Projectile.Center;
				obj.velocity = dustVel * 2.5f;
			}
		}
		for (int k = 0; k < 2; k++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 dustVel2 = Vector2.UnitY.RotatedBy((float)k * (float)Math.PI);
				Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 16, 0f, 0f, 225, newColor3, 1.5f)];
				obj2.noGravity = true;
				obj2.noLight = true;
				obj2.scale = base.Projectile.Opacity * base.Projectile.localAI[0];
				obj2.position = base.Projectile.Center;
				obj2.velocity = dustVel2 * 2.5f;
			}
		}
		if (base.Projectile.localAI[1] < 33f || base.Projectile.localAI[1] > 87f)
		{
			base.Projectile.scale = base.Projectile.Opacity / 2f * base.Projectile.localAI[0];
		}
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] == 60f && base.Projectile.owner == Main.myPlayer)
		{
			int projectileDamage = ((base.Projectile.ai[0] != 0f) ? ((int)base.Projectile.ai[0]) : (Main.masterMode ? 21 : (Main.expertMode ? 25 : 40)));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<TornadoHostile>(), projectileDamage, 3f, base.Projectile.owner, 0f, base.Projectile.ai[1]);
		}
		if (base.Projectile.localAI[1] >= 120f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		Color originalColor = Lighting.GetColor((int)((double)base.Projectile.position.X + (double)base.Projectile.width * 0.5) / 16, (int)(((double)base.Projectile.position.Y + (double)base.Projectile.height * 0.5) / 16.0));
		Vector2 drawPos = base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Texture2D texture2D27 = TextureAssets.Projectile[base.Type].Value;
		Rectangle rectangl = texture2D27.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Color alphaColor = base.Projectile.GetAlpha(originalColor);
		Vector2 halfRect = rectangl.Size() / 2f;
		Color whiteColor = Main.hslToRgb(0.25f, 1f, 1f).MultiplyRGBA(new Color(255, 255, 255, 0));
		Main.spriteBatch.Draw(texture2D27, drawPos, (Rectangle?)rectangl, whiteColor, 0f, halfRect, new Vector2(1f, 5f) * base.Projectile.scale * 2f, (SpriteEffects)0, 0f);
		Main.spriteBatch.Draw(texture2D27, drawPos, (Rectangle?)rectangl, alphaColor, base.Projectile.rotation, halfRect, base.Projectile.scale, (SpriteEffects)0, 0f);
		Main.spriteBatch.Draw(texture2D27, drawPos, (Rectangle?)rectangl, alphaColor, 0f, halfRect, new Vector2(1f, 8f) * base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
