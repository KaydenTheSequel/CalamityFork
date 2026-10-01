using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "LumenousAmulet" })]
[AutoloadEquip(new EquipType[] { EquipType.Neck })]
public class DiamondOfTheDeep : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.dOfTheDeep = true;
		calamityPlayer.dOfTheDeepVisual = !hideVisual;
		calamityPlayer.WaterDebuffMultiplier += 0.75f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaSpiritAmulet>().AddIngredient<AbyssGravel>(10).AddIngredient<DepthCells>(5)
			.AddIngredient<PyreMantle>(10)
			.AddIngredient<ScoriaBar>(5)
			.AddIngredient<Voidstone>(10)
			.AddIngredient<Lumenyl>(5)
			.AddTile(134)
			.Register();
	}
}
