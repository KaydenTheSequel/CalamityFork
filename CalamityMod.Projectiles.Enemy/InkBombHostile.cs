using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class InkBombHostile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 150;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		if (!(base.Projectile.ai[0] > 10f))
		{
			return;
		}
		base.Projectile.ai[0] = 10f;
		if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
			if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
			{
				base.Projectile.velocity.X = 0f;
				base.Projectile.netUpdate = true;
			}
		}
		base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.01f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)tex.Width * 0.5f, (float)base.Projectile.height * 0.5f);
		Vector2 vector = new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y) - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, tex.Height / Main.projFrames[base.Type] * base.Projectile.frame, tex.Width, tex.Height / Main.projFrames[base.Type]);
		Main.EntitySpriteDraw(tex, vector, rectangle, Color.DarkGray, base.Projectile.rotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath28, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			int randProjAmt = Main.rand.Next(5, 9);
			Vector2 randProjRotation = default(Vector2);
			for (int i = 0; i < randProjAmt; i++)
			{
				((Vector2)(ref randProjRotation))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				((Vector2)(ref randProjRotation)).Normalize();
				randProjRotation *= (float)Main.rand.Next(50, 401) * 0.01f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, randProjRotation.X, randProjRotation.Y, ModContent.ProjectileType<InkPoisonCloud>(), (int)Math.Round((double)base.Projectile.damage * 0.165), 1f, base.Projectile.owner, Main.rand.Next(-45, 1));
			}
		}
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int j = 0; j < 10; j++)
		{
			int inkDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 54, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[inkDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[inkDust].scale = 0.5f;
				Main.dust[inkDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int k = 0; k < 15; k++)
		{
			int inkDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 109, 0f, 0f, 100, default(Color), 3f);
			Main.dust[inkDust2].noGravity = true;
			Dust obj2 = Main.dust[inkDust2];
			obj2.velocity *= 5f;
			inkDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 109, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[inkDust2];
			obj3.velocity *= 2f;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 12f, targetHitbox);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(22, 300);
			base.Projectile.Kill();
		}
	}
}
