namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class MediumCoral2Echo : MediumCoralEcho
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/MediumCoral2";

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.15f;
		g = 0.28f;
		b = 0.37f;
	}
}
