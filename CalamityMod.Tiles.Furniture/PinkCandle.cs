using CalamityMod.Buffs.Placeables;
using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture;

public class PinkCandle : ModTile
{
	public static readonly SoundStyle ActivationSound = new SoundStyle("CalamityMod/Sounds/Item/LouderPhantomPhoenix2");

	public override void SetStaticDefaults()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.addTile(base.Type);
		base.AdjTiles = new int[1] { 33 };
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(238, 145, 105), CalamityUtils.GetItemName<VigorousCandle>());
		TileID.Sets.HasOutlines[base.Type] = true;
		base.AnimationFrameHeight = 18;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Player localPlayer = Main.LocalPlayer;
		localPlayer.ClearBuff(ModContent.BuffType<BlueCandleBuff>());
		localPlayer.ClearBuff(ModContent.BuffType<PurpleCandleBuff>());
		localPlayer.ClearBuff(ModContent.BuffType<PinkCandleBuff>());
		localPlayer.ClearBuff(ModContent.BuffType<YellowCandleBuff>());
		localPlayer.AddBuff(ModContent.BuffType<PinkCandleBuff>(), 108000);
		SoundEngine.PlaySound(in ActivationSound, (Vector2?)new Vector2((float)(i * 16), (float)(j * 16)), (SoundUpdateCallback?)null);
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<VigorousCandle>();
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 6)
		{
			frame = (frame + 1) % 5;
			frameCounter = 0;
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.75f;
		g = 0.35f;
		b = 0.65f;
	}
}
