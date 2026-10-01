using CalamityMod.Items.Accessories;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class PinkJellyRegen : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(LifeJelly.AuraRegenBoost.ToRegenPerSecond());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().PinkJellyRegen = true;
	}
}
