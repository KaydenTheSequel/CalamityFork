using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class OrangeCoral : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		Main.tileLighted[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.HitSound = SoundID.Dig;
		base.DustType = 55;
		AddMapEntry(new Color(255, 144, 63));
		Main.tileShine2[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].Get<TileSpecialDrawData>().Flag0)
		{
			r = 0.92f;
			g = 0.62f;
			b = 0.42f;
		}
	}

	public override void PostTileFrame(int i, int j, int up, int down, int left, int right, int upLeft, int upRight, int downLeft, int downRight)
	{
		Tile tile = Main.tile[i, j];
		ref TileSpecialDrawData reference = ref tile.Get<TileSpecialDrawData>();
		tile = Main.tile[i - 1, j];
		int flag;
		if (tile.HasTile)
		{
			tile = Main.tile[i + 1, j];
			if (tile.HasTile)
			{
				tile = Main.tile[i, j - 1];
				if (tile.HasTile)
				{
					tile = Main.tile[i, j + 1];
					flag = ((!tile.HasTile) ? 1 : 0);
					goto IL_0078;
				}
			}
		}
		flag = 1;
		goto IL_0078;
		IL_0078:
		reference.Flag0 = (byte)flag != 0;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
