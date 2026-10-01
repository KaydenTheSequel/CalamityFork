using CalamityMod.Items.Placeables.DraedonStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.DraedonStructures;

public class RustedShelf : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpPlatform(ModContent.ItemType<global::CalamityMod.Items.Placeables.DraedonStructures.RustedShelf>(), lavaImmune: true);
		base.HitSound = SoundID.Tink;
		base.DustType = 32;
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
