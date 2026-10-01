using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class Mangosteen : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(218, 238, 253),
			new Color(178, 199, 214),
			new Color(123, 99, 130)
		};
		ItemID.Sets.IsFood[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 5342;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(30, 32, 206, CalamityUtils.MinutesToFrames(5));
		base.Item.value = Item.sellPrice(0, 0, 20);
		base.Item.rare = 2;
	}
}
