using System;
using CalamityMod.ExtraTextures.GreyscaleGradients;
using CalamityMod.Systems;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.AstralSnow;

public class AstralSnow : GlowMaskTile
{
	public override string GlowMaskAsset => Texture + "Lightmask";

	public override void SetupStatic()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileBrick[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Snow"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithSnow(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		base.DustType = 173;
		base.HitSound = SoundID.Item48;
		AddMapEntry(new Color(189, 211, 221));
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.Snow[base.Type] = true;
		TileID.Sets.Conversion.Snow[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeClearedDuringOreRunner[base.Type] = true;
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
		this.RegisterBlendMergeWith(147);
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

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		int time = (int)(Main.timeForVisualEffects * 0.11);
		float num = 1f - GreyscaleGradient.BlobbyNoise.GetRepeat(i * 60 + time, j * 50 + time) + MathF.Sin((float)(-j) / 1f + (float)Main.GameUpdateCount * 0.02f + (float)(-i) / 30f) - MathF.Sin((float)j / 8f + (float)Main.GameUpdateCount * 0.02f - (float)i / 11f) - MathF.Sin((float)j / 1f + (float)Main.GameUpdateCount * 0.01f - (float)i / 2f) - MathF.Sin((float)(-j) / 2f + (float)Main.GameUpdateCount * 0.03f - (float)i / 4f) + MathF.Sin((float)(-j) / 4f + (float)Main.GameUpdateCount * 0.03f - (float)i / 8f);
		return new Color(num, num, num);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
