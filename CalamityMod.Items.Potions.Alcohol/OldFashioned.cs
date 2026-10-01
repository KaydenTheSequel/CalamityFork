using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class OldFashioned : ModItem, ILocalizedModType, IModType
{
	public static readonly float DamageBoostMultiplier = 1.25f;

	public static readonly float DamageReductionMultiplier = 0.75f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoostMultiplier.ToString("N2"), DamageReductionMultiplier.ToString("N2"));

	public override void SetStaticDefaults()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(255, 118, 3),
			new Color(255, 200, 82),
			new Color(255, 228, 122)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(30, 38, ModContent.BuffType<OldFashionedBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(31, 10).AddIngredient(4290).AddTile(94)
			.Register();
	}
}
