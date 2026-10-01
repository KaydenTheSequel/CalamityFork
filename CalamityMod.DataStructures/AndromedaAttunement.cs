using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class AndromedaAttunement : Attunement
{
	public override float DamageMultiplier => (float)FourSeasonsGalaxia.AndromedaAttunement_BaseDamage / (float)FourSeasonsGalaxia.BaseDamage;

	public AndromedaAttunement()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Andromeda;
		tooltipColor = new Color(132, 128, 255);
		tooltipColor2 = new Color(194, 166, 255);
		tooltipPassiveColor = new Color(203, 25, 119);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<AndromedasStride>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
		item.reuseDelay = 12;
	}
}
