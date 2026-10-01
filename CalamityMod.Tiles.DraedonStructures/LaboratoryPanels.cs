using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.DraedonStructures;

public class LaboratoryPanels : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<HazardChevronPanels>());
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.DustType = 109;
		base.MinPick = 30;
		AddMapEntry(new Color(36, 35, 37));
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
