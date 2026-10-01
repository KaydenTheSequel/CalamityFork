using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "BluntWeaponPrefix" })]
public class Blunt : RogueWeaponPrefix
{
	public override float damageMult => 0.85f;
}
