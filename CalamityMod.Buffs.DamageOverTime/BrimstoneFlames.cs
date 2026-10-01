using CalamityMod.DataStructures;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class BrimstoneFlames : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 60f,
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
		player.Calamity().brimstoneFlames = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().brimstoneFlames = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo, bool hasDebuffResistance = false)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		Vector3 brimstoneDustLight = default(Vector3);
		((Vector3)(ref brimstoneDustLight))._002Ector(0.255f, 0.079f, 0.082f);
		Lighting.AddLight(Player.Center, brimstoneDustLight * 2f);
		if (Main.rand.NextBool(hasDebuffResistance ? 4 : 2))
		{
			Dust.NewDustPerfect(Player.Calamity().RandomDebuffVisualSpot, Main.rand.NextBool(3) ? 114 : ModContent.DustType<BrimstoneFlame>(), new Vector2(0f, Main.rand.NextFloat(-3f, -5f)) + Player.velocity, 0, default(Color), hasDebuffResistance ? 1.1f : 1.6f).noGravity = true;
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDustPerfect(Player.Center + new Vector2(Main.rand.NextFloat(-10f, 10f), 19f), Main.rand.NextBool() ? 90 : ModContent.DustType<BrimstoneFlame>(), new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-1f, -3f)) + Player.velocity, 0, default(Color), hasDebuffResistance ? 0.4f : 1.4f).noGravity = true;
			}
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, Main.rand.NextBool(3) ? 114 : ModContent.DustType<BrimstoneFlame>());
			dust.noGravity = true;
			dust.velocity = new Vector2(0f, Main.rand.NextFloat(-3f, -5f)) + npc.velocity;
			dust.scale = 1.6f;
			for (int i = 0; i < 3; i++)
			{
				Dust dust2 = Dust.NewDustDirect(npc.position + new Vector2(Main.rand.NextFloat(-10f, 10f), (float)(npc.height / 2)), npc.width, npc.height, Main.rand.NextBool() ? 90 : ModContent.DustType<BrimstoneFlame>());
				dust2.noGravity = true;
				dust2.velocity = new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-1f, -3f)) + npc.velocity;
				dust2.scale = 1.4f;
			}
			Lighting.AddLight(npc.position, 0.05f, 0.01f, 0.01f);
		}
	}
}
