using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class PhantomicRegen : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		Main.buffNoTimeDisplay[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		CalamityPlayer calPlayer = player.Calamity();
		if (calPlayer.phantomicHeartRegen <= 0)
		{
			calPlayer.phantomicHeartRegen = 1000;
		}
		player.lifeRegen += 2;
	}
}
