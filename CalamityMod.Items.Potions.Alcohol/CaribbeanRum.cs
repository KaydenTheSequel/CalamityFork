using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class CaribbeanRum : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.25f;

	public static float GravityMultiplier = 0.5f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent(), (1f - GravityMultiplier).ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(105, 29, 14),
			new Color(128, 39, 22),
			new Color(138, 28, 7)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(14, 32, ModContent.BuffType<CaribbeanRumBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 5;
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient(31, 20).AddIngredient(1519).AddTile(94)
			.Register();
	}
}
