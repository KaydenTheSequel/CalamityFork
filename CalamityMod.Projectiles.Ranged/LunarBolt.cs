using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class LunarBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (base.Projectile.alpha < 170)
		{
			for (int d = 0; d < 5; d++)
			{
				Vector2 dspeed = -base.Projectile.velocity * 0.5f;
				int index = Dust.NewDust(base.Projectile.Center, 1, 1, 206, 0f, 0f, 0, default(Color), 1.2f);
				Main.dust[index].alpha = base.Projectile.alpha;
				Main.dust[index].velocity = dspeed;
				Main.dust[index].noGravity = true;
			}
			for (int i = 0; i < 5; i++)
			{
				Vector2 dspeed2 = -base.Projectile.velocity * 0.5f;
				int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 0, default(Color), 0.7f);
				Main.dust[index2].alpha = base.Projectile.alpha;
				Main.dust[index2].velocity = dspeed2;
				Main.dust[index2].noGravity = true;
			}
		}
		if (base.Projectile.alpha > 50)
		{
			base.Projectile.alpha -= 25;
		}
		if (base.Projectile.alpha < 50)
		{
			base.Projectile.alpha = 50;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
			SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
			base.Projectile.ai[0]++;
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(168, 247, 239);
	}
}
