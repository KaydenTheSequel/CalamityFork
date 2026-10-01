using CalamityMod.Buffs.Alcohol;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class Vodka : ModItem, ILocalizedModType, IModType
{
	public static float DebuffBoost = 0.25f;

	public static float DebuffLoss = 0.5f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DebuffBoost.ToPercent(), DebuffLoss.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(219, 219, 208, 180),
			new Color(181, 181, 176, 180)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(14, 36, ModContent.BuffType<VodkaBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(31, 10).AddIngredient<PurifiedGel>(10).AddIngredient<StarblightSoot>(10)
			.AddTile(94)
			.Register();
	}
}
