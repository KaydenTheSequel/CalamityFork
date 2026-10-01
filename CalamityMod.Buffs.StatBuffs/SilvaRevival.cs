using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class SilvaRevival : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}
}
