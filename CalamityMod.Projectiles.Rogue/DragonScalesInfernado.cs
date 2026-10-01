using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DragonScalesInfernado : ModProjectile, ILocalizedModType, IModType
{
	private bool intersectingSomething;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Magic/InfernadoFriendly";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 320;
		base.Projectile.height = 88;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 200;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Generic;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			intersectingSomething = true;
		}
		float scaleBase = 44f;
		float scaleMult = 1.4f;
		float baseWidth = 320f;
		float baseHeight = 88f;
		if (Main.rand.NextBool(25))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 244, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
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
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.scale = (scaleBase - base.Projectile.ai[1]) * scaleMult / scaleBase;
			base.Projectile.ExpandHitboxBy((int)(baseWidth * base.Projectile.scale), (int)(baseHeight * base.Projectile.scale));
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[1] != -1f)
		{
			base.Projectile.scale = (scaleBase - base.Projectile.ai[1]) * scaleMult / scaleBase;
			base.Projectile.width = (int)(baseWidth * base.Projectile.scale);
			base.Projectile.height = (int)(baseHeight * base.Projectile.scale);
		}
		if (!intersectingSomething)
		{
			base.Projectile.alpha -= 30;
			if (base.Projectile.alpha < 100)
			{
				base.Projectile.alpha = 100;
			}
		}
		else
		{
			base.Projectile.alpha += 30;
			if (base.Projectile.alpha > 200)
			{
				base.Projectile.alpha = 200;
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
			center.Y -= baseHeight * base.Projectile.scale / 2f;
			float baseChanger = (scaleBase - base.Projectile.ai[1] + 1f) * scaleMult / scaleBase;
			center.Y -= baseHeight * baseChanger / 2f;
			center.Y += 2f;
			Projectile segment = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), center, base.Projectile.velocity, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 10f, base.Projectile.ai[1] - 1f);
			if (segment.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				segment.DamageType = base.Projectile.DamageType;
				segment.friendly = base.Projectile.friendly;
				segment.hostile = base.Projectile.hostile;
			}
		}
		if (base.Projectile.ai[0] <= 0f)
		{
			float smallWidth = (float)base.Projectile.width / 5f;
			smallWidth *= 2f;
			float xFluctuation = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)base.Projectile.ai[0])) - 0.5) * smallWidth;
			base.Projectile.position.X -= xFluctuation * (float)(-base.Projectile.direction);
			base.Projectile.ai[0]--;
			xFluctuation = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)base.Projectile.ai[0])) - 0.5) * smallWidth;
			base.Projectile.position.X += xFluctuation * (float)(-base.Projectile.direction);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (!intersectingSomething)
		{
			return new Color(95, 95, 19, 255 - base.Projectile.alpha);
		}
		return new Color(64, 64, 13, 255 - base.Projectile.alpha);
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
		int frameDraw = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = frameDraw * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, frameDraw), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)frameDraw / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
