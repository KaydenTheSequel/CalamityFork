using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class DunesandBallFalling : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallDune";

	public override bool Fired => false;

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.Dunesand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.Dunesand>();

	public override int DustType => 147;
}
