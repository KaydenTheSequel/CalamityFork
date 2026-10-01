using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

[LegacyName(new string[] { "AstralSilt" })]
public class NovaeSlag : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		CalamityUtils.MergeWithOres(base.Type);
		base.DustType = ModContent.DustType<AstralBasic>();
		AddMapEntry(new Color(133, 69, 115));
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeClearedDuringOreRunner[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralBlue>(), 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralOrange>(), 0f, 0f, 1, new Color(255, 255, 255));
		return false;
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
