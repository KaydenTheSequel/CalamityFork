using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.DraedonStructures;

public class RustedPipes : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileMerge[base.Type][ModContent.TileType<LaboratoryPipePlating>()] = true;
		base.HitSound = SoundID.Item52;
		base.DustType = 32;
		base.MinPick = 30;
		AddMapEntry(new Color(128, 90, 77));
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void PlaceInWorld(int i, int j, Item item)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item52 with
		{
			Volume = SoundID.Item52.Volume * 0.75f,
			Pitch = SoundID.Item52.Pitch - 0.5f
		};
		SoundEngine.PlaySound(in style, (Vector2?)new Vector2((float)(i * 16), (float)(j * 16)), (SoundUpdateCallback?)null);
	}
}
