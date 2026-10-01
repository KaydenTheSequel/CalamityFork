using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags;

public class LiliesOfFinalityTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileSpelunker[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.addTile(base.Type);
		AddMapEntry(Color.White, CalamityUtils.GetItemName<LiliesOfFinality>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		RegisterItemDrop(ModContent.ItemType<LiliesOfFinality>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<LiliesOfFinality>(), base.Type, default(int));
		base.HitSound = new SoundStyle("CalamityMod/Sounds/Custom/LiliesOfFinalityTileHitSound");
		base.DustType = 91;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = (g = (b = 0.5f));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = ((!WorldGen.genRand.NextBool(3)) ? 175 : 91);
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 5 : 50);
	}
}
