using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Furniture.Fountains;
using CalamityMod.Waters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Furniture.Fountains;

public class AstralFountainTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpFountain(ModContent.ItemType<AstralFountainItem>(), new Color(59, 50, 77));
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (!Main.dedServ && Main.tile[i, j].TileFrameX >= 36)
		{
			Main.SceneMetrics.ActiveFountainColor = AstralWater.Instance.Slot;
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
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralBlue>());
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralOrange>());
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
			frame = (frame + 1) % 4;
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
		localPlayer.cursorItemIconID = ModContent.ItemType<AstralFountainItem>();
	}
}
