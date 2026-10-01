using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class VermillionFlux : ModBuff
{
	public static DebuffData debuffData = new DebuffData(DebuffData.DebuffBehavior.Electric)
	{
		EnemyLostRegen = 100f,
		ElectricDebuffScaling = 1f
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
		player.Calamity().vermillionFlux = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().vermillionFlux = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		Player player = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = player.Calamity();
		bool moving = player.controlLeft || player.controlRight;
		if ((!moving && Main.rand.NextBool(3)) | moving)
		{
			if (Main.rand.NextBool())
			{
				int sparkLifetime = Main.rand.Next(15, 23);
				Vector2 sparkVel = Vector2.UnitY * -10f;
				float maxRotationDeviance = 0.4f;
				float rotationAngle = Main.rand.NextFloat(0f - maxRotationDeviance, maxRotationDeviance);
				sparkVel = sparkVel.RotatedBy(rotationAngle) * Main.rand.NextFloat(0.3f, 1f);
				float sparkScale = Main.rand.NextFloat(0.007f, 0.015f);
				Vector2 compensatedSparkVel = default(Vector2);
				((Vector2)(ref compensatedSparkVel))._002Ector(sparkVel.X - player.velocity.X * 0.12f, sparkVel.Y);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(modPlayer.RandomDebuffVisualSpot, compensatedSparkVel, affectedByGravity: true, sparkLifetime, sparkScale, Main.rand.NextBool() ? Color.Red : Color.Crimson, new Vector2(0.5f, 1.3f)));
			}
			if (Main.rand.NextBool())
			{
				Dust.NewDustPerfect(modPlayer.RandomDebuffVisualSpot, 219, Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.2f, 0.6f));
			}
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		_ = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-2f) : (-8f)), MathHelper.ToRadians(Main.rand.NextBool(3) ? 10f : 35f)) * Main.rand.NextFloat(0.1f, 1.9f);
		if (Main.rand.NextBool(4))
		{
			int sparkLifetime = Main.rand.Next(15, 23);
			Vector2 sparkVel = Vector2.UnitY * -9f;
			float maxRotationDeviance = 0.4f;
			float rotationAngle = Main.rand.NextFloat(0f - maxRotationDeviance, maxRotationDeviance);
			sparkVel = sparkVel.RotatedBy(rotationAngle) * Main.rand.NextFloat(0.3f, 1f);
			float sparkScale = Main.rand.NextFloat(0.007f, 0.015f);
			Vector2 compensatedSparkVel = default(Vector2);
			((Vector2)(ref compensatedSparkVel))._002Ector(sparkVel.X - npc.velocity.X * 0.12f, sparkVel.Y);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(npcSize, compensatedSparkVel, affectedByGravity: true, sparkLifetime, sparkScale, Main.rand.NextBool() ? Color.Red : Color.Crimson, new Vector2(0.5f, 1.3f)));
		}
		Color newColor;
		if (Main.rand.NextBool(3))
		{
			Vector2? velocity = Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f);
			newColor = default(Color);
			Dust.NewDustPerfect(npcSize, 219, velocity, 0, newColor, Main.rand.NextFloat(0.2f, 0.6f));
		}
		Vector2 center = npc.Center;
		newColor = Color.Red;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
	}
}
