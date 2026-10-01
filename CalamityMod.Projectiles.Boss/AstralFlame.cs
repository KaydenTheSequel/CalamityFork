using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AstralFlame : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 100;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		if (Main.zenithWorld)
		{
			base.Projectile.extraUpdates = 1;
		}
	}

	public override void AI()
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
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
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (float)Math.Atan2(0.0 - (double)base.Projectile.velocity.Y, 0.0 - (double)base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.5f, 0.1f);
		int targetPlayer = Player.FindClosest(base.Projectile.Center, 1, 1);
		Vector2 playerDist = Main.player[targetPlayer].Center - base.Projectile.Center;
		if (((Vector2)(ref playerDist)).Length() < 60f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 15f)
		{
			int astralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 0.8f);
			Main.dust[astralDust].noGravity = true;
			Dust obj = Main.dust[astralDust];
			obj.velocity *= 0f;
		}
		if (base.Projectile.ai[0] >= 120f)
		{
			if (base.Projectile.ai[1] < 180f)
			{
				float scaleFactor2 = ((Vector2)(ref base.Projectile.velocity)).Length();
				((Vector2)(ref playerDist)).Normalize();
				playerDist *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 30f + playerDist) / 31f;
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile = base.Projectile;
				projectile.velocity *= scaleFactor2;
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < (death ? 15f : 12f))
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= (death ? 1.015f : 1.01f);
			}
			base.Projectile.ai[1]++;
		}
		else
		{
			if (!(base.Projectile.ai[0] >= 60f))
			{
				return;
			}
			float pushForce = (death ? 0.06f : (revenge ? 0.055f : (expertMode ? 0.05f : 0.04f)));
			float pushDistance = (death ? 150f : (revenge ? 135f : (expertMode ? 120f : 90f)));
			for (int k = 0; k < Main.maxProjectiles; k++)
			{
				Projectile otherProj = Main.projectile[k];
				if (!otherProj.active || k == base.Projectile.whoAmI)
				{
					continue;
				}
				bool num = otherProj.type == base.Projectile.type;
				float taxicabDist = Vector2.Distance(base.Projectile.Center, otherProj.Center);
				if (num && taxicabDist < pushDistance)
				{
					if (base.Projectile.position.X < otherProj.position.X)
					{
						base.Projectile.velocity.X -= pushForce;
					}
					else
					{
						base.Projectile.velocity.X += pushForce;
					}
					if (base.Projectile.position.Y < otherProj.position.Y)
					{
						base.Projectile.velocity.Y -= pushForce;
					}
					else
					{
						base.Projectile.velocity.Y += pushForce;
					}
				}
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Zombie103, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 96);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 2; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 50);
		}
		for (int j = 0; j < 20; j++)
		{
			int deathAstralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[deathAstralDust].noGravity = true;
			Dust obj = Main.dust[deathAstralDust];
			obj.velocity *= 3f;
			deathAstralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 50);
			Dust obj2 = Main.dust[deathAstralDust];
			obj2.velocity *= 2f;
			Main.dust[deathAstralDust].noGravity = true;
		}
		base.Projectile.Damage();
	}
}
