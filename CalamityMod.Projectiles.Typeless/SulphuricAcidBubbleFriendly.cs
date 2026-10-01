using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SulphuricAcidBubbleFriendly : ModProjectile, ILocalizedModType, IModType
{
	private bool fromArmour;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Enemy/SulphuricAcidBubble";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.scale = 0.1f;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 360;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		if (base.Projectile.ai[0] == 1f)
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.scale = 1f;
			fromArmour = true;
		}
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
			if (base.Projectile.scale < 1f || (fromArmour && base.Projectile.scale < 1.8f))
			{
				base.Projectile.scale += 0.02f;
			}
			base.Projectile.width = (int)(30f * base.Projectile.scale);
			base.Projectile.height = (int)(30f * base.Projectile.scale);
		}
		else
		{
			base.Projectile.width = (fromArmour ? base.Projectile.width : 30);
			base.Projectile.height = (fromArmour ? base.Projectile.height : 30);
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.localAI[0] > 2f)
		{
			base.Projectile.alpha -= 20;
			if (base.Projectile.alpha < 100)
			{
				base.Projectile.alpha = 100;
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
		if (!(base.Projectile.localAI[1] < 1f))
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), fromArmour ? 150 : 120);
			base.Projectile.Kill();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (!(base.Projectile.localAI[1] < 1f))
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), fromArmour ? 150 : 120);
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.position);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		if (base.Projectile.Calamity().stealthStrike)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 303, Utils.RotatedByRandom(new Vector2(8f, 8f), 6.2831854820251465) * Main.rand.NextFloat(0.05f, 0.8f));
				dust.scale = Main.rand.NextFloat(0.75f, 0.95f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? Color.YellowGreen : Color.OliveDrab);
			}
			return;
		}
		for (int i = 0; i < 25; i++)
		{
			int toxicDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31);
			Main.dust[toxicDust].position = (Main.dust[toxicDust].position + base.Projectile.position) / 2f;
			Main.dust[toxicDust].velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			((Vector2)(ref Main.dust[toxicDust].velocity)).Normalize();
			Dust obj = Main.dust[toxicDust];
			obj.velocity *= (float)Main.rand.Next(1, 30) * 0.1f;
			Main.dust[toxicDust].alpha = base.Projectile.alpha;
		}
	}
}
