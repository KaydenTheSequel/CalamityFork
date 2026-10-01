using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.SnowRuffian;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class SnowRuffianMask : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.05f;

	public static float GlideFallSpeedMult = 0.9f;

	public static int SetBonusFrostburnDuration = CalamityUtils.SecondsToFrames(2);

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent());

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/SnowRuffian/SnowRuffianWings", EquipType.Wings, this, null, new SnowRuffianWings());
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 2;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<SnowRuffianChestplate>())
		{
			return legs.type == ModContent.ItemType<SnowRuffianGreaves>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.Calamity().snowRuffianSet = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusFrostburnDuration.FramesToSeconds());
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2503, 10).AddIngredient(225, 4).AddIngredient(5070)
			.AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<SnowRuffianChestplate>())
			.Register();
	}
}
