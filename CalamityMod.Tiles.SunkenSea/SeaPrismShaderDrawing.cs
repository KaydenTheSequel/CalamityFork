using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class SeaPrismShaderDrawing : ModSystem
{
	private int SeaPrismTileType = -1;

	private int SeaPrismCrystalTileType = -1;

	private int MediumSeaPrismCrystalTileType = -1;

	private int SeaPrismWallType = -1;

	private int UnsafeSeaPrismWallType = -1;

	public override void OnModLoad()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		On_Main.DrawTiles += new hook_DrawTiles(DrawSeaPrismsAndCrystals);
		On_Main.DrawWalls += new hook_DrawWalls(DrawSeaPrismWalls);
		SeaPrismTileType = ModContent.TileType<SeaPrism>();
		SeaPrismCrystalTileType = ModContent.TileType<SeaPrismCrystals>();
		MediumSeaPrismCrystalTileType = ModContent.TileType<MediumSeaPrismCrystal>();
		SeaPrismWallType = ModContent.WallType<SeaPrismWall>();
		UnsafeSeaPrismWallType = ModContent.WallType<UnsafeSeaPrismWall>();
	}

	private static void GetScreenDrawArea(Vector2 screenPosition, Vector2 offSet, out int firstTileX, out int lastTileX, out int firstTileY, out int lastTileY)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		firstTileX = (int)((screenPosition.X - offSet.X) / 16f - 1f);
		lastTileX = (int)((screenPosition.X + (float)Main.screenWidth + offSet.X) / 16f) + 2;
		firstTileY = (int)((screenPosition.Y - offSet.Y) / 16f - 1f);
		lastTileY = (int)((screenPosition.Y + (float)Main.screenHeight + offSet.Y) / 16f) + 5;
		if (firstTileX < 4)
		{
			firstTileX = 4;
		}
		if (lastTileX > Main.maxTilesX - 4)
		{
			lastTileX = Main.maxTilesX - 4;
		}
		if (firstTileY < 4)
		{
			firstTileY = 4;
		}
		if (lastTileY > Main.maxTilesY - 4)
		{
			lastTileY = Main.maxTilesY - 4;
		}
	}

	private void DrawSeaPrismsAndCrystals(orig_DrawTiles orig, Main self, bool solidLayer, bool forRenderTargets, bool intoRenderTargets, int waterStyleOverride)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offscreenPosition = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Main.tileBatch.End();
		Main.spriteBatch.End();
		Texture oldTex1 = ((Game)Main.instance).GraphicsDevice.Textures[1];
		SamplerState oldSampler1 = ((Game)Main.instance).GraphicsDevice.SamplerStates[1];
		Texture oldTex2 = ((Game)Main.instance).GraphicsDevice.Textures[2];
		SamplerState oldSampler2 = ((Game)Main.instance).GraphicsDevice.SamplerStates[2];
		Texture oldTex3 = ((Game)Main.instance).GraphicsDevice.Textures[3];
		SamplerState oldSampler3 = ((Game)Main.instance).GraphicsDevice.SamplerStates[3];
		Effect shader = CalamityShaders.SeaPrismColorBlendingShader.Value;
		shader.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly);
		shader.Parameters["screenOffset"].SetValue(Main.screenPosition);
		shader.Parameters["offscreenOffset"].SetValue(offscreenPosition);
		shader.Parameters["diagonalScreenLength"].SetValue((float)Main.screenWidth / 2f - (float)Main.screenHeight / 2f);
		shader.Parameters["doGlint"].SetValue(true);
		GetScreenDrawArea(Main.Camera.UnscaledPosition, offscreenPosition + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out var firstTileX, out var lastTileX, out var firstTileY, out var lastTileY);
		if (solidLayer)
		{
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)SeaPrism.Green.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
			((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)SeaPrism.Purple.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearClamp;
			((Game)Main.instance).GraphicsDevice.Textures[3] = (Texture)(object)SeaPrism.Glint.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[3] = SamplerState.LinearClamp;
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shader, Matrix.Identity);
			Rectangle sourceRect = default(Rectangle);
			for (int y = firstTileY; y < lastTileY + 4; y++)
			{
				for (int x = firstTileX - 2; x < lastTileX + 2; x++)
				{
					if (!WorldGen.InWorld(x, y))
					{
						continue;
					}
					Tile tile = Main.tile[x, y];
					if (!(tile == null) && tile.TileType == SeaPrismTileType)
					{
						Vector2 position = new Vector2((float)(x * 16), (float)(y * 16)) - Main.screenPosition + offscreenPosition;
						int frameX = tile.TileFrameX + x % 8 * 468;
						int frameY = tile.TileFrameY + y % 8 * 90;
						((Rectangle)(ref sourceRect))._002Ector(frameX, frameY, 16, 16);
						Color light = Lighting.GetColor(x, y) * 1.5f;
						if (tile.IsActuated)
						{
							light = light.MultiplyRGB(Color.White * 0.4f);
						}
						Main.spriteBatch.Draw(SeaPrism.Blue.Value, position, (Rectangle?)sourceRect, light);
					}
				}
			}
		}
		else
		{
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)SeaPrismCrystals.GreenCrystals.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
			((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)SeaPrismCrystals.PurpleCrystals.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearClamp;
			((Game)Main.instance).GraphicsDevice.Textures[3] = (Texture)(object)SeaPrismCrystals.Glint.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[3] = SamplerState.LinearClamp;
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shader, Matrix.Identity);
			Rectangle sourceRect2 = default(Rectangle);
			for (int i = firstTileY; i < lastTileY + 4; i++)
			{
				for (int j = firstTileX - 2; j < lastTileX + 2; j++)
				{
					if (WorldGen.InWorld(j, i))
					{
						Tile tile2 = Main.tile[j, i];
						if (!(tile2 == null) && tile2.TileType == SeaPrismCrystalTileType)
						{
							Vector2 offScreen = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
							Vector2 position2 = new Vector2((float)(j * 16), (float)(i * 16)) - Main.screenPosition + offScreen;
							((Rectangle)(ref sourceRect2))._002Ector((int)tile2.TileFrameX, (int)tile2.TileFrameY, 16, 16);
							Color light2 = Lighting.GetColor(j, i) * 1.5f;
							Main.spriteBatch.Draw(SeaPrismCrystals.BlueCrystals.Value, position2, (Rectangle?)sourceRect2, light2);
						}
					}
				}
			}
			Main.spriteBatch.End();
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)MediumSeaPrismCrystal.GreenCrystals.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
			((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)MediumSeaPrismCrystal.PurpleCrystals.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearClamp;
			((Game)Main.instance).GraphicsDevice.Textures[3] = (Texture)(object)MediumSeaPrismCrystal.Glint.Value;
			((Game)Main.instance).GraphicsDevice.SamplerStates[3] = SamplerState.LinearClamp;
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shader, Matrix.Identity);
			Rectangle sourceRect3 = default(Rectangle);
			for (int k = firstTileY; k < lastTileY + 4; k++)
			{
				for (int l = firstTileX - 2; l < lastTileX + 2; l++)
				{
					if (WorldGen.InWorld(l, k))
					{
						Tile tile3 = Main.tile[l, k];
						if (!(tile3 == null) && tile3.TileType == MediumSeaPrismCrystalTileType)
						{
							Vector2 offScreen2 = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
							Vector2 position3 = new Vector2((float)(l * 16), (float)(k * 16)) - Main.screenPosition + offScreen2;
							((Rectangle)(ref sourceRect3))._002Ector((int)tile3.TileFrameX, (int)tile3.TileFrameY, 16, 16);
							Color light3 = Lighting.GetColor(l, k) * 1.5f;
							Main.spriteBatch.Draw(MediumSeaPrismCrystal.BlueCrystals.Value, position3, (Rectangle?)sourceRect3, light3);
						}
					}
				}
			}
		}
		Main.spriteBatch.End();
		((Game)Main.instance).GraphicsDevice.Textures[1] = oldTex1;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = oldSampler1;
		((Game)Main.instance).GraphicsDevice.Textures[2] = oldTex2;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = oldSampler2;
		((Game)Main.instance).GraphicsDevice.Textures[3] = oldTex3;
		((Game)Main.instance).GraphicsDevice.SamplerStates[3] = oldSampler3;
		Main.spriteBatch.Begin();
		Main.tileBatch.Begin();
		orig.Invoke(self, solidLayer, forRenderTargets, intoRenderTargets, waterStyleOverride);
	}

	private void DrawSeaPrismWalls(orig_DrawWalls orig, Main self)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offscreenPosition = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Main.tileBatch.End();
		Main.spriteBatch.End();
		Texture oldTex1 = ((Game)Main.instance).GraphicsDevice.Textures[1];
		SamplerState oldSampler1 = ((Game)Main.instance).GraphicsDevice.SamplerStates[1];
		Texture oldTex2 = ((Game)Main.instance).GraphicsDevice.Textures[2];
		SamplerState oldSampler2 = ((Game)Main.instance).GraphicsDevice.SamplerStates[2];
		Texture oldTex3 = ((Game)Main.instance).GraphicsDevice.Textures[3];
		((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)SeaPrismWall.GlowMaskGreen.Texture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
		((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)SeaPrismWall.GlowMaskPurple.Texture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearClamp;
		((Game)Main.instance).GraphicsDevice.Textures[3] = null;
		Effect value = CalamityShaders.SeaPrismColorBlendingShader.Value;
		value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly);
		value.Parameters["screenOffset"].SetValue(Main.screenPosition);
		value.Parameters["offscreenOffset"].SetValue(offscreenPosition);
		value.Parameters["diagonalScreenLength"].SetValue((float)Main.screenWidth / 2f - (float)Main.screenHeight / 2f);
		value.Parameters["doGlint"].SetValue(false);
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Matrix.Identity);
		GetScreenDrawArea(Main.Camera.UnscaledPosition, offscreenPosition + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out var firstTileX, out var lastTileX, out var firstTileY, out var lastTileY);
		Rectangle frame = default(Rectangle);
		for (int y = firstTileY; y < lastTileY + 4; y++)
		{
			for (int x = firstTileX - 2; x < lastTileX + 2; x++)
			{
				if (!WorldGen.InWorld(x, y))
				{
					continue;
				}
				Tile tile = Main.tile[x, y];
				if (tile == null)
				{
					continue;
				}
				int type = tile.WallType;
				if (type == SeaPrismWallType || type == UnsafeSeaPrismWallType)
				{
					Vector2 position = new Vector2((float)(x * 16), (float)(y * 16)) - Main.screenPosition + offscreenPosition;
					int xLength = 32;
					int frameXOffset = x % 8 * 468;
					int frameYOffset = y % 8 * 180;
					int xPos = tile.WallFrameX + frameXOffset;
					int yPos = tile.WallFrameY + frameYOffset;
					((Rectangle)(ref frame))._002Ector(xPos, yPos, xLength, 32);
					if (SeaPrismWall.GlowMaskBlue.HasContentInFramePos(xPos, yPos))
					{
						Main.spriteBatch.Draw(SeaPrismWall.GlowMaskBlue.Texture, position + new Vector2(-8f, -8f), (Rectangle?)frame, Color.White * 1.1f);
					}
				}
			}
		}
		Main.spriteBatch.End();
		((Game)Main.instance).GraphicsDevice.Textures[1] = oldTex1;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = oldSampler1;
		((Game)Main.instance).GraphicsDevice.Textures[2] = oldTex2;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = oldSampler2;
		((Game)Main.instance).GraphicsDevice.Textures[3] = oldTex3;
		Main.spriteBatch.Begin();
		Main.tileBatch.Begin();
		orig.Invoke(self);
	}
}
