using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class MediumPolypForestPile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileLighted[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(162, 55, 196));
		base.DustType = 97;
		base.HitSound = SoundID.Dig;
		base.SetStaticDefaults();
	}
}
