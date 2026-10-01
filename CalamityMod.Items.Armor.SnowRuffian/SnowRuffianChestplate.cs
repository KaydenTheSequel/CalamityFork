using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.SnowRuffian;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class SnowRuffianChestplate : ModItem, ILocalizedModType, IModType
{
	public static int RangedCritBoost = 4;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedCritBoost);

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/SnowRuffian/SnowRuffianChestplate_Back", EquipType.Back, this);
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/SnowRuffian/SnowRuffianChestplate_Neck", EquipType.Neck, this);
		}
	}

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Body);
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlot] = true;
			ArmorIDs.Body.Sets.HidesArms[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 4;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2503, 20).AddIngredient(225, 6).AddIngredient(5070, 2)
			.AddTile(16)
			.Register();
	}
}
