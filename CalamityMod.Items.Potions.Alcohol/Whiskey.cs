using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class Whiskey : ModItem, ILocalizedModType, IModType
{
	public static float MaxDamageBoost = 0.15f;

	public static float MinDamageBoost = -0.15f;

	public static float TimeToDischarge = 600f;

	public static float TimeToRecharge = 300f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinDamageBoost.ToPercent(), MaxDamageBoost.ToPercent(), (TimeToDischarge / 60f).ToString("0.##"), (TimeToRecharge / 60f).ToString("0.##"));

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(227, 148, 11),
			new Color(235, 177, 5),
			new Color(250, 190, 12)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(18, 32, ModContent.BuffType<WhiskeyBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(31, 10).AddIngredient(5277).AddTile(94)
			.Register();
	}
}
