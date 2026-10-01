using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class PhoenixAttunement : Attunement
{
	public override float DamageMultiplier => (float)FourSeasonsGalaxia.PhoenixAttunement_BaseDamage / (float)FourSeasonsGalaxia.BaseDamage;

	public PhoenixAttunement()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Phoenix;
		tooltipColor = new Color(255, 87, 0);
		tooltipColor2 = new Color(255, 143, 0);
		tooltipPassiveColor = new Color(76, 137, 237);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<PhoenixsPride>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
		item.reuseDelay = 12;
	}
}
