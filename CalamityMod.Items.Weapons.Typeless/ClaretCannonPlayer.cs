using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class ClaretCannonPlayer : ModPlayer
{
	public int ClaretCooldown;

	public override void ResetEffects()
	{
		if (ClaretCooldown > 0)
		{
			ClaretCooldown--;
		}
	}
}
