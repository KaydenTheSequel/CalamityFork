using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class CinnamonRoll : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(245, 223, 181),
			new Color(222, 186, 147),
			new Color(176, 129, 106)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(18, 32, ModContent.BuffType<CinnamonRollBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 7;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(31, 10).AddIngredient(1328).AddTile(94)
			.Register();
	}
}
