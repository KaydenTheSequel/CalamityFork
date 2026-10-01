using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class BrainRot : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 40f,
		SicknessDebuffScaling = 1f
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
		player.Calamity().brainRot = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().brainRot = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		Player player = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = player.Calamity();
		if (Main.rand.NextBool())
		{
			int dustType = (Main.rand.NextBool() ? 184 : 18);
			Dust dust = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-5f, 5f), -16f + Main.rand.NextFloat(-5f, 5f)), dustType);
			dust.scale = ((dustType == 18) ? 0.6f : 1.2f);
			dust.velocity = Utils.RotatedByRandom(new Vector2(2f, 2f), 360.0) * Main.rand.NextFloat(0.3f, 0.7f) + player.velocity;
			dust.noGravity = true;
			dust.alpha = 35;
		}
		if (Main.rand.NextBool(4))
		{
			Dust dust2 = Dust.NewDustPerfect(modPlayer.RandomDebuffVisualSpot, 18);
			dust2.noGravity = true;
			dust2.velocity = Vector2.Zero;
			dust2.alpha = 90;
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			Vector2 position = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			int dustType = (Main.rand.NextBool() ? 184 : 18);
			Dust dust = Dust.NewDustPerfect(position, dustType);
			dust.scale = ((dustType == 18) ? 0.6f : 1.2f);
			dust.noGravity = true;
			dust.velocity = Vector2.Zero;
			dust.alpha = Main.rand.Next(35, 90);
		}
	}
}
