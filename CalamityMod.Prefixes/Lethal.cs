using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "LethalWeaponPrefix" })]
public class Lethal : RogueWeaponPrefix
{
	public override float damageMult => 1.1f;

	public override float useTimeMult => 0.95f;

	public override int critBonus => 2;

	public override float shootSpeedMult => 1.05f;

	public override float stealthDmgMult => 1.05f;
}
