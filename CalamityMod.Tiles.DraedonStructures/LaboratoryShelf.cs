using CalamityMod.Items.Placeables.DraedonStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.DraedonStructures;

public class LaboratoryShelf : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpPlatform(ModContent.ItemType<global::CalamityMod.Items.Placeables.DraedonStructures.LaboratoryShelf>(), lavaImmune: true);
		base.HitSound = SoundID.Tink;
		base.DustType = 30;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[base.Type] = false;
	}
}
