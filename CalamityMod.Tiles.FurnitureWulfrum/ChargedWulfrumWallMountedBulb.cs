using CalamityMod.Items.Placeables.FurnitureWulfrum;
using CalamityMod.Sounds;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureWulfrum;

public class ChargedWulfrumWallMountedBulb : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileObsidianKill[base.Type] = false;
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.DustType = 299;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(48, 201, 214), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureWulfrum.ChargedWulfrumWallMountedBulb>());
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 10;
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(1, 0);
		TileObjectData.addAlternate(2);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(4);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(6);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorWall = true;
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(1, 0);
		TileObjectData.addAlternate(8);
		TileObjectData.addTile(base.Type);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 2, 2);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 28f / 85f;
			g = 0.83137256f;
			b = 0.9137255f;
		}
		else
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureWulfrum/ChargedWulfrumWallMountedBulb_Glow", (AssetRequestMode)2);
			}
			Texture2D tex = GlowTexture.Value;
			Tile tile = Main.tile[i, j];
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16);
			TileFramingSystem.SlopedGlowmask(in tile, i, j, tex, frame, CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, Color.White, deepPaintOnly: false), default(Vector2));
		}
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureWulfrum.ChargedWulfrumWallMountedBulb>());
	}
}
