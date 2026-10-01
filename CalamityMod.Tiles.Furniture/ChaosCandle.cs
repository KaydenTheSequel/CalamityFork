using CalamityMod.Buffs.Placeables;
using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Furniture;

public class ChaosCandle : ModTile
{
	public Asset<Texture2D> FlameTexture;

	public override void SetStaticDefaults()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpCandle(ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.ChaosCandle>(), lavaImmune: false, autoMapEntry: false);
		AddMapEntry(new Color(238, 145, 105), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.Furniture.ChaosCandle>());
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.ChaosCandle>();
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		Player player = Main.LocalPlayer;
		if (player != null && !player.dead && player.active && Main.tile[i, j].TileFrameX < 18)
		{
			player.AddBuff(ModContent.BuffType<ChaosCandleBuff>(), 20);
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 0.85f;
			g = 0.25f;
			b = 0.25f;
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

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			CalamityUtils.DrawFlameSparks(235, 5, i, j);
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (FlameTexture == null)
		{
			FlameTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Furniture/ChaosCandleFlame", (AssetRequestMode)2);
		}
		CalamityUtils.DrawFlameEffect(FlameTexture.Value, i, j);
	}

	public override bool RightClick(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 1, 1);
		return true;
	}
}
