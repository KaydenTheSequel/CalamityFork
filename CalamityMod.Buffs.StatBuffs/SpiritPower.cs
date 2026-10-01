using CalamityMod.Items.Accessories;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class SpiritPower : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(SpiritGlyph.SummonDamageBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		Main.buffNoTimeDisplay[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetDamage<SummonDamageClass>() += SpiritGlyph.SummonDamageBoost;
	}
}
