using CalamityMod.ExtraTextures.GreyscaleGradients;
using CalamityMod.Items.Placeables.DraedonStructures.CagedLights;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures.CagedLights;

public class AgedFrostlight : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileObsidianKill[base.Type] = false;
		RegisterItemDrop(ModContent.ItemType<AgedFrostlightItem>());
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.DustType = 247;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(48, 201, 214), CalamityUtils.GetItemName<CagedLablightItem>());
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

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		float brightness = GreyscaleGradient.ElumplatePulse.GetRepeat((int)Main.GameUpdateCount);
		brightness = MathHelper.Clamp(brightness, 0.2f, 0.6f);
		Lighting.AddLight(new Vector2((float)(i * 16), (float)(j * 16)), 3f / 85f * brightness, 0.61960787f * brightness, 14f / 15f * brightness);
	}
}
