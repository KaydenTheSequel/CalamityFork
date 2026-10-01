using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Reaver;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class ReaverScaleMail : ModItem, ILocalizedModType, IModType
{
	public static float DamageReductionBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageReductionBoost.ToPercent());

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
		base.Item.width = 34;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 24;
	}

	public override void UpdateEquip(Player player)
	{
		player.endurance += DamageReductionBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialBar>(15).AddIngredient<LivingShard>(3).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ReaverCuisses>())
			.Register();
	}
}
