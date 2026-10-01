using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "GloriousWeaponPrefix" })]
public class Glorious : RogueWeaponPrefix
{
	public override float damageMult => 1.1f;

	public override float useTimeMult => 0.95f;
}
