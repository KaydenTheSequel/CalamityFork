using CalamityMod.Dusts.Furniture;
using CalamityMod.Items.Placeables.FurnitureProfaned;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureProfaned;

public class ProfanedCandle : ModTile
{
	public Asset<Texture2D> FlameTexture;

	public override void SetStaticDefaults()
	{
		this.SetUpCandle(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureProfaned.ProfanedCandle>(), lavaImmune: true);
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

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 1, 1);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureProfaned.ProfanedCandle>();
	}

	public override bool RightClick(int i, int j)
	{
		FurnitureCommon.RightClickBreak(i, j);
		return true;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (FlameTexture == null)
		{
			FlameTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureProfaned/ProfanedCandleFlame", (AssetRequestMode)2);
		}
		CalamityUtils.DrawFlameEffect(FlameTexture.Value, i, j);
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			CalamityUtils.DrawFlameSparks(246, 5, i, j);
		}
	}
}
