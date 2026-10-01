using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon;

public class VoidConcentrationBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		int count = player.ownedProjectileCounts[ModContent.ProjectileType<VoidConcentrationAura>()];
		player.GetDamage<SummonDamageClass>() += 0.05f;
		calamityPlayer.voidConcentrationAura = true;
		if (!calamityPlayer.voidAuraDamage && count == 0)
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
