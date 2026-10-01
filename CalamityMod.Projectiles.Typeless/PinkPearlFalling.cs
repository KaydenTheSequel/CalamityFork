using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class PinkPearlFalling : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/PinkPearl";

	public override bool Fired => false;

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.PinkPearlPile>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.PinkPearlPile>();

	public override int DustType => 119;
}
