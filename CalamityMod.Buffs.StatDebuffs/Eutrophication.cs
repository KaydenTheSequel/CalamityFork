using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class Eutrophication : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().eutrophication = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().eutrophication = true;
		if ((CalamityNPCSets.ResistSlowingDebuffsAndOtherSpecialEffects[npc.type] || npc.boss) && npc.Calamity().debuffResistanceTimer <= 0)
		{
			npc.Calamity().debuffResistanceTimer = 1800 + npc.buffTime[buffIndex];
		}
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(5))
		{
			Dust dust = Dust.NewDustDirect(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, Main.rand.NextBool(4) ? 56 : 33, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), Main.rand.NextFloat(0.2f, 1.5f));
			if (dust.type == 56)
			{
				dust.velocity.Y += 2f;
				dust.velocity.X *= 0.7f;
				dust.noGravity = false;
			}
			else
			{
				dust.velocity.Y++;
				dust.noGravity = false;
			}
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Main.rand.NextBool(4) ? 56 : 33, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), Main.rand.NextFloat(0.2f, 1.5f));
			if (dust.type == 56)
			{
				dust.velocity.Y += 2f;
				dust.velocity.X *= 0.7f;
				dust.noGravity = false;
			}
			else
			{
				dust.velocity.Y++;
				dust.noGravity = false;
			}
		}
	}
}
