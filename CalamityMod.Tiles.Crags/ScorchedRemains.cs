using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Crags;

public class ScorchedRemains : ModTile
{
	private int sheetWidth = 234;

	private int sheetHeight = 90;

	public override void SetStaticDefaults()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithHell(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<BrimstoneSlag>());
		base.DustType = 155;
		base.HitSound = SoundID.Dig;
		base.MinPick = 100;
		AddMapEntry(new Color(57, 52, 72));
		this.RegisterBlendMergeWith(ModContent.TileType<BrimstoneSlag>());
		this.RegisterBlendMergeWith(57);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile up = Main.tile[i, j - 1];
		Tile left = Main.tile[i - 1, j];
		Tile right = Main.tile[i + 1, j];
		if (WorldGen.genRand.NextBool(3) && !up.HasTile && (left.TileType == ModContent.TileType<ScorchedRemainsGrass>() || right.TileType == ModContent.TileType<ScorchedRemainsGrass>()))
		{
			Main.tile[i, j].TileType = (ushort)ModContent.TileType<ScorchedRemainsGrass>();
		}
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 3 * sheetWidth;
		frameYOffset = j % 3 * sheetHeight;
	}
}
