using CalamityMod.Tiles.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureBasalt;

public class BasaltSlab : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileShine2[base.Type] = true;
		Main.tileBlendAll[base.Type] = true;
		Main.tileLargeFrames[base.Type] = 1;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		base.DustType = 36;
		base.HitSound = SoundID.Tink;
		AddMapEntry(new Color(58, 55, 70));
		base.MinPick = 110;
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Runestone>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<VolcanicSand>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(57);
		this.RegisterBlendMergeWith(59);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}
}
