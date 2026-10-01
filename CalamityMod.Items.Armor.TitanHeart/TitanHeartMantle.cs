using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.TitanHeart;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class TitanHeartMantle : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.1f;

	public static float RogueKnockbackBoost = 0.5f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueKnockbackBoost.ToPercent());

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
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 14;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().titanHeartMantle = true;
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolith>(20).AddIngredient<global::CalamityMod.Items.Materials.TitanHeart>().AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<TitanHeartBoots>())
			.Register();
	}
}
