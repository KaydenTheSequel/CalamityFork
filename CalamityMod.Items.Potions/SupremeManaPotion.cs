using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class SupremeManaPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(0, 255, 250),
			new Color(26, 117, 177),
			new Color(160, 82, 144)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(34, 38, 0, 0, useGulpSound: true);
		base.Item.healMana = 400;
		base.Item.value = Item.sellPrice(0, 0, 10);
		base.Item.rare = 11;
	}

	public override void AddRecipes()
	{
		CreateRecipe(15).AddIngredient(2209, 15).AddIngredient<Necroplasm>().AddTile(13)
			.Register()
			.DisableDecraft();
	}
}
