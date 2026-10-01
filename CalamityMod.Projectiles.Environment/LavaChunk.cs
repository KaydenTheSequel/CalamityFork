using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Environment;

public class LavaChunk : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 360;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.localAI[1] < 1f)
		{
			base.Projectile.localAI[1] += 0.002f;
			base.Projectile.scale -= 0.002f;
			base.Projectile.width = (int)(18f * base.Projectile.scale);
			base.Projectile.height = (int)(18f * base.Projectile.scale);
		}
		else
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.scale > 0.25f)
		{
			for (int i = 0; i < 2; i++)
			{
				float dustYOffset = 0f;
				if (i == 1)
				{
					dustYOffset = base.Projectile.velocity.Y * 0.5f;
				}
				int lavaDust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 3f + dustYOffset) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 6, 0f, 0f, 100, default(Color), base.Projectile.scale);
				Main.dust[lavaDust].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
				Dust obj = Main.dust[lavaDust];
				obj.velocity *= 0.2f;
				Main.dust[lavaDust].noGravity = true;
				lavaDust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 3f + dustYOffset) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 31, 0f, 0f, 100, default(Color), base.Projectile.scale * 0.5f);
				Main.dust[lavaDust].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
				Dust obj2 = Main.dust[lavaDust];
				obj2.velocity *= 0.05f;
			}
		}
		else
		{
			base.Projectile.damage = 0;
		}
		if (base.Projectile.velocity.Y < 6f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.05f;
		}
		if (base.Projectile.wet)
		{
			if (base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y * 0.98f;
			}
			if (base.Projectile.velocity.Y < 0.5f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.01f;
			}
		}
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
