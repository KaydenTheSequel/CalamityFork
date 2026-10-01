using CalamityMod.Sounds;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.DraedonStructures;

public class HazardChevronPanels : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<LaboratoryDoorOpen>());
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<LaboratoryDoorClosed>());
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<AgedLaboratoryDoorOpen>());
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<AgedLaboratoryDoorClosed>());
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<LaboratoryPanels>());
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.DustType = 19;
		base.MinPick = 30;
		AddMapEntry(new Color(163, 150, 73));
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
