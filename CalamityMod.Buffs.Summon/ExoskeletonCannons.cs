using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon;

public class ExoskeletonCannons : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (AresExoskeleton.ArmExists(player))
		{
			modPlayer.AresCannons = true;
		}
		if (modPlayer.AresCannons)
		{
			player.buffTime[buffIndex] = 18000;
			return;
		}
		player.DelBuff(buffIndex);
		buffIndex--;
	}
}
