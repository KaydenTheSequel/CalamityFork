using Microsoft.Xna.Framework;

namespace CalamityMod.CalPlayer;

public struct CalamityPlayerDrawingParameters
{
	public float ProfanedShieldCharge;

	public Color ProfanedShieldColor;

	public float SpongeShieldCharge;

	public float RoverShieldCharge;

	public float LunicShieldCharge;

	public static bool operator ==(CalamityPlayerDrawingParameters left, CalamityPlayerDrawingParameters right)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (left.RoverShieldCharge != right.RoverShieldCharge)
		{
			return false;
		}
		if (left.LunicShieldCharge != right.LunicShieldCharge)
		{
			return false;
		}
		if (left.ProfanedShieldCharge != right.ProfanedShieldCharge)
		{
			return false;
		}
		if (left.ProfanedShieldColor != right.ProfanedShieldColor)
		{
			return false;
		}
		if (left.SpongeShieldCharge != right.SpongeShieldCharge)
		{
			return false;
		}
		return true;
	}

	public static bool operator !=(CalamityPlayerDrawingParameters left, CalamityPlayerDrawingParameters right)
	{
		return !(left == right);
	}

	public override readonly bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	public override readonly int GetHashCode()
	{
		return base.GetHashCode();
	}
}
