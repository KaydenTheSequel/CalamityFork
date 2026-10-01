using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class ZergPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(223, 135, 244),
			new Color(94, 74, 213),
			new Color(156, 217, 246)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(24, 34, ModContent.BuffType<Zerg>(), CalamityUtils.MinutesToFrames(7), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<PurifiedGel>(2).AddIngredient(318, 2)
			.AddTile(355)
			.AddConsumeIngredientCallback(Recipe.IngredientQuantityRules.Alchemy)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(5).AddIngredient<PurifiedGel>(2)
			.AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
