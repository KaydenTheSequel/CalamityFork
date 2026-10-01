using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class PhotosynthesisPotion : ModItem, ILocalizedModType, IModType
{
	public static int IncreasedHeartHeal = 5;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedHeartHeal);

	public override void SetStaticDefaults()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(246, 235, 143),
			new Color(230, 204, 121),
			new Color(214, 173, 78)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(30, 34, ModContent.BuffType<PhotosynthesisBuff>(), CalamityUtils.MinutesToFrames(8), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient(313, 3).AddIngredient<EssenceofSunlight>()
			.AddTile(355)
			.AddConsumeIngredientCallback(Recipe.IngredientQuantityRules.Alchemy)
			.AddDecraftCondition(Condition.Hardmode)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(5).AddIngredient<EssenceofSunlight>()
			.AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
