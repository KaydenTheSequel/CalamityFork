using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Furniture;

public class TwinklerInABottleTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpLantern(ModContent.ItemType<TwinklerInABottle>(), lavaImmune: false, autoMapEntry: false);
		AddMapEntry(new Color(255, 99, 71), CalamityUtils.GetItemName<TwinklerInABottle>());
		base.AnimationFrameHeight = 36;
		base.AdjTiles = new int[3] { 42, 270, 271 };
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return CalamityUtils.DrawSwayingMultiTile(i, j);
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		int frameAmt = 15;
		int timeNeeded = 6;
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
		frameYOffset = this.GetAnimationOffset(i, j, 15, 16, 18, 1, 2, base.AnimationFrameHeight);
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

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1f;
		g = 0.39f;
		b = 0.28f;
	}
}
