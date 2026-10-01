using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class GazeOfCrysthamyr : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Mounts";

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.NPCHit56;
		base.Item.noMelee = true;
		base.Item.mountType = ModContent.MountType<Crysthamyr>();
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3857).AddIngredient(521, 10).AddIngredient<DarksunFragment>(10)
			.AddIngredient<ExodiumCluster>(25)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
