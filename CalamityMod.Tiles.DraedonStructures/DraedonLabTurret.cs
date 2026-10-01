using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures;

public class DraedonLabTurret : ModTile
{
	public const int Width = 3;

	public const int Height = 2;

	public const int OriginOffsetX = 1;

	public const int OriginOffsetY = 1;

	public const int SheetSquare = 18;

	public Asset<Texture2D> GlowTexture;

	public override string Texture => "CalamityMod/Tiles/PlayerTurrets/PlayerLabTurret";

	public override void SetStaticDefaults()
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileID.Sets.PreventsTileRemovalIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileHammeringIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileReplaceIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.LavaDeath = false;
		ModTileEntity te = ModContent.GetInstance<TEHostileLabTurret>();
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(te.Hook_AfterPlacement, -1, 0, processedCoordinates: true);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(67, 72, 81), Language.GetText("MapObject.Turret"));
		base.HitSound = SoundID.Item14;
		base.MineResist = 5f;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 226);
		return false;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Tile t = Main.tile[i, j];
		int left = i - t.TileFrameX % 54 / 18;
		int top = j - t.TileFrameY % 36 / 18;
		CalamityUtils.FindTileEntity<TEHostileLabTurret>(i, j, 3, 2, 18)?.Kill(left, top);
	}

	public override bool IsTileDangerous(int i, int j, Player player)
	{
		return true;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		Tile t = Main.tile[i, j];
		if (t.TileFrameX != 36 || t.TileFrameY != 0 || t.IsTileActuallyInvisible())
		{
			return;
		}
		TEHostileLabTurret te = CalamityUtils.FindTileEntity<TEHostileLabTurret>(i, j, 3, 2, 18);
		if (te == null)
		{
			return;
		}
		int drawDirection = te.Direction;
		Color drawColor = Lighting.GetColor(i, j);
		if (Main.LocalPlayer.dangerSense)
		{
			if (((Color)(ref drawColor)).R < byte.MaxValue)
			{
				((Color)(ref drawColor)).R = byte.MaxValue;
			}
			if (((Color)(ref drawColor)).G < 50)
			{
				((Color)(ref drawColor)).G = 50;
			}
			if (((Color)(ref drawColor)).B < 50)
			{
				((Color)(ref drawColor)).B = 50;
			}
		}
		if (GlowTexture == null)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PulseTurret", (AssetRequestMode)2);
		}
		Texture2D tex = GlowTexture.Value;
		Vector2 screenOffset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + screenOffset;
		drawOffset.Y -= 2f;
		drawOffset.X += ((drawDirection == -1) ? (-10f) : 2f);
		SpriteEffects sfx = (SpriteEffects)((drawDirection == -1) ? 2 : 0);
		spriteBatch.Draw(tex, drawOffset, (Rectangle?)null, drawColor, te.Angle, tex.Size() * 0.5f, 1f, sfx, 0f);
	}

	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		for (int itemCount = 0; itemCount < 8; itemCount++)
		{
			yield return new Item(ModContent.ItemType<DubiousPlating>());
			yield return new Item(ModContent.ItemType<MysteriousCircuitry>());
		}
	}
}
