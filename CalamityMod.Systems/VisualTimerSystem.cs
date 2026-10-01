using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class VisualTimerSystem : ModSystem
{
	public static float GlobalVisualTimer;

	public override void PostUpdateEverything()
	{
		GlobalVisualTimer++;
	}
}
