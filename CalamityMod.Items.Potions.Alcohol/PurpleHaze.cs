using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

[LegacyName(new string[] { "FabsolsVodka" })]
public class PurpleHaze : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.25f;

	public static float StealthDamageLoss = 0.25f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), StealthDamageLoss.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(239, 123, 202),
			new Color(187, 56, 158),
			new Color(165, 47, 255)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(30, 42, ModContent.BuffType<PurpleHazeBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(31, 10).AddIngredient(4295).AddTile(94)
			.Register();
	}
}
