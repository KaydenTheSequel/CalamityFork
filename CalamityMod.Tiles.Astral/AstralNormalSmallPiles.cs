using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralNormalSmallPiles : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		base.DustType = ModContent.DustType<AstralBasic>();
		AddMapEntry(new Color(79, 61, 97));
		base.SetStaticDefaults();
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 4);
	}

	public override void DropCritterChance(int i, int j, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
	{
		if (NPC.CountNPCS(484) < 5 && Main.rand.NextBool(6))
		{
			int worm = NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16 + 10, j * 16, 484);
			Main.npc[worm].TargetClosest();
			Main.npc[worm].velocity.Y = Main.rand.NextFloat(-5f, -2.1f);
			Main.npc[worm].velocity.X = Main.rand.NextFloat(0f, 2.6f) * (float)(-Main.npc[worm].direction);
			Main.npc[worm].direction *= -1;
			Main.npc[worm].netUpdate = true;
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
