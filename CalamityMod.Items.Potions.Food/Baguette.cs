using CalamityMod.Buffs.Potions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class Baguette : ModItem, ILocalizedModType, IModType
{
	public static int RedWineBuffedHealValue = 250;

	public static int RedWineBuffedRegenLoss = 4;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RedWineBuffedHealValue, RedWineBuffedRegenLoss.ToRegenPerSecond());

	public override void SetStaticDefaults()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(231, 137, 159),
			new Color(179, 104, 56),
			new Color(108, 47, 16)
		};
		ItemID.Sets.IsFood[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(52, 38, 26, CalamityUtils.MinutesToFrames(5));
		base.Item.value = Item.sellPrice(0, 0, 1);
		base.Item.rare = 1;
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Food;
	}

	public override void OnConsumeItem(Player player)
	{
		player.AddBuff(26, base.Item.buffTime);
		player.AddBuff(ModContent.BuffType<BaguetteBuff>(), base.Item.buffTime);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1727, 20).AddTile(17).Register();
	}
}
