using CalamityMod.Dusts;
using CalamityMod.Systems;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureMonolith;

public class AstralMonolith : ModTile
{
	private static int sheetWidth = 216;

	private static int sheetHeight = 72;

	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Wood"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		CalamityUtils.SetMerge(base.Type, 192);
		CalamityUtils.SetMerge(base.Type, 384);
		CalamityUtils.SetMerge(base.Type, 191);
		CalamityUtils.SetMerge(base.Type, 383);
		AddMapEntry(new Color(45, 36, 63));
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralBasic>(), 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int xPos = i % 4;
		int yPos = j % 4;
		Tile tile;
		if ((xPos == 0 && yPos == 2) || (xPos == 1 && yPos == 3) || (xPos == 3 && yPos == 1))
		{
			tile = Main.tile[i, j];
			ref TileWallWireStateData reference = ref tile.Get<TileWallWireStateData>();
			tile = Main.tile[i, j - 1];
			reference.TileFrameNumber = tile.TileFrameNumber;
		}
		else if (xPos == 2 && yPos == 2)
		{
			tile = Main.tile[i, j];
			ref TileWallWireStateData reference2 = ref tile.Get<TileWallWireStateData>();
			tile = Main.tile[i - 1, j];
			reference2.TileFrameNumber = tile.TileFrameNumber;
		}
		else if (xPos == 2 && yPos == 3)
		{
			tile = Main.tile[i, j];
			ref TileWallWireStateData reference3 = ref tile.Get<TileWallWireStateData>();
			tile = Main.tile[i - 1, j - 1];
			reference3.TileFrameNumber = tile.TileFrameNumber;
		}
		GetDrawSpecifics(i, j, ref xPos, ref yPos);
		frameXOffset = xPos * sheetWidth;
		frameYOffset = yPos * sheetHeight;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			int xOffset = i % 4;
			int yOffset = j % 4;
			GetDrawSpecifics(i, j, ref xOffset, ref yOffset);
			xOffset *= sheetWidth;
			yOffset *= sheetHeight;
			int xPos = tile.TileFrameX;
			int yPos = tile.TileFrameY;
			xPos += xOffset;
			yPos += yOffset;
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureMonolith/AstralMonolithGlow", (AssetRequestMode)2);
			}
			Texture2D glowmask = GlowTexture.Value;
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
			Color drawColour = CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, new Color(50, 50, 50, 50));
			if (!tile.IsHalfBlock && tile.Slope == SlopeType.Solid)
			{
				Main.spriteBatch.Draw(glowmask, drawOffset, (Rectangle?)new Rectangle(xPos, yPos, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
			else if (tile.IsHalfBlock)
			{
				Main.spriteBatch.Draw(glowmask, drawOffset + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xPos, yPos, 18, 8), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		TileFramingSystem.CompactFraming(i, j, resetFrame);
		return false;
	}

	private void GetDrawSpecifics(int i, int j, ref int xPos, ref int yPos)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameNumber == 1)
		{
			if (xPos == 0 && (yPos == 1 || yPos == 2))
			{
				yPos += 3;
			}
			else if ((xPos == 1 || xPos == 2) && (yPos == 2 || yPos == 3))
			{
				yPos += 2;
			}
			else if (xPos == 3 && (yPos == 0 || yPos == 1))
			{
				yPos += 4;
			}
		}
		else if (tile.TileFrameNumber == 2)
		{
			if (xPos == 1 && yPos == 0)
			{
				xPos = 0;
				yPos = 6;
			}
			else if (xPos == 2 && yPos == 1)
			{
				xPos = 1;
				yPos = 6;
			}
			else if (xPos == 1 && yPos == 3)
			{
				xPos = 2;
				yPos = 6;
			}
			else if (xPos == 3 && yPos == 3)
			{
				xPos = 3;
				yPos = 6;
			}
		}
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
