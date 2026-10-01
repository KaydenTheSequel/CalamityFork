using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class CrushDepth : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 100f,
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
		player.Calamity().crushDepth = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().crushDepth = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(30))
		{
			int bloodLifetime = Main.rand.Next(22, 36);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
			bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
			if (Main.rand.NextBool(15))
			{
				bloodScale *= 1.3f;
			}
			float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 1.5f);
			Vector2 bloodVelocity = Main.rand.NextVector2Unit() * 2f * randomSpeedMultiplier;
			bloodVelocity.Y -= 5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(Player.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
		Dust water = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, Main.rand.NextBool(3) ? 104 : 186, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.4f);
		water.noGravity = true;
		water.velocity *= 0.75f;
		water.velocity.X *= 0.75f;
		water.velocity.Y--;
		if (Main.rand.NextBool(4))
		{
			water.noGravity = false;
			water.scale *= 0.2f;
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(30))
		{
			int bloodLifetime = Main.rand.Next(22, 36);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
			bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
			if (Main.rand.NextBool(15))
			{
				bloodScale *= 1.3f;
			}
			Vector2 relativePosition = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 1.5f);
			Vector2 bloodVelocity = Main.rand.NextVector2Unit() * 2f * randomSpeedMultiplier;
			bloodVelocity.Y -= 5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(relativePosition, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
		Dust water = Dust.NewDustDirect(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, Main.rand.NextBool(3) ? 104 : 186, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.4f);
		water.noGravity = true;
		water.velocity *= 0.75f;
		water.velocity.X = water.velocity.X * 0.75f;
		water.velocity.Y = water.velocity.Y - 1f;
		if (Main.rand.NextBool(4))
		{
			water.noGravity = false;
			water.scale *= 0.2f;
		}
	}
}
