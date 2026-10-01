using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class ChiBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.endurance += 0.08f;
	}
}
