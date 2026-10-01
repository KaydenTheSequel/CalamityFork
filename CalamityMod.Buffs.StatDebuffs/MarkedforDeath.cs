using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class MarkedforDeath : ModBuff
{
	public static int DefenseReduction = 5;

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().markedForDeath = true;
	}
}
