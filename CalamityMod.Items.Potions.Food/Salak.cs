using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class Salak : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(203, 134, 81),
			new Color(162, 109, 68),
			new Color(253, 242, 233)
		};
		ItemID.Sets.IsFood[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 5342;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(28, 26, 26, CalamityUtils.MinutesToFrames(5));
		base.Item.value = Item.sellPrice(0, 0, 20);
		base.Item.rare = 1;
	}
}
