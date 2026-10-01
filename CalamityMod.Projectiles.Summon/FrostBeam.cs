using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FrostBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 220;
		base.Projectile.timeLeft = 200;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 5f)
		{
			for (int i = 0; i < 6; i++)
			{
				Vector2 spawnPosition = base.Projectile.position;
				spawnPosition -= base.Projectile.velocity * (float)i * 0.25f;
				int idx = Dust.NewDust(spawnPosition, 1, 1, 113, 0f, 0f, 0, default(Color), 1.25f);
				Main.dust[idx].position = spawnPosition;
				Main.dust[idx].scale = Main.rand.NextFloat(0.75f, 0.85f);
				Dust obj = Main.dust[idx];
				obj.velocity *= 0.1f;
				Main.dust[idx].noGravity = true;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 70);
		Projectile projectile = base.Projectile;
		projectile.position -= base.Projectile.Size * 0.5f;
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage /= 3;
		base.Projectile.Damage();
		int flowerPetalCount = Main.rand.Next(3, 6);
		float thetaDelta = base.Projectile.velocity.ToRotation();
		float weaveDistanceMin = 2f;
		float weaveDistanceOutwardMax = 3f;
		float weaveDistanceInner = 0.5f;
		for (float theta = 0f; theta < (float)Math.PI * 2f; theta += 0.05f)
		{
			Vector2 velocity = theta.ToRotationVector2() * (weaveDistanceMin + (float)(Math.Sin(thetaDelta + theta * (float)flowerPetalCount) + 0.5 + (double)weaveDistanceInner) * weaveDistanceOutwardMax);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 113, velocity);
			dust.noGravity = true;
			dust.scale = 1.35f;
		}
	}
}
