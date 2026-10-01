using System;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralVines : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileCut[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		TileID.Sets.IsVine[base.Type] = true;
		TileID.Sets.ReplaceTileBreakDown[base.Type] = true;
		TileID.Sets.VineThreads[base.Type] = true;
		TileID.Sets.DrawFlipMode[base.Type] = 1;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		base.DustType = ModContent.DustType<AstralBasic>();
		base.HitSound = SoundID.Grass;
		AddMapEntry(new Color(65, 56, 83));
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		Main.instance.TilesRenderer.CrawlToTopOfVineAndAddSpecialPoint(j, i);
		return false;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (WorldGen.genRand.NextBool() && Main.player[Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].cordage)
		{
			Item.NewItem((IEntitySource)new EntitySource_TileBreak(i, j), new Vector2((float)(i * 16) + 8f, (float)(j * 16) + 8f), 2996, 1, false, 0, false, false);
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		float brightness = 0.9f;
		brightness += 0.2f;
		brightness = MathHelper.Clamp(brightness, 0.5f, 0.9f);
		Color val = new Color(237, 93, 83);
		Color cyan = default(Color);
		((Color)(ref cyan))._002Ector(66, 189, 181);
		Color value = Color.Lerp(val, cyan, (MathF.Sin((float)j / 30f + (float)Main.GameUpdateCount * 0.017f + (float)(-i) / 40f) + 1f) / 2f);
		Color value2 = Color.Lerp(val, cyan, (MathF.Sin((float)(-j - 100) / 40f + (float)Main.GameUpdateCount * 0.014f + (float)i / 20f) + 1f) / 2f);
		r = (float)(((Color)(ref value)).R + ((Color)(ref value2)).R) / 600f;
		g = (float)(((Color)(ref value)).G + ((Color)(ref value2)).G) / 600f;
		b = (float)(((Color)(ref value)).B + ((Color)(ref value2)).B) / 600f;
		r *= brightness;
		g *= brightness;
		b *= brightness;
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
