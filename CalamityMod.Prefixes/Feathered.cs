using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "FeatheredWeaponPrefix" })]
public class Feathered : RogueWeaponPrefix
{
	public override float useTimeMult => 0.85f;

	public override float shootSpeedMult => 1.1f;
}
