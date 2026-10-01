using CalamityMod.Items.Weapons.Magic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class AeolianEarthBuff : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(PrimordialAncient.BuffDamageReductionBoost.ToPercent(), PrimordialAncient.BuffDamageBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = false;
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().aeolianEarthBuff = true;
	}
}
