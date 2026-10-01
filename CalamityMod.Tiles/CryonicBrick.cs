using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class CryonicBrick : ModTile
{
	private int subsheetHeight = 90;

	private int subsheetWidth = 234;

	public override void SetStaticDefaults()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		base.HitSound = SoundID.Tink;
		AddMapEntry(new Color(99, 131, 199));
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(147);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 176, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int xPos = i % 2;
		int yPos = j % 4;
		frameXOffset = xPos * subsheetWidth;
		frameYOffset = yPos * subsheetHeight;
	}
}
