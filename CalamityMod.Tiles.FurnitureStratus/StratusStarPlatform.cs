using CalamityMod.Items.Placeables.FurnitureStratus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureStratus;

public class StratusStarPlatform : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		this.SetUpPlatform(ModContent.ItemType<StratusStarPlatformItem>(), lavaImmune: true);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 130, 150));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 132, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[base.Type] = false;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			int xPos = Main.tile[i, j].TileFrameX;
			int yPos = Main.tile[i, j].TileFrameY;
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureStratus/StratusStarPlatformGlow", (AssetRequestMode)2);
			}
			Texture2D glowmask = GlowTexture.Value;
			Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Tile trackTile = Main.tile[i, j];
			if (!trackTile.IsHalfBlock || trackTile.Slope != SlopeType.Solid)
			{
				spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)new Rectangle(xPos, yPos, 18, 18), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
			else if (trackTile.IsHalfBlock)
			{
				spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xPos, yPos, 18, 8), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
