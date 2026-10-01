using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.FathomSwarmer;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class FathomSwarmerBreastplate : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

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
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 20;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
		player.Calamity().fathomSwarmerBreastplate = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(12).AddIngredient<PlantyMush>(10).AddIngredient<DepthCells>(5)
			.AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<FathomSwarmerBoots>())
			.Register();
	}
}
