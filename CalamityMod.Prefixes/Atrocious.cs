using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "AtrociousWeaponPrefix" })]
public class Atrocious : RogueWeaponPrefix
{
	public override float damageMult => 0.85f;

	public override float shootSpeedMult => 0.9f;

	public override float stealthDmgMult => 0.9f;
}
