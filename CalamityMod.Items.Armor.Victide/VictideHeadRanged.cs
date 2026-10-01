using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Victide;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "VictideVisage" })]
public class VictideHeadRanged : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.defense = 3;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<VictideBreastplate>())
		{
			return legs.type == ModContent.ItemType<VictideGreaves>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalizedValue("SetBonus") + "\n" + CalamityUtils.GetTextValueFromModItem<VictideBreastplate>("CommonSetBonus");
		player.Calamity().victideSet = true;
		player.ignoreWater = true;
		if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
		{
			player.GetDamage<RangedDamageClass>() += 0.1f;
			player.lifeRegen += 3;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<RangedDamageClass>() += 0.05f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(3).AddTile(16).Register();
	}
}
