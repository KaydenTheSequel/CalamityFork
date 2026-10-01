using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class VolcanicSand : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		base.DustType = 78;
		AddMapEntry(new Color(102, 101, 106));
		Main.tileSand[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		TileID.Sets.Suffocate[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.Conversion.Sand[base.Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[base.Type] = true;
		TileID.Sets.Falling[base.Type] = true;
		TileID.Sets.FallingBlockProjectile[base.Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<VolcanicSandBallFalling>());
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Runestone>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(57);
		this.RegisterBlendMergeWith(59);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void RandomUpdate(int i, int j)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Dust obj = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 16, 16, 31, 0f, -1.9069767f, 195, new Color(255, 255, 255))];
		obj.noGravity = false;
		obj.fadeIn = 1.4209301f;
	}
}
