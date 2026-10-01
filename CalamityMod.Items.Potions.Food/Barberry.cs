using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class Barberry : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(112, 1, 25),
			new Color(136, 69, 137),
			new Color(160, 32, 95)
		};
		ItemID.Sets.IsFood[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 5342;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(26, 32, 206, CalamityUtils.MinutesToFrames(5));
		base.Item.value = Item.sellPrice(0, 0, 20);
		base.Item.rare = 2;
	}
}
