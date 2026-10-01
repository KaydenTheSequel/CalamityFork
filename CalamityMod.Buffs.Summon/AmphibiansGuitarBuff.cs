using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon;

public class AmphibiansGuitarBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (player.ownedProjectileCounts[ModContent.ProjectileType<AmphibiansGuitarMinion>()] > 0)
		{
			modPlayer.AmphibiansGuitarBool = true;
		}
		if (!modPlayer.AmphibiansGuitarBool)
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
