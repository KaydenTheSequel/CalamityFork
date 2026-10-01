using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class Everclear : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.25f;

	public static int RegenLoss = 10;

	public static float DefenseLossPercent = 0.3f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), RegenLoss.ToRegenPerSecond(), DefenseLossPercent.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(153, 168, 162, 180),
			new Color(198, 205, 207, 180)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(14, 42, ModContent.BuffType<EverclearBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient(31, 20).AddIngredient(236).AddIngredient(521, 10)
			.AddTile(94)
			.Register();
	}
}
