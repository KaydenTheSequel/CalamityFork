using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Tiles.BaseTiles;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Furniture.BossRelics;

public class YharonRelic : BaseBossRelic
{
	public override string RelicTextureName => "CalamityMod/Tiles/Furniture/BossRelics/YharonRelic";

	public override int AssociatedItem => ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.BossRelics.YharonRelic>();
}
