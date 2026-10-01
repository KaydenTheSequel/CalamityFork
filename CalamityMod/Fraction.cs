namespace CalamityMod;

public struct Fraction(int n, int d)
{
	internal readonly int numerator = ((n >= 0) ? n : 0);

	internal readonly int denominator = ((d <= 0) ? 1 : d);

	public static implicit operator float(Fraction f)
	{
		return (float)f.numerator / (float)f.denominator;
	}
}
