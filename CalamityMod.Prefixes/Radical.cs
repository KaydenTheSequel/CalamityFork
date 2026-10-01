using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "RadicalWeaponPrefix" })]
public class Radical : RogueWeaponPrefix
{
	public override float damageMult => 1.05f;

	public override float useTimeMult => 0.95f;

	public override float shootSpeedMult => 1.05f;

	public override float stealthDmgMult => 0.9f;
}
