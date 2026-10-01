using CalamityMod.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class SigilofCalamitas : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 8));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.manaMagnet = true;
		player.statManaMax2 += 100;
		player.GetDamage<MagicDamageClass>() += 0.15f;
		player.manaCost -= 0.1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2220).AddIngredient<ScoriaBar>(5).AddIngredient<AshesofCalamity>(15)
			.AddTile(134)
			.Register();
	}
}
