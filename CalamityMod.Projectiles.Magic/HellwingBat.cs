using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HellwingBat : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 90;
	}

	public override void AI()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f);
		if (base.Projectile.ai[0] > 7f)
		{
			float scalar = 1f;
			if (base.Projectile.ai[0] == 8f)
			{
				scalar = 0.25f;
			}
			else if (base.Projectile.ai[0] == 9f)
			{
				scalar = 0.5f;
			}
			else if (base.Projectile.ai[0] == 10f)
			{
				scalar = 0.75f;
			}
			base.Projectile.ai[0]++;
			if (Main.rand.NextBool(3))
			{
				int flareDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
				Dust dust = Main.dust[flareDust];
				if (Main.rand.NextBool(3))
				{
					dust.noGravity = true;
					dust.scale *= 2f;
					dust.velocity.X *= 2f;
					dust.velocity.Y *= 2f;
				}
				else
				{
					dust.scale *= 1.5f;
				}
				dust.velocity.X *= 1.2f;
				dust.velocity.Y *= 1.2f;
				dust.scale *= scalar;
			}
		}
		else
		{
			base.Projectile.ai[0]++;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 240);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
