using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class AmidiasBlessing : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.buffNoSave[base.Type] = false;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().amidiasBlessing = true;
		player.breath = player.breathMax + 91;
	}
}
