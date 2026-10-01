using CalamityMod.Items.Placeables.DraedonStructures.CagedLights;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures.CagedLights;

public class MiniAgedLablight : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileObsidianKill[base.Type] = false;
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.DustType = 299;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(48, 201, 214), CalamityUtils.GetItemName<MiniAgedLablightItem>());
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 10;
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(2);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(4);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(6);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorWall = true;
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(8);
		TileObjectData.addTile(base.Type);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.060784314f;
		g = 0.49215686f;
		b = 0.5f;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			int xFrameOffset = Main.tile[i, j].TileFrameX;
			int yFrameOffset = Main.tile[i, j].TileFrameY;
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonStructures/CagedLights/MiniAgedLablight_Glow", (AssetRequestMode)2);
			}
			Texture2D glowmask = GlowTexture.Value;
			Vector2 drawOffest = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + drawOffest;
			Color drawColour = Color.White * 0.6f;
			Tile trackTile = Main.tile[i, j];
			if (!trackTile.IsHalfBlock && trackTile.Slope == SlopeType.Solid)
			{
				spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)new Rectangle(xFrameOffset, yFrameOffset, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
			else if (trackTile.IsHalfBlock)
			{
				spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}
}
