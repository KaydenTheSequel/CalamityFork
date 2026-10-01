using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class CeaselessHungerPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(12, 18, 28),
			new Color(110, 197, 212),
			new Color(158, 81, 153)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(22, 32, ModContent.BuffType<CeaselessHunger>(), CalamityUtils.SecondsToFrames(20), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 10);
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(126, 4).AddIngredient<DarkPlasma>().AddTile(355)
			.AddConsumeIngredientCallback(Recipe.IngredientQuantityRules.Alchemy)
			.Register();
		CreateRecipe(8).AddIngredient(126, 8).AddIngredient<BloodOrb>(10).AddIngredient<DarkPlasma>()
			.AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
