using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ScourgeoftheCosmosMini : ModProjectile, ILocalizedModType, IModType
{
	private int bounce = 3;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 375;
		base.Projectile.extraUpdates = 4;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 270 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 50;
		}
		else
		{
			base.Projectile.extraUpdates = 1;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 1)
		{
			base.Projectile.frame = 0;
		}
		for (int i = 0; i < 1; i++)
		{
			int dustType = (Main.rand.NextBool(3) ? 56 : 242);
			float dustX = base.Projectile.velocity.X / 3f * (float)i;
			float dustY = base.Projectile.velocity.Y / 3f * (float)i;
			int scourgeDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
			Dust obj = Main.dust[scourgeDust];
			obj.position.X = base.Projectile.Center.X - dustX;
			obj.position.Y = base.Projectile.Center.Y - dustY;
			obj.velocity *= 0f;
			obj.scale = 0.5f;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) - (float)Math.PI / 2f;
		float projX = base.Projectile.position.X;
		float projY = base.Projectile.position.Y;
		float homingRange = 100000f;
		bool isHoming = false;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 30f)
		{
			base.Projectile.ai[0] = 30f;
			for (int enemy = 0; enemy < Main.maxNPCs; enemy++)
			{
				if (Main.npc[enemy].CanBeChasedBy(base.Projectile))
				{
					float enemyX = Main.npc[enemy].position.X + (float)(Main.npc[enemy].width / 2);
					float enemyY = Main.npc[enemy].position.Y + (float)(Main.npc[enemy].height / 2);
					float enemyDistance = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - enemyX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - enemyY);
					if (enemyDistance < 800f && enemyDistance < homingRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.npc[enemy].position, Main.npc[enemy].width, Main.npc[enemy].height))
					{
						homingRange = enemyDistance;
						projX = enemyX;
						projY = enemyY;
						isHoming = true;
					}
				}
			}
		}
		if (!isHoming)
		{
			projX = base.Projectile.position.X + (float)(base.Projectile.width / 2) + base.Projectile.velocity.X * 100f;
			projY = base.Projectile.position.Y + (float)(base.Projectile.height / 2) + base.Projectile.velocity.Y * 100f;
		}
		float projVelModifier = 0.16f;
		Vector2 projDirection = default(Vector2);
		((Vector2)(ref projDirection))._002Ector(base.Projectile.position.X + (float)base.Projectile.width * 0.5f, base.Projectile.position.Y + (float)base.Projectile.height * 0.5f);
		float projDirectX = projX - projDirection.X;
		float projDirectY = projY - projDirection.Y;
		float projDistance = (float)Math.Sqrt(projDirectX * projDirectX + projDirectY * projDirectY);
		projDistance = 10f / projDistance;
		projDirectX *= projDistance;
		projDirectY *= projDistance;
		if (base.Projectile.velocity.X < projDirectX)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X + projVelModifier;
			if (base.Projectile.velocity.X < 0f && projDirectX > 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + projVelModifier * 2f;
			}
		}
		else if (base.Projectile.velocity.X > projDirectX)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X - projVelModifier;
			if (base.Projectile.velocity.X > 0f && projDirectX < 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - projVelModifier * 2f;
			}
		}
		if (base.Projectile.velocity.Y < projDirectY)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + projVelModifier;
			if (base.Projectile.velocity.Y < 0f && projDirectY > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + projVelModifier * 2f;
			}
		}
		else if (base.Projectile.velocity.Y > projDirectY)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - projVelModifier;
			if (base.Projectile.velocity.Y > 0f && projDirectY < 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - projVelModifier * 2f;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		bounce--;
		if (bounce <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(9f, 10f);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/ScourgeoftheCosmosMiniGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
	}
}
