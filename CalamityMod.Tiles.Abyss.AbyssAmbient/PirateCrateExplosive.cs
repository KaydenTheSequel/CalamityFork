using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss.AbyssAmbient;

public abstract class PirateCrateExplosive : GlowMaskTile
{
	public static readonly SoundStyle MineSound = new SoundStyle("CalamityMod/Sounds/Custom/CrateBreak", 3)
	{
		Volume = 0.8f
	};

	public abstract string GoreKey { get; }

	public override void SetupStatic()
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
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPosition = default(Vector2);
		((Vector2)(ref spawnPosition))._002Ector((float)i * 16f + 24f, (float)j * 16f - 4f);
		int blastDamage = (Main.getGoodWorld ? 99999 : 150) * (Main.masterMode ? 3 : ((!Main.expertMode) ? 1 : 2));
		Projectile.NewProjectile(new EntitySource_WorldEvent(), spawnPosition.X, spawnPosition.Y, 0f, 0f, 108, blastDamage, 0f);
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

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
