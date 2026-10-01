using System;
using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture;

public class LanternCenterTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 18 };
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(99, 99, 99), CalamityUtils.GetItemName<LanternCenter>());
		TileID.Sets.HasOutlines[base.Type] = true;
		TileID.Sets.InteractibleByNPCs[base.Type] = true;
		TileID.Sets.DisableSmartInteract[base.Type] = true;
		base.AnimationFrameHeight = 54;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		return false;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (!LanternNight.LanternsUp)
		{
			frame = 0;
			frameCounter = 0;
			return;
		}
		frameCounter++;
		if (frameCounter >= 6)
		{
			frame = (frame + 1) % 7;
			frameCounter = 0;
		}
		frame = Math.Clamp(frame, 1, 6);
	}

	public override void HitWire(int i, int j)
	{
		LanternNight.ToggleManualLanterns();
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		LanternNight.ToggleManualLanterns();
		SoundEngine.PlaySound(in SoundID.Mech, (Vector2?)new Vector2((float)(i * 16), (float)(j * 16)), (SoundUpdateCallback?)null);
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<LanternCenter>();
	}
}
