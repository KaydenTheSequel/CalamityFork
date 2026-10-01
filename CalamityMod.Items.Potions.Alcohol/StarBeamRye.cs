using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class StarBeamRye : ModItem, ILocalizedModType, IModType
{
	public static float MagicDmgMult = 0.9f;

	public static int ManaRegenBoost = 30;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDmgMult);

	public override void SetStaticDefaults()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(89, 18, 10),
			new Color(102, 39, 14),
			new Color(128, 31, 9)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(20, 34, ModContent.BuffType<StarBeamRyeBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(31, 10).AddIngredient(4297).AddTile(94)
			.Register();
	}
}
