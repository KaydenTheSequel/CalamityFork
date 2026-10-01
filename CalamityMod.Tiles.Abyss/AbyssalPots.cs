using System.Collections.Generic;
using CalamityMod.Items.TreasureBags.MiscGrabBags;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

public class AbyssalPots : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileOreFinderPriority[base.Type] = 100;
		Main.tileSpelunker[base.Type] = true;
		Main.tileCut[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(47, 79, 79), Language.GetText("MapObject.Pot"));
		base.DustType = 29;
		base.HitSound = SoundID.Shatter;
	}

	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		Tile tileAtPosition = CalamityUtils.ParanoidTileRetrieval(i, j);
		if (tileAtPosition.TileFrameX % 36 != 0 || tileAtPosition.TileFrameY % 36 != 0)
		{
			yield break;
		}
		if (!Main.dedServ)
		{
			int goreAmt = Main.rand.Next(2, 5);
			for (int k = 0; k < goreAmt; k++)
			{
				Gore.NewGore(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, Main.rand.NextVector2CircularEdge(3f, 3f), base.Mod.Find<ModGore>($"AbyssPot{WorldGen.genRand.Next(1, 7)}").Type);
			}
		}
		if (Player.GetClosestRollLuck(i, j, 400) == 0f)
		{
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(new EntitySource_TileBreak(i, j), i * 16 + 16, j * 16 + 16, 0f, -12f, 518, 0, 0f, Main.myPlayer);
			}
		}
		else if (Main.getGoodWorld && Main.rand.NextBool(6))
		{
			Projectile.NewProjectile(new EntitySource_TileBreak(i, j), i * 16 + 16, j * 16 + 8, (float)Main.rand.Next(-100, 101) * 0.002f, 0f, 28, 0, 0f, Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16));
		}
		else if (Main.remixWorld && Main.rand.NextBool(5))
		{
			yield return new Item(75);
		}
		else
		{
			yield return new Item(ModContent.ItemType<AbyssalTreasure>());
		}
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		if (Main.rand.NextBool())
		{
			type = 29;
		}
		else
		{
			type = 186;
		}
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
