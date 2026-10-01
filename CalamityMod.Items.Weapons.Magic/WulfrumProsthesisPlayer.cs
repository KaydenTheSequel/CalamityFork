using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class WulfrumProsthesisPlayer : ModPlayer
{
	public bool ManaDrainActive;

	public override void UpdateDead()
	{
		ManaDrainActive = false;
	}

	public override void PostUpdateMiscEffects()
	{
		if (ManaDrainActive)
		{
			base.Player.manaRegenBonus = 35;
			base.Player.manaRegenDelay = 0f;
			base.Player.manaRegenBuff = true;
		}
		ManaDrainActive = false;
	}
}
