using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.MarniteArchitect;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class MarniteArchitectToga : ModItem, ILocalizedModType, IModType
{
	public static float PlacementSpeedBoost = 0.5f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PlacementSpeedBoost.ToPercent());

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/MarniteArchitect/MarniteArchitectToga_Legs", EquipType.Legs, this);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 5;
	}

	public override void UpdateEquip(Player player)
	{
		player.tileSpeed += PlacementSpeedBoost;
		player.wallSpeed += PlacementSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldBar", 2).AddIngredient(225, 15).AddIngredient(3086, 15)
			.AddIngredient(3081, 15)
			.AddTile(16)
			.Register();
	}

	public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
	{
		robes = true;
		equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Legs);
	}
}
