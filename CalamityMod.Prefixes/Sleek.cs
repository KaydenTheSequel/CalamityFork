using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "SleekWeaponPrefix" })]
public class Sleek : RogueWeaponPrefix
{
	public override float damageMult => 1f;

	public override float useTimeMult => 0.9f;

	public override float shootSpeedMult => 1.15f;
}
