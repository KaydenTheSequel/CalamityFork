using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class HotAttunement : Attunement
{
	public override float DamageMultiplier => (float)BrokenBiomeBlade.HotAttunement_BaseDamage / (float)BrokenBiomeBlade.BaseDamage;

	public HotAttunement()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Hot;
		tooltipColor = new Color(238, 156, 73);
		energyParticleEdgeColor = new Color(137, 32, 0);
		energyParticleCenterColor = new Color(209, 154, 0);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<AridGrandeur>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}
}
