using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Potions;

public class AstralInjectionBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().astralInjection = true;
	}
}
