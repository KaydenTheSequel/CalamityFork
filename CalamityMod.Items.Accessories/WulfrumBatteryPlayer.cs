using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class WulfrumBatteryPlayer : ModPlayer
{
	public bool battery;

	public override void ResetEffects()
	{
		battery = false;
	}

	public override void UpdateDead()
	{
		battery = false;
	}
}
