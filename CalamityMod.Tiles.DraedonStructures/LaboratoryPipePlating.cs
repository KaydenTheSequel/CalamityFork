using CalamityMod.Sounds;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.DraedonStructures;

public class LaboratoryPipePlating : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.DustType = 32;
		base.MinPick = 30;
		AddMapEntry(new Color(91, 64, 56));
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		TileFramingSystem.CustomMergeFrame(i, j, base.Type, ModContent.TileType<RustedPipes>(), false, false, false, false, true);
		return false;
	}
}
