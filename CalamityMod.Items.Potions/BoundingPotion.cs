using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class BoundingPotion : ModItem, ILocalizedModType, IModType
{
	public static float JumpSpeedBoost = 0.25f;

	public static float JumpHeightPercentBoost = 0.2f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(JumpSpeedBoost.ToJumpSpeedPercent(), JumpHeightPercentBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(213, 255, 226),
			new Color(141, 220, 166)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(26, 38, ModContent.BuffType<BoundingBuff>(), CalamityUtils.MinutesToFrames(8), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient(3111).AddIngredient(314)
			.AddTile(13)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(10).AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
