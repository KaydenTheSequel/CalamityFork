using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class ProfanedWeakness : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().relicOfResilienceWeakness = true;
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		if (Main.rand.NextBool(5))
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(npcSize, Vector2.Zero, "CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard" + Main.rand.Next(1, 7), affectedByGravity: true, Main.rand.Next(9, 23), Main.rand.NextFloat(0.4f, 0.8f), Color.White * Main.rand.NextFloat(0.4f, 0.9f), new Vector2(1.1f, 0.8f), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-5f, 5f)));
		}
		if (Main.rand.NextBool(5))
		{
			Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, ModContent.DustType<LightDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), Main.rand.NextFloat(0.9f, 1.7f));
			dust.noGravity = Main.rand.NextBool(3);
			dust.color = (Main.rand.NextBool() ? Color.Sienna : Color.Goldenrod);
		}
	}
}
