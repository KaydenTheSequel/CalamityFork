using CalamityMod.Dusts.Furniture;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultStone" })]
public class OtherworldlyStone : ModTile
{
	public Asset<Texture2D> ClothTexture;

	private int extraFrameHeight = 36;

	private int extraFrameWidth = 90;

	public override void SetStaticDefaults()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		ClothTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureOtherworldly/OtherworldlyStone_Cloth", (AssetRequestMode)2);
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = false;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		base.HitSound = SoundID.Tink;
		base.MineResist = 3f;
		AddMapEntry(new Color(60, 42, 61));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(125, 94, 128));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<OtherworldlyTileCloth>(), 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void PostTileFrame(int i, int j, int up, int down, int left, int right, int upLeft, int upRight, int downLeft, int downRight)
	{
		if (Main.tile[i - 1, j - 1].TileType != base.Type || Main.tile[i, j - 1].TileType != base.Type || Main.tile[i + 1, j - 1].TileType != base.Type || Main.tile[i - 1, j - 2].TileType != base.Type || Main.tile[i, j - 2].TileType != base.Type || Main.tile[i + 1, j - 2].TileType != base.Type)
		{
			Main.tile[i, j].Get<TileSpecialDrawData>().HasSpecialPoint = true;
		}
		else
		{
			Main.tile[i, j].Get<TileSpecialDrawData>().HasSpecialPoint = false;
		}
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.tile[i, j].Get<TileSpecialDrawData>().HasSpecialPoint)
		{
			Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
		}
	}

	public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
			Color drawColour = CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, Lighting.GetColor(i, j), deepPaintOnly: false);
			Texture2D cloth = ClothTexture.Value;
			DrawExtraTop(i, j, cloth, drawOffset, drawColour);
			DrawExtraWallEnds(i, j, cloth, drawOffset, drawColour);
			DrawExtraDrapes(i, j, cloth, drawOffset, drawColour);
		}
	}

	private void DrawExtraTop(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: false, 0, 1, i, j) || (CheckTile(base.Type, equal: true, 0, 1, i, j) && CheckTile(base.Type, equal: false, 1, 1, i, j) && CheckTile(base.Type, equal: false, -1, 1, i, j) && CheckTile(base.Type, equal: true, 1, 0, i, j) && CheckTile(base.Type, equal: true, -1, 0, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("middle") + GetExtraVariant(i, j), GetExtraPattern(i), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(GetExtraState("middle") + GetExtraVariant(i, j), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			DrawExtraOverhang(i, j, extras, drawOffset, drawColour);
		}
	}

	private void DrawExtraWallEnds(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: true, 1, 0, i, j) && CheckTile(base.Type, equal: false, 1, 1, i, j) && CheckTile(base.Type, equal: true, 0, 1, i, j) && (CheckTile(base.Type, equal: true, -1, 1, i, j) || CheckTile(base.Type, equal: false, -1, 0, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndLeft") + GetExtraVariant(i + 1, j), GetExtraPattern(i), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(GetExtraState("wallEndLeft") + GetExtraVariant(i + 1, j), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, -1, 0, i, j) && CheckTile(base.Type, equal: false, -1, 1, i, j) && CheckTile(base.Type, equal: true, 0, 1, i, j) && (CheckTile(base.Type, equal: true, 1, 1, i, j) || CheckTile(base.Type, equal: false, 1, 0, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndRight") + GetExtraVariant(i - 1, j), GetExtraPattern(i), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(GetExtraState("wallEndRight") + GetExtraVariant(i - 1, j), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	private void DrawExtraOverhang(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: false, -1, 0, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(-16f, 0f), (Rectangle?)new Rectangle(GetExtraState("overhangLeft") + GetExtraVariant(i, j), GetExtraPattern(i - 1), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(-16f, 16f), (Rectangle?)new Rectangle(GetExtraState("overhangLeft") + GetExtraVariant(i, j), GetExtraPattern(i - 1) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: false, 1, 0, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(16f, 0f), (Rectangle?)new Rectangle(GetExtraState("overhangRight") + GetExtraVariant(i, j), GetExtraPattern(i + 1), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(16f, 16f), (Rectangle?)new Rectangle(GetExtraState("overhangRight") + GetExtraVariant(i, j), GetExtraPattern(i + 1) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	private void DrawExtraDrapes(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		if ((CheckTile(base.Type, equal: true, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j)) || (CheckTile(base.Type, equal: true, 0, 2, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j) && CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: true, -1, 1, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("middle") + GetExtraVariant(i, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j) && CheckTile(base.Type, equal: true, 0, 2, i, j) && (CheckTile(base.Type, equal: true, -1, 2, i, j) || CheckTile(base.Type, equal: false, -1, 1, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndLeft") + GetExtraVariant(i + 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, -1, 1, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j) && CheckTile(base.Type, equal: true, 0, 2, i, j) && (CheckTile(base.Type, equal: true, 1, 2, i, j) || CheckTile(base.Type, equal: false, 1, 1, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndRight") + GetExtraVariant(i - 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: false, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("overhangLeft") + GetExtraVariant(i + 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, -1, 1, i, j) && CheckTile(base.Type, equal: false, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("overhangRight") + GetExtraVariant(i - 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	private bool CheckTile(int type, bool equal, int x, int y, int i, int j)
	{
		return Main.tile[i + x, j - y].TileType == type == equal;
	}

	private int GetExtraState(string type)
	{
		switch (type)
		{
		case "middle":
			return 36;
		case "overhangLeft":
			return 18;
		case "overhangRight":
			return 54;
		case "wallEndLeft":
			return 0;
		case "wallEndRight":
			return 72;
		default:
			Main.NewText(type.ToString() + " is not a valid Extra sheet state");
			return 0;
		}
	}

	private int GetExtraPattern(int i)
	{
		return i % 3 * extraFrameHeight;
	}

	private int GetExtraVariant(int i, int j)
	{
		return Main.tile[i, j].TileFrameNumber * extraFrameWidth;
	}
}
