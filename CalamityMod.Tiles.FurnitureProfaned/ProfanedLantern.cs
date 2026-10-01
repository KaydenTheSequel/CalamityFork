using CalamityMod.Dusts.Furniture;
using CalamityMod.Items.Placeables.FurnitureProfaned;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureProfaned;

public class ProfanedLantern : ModTile
{
	public Asset<Texture2D> FlameTexture;

	public override void Load()
	{
		FlameTexture = ModContent.Request<Texture2D>(Texture + "Flame", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		this.SetUpLantern(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureProfaned.ProfanedLantern>(), lavaImmune: true);
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
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 246, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<ProfanedTileRock>(), 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 1f;
			g = 0.85f;
			b = 0.7f;
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
		tileFlameData.flameColor = new Color(128, 108, 90, 0);
		tileFlameData.flameCount = 3;
		tileFlameData.flameRangeXMin = -10;
		tileFlameData.flameRangeXMax = 11;
		tileFlameData.flameRangeYMin = -10;
		tileFlameData.flameRangeYMax = 11;
		tileFlameData.flameRangeMultX = 0.1f;
		tileFlameData.flameRangeMultY = 0.1f;
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 1, 2);
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			CalamityUtils.DrawFlameSparks(246, 5, i, j);
		}
	}
}
