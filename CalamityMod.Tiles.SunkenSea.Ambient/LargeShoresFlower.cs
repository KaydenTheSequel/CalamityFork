using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class LargeShoresFlower : ModTile
{
	public Asset<Texture2D> CenterTexture;

	public override void SetStaticDefaults()
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		TileID.Sets.FramesOnKillWall[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.Width = 3;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 3;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(20, 100, 220));
		base.DustType = 15;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		float pulse = 0.95f + 0.15f * (0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 4.2f));
		r = 0.1f * pulse;
		g = 0.3f * pulse;
		b = 0.8f * pulse;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (tile.IsTileActuallyInvisible())
		{
			return false;
		}
		float pulse = 0.95f + 0.15f * MathF.Sin(Main.GlobalTimeWrappedHourly * 4.2f);
		int xFrameOffset = tile.TileFrameX;
		int yFrameOffset = tile.TileFrameY;
		Texture2D glowmask = TextureAssets.Tile[base.Type].Value;
		Vector2 drawOffest = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + drawOffest;
		Color drawColour = Color.White * pulse;
		if (!tile.IsHalfBlock && tile.Slope == SlopeType.Solid)
		{
			spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)new Rectangle(xFrameOffset, yFrameOffset, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		else if (tile.IsHalfBlock)
		{
			spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CenterTexture == null)
		{
			CenterTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/Ambient/LargeShoresFlower_Center", (AssetRequestMode)2);
		}
		CalamityUtils.DrawFlameEffect(CenterTexture.Value, i, j, 2);
		return false;
	}
}
