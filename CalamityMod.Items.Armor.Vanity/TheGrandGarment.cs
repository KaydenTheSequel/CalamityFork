using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class TheGrandGarment : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Vanity/TheGrandGarment_Waist", EquipType.Waist, this);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 20;
		base.Item.rare = 1;
		base.Item.vanity = true;
		base.Item.Calamity().donorItem = true;
	}

	public override void EquipFrameEffects(Player player, EquipType type)
	{
		player.waist = (sbyte)EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Waist);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(225, 5).AddIngredient(259, 2).AddIngredient(1015)
			.AddTile(86)
			.Register();
	}
}
