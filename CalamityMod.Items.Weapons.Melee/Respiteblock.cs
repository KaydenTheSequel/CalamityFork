using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Respiteblock : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 108;
		base.Item.height = 40;
		base.Item.damage = 70;
		base.Item.knockBack = 9f;
		base.Item.useTime = 10;
		base.Item.useAnimation = 20;
		base.Item.axe = 122;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.autoReuse = false;
		base.Item.shoot = ModContent.ProjectileType<RespiteblockHoldout>();
		base.Item.shootSpeed = 1f;
		base.Item.rare = 10;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.Calamity().donorItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3098).AddIngredient(3458, 4).AddIngredient<EssenceofSunlight>(13)
			.AddTile(412)
			.Register();
	}
}
