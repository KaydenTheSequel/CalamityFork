using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WhitePearlFalling : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/WhitePearl";

	public override bool Fired => false;

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.WhitePearlPile>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.WhitePearlPile>();

	public override int DustType => 149;
}
