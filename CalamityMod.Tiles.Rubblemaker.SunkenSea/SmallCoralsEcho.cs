using CalamityMod.Items.Placeables.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class SmallCoralsEcho : ModTile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallCorals";

	public override void SetStaticDefaults()
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = false;
		Main.tileCut[base.Type] = false;
		Main.tileSolid[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.ReplaceTileBreakUp[base.Type] = true;
		TileID.Sets.SwaysInWindBasic[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(178, 28, 153));
		base.DustType = 225;
		base.HitSound = SoundID.Grass;
		RegisterItemDrop(ModContent.ItemType<EutrophicSand>());
		base.SetStaticDefaults();
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = -16;
		height = 32;
	}
}
