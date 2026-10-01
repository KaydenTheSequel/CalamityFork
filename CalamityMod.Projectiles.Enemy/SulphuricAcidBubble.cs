using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class SulphuricAcidBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.scale = 0.01f;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.Opacity = 0f;
		base.Projectile.timeLeft = 360;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 6)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.localAI[1] < 1f)
		{
			base.Projectile.localAI[1] += 0.01f;
			base.Projectile.scale += 0.01f;
			base.Projectile.width = (int)(30f * base.Projectile.scale);
			base.Projectile.height = (int)(30f * base.Projectile.scale);
		}
		else
		{
			base.Projectile.damage = 20;
			base.Projectile.width = 30;
			base.Projectile.height = 30;
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.localAI[0] > 2f)
		{
			base.Projectile.Opacity += 0.08f;
			if (base.Projectile.Opacity > 0.6f)
			{
				base.Projectile.Opacity = 0.6f;
			}
		}
		else
		{
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.ai[1] > 30f)
		{
			if (base.Projectile.velocity.Y > -2f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.05f;
			}
		}
		else
		{
			base.Projectile.ai[1]++;
		}
		if (base.Projectile.wet)
		{
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y * 0.98f;
			}
			if (base.Projectile.velocity.Y > -1f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.2f;
			}
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.localAI[1] >= 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !(base.Projectile.localAI[1] < 1f))
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 25; i++)
		{
			int toxicDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31);
			Main.dust[toxicDust].position = (Main.dust[toxicDust].position + base.Projectile.position) / 2f;
			Main.dust[toxicDust].velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			((Vector2)(ref Main.dust[toxicDust].velocity)).Normalize();
			Dust obj = Main.dust[toxicDust];
			obj.velocity *= (float)Main.rand.Next(1, 30) * 0.1f;
			Main.dust[toxicDust].alpha = 255 - (int)(base.Projectile.Opacity * 255f);
		}
	}
}
