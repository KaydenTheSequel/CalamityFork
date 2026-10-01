using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class SunkenKelp : ModTile
{
	private const int MaxChainLength = 18;

	private static readonly ushort[] ValidAnchors = new ushort[2]
	{
		(ushort)ModContent.TileType<EutrophicSand>(),
		(ushort)ModContent.TileType<HardenedEutrophicSand>()
	};

	private const short FrameBottom = 0;

	private const short FrameMid = 18;

	private const short FrameTop = 36;

	private const short FrameWidth = 18;

	public override void SetStaticDefaults()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Main.tileCut[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		TileID.Sets.IsVine[base.Type] = true;
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.HitSound = SoundID.Grass;
		base.DustType = 2;
		AddMapEntry(new Color(27, 112, 68));
	}

	public override void RandomUpdate(int i, int j)
	{
		if (IsTopmostSegment(i, j) && j > 0 && !Main.tile[i, j - 1].HasTile && IsSupportedFromBelow(i, j) && GetChainLengthUpward(i, j) < 18 && WorldGen.PlaceTile(i, j - 1, base.Type, mute: true, forced: true))
		{
			SetFrame(i, j - 1, 36);
			SetFrame(i, j, 18);
			UpdateBaseSegment(i, j + GetChainLengthDownward(i, j));
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		if (!IsSupportedFromBelow(i, j))
		{
			WorldGen.KillTile(i, j, fail: false, effectOnly: false, noItem: true);
			return false;
		}
		if (IsTopmostSegment(i, j))
		{
			SetFrame(i, j, 36);
		}
		else if (IsBottomSegment(i, j))
		{
			SetFrame(i, j, 0);
		}
		else
		{
			SetFrame(i, j, 18);
		}
		return false;
	}

	private static bool IsSupportedFromBelow(int i, int j)
	{
		if (j >= Main.maxTilesY - 1)
		{
			return false;
		}
		Tile below = Main.tile[i, j + 1];
		if (!below.HasTile)
		{
			return false;
		}
		ushort t = below.TileType;
		if (t == ModContent.TileType<SunkenKelp>())
		{
			return true;
		}
		ushort[] validAnchors = ValidAnchors;
		foreach (ushort anchor in validAnchors)
		{
			if (t == anchor)
			{
				return true;
			}
		}
		return false;
	}

	private static bool IsTopmostSegment(int i, int j)
	{
		if (j <= 0)
		{
			return true;
		}
		Tile above = Main.tile[i, j - 1];
		if (above.HasTile)
		{
			return above.TileType != ModContent.TileType<SunkenKelp>();
		}
		return true;
	}

	private static bool IsBottomSegment(int i, int j)
	{
		if (j >= Main.maxTilesY - 1)
		{
			return true;
		}
		Tile below = Main.tile[i, j + 1];
		if (below.HasTile)
		{
			return below.TileType != ModContent.TileType<SunkenKelp>();
		}
		return true;
	}

	private static int GetChainLengthUpward(int i, int j)
	{
		int length = 1;
		int y = j - 1;
		int vineType = ModContent.TileType<SunkenKelp>();
		while (y >= 0 && Main.tile[i, y].HasTile && Main.tile[i, y].TileType == vineType)
		{
			length++;
			y--;
			if (length > 18)
			{
				break;
			}
		}
		return length;
	}

	private static int GetChainLengthDownward(int i, int j)
	{
		int length = 0;
		int y = j + 1;
		for (int vineType = ModContent.TileType<SunkenKelp>(); y < Main.maxTilesY && Main.tile[i, y].HasTile && Main.tile[i, y].TileType == vineType; y++)
		{
			length++;
		}
		return length;
	}

	private static void SetFrame(int i, int j, short frameYRow)
	{
		Tile tile = Main.tile[i, j];
		tile.TileFrameX = (short)(WorldGen.genRand.Next(3) * 18);
		tile.TileFrameY = frameYRow;
	}

	private static void UpdateBaseSegment(int i, int bottomY)
	{
		Tile t = Main.tile[i, bottomY];
		if (t.HasTile && t.TileType == ModContent.TileType<SunkenKelp>())
		{
			SetFrame(i, bottomY, 0);
		}
	}
}
