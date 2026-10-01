using CalamityMod.Systems.Collections;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class GalvanicCorrosion : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().galvanicCorrosion = true;
		if ((CalamityNPCSets.ResistSlowingDebuffsAndOtherSpecialEffects[npc.type] || npc.boss) && npc.Calamity().debuffResistanceTimer <= 0)
		{
			npc.Calamity().debuffResistanceTimer = 1800 + npc.buffTime[buffIndex];
		}
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().galvanicCorrosion = true;
	}
}
