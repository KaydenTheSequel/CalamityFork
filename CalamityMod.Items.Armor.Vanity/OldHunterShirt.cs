using CalamityMod.Items.Armor.DesertProwler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class OldHunterShirt : ModItem, IBulkyArmor, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public string BulkTexture => "CalamityMod/Items/Armor/Vanity/OldHunterShirt_Bulk";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Vanity/OldHunterShirt_Back", EquipType.Back, this);
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
		base.Item.vanity = true;
	}

	public override void EquipFrameEffects(Player player, EquipType type)
	{
		player.back = (sbyte)EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Back);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DesertProwlerShirt>().AddIngredient(1119).AddTile(228)
			.Register();
	}
}
