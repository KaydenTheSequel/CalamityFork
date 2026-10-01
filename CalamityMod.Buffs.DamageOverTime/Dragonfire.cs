using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Dragonfire : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 960f,
		HeatDebuffScaling = 1f
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
		player.Calamity().dragonFire = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().dragonFire = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		Player player = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = player.Calamity();
		Vector2 compensatedSparkVel = default(Vector2);
		for (int i = 0; i < 2; i++)
		{
			bool fastSpark = Main.rand.NextBool(4);
			int sparkLifetime = Main.rand.Next(3) + (fastSpark ? 7 : 14);
			Vector2 sparkVel = Vector2.UnitY * (fastSpark ? (-18f) : (-4f));
			float maxRotationDeviance = (fastSpark ? 0.16f : 0.4f);
			float rotationAngle = Main.rand.NextFloat(0f - maxRotationDeviance, maxRotationDeviance);
			sparkVel = sparkVel.RotatedBy(rotationAngle) * Main.rand.NextFloat(0.3f, 1f);
			float sparkScale = Main.rand.NextFloat(0.33f, 0.55f);
			((Vector2)(ref compensatedSparkVel))._002Ector(sparkVel.X - player.velocity.X * 0.12f, sparkVel.Y);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(modPlayer.RandomDebuffVisualSpot, compensatedSparkVel, affectedByGravity: false, sparkLifetime, sparkScale, Main.rand.NextBool() ? Color.OrangeRed : Color.Orange));
		}
		if (Main.rand.NextBool(3))
		{
			bool fastSmoke = Main.rand.NextBool();
			Color smokeColor = (Main.rand.NextBool() ? Color.Black : Color.DimGray);
			Vector2 smokeVel = Vector2.UnitY * (fastSmoke ? (-15f) : (-6f));
			float rotationAngle2 = Main.rand.NextFloat(-0.08f, 0.08f);
			smokeVel = smokeVel.RotatedBy(rotationAngle2) * Main.rand.NextFloat(0.1f, 1f);
			float smokeScale = Main.rand.NextFloat(0.4f, 1.2f);
			GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(modPlayer.RandomDebuffVisualSpot, smokeVel, Color.DimGray, smokeColor, smokeScale, 100f));
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
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		Vector2 Vect2 = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-2f) : (-8f)), MathHelper.ToRadians(Main.rand.NextBool(3) ? 10f : 35f)) * Main.rand.NextFloat(0.1f, 1.9f);
		GeneralParticleHandler.SpawnParticle(new SparkParticle(npcSize, new Vector2(Vect2.X - npc.velocity.X * 0.3f, Vect2.Y), affectedByGravity: false, 10, Main.rand.NextFloat(0.4f, 0.5f), Main.rand.NextBool() ? Color.OrangeRed : Color.Orange));
		if (Main.rand.NextBool(3))
		{
			Vector2 Vect3 = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool() ? (-3f) : (-14f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(npcSize, Vect3, Color.DimGray, Main.rand.NextBool() ? Color.Black : Color.DimGray, Main.rand.NextFloat(0.2f, 1.2f), 100f));
		}
		Lighting.AddLight(npc.position, 0.1f, 0f, 0.135f);
	}
}
