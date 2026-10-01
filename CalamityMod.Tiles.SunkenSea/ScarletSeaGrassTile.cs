using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class ScarletSeaGrassTile : ModTile
{
	public enum ExtraState
	{
		Middle = 36,
		OverhangLeft = 18,
		OverhangRight = 54,
		WallEndLeft = 0,
		WallEndRight = 72
	}

	public Asset<Texture2D> GrassTexture;

	private int extraFrameHeight = 36;

	private int extraFrameWidth = 90;

	public override void SetStaticDefaults()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		GrassTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/ScarletSeaGrass", (AssetRequestMode)2);
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		Main.tileSolid[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		Main.tileBlockLight[base.Type] = false;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		base.DustType = 147;
		AddMapEntry(new Color(216, 50, 50));
		Main.tileSand[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		TileID.Sets.Suffocate[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.Conversion.Sand[base.Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[base.Type] = true;
		TileID.Sets.Falling[base.Type] = true;
		TileID.Sets.FallingBlockProjectile[base.Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<PolypSandBallFalling>(), 15);
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
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
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
		Color drawColour = CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, Lighting.GetColor(i, j));
		Texture2D leaves = GrassTexture.Value;
		DrawExtraTop(i, j, leaves, drawOffset, drawColour);
		DrawExtraWallEnds(i, j, leaves, drawOffset, drawColour);
		DrawExtraDrapes(i, j, leaves, drawOffset, drawColour);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.3f;
		g = 0f;
		b = 0.1f;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (WorldGen.genRand.NextBool(1) && !up.HasTile && !up2.HasTile && up.LiquidAmount > 0 && up2.LiquidAmount > 0 && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			up.TileType = (ushort)ModContent.TileType<LongScarletSeagrass>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(16) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
	}

	private void DrawExtraTop(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: false, 0, 1, i, j) || (CheckTile(base.Type, equal: true, 0, 1, i, j) && CheckTile(base.Type, equal: false, 1, 1, i, j) && CheckTile(base.Type, equal: false, -1, 1, i, j) && CheckTile(base.Type, equal: true, 1, 0, i, j) && CheckTile(base.Type, equal: true, -1, 0, i, j)))
		{
			int x = GetExtraState(ExtraState.Middle) + GetExtraVariant(i, j);
			int y = GetExtraPattern(i);
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x, y, 18, 18), drawColour);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(x, y + 18, 18, 18), drawColour);
			DrawExtraOverhang(i, j, extras, drawOffset, drawColour);
		}
	}

	private bool CheckTile(int type, bool equal, int x, int y, int i, int j)
	{
		return Main.tile[i + x, j - y].TileType == type == equal;
	}

	private void DrawExtraWallEnds(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: true, 1, 0, i, j) && CheckTile(base.Type, equal: false, 1, 1, i, j) && CheckTile(base.Type, equal: true, 0, 1, i, j) && (CheckTile(base.Type, equal: true, -1, 1, i, j) || CheckTile(base.Type, equal: false, -1, 0, i, j)))
		{
			int x = GetExtraState(ExtraState.WallEndLeft) + GetExtraVariant(i + 1, j);
			int y = GetExtraPattern(i);
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x, y, 18, 18), drawColour);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(x, y + 18, 18, 18), drawColour);
		}
		if (CheckTile(base.Type, equal: true, -1, 0, i, j) && CheckTile(base.Type, equal: false, -1, 1, i, j) && CheckTile(base.Type, equal: true, 0, 1, i, j) && (CheckTile(base.Type, equal: true, 1, 1, i, j) || CheckTile(base.Type, equal: false, 1, 0, i, j)))
		{
			int x2 = GetExtraState(ExtraState.WallEndRight) + GetExtraVariant(i - 1, j);
			int y2 = GetExtraPattern(i);
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x2, y2, 18, 18), drawColour);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(x2, y2 + 18, 18, 18), drawColour);
		}
	}

	private void DrawExtraOverhang(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: false, -1, 0, i, j))
		{
			int x = GetExtraState(ExtraState.OverhangLeft) + GetExtraVariant(i, j);
			int y = GetExtraPattern(i - 1);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(-16f, 0f), (Rectangle?)new Rectangle(x, y, 18, 18), drawColour);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(-16f, 16f), (Rectangle?)new Rectangle(x, y + 18, 18, 18), drawColour);
		}
		if (CheckTile(base.Type, equal: false, 1, 0, i, j))
		{
			int x2 = GetExtraState(ExtraState.OverhangRight) + GetExtraVariant(i, j);
			int y2 = GetExtraPattern(i + 1);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(16f, 0f), (Rectangle?)new Rectangle(x2, y2, 18, 18), drawColour);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(16f, 16f), (Rectangle?)new Rectangle(x2, y2 + 18, 18, 18), drawColour);
		}
	}

	private void DrawExtraDrapes(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		if ((CheckTile(base.Type, equal: true, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j)) || (CheckTile(base.Type, equal: true, 0, 2, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j) && CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: true, -1, 1, i, j)))
		{
			int x = GetExtraState(ExtraState.Middle) + GetExtraVariant(i, j - 1);
			int y = GetExtraPattern(i) + 18;
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x, y, 18, 18), drawColour);
		}
		if (CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j) && CheckTile(base.Type, equal: true, 0, 2, i, j) && (CheckTile(base.Type, equal: true, -1, 2, i, j) || CheckTile(base.Type, equal: false, -1, 1, i, j)))
		{
			int x2 = GetExtraState(ExtraState.WallEndLeft) + GetExtraVariant(i + 1, j - 1);
			int y2 = GetExtraPattern(i) + 18;
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x2, y2, 18, 18), drawColour);
		}
		if (CheckTile(base.Type, equal: true, -1, 1, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j) && CheckTile(base.Type, equal: true, 0, 2, i, j) && (CheckTile(base.Type, equal: true, 1, 2, i, j) || CheckTile(base.Type, equal: false, 1, 1, i, j)))
		{
			int x3 = GetExtraState(ExtraState.WallEndRight) + GetExtraVariant(i - 1, j - 1);
			int y3 = GetExtraPattern(i) + 18;
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x3, y3, 18, 18), drawColour);
		}
		if (CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: false, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j))
		{
			int x4 = GetExtraState(ExtraState.OverhangLeft) + GetExtraVariant(i + 1, j - 1);
			int y4 = GetExtraPattern(i) + 18;
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x4, y4, 18, 18), drawColour);
		}
		if (CheckTile(base.Type, equal: true, -1, 1, i, j) && CheckTile(base.Type, equal: false, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j))
		{
			int x5 = GetExtraState(ExtraState.OverhangRight) + GetExtraVariant(i - 1, j - 1);
			int y5 = GetExtraPattern(i) + 18;
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(x5, y5, 18, 18), drawColour);
		}
	}

	private int GetExtraState(ExtraState type)
	{
		switch (type)
		{
		case ExtraState.WallEndLeft:
		case ExtraState.OverhangLeft:
		case ExtraState.Middle:
		case ExtraState.OverhangRight:
		case ExtraState.WallEndRight:
			return (int)type;
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
