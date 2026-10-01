using CalamityMod.Systems;
using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Crags;

public class BrimstoneSlag : ModTile
{
	private const short subsheetWidth = 450;

	private const short subsheetHeight = 198;

	public override void SetStaticDefaults()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithHell(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<InfernalSuevite>());
		base.HitSound = SoundID.Tink;
		base.MineResist = 2f;
		base.MinPick = 100;
		AddMapEntry(new Color(53, 33, 56));
		this.RegisterBlendMergeWith(57);
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

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 2 * 450;
		frameYOffset = j % 2 * 198;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BrimstoneFraming(i, j, resetFrame);
	}
}
