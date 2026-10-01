using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class RefractiveHangingCoral : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Main.tileCut[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoFail[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileNoSunLight[base.Type] = false;
		TileID.Sets.IsVine[base.Type] = true;
		TileID.Sets.VineThreads[base.Type] = true;
		AddMapEntry(new Color(76, 133, 191));
		base.DustType = 2;
		base.HitSound = SoundID.Grass;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		Tile tile = Framing.GetTileSafely(i, j + 1);
		if (tile.HasTile && tile.TileType == base.Type)
		{
			WorldGen.KillTile(i, j + 1);
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tileAbove = Framing.GetTileSafely(i, j - 1);
		int type = -1;
		if (tileAbove.HasTile && !tileAbove.BottomSlope)
		{
			type = tileAbove.TileType;
		}
		if (type == ModContent.TileType<Shellstone>() || type == base.Type)
		{
			return true;
		}
		WorldGen.KillTile(i, j);
		return true;
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (closer && Main.rand.NextBool(300))
		{
			Dust obj = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 280, 280, 304, 0.2f, 0f, 0, Color.Lerp(new Color(0, 76, 255), new Color(76, 0, 255), Main.rand.NextFloat()), Main.rand.NextFloat(1f, 2f))];
			obj.noGravity = true;
			obj.noLight = true;
			obj.fadeIn = 2.5f;
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		float brightness = 0.9f;
		float time = Main.GlobalTimeWrappedHourly * 60f;
		brightness *= MathF.Sin((float)(-j) / 40f + time * 0.01f + (float)i);
		Vector3 val = new Vector3(0.4941f, 0.3686f, 0.9882f);
		Vector3 mint = default(Vector3);
		((Vector3)(ref mint))._002Ector(0.3764f, 0.9882f, 0.7294f);
		Vector3 value = Vector3.Lerp(val, mint, (MathF.Sin((float)j / 30f + time * 0.017f + (float)(-i) / 40f) + 1f) * 0.5f);
		Vector3 value2 = Vector3.Lerp(val, mint, (MathF.Sin((float)(-j - 100) / 40f + time * 0.014f + (float)i / 20f) + 1f) * 0.5f);
		r = (value.X + value2.X) / 450f;
		g = (value.Y + value2.Y) / 450f;
		b = (value.Z + value2.Z) / 450f;
		r *= brightness;
		g *= brightness;
		b *= brightness;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileBelow = Framing.GetTileSafely(i, j + 1);
		if (!WorldGen.genRand.NextBool(5) || tileBelow.HasTile || tileBelow.LiquidType == 1)
		{
			return;
		}
		bool PlaceVine = false;
		int Test = j;
		while (Test > j - 10)
		{
			Tile testTile = Framing.GetTileSafely(i, Test);
			if (testTile.BottomSlope)
			{
				break;
			}
			if (!testTile.HasTile || testTile.TileType != ModContent.TileType<Shellstone>())
			{
				Test--;
				continue;
			}
			PlaceVine = true;
			break;
		}
		if (PlaceVine)
		{
			tileBelow.TileType = base.Type;
			tileBelow.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
	}
}
