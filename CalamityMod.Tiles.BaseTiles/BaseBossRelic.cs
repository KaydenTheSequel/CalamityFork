using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.BaseTiles;

public abstract class BaseBossRelic : ModTile
{
	public const int FrameWidth = 54;

	public const int FrameHeight = 72;

	public const int HorizontalFrames = 1;

	public const int VerticalFrames = 1;

	public Asset<Texture2D> RelicTexture;

	public abstract string RelicTextureName { get; }

	public abstract int AssociatedItem { get; }

	public override string Texture => "CalamityMod/Tiles/BaseTiles/RelicPedestal";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			RelicTexture = ModContent.Request<Texture2D>(RelicTextureName, (AssetRequestMode)2);
		}
	}

	public override void Unload()
	{
		RelicTexture = null;
	}

	public override void SetStaticDefaults()
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		RegisterItemDrop(AssociatedItem);
		Main.tileShine[base.Type] = 400;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.InteractibleByNPCs[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.newTile.StyleHorizontal = false;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(233, 207, 94), Language.GetText("MapObject.Relic"));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		return false;
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (drawData.tileFrameX % 54 == 0 && drawData.tileFrameY % 72 == 0)
		{
			Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.CustomNonSolid);
		}
	}

	public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		Point p = default(Point);
		((Point)(ref p))._002Ector(i, j);
		Tile tile = Main.tile[p.X, p.Y];
		if (tile.HasTile)
		{
			Texture2D texture = RelicTexture.Value;
			int frameY = tile.TileFrameX / 54;
			Rectangle frame = texture.Frame(1, 1, 0, frameY);
			Vector2 origin = frame.Size() / 2f;
			Vector2 val = p.ToWorldCoordinates(24f, 64f);
			Color color = Lighting.GetColor(p.X, p.Y);
			SpriteEffects effects = (SpriteEffects)((tile.TileFrameY / 72 != 0) ? 1 : 0);
			float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) / 5f);
			Vector2 drawPos = val - Main.screenPosition + new Vector2(0f, -40f) + new Vector2(0f, offset * 4f);
			spriteBatch.Draw(texture, drawPos, (Rectangle?)frame, color, 0f, origin, 1f, effects, 0f);
			float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) / 2f) * 0.3f + 0.7f;
			Color effectColor = color;
			((Color)(ref effectColor)).A = 0;
			effectColor = effectColor * 0.1f * scale;
			float offset2 = 6f + offset * 2f;
			float oneSixth = 1f / 6f;
			for (float k = 0f; k < 1f; k += oneSixth)
			{
				spriteBatch.Draw(texture, drawPos + ((float)Math.PI * 2f * k).ToRotationVector2() * offset2, (Rectangle?)frame, effectColor, 0f, origin, 1f, effects, 0f);
			}
		}
	}
}
