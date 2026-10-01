using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class SmallLimestoneCobblePileBase : ModTile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallLimestoneCobblePile";

	public override void SetStaticDefaults()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		base.DustType = 1;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(174, 120, 91));
		base.DustType = 96;
		base.HitSound = SoundID.Dig;
	}
}
