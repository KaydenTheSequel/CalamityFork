using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class FlameBurstHostile : ModProjectile, ILocalizedModType, IModType
{
	public float count;

	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		if (count == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item73, base.Projectile.Center);
			base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
			base.Projectile.width = 20;
			base.Projectile.height = 20;
			base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
			for (int i = 0; i < 10; i++)
			{
				int flameDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[flameDust];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[flameDust].scale = 0.5f;
					Main.dust[flameDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 20; j++)
			{
				int sparkDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, default(Color), 3f);
				Main.dust[sparkDust].noGravity = true;
				Dust obj2 = Main.dust[sparkDust];
				obj2.velocity *= 5f;
				sparkDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, default(Color), 2f);
				Dust obj3 = Main.dust[sparkDust];
				obj3.velocity *= 2f;
			}
			count++;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			for (int k = 0; k < 5; k++)
			{
				int sparky = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, default(Color), 0.75f);
				Dust obj4 = Main.dust[sparky];
				obj4.velocity *= 0f;
			}
		}
		int playerTarget = Player.FindClosest(base.Projectile.Center, 1, 1);
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] < 200f && base.Projectile.ai[1] > 40f)
		{
			float scaleFactor = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 playerDistance = Main.player[playerTarget].Center - base.Projectile.Center;
			((Vector2)(ref playerDistance)).Normalize();
			playerDistance *= scaleFactor;
			base.Projectile.velocity = (base.Projectile.velocity * 24f + playerDistance) / 25f;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= scaleFactor;
		}
		if (base.Projectile.ai[0] < 0f && ((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.02f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 120);
		}
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
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 246, base.Projectile.oldVelocity.X * 0f, base.Projectile.oldVelocity.Y * 0f);
		}
	}
}
