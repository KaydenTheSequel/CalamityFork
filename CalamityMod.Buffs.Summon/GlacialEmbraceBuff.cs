using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon;

public class GlacialEmbraceBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.GlacialEmbrace = true;
		if (!calamityPlayer.GlacialEmbrace)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
	}
}
