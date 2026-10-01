using CalamityMod.Items.Armor.Tarragon;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class TarragonCloak : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(TarragonHeadMelee.CloakContactDamageReduction.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().tarragonCloak = true;
	}
}
