using CalamityMod.Items.Placeables.FurnitureMarnite;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureMarnite;

public class PolishedMarnitePlatform : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpPlatform(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureMarnite.PolishedMarnitePlatform>(), lavaImmune: true);
		base.HitSound = SoundID.Tink;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 240, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[base.Type] = false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
