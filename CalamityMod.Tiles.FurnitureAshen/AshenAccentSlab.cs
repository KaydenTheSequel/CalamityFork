using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAshen;

public class AshenAccentSlab : ModTile
{
	public Asset<Texture2D> GlowTexture;

	private int animationFrameWidth = 234;

	public override string Texture => "CalamityMod/Tiles/FurnitureAshen/AshenSlab";

	public override void SetStaticDefaults()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = false;
		Main.tileBlockLight[base.Type] = true;
		base.HitSound = SoundID.Tink;
		base.MineResist = 2f;
		base.MinPick = 100;
		AddMapEntry(new Color(40, 24, 48));
		base.AnimationFrameHeight = 90;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 60, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int uniqueAnimationFrameX = Main.tileFrame[base.Type] + i;
		int uniqueAnimationFrameY = Main.tileFrame[base.Type] + j;
		int xPos = i % 2;
		int yPos = j % 3;
		int xPattern = i % 20 / 2;
		int yPattern = j % 30 / 3;
		int xOffset = xPattern switch
		{
			0 => yPattern switch
			{
				1 => 1, 
				5 => 0, 
				8 => 2, 
				9 => 1, 
				_ => -1, 
			}, 
			1 => yPattern switch
			{
				0 => 2, 
				3 => 0, 
				8 => 1, 
				_ => -1, 
			}, 
			2 => yPattern switch
			{
				2 => 0, 
				7 => 1, 
				8 => 0, 
				_ => -1, 
			}, 
			3 => yPattern switch
			{
				2 => 2, 
				5 => 2, 
				7 => 1, 
				_ => -1, 
			}, 
			4 => yPattern switch
			{
				1 => 0, 
				4 => 0, 
				9 => 2, 
				_ => -1, 
			}, 
			5 => yPattern switch
			{
				0 => 1, 
				7 => 0, 
				8 => 1, 
				_ => -1, 
			}, 
			6 => yPattern switch
			{
				5 => 2, 
				8 => 1, 
				12 => 0, 
				_ => -1, 
			}, 
			7 => yPattern switch
			{
				2 => 0, 
				6 => 0, 
				7 => 1, 
				_ => -1, 
			}, 
			8 => yPattern switch
			{
				3 => 0, 
				4 => 0, 
				9 => 0, 
				_ => -1, 
			}, 
			9 => yPattern switch
			{
				1 => 0, 
				7 => 0, 
				_ => -1, 
			}, 
			_ => -1, 
		};
		if (yPos < 2)
		{
			if (xOffset != -1)
			{
				if (j % 3 < 2)
				{
					uniqueAnimationFrameX = Main.tile[i - i % 2, j - j % 3].TileFrameNumber;
				}
				if (uniqueAnimationFrameX != 0)
				{
					uniqueAnimationFrameX += xOffset;
				}
			}
			else
			{
				uniqueAnimationFrameX = 0;
			}
		}
		switch (yPos)
		{
		case 0:
			switch (xPos)
			{
			case 0:
				uniqueAnimationFrameY = 0;
				break;
			case 1:
				uniqueAnimationFrameY = 2;
				break;
			}
			break;
		case 1:
			switch (xPos)
			{
			case 0:
				uniqueAnimationFrameY = 1;
				break;
			case 1:
				uniqueAnimationFrameY = 3;
				break;
			}
			break;
		case 2:
			uniqueAnimationFrameY = 4 + xPos;
			uniqueAnimationFrameX = 0;
			break;
		default:
			uniqueAnimationFrameY = 0;
			break;
		}
		frameXOffset = uniqueAnimationFrameX * animationFrameWidth;
		frameYOffset = uniqueAnimationFrameY * base.AnimationFrameHeight;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].IsTileActuallyInvisible())
		{
			return;
		}
		int uniqueAnimationFrameX = 0;
		int uniqueAnimationFrameY = 0;
		int xPos = i % 2;
		int yPos = j % 3;
		int xPattern = i % 20 / 2;
		int yPattern = j % 30 / 3;
		int xOffset = xPattern switch
		{
			0 => yPattern switch
			{
				1 => 1, 
				5 => 0, 
				8 => 2, 
				9 => 1, 
				_ => -1, 
			}, 
			1 => yPattern switch
			{
				0 => 2, 
				3 => 0, 
				8 => 1, 
				_ => -1, 
			}, 
			2 => yPattern switch
			{
				2 => 0, 
				7 => 1, 
				8 => 0, 
				_ => -1, 
			}, 
			3 => yPattern switch
			{
				2 => 2, 
				5 => 2, 
				7 => 1, 
				_ => -1, 
			}, 
			4 => yPattern switch
			{
				1 => 0, 
				4 => 0, 
				9 => 2, 
				_ => -1, 
			}, 
			5 => yPattern switch
			{
				0 => 1, 
				7 => 0, 
				8 => 1, 
				_ => -1, 
			}, 
			6 => yPattern switch
			{
				5 => 2, 
				8 => 1, 
				12 => 0, 
				_ => -1, 
			}, 
			7 => yPattern switch
			{
				2 => 0, 
				6 => 0, 
				7 => 1, 
				_ => -1, 
			}, 
			8 => yPattern switch
			{
				3 => 0, 
				4 => 0, 
				9 => 0, 
				_ => -1, 
			}, 
			9 => yPattern switch
			{
				1 => 0, 
				7 => 0, 
				_ => -1, 
			}, 
			_ => -1, 
		};
		if (yPos < 2)
		{
			if (xOffset != -1)
			{
				if (j % 3 < 2)
				{
					uniqueAnimationFrameX = Main.tile[i - i % 2, j - j % 3].TileFrameNumber;
				}
				if (uniqueAnimationFrameX != 0)
				{
					uniqueAnimationFrameX += xOffset;
				}
			}
			else
			{
				uniqueAnimationFrameX = 0;
			}
		}
		switch (yPos)
		{
		case 0:
			switch (xPos)
			{
			case 0:
				uniqueAnimationFrameY = 0;
				break;
			case 1:
				uniqueAnimationFrameY = 2;
				break;
			}
			break;
		case 1:
			switch (xPos)
			{
			case 0:
				uniqueAnimationFrameY = 1;
				break;
			case 1:
				uniqueAnimationFrameY = 3;
				break;
			}
			break;
		case 2:
			uniqueAnimationFrameY = 4 + xPos;
			uniqueAnimationFrameX = 0;
			break;
		default:
			uniqueAnimationFrameY = 0;
			break;
		}
		int AnimationFrameHeight = 90;
		int animationFrameWidth = 234;
		int xDrawPos = Main.tile[i, j].TileFrameX + uniqueAnimationFrameX * animationFrameWidth;
		int yDrawPos = Main.tile[i, j].TileFrameY + uniqueAnimationFrameY * AnimationFrameHeight;
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
		if (GlowTexture == null)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureAshen/AshenSlabGlow", (AssetRequestMode)2);
		}
		Texture2D glowmask = GlowTexture.Value;
		Color drawColour = CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, new Color(64, 64, 64, 64));
		Tile trackTile = Main.tile[i, j];
		if (!trackTile.IsHalfBlock && trackTile.Slope == SlopeType.Solid)
		{
			Main.spriteBatch.Draw(glowmask, drawOffset, (Rectangle?)new Rectangle(xDrawPos, yDrawPos, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		else if (trackTile.IsHalfBlock)
		{
			Main.spriteBatch.Draw(glowmask, drawOffset + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xDrawPos, yDrawPos, 18, 8), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}
}
