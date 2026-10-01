using System;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class SeaPrism : ModTile
{
	internal const short subsheetWidth = 468;

	internal const short subsheetHeight = 90;

	internal static Asset<Texture2D> Blue;

	internal static Asset<Texture2D> Purple;

	internal static Asset<Texture2D> Green;

	internal static Asset<Texture2D> Glint;

	public override void SetStaticDefaults()
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = false;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		Main.tileMerge[base.Type][ModContent.TileType<MediumSeaPrismCrystal>()] = true;
		Main.tileMerge[base.Type][ModContent.TileType<SeaPrismCrystals>()] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		Main.tileLighted[base.Type] = true;
		Main.tileShine2[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.DustType = 33;
		AddMapEntry(new Color(97, 212, 223));
		base.HitSound = SoundID.Tink;
		Main.tileSpelunker[base.Type] = true;
		base.MinPick = 55;
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		Blue = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrism_Blue", (AssetRequestMode)2);
		Purple = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrism_Purple", (AssetRequestMode)2);
		Green = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrism_Green", (AssetRequestMode)2);
		Glint = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrism_GlintMask", (AssetRequestMode)2);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 8 * 468;
		frameYOffset = j % 8 * 90;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		float fade1 = GetFade1(i, j);
		float fade2 = GetFade2(i, j);
		Color baseColor = default(Color);
		((Color)(ref baseColor))._002Ector(162, 216, 218);
		Color glow1 = default(Color);
		((Color)(ref glow1))._002Ector(171, 113, 215);
		Color glow2 = default(Color);
		((Color)(ref glow2))._002Ector(56, 174, 117);
		Vector3 blended = ((Color)(ref baseColor)).ToVector3();
		blended = Vector3.Lerp(blended, ((Color)(ref glow1)).ToVector3(), fade1 * 0.5f);
		blended = Vector3.Lerp(blended, ((Color)(ref glow2)).ToVector3(), fade2 * 0.5f);
		float brightness = 0.6f;
		blended *= brightness;
		r = blended.X;
		g = blended.Y;
		b = blended.Z;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	internal static float GetFade1(int i, int j)
	{
		return (MathF.Sin(Main.GlobalTimeWrappedHourly * 0.2f) + 1f) / 2f;
	}

	internal static float GetFade2(int i, int j)
	{
		return (MathF.Sin(Main.GlobalTimeWrappedHourly * 0.1f + (float)i * 0.08f - (float)j * 0.05f) + 1f) / 2f;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return false;
	}
}
