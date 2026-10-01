using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseMinionProjectile : ModProjectile, ILocalizedModType, IModType
{
	private float _gravity = 0.4f;

	private float _maxGravity = 20f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public abstract int AssociatedProjectileTypeID { get; }

	public abstract int AssociatedBuffTypeID { get; }

	public abstract ref bool AssociatedMinionBool { get; }

	public virtual float MinionSlots => 1f;

	public virtual float EnemyDistanceDetection => 1200f;

	public virtual float MinEnemyDistanceDetection => 960f;

	private float AdaptiveEnemyDistanceDetection
	{
		get
		{
			if (Target != null)
			{
				return EnemyDistanceDetection;
			}
			return MinEnemyDistanceDetection;
		}
	}

	public int IFrames { get; set; } = 10;

	public virtual bool PreHardmodeMinionTileVision => false;

	public virtual bool PreventTargettingUntilTargetHit => true;

	public virtual bool Grounded => false;

	public float Gravity
	{
		get
		{
			return _gravity;
		}
		set
		{
			_gravity = MathF.Abs(value);
		}
	}

	public float MaxGravity
	{
		get
		{
			return _maxGravity;
		}
		set
		{
			_maxGravity = MathF.Abs(value);
		}
	}

	public virtual int AnimationFrames => 1;

	public int FramesUntilNextAnimationFrame { get; set; } = 5;

	public int TrailingMode { get; set; } = 2;

	public int TrailCacheLength { get; set; } = 5;

	public Player Owner { get; set; }

	public CalamityPlayer ModdedOwner { get; set; }

	public NPC Target { get; set; }

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = AnimationFrames;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = TrailingMode;
		ProjectileID.Sets.TrailCacheLength[base.Type] = TrailCacheLength;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = (int)EnemyDistanceDetection;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.minionSlots = MinionSlots;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = Grounded;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override bool PreAI()
	{
		base.Projectile.Calamity().overridesMinionDamagePrevention = !PreventTargettingUntilTargetHit;
		return true;
	}

	public override void AI()
	{
		base.Projectile.localNPCHitCooldown = IFrames * base.Projectile.MaxUpdates;
		SetOwnerTarget();
		CheckMinionExistence();
		DoAnimation();
		MinionAI();
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public abstract void MinionAI();

	public virtual void CheckMinionExistence()
	{
		Owner.AddBuff(AssociatedBuffTypeID, 2);
		if (base.Type == AssociatedProjectileTypeID)
		{
			if (Owner.dead)
			{
				AssociatedMinionBool = false;
			}
			if (AssociatedMinionBool)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public virtual void DoAnimation()
	{
		if (Main.projFrames[base.Type] > 1)
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter >= FramesUntilNextAnimationFrame * base.Projectile.MaxUpdates)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			}
		}
	}

	public virtual void SetOwnerTarget()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Owner = Main.player[base.Projectile.owner];
		ModdedOwner = Owner.Calamity();
		Target = Owner.Center.MinionHoming(AdaptiveEnemyDistanceDetection, Owner, !PreHardmodeMinionTileVision || (CalamityPlayer.areThereAnyDamnBosses && !Grounded));
		if (Grounded && Target == null && PreHardmodeMinionTileVision)
		{
			Target = base.Projectile.Center.MinionHoming(AdaptiveEnemyDistanceDetection, Owner, ignoreTiles: false);
		}
	}

	public void DoGravity()
	{
		float speedY = base.Projectile.velocity.Y;
		if (speedY < _maxGravity)
		{
			speedY = MathF.Min(speedY + _gravity, _maxGravity);
		}
		base.Projectile.velocity.Y = speedY;
	}

	public bool IsTileBetweenTwoVectorsVertically(Vector2 startVector, Vector2 endVector, bool platformCheckDownwards = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Point startPoint = startVector.ToSafeTileCoordinates();
		Point endPoint = endVector.ToSafeTileCoordinates();
		if (endVector.Y > startVector.Y)
		{
			for (int coordY = endPoint.Y; coordY >= startPoint.Y; coordY--)
			{
				if (Main.tile[startPoint.X, coordY].IsTileSolidGround())
				{
					return true;
				}
			}
		}
		else
		{
			for (int i = endPoint.Y; i <= startPoint.Y; i++)
			{
				if (platformCheckDownwards)
				{
					if (Main.tile[startPoint.X, i].IsTileSolidGround())
					{
						return true;
					}
				}
				else if (Main.tile[startPoint.X, i].IsTileSolid())
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsTileBetweenOwnerAndVectorVertically(Vector2 vector, bool platformCheckDownwards = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return IsTileBetweenTwoVectorsVertically(Owner.Center, vector, platformCheckDownwards);
	}

	public bool IsTileBetweenTargetAndVectorVertically(Vector2 vector, bool platformCheckDownwards = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return IsTileBetweenTwoVectorsVertically(Target.Center, vector, platformCheckDownwards);
	}

	public bool IsTileBetweenVectorAndMinionVertically(Vector2 vector, bool platformCheckDownwards = false)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		return IsTileBetweenTwoVectorsVertically(vector, base.Projectile.Center, platformCheckDownwards);
	}

	public bool IsTileBetweenOwnerAndMinionVertically(bool platformCheckDownwards = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return IsTileBetweenTwoVectorsVertically(Owner.Center, base.Projectile.Center, platformCheckDownwards);
	}

	public bool IsTileBetweenTargetAndMinionVertically(bool platformCheckDownwards = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return IsTileBetweenTwoVectorsVertically(Target.Center, base.Projectile.Center, platformCheckDownwards);
	}

	public bool IsMinionFacingTile()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Rectangle inflatedMinionHitbox = base.Projectile.getRect();
		((Rectangle)(ref inflatedMinionHitbox)).Inflate(1, 0);
		bool num = Main.tile[CalamityUtils.ToSafeTileCoordinates(new Vector2((float)inflatedMinionHitbox.X, (float)(inflatedMinionHitbox.Y + inflatedMinionHitbox.Height - 17)))].IsTileSolid();
		bool stuckOnRightTile = Main.tile[CalamityUtils.ToSafeTileCoordinates(new Vector2((float)(inflatedMinionHitbox.X + inflatedMinionHitbox.Width), (float)(inflatedMinionHitbox.Y + inflatedMinionHitbox.Height - 17)))].IsTileSolid();
		return num | stuckOnRightTile;
	}
}
