using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AstralStar : ModProjectile, ILocalizedModType, IModType
{
	private int noTileHitCounter = 90;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 50;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 3f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 300f, 12f, 20f);
		}
		if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.DamageType = RogueDamageClass.Instance;
		}
		noTileHitCounter--;
		if (noTileHitCounter == 0)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
			}
		}
		base.Projectile.alpha -= 15;
		int alphaControl = 150;
		if (base.Projectile.Center.Y >= base.Projectile.ai[1])
		{
			alphaControl = 0;
		}
		if (base.Projectile.alpha < alphaControl)
		{
			base.Projectile.alpha = alphaControl;
		}
		base.Projectile.localAI[0] += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		if (Main.rand.NextBool(16))
		{
			Vector2 rotational = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy(base.Projectile.velocity.ToRotation());
			int astralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 150);
			Main.dust[astralDust].velocity = rotational * 0.66f;
			Main.dust[astralDust].position = base.Projectile.Center + rotational * 12f;
		}
		if (Main.rand.NextBool(48) && !Main.dedServ)
		{
			int starry = Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.Center, new Vector2(base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f), 16);
			Gore obj = Main.gore[starry];
			obj.velocity *= 0.66f;
			Gore obj2 = Main.gore[starry];
			obj2.velocity += base.Projectile.velocity * 0.3f;
		}
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.light = 0.9f;
			if (Main.rand.NextBool(10))
			{
				Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 150);
			}
			if (Main.rand.NextBool(20) && !Main.dedServ)
			{
				Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.position, new Vector2(base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f), Main.rand.Next(16, 18));
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.DamageType != DamageClass.Ranged)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.DamageType != DamageClass.Ranged)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 100, 250, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			return;
		}
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 36;
		base.Projectile.height = 36;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 5; i++)
		{
			int starryDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[starryDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[starryDust].scale = 0.5f;
				Main.dust[starryDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 5; j++)
		{
			int starryDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.7f);
			Main.dust[starryDust2].noGravity = true;
			Dust obj2 = Main.dust[starryDust2];
			obj2.velocity *= 5f;
			starryDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
			Dust obj3 = Main.dust[starryDust2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			for (int k = 0; k < 3; k++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, new Vector2(base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawStarTrail(Color.Coral, Color.White);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
