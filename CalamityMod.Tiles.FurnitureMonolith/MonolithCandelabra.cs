using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureMonolith;

public class MonolithCandelabra : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public Asset<Texture2D> FlameTexture;

	public override void SetStaticDefaults()
	{
		this.SetUpCandelabra(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureMonolith.MonolithCandelabra>(), lavaImmune: true);
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

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			int xPos = Main.tile[i, j].TileFrameX;
			int yPos = Main.tile[i, j].TileFrameY;
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureMonolith/MonolithCandelabraGlow", (AssetRequestMode)2);
			}
			Color drawColour = CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, new Color(100, 100, 100, 100));
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
			Main.spriteBatch.Draw(GlowTexture.Value, drawOffset, (Rectangle?)new Rectangle(xPos, yPos, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			if (FlameTexture == null)
			{
				FlameTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureMonolith/MonolithCandelabraFlame", (AssetRequestMode)2);
			}
			Main.spriteBatch.Draw(FlameTexture.Value, drawOffset, (Rectangle?)new Rectangle(xPos, yPos, 18, 18), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
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

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 2, 2);
	}
}
