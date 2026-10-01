using System;
using CalamityMod.Items.Placeables.SunkenSea;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea;

public class MediumSeaPrismCrystal : ModTile
{
	internal static Asset<Texture2D> BlueCrystals;

	internal static Asset<Texture2D> PurpleCrystals;

	internal static Asset<Texture2D> GreenCrystals;

	internal static Asset<Texture2D> Glint;

	public override void SetStaticDefaults()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		Main.tileSpelunker[base.Type] = true;
		Main.tileShine[base.Type] = 5600;
		Main.tileShine2[base.Type] = true;
		base.HitSound = SoundID.Item27;
		base.DustType = 67;
		base.MinPick = 55;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(53, 136, 207), CalamityUtils.GetItemName<PrismShard>());
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 32;
		TileObjectData.newTile.StyleWrapLimit = 8;
		TileObjectData.newTile.RandomStyleRange = 8;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(1, 0);
		TileObjectData.addAlternate(8);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(16);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
		TileObjectData.newAlternate.Origin = new Point16(0, 0);
		TileObjectData.addAlternate(24);
		TileObjectData.addTile(base.Type);
		BlueCrystals = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/MediumSeaPrismCrystal_Blue", (AssetRequestMode)2);
		PurpleCrystals = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/MediumSeaPrismCrystal_Purple", (AssetRequestMode)2);
		GreenCrystals = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/MediumSeaPrismCrystal_Green", (AssetRequestMode)2);
		Glint = ModContent.Request<Texture2D>("CalamityMod/Tiles/SunkenSea/MediumSeaPrismCrystal_Glint", (AssetRequestMode)2);
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

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<PrismShard>(), 4);
	}
}
