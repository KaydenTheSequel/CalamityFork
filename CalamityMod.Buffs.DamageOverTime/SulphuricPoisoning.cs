using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class SulphuricPoisoning : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 240f,
		SicknessDebuffScaling = 1f
	};

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
		BuffID.Sets.GrantImmunityWith[70].Add(base.Type);
		BuffID.Sets.GrantImmunityWith[base.Type].Add(70);
		BuffID.Sets.GrantImmunityWith[20].Add(base.Type);
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().sulphurPoison = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().sulphurPoison = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.Next(5) < 4)
		{
			Dust microbe = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, 298, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 0.6f);
			microbe.noGravity = true;
			microbe.velocity *= 0.75f;
			microbe.velocity.X *= 0.75f;
			microbe.velocity.Y--;
			if (Main.rand.NextBool(4))
			{
				microbe.noGravity = false;
				microbe.scale *= 0.2f;
			}
			if (Main.rand.NextBool(5))
			{
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(Player.Calamity().RandomDebuffVisualSpot, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-3f, -4f)), Main.rand.NextBool() ? Color.OliveDrab : Color.GreenYellow, new Vector2(0.8f, 1f), 0f, 0.09f, 0f, 45));
			}
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
			Dust microbe = Dust.NewDustDirect(npc.position - new Vector2(2f), npc.width + 4, npc.height + 4, 298, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.2f + 3E-06f * (float)npc.width * (float)npc.height);
			microbe.noGravity = true;
			microbe.velocity *= 0.75f;
			microbe.velocity.X *= 0.75f;
			microbe.velocity.Y--;
			if (Main.rand.NextBool(4))
			{
				microbe.noGravity = false;
				microbe.scale *= 0.4f;
			}
			if (Main.rand.NextBool())
			{
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(npcSize, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-4.5f, -6f)), Main.rand.NextBool() ? Color.OliveDrab : Color.GreenYellow, new Vector2(0.8f, 1f), 0f, 0.12f + 7E-07f * (float)npc.width * (float)npc.height, 0f, 45));
			}
		}
	}
}
