using System;
using CalamityMod.NPCs.Cryogen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class IceBomb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.scale = 0.6f;
		base.Projectile.hostile = true;
		base.Projectile.coldDamage = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.ai[0] >= 120f;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		if (!(base.Projectile.ai[0] < 120f))
		{
			return;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] != 120f)
		{
			return;
		}
		for (int i = 0; i < 8; i++)
		{
			int iceDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[iceDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[iceDust].scale = 0.5f;
				Main.dust[iceDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 14; j++)
		{
			int iceDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, default(Color), 3f);
			Main.dust[iceDust2].noGravity = true;
			Dust obj2 = Main.dust[iceDust2];
			obj2.velocity *= 5f;
			iceDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[iceDust2];
			obj3.velocity *= 2f;
		}
		base.Projectile.scale = 1.2f;
		base.Projectile.ExpandHitboxBy((int)(30f * base.Projectile.scale));
		SoundEngine.PlaySound(in SoundID.Item30, base.Projectile.Center);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		return new Color(1f, 1f, 1f, 1f) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawProjectileWithBackglow(Cryogen.BackglowColor, lightColor, 4f, null, null, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			int totalProjectiles = 8;
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<IceRain>();
			float velocity = 1f;
			Vector2 spinningPoint = default(Vector2);
			((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity);
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 vector255 = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector255, type, Cryogen.IceRainDamage, 0f, base.Projectile.owner, 1f);
			}
		}
		for (int i = 0; i < 10; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 67, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(44, 180);
		}
	}
}
