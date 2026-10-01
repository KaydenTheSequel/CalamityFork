using CalamityMod.Items.Armor.Reaver;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class ReaverRage : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(ReaverHeadTank.ReaverRageDefenseBoost, ReaverHeadTank.ReaverRageDamageBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().rRage = true;
	}
}
