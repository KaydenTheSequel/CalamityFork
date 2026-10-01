using CalamityMod.Systems;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class Shellstone : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.HitSound = SoundID.Tink;
		base.DustType = 17;
		AddMapEntry(new Color(123, 127, 170));
		this.RegisterBlendMergeWith(ModContent.TileType<VolcanicSand>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(57);
		this.RegisterBlendMergeWith(59);
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
		if (!Below.HasTile && Below.LiquidType == 0 && !Tile.BottomSlope && Main.rand.NextBool(10))
		{
			Below.TileType = (ushort)ModContent.TileType<RefractiveHangingCoral>();
			Below.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
	}
}
