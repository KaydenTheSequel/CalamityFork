using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "FlawlessWeaponPrefix" })]
public class Flawless : RogueWeaponPrefix
{
	public override float damageMult => 1.15f;

	public override float useTimeMult => 0.9f;

	public override int critBonus => 5;

	public override float shootSpeedMult => 1.1f;

	public override float stealthDmgMult => 1.15f;
}
