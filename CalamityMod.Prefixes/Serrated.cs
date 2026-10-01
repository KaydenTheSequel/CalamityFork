using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "SerratedWeaponPrefix" })]
public class Serrated : RogueWeaponPrefix
{
	public override float damageMult => 1.1f;

	public override float useTimeMult => 0.9f;

	public override float shootSpeedMult => 1.05f;
}
