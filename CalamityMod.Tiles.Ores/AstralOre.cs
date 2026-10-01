using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Tiles.Astral;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

public class AstralOre : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 900;
		Main.tileSpelunker[base.Type] = true;
		Main.tileShine2[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		TileID.Sets.Ore[base.Type] = true;
		TileID.Sets.OreMergesWithMud[base.Type] = true;
		base.MinPick = 110;
		base.DustType = 173;
		AddMapEntry(new Color(255, 153, 255), CreateMapEntryName());
		base.MineResist = 3f;
		base.HitSound = SoundID.Tink;
		TileID.Sets.Ore[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.AvoidedByMeteorLanding[base.Type] = true;
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		if (j >= 2)
		{
			_ = Main.tile[i, j];
			Tile up = Main.tile[i, j - 1];
			Tile up2 = Main.tile[i, j - 2];
			if (closer && Main.rand.NextBool(60) && !up.HasTile && !up2.HasTile && (double)j < Main.worldSurface)
			{
				_ = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 16, 16, ModContent.DustType<AstralBlue>(), -0.4651165f, 0f, 17, new Color(0, 255, 244), 1.5f)];
				_ = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 16, 16, ModContent.DustType<AstralOrange>(), -0.4651165f, 0f, 17, new Color(255, 255, 255), 1.5f)];
			}
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		if (Main.rand.NextBool(4))
		{
			int xRandom = Main.rand.Next(-3, 3);
			int yRandom = Main.rand.Next(-3, 3);
			if (Main.tile[i + xRandom, j + yRandom].TileType == 37)
			{
				AstralBiome.ConvertToAstral(i + xRandom, j + yRandom, convertOre: true);
			}
		}
	}

	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		if (!DownedBossSystem.downedAstrumDeus)
		{
			if (Main.rand.NextBool())
			{
				yield return new Item(ModContent.ItemType<StarblightSoot>());
			}
			else
			{
				yield return new Item(0);
			}
		}
		else
		{
			yield return new Item(ModContent.ItemType<global::CalamityMod.Items.Placeables.Ores.AstralOre>());
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		float brightness = 0.9f;
		Color val = new Color(237, 93, 83);
		Color cyan = default(Color);
		((Color)(ref cyan))._002Ector(66, 189, 181);
		Color value = Color.Lerp(val, cyan, (MathF.Sin((float)(-j) / 30f + (float)Main.GameUpdateCount * 0.017f + (float)i / 40f) + 1f) / 2f);
		Color value2 = Color.Lerp(val, cyan, (MathF.Sin((float)(j - 100) / 40f + (float)Main.GameUpdateCount * 0.014f + (float)(-i) / 20f) + 1f) / 2f);
		r = (float)(((Color)(ref value)).R + ((Color)(ref value2)).R) / 600f;
		g = (float)(((Color)(ref value)).G + ((Color)(ref value2)).G) / 600f;
		b = (float)(((Color)(ref value)).B + ((Color)(ref value2)).B) / 600f;
		r *= brightness;
		g *= brightness;
		b *= brightness;
	}
}
