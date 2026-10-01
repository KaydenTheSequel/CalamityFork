using CalamityMod.Items.Placeables.FurnitureSacrilegious;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureSacrilegious;

public class LargeRitualCandleTile : ModTile
{
	public Asset<Texture2D> FlameTexture;

	public override void SetStaticDefaults()
	{
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		RegisterItemDrop(ModContent.ItemType<LargeRitualCandle>());
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Height = 6;
		TileObjectData.newTile.CoordinateHeights = new int[6] { 16, 16, 16, 16, 16, 16 };
		TileObjectData.newTile.Origin = new Point16(0, 4);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 2;
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.addTile(base.Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(43, 19, 42), CalamityUtils.GetItemName<LargeRitualCandle>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.AdjTiles = new int[1] { 93 };
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 60, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 8, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 36)
		{
			r = 3f;
			g = 0.6f;
			b = 0.6f;
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
		FurnitureCommon.LightHitWire(base.Type, i, j, 2, 6);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (FlameTexture == null)
		{
			FlameTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureSacrilegious/LargeRitualCandleTileFlame", (AssetRequestMode)2);
		}
		CalamityUtils.DrawFlameEffect(FlameTexture.Value, i, j);
	}
}
