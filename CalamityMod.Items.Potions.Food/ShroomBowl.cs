using CalamityMod.Items.Critters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

public class ShroomBowl : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(248, 195, 96),
			new Color(115, 57, 15),
			new Color(230, 124, 45)
		};
		ItemID.Sets.IsFood[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(30, 24, 206, CalamityUtils.MinutesToFrames(10), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 10);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(356).AddIngredient<ShroombleItem>().AddTile(96)
			.DisableDecraft()
			.Register();
	}
}
