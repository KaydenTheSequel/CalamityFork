using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SeaDragonRocket : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 95;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			for (int i = 0; i < 5; i++)
			{
				int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[fire];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[fire].scale = 0.5f;
					Main.dust[fire].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			base.Projectile.ai[1] = 1f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.timeLeft <= 3)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.ai[1] = 0f;
			base.Projectile.alpha = 255;
			base.Projectile.position.X = base.Projectile.Center.X;
			base.Projectile.position.Y = base.Projectile.Center.Y;
			base.Projectile.width = 200;
			base.Projectile.height = 200;
			base.Projectile.position.X -= base.Projectile.width / 2;
			base.Projectile.position.Y -= base.Projectile.height / 2;
			base.Projectile.knockBack = 10f;
		}
		else if (Math.Abs(base.Projectile.velocity.X) >= 8f || Math.Abs(base.Projectile.velocity.Y) >= 8f)
		{
			for (int j = 0; j < 2; j++)
			{
				float halfX = 0f;
				float halfY = 0f;
				if (j == 1)
				{
					halfX = base.Projectile.velocity.X * 0.5f;
					halfY = base.Projectile.velocity.Y * 0.5f;
				}
				int explosion = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + halfX, base.Projectile.position.Y + 3f + halfY) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 6, 0f, 0f, 100);
				Main.dust[explosion].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
				Dust obj2 = Main.dust[explosion];
				obj2.velocity *= 0.2f;
				Main.dust[explosion].noGravity = true;
				explosion = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + halfX, base.Projectile.position.Y + 3f + halfY) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 33, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[explosion].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
				Dust obj3 = Main.dust[explosion];
				obj3.velocity *= 0.05f;
			}
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 200f, 12f, 20f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(192);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item110, base.Projectile.position);
		for (int i = 0; i < 15; i++)
		{
			int smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[smoke];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[smoke].scale = 0.5f;
				Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 25; j++)
		{
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fire].noGravity = true;
			Dust obj2 = Main.dust[fire];
			obj2.velocity *= 5f;
			fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fire];
			obj3.velocity *= 2f;
		}
		int projAmt = Main.rand.Next(2, 4);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int k = 0; k < projAmt; k++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<RocketFire>(), (int)((double)base.Projectile.damage * 0.33), 0f, base.Projectile.owner);
			}
		}
	}
}
