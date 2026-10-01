using CalamityMod.Dusts;
using CalamityMod.Systems;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class AstralBrick : GlowMaskTile
{
	private const short subsheetWidth = 324;

	private const short subsheetHeight = 90;

	public override void SetupStatic()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.HitSound = SoundID.Tink;
		AddMapEntry(new Color(128, 128, 158));
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralBasic>(), 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int xPos = i % 2;
		int yPos = j % 2;
		frameXOffset = xPos * 324;
		frameYOffset = yPos * 90;
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return new Color(50, 50, 50);
	}
}
