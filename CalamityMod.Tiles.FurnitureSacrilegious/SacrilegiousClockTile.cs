using System;
using CalamityMod.Items.Placeables.FurnitureSacrilegious;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureSacrilegious;

public class SacrilegiousClockTile : ModTile
{
	public Asset<Texture2D> IconTexture;

	public override void SetStaticDefaults()
	{
		Main.tileLighted[base.Type] = true;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		this.SetUpClock(ModContent.ItemType<SacrilegiousClock>(), lavaImmune: true);
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
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 8, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1.2f;
		g = 0.2f;
		b = 0.2f;
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
		FurnitureCommon.MouseOver(i, j, ModContent.ItemType<SacrilegiousClock>());
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		if (IconTexture == null)
		{
			IconTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureSacrilegious/SacrilegiousClockTile_Icon", (AssetRequestMode)2);
		}
		Texture2D texture = IconTexture.Value;
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			int xPos = tile.TileFrameX;
			int yPos = tile.TileFrameY;
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			float yOffset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) / 5f) * 2f;
			Vector2 correction = default(Vector2);
			((Vector2)(ref correction))._002Ector(16f, -10f);
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y + yOffset) + zero + correction;
			Rectangle rect = default(Rectangle);
			((Rectangle)(ref rect))._002Ector(xPos, yPos, texture.Width, texture.Height);
			Color color = default(Color);
			((Color)(ref color))._002Ector(100, 100, 100, 0);
			Vector2 origin = rect.Size() / 2f;
			for (int c = 0; c < 5; c++)
			{
				spriteBatch.Draw(texture, drawOffset, (Rectangle?)rect, color, 0f, origin, 1f, (SpriteEffects)0, 0f);
			}
		}
	}
}
