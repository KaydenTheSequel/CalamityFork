using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class SeaAnemone : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		base.AnimationFrameHeight = 36;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(base.Type);
		base.DustType = 253;
		AddMapEntry(new Color(54, 69, 72));
		base.SetStaticDefaults();
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.11f;
		g = 0.29f;
		b = 0.57f;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter > 12)
		{
			frameCounter = 0;
			frame++;
			if (frame > 5)
			{
				frame = 0;
			}
		}
	}
}
