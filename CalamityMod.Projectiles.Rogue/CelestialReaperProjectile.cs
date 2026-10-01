using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CelestialReaperProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int HomingCooldown;

	public NPC HitTarget;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/CelestialReaper";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 66;
		base.Projectile.height = 76;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 6;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += MathHelper.ToRadians(30f) / (float)Math.Log(6f - (float)base.Projectile.penetrate + 2f) / 1.4f;
		if (HomingCooldown > 0)
		{
			HomingCooldown--;
		}
		else
		{
			NPC target = ((HitTarget != null && HitTarget.active) ? HitTarget : base.Projectile.Center.ClosestNPCAt(800f));
			if (target != null)
			{
				base.Projectile.velocity = (base.Projectile.velocity * 20f + base.Projectile.SafeDirectionTo(target.Center) * 20f) / 21f;
			}
		}
		if (base.Projectile.ai[0] == 1f)
		{
			float framesNeeded = ((base.Projectile.numHits > 0) ? 20f : 60f);
			if ((float)(base.Projectile.timeLeft % (int)framesNeeded) == 0f)
			{
				int projID = ModContent.ProjectileType<CelestialReaperAfterimage>();
				int damage = (int)((float)base.Projectile.damage * 0.25f);
				float kb = base.Projectile.knockBack * 0.5f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, projID, damage, kb, base.Projectile.owner);
			}
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (HomingCooldown <= 0)
		{
			return null;
		}
		return false;
	}

	public override bool CanHitPvp(Player target)
	{
		return HomingCooldown <= 0;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (HitTarget == null || !HitTarget.active)
		{
			HitTarget = target;
		}
		HomingCooldown = 25;
		Projectile projectile = base.Projectile;
		projectile.velocity *= -0.75f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		bool ss = base.Projectile.Calamity().stealthStrike;
		int numSplits = 4;
		int projID = ModContent.ProjectileType<CelestialReaperAfterimage>();
		int damage = (int)((float)base.Projectile.damage * (ss ? 0.25f : 0.5f));
		float kb = base.Projectile.knockBack * 0.5f;
		float speed = 12f;
		for (float i = 0f; i < (float)numSplits; i++)
		{
			Vector2 velocity = ((float)Math.PI * 2f * i / (float)numSplits).ToRotationVector2() * speed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projID, damage, kb, base.Projectile.owner);
		}
	}
}
