using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Particles;
using CalamityMod.Tiles.BaseTiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.Monoliths;

public class PlagueHumidifierTile : BaseMonolith
{
	public override int TileWidth => 2;

	public override int TileHeight => 4;

	public override int AnimationFrameCount => 4;

	public override int AnimationDelay => 8;

	public override int CursorItemType => ModContent.ItemType<PlagueHumidifier>();

	public override void SetStaticDefaults()
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		RegisterItemDrop(ModContent.ItemType<PlagueHumidifier>());
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.Origin = new Point16(1, 3);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 18 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 2, 0);
		base.AnimationFrameHeight = TileObjectData.newTile.CoordinateFullHeight;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(44, 150, 54));
		base.DustType = 46;
	}

	public override void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		if (monolithEnabled && localPlayer != null && localPlayer.active)
		{
			localPlayer.Calamity().monolithPlagueShader = 30;
			if (Main.tile[i, j + 1].TileType != base.Type && Main.rand.NextBool(30))
			{
				Vector2 relativePosition = new Vector2((float)(i * 16), (float)(j * 16)) + new Vector2(Main.rand.NextFloat(0f, 16f), Main.rand.NextFloat(16f));
				int lifeTime = Main.rand.Next(40, 80);
				float size = Main.rand.NextFloat(0.8f, 1.2f);
				Vector2 speed = Vector2.UnitX * Main.rand.NextFloat(-0.8f, 0.8f);
				GeneralParticleHandler.SpawnParticle(new PlagueHumidifierMist(relativePosition, lifeTime, size, speed));
			}
		}
	}
}
