using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Astral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.AstralDesert;

[LegacyName(new string[] { "AstralFossil" })]
public class CelestialRemains : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		base.DustType = ModContent.DustType<AstralBasic>();
		AddMapEntry(new Color(59, 50, 77), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.Astral.CelestialRemains>());
		TileID.Sets.ForAdvancedCollision.ForSandshark[base.Type] = true;
		this.RegisterBlendMergeWith(ModContent.TileType<AstralSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<AstralSandstone>());
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
