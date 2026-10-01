using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Plague : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 100f,
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
		player.Calamity().plague = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().plague = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		float numberOfDusts = 2f;
		float rotFactor = 360f / numberOfDusts;
		int particleAmt = (Player.Calamity().alchFlask ? 1 : 2);
		int dustSpawnAmt = (Player.Calamity().alchFlask ? 4 : 7);
		if (Player.miscCounter % 4 != 0)
		{
			return;
		}
		for (int i = 0; i < particleAmt; i++)
		{
			float pulseScale = Main.rand.NextFloat(Player.Calamity().alchFlask ? 0.04f : 0.07f, Player.Calamity().alchFlask ? 0.12f : 0.18f);
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(Player.Calamity().RandomDebuffVisualSpot, Vector2.Zero, Main.rand.NextBool(3) ? Color.LimeGreen : Color.Green, Vector2.One, 0f, pulseScale, 0f, 20));
		}
		for (int j = 0; j < dustSpawnAmt; j++)
		{
			int DustID = (Main.rand.NextBool(30) ? 220 : 89);
			float rot = MathHelper.ToRadians((float)j * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(0.3f, 0f), (double)(rot * Main.rand.NextFloat(0.2f, 0.3f)), default(Vector2));
			Dust dust2 = Dust.NewDustPerfect(Player.Calamity().RandomDebuffVisualSpot + offset, DustID);
			dust2.scale = Main.rand.NextFloat(0.3f, 0.4f);
			if (DustID == 220)
			{
				dust2.scale = Main.rand.NextFloat(1f, 1.2f);
			}
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(npcSize, Vector2.Zero, Main.rand.NextBool(3) ? Color.LimeGreen : Color.Green, new Vector2(1f, 1f), 0f, Main.rand.NextFloat(0.07f, 0.18f) + 7E-07f * (float)npc.width * (float)npc.height, 0f, 15));
			for (int i = 0; i < 4; i++)
			{
				int DustID = (Main.rand.NextBool(30) ? 220 : 89);
				Dust dust2 = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID);
				dust2.scale = Main.rand.NextFloat(0.3f, 0.4f);
				if (DustID == 220)
				{
					dust2.scale = Main.rand.NextFloat(1f, 1.2f);
				}
			}
		}
		Lighting.AddLight(npc.position, 0.07f, 0.15f, 0.01f);
	}
}
