using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralStone : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileBrick[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		CalamityUtils.MergeWithOres(base.Type);
		base.DustType = ModContent.DustType<AstralBasic>();
		base.HitSound = SoundID.Tink;
		AddMapEntry(new Color(93, 78, 107));
		TileID.Sets.Stone[base.Type] = true;
		TileID.Sets.Conversion.Stone[base.Type] = true;
		TileID.Sets.CanBeClearedDuringOreRunner[base.Type] = true;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
