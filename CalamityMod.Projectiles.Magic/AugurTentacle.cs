using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AugurTentacle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
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
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
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
		Vector2 projCenter = base.Projectile.Center;
		base.Projectile.scale = 1f - base.Projectile.localAI[0];
		base.Projectile.width = (int)(20f * base.Projectile.scale);
		base.Projectile.height = base.Projectile.width;
		base.Projectile.position.X = projCenter.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = projCenter.Y - (float)(base.Projectile.height / 2);
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
		base.Projectile.velocity.X = base.Projectile.velocity.X + base.Projectile.ai[0] * 2f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + base.Projectile.ai[1] * 2f;
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
			for (int scaleLoopCheck = 0; (float)scaleLoopCheck < base.Projectile.scale * 10f; scaleLoopCheck++)
			{
				int rainbow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.1f);
				Main.dust[rainbow].position = (Main.dust[rainbow].position + base.Projectile.Center) / 2f;
				Main.dust[rainbow].noGravity = true;
				Dust obj = Main.dust[rainbow];
				obj.velocity *= 0.1f;
				Dust obj2 = Main.dust[rainbow];
				obj2.velocity -= base.Projectile.velocity * (1.3f - base.Projectile.scale);
				Main.dust[rainbow].scale += base.Projectile.scale * 0.75f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 120);
	}
}
