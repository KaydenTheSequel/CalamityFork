using CalamityMod.Systems;
using CalamityMod.Tiles.AstralSnow;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Merges;

public sealed class AstralSnowMerge : TileBlendTexture
{
	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.AstralSnow.AstralSnow>();
}
