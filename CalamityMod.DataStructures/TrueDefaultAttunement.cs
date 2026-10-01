using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class TrueDefaultAttunement : Attunement
{
	public override float DamageMultiplier => (float)TrueBiomeBlade.DefaultAttunement_BaseDamage / (float)TrueBiomeBlade.BaseDamage;

	public TrueDefaultAttunement()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.TrueDefault;
		tooltipColor = new Color(201, 220, 93);
		energyParticleEdgeColor = new Color(117, 126, 72);
		energyParticleCenterColor = new Color(200, 184, 136);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<TruePureClarity>();
		item.shootSpeed = 0f;
		item.UseSound = null;
		item.noMelee = true;
	}
}
