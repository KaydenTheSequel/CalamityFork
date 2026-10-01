namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class MediumCoral3Echo : MediumCoralEcho
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/MediumCoral3";

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.24f;
		g = 0.43f;
		b = 0.57f;
	}
}
