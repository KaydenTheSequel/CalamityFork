using CalamityMod.TileEntities;
using CalamityMod.UI.DraedonSummoning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonSummoner;

public class CodebreakerTile : ModTile
{
	public const int Width = 5;

	public const int Height = 8;

	public const int OriginOffsetX = 2;

	public const int OriginOffsetY = 7;

	public const int SheetSquare = 18;

	public static Texture2D TileTexture;

	public static Texture2D ComputerTexture;

	public static Texture2D SensorTexture;

	public static Texture2D DisplayTexture;

	public static Texture2D VoltageRegulatorTexture;

	public static Texture2D VoltageRegulatorTexture2;

	public static Texture2D CoolingCellTexture;

	public static Texture2D HighlightT1;

	public static Texture2D HighlightT2;

	public static Texture2D HighlightT3;

	public static Texture2D HighlightT4;

	public override void SetStaticDefaults()
	{
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			TileTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerTile", (AssetRequestMode)1).Value;
			ComputerTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerDecryptionComputer", (AssetRequestMode)1).Value;
			SensorTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerLongRangedSensorArray", (AssetRequestMode)1).Value;
			DisplayTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerAdvancedDisplay", (AssetRequestMode)1).Value;
			VoltageRegulatorTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerVoltageRegulationSystem", (AssetRequestMode)1).Value;
			VoltageRegulatorTexture2 = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerVoltageRegulationSystem2", (AssetRequestMode)1).Value;
			CoolingCellTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerAuricQuantumCoolingCell", (AssetRequestMode)1).Value;
			HighlightT1 = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerT1Highlight", (AssetRequestMode)1).Value;
			HighlightT2 = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerT2Highlight", (AssetRequestMode)1).Value;
			HighlightT3 = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerT3Highlight", (AssetRequestMode)1).Value;
			HighlightT4 = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonSummoner/CodebreakerT4Highlight", (AssetRequestMode)1).Value;
		}
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileID.Sets.PreventsTileRemovalIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileHammeringIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Width = 5;
		TileObjectData.newTile.Height = 8;
		TileObjectData.newTile.Origin = new Point16(2, 7);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = new int[8];
		for (int i = 0; i < 8; i++)
		{
			TileObjectData.newTile.CoordinateHeights[i] = 16;
		}
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TECodebreaker>().Hook_AfterPlacement, -1, 0, processedCoordinates: true);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(92, 107, 112), CreateMapEntryName());
		base.AnimationFrameHeight = 144;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CanPlace(int i, int j)
	{
		int startOfTileCoordinateCheckX = i - 2;
		for (int k = startOfTileCoordinateCheckX; k < startOfTileCoordinateCheckX + 5; k++)
		{
			if (Main.tile[k, j + 1].TileType == 235)
			{
				return false;
			}
		}
		return true;
	}

	public override bool CanKillTile(int i, int j, ref bool blockDamaged)
	{
		TECodebreaker codebreakerTileEntity = CalamityUtils.FindTileEntity<TECodebreaker>(i, j, 5, 8, 18);
		if (codebreakerTileEntity == null)
		{
			return true;
		}
		if (codebreakerTileEntity.DecryptionCountdown > 0)
		{
			return false;
		}
		return true;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.18f;
		g = 0.8f;
		b = 0.9f;
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

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Tile t = Main.tile[i, j];
		int left = i - t.TileFrameX % 90 / 18;
		int top = j - t.TileFrameY % 144 / 18;
		TECodebreaker tECodebreaker = CalamityUtils.FindTileEntity<TECodebreaker>(i, j, 5, 8, 18);
		tECodebreaker?.DropConstituents(i, j);
		tECodebreaker?.Kill(left, top);
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		TECodebreaker codebreakerTileEntity = CalamityUtils.FindTileEntity<TECodebreaker>(i, j, 5, 8, 18);
		Player player = Main.LocalPlayer;
		player.CancelSignsAndChests();
		if (codebreakerTileEntity == null || codebreakerTileEntity.ID == CodebreakerUI.ViewedTileEntityID || !codebreakerTileEntity.ContainsDecryptionComputer)
		{
			if (!codebreakerTileEntity.ContainsDecryptionComputer)
			{
				CombatText.NewText(player.Hitbox, Color.Cyan, CalamityUtils.GetTextValue("Misc.NoComputer"));
			}
			CodebreakerUI.ViewedTileEntityID = -1;
			SoundEngine.PlaySound(in SoundID.MenuClose);
		}
		else if (codebreakerTileEntity != null)
		{
			SoundEngine.PlaySound((CodebreakerUI.ViewedTileEntityID == -1) ? SoundID.MenuOpen : SoundID.MenuTick);
			CodebreakerUI.ViewedTileEntityID = codebreakerTileEntity.ID;
			Main.playerInventory = true;
			Main.recBigList = false;
		}
		Recipe.FindRecipes();
		return true;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		Tile t = Main.tile[i, j];
		if (t.IsTileActuallyInvisible())
		{
			return false;
		}
		int left = i - t.TileFrameX % 90 / 18;
		int frameXPos = t.TileFrameX;
		int frameYPos = t.TileFrameY + 144 * (int)((Main.GlobalTimeWrappedHourly * 12f + (float)left) % 8f);
		TECodebreaker codebreakerTileEntity = CalamityUtils.FindTileEntity<TECodebreaker>(i, j, 5, 8, 18);
		Vector2 offset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		if (t.IsHalfBlock)
		{
			offset.Y += 8f;
		}
		Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + offset;
		Color drawColor = Lighting.GetColor(i, j);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(frameXPos, frameYPos, 16, 16);
		Texture2D HighlightToUse = HighlightT1;
		if ((!t.IsHalfBlock && t.Slope == SlopeType.Solid) || t.IsHalfBlock)
		{
			spriteBatch.Draw(TileTexture, drawPosition, (Rectangle?)frame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			if (codebreakerTileEntity != null)
			{
				if (codebreakerTileEntity.ContainsDecryptionComputer)
				{
					spriteBatch.Draw(ComputerTexture, drawPosition, (Rectangle?)frame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
				}
				if (codebreakerTileEntity.ContainsVoltageRegulationSystem)
				{
					spriteBatch.Draw(VoltageRegulatorTexture2, drawPosition, (Rectangle?)frame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
				}
				if (codebreakerTileEntity.ContainsSensorArray)
				{
					spriteBatch.Draw(SensorTexture, drawPosition, (Rectangle?)frame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
				}
				if (codebreakerTileEntity.ContainsCoolingCell)
				{
					spriteBatch.Draw(CoolingCellTexture, drawPosition, (Rectangle?)frame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
				}
				if (codebreakerTileEntity.ContainsVoltageRegulationSystem)
				{
					spriteBatch.Draw(VoltageRegulatorTexture, drawPosition, (Rectangle?)frame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
				}
				if (codebreakerTileEntity.ContainsAdvancedDisplay)
				{
					spriteBatch.Draw(DisplayTexture, drawPosition, (Rectangle?)frame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
				}
				if (codebreakerTileEntity.ContainsDecryptionComputer && codebreakerTileEntity.ContainsSensorArray && !codebreakerTileEntity.ContainsAdvancedDisplay && !codebreakerTileEntity.ContainsVoltageRegulationSystem)
				{
					HighlightToUse = HighlightT2;
				}
				if (codebreakerTileEntity.ContainsDecryptionComputer && codebreakerTileEntity.ContainsSensorArray && codebreakerTileEntity.ContainsAdvancedDisplay && !codebreakerTileEntity.ContainsVoltageRegulationSystem)
				{
					HighlightToUse = HighlightT3;
				}
				if (codebreakerTileEntity.ContainsDecryptionComputer && codebreakerTileEntity.ContainsSensorArray && codebreakerTileEntity.ContainsAdvancedDisplay && codebreakerTileEntity.ContainsVoltageRegulationSystem)
				{
					HighlightToUse = HighlightT4;
				}
				if (codebreakerTileEntity.ContainsDecryptionComputer && !codebreakerTileEntity.ContainsSensorArray && (codebreakerTileEntity.ContainsAdvancedDisplay || codebreakerTileEntity.ContainsVoltageRegulationSystem))
				{
					return false;
				}
				if (codebreakerTileEntity.ContainsDecryptionComputer && codebreakerTileEntity.ContainsSensorArray && !codebreakerTileEntity.ContainsAdvancedDisplay && codebreakerTileEntity.ContainsVoltageRegulationSystem)
				{
					return false;
				}
				if (Main.InSmartCursorHighlightArea(i, j, out var actuallySelected) && codebreakerTileEntity.ContainsDecryptionComputer)
				{
					int avgBrightness = (((Color)(ref drawColor)).R + ((Color)(ref drawColor)).G + ((Color)(ref drawColor)).B) / 3;
					if (avgBrightness > 10)
					{
						Color highlightColor = Colors.GetSelectionGlowColor(actuallySelected, avgBrightness);
						spriteBatch.Draw(HighlightToUse, drawPosition, (Rectangle?)frame, highlightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
					}
				}
			}
		}
		return false;
	}
}
