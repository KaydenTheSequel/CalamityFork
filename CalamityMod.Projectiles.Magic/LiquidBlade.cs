using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class LiquidBlade : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/Magic/InfernalBlade";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.v3_1 = new Vector3(0.6f, 1f, 1f) * 0.2f;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * 10f, 8f, DelegateMethods.CastLightOpen);
		if (base.Projectile.alpha > 0)
		{
			SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.Center);
			base.Projectile.alpha = 0;
			base.Projectile.scale = 1.1f;
			base.Projectile.frame = Main.rand.Next(14);
			float dustLoopCheck = 16f;
			for (int dustIncr = 0; (float)dustIncr < dustLoopCheck; dustIncr++)
			{
				Vector2 dustRotate = Vector2.UnitX * 0f;
				dustRotate += -Vector2.UnitY.RotatedBy((float)dustIncr * ((float)Math.PI * 2f / dustLoopCheck)) * new Vector2(1f, 4f);
				dustRotate = dustRotate.RotatedBy(base.Projectile.velocity.ToRotation());
				int rainbow = Dust.NewDust(base.Projectile.Center, 0, 0, 66, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				Main.dust[rainbow].scale = 1.5f;
				Main.dust[rainbow].noGravity = true;
				Main.dust[rainbow].position = base.Projectile.Center + dustRotate;
				Main.dust[rainbow].velocity = base.Projectile.velocity * 0f + dustRotate.SafeNormalize(Vector2.UnitY) * 1f;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		int randDustAmt = Main.rand.Next(4, 10);
		for (int j = 0; j < randDustAmt; j++)
		{
			int rainbowDeath = Dust.NewDust(base.Projectile.Center, 0, 0, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
			Dust obj = Main.dust[rainbowDeath];
			obj.velocity *= 1.6f;
			obj.velocity.Y--;
			obj.velocity += -base.Projectile.velocity * (Main.rand.NextFloat() * 2f - 1f) * 0.5f;
			obj.scale = 2f;
			obj.fadeIn = 0.5f;
			obj.noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color colorArea = Lighting.GetColor((int)((double)base.Projectile.position.X + (double)base.Projectile.width * 0.5) / 16, (int)(((double)base.Projectile.position.Y + (double)base.Projectile.height * 0.5) / 16.0));
		Texture2D texture2D3 = TextureAssets.Projectile[base.Type].Value;
		int textureArea = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y3 = textureArea * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, y3, texture2D3.Width, textureArea);
		Vector2 halfRect = rectangle.Size() / 2f;
		((Rectangle)(ref rectangle))._002Ector(38 * base.Projectile.frame, 0, 38, 38);
		halfRect = rectangle.Size() / 2f;
		for (int i = 1; i < 3; i++)
		{
			Color alphaColor = colorArea;
			alphaColor = base.Projectile.GetAlpha(alphaColor);
			alphaColor *= (float)(3 - i) / ((float)ProjectileID.Sets.TrailCacheLength[base.Type] * 1.5f);
			Vector2 oldPosition = base.Projectile.oldPos[i];
			float projRotation = base.Projectile.rotation;
			SpriteEffects effects = spriteEffects;
			if (ProjectileID.Sets.TrailingMode[base.Type] == 2)
			{
				projRotation = base.Projectile.oldRot[i];
				effects = (SpriteEffects)(base.Projectile.oldSpriteDirection[i] == -1);
			}
			Main.spriteBatch.Draw(texture2D3, oldPosition + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, alphaColor, projRotation + base.Projectile.rotation * 0f * (float)(i - 1) * (0f - (float)((Enum)spriteEffects).HasFlag((Enum)(object)(SpriteEffects)1).ToDirectionInt()), halfRect, MathHelper.Lerp(base.Projectile.scale, 8f, (float)i / 15f), effects, 0f);
		}
		Color alphaColorAgain = base.Projectile.GetAlpha(colorArea);
		Main.spriteBatch.Draw(texture2D3, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, alphaColorAgain, base.Projectile.rotation, halfRect, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center);
	}

	private void OnHitEffects(Vector2 targetPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			CalamityUtils.ProjectileBarrage(base.Projectile.GetSource_FromThis(), base.Projectile.Center, targetPos, Main.rand.NextBool(), 800f, 800f, 0f, 800f, 10f, ModContent.ProjectileType<LiquidBlade2>(), (int)((double)base.Projectile.damage * 0.75), 1f, base.Projectile.owner, clamped: true);
		}
	}
}
