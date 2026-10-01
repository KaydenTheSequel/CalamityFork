using System;
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

public class RavagerHead2 : ModNPC
{
	public static int HomingDartDamage = 30;

	public static int PostProviDartBuff = 20;

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
		base.NPC.HitSound = RavagerBody.HitSound;
		base.NPC.DeathSound = RavagerBody.LimbLossSound;
		if (DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive)
		{
			base.NPC.defense *= 2;
			base.NPC.lifeMax *= 3;
		}
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 22500;
		}
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
		base.NPC.dontTakeDamage = true;
	}

	public override void AI()
	{
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.scavenger < 0 || !Main.npc[CalamityGlobalNPC.scavenger].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		Player player = Main.player[Main.npc[CalamityGlobalNPC.scavenger].target];
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool provy = DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive;
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		float playerXDist = base.NPC.position.X + (float)(base.NPC.width / 2) - player.position.X - (float)(player.width / 2);
		float headRotation = (float)Math.Atan2(base.NPC.position.Y + (float)base.NPC.height - 59f - player.position.Y - (float)(player.height / 2), playerXDist) + (float)Math.PI / 2f;
		if (headRotation < 0f)
		{
			headRotation += (float)Math.PI * 2f;
		}
		else if (headRotation > (float)Math.PI * 2f)
		{
			headRotation -= (float)Math.PI * 2f;
		}
		float headRotateIncrement = 0.1f;
		if (base.NPC.rotation < headRotation)
		{
			if (headRotation - base.NPC.rotation > (float)Math.PI)
			{
				base.NPC.rotation -= headRotateIncrement;
			}
			else
			{
				base.NPC.rotation += headRotateIncrement;
			}
		}
		else if (base.NPC.rotation > headRotation)
		{
			if (base.NPC.rotation - headRotation > (float)Math.PI)
			{
				base.NPC.rotation += headRotateIncrement;
			}
			else
			{
				base.NPC.rotation -= headRotateIncrement;
			}
		}
		if (base.NPC.rotation > headRotation - headRotateIncrement && base.NPC.rotation < headRotation + headRotateIncrement)
		{
			base.NPC.rotation = headRotation;
		}
		if (base.NPC.rotation < 0f)
		{
			base.NPC.rotation += (float)Math.PI * 2f;
		}
		else if (base.NPC.rotation > (float)Math.PI * 2f)
		{
			base.NPC.rotation -= (float)Math.PI * 2f;
		}
		if (base.NPC.rotation > headRotation - headRotateIncrement && base.NPC.rotation < headRotation + headRotateIncrement)
		{
			base.NPC.rotation = headRotation;
		}
		base.NPC.ai[1]++;
		bool fireProjectiles = base.NPC.ai[1] >= 480f;
		if (fireProjectiles && Vector2.Distance(base.NPC.Center, player.Center) > 80f)
		{
			int type = ModContent.ProjectileType<HomingLaserDart>();
			float projectileVelocity = (death ? 8f : 6f);
			if (base.NPC.ai[1] >= 600f)
			{
				base.NPC.ai[0]++;
				base.NPC.ai[1] = 0f;
				if (Main.netMode != 1)
				{
					SoundEngine.PlaySound(in RavagerHead.MissileSound, base.NPC.Center);
					type = ModContent.ProjectileType<RavagerNuke>();
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Normalize(player.Center - base.NPC.Center) * projectileVelocity * 0.25f, type, RavagerHead.NukeDamage + (provy ? RavagerHead.PostProviNukeBuff : 0), 0f, Main.myPlayer, Main.npc[CalamityGlobalNPC.scavenger].target);
				}
			}
		}
		float attackMovementVel = 22f;
		float attackMovementAccel = 0.3f;
		if (death)
		{
			attackMovementVel += 4f;
			attackMovementAccel += 0.05f;
		}
		if (provy)
		{
			attackMovementVel *= 1.25f;
			attackMovementAccel *= 1.25f;
		}
		Vector2 npcCenter = base.NPC.Center;
		float distanceX = ((base.NPC.ai[0] % 2f == 0f) ? 480f : (-480f));
		float distanceY = (fireProjectiles ? (-320f) : 320f);
		float playerXDistAttack = player.Center.X + (fireProjectiles ? distanceX : 0f) - npcCenter.X;
		float playerYDistAttack = player.Center.Y + distanceY - npcCenter.Y;
		float playerDistanceAttack = (float)Math.Sqrt(playerXDistAttack * playerXDistAttack + playerYDistAttack * playerYDistAttack);
		playerDistanceAttack = attackMovementVel / playerDistanceAttack;
		playerXDistAttack *= playerDistanceAttack;
		playerYDistAttack *= playerDistanceAttack;
		if (base.NPC.velocity.X < playerXDistAttack)
		{
			base.NPC.velocity.X += attackMovementAccel;
			if (base.NPC.velocity.X < 0f && playerXDistAttack > 0f)
			{
				base.NPC.velocity.X += attackMovementAccel;
			}
		}
		else if (base.NPC.velocity.X > playerXDistAttack)
		{
			base.NPC.velocity.X -= attackMovementAccel;
			if (base.NPC.velocity.X > 0f && playerXDistAttack < 0f)
			{
				base.NPC.velocity.X -= attackMovementAccel;
			}
		}
		if (base.NPC.velocity.Y < playerYDistAttack)
		{
			base.NPC.velocity.Y += attackMovementAccel;
			if (base.NPC.velocity.Y < 0f && playerYDistAttack > 0f)
			{
				base.NPC.velocity.Y += attackMovementAccel;
			}
		}
		else if (base.NPC.velocity.Y > playerYDistAttack)
		{
			base.NPC.velocity.Y -= attackMovementAccel;
			if (base.NPC.velocity.Y > 0f && playerYDistAttack < 0f)
			{
				base.NPC.velocity.Y -= attackMovementAccel;
			}
		}
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
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerHead").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerHead2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerHead3").Type);
			}
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, hit.HitDirection, -1f);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}
}
