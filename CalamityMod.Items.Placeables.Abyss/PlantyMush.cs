using CalamityMod.Tiles.Abyss;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Abyss;

public class PlantyMush : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Abyss.PlantyMush>());
		base.Item.value = Item.sellPrice(0, 0, 0, 20);
	}

	public override void CaughtFishStack(ref int stack)
	{
		stack = Main.rand.Next(5, 16);
	}
}
