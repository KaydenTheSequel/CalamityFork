using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class ElementalMix : ModBuff
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
		player.Calamity().elementalMix = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().elementalMix = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		Player player = drawInfo.drawPlayer;
		player.Calamity();
		Color effectcolor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.DeepSkyBlue, 
			1 => Color.MediumSpringGreen, 
			2 => Color.DarkOrange, 
			_ => Color.Violet, 
		});
		Vector2 speed = default(Vector2);
		((Vector2)(ref speed))._002Ector(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-2.5f, -8.3f));
		GeneralParticleHandler.SpawnParticle(new TechyHoloysquareParticle(player.Calamity().RandomDebuffVisualSpot, speed, Main.rand.NextFloat(1.2f, 1.8f), effectcolor, Main.rand.Next(8, 14)));
		int dustType = (Main.rand.NextBool() ? 66 : 247);
		Dust dust = Dust.NewDustPerfect(player.Calamity().RandomDebuffVisualSpot, dustType);
		dust.scale = ((dustType == 66) ? 1.4f : 1.2f);
		dust.velocity = Vector2.Zero + new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(4f, 8f)) - player.velocity / 2f;
		dust.noGravity = true;
		dust.alpha = Main.rand.Next(90, 150);
		dust.color = effectcolor;
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			Color effectcolor = (Color)(Main.rand.Next(4) switch
			{
				0 => Color.DeepSkyBlue, 
				1 => Color.MediumSpringGreen, 
				2 => Color.DarkOrange, 
				_ => Color.Violet, 
			});
			Vector2 position = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			Vector2 speed = default(Vector2);
			((Vector2)(ref speed))._002Ector(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-2.5f, -8.3f));
			GeneralParticleHandler.SpawnParticle(new TechyHoloysquareParticle(position, speed, Main.rand.NextFloat(1.2f, 3.1f), effectcolor, Main.rand.Next(8, 14)));
			int dustType = (Main.rand.NextBool() ? 66 : 247);
			Dust dust = Dust.NewDustPerfect(position, dustType);
			dust.scale = ((dustType == 66) ? 1.4f : 1.2f);
			dust.noGravity = true;
			dust.velocity = Vector2.Zero + new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(2f, 4f)) - npc.velocity / 2f;
			dust.alpha = Main.rand.Next(90, 150);
			dust.color = effectcolor;
		}
		if (Main.rand.NextBool(5))
		{
			Color effectcolor2 = (Color)(Main.rand.Next(4) switch
			{
				0 => Color.DeepSkyBlue, 
				1 => Color.MediumSpringGreen, 
				2 => Color.DarkOrange, 
				_ => Color.Violet, 
			});
			Vector2 position2 = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			int dustType2 = (Main.rand.NextBool() ? 66 : 247);
			Dust dust2 = Dust.NewDustPerfect(position2, dustType2);
			dust2.scale = ((dustType2 == 66) ? 0.9f : 1.4f);
			dust2.noGravity = true;
			dust2.velocity = Vector2.Zero + new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(2f, 4f));
			dust2.alpha = Main.rand.Next(35, 90);
			dust2.color = effectcolor2;
		}
	}
}
