using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SolsticeBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.5f, 0.5f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.position);
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale -= 0.02f;
			base.Projectile.alpha += 30;
			if (base.Projectile.alpha >= 250)
			{
				base.Projectile.alpha = 255;
				base.Projectile.localAI[0] = 1f;
			}
		}
		else if (base.Projectile.localAI[0] == 1f)
		{
			base.Projectile.scale += 0.02f;
			base.Projectile.alpha -= 30;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.alpha = 0;
				base.Projectile.localAI[0] = 0f;
			}
		}
		int dustType = 0;
		switch (CalamityMod.CurrentSeason)
		{
		case Season.Spring:
			dustType = Utils.SelectRandom<int>(Main.rand, 74, 157, 107);
			break;
		case Season.Summer:
			dustType = Utils.SelectRandom<int>(Main.rand, 247, 228, 57);
			break;
		case Season.Fall:
			dustType = Utils.SelectRandom<int>(Main.rand, 6, 259, 158);
			break;
		case Season.Winter:
			dustType = Utils.SelectRandom<int>(Main.rand, 67, 229, 185);
			break;
		}
		if (Main.rand.NextBool(3))
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f);
			Main.dust[dust].noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		byte red = byte.MaxValue;
		byte green = byte.MaxValue;
		byte blue = byte.MaxValue;
		switch (CalamityMod.CurrentSeason)
		{
		case Season.Spring:
			red = 0;
			green = 250;
			blue = 0;
			break;
		case Season.Summer:
			red = 250;
			green = 250;
			blue = 0;
			break;
		case Season.Fall:
			red = 250;
			green = 150;
			blue = 50;
			break;
		case Season.Winter:
			red = 100;
			green = 150;
			blue = 250;
			break;
		}
		return new Color((int)red, (int)green, (int)blue, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		int dustType = 0;
		switch (CalamityMod.CurrentSeason)
		{
		case Season.Spring:
			dustType = Utils.SelectRandom<int>(Main.rand, 245, 157, 107);
			break;
		case Season.Summer:
			dustType = Utils.SelectRandom<int>(Main.rand, 247, 228, 57);
			break;
		case Season.Fall:
			dustType = Utils.SelectRandom<int>(Main.rand, 6, 259, 158);
			break;
		case Season.Winter:
			dustType = Utils.SelectRandom<int>(Main.rand, 67, 229, 185);
			break;
		}
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int i = 0; i < 27; i++)
		{
			float oldXVel = base.Projectile.oldVelocity.X * (30f / (float)i);
			float oldYVel = base.Projectile.oldVelocity.Y * (30f / (float)i);
			int solsticeDust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel, base.Projectile.oldPosition.Y - oldYVel), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.8f);
			Dust obj = Main.dust[solsticeDust];
			obj.noGravity = true;
			obj.velocity *= 0.5f;
			solsticeDust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel, base.Projectile.oldPosition.Y - oldYVel), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
			obj.velocity *= 0.05f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		int buff = (Main.dayTime ? 189 : ModContent.BuffType<Nightwither>());
		target.AddBuff(buff, 180);
	}
}
