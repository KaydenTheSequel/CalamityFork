using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class AstralInfectionDebuff : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 75f,
		SicknessDebuffScaling = 1f
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
		player.Calamity().astralInfection = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().astralInfection = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(4))
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(Player.Calamity().RandomDebuffVisualSpot, Vector2.Zero, Main.rand.NextBool() ? Color.DarkTurquoise : Color.Coral, new Vector2(1f, 1f), 0f, 0.08f, 0f, 20));
			GeneralParticleHandler.SpawnParticle(new GenericBloom(Player.Calamity().RandomDebuffVisualSpot, Vector2.Zero, Main.rand.NextBool() ? Color.DarkTurquoise : Color.Coral, 0.055f, 8));
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Vector2 position = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			Vector2 npcSize2 = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(position, Vector2.Zero, Main.rand.NextBool() ? Color.DarkTurquoise : Color.Coral, new Vector2(1f, 1f), 0f, 0.1f + 7E-07f * (float)npc.width * (float)npc.height, 0f, 20));
			GeneralParticleHandler.SpawnParticle(new GenericBloom(npcSize2, Vector2.Zero, Main.rand.NextBool() ? Color.DarkTurquoise : Color.Coral, 0.065f + 7E-07f * (float)npc.width * (float)npc.height, 8));
		}
	}
}
