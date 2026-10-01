using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class Cometfruit : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(153, 100, 184),
			new Color(181, 141, 166),
			new Color(147, 200, 221)
		};
		ItemID.Sets.IsFood[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 5342;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(32, 32, 206, CalamityUtils.MinutesToFrames(5));
		base.Item.value = Item.sellPrice(0, 0, 20);
		base.Item.rare = 2;
	}
}
