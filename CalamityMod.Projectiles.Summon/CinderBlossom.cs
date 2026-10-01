using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CinderBlossom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center.MinionHoming(1200f, Owner, CalamityPlayer.areThereAnyDamnBosses);
		}
	}

	public ref float DelayBetweenShooting => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.penetrate = -1;
		base.Projectile.width = 42;
		base.Projectile.height = 42;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistince();
		ShootTarget(Target);
		base.Projectile.Center = Owner.Center - Vector2.UnitY * (60f - Owner.gfxOffY);
		base.Projectile.rotation += MathHelper.ToRadians(5f * (float)Owner.direction);
		Vector2 center = base.Projectile.Center;
		Color orange = Color.Orange;
		Lighting.AddLight(center, ((Color)(ref orange)).ToVector3());
	}

	public void CheckMinionExistince()
	{
		Owner.AddBuff(ModContent.BuffType<CinderBlossomBuff>(), 1);
		if (base.Projectile.type == ModContent.ProjectileType<CinderBlossom>())
		{
			if (Owner.dead)
			{
				ModdedOwner.cinderBlossom = false;
			}
			if (ModdedOwner.cinderBlossom)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 36; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 6);
			dust.noGravity = true;
			dust.velocity = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(2f, 6f);
		}
	}

	public void ShootTarget(NPC target)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (target != null && base.Projectile.owner == Main.myPlayer)
		{
			if (DelayBetweenShooting == 35f)
			{
				Vector2 velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, target, 20f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<Cinder>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				DelayBetweenShooting = 0f;
				base.Projectile.netUpdate = true;
			}
			if (DelayBetweenShooting < 35f)
			{
				DelayBetweenShooting++;
			}
		}
	}
}
