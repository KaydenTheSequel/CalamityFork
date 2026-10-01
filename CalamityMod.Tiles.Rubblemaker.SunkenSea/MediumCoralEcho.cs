using CalamityMod.Items.Placeables.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class MediumCoralEcho : ModTile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/MediumCoral";

	public override void SetStaticDefaults()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(233, 132, 58));
		base.DustType = 225;
		RegisterItemDrop(ModContent.ItemType<EutrophicSand>());
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<EutrophicSand>(), base.Type, default(int));
		base.SetStaticDefaults();
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.65f;
		g = 0.39f;
		b = 0.58f;
	}
}
