using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureStatigel;

public class StatigelBlock : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["PinkSlime"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		AddMapEntry(new Color(215, 74, 121));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 243, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}
}
