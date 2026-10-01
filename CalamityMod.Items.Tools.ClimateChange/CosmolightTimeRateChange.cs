using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools.ClimateChange;

public class CosmolightTimeRateChange : ModSystem
{
	public override void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
	{
		if (Main.player.Any((Player x) => x.active && x.channel && x.altFunctionUse != 2 && x.HeldItem.type == ModContent.ItemType<Cosmolight>()))
		{
			timeRate *= 120.0;
		}
	}
}
