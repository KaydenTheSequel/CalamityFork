using CalamityMod.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class HolyInferno : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 10000f,
		HeatDebuffScaling = 1f,
		GearCanModifyDebuff = false
	};

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().holyInferno = true;
	}
}
