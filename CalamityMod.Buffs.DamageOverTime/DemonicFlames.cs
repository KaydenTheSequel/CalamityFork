using System;
using CalamityMod.DataStructures;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class DemonicFlames : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 60f,
		HeatDebuffScaling = 1f,
		NPCLifeRegenMethod = DemonFlamesNPCLifeRegen
	};

	public static void DemonFlamesNPCLifeRegen(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		int baseDemonicFlamesDoTValue = (int)Math.Max(npc.Calamity().ActiveHeatDebuffMultiplier.ApplyTo(npc.Calamity().demonicFlamesBonusDamage), npc.Calamity().demonicFlamesBonusDamage);
		npc.Calamity().ApplyDPSDebuff(baseDemonicFlamesDoTValue, baseDemonicFlamesDoTValue / 15, ref npc.lifeRegen, ref damage);
	}

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
		player.Calamity().demonicFlames = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().demonicFlames = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo, bool hasDebuffResistance = false)
	{
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		Color newColor;
		if (Main.rand.NextBool())
		{
			Vector2 position = Player.position;
			int width = Player.width;
			int height = Player.height;
			int type = ModContent.DustType<LightDust>();
			newColor = default(Color);
			Dust dust = Dust.NewDustDirect(position, width, height, type, 0f, 0f, 0, newColor);
			dust.noGravity = true;
			dust.velocity = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextFloat(-4f, -8f)), 0.30000001192092896) + Player.velocity;
			dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
			dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
			dust.noLightEmittence = true;
			Vector2 sparkVel = default(Vector2);
			for (int i = 0; i < 2; i++)
			{
				((Vector2)(ref sparkVel))._002Ector(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-1f, -3f));
				GeneralParticleHandler.SpawnParticle(new VelChangingSpark(Player.Center + new Vector2(Main.rand.NextFloat(-10f, 10f), (float)(Player.height / 2)), sparkVel + Player.velocity, new Vector2((0f - sparkVel.X) * 0.5f, sparkVel.Y * 2f) * 3.5f, "CalamityMod/Particles/SmallBloom", Main.rand.Next(13, 21), Main.rand.NextFloat(0.1f, 0.25f) * 0.5f, (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet) * 0.75f, new Vector2(0.7f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.45f, 0.055f));
			}
		}
		Vector2 center = Player.Center;
		newColor = Color.MediumOrchid;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		Color newColor;
		if (Main.rand.NextBool(3))
		{
			Vector2 position = npc.position;
			int width = npc.width;
			int height = npc.height;
			int type = ModContent.DustType<LightDust>();
			newColor = default(Color);
			Dust dust = Dust.NewDustDirect(position, width, height, type, 0f, 0f, 0, newColor);
			dust.noGravity = true;
			dust.velocity = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextFloat(-4f, -8f)), 0.30000001192092896) + npc.velocity;
			dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
			dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
			dust.noLightEmittence = true;
			Vector2 sparkVel = default(Vector2);
			for (int i = 0; i < 2; i++)
			{
				((Vector2)(ref sparkVel))._002Ector(Main.rand.NextFloat(-npc.width / 6, npc.width / 6), Main.rand.NextFloat(-npc.height / 20, -npc.height / 17));
				GeneralParticleHandler.SpawnParticle(new VelChangingSpark(npc.Center + new Vector2(Main.rand.NextFloat(-10f, 10f), (float)(npc.height / 2)) + sparkVel * 0.5f, sparkVel + npc.velocity, new Vector2((0f - sparkVel.X) * 0.5f, sparkVel.Y * 2f) * 3.5f, "CalamityMod/Particles/SmallBloom", Main.rand.Next(13, 21), Main.rand.NextFloat(0.1f, 0.25f) * MathHelper.Lerp((float)(Math.Max(npc.height, npc.width) / 120), 0.5f, 0.7f), (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet) * 0.75f, new Vector2(0.7f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.3f, 0.055f));
			}
		}
		Vector2 center = npc.Center;
		newColor = Color.MediumOrchid;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
	}
}
