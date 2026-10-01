using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class AriesAttunement : Attunement
{
	public override float DamageMultiplier => (float)FourSeasonsGalaxia.AriesAttunement_BaseDamage / (float)FourSeasonsGalaxia.BaseDamage;

	public AriesAttunement()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Aries;
		tooltipColor = new Color(196, 89, 201);
		tooltipColor2 = new Color(255, 0, 0);
		tooltipPassiveColor = new Color(76, 137, 237);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<AriesWrath>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
		item.reuseDelay = 12;
	}
}
