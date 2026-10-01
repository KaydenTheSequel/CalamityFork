using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class ManaBurn : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		Main.buffNoTimeDisplay[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().ManaBurn = true;
	}
}
