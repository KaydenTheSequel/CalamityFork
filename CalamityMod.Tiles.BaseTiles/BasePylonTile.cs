using System;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace CalamityMod.Tiles.BaseTiles;

public abstract class BasePylonTile : ModPylon
{
	public const int CrystalHorizontalFrameCount = 1;

	public const int CrystalVerticalFrameCount = 8;

	public const int CrystalFrameHeight = 64;

	public Asset<Texture2D> crystalTexture;

	public Asset<Texture2D> mapIcon;

	public virtual int DustID => 43;

	public virtual Color DustColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public virtual Color LightColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public abstract int AssociatedItem { get; }

	public abstract Color PylonMapColor { get; }

	public override void Load()
	{
		if (!Main.dedServ)
		{
			crystalTexture = ModContent.Request<Texture2D>(Texture + "_Crystal", (AssetRequestMode)2);
			mapIcon = ModContent.Request<Texture2D>(Texture + "_MapIcon", (AssetRequestMode)2);
		}
	}

	public override void Unload()
	{
		crystalTexture = null;
		mapIcon = null;
	}

	public override void SetStaticDefaults()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		RegisterItemDrop(AssociatedItem);
		TEModdedPylon moddedPylon = ModContent.GetInstance<TECalamityPylon>();
		this.SetUpPylon(moddedPylon, lavaImmune: true);
		TileID.Sets.InteractibleByNPCs[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		AddMapEntry(PylonMapColor, CalamityUtils.GetItemName(AssociatedItem));
	}

	public override void MouseOver(int i, int j)
	{
		Main.LocalPlayer.cursorItemIconEnabled = true;
		Main.LocalPlayer.cursorItemIconID = AssociatedItem;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		ModContent.GetInstance<TECalamityPylon>().Kill(i, j);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		return false;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].TileFrameX < 66)
		{
			Color lightColor = LightColor;
			r = (float)(int)((Color)(ref lightColor)).R / 255f;
			lightColor = LightColor;
			g = (float)(int)((Color)(ref lightColor)).G / 255f;
			lightColor = LightColor;
			b = (float)(int)((Color)(ref lightColor)).B / 255f;
		}
	}

	public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offScreen = default(Vector2);
		((Vector2)(ref offScreen))._002Ector((float)Main.offScreenRange);
		if (Main.drawToScreen)
		{
			offScreen = Vector2.Zero;
		}
		Point p = default(Point);
		((Point)(ref p))._002Ector(i, j);
		Tile tile = Main.tile[p.X, p.Y];
		if (tile == null || !tile.HasTile)
		{
			return;
		}
		Texture2D crystalTex = crystalTexture.Value;
		int frameY = (Main.tileFrameCounter[597] + p.X + p.Y) % 64 / 8;
		Rectangle frame = crystalTexture.Frame(1, 8, 0, frameY);
		Vector2 origin = frame.Size() / 2f;
		float sineOffset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) / 5f);
		Vector2 drawCenter = p.ToWorldCoordinates(24f, 64f) + offScreen + Vector2.UnitY * (-40f + sineOffset * 4f);
		drawCenter -= Vector2.One;
		bool frameToSpawnDust = !Lighting.UpdateEveryFrame || Main.rand.NextBool(4);
		if (((!Main.gamePaused && ((Game)Main.instance).IsActive) & frameToSpawnDust) && Main.rand.NextBool(10))
		{
			Rectangle dustBox = Utils.CenteredRectangle(drawCenter - offScreen, frame.Size());
			int dust = Dust.NewDust(dustBox.TopLeft(), dustBox.Width, dustBox.Height, DustID, 0f, 0f, 254, DustColor, 0.5f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.1f;
			Main.dust[dust].velocity.Y -= 0.2f;
		}
		Color crystalColor = Color.Lerp(Lighting.GetColor(p.X, p.Y), Color.White, 0.8f);
		spriteBatch.Draw(crystalTex, drawCenter - Main.screenPosition, (Rectangle?)frame, crystalColor * 0.7f, 0f, origin, 1f, (SpriteEffects)0, 0f);
		float glowOpacity = (float)Math.Sin((double)Main.GlobalTimeWrappedHourly * 6.2831854820251465) * 0.2f + 0.8f;
		Color glowColor = new Color(255, 255, 255, 0) * 0.1f * glowOpacity;
		float oneSixth = 1f / 6f;
		float offset = 6f + sineOffset * 2f;
		for (float k = 0f; k < 1f; k += oneSixth)
		{
			spriteBatch.Draw(crystalTex, drawCenter - Main.screenPosition + ((float)Math.PI * 2f * k).ToRotationVector2() * offset, (Rectangle?)frame, glowColor, 0f, origin, 1f, (SpriteEffects)0, 0f);
		}
		int tileSelectionTier = 0;
		if (Main.InSmartCursorHighlightArea(p.X, p.Y, out var actuallySelected))
		{
			tileSelectionTier = 1;
			if (actuallySelected)
			{
				tileSelectionTier = 2;
			}
		}
		if (tileSelectionTier != 0)
		{
			int averageBrightness = (((Color)(ref crystalColor)).R + ((Color)(ref crystalColor)).G + ((Color)(ref crystalColor)).B) / 3;
			if (averageBrightness > 10)
			{
				Texture2D vanillaCrystalSheet = TextureAssets.Extra[181].Value;
				Rectangle smartCursorGlowFrame = vanillaCrystalSheet.Frame(12, 8, 2, frameY);
				Color selectionGlowColor = Colors.GetSelectionGlowColor(tileSelectionTier == 2, averageBrightness);
				spriteBatch.Draw(vanillaCrystalSheet, drawCenter - Main.screenPosition, (Rectangle?)smartCursorGlowFrame, selectionGlowColor, 0f, origin, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		bool mouseOver = DefaultDrawMapIcon(ref context, mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
		DefaultMapClickHandle(mouseOver, pylonInfo, Lang.GetItemName(AssociatedItem).Key, ref mouseOverText);
	}
}
