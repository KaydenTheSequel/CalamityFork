using System;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Other;

public class ExhumedHeart : ModNPC
{
	public ref float Time => ref base.NPC.ai[0];

	public Player Owner
	{
		get
		{
			if (base.NPC.target >= 255 || base.NPC.target < 0)
			{
				base.NPC.TargetClosest();
			}
			return Main.player[base.NPC.target];
		}
	}

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.NPC.width = (base.NPC.height = 38);
		base.NPC.damage = 0;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 50000;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit13;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0f;
		base.NPC.netAlways = true;
		base.NPC.aiStyle = -1;
		base.NPC.Calamity().ProvidesProximityRage = false;
		base.NPC.Calamity().DoesNotDisappearInBossRush = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.Opacity = Utils.GetLerpValue(0f, 25f, Time, clamped: true);
		base.NPC.Center = Owner.Center + ((float)Math.PI * 2f * Time / 540f).ToRotationVector2() * 350f;
		base.NPC.velocity = Vector2.Zero;
		if (!Owner.active || Owner.dead || Owner.ownedProjectileCounts[ModContent.ProjectileType<SepulcherMinion>()] <= 0)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
		}
		Time++;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		base.NPC.frame.Y = (int)(base.NPC.frameCounter / 5.0) % Main.npcFrameCount[base.Type] * frameHeight;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Color color = Color.Purple * base.NPC.Opacity;
		((Color)(ref color)).A = 127;
		return color;
	}
}
