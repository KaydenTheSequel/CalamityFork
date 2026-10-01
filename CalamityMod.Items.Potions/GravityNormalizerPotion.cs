using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class GravityNormalizerPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(255, 164, 94),
			new Color(109, 242, 196),
			new Color(255, 255, 191)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(24, 36, ModContent.BuffType<GravityNormalizerBuff>(), CalamityUtils.MinutesToFrames(8), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 7;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<AldebaranAlewife>().AddIngredient<AureusCell>()
			.AddTile(355)
			.AddConsumeIngredientCallback(Recipe.IngredientQuantityRules.Alchemy)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(10).AddIngredient<AureusCell>()
			.AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
