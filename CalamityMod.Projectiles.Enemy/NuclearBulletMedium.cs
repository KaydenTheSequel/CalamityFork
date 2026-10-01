using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class NuclearBulletMedium : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 12;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 360;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3() * 1.25f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] < 85f)
		{
			base.Projectile.velocity.Y += (float)Math.Sign(base.Projectile.localAI[0]) * 0.1f;
		}
		if (base.Projectile.ai[0] == 160f)
		{
			base.Projectile.ai[1] = (int)Player.FindClosest(base.Projectile.Center, 1, 1);
		}
		if (base.Projectile.ai[0] >= 160f && base.Projectile.ai[0] <= 200f)
		{
			Player player = Main.player[(int)base.Projectile.ai[1]];
			float inertia = 10f;
			if (base.Projectile.Distance(player.Center) > 70f)
			{
				base.Projectile.velocity = (base.Projectile.velocity * inertia + base.Projectile.SafeDirectionTo(player.Center) * 18.5f) / (inertia + 1f);
			}
			base.Projectile.tileCollide = true;
		}
		else
		{
			base.Projectile.tileCollide = false;
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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, 8, 8, 75, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			idx = Dust.NewDust(base.Projectile.position, 8, 8, 75, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 3f;
		}
	}
}
