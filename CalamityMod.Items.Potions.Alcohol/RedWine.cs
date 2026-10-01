using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class RedWine : ModItem, ILocalizedModType, IModType
{
	public static float VerticalSpeedBoost = 0.1f;

	public static float FlightTimeLoss = 0.25f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(VerticalSpeedBoost.ToPercent(), FlightTimeLoss.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(54, 5, 21),
			new Color(82, 9, 36),
			new Color(105, 4, 29)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(14, 48, ModContent.BuffType<RedWineBuff>(), CalamityUtils.MinutesToFrames(6));
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 5;
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient(31, 20).AddIngredient(1518).AddTile(94)
			.Register();
	}
}
