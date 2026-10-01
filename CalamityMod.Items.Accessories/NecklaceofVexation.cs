using System.Collections.Generic;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class NecklaceofVexation : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().vexation = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(935).AddIngredient<PerennialBar>(2).AddTile(134)
			.Register();
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		Player player = Main.LocalPlayer;
		if (player != null)
		{
			list.FindAndReplace("[DAMAGE]", (0.3f * (1f - (float)player.statLife / (float)player.statLifeMax2)).ToPercent());
		}
	}
}
