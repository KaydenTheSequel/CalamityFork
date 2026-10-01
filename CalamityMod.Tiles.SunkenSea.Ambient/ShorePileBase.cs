using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class ShorePileBase : ModTile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/ShorePile";

	public override void SetStaticDefaults()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		base.DustType = 1;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(189, 120, 94));
		base.DustType = 96;
		base.HitSound = SoundID.Dig;
	}
}
