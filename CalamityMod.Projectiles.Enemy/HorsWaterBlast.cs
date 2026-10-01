using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class HorsWaterBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
	}

	public override void AI()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			for (int i = 0; i < 20; i++)
			{
				int waterDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[waterDust];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[waterDust].scale = 0.5f;
					Main.dust[waterDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.Center);
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.35f / 255f);
		for (int j = 0; j < 10; j++)
		{
			int watery = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[watery].noGravity = true;
			Dust obj2 = Main.dust[watery];
			obj2.velocity *= 0.5f;
			Dust obj3 = Main.dust[watery];
			obj3.velocity += base.Projectile.velocity * 0.1f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.Center);
		for (int dust = 0; dust <= 40; dust++)
		{
			float rando1 = Main.rand.Next(-10, 11);
			float rando2 = Main.rand.Next(-10, 11);
			float num = Main.rand.Next(3, 9);
			float randoAdjuster = (float)Math.Sqrt(rando1 * rando1 + rando2 * rando2);
			randoAdjuster = num / randoAdjuster;
			rando1 *= randoAdjuster;
			rando2 *= randoAdjuster;
			int killWaterDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[killWaterDust].noGravity = true;
			Main.dust[killWaterDust].position.X = base.Projectile.Center.X;
			Main.dust[killWaterDust].position.Y = base.Projectile.Center.Y;
			Main.dust[killWaterDust].position.X += Main.rand.Next(-10, 11);
			Main.dust[killWaterDust].position.Y += Main.rand.Next(-10, 11);
			Main.dust[killWaterDust].velocity.X = rando1;
			Main.dust[killWaterDust].velocity.Y = rando2;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(103, 300);
		}
	}
}
