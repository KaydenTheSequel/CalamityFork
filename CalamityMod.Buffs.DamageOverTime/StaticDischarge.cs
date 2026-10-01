using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class StaticDischarge : ModBuff
{
	public static DebuffData debuffData = new DebuffData(DebuffData.DebuffBehavior.Electric)
	{
		EnemyLostRegen = 5f,
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
		player.Calamity().staticDischarge = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().staticDischarge = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		Player player = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = player.Calamity();
		bool moving = player.controlLeft || player.controlRight;
		if (((!moving && Main.rand.NextBool(3)) | moving) && Main.rand.NextBool(3))
		{
			Dust.NewDustPerfect(modPlayer.RandomDebuffVisualSpot, 278, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.2f, 0.6f)).color = (Main.rand.NextBool(3) ? Color.Yellow : Color.LightSkyBlue);
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
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		_ = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextBool(4) ? (-2f) : (-8f)), MathHelper.ToRadians(Main.rand.NextBool(3) ? 10f : 35f)) * Main.rand.NextFloat(0.1f, 1.9f);
		if (Main.rand.NextBool(4))
		{
			Dust.NewDustPerfect(npcSize, 278, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.3f, 0.7f), 0, default(Color), Main.rand.NextFloat(0.2f, 0.6f)).color = (Main.rand.NextBool(3) ? Color.Yellow : Color.LightSkyBlue);
		}
	}
}
