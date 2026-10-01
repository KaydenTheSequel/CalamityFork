using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.Abyss;

public class MassiveRarePearlEcho : ModTile
{
	public override string Texture => "CalamityMod/Tiles/Abyss/AbyssAmbient/MassiveRarePearl";

	public override void SetStaticDefaults()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(106, 80, 102));
		base.DustType = 33;
		RegisterItemDrop(4412);
		FlexibleTileWand.RubblePlacementLarge.AddVariations(4412, base.Type, default(int));
		base.SetStaticDefaults();
	}
}
