namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class MediumCoral3 : MediumCoral
{
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.23f;
		g = 0.43f;
		b = 0.57f;
	}
}
