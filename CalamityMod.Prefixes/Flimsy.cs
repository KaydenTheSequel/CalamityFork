using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "FlimsyWeaponPrefix" })]
public class Flimsy : RogueWeaponPrefix
{
	public override float damageMult => 0.9f;

	public override float stealthDmgMult => 0.9f;
}
