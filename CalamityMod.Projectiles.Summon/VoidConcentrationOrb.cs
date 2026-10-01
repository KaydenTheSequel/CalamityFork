using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class VoidConcentrationOrb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 50;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.scale = 0.01f;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.scale < 1f)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 6; d++)
		{
			int shadow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 2f);
			Dust obj = Main.dust[shadow];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[shadow].scale = 0.5f;
				Main.dust[shadow].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 10; i++)
		{
			int shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 3f);
			Main.dust[shadow2].noGravity = true;
			Dust obj2 = Main.dust[shadow2];
			obj2.velocity *= 5f;
			shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 2f);
			Dust obj3 = Main.dust[shadow2];
			obj3.velocity *= 2f;
		}
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		NPC target = base.Projectile.Center.MinionHoming(1800f, owner);
		if (base.Projectile.scale >= 1f)
		{
			if (target != null)
			{
				float projSpeed = 40f;
				Vector2 fireDirection = base.Projectile.Center;
				float fireXVel = target.Center.X - fireDirection.X;
				float fireYVel = target.Center.Y - fireDirection.Y;
				float fireVelocity = (float)Math.Sqrt(fireXVel * fireXVel + fireYVel * fireYVel);
				if (fireVelocity < 100f)
				{
					projSpeed = 28f;
				}
				fireVelocity = projSpeed / fireVelocity;
				fireXVel *= fireVelocity;
				fireYVel *= fireVelocity;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * 25f + fireXVel) / 26f;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 25f + fireYVel) / 26f;
				if (Main.rand.NextBool(5))
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 1.1f;
				}
			}
		}
		else
		{
			base.Projectile.scale += 0.025f;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.03f;
		}
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type] - 1)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.timeLeft <= 60)
		{
			base.Projectile.alpha += 4;
		}
	}
}
