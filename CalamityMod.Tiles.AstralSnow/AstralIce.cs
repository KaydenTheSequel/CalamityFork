using System;
using CalamityMod.Systems;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.AstralSnow;

public class AstralIce : GlowMaskTile
{
	public override string GlowMaskAsset => Texture + "Lightmask";

	public override void SetupStatic()
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = false;
		Main.tileBrick[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Ice"]);
		Main.tileLighted[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithSnow(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		base.DustType = 173;
		base.HitSound = SoundID.Item50;
		AddMapEntry(new Color(153, 143, 168));
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.Ices[base.Type] = true;
		TileID.Sets.IcesSlush[base.Type] = true;
		TileID.Sets.IcesSnow[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.Conversion.Ice[base.Type] = true;
		TileID.Sets.CanBeClearedDuringOreRunner[base.Type] = true;
		this.RegisterBlendMergeWith(ModContent.TileType<AstralSnow>());
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void FloorVisuals(Player player)
	{
		player.slippy = true;
		base.FloorVisuals(player);
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
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.timeForVisualEffects;
		float num = 0.2f + MathF.Sin((float)(-j) / 1f + (float)Main.GameUpdateCount * 0.002f + (float)(-i) / 30f) - MathF.Sin((float)j / 8f + (float)Main.GameUpdateCount * 0.002f - (float)i / 11f) - MathF.Sin((float)j / 1f + (float)Main.GameUpdateCount * 0.001f - (float)i / 2f) - MathF.Sin((float)(-j) / 2f + (float)Main.GameUpdateCount * 0.003f - (float)i / 4f) + MathF.Sin((float)(-j) / 4f + (float)Main.GameUpdateCount * 0.003f - (float)i / 8f);
		return new Color(num, num, num);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		float brightness = 0.2f;
		brightness *= MathF.Sin((float)(-j) / 40f + (float)Main.GameUpdateCount * 0.005f + (float)i / 8f);
		r = 0.08733334f;
		g = 0.074f;
		b = 0.114f;
		brightness += MathF.Sin((float)(-j) / 1f + (float)Main.GameUpdateCount * 0.002f + (float)(-i) / 30f);
		brightness -= MathF.Sin((float)j / 8f + (float)Main.GameUpdateCount * 0.002f - (float)i / 11f);
		brightness -= MathF.Sin((float)j / 1f + (float)Main.GameUpdateCount * 0.001f - (float)i / 2f);
		brightness -= MathF.Sin((float)(-j) / 2f + (float)Main.GameUpdateCount * 0.003f - (float)i / 4f);
		brightness += MathF.Sin((float)(-j) / 4f + (float)Main.GameUpdateCount * 0.003f - (float)i / 8f);
		brightness -= 0.05f;
		r *= brightness;
		g *= brightness;
		b *= brightness;
	}
}
