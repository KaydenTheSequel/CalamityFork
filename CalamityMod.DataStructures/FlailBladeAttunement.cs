using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class FlailBladeAttunement : Attunement
{
	public override float DamageMultiplier => (float)OmegaBiomeBlade.FlailBladeAttunement_BaseDamage / (float)OmegaBiomeBlade.BaseDamage;

	public FlailBladeAttunement()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.FlailBlade;
		tooltipColor = new Color(113, 239, 177);
		tooltipColor2 = new Color(169, 207, 255);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<LamentationsOfTheChained>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override void PassiveEffect(Player player, IEntitySource source, ref int UseTimer, ref bool Procced, Projectile projectile = null)
	{
		if (Procced)
		{
			if (projectile.ModProjectile is ChainedMeatHook hook && hook.Twirling == 0f)
			{
				hook.Twirling = 1f;
				hook.Projectile.timeLeft = 30;
			}
			Procced = false;
		}
	}
}
