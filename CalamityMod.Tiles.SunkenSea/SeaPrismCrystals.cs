using System;
using CalamityMod.Items.Placeables.SunkenSea;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class SeaPrismCrystals : ModTile
{
	internal static Asset<Texture2D> BlueCrystals;

	internal static Asset<Texture2D> PurpleCrystals;

	internal static Asset<Texture2D> GreenCrystals;

	internal static Asset<Texture2D> Glint;

	public override void SetStaticDefaults()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(53, 136, 207), CalamityUtils.GetItemName<PrismShard>());
		base.HitSound = SoundID.Item27;
		base.DustType = 67;
		Main.tileSpelunker[base.Type] = true;
		base.MinPick = 55;
		BlueCrystals = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrismCrystals", (AssetRequestMode)2);
		PurpleCrystals = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrismCrystals_Purple", (AssetRequestMode)2);
		GreenCrystals = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrismCrystals_Green", (AssetRequestMode)2);
		Glint = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/SeaPrismCrystals_Glint", (AssetRequestMode)2);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		float fade1 = GetFade1(i, j);
		float fade2 = GetFade2(i, j);
		Color baseColor = default(Color);
		((Color)(ref baseColor))._002Ector(162, 216, 218);
		Color glow1 = default(Color);
		((Color)(ref glow1))._002Ector(171, 113, 215);
		Color glow2 = default(Color);
		((Color)(ref glow2))._002Ector(56, 174, 117);
		Vector3 blended = ((Color)(ref baseColor)).ToVector3();
		blended = Vector3.Lerp(blended, ((Color)(ref glow1)).ToVector3(), fade1 * 0.5f);
		blended = Vector3.Lerp(blended, ((Color)(ref glow2)).ToVector3(), fade2 * 0.5f);
		float brightness = 0.6f;
		blended *= brightness;
		r = blended.X;
		g = blended.Y;
		b = blended.Z;
	}

	private static float GetFade1(int i, int j)
	{
		return (MathF.Sin(Main.GlobalTimeWrappedHourly * 0.2f) + 1f) / 2f;
	}

	private static float GetFade2(int i, int j)
	{
		return (MathF.Sin(Main.GlobalTimeWrappedHourly * 0.1f + (float)i * 0.08f - (float)j * 0.05f) + 1f) / 2f;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return false;
	}

	public override bool CanPlace(int i, int j)
	{
		Tile belowTile = Main.tile[i, j + 1];
		Tile aboveTile = Main.tile[i, j - 1];
		Tile rightTile = Main.tile[i + 1, j];
		Tile leftTile = Main.tile[i - 1, j];
		if ((belowTile.Slope == SlopeType.Solid && !belowTile.IsHalfBlock && belowTile.HasTile && belowTile.IsTileSolid()) || (aboveTile.Slope == SlopeType.Solid && !aboveTile.IsHalfBlock && aboveTile.HasTile && aboveTile.IsTileSolid()) || (rightTile.Slope == SlopeType.Solid && !rightTile.IsHalfBlock && rightTile.HasTile && rightTile.IsTileSolid()) || (leftTile.Slope == SlopeType.Solid && !leftTile.IsHalfBlock && leftTile.HasTile && leftTile.IsTileSolid()))
		{
			return true;
		}
		return false;
	}

	public override void PlaceInWorld(int i, int j, Item item)
	{
		Tile belowTile = Main.tile[i, j + 1];
		Tile aboveTile = Main.tile[i, j - 1];
		Tile rightTile = Main.tile[i + 1, j];
		Tile leftTile = Main.tile[i - 1, j];
		if (belowTile.Slope == SlopeType.Solid && !belowTile.IsHalfBlock && belowTile.HasTile && belowTile.IsTileSolid())
		{
			Main.tile[i, j].TileFrameY = 0;
		}
		else if (aboveTile.Slope == SlopeType.Solid && !aboveTile.IsHalfBlock && aboveTile.HasTile && aboveTile.IsTileSolid())
		{
			Main.tile[i, j].TileFrameY = 18;
		}
		else if (rightTile.Slope == SlopeType.Solid && !rightTile.IsHalfBlock && rightTile.HasTile && rightTile.IsTileSolid())
		{
			Main.tile[i, j].TileFrameY = 36;
		}
		else if (leftTile.Slope == SlopeType.Solid && !leftTile.IsHalfBlock && leftTile.HasTile && leftTile.IsTileSolid())
		{
			Main.tile[i, j].TileFrameY = 54;
		}
		Main.tile[i, j].TileFrameX = (short)(WorldGen.genRand.Next(18) * 18);
	}
}
