using System;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class SmallBrainCoral : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(base.Type);
		base.DustType = 253;
		AddMapEntry(new Color(36, 61, 111));
		base.SetStaticDefaults();
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (tile.IsTileActuallyInvisible())
		{
			return false;
		}
		float glowbrightness = 1f;
		float glowspeed = (float)(Main.timeForVisualEffects * 0.01);
		glowbrightness *= MathF.Sin((float)i / 60f + glowspeed);
		int xFrameOffset = tile.TileFrameX;
		int yFrameOffset = tile.TileFrameY;
		Texture2D glowmask = TextureAssets.Tile[base.Type].Value;
		Vector2 drawOffest = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + drawOffest;
		Color drawColour = Color.White * glowbrightness;
		if (!tile.IsHalfBlock && tile.Slope == SlopeType.Solid)
		{
			spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)new Rectangle(xFrameOffset, yFrameOffset, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		else if (tile.IsHalfBlock)
		{
			spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		if (Main.gamePaused || !closer || !Main.rand.NextBool(300))
		{
			return;
		}
		int tileLocationY = j - 1;
		if (Main.tile[i, tileLocationY] != null && !Main.tile[i, tileLocationY].HasTile && Main.tile[i, tileLocationY].LiquidAmount == byte.MaxValue && Main.tile[i, tileLocationY - 1].LiquidAmount == byte.MaxValue && Main.tile[i, tileLocationY - 2].LiquidAmount == byte.MaxValue)
		{
			for (int t = 0; t < 5; t++)
			{
				Dust dust = Dust.NewDustDirect(new Vector2((float)i, (float)j + 0.5f) * 16f, 16, 16, 59, 0f, 0f, 1, default(Color), 1.5f);
				dust.velocity *= 0.2f;
				dust.noGravity = true;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			Dust dust2 = Dust.NewDustDirect(new Vector2((float)i, (float)j + 0.5f) * 16f, 16, 16, 15, 0f, 0f, 1, Color.LightSkyBlue, 0.5f);
			dust2.velocity *= 0.2f;
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(new EntitySource_WorldEvent(), i * 16 + 16, tileLocationY * 16 + 16, 0f, -0.1f, ModContent.ProjectileType<CoralBubbleSmall>(), 0, 1f, Main.myPlayer);
			}
		}
	}
}
