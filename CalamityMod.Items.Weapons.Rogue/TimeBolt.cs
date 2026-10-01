using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class TimeBolt : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.7f;

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 46;
		base.Item.damage = 432;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.useTime = 20;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<TimeBoltKnife>();
		base.Item.shootSpeed = 16f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable() && proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = true;
			Main.projectile[proj].penetrate = 11;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmicKunai>().AddIngredient(889).AddIngredient<RuinousSoul>(5)
			.AddIngredient<Necroplasm>(20)
			.AddTile(134)
			.Register();
	}
}
