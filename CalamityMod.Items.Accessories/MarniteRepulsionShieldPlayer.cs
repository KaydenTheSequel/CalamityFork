using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class MarniteRepulsionShieldPlayer : ModPlayer
{
	public bool shieldEquipped;

	public override void ResetEffects()
	{
		shieldEquipped = false;
	}

	public override void UpdateDead()
	{
		shieldEquipped = false;
	}
}
