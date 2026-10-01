using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BrinySpout : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 150;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 60;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 8;
	}

	public override void AI()
	{
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		int projScale = 32;
		float scaleModifier = 1.5f;
		int projWidth = 150;
		int projHeight = 42;
		if (base.Projectile.velocity.X != 0f)
		{
			base.Projectile.direction = (base.Projectile.spriteDirection = -Math.Sign(base.Projectile.velocity.X));
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 2)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 6)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
			base.Projectile.scale = ((float)projScale - base.Projectile.ai[1]) * scaleModifier / (float)projScale;
			base.Projectile.width = (int)((float)projWidth * base.Projectile.scale);
			base.Projectile.height = (int)((float)projHeight * base.Projectile.scale);
			base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[1] != -1f)
		{
			base.Projectile.scale = ((float)projScale - base.Projectile.ai[1]) * scaleModifier / (float)projScale;
			base.Projectile.width = (int)((float)projWidth * base.Projectile.scale);
			base.Projectile.height = (int)((float)projHeight * base.Projectile.scale);
		}
		if (!Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.alpha -= 3;
			if (base.Projectile.alpha < 60)
			{
				base.Projectile.alpha = 60;
			}
		}
		else
		{
			base.Projectile.alpha += 3;
			if (base.Projectile.alpha > 150)
			{
				base.Projectile.alpha = 150;
			}
		}
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
		}
		if (base.Projectile.ai[0] == 1f && base.Projectile.ai[1] > 0f && base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.netUpdate = true;
			Vector2 center = base.Projectile.Center;
			center.Y -= (float)projHeight * base.Projectile.scale / 2f;
			float nextSegment = ((float)projScale - base.Projectile.ai[1] + 1f) * scaleModifier / (float)projScale;
			center.Y -= (float)projHeight * nextSegment / 2f;
			center.Y += 2f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), center.X, center.Y, base.Projectile.velocity.X, base.Projectile.velocity.Y, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 8f, base.Projectile.ai[1] - 1f);
		}
		if (base.Projectile.ai[0] <= 0f)
		{
			float smolWidth = (float)base.Projectile.width / 5f;
			smolWidth *= 2f;
			float projXChange = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)base.Projectile.ai[0])) - 0.5) * smolWidth;
			base.Projectile.position.X = base.Projectile.position.X - projXChange * (0f - (float)base.Projectile.direction);
			base.Projectile.ai[0]--;
			projXChange = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)base.Projectile.ai[0])) - 0.5) * smolWidth;
			base.Projectile.position.X = base.Projectile.position.X + projXChange * (0f - (float)base.Projectile.direction);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(53, Main.DiscoG, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
	}
}
