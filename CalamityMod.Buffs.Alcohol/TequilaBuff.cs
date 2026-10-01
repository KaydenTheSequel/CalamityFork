using CalamityMod.CalPlayer;
using CalamityMod.Items.Potions.Alcohol;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Alcohol;

public class TequilaBuff : ModBuff
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
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.tequila = true;
		calamityPlayer.ElectricDebuffMultiplier += Tequila.DebuffBoost;
		calamityPlayer.ColdDebuffMultiplier -= Tequila.DebuffLoss;
	}
}
