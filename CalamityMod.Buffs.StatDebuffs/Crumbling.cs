using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class Crumbling : ModBuff
{
	public static int DefenseReduction = 10;

	public static float MultiplicativeDamageReductionPlayer = 0.7f;

	public static float MultiplicativeDamageReductionEnemy = 0.92f;

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
		npc.Calamity().crumble = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().crumble = true;
	}
}
