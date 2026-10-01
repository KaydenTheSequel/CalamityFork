using CalamityMod.ExtraTextures.GreyscaleGradients;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace CalamityMod.Tiles.Plates;

public class PlagueContainmentCells : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		base.HitSound = CommonCalamitySounds.PlatingMine;
		base.MineResist = 1f;
		AddMapEntry(new Color(128, 188, 67));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		int dust = Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 89, 0f, 0f, 100, default(Color), 2f);
		Main.dust[dust].noGravity = true;
		Main.dust[dust].velocity.Y = -0.15f;
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override void RandomUpdate(int i, int j)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		int dust = Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 89, 0f, 0f, 100, default(Color), 2f);
		Main.dust[dust].noGravity = true;
		Main.dust[dust].velocity.Y = -0.15f;
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		float brightness = GreyscaleGradient.PlagueContainmentCellsPulse.GetRepeat((int)Main.GameUpdateCount);
		brightness = 0.04f + brightness * 0.156f;
		return Color.White * brightness;
	}
}
