using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class CoralSpoutPlayer : ModPlayer
{
	public bool Symbiosis;

	public override void ResetEffects()
	{
		Symbiosis = false;
	}

	public override void UpdateDead()
	{
		Symbiosis = false;
	}
}
