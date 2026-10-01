using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class TundraFlameBlossom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public ref float FlowerShootTimer => ref base.Projectile.ai[0];

	public ref float RotationMovement => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1200f, Owner);
		CheckMinionExistance();
		SpawnEffect();
		TargetNPC(potentialTarget);
		Vector2 center = base.Projectile.Center;
		Color fuchsia = Color.Fuchsia;
		Lighting.AddLight(center, ((Color)(ref fuchsia)).ToVector3());
		base.Projectile.Center = Owner.Center + RotationMovement.ToRotationVector2() * 100f + Vector2.UnitY * Owner.gfxOffY;
		base.Projectile.rotation += MathHelper.ToRadians(6.25f * (float)Owner.direction);
		base.Projectile.scale = MathHelper.Lerp(1f, 1.005f, FlowerShootTimer % 100f);
		RotationMovement += MathHelper.ToRadians(1.25f * (float)Owner.direction);
	}

	public void CheckMinionExistance()
	{
		Owner.AddBuff(ModContent.BuffType<TundraFlameBlossomsBuff>(), 1);
		if (base.Projectile.type == ModContent.ProjectileType<TundraFlameBlossom>())
		{
			if (Owner.dead)
			{
				moddedOwner.tundraFlameBlossom = false;
			}
			if (moddedOwner.tundraFlameBlossom)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void SpawnEffect()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 36; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 179);
				dust.noGravity = true;
				dust.velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(2f, 7f);
			}
			base.Projectile.localAI[0]++;
		}
	}

	public void ShootFlowers()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item20, Owner.Center);
		Vector2 velocity = -base.Projectile.SafeDirectionTo(Owner.Center) * 10f + base.Projectile.velocity;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<TundraFlameBlossomsOrb>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		base.Projectile.netUpdate = true;
	}

	public void TargetNPC(NPC target)
	{
		if (target != null)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				FlowerShootTimer++;
				if (FlowerShootTimer % 100f == 0f)
				{
					ShootFlowers();
				}
				FlowerShootTimer = ((FlowerShootTimer == 101f) ? 1f : FlowerShootTimer);
			}
		}
		else
		{
			FlowerShootTimer--;
		}
		FlowerShootTimer = MathHelper.Clamp(FlowerShootTimer, 0f, 101f);
		base.Projectile.netUpdate = true;
	}
}
