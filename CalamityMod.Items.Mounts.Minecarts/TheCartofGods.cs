using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts.Minecarts;

[LegacyName(new string[] { "DoGCart" })]
public class TheCartofGods : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Mounts";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 36;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item68;
		base.Item.noMelee = true;
		base.Item.mountType = ModContent.MountType<DoGCartMount>();
		base.Item.value = Item.sellPrice(0, 30);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(10).AddIngredient<AscendantSpiritEssence>().AddIngredient(530, 60)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
