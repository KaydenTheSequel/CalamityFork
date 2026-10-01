using System;
using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using CalamityMod.Particles;
using CalamityMod.Systems.Graphic.LiquidSystem;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class SulphuricWater : ModWaterStyle, IWaterStyleModifyColor, IWaterStyleModifyLight, IWaterStylePostDrawEffect
{
	public static ModWaterStyle Instance { get; private set; }

	public static ModWaterfallStyle WaterfallStyle { get; private set; }

	public static int SplashDust { get; private set; }

	public static int DropletGore { get; private set; }

	public static Asset<Texture2D> RainTexture { get; private set; }

	public override void SetStaticDefaults()
	{
		Instance = this;
		WaterfallStyle = ModContent.Find<ModWaterfallStyle>("CalamityMod/SulphuricWaterflow");
		SplashDust = ModContent.DustType<SulphuricSplash>();
		DropletGore = ModContent.GoreType<SulphuricWaterDroplet>();
	}

	public override void Unload()
	{
		Instance = null;
		WaterfallStyle = null;
		SplashDust = 0;
		DropletGore = 0;
		RainTexture = null;
	}

	public override int ChooseWaterfallStyle()
	{
		return WaterfallStyle.Slot;
	}

	public override int GetSplashDust()
	{
		return SplashDust;
	}

	public override int GetDropletGore()
	{
		return DropletGore;
	}

	public override Asset<Texture2D> GetRainTexture()
	{
		return RainTexture ?? (RainTexture = ModContent.Request<Texture2D>("CalamityMod/Waters/SulphuricRain", (AssetRequestMode)2));
	}

	public override byte GetRainVariant()
	{
		return (byte)Main.rand.Next(3);
	}

	public override Color BiomeHairColor()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		return new Color(43, 168, 110);
	}

	public void ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		WaterStyleCommon.ModifySulphuricWaterColor(x, y, ref liquidColor, isSlope);
	}

	public void ModifyLight(in Tile tile, int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 outputColor = default(Vector3);
		((Vector3)(ref outputColor))._002Ector(r, g, b);
		if (tile.TileType != RustyChestTile.TileType)
		{
			Color lightSeaGreen;
			if (Main.dayTime && !Main.raining)
			{
				float brightness = MathHelper.Clamp(0.2f - (float)(j / 680), 0f, 0.2f);
				if (j > 580)
				{
					brightness *= 1f - (float)(j - 580) / 100f;
				}
				float num = Main.GameUpdateCount;
				float waveScale1 = num * 0.014f;
				float num2 = num * 0.1f;
				int scalar = i + -j / 2;
				float wave1 = waveScale1 * -50f + (float)(scalar * 15);
				float wave2 = num2 * -10f + (float)(scalar * 14);
				float wave3 = waveScale1 * -100f + (float)(scalar * 13);
				float wave4 = num2 * 10f + (float)(scalar * 25);
				float wave5 = waveScale1 * -70f + (float)(scalar * 5);
				float wave1angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave1));
				float wave2angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave2));
				float wave3angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave3));
				float wave4angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave4));
				float wave5angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave5));
				Vector3 val = outputColor;
				lightSeaGreen = Color.LightSeaGreen;
				outputColor = Vector3.Lerp(val, ((Color)(ref lightSeaGreen)).ToVector3(), 0.41f + wave1angle + wave2angle + wave3angle + wave4angle + wave5angle);
				outputColor *= brightness;
			}
			if (!Main.dayTime && !Main.raining)
			{
				float brightness2 = MathHelper.Clamp(0.17f - (float)(j / 680), 0f, 0.17f);
				if (j > 580)
				{
					brightness2 *= 1f - (float)(j - 580) / 100f;
				}
				float num3 = (float)Main.GameUpdateCount * 0.014f;
				float waveScale2 = (float)Main.GameUpdateCount * 0.1f;
				int scalar2 = i + -j / 2;
				float wave6 = num3 * -50f + (float)(scalar2 * 15);
				float wave7 = waveScale2 * -10f + (float)(scalar2 * 14);
				float wave8 = num3 * -100f + (float)(scalar2 * 13);
				float wave9 = waveScale2 * 10f + (float)(scalar2 * 25);
				float wave10 = num3 * -70f + (float)(scalar2 * 5);
				float wave1angle2 = 0.55f + 0.45f * (float)Math.Sin(MathHelper.ToRadians(wave6));
				float wave2angle2 = 0.55f + 0.45f * (float)Math.Sin(MathHelper.ToRadians(wave7));
				float wave3angle2 = 0.55f + 0.45f * (float)Math.Sin(MathHelper.ToRadians(wave8));
				float wave4angle2 = 0.55f + 0.45f * (float)Math.Sin(MathHelper.ToRadians(wave9));
				float wave5angle2 = 0.55f + 0.45f * (float)Math.Sin(MathHelper.ToRadians(wave10));
				Vector3 val2 = outputColor;
				lightSeaGreen = Color.LightSeaGreen;
				outputColor = Vector3.Lerp(val2, ((Color)(ref lightSeaGreen)).ToVector3(), 0.41f + wave1angle2 + wave2angle2 + wave3angle2 + wave4angle2 + wave5angle2);
				outputColor *= brightness2;
			}
			if (Main.raining)
			{
				float brightness3 = MathHelper.Clamp(1f - (float)(j / 680), 0f, 1f);
				if (j > 580)
				{
					brightness3 *= 1f - (float)(j - 580) / 100f;
				}
				Vector3 val3 = outputColor;
				lightSeaGreen = Color.LightSeaGreen;
				outputColor = Vector3.Lerp(val3, ((Color)(ref lightSeaGreen)).ToVector3(), 0.41f);
				outputColor *= brightness3;
			}
		}
		r = outputColor.X;
		g = outputColor.Y;
		b = outputColor.Z;
	}

	public void PostDrawEffect(in Tile tile, int x, int y)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Tile above = CalamityUtils.ParanoidTileRetrieval(x, y - 1);
		if (!Main.gamePaused && !above.HasTile && above.LiquidAmount <= 0 && Main.rand.NextBool(9))
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(new Vector2((float)x * 16f + Main.rand.NextFloat(16f), (float)y * 16f + 8f), -Vector2.UnitY.RotatedByRandom(0.6700000166893005) * Main.rand.NextFloat(1f, 2.4f), Color.LightSeaGreen, Color.White, 0.16f, 128f, 0.02f));
		}
	}

	void IWaterStyleModifyColor.ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		ModifyColor(in tile, x, y, ref liquidColor, isSlope);
	}

	void IWaterStyleModifyLight.ModifyLight(in Tile tile, int x, int y, ref float r, ref float g, ref float b)
	{
		ModifyLight(in tile, x, y, ref r, ref g, ref b);
	}

	void IWaterStylePostDrawEffect.PostDrawEffect(in Tile tile, int x, int y)
	{
		PostDrawEffect(in tile, x, y);
	}
}
