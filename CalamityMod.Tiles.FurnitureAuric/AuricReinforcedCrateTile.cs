using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureAuric;

public class AuricReinforcedCrateTile : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileSolidTop[base.Type] = true;
		Main.tileTable[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(base.Type);
		AddMapEntry(Color.Goldenrod, CalamityUtils.GetText("Tiles.ReinforcedCrate"));
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 182);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
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
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			int xFrameOffset = Main.tile[i, j].TileFrameX;
			int yFrameOffset = Main.tile[i, j].TileFrameY;
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureAuric/AuricReinforcedCrateTile_Glow", (AssetRequestMode)2);
			}
			Texture2D glowmask = GlowTexture.Value;
			Vector2 drawOffest = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + drawOffest;
			Color drawColour = Color.White;
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
