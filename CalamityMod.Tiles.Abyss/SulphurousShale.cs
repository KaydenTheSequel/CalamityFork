using CalamityMod.BiomeManagers;
using CalamityMod.Systems;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using CalamityMod.Waters;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class SulphurousShale : ModTile
{
	private int animationFrameWidth = 234;

	public static readonly SoundStyle MineSound = new SoundStyle("CalamityMod/Sounds/Custom/AbyssGravelMine", 3);

	public override void SetStaticDefaults()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		AddMapEntry(new Color(57, 44, 93));
		base.MineResist = 3f;
		base.MinPick = 65;
		base.HitSound = MineSound;
		base.DustType = 33;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<AbyssGravel>());
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		global::CalamityMod.World.Abyss.FillTileWithWater(i, j);
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (!Main.dedServ && Main.LocalPlayer.InModBiome(ModContent.GetInstance<AbyssLayer1Biome>()))
		{
			Main.SceneMetrics.ActiveFountainColor = SulphuricDepthsWater.Instance.Slot;
		}
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
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
		if (Main.tile[i, j + 1] != null && nearbyVineCount < 5 && j >= SulphurousSea.VineGrowTopLimit && !Main.tile[i, j + 1].HasTile && Main.tile[i, j + 1].TileType != (ushort)ModContent.TileType<SulphurousVines>() && Main.tile[i, j + 1].LiquidAmount == byte.MaxValue && Main.tile[i, j + 1].LiquidType != 1)
		{
			bool canGrowVine = false;
			for (int k = vineLength; k > vineLength - 10; k--)
			{
				if (Main.tile[i, k].BottomSlope)
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
			Main.tile[i, j].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
			Main.tile[i, j].Get<TileWallWireStateData>().IsHalfBlock = false;
		}
		Tile tile = Main.tile[i, j];
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (WorldGen.genRand.NextBool(10) && !up.HasTile && !up2.HasTile && up.LiquidAmount > 0 && up2.LiquidAmount > 0 && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			up.TileType = (ushort)ModContent.TileType<SulphurTentacleCorals>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(22) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = animationFrameWidth * TileFramingSystem.GetVariation4x4_012_Low0(i, j);
	}
}
