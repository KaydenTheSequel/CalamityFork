using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureExo;

public class ExoScreenTile : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(71, 95, 114), CalamityUtils.GetText("Tiles.Screen"));
		base.DustType = 8;
		base.AnimationFrameHeight = 36;
		TileID.Sets.FramesOnKillWall[base.Type] = true;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		int frameAmt = 4;
		frameCounter++;
		if (frameCounter >= 180)
		{
			frame = (frame + Main.rand.Next(1, frameAmt)) % frameAmt;
			frameCounter = 0;
		}
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameYOffset = this.GetAnimationOffset(i, j, 4, 18, 18, 3, 2, base.AnimationFrameHeight);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			int yOffset = TileObjectData.GetTileData(tile).DrawYOffset;
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureExo/ExoScreenGlow", (AssetRequestMode)2);
			}
			Texture2D glowmask = GlowTexture.Value;
			Vector2 drawOffset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
			Vector2 drawPosition = new Vector2((float)(i * 16) - (float)(int)Main.screenPosition.X / 2f, (float)(j * 16 - (int)Main.screenPosition.Y + yOffset)) + drawOffset;
			Color drawColour = Color.White;
			Main.spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)new Rectangle((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}
}
