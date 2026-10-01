using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

internal sealed class MountsBalancingSystem : ModSystem
{
	public override void OnModLoad()
	{
		Mount.drillPickPower = 225;
	}

	public override void Unload()
	{
		Mount.drillPickPower = 210;
	}
}
