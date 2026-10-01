using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.AstralDesert;

public class HardenedAstralSand : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		base.DustType = 108;
		AddMapEntry(new Color(128, 128, 158));
		TileID.Sets.Conversion.HardenedSand[base.Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[base.Type] = true;
		this.RegisterBlendMergeWith(ModContent.TileType<AstralSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<AstralSandstone>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(397);
		this.RegisterBlendMergeWith(53);
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
