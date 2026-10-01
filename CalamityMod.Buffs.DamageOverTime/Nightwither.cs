using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Nightwither : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 200f,
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
		player.Calamity().nightwither = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().nightwither = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(2))
		{
			Vector2 Vect = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(Player.Calamity().RandomDebuffVisualSpot, Vect, Main.rand.NextBool() ? Color.Cyan : Color.Turquoise, Color.PaleTurquoise, 0.8f, 15, 2f, 1.9f));
		}
		for (int i = 0; i < 2; i++)
		{
			Vector2 position = Player.position - 2f * Vector2.One;
			Vector2 dustVel = Player.velocity + new Vector2(0f, Main.rand.NextFloat(-11f, -2f));
			Dust dust = Dust.NewDustDirect(position, Player.width + 4, Player.height + 4, Main.rand.NextBool(4) ? 300 : 323, dustVel.X, dustVel.Y);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.5f);
			dust.alpha = 235;
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		if (Main.rand.NextBool(3))
		{
			Vector2 Vect = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-5f) : (-9f)), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(npcSize, Vect, Main.rand.NextBool() ? Color.Cyan : Color.Turquoise, Color.PaleTurquoise, 0.8f, 15, 2f, 1.9f));
		}
		for (int i = 0; i < 2; i++)
		{
			Vector2 position = npc.position - 2f * Vector2.One;
			Vector2 dustVel = npc.velocity + new Vector2(0f, Main.rand.NextFloat(-11f, -2f));
			Dust dust = Dust.NewDustDirect(position, npc.width + 4, npc.height + 4, Main.rand.NextBool(4) ? 300 : 323, dustVel.X, dustVel.Y);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.5f);
			dust.alpha = 235;
		}
	}
}
