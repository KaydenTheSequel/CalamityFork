using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Brimflame;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class BrimflameRobes : ModItem, ILocalizedModType, IModType
{
	public static float MagicDamageBoost = 0.07f;

	public static int MagicCritBoost = 7;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBoost.ToPercent());

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
		base.Item.defense = 16;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AshesofCalamity>(8).AddIngredient<UnholyCore>(4).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BrimflameBoots>())
			.Register();
	}
}
