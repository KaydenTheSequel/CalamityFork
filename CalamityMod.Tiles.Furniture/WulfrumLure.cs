using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Tools;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture;

public class WulfrumLure : ModTile
{
	public const int Width = 2;

	public const int Height = 2;

	public Asset<Texture2D> CogTexture;

	public Asset<Texture2D> CoverTexture;

	public override void SetStaticDefaults()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(194, 255, 67), CalamityUtils.GetItemName<WulfrumLureItem>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.DustType = 83;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		int left = i - tile.TileFrameX / 18;
		int top = j - tile.TileFrameY / 18;
		if (!Main.LocalPlayer.HasItem(ModContent.ItemType<EnergyCore>()))
		{
			return true;
		}
		if (Main.projectile.Any(delegate(Projectile p)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			if (p.active && p.type == ModContent.ProjectileType<WulfrumLureSignal>())
			{
				Vector2 val = p.Center - Main.LocalPlayer.Center;
				return ((Vector2)(ref val)).Length() < 2000f;
			}
			return false;
		}))
		{
			return true;
		}
		Vector2 lurePosition = Utils.ToWorldCoordinates(new Vector2((float)(left + 1), (float)top), 8f, 8f);
		lurePosition += new Vector2(0f, -24f);
		SoundEngine.PlaySound(in WulfrumTreasurePinger.ScanBeepSound, lurePosition);
		Projectile.NewProjectile(new EntitySource_WorldEvent(), lurePosition, Vector2.Zero, ModContent.ProjectileType<WulfrumLureSignal>(), 0, 0f, Main.myPlayer);
		Main.LocalPlayer.ConsumeItem(ModContent.ItemType<EnergyCore>(), reverseOrder: true);
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<EnergyCore>();
		Main.LocalPlayer.noThrow = 2;
		Main.LocalPlayer.cursorItemIconEnabled = true;
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (drawData.tileFrameX % 36 == 0 && drawData.tileFrameY % 36 == 0)
		{
			Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
		}
	}

	public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].IsTileActuallyInvisible())
		{
			return;
		}
		Vector2 offScreen = default(Vector2);
		((Vector2)(ref offScreen))._002Ector((float)Main.offScreenRange);
		if (Main.drawToScreen)
		{
			offScreen = Vector2.Zero;
		}
		Point p = default(Point);
		((Point)(ref p))._002Ector(i, j);
		Tile tile = Main.tile[p.X, p.Y];
		if (!(tile == null) && tile.HasTile)
		{
			if (CogTexture == null)
			{
				CogTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Furniture/WulfrumLureCog", (AssetRequestMode)2);
			}
			Texture2D cogTexture = CogTexture.Value;
			if (CoverTexture == null)
			{
				CoverTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Furniture/WulfrumLureCover", (AssetRequestMode)2);
			}
			Texture2D coverTex = CoverTexture.Value;
			Vector2 val = p.ToWorldCoordinates(16f, 16f);
			Color color = Lighting.GetColor(p.X, p.Y);
			bool direction = tile.TileFrameY / 32 != 0;
			SpriteEffects effects = (SpriteEffects)(direction ? 1 : 0);
			Vector2 drawPos = val + offScreen - Main.screenPosition;
			spriteBatch.Draw(cogTexture, drawPos, (Rectangle?)null, color, Main.GlobalTimeWrappedHourly * 1.5f * (float)((!direction) ? 1 : (-1)), cogTexture.Size() / 2f, 1f, effects, 0f);
			spriteBatch.Draw(coverTex, p.ToWorldCoordinates(0f, 0f) + Vector2.UnitY * 2f + offScreen - Main.screenPosition, (Rectangle?)null, color, 0f, Vector2.Zero, 1f, effects, 0f);
		}
	}
}
