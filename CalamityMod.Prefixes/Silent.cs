using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

[LegacyName(new string[] { "Cloaked", "CloakedPrefix", "QuietPrefix", "SilentPrefix", "CamouflagedPrefix" })]
public class Silent : RogueAccessoryPrefix
{
	public override float stealthGenBonus => 0.08f;
}
