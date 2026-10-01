using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class ProfanedCrystalBuff : ModBuff
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
		if (!player.Calamity().profanedCrystal)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}

	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		Player player = Main.LocalPlayer;
		if (player.Calamity().profanedCrystalBuffs)
		{
			if (player.Calamity().pscState == 3)
			{
				tip = tip + "\n" + this.GetLocalizedValue("Empowered");
				return;
			}
			bool offense = (Main.dayTime && !player.wet) || player.lavaWet;
			tip = tip + "\n" + (offense ? this.GetLocalization("Offense").Format(this.GetLocalizedValue(player.lavaWet ? "Lava" : "Day")) : this.GetLocalization("Defense").Format(this.GetLocalizedValue(player.honeyWet ? "Honey" : (player.wet ? "Water" : "Night"))));
			if (!Main.dayTime)
			{
				tip = tip + "\n" + this.GetLocalizedValue("Enrage");
			}
		}
		else if (DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs)
		{
			tip = this.GetLocalizedValue("LockedSlots");
		}
		else
		{
			tip = this.GetLocalizedValue((!DownedBossSystem.downedExoMechs) ? "LockedExos" : "LockedSCal");
		}
	}
}
