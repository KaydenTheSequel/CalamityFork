using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

[LegacyName(new string[] { "BrimstoneSlagWallUnsafe" })]
public class UnsafeBrimstoneSlagWall : MultiVariantModWall
{
	public override string Texture => "CalamityMod/Walls/BrimstoneSlagWall";

	public override void SetStaticDefaults()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		AddMapEntry(new Color(24, 16, 29));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 60, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void PopulateWallVariant(int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 2 * 468;
		frameYOffset = j % 2 * 180;
	}
}
