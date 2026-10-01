using CalamityMod.Buffs.Alcohol;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class Moonshine : ModItem, ILocalizedModType, IModType
{
	public static float MaxLifePercentBoost = 0.33f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxLifePercentBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(237, 237, 218, 128),
			new Color(227, 219, 191, 128),
			new Color(209, 204, 194, 128)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(24, 28, ModContent.BuffType<MoonshineBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 7;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(31, 10).AddIngredient(1291).AddIngredient<AncientBoneDust>(5)
			.AddTile(94)
			.Register();
	}
}
