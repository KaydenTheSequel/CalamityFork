using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss.AbyssAmbient;

public abstract class PirateCrateGold : ModTile
{
	public static readonly SoundStyle MineSound = new SoundStyle("CalamityMod/Sounds/Custom/CrateBreak", 3)
	{
		Volume = 0.8f
	};

	public abstract string GoreKey { get; }

	public override void SetStaticDefaults()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(97, 69, 52), CalamityUtils.GetText("Tiles.PirateCrate"));
		base.DustType = 7;
		base.HitSound = MineSound;
		base.SetStaticDefaults();
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, 73, Main.rand.Next(1, 2));
		}
		Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, 72, Main.rand.Next(45, 75));
		if (!Main.dedServ)
		{
			Gore.NewGore(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, Main.rand.NextVector2CircularEdge(3f, 3f), base.Mod.Find<ModGore>(GoreKey + "1").Type);
			Gore.NewGore(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, Main.rand.NextVector2CircularEdge(3f, 3f), base.Mod.Find<ModGore>(GoreKey + "2").Type);
			Gore.NewGore(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, Main.rand.NextVector2CircularEdge(3f, 3f), base.Mod.Find<ModGore>(GoreKey + "3").Type);
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}
}
