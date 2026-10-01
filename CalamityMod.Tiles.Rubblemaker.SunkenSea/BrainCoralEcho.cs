using CalamityMod.Items.Placeables.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class BrainCoralEcho : ModTile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/BrainCoral";

	public override void SetStaticDefaults()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.addTile(base.Type);
		base.DustType = 253;
		AddMapEntry(new Color(36, 61, 111));
		RegisterItemDrop(ModContent.ItemType<HardenedEutrophicSand>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<HardenedEutrophicSand>(), base.Type, default(int));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
