namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class SmallWideCoral2Echo : SmallWideCoralEcho
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallWideCoral2";

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.33f;
		g = 0.2f;
		b = 0.29f;
	}
}
