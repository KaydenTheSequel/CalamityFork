using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class ZenPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(216, 230, 236),
			new Color(137, 149, 173),
			new Color(102, 85, 128)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(24, 28, ModContent.BuffType<Zen>(), CalamityUtils.MinutesToFrames(12), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<PurifiedGel>(2).AddIngredient(313, 3)
			.AddTile(355)
			.AddConsumeIngredientCallback(Recipe.IngredientQuantityRules.Alchemy)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(5).AddIngredient<PurifiedGel>(2)
			.AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
