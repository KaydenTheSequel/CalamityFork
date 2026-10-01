using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class VoidTentacle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.height = 160;
		base.Projectile.width = 160;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.friendly = true;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 4;
	}

	public override void AI()
	{
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
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
		if ((double)base.Projectile.localAI[0] < 0.1)
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
		if (base.Projectile.scale < 1f)
		{
			for (int i = 0; (float)i < base.Projectile.scale * 4f; i++)
			{
				int dustID = (Main.rand.NextBool(5) ? 199 : 175);
				int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100, default(Color), 1.1f);
				Main.dust[idx].position = (Main.dust[idx].position + base.Projectile.Center) / 2f;
				Main.dust[idx].noGravity = true;
				Dust obj = Main.dust[idx];
				obj.velocity *= 0.1f;
				Dust obj2 = Main.dust[idx];
				obj2.velocity -= base.Projectile.velocity * (1.3f - base.Projectile.scale);
				Main.dust[idx].fadeIn = 100 + base.Projectile.owner;
				Main.dust[idx].scale += base.Projectile.scale * 0.75f;
			}
		}
	}
}
