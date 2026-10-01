using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AstrealArrow : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale -= 0.02f;
			base.Projectile.alpha += 30;
			if (base.Projectile.alpha >= 250)
			{
				base.Projectile.alpha = 255;
				base.Projectile.localAI[0] = 1f;
			}
		}
		else if (base.Projectile.localAI[0] == 1f)
		{
			base.Projectile.scale += 0.02f;
			base.Projectile.alpha -= 30;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.alpha = 0;
				base.Projectile.localAI[0] = 0f;
			}
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f)
		{
			switch ((int)base.Projectile.ai[0])
			{
			case 0:
				base.Projectile.velocity.X *= 1.02f;
				break;
			case 1:
				base.Projectile.velocity.Y *= 1.02f;
				break;
			case 2:
				base.Projectile.velocity.X += 0.1f;
				base.Projectile.velocity.Y *= 1.01f;
				break;
			case 3:
				base.Projectile.velocity.Y += 0.1f;
				base.Projectile.velocity.X *= 1.01f;
				break;
			}
		}
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 173, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		base.Projectile.ai[1] += Main.rand.Next(2) + 1;
		if (base.Projectile.ai[1] >= 135f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<AstrealFlame>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 173, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(153, 180);
	}
}
