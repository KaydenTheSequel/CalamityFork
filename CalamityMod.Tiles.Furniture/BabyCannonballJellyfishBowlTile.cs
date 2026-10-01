using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture;

public class BabyCannonballJellyfishBowlTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(base.Type);
		base.AnimationFrameHeight = 36;
		AddMapEntry(new Color(64, 224, 208), CalamityUtils.GetItemName<BabyCannonballJellyfishBowl>());
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 13);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		int frameAmt = 8;
		int timeNeeded = 10;
		frameCounter++;
		if (frameCounter >= timeNeeded)
		{
			frame++;
			frameCounter = 0;
		}
		if (frame >= frameAmt)
		{
			frame = 0;
		}
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameYOffset = this.GetAnimationOffset(i, j, 8, 18, 18, 2, 2, base.AnimationFrameHeight);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.26f;
		g = 0.85f;
		b = 0.65f;
	}
}
