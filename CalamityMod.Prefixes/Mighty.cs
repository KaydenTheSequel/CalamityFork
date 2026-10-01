using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "MightyWeaponPrefix" })]
public class Mighty : RogueWeaponPrefix
{
	public override float damageMult => 1.15f;

	public override float stealthDmgMult => 1.05f;
}
