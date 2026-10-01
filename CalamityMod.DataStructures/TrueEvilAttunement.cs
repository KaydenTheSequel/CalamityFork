using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class TrueEvilAttunement : Attunement
{
	public override float DamageMultiplier => (float)TrueBiomeBlade.EvilAttunement_BaseDamage / (float)TrueBiomeBlade.BaseDamage;

	public TrueEvilAttunement()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.TrueEvil;
		tooltipColor = new Color(211, 64, 147);
		energyParticleEdgeColor = new Color(112, 4, 35);
		energyParticleCenterColor = new Color(195, 42, 200);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = false;
		item.noUseGraphic = true;
		item.useStyle = 3;
		item.shoot = ModContent.ProjectileType<TrueDecaysRetort>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override bool Shoot(Player player, IEntitySource source, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		bool powerLungeAvailable = false;
		if (PowerLungeCounter == 3)
		{
			powerLungeAvailable = true;
			PowerLungeCounter = 0;
		}
		if (Projectile.NewProjectileDirect(source, player.Center, new Vector2(speedX, speedY), ModContent.ProjectileType<TrueDecaysRetort>(), damage, knockBack, player.whoAmI, 26f, (CanLunge > 0) ? 1f : 0f).ModProjectile is TrueDecaysRetort rapier)
		{
			rapier.ChargedUp = powerLungeAvailable;
		}
		CanLunge--;
		if (CanLunge < 0)
		{
			CanLunge = 0;
		}
		return false;
	}
}
