using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class MediumRadiantReefPile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
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
		AddMapEntry(new Color(136, 129, 154));
		base.DustType = 93;
		base.HitSound = SoundID.Dig;
		base.SetStaticDefaults();
	}
}
