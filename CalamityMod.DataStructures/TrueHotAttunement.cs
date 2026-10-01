using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class TrueHotAttunement : Attunement
{
	public override float DamageMultiplier => (float)TrueBiomeBlade.HotAttunement_BaseDamage / (float)TrueBiomeBlade.BaseDamage;

	public TrueHotAttunement()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.TrueHot;
		tooltipColor = new Color(238, 156, 73);
		energyParticleEdgeColor = new Color(137, 32, 0);
		energyParticleCenterColor = new Color(209, 154, 0);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<TrueAridGrandeur>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}
}
