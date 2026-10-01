using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityMod.Tiles.Ores;

public class AuricOre : GlowMaskTile
{
	public static readonly SoundStyle MineSound = new SoundStyle("CalamityMod/Sounds/Custom/AuricMine", 3);

	public static bool Animate;

	public override void SetupStatic()
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		base.AnimationFrameHeight = 90;
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileSpelunker[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 1000;
		Main.tileShine[base.Type] = 3500;
		Main.tileShine2[base.Type] = false;
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.Ore[base.Type] = true;
		TileID.Sets.OreMergesWithMud[base.Type] = true;
		base.DustType = 55;
		AddMapEntry(new Color(255, 200, 0), CreateMapEntryName());
		base.MineResist = 5f;
		base.MinPick = 250;
		base.HitSound = MineSound;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Animate)
		{
			r = 0.24f;
			g = 0.4f;
			b = 0.47f;
		}
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (!Animate)
		{
			return;
		}
		frameCounter++;
		if (frameCounter > 4)
		{
			frameCounter = 0;
			frame++;
			if (frame > 7)
			{
				Animate = false;
				frame = 0;
			}
		}
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
