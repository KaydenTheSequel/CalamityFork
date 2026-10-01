using CalamityMod.Items.Placeables.FurnitureExo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureExo;

public class ExoPrismPlatformTile : ModTile
{
	internal static Texture2D GlowTexture;

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureExo/ExoPrismPlatformTileGlow", (AssetRequestMode)1).Value;
		}
		this.SetUpPlatform(ModContent.ItemType<ExoPrismPlatform>(), lavaImmune: true);
		base.AnimationFrameHeight = 18;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 107, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[base.Type] = false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int frameOffset = (i + j) % 6;
		frameYOffset = frameOffset * base.AnimationFrameHeight;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		if (GlowTexture == null)
		{
			return;
		}
		Tile tile = CalamityUtils.ParanoidTileRetrieval(i, j);
		if (!tile.IsTileActuallyInvisible())
		{
			int xPos = tile.TileFrameX;
			int frameOffset = (i + j) % 6 * base.AnimationFrameHeight;
			int yPos = tile.TileFrameY + frameOffset;
			Vector2 drawOffset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + drawOffset;
			if (!tile.IsHalfBlock)
			{
				spriteBatch.Draw(GlowTexture, drawPosition, (Rectangle?)new Rectangle(xPos, yPos, 18, 18), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
			else if (tile.IsHalfBlock)
			{
				spriteBatch.Draw(GlowTexture, drawPosition + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xPos, yPos, 18, 8), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}
}
