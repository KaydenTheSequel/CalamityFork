using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss.AbyssAmbient;

public class AbyssGiantKelp1 : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.Origin = new Point16(1, 4);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(92, 93, 42));
		base.DustType = 2;
		base.HitSound = SoundID.Grass;
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

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Framing.GetTileSafely(i, j).TileFrameY <= 36)
		{
			r = 0.72f;
			g = 0.35f;
			b = 0.08f;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
