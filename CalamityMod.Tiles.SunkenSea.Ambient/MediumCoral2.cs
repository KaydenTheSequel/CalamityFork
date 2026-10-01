namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class MediumCoral2 : MediumCoral
{
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.15f;
		g = 0.37f;
		b = 0.46f;
	}
}
