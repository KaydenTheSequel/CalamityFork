using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class HolyFlames : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 300f,
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
		player.Calamity().holyFlames = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().holyFlames = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(2))
		{
			Vector2 Vect = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(Player.Calamity().RandomDebuffVisualSpot, Vect, Main.rand.NextBool() ? Color.Coral : Color.OrangeRed, Color.Orange, 0.8f, 15, 2f, 1.9f));
		}
		if (Main.rand.NextBool(2))
		{
			Vector2 position = Player.position - 2f * Vector2.One;
			Vector2 dustVel = Player.velocity + new Vector2(0f, Main.rand.NextFloat(-5f, -1f));
			Dust dust = Dust.NewDustDirect(position, Player.width + 4, Player.height + 4, 87, dustVel.X, dustVel.Y);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.7f, 1.2f);
			dust.alpha = 235;
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			Vector2 position = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			Vector2 Vect = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(position, Vect, Main.rand.NextBool() ? Color.Coral : Color.OrangeRed, Color.Orange, 0.8f, 15, 2f, 1.9f));
		}
		if (Main.rand.NextBool(4))
		{
			Vector2 position2 = npc.position - 2f * Vector2.One;
			Vector2 dustVel = npc.velocity + new Vector2(0f, Main.rand.NextFloat(-5f, -1f));
			Dust dust = Dust.NewDustDirect(position2, npc.width + 4, npc.height + 4, 87, dustVel.X, dustVel.Y);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.7f, 1.2f);
			dust.alpha = 235;
		}
		Lighting.AddLight(npc.position, 0.25f, 0.25f, 0.1f);
	}
}
