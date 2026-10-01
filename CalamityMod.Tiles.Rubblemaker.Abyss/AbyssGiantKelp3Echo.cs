using CalamityMod.Items.Placeables.Abyss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.Abyss;

public class AbyssGiantKelp3Echo : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override string Texture => "CalamityMod/Tiles/Abyss/AbyssAmbient/AbyssGiantKelp3";

	public override void SetStaticDefaults()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.Origin = new Point16(1, 3);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(92, 93, 42));
		base.DustType = 2;
		base.HitSound = SoundID.Grass;
		RegisterItemDrop(ModContent.ItemType<PlantyMush>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<PlantyMush>(), base.Type, default(int));
		base.SetStaticDefaults();
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Framing.GetTileSafely(i, j).TileFrameY <= 36)
		{
			r = 0.46f;
			g = 0.225f;
			b = 0.05f;
		}
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (closer && Main.rand.NextBool(100) && (double)j > Main.worldSurface)
		{
			Dust obj = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 280, 280, 304, 0.2f, 0f, 0, new Color(157, 175, 15), Main.rand.NextFloat(1f, 2f))];
			obj.noGravity = true;
			obj.noLight = true;
			obj.fadeIn = 2.5f;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Framing.GetTileSafely(i, j);
		if (!tile.IsTileActuallyInvisible())
		{
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Abyss/AbyssAmbient/AbyssGiantKelp3Glow", (AssetRequestMode)2);
			}
			Texture2D tex = GlowTexture.Value;
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
			spriteBatch.Draw(tex, new Vector2((float)(i * 16), (float)(j * 16 + 2)) - Main.screenPosition + zero, (Rectangle?)new Rectangle((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16), Color.White);
		}
	}
}
