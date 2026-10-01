using CalamityMod.ExtraTextures.GreyscaleGradients;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace CalamityMod.Tiles.Plates;

public class Onyxplate : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.MineResist = 1f;
		base.DustType = 173;
		AddMapEntry(new Color(182, 28, 232));
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		float brightness = GreyscaleGradient.OnyxplatePulse.GetRepeat((int)Main.GameUpdateCount);
		brightness = 0.04f + brightness * 0.31f;
		return Color.White * brightness;
	}
}
