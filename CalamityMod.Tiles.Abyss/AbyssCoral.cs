using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class AbyssCoral : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		Main.tileLighted[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.HitSound = SoundID.Dig;
		base.DustType = 119;
		AddMapEntry(new Color(70, 115, 144));
		Main.tileShine2[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (!Main.tile[i - 1, j].HasTile || !Main.tile[i + 1, j].HasTile || !Main.tile[i, j - 1].HasTile || !Main.tile[i, j + 1].HasTile)
		{
			r = 0.17f;
			g = 0.28f;
			b = 0.36f;
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
