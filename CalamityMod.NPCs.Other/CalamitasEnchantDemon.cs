using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Other;

public class CalamitasEnchantDemon : ModNPC
{
	public Player Target => Main.player[base.NPC.target];

	public ref float AttackTimer => ref base.NPC.ai[0];

	public ref float FadeAwayTimer => ref base.NPC.ai[1];

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 68;
		base.NPC.height = 68;
		base.NPC.damage = 270;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 100000;
		base.NPC.HitSound = SoundID.NPCHit47;
		base.NPC.DeathSound = SoundID.NPCDeath18;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.knockBackResist = 0f;
		base.NPC.netAlways = true;
		base.NPC.aiStyle = 0;
		base.NPC.Calamity().DoesNotDisappearInBossRush = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = 100000;
	}

	public override void AI()
	{
		FadeAwayTimer++;
		if (!Main.projectile.IndexInRange(base.NPC.target) || Target.dead || !Target.active)
		{
			FadeAway();
		}
		else if (FadeAwayTimer < 15f)
		{
			DoInitializationAI();
		}
		else
		{
			AttackTarget();
		}
	}

	public void FadeAway()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.dontTakeDamage = true;
		base.NPC.Opacity = Utils.GetLerpValue(50f, 0f, FadeAwayTimer, clamped: true);
		if (base.NPC.Opacity <= 0f)
		{
			base.NPC.active = false;
			base.NPC.netUpdate = true;
		}
		base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, Vector2.UnitY * base.NPC.Opacity * -2f, 0.125f);
		FadeAwayTimer++;
		if (!Main.dedServ)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 223);
				dust.velocity = -Vector2.UnitY * Main.rand.NextFloat(2.8f, 3.5f);
				dust.scale = Main.rand.NextFloat(1f, 1.125f);
				dust.fadeIn = 0.4f;
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}

	public void DoInitializationAI()
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ && FadeAwayTimer == 1f)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 223);
				dust.velocity = -Vector2.UnitY * Main.rand.NextFloat(2.8f, 3.5f);
				dust.scale = Main.rand.NextFloat(1f, 1.125f);
				dust.fadeIn = 0.7f;
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
		base.NPC.velocity = Vector2.UnitX * MathHelper.Lerp(-0.1f, -3.5f, FadeAwayTimer / 15f);
	}

	public void AttackTarget()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		if (AttackTimer % 120f > 90f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.94f;
		}
		else if (!base.NPC.WithinRange(Target.Center, 150f))
		{
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(Target.Center) * 16f;
			base.NPC.velocity = (base.NPC.velocity * 29f + idealVelocity) / 30f;
			base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, idealVelocity, 0.025f);
		}
		if (AttackTimer % 120f == 119f)
		{
			base.NPC.velocity = base.NPC.SafeDirectionTo(Target.Center) * 23.5f;
		}
		base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter % 5.0 == 4.0)
		{
			base.NPC.frame.Y += frameHeight;
		}
		if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			Utils.PoofOfSmoke(base.NPC.Center);
		}
	}

	public override void OnKill()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			Vector2 shootVelocity = base.NPC.SafeDirectionTo(Target.Center).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(12f, 14f);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, shootVelocity, ModContent.ProjectileType<DemonHeal>(), 0, 0f, base.NPC.target);
		}
	}
}
