using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Ravager;

[HasPierceResist(false)]
public class RavagerHead : ModNPC
{
	public static readonly SoundStyle MissileSound = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerMissileLaunch");

	public static int NukeDamage = 35;

	public static int PostProviNukeBuff = 25;

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
		base.NPC.width = 80;
		base.NPC.height = 80;
		base.NPC.defense = 40;
		base.NPC.DR_NERD(0.15f);
		base.NPC.lifeMax = 20000;
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.netAlways = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.alpha = 255;
		base.NPC.HitSound = RavagerBody.HitSound;
		base.NPC.DeathSound = null;
		if (DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive)
		{
			base.NPC.defense *= 2;
			base.NPC.lifeMax *= 4;
		}
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 45000;
		}
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void AI()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.scavenger < 0 || !Main.npc[CalamityGlobalNPC.scavenger].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		bool provy = DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		base.NPC.Center = Main.npc[CalamityGlobalNPC.scavenger].Center + new Vector2(1f, -20f);
		if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 10;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		base.NPC.ai[1]++;
		if (base.NPC.ai[1] >= (death ? 420f : 480f))
		{
			SoundEngine.PlaySound(in MissileSound, base.NPC.Center);
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.TargetClosest();
			}
			if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
			{
				base.NPC.TargetClosest();
			}
			base.NPC.ai[1] = 0f;
			int type = ModContent.ProjectileType<RavagerNuke>();
			if (Main.netMode != 1)
			{
				Vector2 shootFromVector = default(Vector2);
				((Vector2)(ref shootFromVector))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - 20f);
				Vector2 velocity = default(Vector2);
				((Vector2)(ref velocity))._002Ector(0f, -15f);
				int nuke = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFromVector, velocity, type, NukeDamage + (provy ? PostProviNukeBuff : 0), 0f, Main.myPlayer, base.NPC.target);
				Main.projectile[nuke].velocity.Y = -15f;
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			for (int dustCounter = 0; (double)dustCounter < (double)hit.Damage / (double)base.NPC.lifeMax * 100.0; dustCounter++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
		else if (Main.netMode != 1 && !Main.zenithWorld)
		{
			NPC.NewNPC(base.NPC.GetSource_Death(), (int)base.NPC.Center.X, (int)base.NPC.position.Y + base.NPC.height, ModContent.NPCType<RavagerHead2>(), base.NPC.whoAmI);
		}
	}
}
