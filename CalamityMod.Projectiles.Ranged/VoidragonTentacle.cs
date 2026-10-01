using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class VoidragonTentacle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[1]++;
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			if (Math.Abs(base.Projectile.velocity.X) < 1f)
			{
				base.Projectile.velocity.X = 0f - base.Projectile.velocity.X;
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y)
		{
			if (Math.Abs(base.Projectile.velocity.Y) < 1f)
			{
				base.Projectile.velocity.Y = 0f - base.Projectile.velocity.Y;
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		Vector2 center10 = base.Projectile.Center;
		base.Projectile.scale = 1f - base.Projectile.localAI[0];
		base.Projectile.width = (int)(20f * base.Projectile.scale);
		base.Projectile.height = base.Projectile.width;
		base.Projectile.position.X = center10.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = center10.Y - (float)(base.Projectile.height / 2);
		if (base.Projectile.localAI[0] < 0.1f)
		{
			base.Projectile.localAI[0] += 0.01f;
		}
		else
		{
			base.Projectile.localAI[0] += 0.025f;
		}
		if (base.Projectile.localAI[0] >= 0.95f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.velocity.X = base.Projectile.velocity.X + base.Projectile.ai[0] * 1.5f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + base.Projectile.ai[1] * 1.5f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 16f)
		{
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= 16f;
		}
		base.Projectile.ai[0] *= 1.05f;
		base.Projectile.ai[1] *= 1.05f;
		if (base.Projectile.scale < 1f && base.Projectile.localAI[1] > 5f)
		{
			for (int dustAmount = 0; (float)dustAmount < base.Projectile.scale * 10f; dustAmount++)
			{
				int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 62, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100, default(Color), 1.3f);
				Main.dust[dust].position = (Main.dust[dust].position + base.Projectile.Center) / 2f;
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 0.1f;
				Dust obj2 = Main.dust[dust];
				obj2.velocity -= base.Projectile.velocity * (1.1f - base.Projectile.scale);
				Main.dust[dust].fadeIn = 125 + base.Projectile.owner;
				Main.dust[dust].scale += base.Projectile.scale * 0.75f;
			}
		}
	}
}
