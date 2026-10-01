using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class SmallWideCoralEcho : ModTile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallWideCoral";

	public override void SetStaticDefaults()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(137, 154, 71));
		base.DustType = 225;
		RegisterItemDrop(2435);
		FlexibleTileWand.RubblePlacementMedium.AddVariations(2435, base.Type, default(int));
		base.SetStaticDefaults();
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.32f;
		g = 0.37f;
		b = 0.15f;
	}
}
