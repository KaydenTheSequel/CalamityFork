using CalamityMod.DataStructures;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Vaporfied : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 30f,
		MinimumDamageTickSize = 6,
		MultiplierDamageTickSize = 0f
	};

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().vaporfied = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().vaporfied = true;
		if ((CalamityNPCSets.ResistSlowingDebuffsAndOtherSpecialEffects[npc.type] || npc.boss) && npc.Calamity().debuffResistanceTimer <= 0)
		{
			npc.Calamity().debuffResistanceTimer = 1800 + npc.buffTime[buffIndex];
		}
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		int dustType = Utils.SelectRandom<int>(Main.rand, 246, 242, 229, 226, 247, 187, 234);
		if (Main.rand.NextBool(4))
		{
			Dust dust = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, dustType, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
			dust.noGravity = true;
			dust.velocity *= 1.8f;
			dust.velocity.Y -= 0.5f;
			if (Main.rand.NextBool(4))
			{
				dust.noGravity = false;
				dust.scale *= 0.5f;
			}
			drawInfo.DustCache.Add(dust.dustIndex);
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		int dustType = Utils.SelectRandom<int>(Main.rand, 246, 242, 229, 226, 247, 187, 234);
		if (Main.rand.Next(5) < 4)
		{
			Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, dustType, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
			dust.noGravity = true;
			dust.velocity *= 1.8f;
			dust.velocity.Y -= 0.5f;
			if (Main.rand.NextBool(4))
			{
				dust.noGravity = false;
				dust.scale *= 0.5f;
			}
		}
	}
}
