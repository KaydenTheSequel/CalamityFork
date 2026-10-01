using CalamityMod.DataStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class WeakBrimstoneFlames : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 10f,
		HeatDebuffScaling = 1f
	};

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		Main.buffNoTimeDisplay[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().weakBrimstoneFlames = true;
	}
}
