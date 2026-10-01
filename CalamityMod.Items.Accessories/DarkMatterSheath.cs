using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "DarkGodsSheath" })]
public class DarkMatterSheath : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 62;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.stealthStrikeHalfCost = true;
		calamityPlayer.rogueStealthMax += 0.1f;
		calamityPlayer.darkGodSheath = true;
		player.GetCritChance<ThrowingDamageClass>() += 6f;
		player.GetDamage<ThrowingDamageClass>() += 0.06f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SilencingSheath>().AddIngredient<RuinMedallion>().AddIngredient<MeldConstruct>(5)
			.AddTile(412)
			.Register();
	}
}
