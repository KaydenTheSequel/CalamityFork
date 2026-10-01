using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class NavystonePileBase : ModTile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/NavystonePile";

	public override void SetStaticDefaults()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		base.DustType = 1;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(0, 62, 84));
		base.DustType = 96;
		base.HitSound = SoundID.Dig;
	}
}
