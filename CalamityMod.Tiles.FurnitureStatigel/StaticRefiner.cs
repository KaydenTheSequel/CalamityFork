using CalamityMod.Items.Placeables.FurnitureStatigel;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureStatigel;

public class StaticRefiner : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureStatigel.StaticRefiner>());
		base.AnimationFrameHeight = 54;
		base.AdjTiles = new int[1] { 220 };
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 243, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 6)
		{
			frame = (frame + 1) % 10;
			frameCounter = 0;
		}
	}
}
