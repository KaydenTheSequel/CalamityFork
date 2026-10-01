using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class BossEffects : ModBuff
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
		player.Calamity().isNearbyBoss = true;
	}

	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		if (CalamityServerConfig.Instance.BossZen)
		{
			tip = tip.Replace(":", ":\n" + this.GetLocalizedValue("ZenDescription"));
		}
	}
}
