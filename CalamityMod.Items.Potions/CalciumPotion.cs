using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class CalciumPotion : ModItem, ILocalizedModType, IModType
{
	public static float KnockbackResistance = 0.5f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(KnockbackResistance.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(194, 202, 134),
			new Color(149, 144, 86)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(18, 30, ModContent.BuffType<CalciumBuff>(), CalamityUtils.MinutesToFrames(20), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<AncientBoneDust>().AddTile(13)
			.Register()
			.DisableDecraft();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(5).AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
