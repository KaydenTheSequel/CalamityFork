using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AquaticStarMinion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public ref float CheckForSpawning => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 32;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.penetrate = -1;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 35;
	}

	public override void AI()
	{
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		Owner.AddBuff(ModContent.BuffType<AquaticStar>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<AquaticStarMinion>())
		{
			if (Owner.dead)
			{
				moddedOwner.aquaticStar = false;
			}
			if (moddedOwner.aquaticStar)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (CheckForSpawning == 0f)
		{
			for (int i = 0; i < 45; i++)
			{
				Vector2 direction = ((float)Math.PI * 2f / 45f * (float)i).ToRotationVector2() * 10f;
				Dust.NewDustPerfect(base.Projectile.Center, 33, direction).noGravity = true;
			}
			CheckForSpawning++;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.04f;
		base.Projectile.ChargingMinionAI(1200f, 1500f, 2200f, 150f, 0, 24f, 15f, 4f, new Vector2(0f, -60f), 12f, 12f, tileVision: false, ignoreTilesWhenCharging: false);
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = true;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
