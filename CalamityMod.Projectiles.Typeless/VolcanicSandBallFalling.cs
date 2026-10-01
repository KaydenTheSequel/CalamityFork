using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class VolcanicSandBallFalling : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallVolcanic";

	public override bool Fired => false;

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.VolcanicSand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.VolcanicSand>();

	public override int DustType => 78;
}
