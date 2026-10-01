using CalamityMod.CalPlayer;
using CalamityMod.Items.Potions.Alcohol;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Alcohol;

public class VodkaBuff : ModBuff
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
		calamityPlayer.vodka = true;
		calamityPlayer.TypelessDebuffMultiplier += Vodka.DebuffBoost;
		calamityPlayer.HeatDebuffMultiplier -= Vodka.DebuffLoss;
		calamityPlayer.ColdDebuffMultiplier -= Vodka.DebuffLoss;
		calamityPlayer.SicknessDebuffMultiplier -= Vodka.DebuffLoss;
		calamityPlayer.WaterDebuffMultiplier -= Vodka.DebuffLoss;
		calamityPlayer.ElectricDebuffMultiplier -= Vodka.DebuffLoss;
	}
}
