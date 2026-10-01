using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Providence;

public static class ProvUtils
{
	public static bool StandardAI()
	{
		if (CalamityGlobalNPC.holyBoss == -1 || !Main.npc[CalamityGlobalNPC.holyBoss].Calamity().CurrentlyEnraged)
		{
			return !Main.zenithWorld;
		}
		return false;
	}

	public static int CalculateProvidenceDamage(this int damage)
	{
		if (Main.zenithWorld)
		{
			return 0;
		}
		if (CalamityGlobalNPC.holyBoss != -1 && Main.npc[CalamityGlobalNPC.holyBoss].Calamity().CurrentlyEnraged)
		{
			damage *= 2;
		}
		if (CalamityGlobalNPC.holyBossAttacker != -1 && Main.npc[CalamityGlobalNPC.holyBossAttacker].active)
		{
			damage = (int)((float)damage * 1.25f);
		}
		return damage;
	}

	public static Color GetColorBasedOnEnrage(int Alpha, bool Outline = false)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return GetColorBasedOnEnrage(!Main.IsItDay() && !Main.remixWorld, Alpha, Outline);
	}

	public static Color GetColorBasedOnEnrage(bool Night, int Alpha, bool Outline = false)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Color FinalColor = default(Color);
		((Color)(ref FinalColor))._002Ector(255, (!Outline) ? 155 : 0, (!Outline) ? 25 : 0, Alpha);
		if (Night)
		{
			((Color)(ref FinalColor))._002Ector(100, Outline ? 250 : 200, Outline ? 200 : 250, Alpha);
		}
		return FinalColor;
	}

	public static Color GetProjectileColor(Color givenLightColor, bool Outline = false)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		int alpha = 0;
		if (!Outline && Main.zenithWorld && Main.GlobalTimeWrappedHourly % 6f >= 2f && Main.GlobalTimeWrappedHourly % 6f < 3f)
		{
			float colorBrightness = (float)(((Color)(ref givenLightColor)).R + ((Color)(ref givenLightColor)).G + ((Color)(ref givenLightColor)).B / 3) / 255f;
			alpha = (int)MathHelper.Lerp(0f, 155f, colorBrightness);
		}
		else
		{
			alpha = 100;
		}
		Color FinalColor = default(Color);
		((Color)(ref FinalColor))._002Ector(255, (!Outline) ? 255 : 0, (!Outline) ? 255 : 0, alpha);
		if (CalamityGlobalNPC.holyBoss == -1)
		{
			return FinalColor;
		}
		if (Main.zenithWorld)
		{
			if (Main.GlobalTimeWrappedHourly % 6f >= 5f)
			{
				((Color)(ref FinalColor))._002Ector(Outline ? 100 : 150, Outline ? 150 : 100, 250, alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 4f)
			{
				((Color)(ref FinalColor))._002Ector(100, Outline ? 250 : 200, Outline ? 200 : 250, alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 3f)
			{
				((Color)(ref FinalColor))._002Ector(Outline ? 200 : 100, 250, 100, alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 2f)
			{
				((Color)(ref FinalColor))._002Ector(255, (!Outline) ? 255 : 0, (!Outline) ? 255 : 0, alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 1f)
			{
				((Color)(ref FinalColor))._002Ector(250, 150, Outline ? 150 : 100, alpha);
			}
			else
			{
				((Color)(ref FinalColor))._002Ector(250, 100, Outline ? 200 : 100, alpha);
			}
		}
		else if (!StandardAI())
		{
			((Color)(ref FinalColor))._002Ector(100, Outline ? 250 : 200, Outline ? 200 : 250, alpha);
		}
		if (Outline)
		{
			FinalColor *= 0.1f;
			return FinalColor;
		}
		return FinalColor;
	}

	public static Color GetProjectileColor(int Alpha, bool Outline = false)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		Color FinalColor = default(Color);
		((Color)(ref FinalColor))._002Ector(255, (!Outline) ? 155 : 0, (!Outline) ? 25 : 0, Alpha);
		if (CalamityGlobalNPC.holyBoss == -1)
		{
			return FinalColor;
		}
		if (Main.zenithWorld)
		{
			if (Main.GlobalTimeWrappedHourly % 6f >= 5f)
			{
				((Color)(ref FinalColor))._002Ector(Outline ? 100 : 150, Outline ? 150 : 100, 250, Alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 4f)
			{
				((Color)(ref FinalColor))._002Ector(100, Outline ? 250 : 200, Outline ? 200 : 250, Alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 3f)
			{
				((Color)(ref FinalColor))._002Ector(Outline ? 200 : 100, 250, 100, Alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 2f)
			{
				((Color)(ref FinalColor))._002Ector(255, (!Outline) ? 155 : 0, (!Outline) ? 25 : 0, Alpha);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 1f)
			{
				((Color)(ref FinalColor))._002Ector(250, 150, Outline ? 150 : 100, Alpha);
			}
			else
			{
				((Color)(ref FinalColor))._002Ector(250, 100, Outline ? 200 : 100, Alpha);
			}
		}
		else if (!StandardAI())
		{
			((Color)(ref FinalColor))._002Ector(100, Outline ? 250 : 200, Outline ? 200 : 250, Alpha);
		}
		if (Outline)
		{
			FinalColor *= 0.1f;
			return FinalColor;
		}
		return FinalColor;
	}

	public static int GetDustID(bool? Night = null)
	{
		int DustType = 244;
		if (Night.HasValue)
		{
			if (Night.Value)
			{
				DustType = 185;
			}
		}
		else if (Main.zenithWorld)
		{
			DustType = ((Main.GlobalTimeWrappedHourly % 6f >= 5f) ? 62 : ((Main.GlobalTimeWrappedHourly % 6f >= 4f) ? 185 : ((Main.GlobalTimeWrappedHourly % 6f >= 3f) ? 61 : ((Main.GlobalTimeWrappedHourly % 6f >= 2f) ? 244 : ((!(Main.GlobalTimeWrappedHourly % 6f >= 1f)) ? 60 : 158)))));
		}
		else if (!StandardAI())
		{
			DustType = 185;
		}
		return DustType;
	}

	public static void ApplyGFBDamage(Projectile proj, int BaseDuration, int NegativeHealValue)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.zenithWorld)
		{
			return;
		}
		int index = Player.FindClosest(proj.position, proj.width, proj.height);
		Player player = Main.player[index];
		if (player != null && proj.Colliding(proj.Hitbox, player.Hitbox))
		{
			ApplyDebuffs(player, BaseDuration, NegativeHealValue);
			if (proj.type == ModContent.ProjectileType<HolyBurnOrb>())
			{
				proj.Kill();
			}
		}
	}

	public static void ApplyDebuffs(Player Target, int BaseDuration, int NegativeHealValue = 0)
	{
		int BuffType = ModContent.BuffType<HolyFlames>();
		float Multiplier = 1f;
		if (Main.zenithWorld)
		{
			if (Main.GlobalTimeWrappedHourly % 6f >= 5f)
			{
				BuffType = ModContent.BuffType<Shadowflame>();
				Multiplier = 2f;
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 4f)
			{
				BuffType = ModContent.BuffType<Nightwither>();
				Multiplier = 1.5f;
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 3f)
			{
				BuffType = 39;
				Multiplier = 2.5f;
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 2f)
			{
				BuffType = ModContent.BuffType<HolyFlames>();
				Multiplier = 1.5f;
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 1f)
			{
				BuffType = ModContent.BuffType<Dragonfire>();
			}
			else
			{
				BuffType = ModContent.BuffType<BrimstoneFlames>();
				Multiplier = 2f;
			}
		}
		Target.AddBuff(BuffType, (int)((float)BaseDuration * Multiplier));
		if (Main.zenithWorld)
		{
			if (CalamityGlobalNPC.holyBossAttacker != -1 && Main.npc[CalamityGlobalNPC.holyBossAttacker].active)
			{
				NegativeHealValue *= 2;
			}
			Target.HealEffect(-1 * NegativeHealValue, broadcast: false);
			Target.statLife -= NegativeHealValue;
			if (Target.statLife < 0)
			{
				PlayerDeathReason CustomSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ProvidenceAntiHealing").ToNetworkText(Target.name));
				Target.KillMe(CustomSource, NegativeHealValue, 0);
			}
			NetMessage.SendData(66, -1, -1, null, Target.whoAmI, NegativeHealValue);
			Target.AddBuff(ModContent.BuffType<Vaporfied>(), (int)((float)BaseDuration * Multiplier));
		}
	}
}
