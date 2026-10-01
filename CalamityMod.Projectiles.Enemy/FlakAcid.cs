using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class FlakAcid : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 50;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 480;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0]++ <= 30f)
		{
			base.Projectile.alpha = (int)MathHelper.Lerp(255f, 0f, base.Projectile.ai[0] / 30f);
		}
		if (base.Projectile.velocity.Y < 10f)
		{
			base.Projectile.velocity.Y += 0.15f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.Distance(base.Projectile.Center) < 60f)
			{
				base.Projectile.Kill();
			}
		}
		if (Math.Sign(base.Projectile.velocity.Y) != Math.Sign(base.Projectile.oldVelocity.Y) && base.Projectile.ai[0] >= 5f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(150);
		base.Projectile.Damage();
		for (int i = 0; i <= 40; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, 200, 200, 75, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			Vector2 val = Vector2.One.RotatedByRandom(6.2831854820251465);
			Vector2 val2 = Main.dust[idx].position - base.Projectile.Center;
			obj.velocity = val * ((Vector2)(ref val2)).Length() / 30f;
			Main.dust[idx].scale = 2.5f;
		}
		for (int j = 0; j <= 90; j++)
		{
			int idx2 = Dust.NewDust(base.Projectile.Center, 0, 0, 75);
			Main.dust[idx2].velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * 8f;
			Main.dust[idx2].scale = 3f;
			Main.dust[idx2].noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
