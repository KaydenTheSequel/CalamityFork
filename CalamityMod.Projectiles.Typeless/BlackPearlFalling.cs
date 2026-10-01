using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BlackPearlFalling : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/BlackPearl";

	public override bool Fired => false;

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.BlackPearlPile>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.BlackPearlPile>();

	public override int DustType => 82;
}
