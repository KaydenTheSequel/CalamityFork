using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AegisBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0.25f, 0f);
		base.Projectile.rotation++;
		base.Projectile.alpha -= 25;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item73, base.Projectile.position);
			base.Projectile.localAI[0]++;
		}
		int goldDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, new Color(255, Main.DiscoG, 53), 0.8f);
		Main.dust[goldDust].noGravity = true;
		Dust obj = Main.dust[goldDust];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[goldDust];
		obj2.velocity += base.Projectile.velocity * 0.1f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		for (int d = 0; d <= 30; d++)
		{
			float rando = Main.rand.Next(-10, 11);
			float rando2 = Main.rand.Next(-10, 11);
			float num = Main.rand.Next(3, 9);
			float randoAdjuster = (float)Math.Sqrt(rando * rando + rando2 * rando2);
			randoAdjuster = num / randoAdjuster;
			rando *= randoAdjuster;
			rando2 *= randoAdjuster;
			int deathDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, new Color(255, Main.DiscoG, 53), 1.2f);
			Dust obj = Main.dust[deathDust];
			obj.noGravity = true;
			obj.position.X = base.Projectile.Center.X;
			obj.position.Y = base.Projectile.Center.Y;
			obj.position.X += Main.rand.Next(-10, 11);
			obj.position.Y += Main.rand.Next(-10, 11);
			obj.velocity.X = rando;
			obj.velocity.Y = rando2;
		}
		int flameAmt = Main.rand.Next(2, 4);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < flameAmt; i++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<AegisFlame>(), (int)((double)base.Projectile.damage * 0.75), 0f, base.Projectile.owner);
			}
		}
	}
}
