using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class HolyAttunement : Attunement
{
	public override float DamageMultiplier => (float)TrueBiomeBlade.HolyAttunement_BaseDamage / (float)TrueBiomeBlade.BaseDamage;

	public HolyAttunement()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Holy;
		tooltipColor = new Color(220, 143, 255);
		energyParticleEdgeColor = new Color(62, 55, 110);
		energyParticleCenterColor = new Color(255, 143, 255);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<HeavensMight>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}
}
