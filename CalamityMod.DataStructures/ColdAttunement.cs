using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class ColdAttunement : Attunement
{
	public override float DamageMultiplier => (float)BrokenBiomeBlade.ColdAttunement_BaseDamage / (float)BrokenBiomeBlade.BaseDamage;

	public ColdAttunement()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Cold;
		tooltipColor = new Color(165, 235, 235);
		energyParticleEdgeColor = new Color(165, 235, 235);
		energyParticleCenterColor = new Color(58, 110, 141);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<BitingEmbrace>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override bool Shoot(Player player, IEntitySource source, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		switch (Combo)
		{
		case 0:
			Projectile.NewProjectile(source, player.Center, new Vector2(speedX, speedY), ModContent.ProjectileType<BitingEmbrace>(), damage, knockBack, player.whoAmI, 0f, 15f);
			break;
		case 1:
			Projectile.NewProjectile(source, player.Center, new Vector2(speedX, speedY), ModContent.ProjectileType<BitingEmbrace>(), damage, knockBack, player.whoAmI, 1f, 20f);
			break;
		case 2:
			Projectile.NewProjectile(source, player.Center, new Vector2(speedX, speedY), ModContent.ProjectileType<BitingEmbrace>(), damage, knockBack, player.whoAmI, 2f, 50f);
			break;
		}
		Combo++;
		if (Combo > 2)
		{
			Combo = 0;
		}
		return false;
	}
}
