using CalamityMod.CalPlayer;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures;

public class ChargingStation : ModTile
{
	public const int Width = 3;

	public const int Height = 2;

	public const int OriginOffsetX = 1;

	public const int OriginOffsetY = 1;

	public const int SheetSquare = 18;

	public const int FramesPerChargeAction = 8;

	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.LavaDeath = false;
		ModTileEntity te = ModContent.GetInstance<TEChargingStation>();
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(te.Hook_AfterPlacement, -1, 0, processedCoordinates: true);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(67, 72, 81), CalamityUtils.GetItemName<ChargingStationItem>());
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
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 226);
		return false;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		Tile t = Main.tile[i, j];
		int left = i - t.TileFrameX % 54 / 18;
		int top = j - t.TileFrameY % 36 / 18;
		Vector2 dropPos = new Vector2((float)i, (float)j) * 16f;
		TEChargingStation tEChargingStation = CalamityUtils.FindTileEntity<TEChargingStation>(i, j, 3, 2, 18);
		int numCells = tEChargingStation?.CellStack ?? 0;
		if (numCells > 0)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), dropPos, ModContent.ItemType<DraedonPowerCell>(), numCells);
		}
		Item pluggedItem = tEChargingStation?.PluggedItem ?? null;
		if (pluggedItem != null && !pluggedItem.IsAir && Main.netMode != 1)
		{
			DropHelper.DropItemClone(new EntitySource_TileBreak(i, j), pluggedItem, dropPos, pluggedItem.stack);
		}
		tEChargingStation?.Kill(left, top);
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		TEChargingStation thisCharger = CalamityUtils.FindTileEntity<TEChargingStation>(i, j, 3, 2, 18);
		Player localPlayer = Main.LocalPlayer;
		localPlayer.CancelSignsAndChests();
		CalamityPlayer mp = localPlayer.Calamity();
		if (thisCharger == null || thisCharger.ID == mp.CurrentlyViewedChargerID)
		{
			mp.CurrentlyViewedChargerID = -1;
			SoundEngine.PlaySound(in SoundID.MenuClose);
		}
		else if (thisCharger != null)
		{
			SoundEngine.PlaySound((mp.CurrentlyViewedChargerID == -1) ? SoundID.MenuOpen : SoundID.MenuTick);
			mp.CurrentlyViewedChargerID = thisCharger.ID;
			Main.playerInventory = true;
			Main.recBigList = false;
		}
		Recipe.FindRecipes();
		return true;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			Tile t = Main.tile[i, j];
			int xFrame = t.TileFrameX;
			int yFrame = t.TileFrameY;
			TEChargingStation tEChargingStation = CalamityUtils.FindTileEntity<TEChargingStation>(i, j, 3, 2, 18);
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/DraedonStructures/ChargingStation_Glow", (AssetRequestMode)2);
			}
			Vector2 screenOffset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + screenOffset;
			Color drawColor = tEChargingStation?.LightColor ?? Color.Red;
			if (!t.IsHalfBlock && t.Slope == SlopeType.Solid)
			{
				Main.spriteBatch.Draw(GlowTexture.Value, drawOffset, (Rectangle?)new Rectangle(xFrame, yFrame, 18, 18), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
			else if (t.IsHalfBlock)
			{
				Main.spriteBatch.Draw(GlowTexture.Value, drawOffset + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xFrame, yFrame, 18, 8), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}
}
