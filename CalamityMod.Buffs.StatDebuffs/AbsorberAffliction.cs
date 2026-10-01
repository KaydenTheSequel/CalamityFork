using CalamityMod.DataStructures;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class AbsorberAffliction : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 400f,
		SicknessDebuffScaling = 1f,
		MultiplierDamageTickSize = 0.05f
	};

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().absorberAffliction = true;
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		Color fxColor = Color.Lerp(Color.DarkSeaGreen, Color.MediumSeaGreen, Main.rand.NextFloat(1f));
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(npcSize, Vector2.UnitY * Main.rand.NextFloat(4f, -4f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, Main.rand.Next(16, 27), Main.rand.NextFloat(1.5f, 2f), fxColor, new Vector2(0.5f, 1.1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.3f) + 0.3f));
		}
		if (Main.rand.Next(5) >= 0)
		{
			Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, ModContent.DustType<LightDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), Main.rand.NextFloat(0.8f, 1.8f));
			dust.noGravity = true;
			dust.velocity.Y -= 1.8f;
			dust.velocity.Y *= 2.5f;
			dust.color = (Main.rand.NextBool(3) ? Color.PaleGreen : Color.DarkSeaGreen);
		}
	}
}
