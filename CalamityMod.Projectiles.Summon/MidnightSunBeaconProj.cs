using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MidnightSunBeaconProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Items/Weapons/Summon/MidnightSunBeacon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 420;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(-(float)Math.PI / 4f, 0.08f);
		if (Math.Abs(base.Projectile.rotation + (float)Math.PI / 4f) < 0.02f && base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[1] = 85f;
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[1] == 1f)
		{
			int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.UnitY * 30f, ModContent.ProjectileType<MidnightSunUFO>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Projectile.originalDamage;
			}
			base.Projectile.Kill();
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]--;
		}
		if (base.Projectile.ai[1] > 1f && base.Projectile.ai[1] <= 60f)
		{
			base.Projectile.velocity.Y -= 0.4f;
			return;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.96f;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
