using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Voidfrost : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 400f,
		ColdDebuffScaling = 1f
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
		player.Calamity().voidfrost = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().voidfrost = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo, bool hasDebuffResistance = false)
	{
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(5))
		{
			Vector2 Vect = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new SnowflakeSparkle(Player.Calamity().RandomDebuffVisualSpot, Vect, Main.rand.NextBool() ? Color.Cyan : Color.DarkBlue, Color.DodgerBlue, 0.4f, 15, 2f, 1.9f));
		}
		if (Main.rand.NextBool(40))
		{
			Vector2 Vect2 = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(Player.Calamity().RandomDebuffVisualSpot, Vect2, new Color(172, 238, 255), new Color(145, 170, 188), Main.rand.NextFloat(0.5f, 1.5f), 245 - Main.rand.Next(50), 0.02f));
		}
		Vector2 position = Player.position - 2f * Vector2.One;
		Vector2 dustVel = Player.velocity + new Vector2(0f, Main.rand.NextFloat(-11f, -2f));
		Dust dust = Dust.NewDustDirect(position, Player.width + 4, Player.height + 4, Main.rand.NextBool(4) ? 20 : 113, dustVel.X, dustVel.Y);
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(1f, 0.3f);
		dust.alpha = 10;
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		if (Main.rand.NextBool(5))
		{
			Vector2 Vect = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new SnowflakeSparkle(npcSize, Vect, Main.rand.NextBool() ? Color.Cyan : Color.DarkBlue, Color.DodgerBlue, 0.8f, 15, 2f, 1.9f));
		}
		if (Main.rand.NextBool(40))
		{
			Vector2 Vect2 = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(npcSize, Vect2, new Color(172, 238, 255), new Color(145, 170, 188), Main.rand.NextFloat(0.5f, 1.5f), 245 - Main.rand.Next(50), 0.02f));
		}
		Vector2 position = npc.position - 2f * Vector2.One;
		Vector2 dustVel = npc.velocity + new Vector2(0f, Main.rand.NextFloat(-11f, -2f));
		Dust dust = Dust.NewDustDirect(position, npc.width + 4, npc.height + 4, Main.rand.NextBool(4) ? 20 : 113, dustVel.X, dustVel.Y);
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(1f, 0.3f);
		dust.alpha = 10;
	}
}
