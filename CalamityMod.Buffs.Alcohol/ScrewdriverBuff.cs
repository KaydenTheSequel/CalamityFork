using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Alcohol;

public class ScrewdriverBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		Main.persistentBuff[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().screwdriver = true;
		player.blockRange += 5;
		player.tileSpeed++;
		player.wallSpeed++;
		player.GetDamage(DamageClass.Generic) *= 0.75f;
	}
}
