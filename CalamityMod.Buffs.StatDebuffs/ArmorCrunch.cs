using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class ArmorCrunch : ModBuff
{
	public static int DefenseReduction = 15;

	public static float MultiplicativeDamageReductionPlayer = 0.5f;

	public static float MultiplicativeDamageReductionEnemy = 0.85f;

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().armorCrunch = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().armorCrunch = true;
	}
}
