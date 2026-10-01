using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "SharpWeaponPrefix" })]
public class Sharp : RogueWeaponPrefix
{
	public override float damageMult => 1.15f;
}
