using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StormMark : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 900;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 25;
	}

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		Color newColor3 = default(Color);
		((Color)(ref newColor3))._002Ector(255, 255, 255);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = -1;
			SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.Center);
		}
		if (base.Projectile.localAI[1] < 30f)
		{
			Vector2 dustMovement = default(Vector2);
			Vector2 dustMovement2 = default(Vector2);
			for (int i = 0; i < 1; i++)
			{
				float lerpvalue1 = -0.5f;
				float lerpvalue2 = 0.9f;
				float randomLerp = Main.rand.NextFloat();
				((Vector2)(ref dustMovement))._002Ector(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(lerpvalue1, lerpvalue2, randomLerp));
				dustMovement.X *= MathHelper.Lerp(2.2f, 0.6f, randomLerp);
				dustMovement.X *= -1f;
				((Vector2)(ref dustMovement2))._002Ector(2f, 10f);
				Vector2 position4 = base.Projectile.Center + new Vector2(60f, 200f) * dustMovement * 0.5f + dustMovement2;
				Dust stormy = Main.dust[Dust.NewDust(position4, 0, 0, 187, 0f, 0f, 0, default(Color), 0.5f)];
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
				Dust obj = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 187, 0f, 0f, 225, newColor3)];
				obj.noGravity = true;
				obj.noLight = true;
				obj.scale = base.Projectile.Opacity * base.Projectile.localAI[0];
				obj.position = base.Projectile.Center;
				obj.velocity = dustVel * 2.5f;
			}
		}
		for (int num1136 = 0; num1136 < 2; num1136++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 dustVel2 = Vector2.UnitY.RotatedBy((float)num1136 * (float)Math.PI);
				Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 187, 0f, 0f, 225, newColor3, 1.5f)];
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
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Tornado>(), base.Projectile.damage, 3f, base.Projectile.owner);
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
