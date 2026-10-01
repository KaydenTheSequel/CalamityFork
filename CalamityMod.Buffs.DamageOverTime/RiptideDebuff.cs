using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class RiptideDebuff : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 30f,
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
		player.Calamity().riptide = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().riptide = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = Player.Calamity();
		if (Main.rand.NextBool(14))
		{
			Dust dust = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, 165, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);
			dust.noGravity = false;
			dust.velocity *= 1.2f;
			dust.velocity.Y += 0.8f;
			drawInfo.DustCache.Add(dust.dustIndex);
		}
		if (Main.rand.NextBool(9))
		{
			Gore gore = Gore.NewGorePerfect(Player.GetSource_FromAI(), modPlayer.RandomDebuffVisualSpot, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 411);
			gore.timeLeft = 4 + Main.rand.Next(7);
			gore.scale = Main.rand.NextFloat(0.6f, 1f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		if (Main.rand.NextBool(9))
		{
			Dust.NewDustPerfect(npcSize, 76, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.2f, 0.6f)).color = (Main.rand.NextBool(3) ? Color.LightBlue : Color.LightSkyBlue);
		}
		if (Main.rand.NextBool(8))
		{
			Gore gore = Gore.NewGorePerfect(npc.GetSource_FromAI(), npcSize, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 411);
			gore.timeLeft = 4 + Main.rand.Next(7);
			gore.scale = Main.rand.NextFloat(0.6f, 1f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
	}
}
