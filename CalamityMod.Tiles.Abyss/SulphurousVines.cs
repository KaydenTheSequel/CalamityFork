using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class SulphurousVines : ModTile
{
	private const int MaxVineHeight = 10;

	public override void SetStaticDefaults()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileCut[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileNoSunLight[base.Type] = false;
		AddMapEntry(new Color(0, 50, 0));
		base.HitSound = SoundID.Grass;
		base.DustType = 2;
		TileID.Sets.IsVine[base.Type] = true;
		TileID.Sets.ReplaceTileBreakDown[base.Type] = true;
		TileID.Sets.VineThreads[base.Type] = true;
		TileID.Sets.DrawFlipMode[base.Type] = 1;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		Main.instance.TilesRenderer.CrawlToTopOfVineAndAddSpecialPoint(j, i);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		if (!Framing.GetTileSafely(i, j - 1).HasTile)
		{
			WorldGen.KillTile(i, j);
			return true;
		}
		return true;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (WorldGen.genRand.NextBool(2) && Main.player[Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].cordage)
		{
			Item.NewItem((IEntitySource)new EntitySource_TileBreak(i, j), new Vector2((float)(i * 16) + 8f, (float)(j * 16) + 8f), 2996, 1, false, 0, false, false);
		}
		if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].HasTile && Main.tile[i, j + 1].TileType == ModContent.TileType<SulphurousVines>())
		{
			WorldGen.KillTile(i, j + 1);
			if (!Main.tile[i, j + 1].HasTile && Main.netMode != 0)
			{
				NetMessage.SendData(17, -1, -1, null, 0, i, (float)j + 1f);
			}
		}
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (closer && Main.rand.NextBool(((double)j > Main.worldSurface) ? 200 : 300) && (double)j > Main.worldSurface - (double)200.TilesToPixels())
		{
			Dust obj = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 280, 280, 304, 0.2f, 0f, 0, (Color)(((double)j > Main.worldSurface) ? new Color(200, 255, 0) : Color.Lime), Main.rand.NextFloat(1f, 2f))];
			obj.noGravity = true;
			obj.noLight = true;
			obj.fadeIn = 2.5f;
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile below = Main.tile[i, j + 1];
		if (below.HasTile || below.LiquidType != 0 || below.LiquidAmount < 128)
		{
			return;
		}
		bool growVine = false;
		for (int vineOriginYPos = j; vineOriginYPos > j - 10; vineOriginYPos--)
		{
			if (Main.tile[i, vineOriginYPos].BottomSlope)
			{
				growVine = false;
				break;
			}
			if (Main.tile[i, vineOriginYPos].HasTile && !Main.tile[i, vineOriginYPos].BottomSlope && Main.tileSolid[Main.tile[i, vineOriginYPos].TileType])
			{
				growVine = true;
				break;
			}
		}
		if (growVine)
		{
			int y = j + 1;
			Main.tile[i, y].TileType = (ushort)ModContent.TileType<SulphurousVines>();
			Main.tile[i, y].TileFrameX = (short)(WorldGen.genRand.Next(8) * 18);
			Main.tile[i, y].TileFrameY = 72;
			Main.tile[i, y].Get<TileWallWireStateData>().HasTile = true;
			Main.tile[i, j].TileFrameX = (short)(WorldGen.genRand.Next(12) * 18);
			Main.tile[i, j].TileFrameY = (short)(WorldGen.genRand.Next(4) * 18);
			WorldGen.SquareTileFrame(i, y);
			WorldGen.SquareTileFrame(i, j);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, y, 3);
			}
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		float brightness = 0.7f;
		brightness *= MathF.Sin((float)(-j) / 40f + (float)Main.GameUpdateCount * 0.01f + (float)i);
		brightness += 0.5f;
		r = 0.68f;
		g = 1f;
		b = 0.78f;
		r *= brightness;
		g *= brightness;
		b *= brightness;
	}
}
