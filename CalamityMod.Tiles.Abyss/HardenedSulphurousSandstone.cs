using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class HardenedSulphurousSandstone : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		base.DustType = 32;
		AddMapEntry(new Color(76, 58, 59));
		base.HitSound = SoundID.Dig;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<SulphurousShale>());
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (CalamityUtils.ParanoidTileRetrieval(i, j + 1).HasTile && CalamityUtils.ParanoidTileRetrieval(i, j + 1).TileType == (ushort)ModContent.TileType<SulphurousVines>())
		{
			WorldGen.KillTile(i, j + 1);
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		int vineLength = WorldGen.genRand.Next((int)Main.rockLayer, (int)(Main.rockLayer + (double)Main.maxTilesY * 0.143));
		int nearbyVineCount = 0;
		for (int x = i - 15; x <= i + 15; x++)
		{
			for (int y = j - 15; y <= j + 15; y++)
			{
				if (WorldGen.InWorld(x, y) && CalamityUtils.ParanoidTileRetrieval(x, y).HasTile && CalamityUtils.ParanoidTileRetrieval(x, y).TileType == (ushort)ModContent.TileType<SulphurousVines>())
				{
					nearbyVineCount++;
				}
			}
		}
		if (!(Main.tile[i, j + 1] != null) || nearbyVineCount >= 5 || j < SulphurousSea.VineGrowTopLimit || Main.tile[i, j + 1].HasTile || Main.tile[i, j + 1].TileType == (ushort)ModContent.TileType<SulphurousVines>())
		{
			return;
		}
		if (Main.tile[i, j + 1].LiquidAmount == byte.MaxValue && Main.tile[i, j + 1].LiquidType != 1)
		{
			bool canGrowVine = false;
			for (int k = vineLength; k > vineLength - 10; k--)
			{
				if (CalamityUtils.ParanoidTileRetrieval(i, k).BottomSlope)
				{
					canGrowVine = false;
					break;
				}
				if (Main.tile[i, k].HasTile && !Main.tile[i, k].BottomSlope)
				{
					canGrowVine = true;
					break;
				}
			}
			if (canGrowVine)
			{
				int vineY = j + 1;
				Main.tile[i, vineY].TileType = (ushort)ModContent.TileType<SulphurousVines>();
				Main.tile[i, vineY].Get<TileWallWireStateData>().HasTile = true;
				WorldGen.SquareTileFrame(i, vineY);
				if (Main.dedServ)
				{
					NetMessage.SendTileSquare(-1, i, vineY, 3);
				}
			}
		}
		Main.tile[i, j].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
		Main.tile[i, j].Get<TileWallWireStateData>().IsHalfBlock = false;
	}
}
