using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class AquaticHeartBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		Main.buffNoTimeDisplay[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.aquaticHeartPrevious)
		{
			player.ignoreWater = NPC.downedBoss3;
			player.accFlipper = true;
			if (player.breath <= player.breathMax + 2 && !modPlayer.ZoneAbyss && NPC.downedBoss3)
			{
				player.breath = player.breathMax + 3;
			}
			if (Main.myPlayer == player.whoAmI && player.Calamity().countsAsAnyWet && NPC.downedBoss3)
			{
				player.AddBuff(ModContent.BuffType<AquaticHeartWaterSpeed>(), 360);
			}
		}
		else
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}
}
