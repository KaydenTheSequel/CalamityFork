using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Tiles.AstralDesert;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AstralSandBallFalling : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallAstral";

	public override bool Fired => false;

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.AstralDesert.AstralSand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.Astral.AstralSand>();

	public override int DustType => 108;
}
