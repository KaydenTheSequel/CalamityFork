using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "PointyWeaponPrefix" })]
public class Pointy : RogueWeaponPrefix
{
	public override float damageMult => 1.1f;
}
