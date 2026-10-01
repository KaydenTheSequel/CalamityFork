using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MonstrousKnife : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 2;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 30f)
		{
			base.Projectile.alpha += 10;
			if (base.Projectile.damage > 1)
			{
				base.Projectile.damage = (int)((double)base.Projectile.damage * 0.9);
			}
			base.Projectile.knockBack = base.Projectile.knockBack * 0.9f;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.ai[0] < 30f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		for (int dustIndex = 0; dustIndex < 3; dustIndex++)
		{
			int redDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 182, 0f, 0f, 100, default(Color), 0.8f);
			Dust obj = Main.dust[redDust];
			obj.noGravity = true;
			obj.velocity *= 1.2f;
			obj.velocity -= base.Projectile.oldVelocity * 0.3f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[base.Projectile.owner].SpawnLifeStealProjectile(target, base.Projectile, 305, 1, 4f);
	}
}
