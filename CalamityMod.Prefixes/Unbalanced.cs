using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "UnbalancedWeaponPrefix" })]
public class Unbalanced : RogueWeaponPrefix
{
	public override float useTimeMult => 1.15f;

	public override float shootSpeedMult => 0.95f;
}
