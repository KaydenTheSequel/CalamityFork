using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class TrueTropicalAttunement : Attunement
{
	public override float DamageMultiplier => (float)TrueBiomeBlade.TropicalAttunement_BaseDamage / (float)TrueBiomeBlade.BaseDamage;

	public TrueTropicalAttunement()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.TrueTropical;
		tooltipColor = new Color(162, 200, 85);
		energyParticleEdgeColor = new Color(53, 112, 4);
		energyParticleCenterColor = new Color(131, 173, 39);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = false;
		item.noUseGraphic = true;
		item.useStyle = 1;
		item.shoot = ModContent.ProjectileType<TrueGrovetendersTouch>();
		item.shootSpeed = 30f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override bool Shoot(Player player, IEntitySource source, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (Projectile.NewProjectileDirect(source, player.Center, new Vector2(speedX, speedY), ModContent.ProjectileType<TrueGrovetendersTouch>(), damage, knockBack, player.whoAmI).ModProjectile is TrueGrovetendersTouch whip)
		{
			whip.flipped = ((Combo == 0) ? 1 : (-1));
		}
		Combo++;
		if (Combo > 1)
		{
			Combo = 0;
		}
		return false;
	}
}
