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

public class SulphurousPots : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileOreFinderPriority[base.Type] = 100;
		Main.tileSpelunker[base.Type] = true;
		Main.tileCut[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.DrawYOffset = 4;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(226, 205, 101), Language.GetText("MapObject.Pot"));
		base.DustType = 75;
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
			int goreAmt = Main.rand.Next(1, 3);
			for (int k = 0; k < goreAmt; k++)
			{
				Gore.NewGore(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, Main.rand.NextVector2CircularEdge(3f, 3f), base.Mod.Find<ModGore>("SulphPotGore1").Type);
				Gore.NewGore(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, Main.rand.NextVector2CircularEdge(3f, 3f), base.Mod.Find<ModGore>("SulphPotGore2").Type);
			}
		}
		if (Player.GetClosestRollLuck(i, j, 400) == 0f)
		{
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(new EntitySource_TileBreak(i, j), i * 16 + 16, j * 16 + 16, 0f, -12f, 518, 0, 0f, Main.myPlayer);
			}
		}
		else if (Main.getGoodWorld && Main.rand.NextBool(4))
		{
			Projectile.NewProjectile(new EntitySource_TileBreak(i, j), i * 16 + 16, j * 16 + 8, (float)Main.rand.Next(-100, 101) * 0.002f, 0f, 28, 0, 0f, Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16));
		}
		else if (Main.remixWorld && Main.rand.NextBool(5))
		{
			yield return new Item(75);
		}
		else
		{
			yield return new Item(ModContent.ItemType<SulphuricTreasure>());
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
