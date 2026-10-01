using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class AuricRebuke : ModBuff
{
	public static DebuffData debuffData = new DebuffData(DebuffData.DebuffBehavior.Electric)
	{
		EnemyLostRegen = 200f,
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
		player.Calamity().auricRebuke = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().auricRebuke = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		Player player = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = player.Calamity();
		bool moving = player.controlLeft || player.controlRight;
		if ((!moving && Main.rand.NextBool(3)) | moving)
		{
			if (Main.rand.NextBool())
			{
				int sparkLifetime = Main.rand.Next(11, 14);
				Vector2 sparkVel = Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(modPlayer.RandomDebuffVisualSpot, sparkVel, affectedByGravity: false, sparkLifetime, Main.rand.NextFloat(0.008f, 0.012f), Color.Lerp(Color.Cyan, Color.Lavender, Main.rand.NextFloat(0f, 0.6f)), new Vector2(1f, 0.7f), quickShrink: true));
			}
			Dust.NewDustPerfect(modPlayer.RandomDebuffVisualSpot, 278, Utils.RotatedByRandom(new Vector2(1.5f, 1.5f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.1f, 0.6f)).color = Color.Lerp(Color.Cyan, Color.Lavender, Main.rand.NextFloat(0f, 0.6f));
			if (Main.rand.NextBool(6))
			{
				Dust.NewDustPerfect(modPlayer.RandomDebuffVisualSpot, 278, Utils.RotatedByRandom(new Vector2(4.5f, 4.5f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.8f, 0.95f)).color = (Main.rand.NextBool(4) ? Color.Lavender : Color.Cyan);
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
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		_ = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-2f) : (-8f)), MathHelper.ToRadians(Main.rand.NextBool(3) ? 10f : 35f)) * Main.rand.NextFloat(0.1f, 1.9f);
		if (Main.rand.NextBool(4))
		{
			int sparkLifetime = Main.rand.Next(11, 14);
			Vector2 sparkVel = Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(npcSize, sparkVel, affectedByGravity: false, sparkLifetime, Main.rand.NextFloat(0.008f, 0.012f), Color.Lerp(Color.Cyan, Color.Lavender, Main.rand.NextFloat(0f, 0.6f)), new Vector2(1f, 0.7f), quickShrink: true));
		}
		if (Main.rand.NextBool())
		{
			Dust.NewDustPerfect(npcSize, 278, Utils.RotatedByRandom(new Vector2(1.5f, 1.5f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.1f, 0.6f)).color = Color.Lerp(Color.Cyan, Color.Lavender, Main.rand.NextFloat(0f, 0.6f));
		}
		if (Main.rand.NextBool(10))
		{
			Dust.NewDustPerfect(npcSize, 278, Utils.RotatedByRandom(new Vector2(4.5f, 4.5f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.8f, 0.95f)).color = (Main.rand.NextBool(4) ? Color.Lavender : Color.Cyan);
		}
	}
}
