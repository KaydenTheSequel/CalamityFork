using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.DesertProwler;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class DesertProwlerShirt : ModItem, IBulkyArmor, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.05f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public string BulkTexture => "CalamityMod/Items/Armor/DesertProwler/DesertProwlerShirt_Bulk";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent());

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
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StormlionMandible>(3).AddIngredient(225, 10).AddTile(86)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<DesertProwlerPants>())
			.Register();
	}
}
