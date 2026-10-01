using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class BurningBlood : ModBuff
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
		player.Calamity().burningBlood = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().burningBlood = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(11))
		{
			int bloodLifetime = Main.rand.Next(22, 36);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
			bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
			if (Main.rand.NextBool(15))
			{
				bloodScale *= 1.3f;
			}
			float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 1.5f);
			Vector2 bloodVelocity = Main.rand.NextVector2Unit() * 2f * randomSpeedMultiplier;
			bloodVelocity.Y -= 5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(Player.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
		for (int i = 0; i < 2; i++)
		{
			float rot = MathHelper.ToRadians((float)(i * 280));
			Vector2 offset = Utils.RotatedBy(new Vector2(0.1f, 0f), (double)(rot * Main.rand.NextFloat(0.08f, 0.05f)), default(Vector2));
			Dust.NewDustPerfect(Player.Calamity().RandomDebuffVisualSpot + offset, 5).scale = Main.rand.NextFloat(0.6f, 0.7f);
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, Main.rand.NextBool(8) ? 296 : 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.25f);
			dust.noGravity = true;
			dust.velocity *= 1.3f;
			dust.velocity.Y -= 0.5f;
		}
		Lighting.AddLight(npc.Center, 0.08f, 0f, 0f);
	}
}
