using System.Collections.Generic;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Placeables.Abyss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

[LegacyName(new string[] { "SunkenStew" })]
public class HadalStew : ModItem, ILocalizedModType, IModType
{
	public static int BuffType = 207;

	public static int BuffDuration = CalamityUtils.MinutesToFrames(8);

	public static int SicknessDuration = CalamityUtils.SecondsToFrames(50);

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BuffDuration / 3600);

	public override void SetStaticDefaults()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 30;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[4]
		{
			new Color(185, 117, 70),
			new Color(214, 98, 44),
			new Color(235, 156, 117),
			new Color(89, 54, 46)
		};
		ItemID.Sets.IsFood[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToHealingPotion(28, 18, 100);
		base.Item.value = Item.sellPrice(0, 0, 40);
		base.Item.rare = 2;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		string duration = (Main.LocalPlayer.pStone ? ((float)(SicknessDuration / 60) * 0.75f).ToString("N1") : (SicknessDuration / 60).ToString());
		list.FindAndReplace("[S]", duration);
	}

	public override void ModifyPotionDelay(Player player, ref int baseDelay)
	{
		baseDelay -= CalamityUtils.SecondsToFrames(60) - SicknessDuration;
	}

	public override void OnConsumeItem(Player player)
	{
		player.AddBuff(BuffType, BuffDuration);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AbyssGravel>(3).AddIngredient<CoastalDemonfish>().AddIngredient(356)
			.AddTile(96)
			.Register();
		CreateRecipe().AddIngredient<Voidstone>(3).AddIngredient<CoastalDemonfish>().AddIngredient(356)
			.AddTile(96)
			.Register();
	}
}
