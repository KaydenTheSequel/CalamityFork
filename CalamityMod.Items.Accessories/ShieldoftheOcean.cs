using CalamityMod.Items.Armor.Victide;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ShieldoftheOcean : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.defense = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
		{
			player.statDefense += 5;
		}
		if ((player.armor[0].type == ModContent.ItemType<VictideHeadMagic>() || player.armor[0].type == ModContent.ItemType<VictideHeadSummon>() || player.armor[0].type == ModContent.ItemType<VictideHeadMelee>() || player.armor[0].type == ModContent.ItemType<VictideHeadRanged>() || player.armor[0].type == ModContent.ItemType<VictideHeadRogue>()) && player.armor[1].type == ModContent.ItemType<VictideBreastplate>() && player.armor[2].type == ModContent.ItemType<VictideGreaves>())
		{
			player.moveSpeed += 0.1f;
			player.lifeRegen += 2;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(5).AddIngredient(2626, 5).AddTile(16)
			.Register();
	}
}
