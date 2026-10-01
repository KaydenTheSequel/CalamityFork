using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MagneticOrb : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 120;

	private const float FramesPerBeam = 12f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 50;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 40)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.882f;
		}
		if (base.Projectile.timeLeft == 120)
		{
			base.Projectile.localAI[1] = Main.rand.NextFloat(0f, 12f);
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.Y) + Math.Abs(base.Projectile.velocity.X)) * 0.001f;
		}
		else
		{
			base.Projectile.rotation -= (Math.Abs(base.Projectile.velocity.Y) + Math.Abs(base.Projectile.velocity.X)) * 0.001f;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 4)
			{
				base.Projectile.frame = 0;
			}
		}
		NPC target = base.Projectile.Center.ClosestNPCAt(550f);
		if ((base.Projectile.timeLeft == 30 || base.Projectile.timeLeft == 10) && target != null)
		{
			CalamityUtils.MagnetSphereHitscan(base.Projectile, Vector2.Distance(base.Projectile.Center, target.Center), 8f, 0f, 1, ModContent.ProjectileType<MagneticBeam>(), 1.0, attackMultiple: true);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 30)
		{
			float timeAlpha = (float)base.Projectile.timeLeft / 30f;
			base.Projectile.alpha = (int)(255f - 255f * timeAlpha);
		}
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 0);
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
}
