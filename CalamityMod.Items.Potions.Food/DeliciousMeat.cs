using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class DeliciousMeat : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<DisgustingMeat>();
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(147, 197, 206),
			new Color(94, 131, 168)
		};
		ItemID.Sets.IsFood[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(32, 30, 206, CalamityUtils.MinutesToFrames(30));
		base.Item.value = Item.buyPrice(0, 0, 50);
		base.Item.rare = 5;
	}
}
