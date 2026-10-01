using CalamityMod.Items.Potions;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Potions;

public class CalciumBuff : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(CalciumPotion.KnockbackResistance.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().calcium = true;
	}
}
