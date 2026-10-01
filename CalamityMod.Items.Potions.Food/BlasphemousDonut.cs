using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class BlasphemousDonut : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(122, 66, 59),
			new Color(206, 116, 59),
			new Color(198, 153, 113)
		};
		ItemID.Sets.IsFood[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(40, 26, 206, CalamityUtils.MinutesToFrames(60));
		base.Item.value = Item.sellPrice(0, 0, 60);
		base.Item.rare = 11;
	}
}
