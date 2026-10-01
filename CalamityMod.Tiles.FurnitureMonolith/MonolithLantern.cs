using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureMonolith;

public class MonolithLantern : ModTile
{
	public Asset<Texture2D> FlameTexture;

	public Asset<Texture2D> GlowTexture;

	public override void Load()
	{
		FlameTexture = ModContent.Request<Texture2D>(Texture + "Flame", (AssetRequestMode)2);
		GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		this.SetUpLantern(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureMonolith.MonolithLantern>(), lavaImmune: true);
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return CalamityUtils.DrawSwayingMultiTile(i, j);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralBasic>(), 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AdjustMultiTileVineParameters(int i, int j, ref float? overrideWindCycle, ref float windPushPowerX, ref float windPushPowerY, ref bool dontRotateTopTiles, ref float totalWindMultiplier, ref Texture2D glowTexture, ref Color glowColor)
	{
		glowTexture = GlowTexture.Value;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 0.8f;
			g = 0.9f;
			b = 1f;
		}
		else
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
	}

	public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		ulong flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);
		tileFlameData.flameSeed = flameSeed;
		tileFlameData.flameTexture = FlameTexture.Value;
		tileFlameData.flameColor = new Color(102, 115, 128, 0);
		tileFlameData.flameCount = 1;
		tileFlameData.flameRangeXMin = -10;
		tileFlameData.flameRangeXMax = 11;
		tileFlameData.flameRangeYMin = -10;
		tileFlameData.flameRangeYMax = 11;
		tileFlameData.flameRangeMultX = 0f;
		tileFlameData.flameRangeMultY = 0f;
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 1, 2);
	}
}
