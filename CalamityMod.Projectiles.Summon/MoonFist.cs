using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MoonFist : ModProjectile, ILocalizedModType, IModType
{
	public int DelayUntilNextPunch;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int FistIndex => (int)base.Projectile.ai[0];

	public ref float AttackTimer => ref base.Projectile.ai[1];

	public ref float FrameTimer => ref base.Projectile.localAI[0];

	public float FistInterpolant
	{
		get
		{
			float projectileCounts = Owner.ownedProjectileCounts[base.Type];
			if (projectileCounts <= 1f)
			{
				return 0.5f;
			}
			return (float)FistIndex / (projectileCounts - 1f);
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 18;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.minion = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
		base.Projectile.alpha = 255;
		base.Projectile.minionSlots = 4f;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 10;
		base.Projectile.netImportant = true;
		base.Projectile.timeLeft = 90000;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(DelayUntilNextPunch);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		DelayUntilNextPunch = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		ApplyMinionBuffs();
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1650f, Owner);
		if (potentialTarget == null)
		{
			HoverNearOwner();
		}
		else
		{
			AttackTarget(potentialTarget);
		}
		FrameTimer++;
		base.Projectile.frameCounter++;
		if (DelayUntilNextPunch > 0)
		{
			DelayUntilNextPunch--;
		}
		base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.1f, 0f, 1f);
	}

	public void ApplyMinionBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<MoonFistBuff>(), 3600);
		if (Owner.dead)
		{
			Owner.Calamity().MoonFist = false;
		}
		if (Owner.Calamity().MoonFist)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	public void HoverNearOwner()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		float hoverOffsetAngle = MathHelper.Lerp(-(float)Math.PI / 2f, (float)Math.PI / 2f, FistInterpolant);
		Vector2 hoverDestination = Owner.Center - Vector2.UnitY.RotatedBy(hoverOffsetAngle) * ((float)Owner.height + 70f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.7f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.13333f);
		float frameInterpolant = (FrameTimer / 50f + FistInterpolant) % 1f;
		base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(0f, 5f, frameInterpolant));
		base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.333f);
		base.Projectile.spriteDirection = Owner.direction;
		AttackTimer = 0f;
	}

	public void AttackTarget(NPC target)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		int reelBackTime = 32;
		int homeDelay = 27;
		int attackTime = 150;
		int attackCycleInterval = reelBackTime + attackTime;
		if (!base.Projectile.WithinRange(target.Center, 1200f))
		{
			Vector2 teleportDestination = target.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(250f, 300f);
			DoTeleport(teleportDestination);
		}
		base.Projectile.rotation = base.Projectile.AngleTo(target.Center);
		base.Projectile.spriteDirection = (target.Center.X < base.Projectile.Center.X).ToDirectionInt();
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
		if (AttackTimer <= (float)reelBackTime)
		{
			base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(6f, 11f, AttackTimer / (float)reelBackTime));
			float hoverOffset = MathHelper.Lerp(200f, 480f, AttackTimer / (float)reelBackTime);
			hoverOffset += MathHelper.Lerp(-10f, 75f, (float)base.Projectile.identity % 9f / 1f);
			float spin = MathHelper.Lerp(-0.27f, 0.27f, (float)base.Projectile.identity / 7f % 1f);
			Vector2 hoverDestination = target.Center - target.SafeDirectionTo(target.Center, Vector2.UnitY).RotatedBy(spin) * hoverOffset;
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(hoverDestination) * MathHelper.Min(25f, base.Projectile.Distance(hoverDestination));
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.02f);
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, 0.03f).MoveTowards(idealVelocity, 0.5f);
			if (AttackTimer == (float)reelBackTime)
			{
				SoundEngine.PlaySound(in SoundID.DD2_WyvernDiveDown, base.Projectile.Center);
				base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, target, 40f);
				base.Projectile.netUpdate = true;
			}
		}
		else if (DelayUntilNextPunch <= 0 && AttackTimer >= (float)(reelBackTime + homeDelay))
		{
			base.Projectile.velocity = base.Projectile.SuperhomeTowardsTarget(target, 34f, 24f);
		}
		AttackTimer = (AttackTimer + 1f) % (float)attackCycleInterval;
	}

	public void DoTeleport(Vector2 end)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		Vector2 start = base.Projectile.Center;
		for (int i = 0; i < 75; i++)
		{
			Dust dust = Dust.NewDustPerfect(Vector2.Lerp(start, end, (float)i / 74f), 267);
			dust.velocity = -Vector2.UnitY * Main.rand.NextFloat(0.2f, 0.235f);
			dust.color = Color.LightCyan;
			((Color)(ref dust.color)).A = 0;
			dust.scale = 0.8f;
			dust.fadeIn = 1.4f;
			dust.noGravity = true;
		}
		if (Main.netMode != 1)
		{
			for (int j = 0; j < 6; j++)
			{
				int magic = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), start, Vector2.Zero, ModContent.ProjectileType<MoonFistTeleportVisual>(), 0, 0f);
				Main.projectile[magic].timeLeft -= j * 2;
				magic = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), end, Vector2.Zero, ModContent.ProjectileType<MoonFistTeleportVisual>(), 0, 0f);
				Main.projectile[magic].timeLeft -= j * 2;
			}
			base.Projectile.Center = end;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		if (DelayUntilNextPunch > 0 || AttackTimer <= 0f)
		{
			return;
		}
		Vector2 impactPoint = Vector2.Lerp(base.Projectile.Center, target.Hitbox.ClosestPointInRect(base.Projectile.Center), 0.5f);
		for (int i = 0; i < 7; i++)
		{
			ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StardustPunch, new ParticleOrchestraSettings
			{
				PositionInWorld = impactPoint + Vector2.UnitY * Main.rand.NextFloatDirection() * 10f,
				MovementVector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(3f, 9.6f)
			});
		}
		for (int j = 0; j < 15; j++)
		{
			int dustID = (Main.rand.NextBool() ? 180 : 173);
			Dust cosmicDust = Dust.NewDustPerfect(impactPoint + Main.rand.NextVector2Circular(10f, 10f), dustID);
			cosmicDust.velocity = base.Projectile.velocity.RotatedByRandom(0.6000000238418579) * 0.2f;
			cosmicDust.scale = Main.rand.NextFloat(1f, 1.4f);
			cosmicDust.noGravity = true;
			if (Main.rand.NextBool(5))
			{
				cosmicDust.scale += 0.45f;
			}
		}
		SoundStyle style = SoundID.Item74 with
		{
			Pitch = -0.36f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		DelayUntilNextPunch = 26;
		base.Projectile.velocity = base.Projectile.SafeDirectionTo(target.Center, -Vector2.UnitY).RotatedBy(0.1599999964237213) * ((Vector2)(ref base.Projectile.velocity)).Length() * -0.45f;
		base.Projectile.netUpdate = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		if (AttackTimer != 0f)
		{
			direction = (SpriteEffects)(direction | 2);
		}
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}
}
