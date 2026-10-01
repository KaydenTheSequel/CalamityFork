using CalamityMod.Items.Placeables.Furniture.Fountains;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Furniture.Fountains;

public class BrimstoneLavaFountainTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpFountain(ModContent.ItemType<BrimstoneLavaFountainItem>(), new Color(251, 136, 130), lava: true);
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		Player player = Main.LocalPlayer;
		if (player != null && !Main.dedServ && Main.tile[i, j].TileFrameX >= 36)
		{
			player.Calamity().BrimstoneLavaFountainCounter = 5;
		}
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(119, 102, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 35, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 6)
		{
			frame = (frame + 1) % 6;
			frameCounter = 0;
		}
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 2, 4);
	}

	public override bool RightClick(int i, int j)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		FurnitureCommon.LightHitWire(base.Type, i, j, 2, 4);
		SoundEngine.PlaySound(in SoundID.Mech, (Vector2?)new Vector2((float)(i * 16), (float)(j * 16)), (SoundUpdateCallback?)null);
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<BrimstoneLavaFountainItem>();
	}
}
