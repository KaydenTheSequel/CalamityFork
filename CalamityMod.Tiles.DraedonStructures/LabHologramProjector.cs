using CalamityMod.CalPlayer;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures;

public class LabHologramProjector : ModTile
{
	public const int Width = 6;

	public const int Height = 7;

	public const int SheetSquare = 16;

	public const int IdleFrames = 8;

	public const int TalkingFrames = 8;

	public const int FrameCount = 16;

	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
		TileObjectData.newTile.Height = 7;
		TileObjectData.newTile.Origin = new Point16(3, 6);
		TileObjectData.newTile.CoordinateHeights = new int[TileObjectData.newTile.Height];
		TileObjectData.newTile.CoordinatePadding = 0;
		for (int i = 0; i < TileObjectData.newTile.CoordinateHeights.Length; i++)
		{
			TileObjectData.newTile.CoordinateHeights[i] = 16;
		}
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 6, 0);
		TileObjectData.newTile.LavaDeath = false;
		ModTileEntity te = ModContent.GetInstance<TELabHologramProjector>();
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(te.Hook_AfterPlacement, -1, 0, processedCoordinates: true);
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.newTile.StyleMultiplier = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(99, 131, 199), CalamityUtils.GetItemName<LabHologramProjectorItem>());
		base.AnimationFrameHeight = 112;
		base.DustType = 229;
		base.HitSound = SoundID.Tink;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Tile tile = Main.tile[i, j];
		int left = i - tile.TileFrameX % 96 / 16;
		int top = j - tile.TileFrameY % 112 / 16;
		CalamityUtils.FindTileEntity<TELabHologramProjector>(i, j, 6, 7)?.Kill(left, top);
	}

	public override bool RightClick(int i, int j)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		CalamityPlayer mp = player.Calamity();
		if (mp.CurrentlyViewedHologramID == -1)
		{
			mp.CurrentlyViewedHologramID = CalamityUtils.FindTileEntity<TELabHologramProjector>(i, j, 6, 7)?.ID ?? (-1);
			if (mp.CurrentlyViewedHologramID != -1)
			{
				SoundEngine.PlaySound(in SoundID.Chat);
				player.SetTalkNPC(-1);
			}
		}
		else
		{
			mp.CurrentlyViewedHologramID = -1;
		}
		return true;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		Tile trackTile = Main.tile[i, j];
		if (trackTile.IsTileActuallyInvisible())
		{
			return false;
		}
		int num = (int)(Main.GlobalTimeWrappedHourly * 60f);
		TELabHologramProjector hologramTileEntity = CalamityUtils.FindTileEntity<TELabHologramProjector>(i, j, 6, 7);
		bool popup = false;
		if (hologramTileEntity != null && hologramTileEntity.PoppingUp)
		{
			popup = true;
		}
		int frame = num / 5 % 16;
		if (popup)
		{
			if (frame <= 8)
			{
				frame += 8;
			}
			if (frame >= 16)
			{
				frame = 8 + frame % 8;
			}
		}
		else
		{
			frame %= 8;
			if (frame >= 6)
			{
				frame -= 6;
			}
		}
		int xPos = trackTile.TileFrameX;
		int yPos = trackTile.TileFrameY;
		yPos += frame % 16 * 112;
		if (GlowTexture == null)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonStructures/LabHologramProjector", (AssetRequestMode)2);
		}
		Texture2D tileTexture = GlowTexture.Value;
		Vector2 offset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + offset;
		Color drawColor = Lighting.GetColor(i, j);
		if (!trackTile.IsHalfBlock && trackTile.Slope == SlopeType.Solid)
		{
			spriteBatch.Draw(tileTexture, drawOffset, (Rectangle?)new Rectangle(xPos, yPos, 16, 16), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		else if (trackTile.IsHalfBlock)
		{
			spriteBatch.Draw(tileTexture, drawOffset + Vector2.UnitY * 8f, (Rectangle?)new Rectangle(xPos, yPos, 16, 16), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
