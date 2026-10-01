using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureCosmilite;

public class CosmiliteBasinTile : ModTile
{
	public Asset<Texture2D> FlameTexture;

	private int animationFrame;

	public override void SetStaticDefaults()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetText("Tiles.Basin"));
		base.AnimationFrameHeight = 54;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 132, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 134, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= 6)
		{
			frame = (frame + 1) % 6;
			animationFrame = frame;
			frameCounter = 0;
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 54)
		{
			r = 1f;
			g = 0.6f;
			b = 1f;
		}
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 3, 3);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (FlameTexture == null)
		{
			FlameTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureCosmilite/CosmiliteBasinFlame", (AssetRequestMode)2);
		}
		CalamityUtils.DrawStaticFlameEffect(FlameTexture.Value, i, j, 0, animationFrame * base.AnimationFrameHeight);
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameY == 18 && tile.TileFrameX < 54)
		{
			CalamityUtils.DrawFlameSparks(173, 5, i, j);
		}
	}
}
