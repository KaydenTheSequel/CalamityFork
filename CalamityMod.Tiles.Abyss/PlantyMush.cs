using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Systems;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using CalamityMod.Walls;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Metadata;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

[LegacyName(new string[] { "Tenebris" })]
public class PlantyMush : ModTile
{
	public static readonly SoundStyle MineSound = new SoundStyle("CalamityMod/Sounds/Custom/PlantyMushMine", 3);

	private int animationFrameWidth = 234;

	public override void SetStaticDefaults()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Organic"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		base.DustType = 2;
		AddMapEntry(new Color(84, 102, 39), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.Abyss.PlantyMush>());
		base.HitSound = MineSound;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<AbyssGravel>());
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		global::CalamityMod.World.Abyss.FillTileWithWater(i, j);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = animationFrameWidth * TileFramingSystem.GetVariation4x4_01_Low0(i, j);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (WorldGen.genRand.NextBool(5) && !up.HasTile && !up2.HasTile && up.LiquidAmount > 0 && up2.LiquidAmount > 0 && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			up.TileType = (ushort)ModContent.TileType<AbyssKelp>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(7) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
		Tile down = Main.tile[i, j + 1];
		int vineLength = WorldGen.genRand.Next((int)Main.rockLayer, (int)(Main.rockLayer + (double)Main.maxTilesY * 0.143));
		if (!(down != null) || down.HasTile || down.TileType == (ushort)ModContent.TileType<ViperVines>() || down.LiquidAmount != byte.MaxValue || down.WallType != (ushort)ModContent.WallType<SafeAbyssGravelWall>() || down.LiquidType == 1)
		{
			return;
		}
		bool canGrowVine = false;
		for (int k = vineLength; k > vineLength - 10; k--)
		{
			Tile vineTile = Main.tile[i, k];
			if (vineTile.BottomSlope)
			{
				canGrowVine = false;
				break;
			}
			if (vineTile.HasTile && !vineTile.BottomSlope)
			{
				canGrowVine = true;
				break;
			}
		}
		if (canGrowVine)
		{
			int vineY = j + 1;
			Tile newVineTile = Main.tile[i, vineY];
			newVineTile.TileType = (ushort)ModContent.TileType<ViperVines>();
			newVineTile.Get<TileWallWireStateData>().HasTile = true;
			WorldGen.SquareTileFrame(i, vineY);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, vineY, 3);
			}
		}
	}
}
