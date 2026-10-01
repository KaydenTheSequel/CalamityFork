using System;
using CalamityMod.Items.Placeables.FurnitureAshen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAshen;

public class AshenMonolith : ModTile
{
	public Asset<Texture2D> EyeTexture;

	public int animationFrameWidth = 36;

	public override void SetStaticDefaults()
	{
		this.SetUpClock(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureAshen.AshenMonolith>(), lavaImmune: true);
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
		r = 1f;
		g = 0.5f;
		b = 0.5f;
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
		FurnitureCommon.MouseOver(i, j, ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureAshen.AshenMonolith>());
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		Tile currentTile = Main.tile[i, j];
		if (currentTile.IsTileActuallyInvisible())
		{
			return;
		}
		Vector2 eyeCentre = default(Vector2);
		((Vector2)(ref eyeCentre))._002Ector((float)(i * 16), (float)(j * 16));
		if (currentTile.TileFrameX == 0)
		{
			eyeCentre += new Vector2(16f, 0f);
		}
		if (currentTile.TileFrameY == 0)
		{
			eyeCentre += new Vector2(0f, 16f);
		}
		Vector2 playerPos = eyeCentre;
		float distanceToTarget = 9999f;
		for (int x = 0; x < Main.player.Length; x++)
		{
			Vector2 val = Main.player[x].position - eyeCentre;
			if (((Vector2)(ref val)).Length() < distanceToTarget)
			{
				playerPos = Main.player[x].position;
				val = Main.player[x].position - eyeCentre;
				distanceToTarget = ((Vector2)(ref val)).Length();
			}
		}
		int frameX = 5;
		int frameY = 2;
		int xRange = 0;
		if (distanceToTarget > 250f)
		{
			xRange = 5;
		}
		else if (distanceToTarget > 170f)
		{
			xRange = 4;
		}
		else if (distanceToTarget > 100f)
		{
			xRange = 3;
		}
		else if (distanceToTarget > 40f)
		{
			xRange = 2;
		}
		else if (distanceToTarget > 10f)
		{
			xRange = 1;
		}
		int yRange = 0;
		if (distanceToTarget > 170f)
		{
			yRange = 2;
		}
		else if (distanceToTarget > 10f)
		{
			yRange = 1;
		}
		Vector2 eyeToPlayer = (playerPos - eyeCentre) / 16f;
		eyeToPlayer.X = (int)eyeToPlayer.X;
		eyeToPlayer.Y = (int)eyeToPlayer.Y;
		float factor = Math.Abs((Math.Abs(eyeToPlayer.X) >= 2f * Math.Abs(eyeToPlayer.Y)) ? (eyeToPlayer.X / (float)xRange) : (eyeToPlayer.Y / (float)yRange));
		if (factor != 0f)
		{
			eyeToPlayer /= factor;
		}
		frameX += (int)eyeToPlayer.X;
		frameY += (int)eyeToPlayer.Y;
		frameX *= animationFrameWidth;
		frameY *= 90;
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
		if (EyeTexture == null)
		{
			EyeTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureAshen/AshenMonolith_Eye", (AssetRequestMode)2);
		}
		Texture2D eyeSheet = EyeTexture.Value;
		spriteBatch.Draw(eyeSheet, drawOffset, (Rectangle?)new Rectangle(frameX + currentTile.TileFrameX, frameY + currentTile.TileFrameY, 16, 16), new Color(255, 255, 255, 255), 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
	}
}
