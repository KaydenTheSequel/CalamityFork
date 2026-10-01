using CalamityMod.Items.Armor.Demonshade;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class Enraged : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs((1.0 + (double)DemonshadeHelm.MultDamageBoost).ToString(), (1.0 + DemonshadeHelm.MultDamageTakenBoost).ToString());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
		BuffID.Sets.IsATagBuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().enraged = true;
	}
}
