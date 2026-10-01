using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class EmpyreanKnife : ModProjectile, ILocalizedModType, IModType
{
	private int bounce = 3;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 75f)
		{
			base.Projectile.alpha += 10;
			base.Projectile.damage = (int)((double)base.Projectile.damage * 0.95);
			base.Projectile.knockBack = base.Projectile.knockBack * 0.95f;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.active = false;
			}
		}
		if (base.Projectile.ai[0] < 75f)
		{
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		}
		else
		{
			base.Projectile.rotation += 0.5f;
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 150f, 12f, 20f);
		if (Main.rand.NextBool(6))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 58, base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		bounce--;
		if (bounce <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			int empyreanDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 58, 0f, 0f, 100, default(Color), 0.8f);
			Main.dust[empyreanDust].noGravity = true;
			Dust obj = Main.dust[empyreanDust];
			obj.velocity *= 1.2f;
			Dust obj2 = Main.dust[empyreanDust];
			obj2.velocity -= base.Projectile.oldVelocity * 0.3f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[base.Projectile.owner].SpawnLifeStealProjectile(target, base.Projectile, 305, (int)Math.Round((double)hit.Damage * 0.015), 0.75f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
