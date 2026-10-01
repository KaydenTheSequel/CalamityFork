using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class RedLightningFeather : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
	}

	public override void AI()
	{
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
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
			base.Projectile.rotation = (float)Math.Atan2(0f - base.Projectile.velocity.Y, 0f - base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X);
		}
		base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 1170) / 30f, 0f, 1f);
		Lighting.AddLight(base.Projectile.Center, 0.7f * base.Projectile.Opacity, 0f, 0f);
		base.Projectile.ai[0]++;
		float timeGateValue = 150f;
		float timeGateValue2 = 300f;
		if (!(base.Projectile.ai[0] > timeGateValue))
		{
			return;
		}
		if (base.Projectile.ai[0] <= timeGateValue2)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 10f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.015f;
			}
			int playerTracker = Player.FindClosest(base.Projectile.Center, 1, 1);
			float projVelocity = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 playerDirection = Main.player[playerTracker].Center - base.Projectile.Center;
			((Vector2)(ref playerDirection)).Normalize();
			playerDirection *= projVelocity;
			base.Projectile.velocity = (base.Projectile.velocity * 19f + playerDirection) / 20f;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= projVelocity;
		}
		else
		{
			base.Projectile.tileCollide = true;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 20f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 1.015f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, TextureAssets.Projectile[base.Type].Value, drawCentered: false);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		EmitDust();
		base.Projectile.Damage();
	}

	private void EmitDust()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item109, base.Projectile.Center);
		for (int j = 0; j < 6; j++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int k = 0; k < 10; k++)
		{
			int redDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[redDust].noGravity = true;
			Dust obj = Main.dust[redDust];
			obj.velocity *= 3f;
			redDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj2 = Main.dust[redDust];
			obj2.velocity *= 2f;
			Main.dust[redDust].noGravity = true;
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.Opacity == 1f)
		{
			target.AddBuff(ModContent.BuffType<VermillionFlux>(), 60);
		}
	}
}
