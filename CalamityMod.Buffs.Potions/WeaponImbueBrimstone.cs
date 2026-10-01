using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Potions;

public class WeaponImbueBrimstone : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		Main.meleeBuff[base.Type] = true;
		Main.persistentBuff[base.Type] = true;
		BuffID.Sets.IsAFlaskBuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().flaskBrimstone = true;
		player.meleeEnchant = 99;
	}
}
