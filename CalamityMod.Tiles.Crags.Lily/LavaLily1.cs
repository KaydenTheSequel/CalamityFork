using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags.Lily;

public class LavaLily1 : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public Asset<Texture2D> TopTexture;

	public Asset<Texture2D> TopGlowTexture;

	public static Vector2 TileOffset
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (Lighting.LegacyEngine.Mode <= 1 || Main.GameZoomTarget != 1f)
			{
				return Vector2.One * 12f;
			}
			return Vector2.Zero;
		}
	}

	public override void SetStaticDefaults()
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileAxe[base.Type] = true;
		TileObjectData.newTile.Width = 5;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(3, 2);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = 3;
		TileObjectData.addTile(base.Type);
		base.MineResist = 3f;
		AddMapEntry(new Color(153, 100, 176));
		base.DustType = 97;
		base.HitSound = SoundID.Grass;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = (WorldGen.genRand.NextBool(2) ? 97 : 292);
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 10);
	}

	internal static void DrawLilyTop(int i, int j, Texture2D tex, Rectangle? source, Vector2? offset = null, Vector2? origin = null, bool Glow = false)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPos = Utils.ToWorldCoordinates(new Vector2((float)i, (float)j), 8f, 8f) - Main.screenPosition + (Vector2)(((_003F?)offset) ?? new Vector2(0f, -2f));
		Color color = Lighting.GetColor(i, j);
		Main.spriteBatch.Draw(tex, drawPos, source, Glow ? Color.White : color, 0f, (Vector2)(((_003F?)origin) ?? (source.Value.Size() / 3f)), 1f, (SpriteEffects)0, 0f);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Framing.GetTileSafely(i, j);
		if (tile.IsTileActuallyInvisible())
		{
			return;
		}
		if (GlowTexture == null)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Lily/LavaLily1Glow", (AssetRequestMode)2);
		}
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
		spriteBatch.Draw(GlowTexture.Value, new Vector2((float)(i * 16), (float)(j * 16 + 2)) - Main.screenPosition + zero, (Rectangle?)new Rectangle((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16), Color.White);
		if (Framing.GetTileSafely(i, j).TileFrameX == 36 && Framing.GetTileSafely(i, j).TileFrameY == 18)
		{
			if (TopTexture == null)
			{
				TopTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Lily/LavaLily1Top", (AssetRequestMode)2);
			}
			if (TopGlowTexture == null)
			{
				TopGlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Lily/LavaLily1TopGlow", (AssetRequestMode)2);
			}
			DrawLilyTop(i, j, TopTexture.Value, (Rectangle?)new Rectangle(0, 0, 178, 184), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)new Vector2(98f, 191f), false);
			DrawLilyTop(i, j, TopGlowTexture.Value, (Rectangle?)new Rectangle(0, 0, 178, 184), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)new Vector2(98f, 191f), true);
		}
	}
}
