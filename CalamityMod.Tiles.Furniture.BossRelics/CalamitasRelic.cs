using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Tiles.BaseTiles;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Furniture.BossRelics;

public class CalamitasRelic : BaseBossRelic
{
	public override string RelicTextureName => "CalamityMod/Tiles/Furniture/BossRelics/CalamitasRelic";

	public override int AssociatedItem => ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.BossRelics.CalamitasRelic>();
}
