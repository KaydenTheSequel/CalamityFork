using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DraconicSpark : ModProjectile, ILocalizedModType, IModType
{
	public static int Lifetime = 120;

	public static float MaxHomingRange = 600f;

	public static float HomingVelocity = 20f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		DrawProjectile();
		ArchAmaryllisHoming();
	}

	private void ArchAmaryllisHoming()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		float targetX = base.Projectile.Center.X;
		float targetY = base.Projectile.Center.Y;
		bool foundTarget = false;
		float maxRange = MaxHomingRange;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile) && Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1))
			{
				float iterCenterX = n.position.X + (float)(n.width / 2);
				float iterCenterY = n.position.Y + (float)(n.height / 2);
				float dist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - iterCenterX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - iterCenterY);
				if (dist < maxRange)
				{
					maxRange = dist;
					targetX = iterCenterX;
					targetY = iterCenterY;
					foundTarget = true;
				}
			}
		}
		if (foundTarget)
		{
			float homingVelocity = HomingVelocity;
			Vector2 projCenter = base.Projectile.Center;
			float xDist = targetX - projCenter.X;
			float yDist = targetY - projCenter.Y;
			float dist2 = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
			dist2 = homingVelocity / dist2;
			xDist *= dist2;
			yDist *= dist2;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 20f + xDist) / 21f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 20f + yDist) / 21f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
	}

	private void DrawProjectile()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			int dustID = 244;
			if (!Main.rand.NextBool(3))
			{
				Main.rand.NextFloat(0.8f, 1.4f);
				int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID);
				Main.dust[idx].noGravity = true;
				Dust obj = Main.dust[idx];
				obj.velocity += base.Projectile.velocity * 0.1f;
			}
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			int dustID2 = 235;
			if (!Main.rand.NextBool(3))
			{
				float scale = Main.rand.NextFloat(0.6f, 1.2f);
				int idx2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID2, 0f, 0f, 100, default(Color), scale);
				Main.dust[idx2].noGravity = true;
				Dust obj2 = Main.dust[idx2];
				obj2.velocity += base.Projectile.velocity * 0.1f;
			}
		}
		else if (base.Projectile.ai[0] == 2f)
		{
			int dustID3 = 246;
			if (!Main.rand.NextBool(3))
			{
				Main.rand.NextFloat(0.8f, 1.4f);
				int idx3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID3);
				Main.dust[idx3].noGravity = true;
				Dust obj3 = Main.dust[idx3];
				obj3.velocity += base.Projectile.velocity * 0.1f;
			}
		}
	}
}
