using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class GodSlayerInferno : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 500f,
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
		player.Calamity().godSlayerInferno = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().godSlayerInferno = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new SparkParticle(drawInfo.drawPlayer.Calamity().RandomDebuffVisualSpot, new Vector2(0f, Main.rand.NextFloat(-5f, 5f)), affectedByGravity: false, Main.rand.Next(11, 13), Main.rand.NextFloat(0.2f, 0.5f), Main.rand.NextBool(7) ? Color.Aqua : Color.Fuchsia));
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2)), new Vector2(0f, Main.rand.NextFloat(-5f, 5f)), affectedByGravity: false, Main.rand.Next(11, 13), Main.rand.NextFloat(0.2f, 0.5f), Main.rand.NextBool(7) ? Color.Aqua : Color.Fuchsia));
		}
		Lighting.AddLight(npc.position, 0.1f, 0f, 0.135f);
	}
}
