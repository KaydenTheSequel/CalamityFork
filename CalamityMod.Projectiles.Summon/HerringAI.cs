using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HerringAI : ModProjectile, ILocalizedModType, IModType
{
	public float EnemyDistanceDetection = 1200f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.localNPCHitCooldown = 35;
		base.Projectile.penetrate = -1;
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistance();
		base.Projectile.rotation = ((base.Projectile.spriteDirection == -1) ? (base.Projectile.velocity.ToRotation() + (float)Math.PI) : base.Projectile.velocity.ToRotation());
		base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		base.Projectile.ChargingMinionAI(EnemyDistanceDetection, 1500f, 2200f, 150f, 0, 24f, 15f, 4f, new Vector2(0f, -60f), 12f, 12f, CalamityPlayer.areThereAnyDamnBosses, ignoreTilesWhenCharging: true);
		base.Projectile.netUpdate = true;
	}

	public void CheckMinionExistance()
	{
		Owner.AddBuff(ModContent.BuffType<Herring>(), 1);
		if (base.Projectile.type == ModContent.ProjectileType<HerringAI>())
		{
			if (Owner.dead)
			{
				ModdedOwner.herring = false;
			}
			if (ModdedOwner.herring)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		int herringAmount = 3;
		for (int herringIndex = 0; herringIndex < herringAmount; herringIndex++)
		{
			float angle = (float)Math.PI * 2f / (float)herringAmount * (float)herringIndex;
			Vector2 velocity = angle.ToRotationVector2();
			int herring = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<HerringMinion>(), 0, 0f, base.Projectile.owner, base.Projectile.whoAmI, angle);
			if (Main.projectile.IndexInRange(herring))
			{
				Main.projectile[herring].originalDamage = 0;
			}
		}
		int dustAmount = 100;
		for (int dustIndex = 0; dustIndex < dustAmount; dustIndex++)
		{
			Vector2 velocity2 = ((float)Math.PI * 2f / (float)dustAmount * (float)dustIndex).ToRotationVector2() * 20f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 33, velocity2);
			dust.customData = false;
			dust.velocity *= 0.3f;
			dust.scale = ((Vector2)(ref velocity2)).Length() * 0.1f;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
