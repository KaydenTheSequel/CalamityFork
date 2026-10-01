using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[]
{
	EquipType.HandsOn,
	EquipType.HandsOff
})]
public class ElementalGauntlet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 38;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.eGauntlet = true;
		player.kbGlove = true;
		player.autoReuseGlove = true;
		player.meleeScaleGlove = true;
		calamityPlayer.gloveLevel = 5;
		player.GetDamage<TrueMeleeDamageClass>() += 0.1f;
		calamityPlayer.eGauntletVisuals = !hideVisual;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1343).AddIngredient<AscendantSpiritEssence>(4).AddTile<CosmicAnvil>()
			.Register();
	}
}
