using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "HeftyWeaponPrefix" })]
public class Hefty : RogueWeaponPrefix
{
	public override float damageMult => 1.1f;

	public override float stealthDmgMult => 1.15f;
}
