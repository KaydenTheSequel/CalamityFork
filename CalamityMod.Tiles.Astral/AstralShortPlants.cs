using System.Collections.Generic;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralShortPlants : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		Main.tileCut[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.ReplaceTileBreakUp[base.Type] = true;
		TileID.Sets.SwaysInWindBasic[base.Type] = true;
		TileID.Sets.IgnoredByGrowingSaplings[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		base.DustType = ModContent.DustType<AstralBasic>();
		base.HitSound = SoundID.Grass;
		AddMapEntry(new Color(127, 111, 144));
		base.SetStaticDefaults();
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tileBelow = Framing.GetTileSafely(i, j + 1);
		int type = -1;
		if (tileBelow.HasTile)
		{
			type = tileBelow.TileType;
		}
		if (type == ModContent.TileType<AstralGrass>())
		{
			return true;
		}
		WorldGen.KillTile(i, j);
		return true;
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}

	public override void DropCritterChance(int i, int j, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
	{
		if (NPC.CountNPCS(484) < 5 && Main.rand.NextBool(400))
		{
			int worm = NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16 + 10, j * 16, 484);
			Main.npc[worm].TargetClosest();
			Main.npc[worm].velocity.Y = Main.rand.NextFloat(-5f, -2.1f);
			Main.npc[worm].velocity.X = Main.rand.NextFloat(0f, 2.6f) * (float)(-Main.npc[worm].direction);
			Main.npc[worm].direction *= -1;
			Main.npc[worm].netUpdate = true;
		}
	}

	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		Vector2 worldPosition = Utils.ToWorldCoordinates(new Vector2((float)i, (float)j), 8f, 8f);
		Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];
		if (nearestPlayer.active && nearestPlayer.HeldItem.type == 1786)
		{
			yield return new Item(1727, Main.rand.Next(1, 3));
		}
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
