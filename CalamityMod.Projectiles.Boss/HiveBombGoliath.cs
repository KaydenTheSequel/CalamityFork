using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.PlaguebringerGoliath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HiveBombGoliath : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f + base.Projectile.ai[0] * 0.0002f;
		}
		if (base.Projectile.position.Y > base.Projectile.ai[1])
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (Math.Abs(base.Projectile.velocity.X) >= 3f || Math.Abs(base.Projectile.velocity.Y) >= 3f)
		{
			float randDustXVel = 0f;
			float randDustYVel = 0f;
			if (Main.rand.NextBool(2))
			{
				randDustXVel = base.Projectile.velocity.X * 0.5f;
				randDustYVel = base.Projectile.velocity.Y * 0.5f;
			}
			int bombDust = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + randDustXVel, base.Projectile.position.Y + 3f + randDustYVel) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 6, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[bombDust].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
			Dust obj = Main.dust[bombDust];
			obj.velocity *= 0.2f;
			Main.dust[bombDust].noGravity = true;
			bombDust = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + randDustXVel, base.Projectile.position.Y + 3f + randDustYVel) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.25f);
			Main.dust[bombDust].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
			Dust obj2 = Main.dust[bombDust];
			obj2.velocity *= 0.05f;
		}
		else if (Main.rand.NextBool(4))
		{
			int smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[smoke].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
			Main.dust[smoke].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
			Main.dust[smoke].noGravity = true;
			Main.dust[smoke].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (0f - (float)base.Projectile.height) / 2f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
			Main.rand.Next(2);
			smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
			Main.dust[smoke].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
			Main.dust[smoke].noGravity = true;
			Main.dust[smoke].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (0f - (float)base.Projectile.height) / 2f - 6f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawBackglow(PlaguebringerGoliath.BackglowColor, 4f, null, null, (SpriteEffects)0);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HiveBombGoliathGlow", (AssetRequestMode)2).Value;
		Vector2 textureArea = default(Vector2);
		((Vector2)(ref textureArea))._002Ector((float)(glow.Width / 2), (float)(glow.Height / Main.projFrames[base.Type] / 2));
		Vector2 drawArea = base.Projectile.Center - Main.screenPosition;
		drawArea -= new Vector2((float)glow.Width, (float)(glow.Height / Main.projFrames[base.Type])) / 2f;
		drawArea += textureArea + new Vector2(0f, base.Projectile.gfxOffY);
		Color whiteColor = Color.White;
		int height = glow.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Main.spriteBatch.Draw(glow, drawArea, (Rectangle?)new Rectangle(0, drawStart, glow.Width, height), whiteColor, base.Projectile.rotation, textureArea, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int i = 0; i < 8; i++)
		{
			int plagued = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[plagued];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[plagued].scale = 0.5f;
				Main.dust[plagued].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int plagued2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 100, default(Color), 3f);
			Main.dust[plagued2].noGravity = true;
			Dust obj2 = Main.dust[plagued2];
			obj2.velocity *= 5f;
			plagued2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[plagued2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			Vector2 goreSource = base.Projectile.Center;
			int goreAmt = 3;
			Vector2 source = default(Vector2);
			((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
			for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
			{
				float velocityMult = 0.33f;
				if (goreIndex < goreAmt / 3)
				{
					velocityMult = 0.66f;
				}
				if (goreIndex >= 2 * goreAmt / 3)
				{
					velocityMult = 1f;
				}
				ModContent.GetInstance<CalamityMod>();
				int type = Main.rand.Next(61, 64);
				int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj4 = Main.gore[smoke];
				obj4.velocity *= velocityMult;
				obj4.velocity.X++;
				obj4.velocity.Y++;
				type = Main.rand.Next(61, 64);
				smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj5 = Main.gore[smoke];
				obj5.velocity *= velocityMult;
				obj5.velocity.X--;
				obj5.velocity.Y++;
				type = Main.rand.Next(61, 64);
				smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj6 = Main.gore[smoke];
				obj6.velocity *= velocityMult;
				obj6.velocity.X++;
				obj6.velocity.Y--;
				type = Main.rand.Next(61, 64);
				smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj7 = Main.gore[smoke];
				obj7.velocity *= velocityMult;
				obj7.velocity.X--;
				obj7.velocity.Y--;
			}
		}
		if (Main.netMode == 1 || !Main.zenithWorld)
		{
			return;
		}
		Vector2 valueBoom = base.Projectile.Center;
		float spreadBoom = 0.261f;
		double startAngleBoom = Math.Atan2(base.Projectile.velocity.X, base.Projectile.velocity.Y) - (double)(spreadBoom / 2f);
		double deltaAngleBoom = spreadBoom / 8f;
		int damageBoom = base.Projectile.damage / 2;
		for (int iBoom = 0; iBoom < 5; iBoom++)
		{
			if (Main.rand.NextBool(5) && iBoom > 0)
			{
				int projectileType = ModContent.ProjectileType<SandPoisonCloud>();
				double offsetAngleBoom = startAngleBoom + deltaAngleBoom * (double)(iBoom + iBoom * iBoom) / 2.0 + (double)(32f * (float)iBoom);
				float velocity = 0.5f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), valueBoom.X, valueBoom.Y, (float)(Math.Sin(offsetAngleBoom) * (double)velocity), (float)(Math.Cos(offsetAngleBoom) * (double)velocity), projectileType, damageBoom, 0f, Main.myPlayer);
				Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), valueBoom.X, valueBoom.Y, (float)((0.0 - Math.Sin(offsetAngleBoom)) * (double)velocity), (float)((0.0 - Math.Cos(offsetAngleBoom)) * (double)velocity), projectileType, damageBoom, 0f, Main.myPlayer);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			if (Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 240);
				target.AddBuff(20, 240);
				target.AddBuff(70, 240);
			}
			target.AddBuff(ModContent.BuffType<Plague>(), 120);
		}
	}
}
