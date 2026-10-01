using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Ravager;

[HasPierceResist(false)]
public class RavagerLegRight : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.RavagerBody.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.NPC.damage = 0;
		base.NPC.width = 60;
		base.NPC.height = 60;
		base.NPC.defense = 40;
		base.NPC.DR_NERD(0.15f);
		base.NPC.lifeMax = 12500;
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.netAlways = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.alpha = 255;
		base.NPC.HitSound = RavagerBody.HitSound;
		base.NPC.DeathSound = RavagerBody.LimbLossSound;
		if (DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive)
		{
			base.NPC.defense *= 2;
			base.NPC.lifeMax *= 4;
		}
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 40000;
		}
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void AI()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.scavenger < 0 || !Main.npc[CalamityGlobalNPC.scavenger].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 10;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			base.NPC.ai[1] = 0f;
		}
		base.NPC.Center = Main.npc[CalamityGlobalNPC.scavenger].Center + new Vector2(70f, 88f);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerLegRight").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerLegRight2").Type);
			}
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, hit.HitDirection, -1f);
			}
		}
	}
}
