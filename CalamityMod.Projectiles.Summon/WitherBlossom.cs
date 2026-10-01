using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class WitherBlossom : BaseMinionProjectile
{
	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<WitherBlossom>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<WitherBlossomsBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.witherBlossom;

	public override float MinionSlots => 0.5f;

	public ref float OffsetAngle => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = (base.Projectile.height = 30);
	}

	public override void MinionAI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = base.Owner.Center + OffsetAngle.ToRotationVector2() * 150f + Vector2.UnitY * base.Owner.gfxOffY;
		Time++;
		if (Time % 50f == 49f && Main.myPlayer == base.Projectile.owner && base.Target != null)
		{
			Vector2 shootVelocity = base.Projectile.SafeDirectionTo(base.Target.Center).RotatedByRandom(0.25) * 8f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity, ModContent.ProjectileType<WitherBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (!Main.dedServ)
		{
			base.Projectile.rotation += MathHelper.ToRadians(5f);
			OffsetAngle += MathHelper.ToRadians(4f);
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 36; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 179);
				dust.noGravity = true;
				dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 7f);
			}
		}
	}
}
