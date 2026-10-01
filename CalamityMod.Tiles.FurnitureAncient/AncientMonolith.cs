using CalamityMod.Items.Placeables.FurnitureAncient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAncient;

public class AncientMonolith : ModTile
{
	private int animationFrameWidth = 36;

	public override void SetStaticDefaults()
	{
		this.SetUpClock(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureAncient.AncientMonolith>(), lavaImmune: true);
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 60, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.9f;
		g = 0.3f;
		b = 0.3f;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int uniqueAnimationFrame = Main.tileFrame[base.Type] + i;
		uniqueAnimationFrame = uniqueAnimationFrame % 35 + 1;
		frameXOffset = uniqueAnimationFrame * animationFrameWidth;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter > 6)
		{
			frameCounter = 0;
			frame++;
			if (frame > 35)
			{
				frame = 1;
			}
		}
	}

	public override bool RightClick(int x, int y)
	{
		return FurnitureCommon.ClockRightClick();
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			Main.SceneMetrics.HasClock = true;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.MouseOver(i, j, ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureAncient.AncientMonolith>());
	}
}
