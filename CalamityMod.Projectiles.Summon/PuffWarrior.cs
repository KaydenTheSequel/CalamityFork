using System;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles.Summon;

public class PuffWarrior : ModProjectile, ILocalizedModType, IModType
{
	public const float Gravity = 0.425f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public Tile GroundTile
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return CalamityUtils.ParanoidTileRetrieval((int)base.Projectile.Bottom.X / 16, (int)base.Projectile.Bottom.Y / 16);
		}
	}

	public bool Jumping
	{
		get
		{
			if (GroundTile.IsTileSolidGround())
			{
				return Math.Abs(base.Projectile.velocity.Y) > 5f;
			}
			return true;
		}
	}

	public ref float JumpCountdown => ref base.Projectile.localAI[1];

	public ref float JumpCounter => ref base.Projectile.ai[1];

	public ref float EnemyFailureCounter => ref base.Projectile.ai[0];

	public ref float EnemyFailureCooldown => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 18;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		ProvidePlayerMinionBuffs();
		DetermineFrames();
		NPC potentialTarget = base.Projectile.Center.MinionHoming(660f, Owner);
		if (potentialTarget == null)
		{
			HopToOwner(out var guardSpot);
			TeleportToFarOffDestination(guardSpot);
		}
		else
		{
			HopToTarget(potentialTarget);
		}
		if (EnemyFailureCooldown > 0f)
		{
			EnemyFailureCooldown--;
		}
		if (JumpCounter > 120f && Jumping)
		{
			HopToOwner(out var guardSpot2, ignoreJumping: true);
			TeleportToFarOffDestination(guardSpot2);
		}
		if (Jumping)
		{
			JumpCounter++;
		}
		else
		{
			JumpCounter = 0f;
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0.02f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		base.Projectile.rotation = (Jumping ? (base.Projectile.rotation + (float)Math.PI / 10f * (float)base.Projectile.spriteDirection) : 0f);
		while (Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.position.Y -= 10f;
		}
		if (base.Projectile.velocity.Y < 16f)
		{
			base.Projectile.velocity.Y += 0.425f;
		}
	}

	internal void ProvidePlayerMinionBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<PuffWarriorBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<PuffWarrior>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().puffWarrior = false;
			}
			if (Owner.Calamity().puffWarrior)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	internal void DetermineFrames()
	{
		int startingFrame = (Jumping ? (Main.projFrames[base.Type] - 3) : 0);
		int endingFrame = (Jumping ? (Main.projFrames[base.Type] - 4) : (Main.projFrames[base.Type] - 4));
		if (Jumping)
		{
			JumpCountdown = 12f;
		}
		else if (Math.Abs(base.Projectile.velocity.Y) <= 0.425f && JumpCountdown > 0f)
		{
			startingFrame = Main.projFrames[base.Type] - 1;
			endingFrame = Main.projFrames[base.Type] - 1;
			JumpCountdown--;
		}
		if (base.Projectile.frame < startingFrame)
		{
			base.Projectile.frame = startingFrame;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 6 == 5)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= endingFrame)
		{
			base.Projectile.frame = startingFrame;
		}
	}

	internal void TeleportToFarOffDestination(Vector2 guardSpot)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		bool obstructionBetweenSpot = Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, guardSpot, 4, 4);
		bool outOfRangeOfSpot = !base.Projectile.WithinRange(guardSpot, obstructionBetweenSpot ? 700f : 1900f);
		if ((Main.myPlayer == base.Projectile.owner) & outOfRangeOfSpot)
		{
			base.Projectile.Center = guardSpot;
			base.Projectile.netUpdate = true;
		}
	}

	internal void HopToOwner(out Vector2 guardSpot, bool ignoreJumping = false)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		EnemyFailureCounter = 0f;
		guardSpot = Owner.Center;
		guardSpot.X += (float)Owner.direction * 40f * ((float)base.Projectile.identity % 14f + (float)(base.Projectile.identity / 14) * 0.2f);
		Vector2 searchPoint = guardSpot - Vector2.UnitY * 420f;
		searchPoint.Y = MathHelper.Clamp(searchPoint.Y, 32f, (float)Main.maxTilesY * 16f - 32f);
		WorldUtils.Find(searchPoint.ToTileCoordinates(), Searches.Chain(new Searches.Down(Main.maxTilesY + 2), new Conditions.IsSolid()), out var guardPoint);
		guardSpot = guardPoint.ToWorldCoordinates(8f, 16f);
		if ((!Jumping | ignoreJumping) && MathHelper.Distance(guardSpot.X, base.Projectile.Center.X) > 10f)
		{
			base.Projectile.velocity = CalamityUtils.GetProjectilePhysicsFiringVelocity(base.Projectile.Center, guardSpot, 0.425f, 12f);
			JumpCounter = 0f;
			base.Projectile.netUpdate = true;
		}
	}

	internal void HopToTarget(NPC target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 attackPosition = target.Center;
		float attackPositionOffsetDirection = (target.velocity.X > 0f).ToDirectionInt() * Math.Sign(base.Projectile.Center.X - target.Center.X);
		attackPosition.X += attackPositionOffsetDirection * 22f * ((float)base.Projectile.identity % 14f + (float)(base.Projectile.identity / 14) * 0.2f);
		Vector2 searchPoint = attackPosition - Vector2.UnitY * 360f;
		searchPoint.Y = MathHelper.Clamp(searchPoint.Y, 32f, (float)Main.maxTilesY * 16f - 32f);
		WorldUtils.Find(searchPoint.ToTileCoordinates(), Searches.Chain(new Searches.Down(Main.maxTilesY + 2), new Conditions.IsSolid()), out var guardPoint);
		attackPosition = guardPoint.ToWorldCoordinates();
		if (Jumping)
		{
			return;
		}
		Projectile projectile = base.Projectile;
		projectile.position += new Vector2((float)base.Projectile.spriteDirection * 4f, -4f);
		base.Projectile.velocity = CalamityUtils.GetProjectilePhysicsFiringVelocity(base.Projectile.Center, attackPosition, 0.425f, 17f + EnemyFailureCounter);
		EnemyFailureCounter++;
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.WithinRange(attackPosition, 360f))
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 shootPosition = base.Projectile.Top + Vector2.UnitY * (8f + (float)i * 3f);
				Vector2 shootVelocity = (target.Center - shootPosition).SafeNormalize(Vector2.UnitY) * 14f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shootPosition, shootVelocity, ModContent.ProjectileType<PuffCloud>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		JumpCounter = 0f;
		base.Projectile.netUpdate = true;
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (!(EnemyFailureCooldown > 0f))
		{
			EnemyFailureCounter--;
			if (EnemyFailureCounter < 0f)
			{
				EnemyFailureCounter = 0f;
			}
			EnemyFailureCooldown = 60f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.oldVelocity)).Length() > 6f)
		{
			base.Projectile.velocity = oldVelocity * 0.5f;
		}
		else
		{
			base.Projectile.velocity.X = 0f;
		}
		return false;
	}
}
