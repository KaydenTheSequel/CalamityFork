using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class EvilAttunement : Attunement
{
	public override float DamageMultiplier => (float)BrokenBiomeBlade.EvilAttunement_BaseDamage / (float)BrokenBiomeBlade.BaseDamage;

	public EvilAttunement()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Evil;
		tooltipColor = new Color(211, 64, 147);
		energyParticleEdgeColor = new Color(112, 4, 35);
		energyParticleCenterColor = new Color(195, 42, 200);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = false;
		item.noUseGraphic = true;
		item.useStyle = 3;
		item.shoot = ModContent.ProjectileType<DecaysRetort>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override bool Shoot(Player player, IEntitySource source, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, player.Center, new Vector2(speedX, speedY), ModContent.ProjectileType<DecaysRetort>(), damage, knockBack, player.whoAmI, 26f, CanLunge);
		CanLunge = 0;
		return false;
	}
}
