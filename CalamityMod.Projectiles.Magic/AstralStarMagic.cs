using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AstralStarMagic : ModProjectile, ILocalizedModType, IModType
{
	private const int NoTileCollideTime = 30;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/Typeless/AstralStar";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 30f)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.Center);
			}
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		if (Main.rand.NextBool(8))
		{
			int spawnDustAmount = 2;
			for (int i = 0; i < spawnDustAmount; i++)
			{
				Color newColor = Main.hslToRgb(0.5f, 1f, 0.5f);
				int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 267, 0f, 0f, 0, newColor);
				Main.dust[dust].position = base.Projectile.Center + Main.rand.NextVector2Circular(base.Projectile.width, base.Projectile.height) * 0.5f;
				Dust obj = Main.dust[dust];
				obj.velocity *= Main.rand.NextFloat() * 0.8f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].fadeIn = 0.6f + Main.rand.NextFloat();
				Dust obj2 = Main.dust[dust];
				obj2.velocity += base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 3f;
				Main.dust[dust].scale = 0.7f;
				if (dust != 6000)
				{
					Dust dust2 = DustExtensions.BetterCloneDust(dust);
					dust2.scale /= 2f;
					dust2.fadeIn *= 0.85f;
					dust2.color = new Color(255, 255, 255, 255);
				}
			}
			Vector2 velocity = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy(base.Projectile.velocity.ToRotation());
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 150);
			Main.dust[idx].velocity = velocity * 0.33f;
			Main.dust[idx].position = base.Projectile.Center + velocity * 6f;
		}
		if (Main.rand.NextBool(24) && !Main.dedServ)
		{
			int idx2 = Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.Center, base.Projectile.velocity * 0.1f, 16);
			Gore obj3 = Main.gore[idx2];
			obj3.velocity *= 0.66f;
			Gore obj4 = Main.gore[idx2];
			obj4.velocity += base.Projectile.velocity * 0.15f;
		}
		base.Projectile.light = 0.9f;
		if (Main.rand.NextBool(5))
		{
			Color newColor2 = Main.hslToRgb(1f, 1f, 0.5f);
			int dust3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 267, 0f, 0f, 0, newColor2);
			Main.dust[dust3].position = base.Projectile.Center + Main.rand.NextVector2Circular(base.Projectile.width, base.Projectile.height) * 0.5f;
			Dust obj5 = Main.dust[dust3];
			obj5.velocity *= Main.rand.NextFloat() * 0.8f;
			Main.dust[dust3].noGravity = true;
			Main.dust[dust3].fadeIn = 0.6f + Main.rand.NextFloat();
			Dust obj6 = Main.dust[dust3];
			obj6.velocity += base.Projectile.velocity * 0.25f;
			Main.dust[dust3].scale = 0.7f;
			if (dust3 != 6000)
			{
				Dust dust4 = DustExtensions.BetterCloneDust(dust3);
				dust4.scale /= 2f;
				dust4.fadeIn *= 0.85f;
				dust4.color = new Color(255, 255, 255, 255);
			}
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 150);
		}
		if (Main.rand.NextBool(10) && !Main.dedServ)
		{
			Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity * 0.1f, Main.rand.Next(16, 18));
		}
		if (base.Projectile.ai[0] == 1f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, base.Projectile.tileCollide, 500f, 15f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 100, 250, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawStarTrail(Color.Coral, Color.White);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(50);
		int spawnDustAmount = 3;
		for (int i = 0; i < spawnDustAmount; i++)
		{
			Color newColor = Main.hslToRgb(1f, 1f, 0.5f);
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 267, 0f, 0f, 0, newColor);
			Main.dust[dust].position = base.Projectile.Center + Main.rand.NextVector2Circular(base.Projectile.width, base.Projectile.height);
			Dust obj = Main.dust[dust];
			obj.velocity *= Main.rand.NextFloat() * 2.4f;
			Main.dust[dust].noGravity = true;
			Main.dust[dust].fadeIn = 0.6f + Main.rand.NextFloat();
			Main.dust[dust].scale = 1.4f;
			if (dust != 6000)
			{
				Dust dust2 = DustExtensions.BetterCloneDust(dust);
				dust2.scale /= 2f;
				dust2.fadeIn *= 0.85f;
				dust2.color = new Color(255, 255, 255, 255);
			}
		}
		for (int j = 0; j < 3; j++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale = 0.5f;
				Main.dust[idx].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int k = 0; k < 3; k++)
		{
			int idx2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[idx2].noGravity = true;
			Dust obj3 = Main.dust[idx2];
			obj3.velocity *= 5f;
			idx2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
			Dust obj4 = Main.dust[idx2];
			obj4.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			for (int l = 0; l < 3; l++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, base.Projectile.velocity * 0.05f, Main.rand.Next(16, 18));
			}
		}
	}
}
