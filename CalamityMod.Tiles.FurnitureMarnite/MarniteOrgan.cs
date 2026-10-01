using CalamityMod.Items.Placeables.FurnitureMarnite;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureMarnite;

public class MarniteOrgan : ModTile
{
	public static readonly SoundStyle MarniteOrganSound = new SoundStyle("CalamityMod/Sounds/Music/MarniteOrgan", 1);

	public override void SetStaticDefaults()
	{
		this.SetUpPiano(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureMarnite.MarniteOrgan>(), lavaImmune: true);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 240, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureMarnite.MarniteOrgan>();
	}

	public override bool RightClick(int i, int j)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in MarniteOrganSound);
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
