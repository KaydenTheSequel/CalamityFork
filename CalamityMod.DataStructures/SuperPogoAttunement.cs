using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class SuperPogoAttunement : Attunement
{
	public override float DamageMultiplier => (float)OmegaBiomeBlade.SuperPogoAttunement_BaseDamage / (float)OmegaBiomeBlade.BaseDamage;

	public SuperPogoAttunement()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.SuperPogo;
		tooltipColor = new Color(216, 55, 22);
		tooltipColor2 = new Color(216, 131, 22);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<SanguineFury>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override void PassiveEffect(Player player, IEntitySource source, ref int UseTimer, ref bool Procced, Projectile projectile = null)
	{
		if (Procced)
		{
			player.DoLifestealDirect(null, OmegaBiomeBlade.SuperPogoAttunement_PassiveLifeSteal);
			Procced = false;
		}
	}
}
