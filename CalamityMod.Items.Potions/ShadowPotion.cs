using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class ShadowPotion : ModItem, ILocalizedModType, IModType
{
	public static float StealthRegenBoost = 0.08f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(StealthRegenBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(45, 45, 45),
			new Color(0, 0, 0),
			new Color(95, 0, 36)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(20, 28, ModContent.BuffType<ShadowBuff>(), CalamityUtils.MinutesToFrames(8), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<Shadowfish>().AddIngredient(315)
			.AddTile(13)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(10).AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
