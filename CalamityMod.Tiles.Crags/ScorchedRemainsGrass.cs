using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Systems;
using CalamityMod.Tiles.Crags.Lily;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Crags;

public class ScorchedRemainsGrass : ModTile
{
	public Asset<Texture2D> GrassTexture;

	private const short subsheetWidth = 234;

	private const short subsheetHeight = 90;

	private int extraFrameHeight = 36;

	private int extraFrameWidth = 90;

	public override void SetStaticDefaults()
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		GrassTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/CinderBlossomGrassGrass", (AssetRequestMode)2);
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithHell(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<ScorchedRemains>());
		base.HitSound = SoundID.Dig;
		base.MinPick = 100;
		RegisterItemDrop(ModContent.ItemType<global::CalamityMod.Items.Placeables.Crags.ScorchedRemains>());
		AddMapEntry(new Color(212, 82, 227));
		this.RegisterBlendMergeWith(ModContent.TileType<BrimstoneSlag>());
		this.RegisterBlendMergeWith(57);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1.82f;
		g = 0.56f;
		b = 1.07f;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (fail && !effectOnly)
		{
			Main.tile[i, j].TileType = (ushort)ModContent.TileType<ScorchedRemains>();
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (up.LiquidAmount > 0)
		{
			Main.tile[i, j].TileType = (ushort)ModContent.TileType<ScorchedRemains>();
		}
		if (WorldGen.genRand.NextBool(5) && !up.HasTile && !up2.HasTile && up.LiquidAmount == 0)
		{
			up.TileType = (ushort)ModContent.TileType<CinderBlossomTallPlants>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(20) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
		if (WorldGen.genRand.NextBool(100))
		{
			ushort[] Lillies = new ushort[6]
			{
				(ushort)ModContent.TileType<LavaLily1>(),
				(ushort)ModContent.TileType<LavaLily2>(),
				(ushort)ModContent.TileType<LavaLily3>(),
				(ushort)ModContent.TileType<LavaLily4>(),
				(ushort)ModContent.TileType<LavaLily5>(),
				(ushort)ModContent.TileType<LavaLily6>()
			};
			WorldGen.PlaceObject(i, j - 1, WorldGen.genRand.Next(Lillies), mute: true);
		}
		if (up.LiquidAmount > 0)
		{
			Main.tile[i, j].TileType = (ushort)ModContent.TileType<ScorchedRemains>();
		}
		if (WorldGen.genRand.NextBool(60) && !up.HasTile && !up2.HasTile && up.LiquidAmount == 0)
		{
			up.TileType = (ushort)ModContent.TileType<LavaPistil>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(8) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		bool isPlayerNear = WorldGen.PlayerLOS(i, j);
		Tile Above = Framing.GetTileSafely(i, j - 1);
		if (((!Main.gamePaused && ((Game)Main.instance).IsActive && !Above.HasTile) & isPlayerNear) && Main.rand.NextBool(300))
		{
			int newDust = Dust.NewDust(new Vector2((float)((i - 2) * 16), (float)((j - 1) * 16)), 5, 5, ModContent.DustType<CinderBlossomDust>());
			Main.dust[newDust].velocity.Y += 0.09f;
		}
		if (Main.tile[i, j].Get<TileSpecialDrawData>().HasSpecialPoint)
		{
			Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
		}
	}

	public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
			Color drawColour = CalamityUtils.ApplyPaint(tile.TileColor, Lighting.GetColor(i, j));
			Texture2D leaves = GrassTexture.Value;
			DrawExtraTop(i, j, leaves, drawOffset, drawColour);
			DrawExtraWallEnds(i, j, leaves, drawOffset, drawColour);
			DrawExtraDrapes(i, j, leaves, drawOffset, drawColour);
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
