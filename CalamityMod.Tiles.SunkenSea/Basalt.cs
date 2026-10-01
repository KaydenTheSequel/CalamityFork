using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class Basalt : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileShine2[base.Type] = false;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		base.DustType = 36;
		base.HitSound = SoundID.Tink;
		AddMapEntry(new Color(77, 75, 86));
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
