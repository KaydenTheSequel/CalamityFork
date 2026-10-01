using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class SoaringPotion : ModItem, ILocalizedModType, IModType
{
	public static float FlightBoost = 0.1f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FlightBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(85, 181, 217),
			new Color(190, 237, 232),
			new Color(81, 114, 173)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(30, 30, ModContent.BuffType<Soaring>(), CalamityUtils.MinutesToFrames(8), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<SunkenSailfish>().AddIngredient(575)
			.AddTile(355)
			.AddConsumeIngredientCallback(Recipe.IngredientQuantityRules.Alchemy)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(15).AddIngredient(575)
			.AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
