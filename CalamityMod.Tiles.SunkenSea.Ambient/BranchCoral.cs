using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class BranchCoral : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		Main.tileSpelunker[base.Type] = false;
		base.HitSound = SoundID.Dig;
		base.DustType = 125;
		AddMapEntry(new Color(210, 66, 90));
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 32;
		TileObjectData.newTile.StyleWrapLimit = 8;
		TileObjectData.newTile.RandomStyleRange = 8;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(1, 0);
		TileObjectData.addAlternate(8);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(16);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(24);
		TileObjectData.addTile(base.Type);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.464f;
		g = 0.244f;
		b = 0.244f;
	}
}
