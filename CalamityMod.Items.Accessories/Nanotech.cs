using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class Nanotech : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 46;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.nanotech = true;
		calamityPlayer.electricianGlove = true;
		calamityPlayer.filthyGlove = true;
		calamityPlayer.bloodyGlove = true;
		player.GetDamage<ThrowingDamageClass>() += 0.15f;
		player.Calamity().rogueVelocity += 0.15f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RogueEmblem>().AddIngredient<MoonstoneCrown>().AddIngredient<ElectriciansGlove>()
			.AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
