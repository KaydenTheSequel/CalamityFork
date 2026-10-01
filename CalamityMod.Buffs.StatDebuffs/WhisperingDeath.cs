using CalamityMod.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class WhisperingDeath : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		SicknessDebuffScaling = 1f
	};

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().whisperingDeath = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().whisperingDeath = true;
	}
}
