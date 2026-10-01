using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class GoldplumeSpearProjectile : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<GoldplumeSpear>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 1.1f;

	public override float ForwardSpeed => 0.4f;

	public override Action<Projectile> EffectBeforeReelback => delegate
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 velocity = base.Projectile.velocity.RotatedByRandom(0.07853981852531433) * Main.rand.NextFloat(1.55f, 1.85f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center - base.Projectile.velocity * 4f, velocity, ModContent.ProjectileType<Feather>(), (int)((double)base.Projectile.damage * 0.5), 0f, base.Projectile.owner);
			}
		}
	};

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void ExtraBehavior()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 59, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}
}
