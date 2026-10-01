using CalamityMod.Items.Placeables.FurnitureAshen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAshen;

public class AshenChandelier : ModTile
{
	private int animationFrame;

	public Asset<Texture2D> FlameTexture;

	public override void Load()
	{
		FlameTexture = ModContent.Request<Texture2D>(Texture + "Flame", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		this.SetUpChandelier(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureAshen.AshenChandelier>(), lavaImmune: true);
		base.AnimationFrameHeight = 54;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return CalamityUtils.DrawSwayingMultiTile(i, j);
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

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 6)
		{
			frame = (frame + 1) % 6;
			animationFrame = frame;
			frameCounter = 0;
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 1f;
			g = 0.5f;
			b = 0.5f;
		}
		else
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
	}

	public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		ulong flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);
		tileFlameData.flameSeed = flameSeed;
		tileFlameData.flameTexture = FlameTexture.Value;
		tileFlameData.flameColor = new Color(128, 64, 64, 0);
		tileFlameData.flameCount = 3;
		tileFlameData.flameRangeXMin = -10;
		tileFlameData.flameRangeXMax = 11;
		tileFlameData.flameRangeYMin = -10;
		tileFlameData.flameRangeYMax = 11;
		tileFlameData.flameRangeMultX = 0.05f;
		tileFlameData.flameRangeMultY = 0f;
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 3, 3);
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameY == 18 && tile.TileFrameX < 54)
		{
			CalamityUtils.DrawFlameSparks(60, 5, i, j);
		}
	}
}
