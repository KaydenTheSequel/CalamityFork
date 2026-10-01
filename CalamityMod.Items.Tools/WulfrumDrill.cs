using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

[LegacyName(new string[] { "WulfrumPickaxe" })]
public class WulfrumDrill : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 38;
		base.Item.damage = 5;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.pick = 35;
		base.Item.tileBoost++;
		base.Item.useAnimation = 16;
		base.Item.useTime = 5;
		base.Item.knockBack = 0.5f;
		base.Item.shoot = ModContent.ProjectileType<WulfrumDrillProj>();
		base.Item.UseSound = SoundID.Item23;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(5).AddTile(16).Register();
	}
}
