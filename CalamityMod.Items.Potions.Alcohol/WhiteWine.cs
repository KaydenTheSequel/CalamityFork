using CalamityMod.Buffs.Alcohol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Alcohol;

public class WhiteWine : ModItem, ILocalizedModType, IModType
{
	public static float FlightTimeRecoveryAmount = 0.66f;

	public static float FlightTimeLoss = 0.5f;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FlightTimeLoss.ToPercent());

	public override void SetStaticDefaults()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(242, 252, 177, 180),
			new Color(250, 252, 215, 180),
			new Color(228, 245, 181, 180)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(14, 44, ModContent.BuffType<WhiteWineBuff>(), CalamityUtils.MinutesToFrames(6), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 5;
	}

	public override void OnConsumeItem(Player player)
	{
		player.AddBuff(base.Item.buffType, base.Item.buffTime);
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient(31, 20).AddIngredient(1516).AddTile(94)
			.Register();
	}
}
