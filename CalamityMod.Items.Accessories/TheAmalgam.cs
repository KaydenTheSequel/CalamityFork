using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class TheAmalgam : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Accessories";

	public static int NimbusDamage => 200.ScaleWithDifficulty();

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(9, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 34;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.rBrain = true;
		calamityPlayer.amalgam = true;
		player.brainOfConfusionItem = base.Item;
		calamityPlayer.HeatDebuffMultiplier += 2f;
		calamityPlayer.ColdDebuffMultiplier += 2f;
		calamityPlayer.SicknessDebuffMultiplier += 2f;
		calamityPlayer.WaterDebuffMultiplier += 2f;
		calamityPlayer.ElectricDebuffMultiplier += 2f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AmalgamatedBrain>().AddIngredient<AscendantSpiritEssence>(4).AddIngredient(3467, 10)
			.AddIngredient(3458, 10)
			.AddIngredient<PlagueCellCanister>(15)
			.AddIngredient<DepthCells>(15)
			.AddIngredient<EffulgentFeather>(8)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
