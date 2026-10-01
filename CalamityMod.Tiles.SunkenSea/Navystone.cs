using CalamityMod.Systems;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class Navystone : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.DustType = 96;
		AddMapEntry(new Color(28, 72, 96));
		base.HitSound = SoundID.Tink;
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile Tile = Framing.GetTileSafely(i, j);
		Tile Below = Framing.GetTileSafely(i, j + 1);
		Framing.GetTileSafely(i, j - 1);
		if (!Below.HasTile && Below.LiquidType <= 0 && !Tile.BottomSlope && Main.rand.NextBool(10))
		{
			Below.TileType = (ushort)ModContent.TileType<DepthVines>();
			Below.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
	}
}
