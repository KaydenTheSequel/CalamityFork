using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class VirulentWave : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 70;
		base.Projectile.height = 70;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 150;
		base.Projectile.alpha = 100;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 5;
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		Lighting.AddLight(base.Projectile.Center, 0.05f, 0.4f, 0f);
		if (base.Projectile.ai[1] < 60f)
		{
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
				int dustType = 89;
				int plague = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
				Dust dust = Main.dust[plague];
				if (Main.rand.NextBool(3))
				{
					dust.noGravity = true;
					dust.scale *= 1.8f;
					dust.velocity.X *= 2f;
					dust.velocity.Y *= 2f;
				}
				else
				{
					dust.scale *= 1.3f;
				}
				dust.velocity.X *= 1.2f;
				dust.velocity.Y *= 1.2f;
				dust.scale *= scalar;
			}
			else
			{
				base.Projectile.ai[0]++;
			}
		}
		else
		{
			base.Projectile.damage = (int)((double)base.Projectile.damage * 0.6);
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.85f;
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha += 5;
			}
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
