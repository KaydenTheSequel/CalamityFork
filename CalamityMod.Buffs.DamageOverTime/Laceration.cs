using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Laceration : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 400f
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
		player.Calamity().laceration = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().laceration = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = drawInfo.drawPlayer.Calamity();
		if (Main.rand.NextBool())
		{
			Vector2 randVel = Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * Main.rand.NextFloat(0.3f, 1f);
			Dust.NewDustPerfect(modPlayer.RandomDebuffVisualSpot, (!ChildSafety.Disabled) ? 16 : 5, randVel * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(0.6f, 0.9f)).noGravity = false;
			GeneralParticleHandler.SpawnParticle(new AltSparkParticle(modPlayer.RandomDebuffVisualSpot, randVel + new Vector2(0f, -4f), affectedByGravity: true, 12, Main.rand.NextFloat(0.25f, 0.6f), ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed) * 0.5f));
		}
		if (Main.rand.NextBool(8))
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(modPlayer.RandomDebuffVisualSpot, new Vector2(0f, 4f) * Main.rand.NextFloat(0.5f, 0.7f), affectedByGravity: true, 16, Main.rand.NextFloat(0.55f, 0.8f), ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed) * 0.8f, AddativeBlend: false, needed: false, GlowCenter: false));
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		Vector2 randVel = Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * Main.rand.NextFloat(0.3f, 1f);
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new AltSparkParticle(npcSize, randVel + new Vector2(0f, -4f), affectedByGravity: true, 12, Main.rand.NextFloat(0.25f, 0.6f), ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed) * 0.5f));
		}
		else
		{
			Dust.NewDustPerfect(npcSize, (!ChildSafety.Disabled) ? 16 : 5, randVel * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(0.2f, 0.6f)).noGravity = false;
		}
		if (Main.rand.NextBool(8))
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(npcSize, new Vector2(0f, 4f) * Main.rand.NextFloat(0.5f, 0.7f), affectedByGravity: true, 16, Main.rand.NextFloat(0.55f, 0.8f), ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed) * 0.8f, AddativeBlend: false, needed: false, GlowCenter: false));
		}
	}
}
