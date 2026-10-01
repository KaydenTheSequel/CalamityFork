using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class LongScarletSeagrass : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		base.AnimationFrameHeight = 50;
		Main.tileLighted[base.Type] = true;
		Main.tileCut[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.ReplaceTileBreakUp[base.Type] = true;
		TileID.Sets.SwaysInWindBasic[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(187, 43, 44));
		base.DustType = 125;
		base.HitSound = SoundID.Grass;
		base.SetStaticDefaults();
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = -30;
		height = 48;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (++frameCounter >= 12)
		{
			frameCounter = 0;
			if (++frame >= 6)
			{
				frame = 0;
			}
		}
	}
}
