using CalamityMod.Balancing;
using CalamityMod.Systems.Collections;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs;

public class CalamityGlobalBuff : GlobalBuff
{
	public const byte ModdedFlaskEnchant = 99;

	public override void Update(int type, Player player, ref int buffIndex)
	{
		if (CalamityBuffSets.IsSummonTagBuff[type] && buffIndex > 0)
		{
			for (int i = buffIndex; i >= 0; i--)
			{
				if (player.buffTime[i] > 0)
				{
					int buffID = player.buffType[i];
					if (CalamityBuffSets.IsSummonTagBuff[buffID] && buffID != type)
					{
						player.DelBuff(i);
						break;
					}
				}
			}
		}
		switch (type)
		{
		case 16:
			player.arrowDamage *= 0.955f;
			return;
		case 7:
			player.GetDamage<MagicDamageClass>() -= 0.1f;
			return;
		case 29:
			player.GetDamage<MagicDamageClass>() -= 0.02f;
			player.GetCritChance<MagicDamageClass>() -= 2f;
			return;
		case 192:
			player.moveSpeed -= 0.1f;
			player.pickSpeed += 0.1f;
			return;
		case 104:
			player.pickSpeed += 0.1f;
			return;
		case 3:
			player.moveSpeed -= 0.1f;
			return;
		case 25:
			player.statDefense += 4;
			player.GetCritChance<MeleeDamageClass>() -= 2f;
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.2f;
			player.GetDamage<MeleeDamageClass>() -= 0.1f;
			return;
		case 26:
			player.moveSpeed -= 0.15f;
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.05f;
			return;
		case 206:
			player.moveSpeed -= 0.225f;
			player.pickSpeed += 0.025f;
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.075f;
			return;
		case 207:
			player.moveSpeed -= 0.3f;
			player.pickSpeed += 0.05f;
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.1f;
			return;
		case 33:
			player.GetAttackSpeed<MeleeDamageClass>() += 0.051f;
			return;
		case 11:
			player.Calamity().shine = true;
			return;
		case 158:
			if (!player.manaRegenBuff)
			{
				player.manaRegenBuff = true;
				player.manaRegenDelayBonus -= 0.5f;
				player.manaRegenBonus -= 10;
				return;
			}
			break;
		}
		if (type >= 95 && type <= 97 && player.beetleDefense)
		{
			int orbsToGrant = ((player.beetleOrbs >= 0) ? player.beetleOrbs : 0);
			if (orbsToGrant > 3)
			{
				orbsToGrant = 3;
			}
			player.endurance += BalancingConstants.BeetleShellDRPerBeetle * (float)orbsToGrant;
			return;
		}
		if (type >= 170 && type <= 172)
		{
			player.endurance += BalancingConstants.SolarFlareShieldDR;
			return;
		}
		switch (type)
		{
		case 148:
			player.GetDamage<GenericDamageClass>() -= 0.2f;
			if (player.buffTime[buffIndex] % 600 == 300)
			{
				player.AddBuff(Main.rand.Next(4) switch
				{
					0 => 33, 
					1 => 30, 
					2 => 22, 
					_ => 36, 
				}, Main.rand.Next(90, 211));
			}
			break;
		case 28:
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.051f;
			break;
		}
	}

	public override void Update(int type, NPC npc, ref int buffIndex)
	{
		if (CalamityBuffSets.SummonTagDebuff.TryGetValue(type, out var tag1) && !tag1.AllowsWhipStacking && buffIndex > 0)
		{
			for (int i = buffIndex; i >= 0; i--)
			{
				if (npc.buffTime[i] > 0)
				{
					int buffID = npc.buffType[i];
					if (CalamityBuffSets.SummonTagDebuff.TryGetValue(buffID, out var tag2) && !tag2.AllowsWhipStacking && buffID != type)
					{
						npc.DelBuff(i);
						break;
					}
				}
			}
		}
		if (type == 149)
		{
			npc.Calamity().webbed = true;
			if ((CalamityNPCSets.ResistSlowingDebuffsAndOtherSpecialEffects[npc.type] || npc.boss) && npc.Calamity().debuffResistanceTimer <= 0)
			{
				npc.Calamity().debuffResistanceTimer = 1800 + npc.buffTime[buffIndex];
			}
		}
		if (type == 144)
		{
			npc.Calamity().electrified = true;
		}
	}

	public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
	{
		switch (type)
		{
		case 16:
			tip = tip.Replace("10", "5");
			break;
		case 95:
			tip = tip.Replace("15", "10");
			break;
		case 96:
			tip = tip.Replace("30", "20");
			break;
		case 97:
			tip = tip.Replace("45", "30");
			break;
		case 98:
			tip = CalamityUtils.GetText("Vanilla.BuffDescription.BeetleMight").Format(10, 5);
			break;
		case 99:
			tip = CalamityUtils.GetText("Vanilla.BuffDescription.BeetleMight").Format(20, 10);
			break;
		case 100:
			tip = CalamityUtils.GetText("Vanilla.BuffDescription.BeetleMight").Format(30, 15);
			break;
		case 88:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.ChaosState");
			break;
		case 7:
			tip = tip.Replace("20", "10");
			break;
		case 104:
			tip = tip.Replace("25", "15");
			break;
		case 147:
		{
			string[] tooltipLines = tip.Split("\n");
			string result = "";
			string[] array = tooltipLines;
			foreach (string tooltip in array)
			{
				result = ((!(tooltip == tooltipLines[0])) ? (result + "\n" + tooltip) : (result + CalamityUtils.GetText("Vanilla.BuffDescription.Banner")));
			}
			tip = result;
			break;
		}
		case 179:
			tip = tip.Replace("15", "7.5");
			break;
		case 180:
			tip = tip.Replace("30", "15");
			break;
		case 181:
			tip = tip.Replace("45", "22.5");
			break;
		case 148:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.Rabies");
			break;
		case 192:
			tip = tip.Replace("20", "10");
			break;
		case 3:
			tip = tip.Replace("25", "15");
			break;
		case 78:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbueConfetti");
			break;
		case 73:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbueCursedFlames");
			break;
		case 74:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbueFire");
			break;
		case 75:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbueGold");
			break;
		case 76:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbueIchor");
			break;
		case 77:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbueNanites");
			break;
		case 79:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbuePoison");
			break;
		case 71:
			tip = CalamityUtils.GetTextValue("Vanilla.BuffDescription.WeaponImbueVenom");
			break;
		case 26:
			tip = Language.GetTextValue("BuffDescription.WellFed");
			break;
		case 206:
			tip = Language.GetTextValue("BuffDescription.WellFed2");
			break;
		case 207:
			tip = Language.GetTextValue("BuffDescription.WellFed3");
			break;
		}
	}
}
