using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class Cinder : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 300;
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesIDStaticNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		SpawnDust();
		Vector2 center = base.Projectile.Center;
		Color orange = Color.Orange;
		Lighting.AddLight(center, ((Color)(ref orange)).ToVector3());
	}

	public void SpawnDust()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		int bootlegTexture = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
		Dust obj = Main.dust[bootlegTexture];
		obj.position += new Vector2(2f);
		Main.dust[bootlegTexture].scale += 0.3f + Main.rand.NextFloat(0.5f);
		Main.dust[bootlegTexture].noGravity = true;
		Main.dust[bootlegTexture].velocity.Y -= 2f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		int flowerPetalCount = Main.rand.Next(3, 6);
		float thetaDelta = base.Projectile.velocity.ToRotation();
		float weaveDistanceMin = 2f;
		float weaveDistanceOutwardMax = 3f;
		float weaveDistanceInner = 0.5f;
		for (float theta = 0f; theta < (float)Math.PI * 2f; theta += 0.05f)
		{
			Vector2 velocity = theta.ToRotationVector2() * (weaveDistanceMin + (float)(Math.Sin(thetaDelta + theta * (float)flowerPetalCount) + 0.5 + (double)weaveDistanceInner) * weaveDistanceOutwardMax);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 6, velocity);
			dust.noGravity = true;
			dust.scale = 1.35f;
		}
		target.AddBuff(24, 180);
	}
}
