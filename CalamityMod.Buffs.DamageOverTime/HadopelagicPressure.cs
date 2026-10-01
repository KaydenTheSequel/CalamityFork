using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class HadopelagicPressure : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 400f,
		WaterDebuffScaling = 1f
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
		player.Calamity().hadopelagicPressure = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().hadopelagicPressure = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		Dust water = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, 390, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.4f);
		water.noGravity = true;
		water.velocity *= 0.75f;
		water.velocity.X *= 0.75f;
		water.velocity.Y--;
		if (Main.rand.NextBool(4))
		{
			water.noGravity = false;
			water.scale *= 0.5f;
		}
		if (Main.rand.NextBool(4))
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(Player.Calamity().RandomDebuffVisualSpot, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-3f, -4f)), Main.rand.NextBool() ? Color.DeepSkyBlue : Color.MediumBlue, new Vector2(0.8f, 1f), 0f, 0.09f, 0f, 45));
		}
		if (Main.rand.NextBool(10))
		{
			Color smokeColor = Color.MediumBlue;
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(Player.Calamity().RandomDebuffVisualSpot, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), smokeColor, 40, Main.rand.NextFloat(0.3f, 0.4f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), glowing: false, 0f, required: true));
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		if (Main.rand.NextBool(13))
		{
			Color smokeColor = Color.MediumBlue;
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(npcSize, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), smokeColor, 40, Main.rand.NextFloat(0.3f, 0.4f) + 1.3E-07f * (float)npc.width * (float)npc.height, 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), glowing: false, 0f, required: true));
		}
		Dust water = Dust.NewDustDirect(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, 360, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.4f);
		water.noGravity = true;
		water.velocity *= 0.75f;
		water.velocity.X = water.velocity.X * 0.75f;
		water.velocity.Y = water.velocity.Y - 1f;
		if (Main.rand.NextBool(4))
		{
			water.noGravity = false;
			water.scale *= 0.5f;
		}
		if (Main.rand.NextBool(6))
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(npcSize, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-4.5f, -6f)), Main.rand.NextBool() ? Color.DeepSkyBlue : Color.MediumBlue, new Vector2(1f), 0f, 0.12f + 7E-07f * (float)npc.width * (float)npc.height, 0f, 35));
		}
	}
}
