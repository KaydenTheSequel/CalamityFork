using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "ViciousWeaponPrefix" })]
public class Vicious : RogueWeaponPrefix
{
	public override float damageMult => 1.1f;

	public override float useTimeMult => 0.95f;

	public override float shootSpeedMult => 1.15f;
}
