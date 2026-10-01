using CalamityMod.Items.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class WitherDebuff : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(RemsRevenge.WitherDefenseReduction);

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().wither = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().wither = true;
	}
}
