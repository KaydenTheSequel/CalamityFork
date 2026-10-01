using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseMaceFlailProjectile : ModProjectile
{
	public enum FlailState
	{
		Spinning = 0,
		LaunchingForward = 1,
		Retracting = 2,
		ForcedRetracting = 4,
		Ricochet = 5,
		Dropping = 6
	}

	public abstract int AssociatedItemID { get; }

	public FlailState CurrentFlailState
	{
		get
		{
			return (FlailState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public ref float StateTimer => ref base.Projectile.ai[1];

	public ref float CollisionCounter => ref base.Projectile.localAI[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public virtual int BaseIFrames => 10;

	public virtual int SpinIFrames => 20;

	public virtual int LaunchIFrames => 10;

	public virtual float SpinSpeed => 10f;

	public virtual float SpinHitboxRadius => 55f;

	public virtual float SpinVisualRadius => 30f;

	public virtual Action<Projectile> EffectBeforeLaunch => null;

	public virtual int AfterimageLength => 6;

	public virtual float LaunchSpeed { get; set; } = 16f;

	public virtual int LaunchLifespan => 15;

	public virtual float MaxLaunchRange => 960f;

	public virtual float MaxDropRange => 400f;

	public virtual Action<Projectile> EffectBeforePullback => null;

	public virtual int RicochetLifespan => LaunchLifespan + 5;

	public virtual float MaxRetractSpeed { get; set; } = 15f;

	public virtual float RetractAcceleration { get; set; } = 2.5f;

	public virtual string ChainTexturePath => Texture + "Chain";

	public virtual float SpinDamage => 1.2f;

	public virtual float SpinKnockback => 0.25f;

	public virtual float SpinVerticalFactor => 0.8f;

	public virtual float LaunchDamage => 2f;

	public virtual float DropKnockback => 0.5f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName(AssociatedItemID);

	public virtual void ScaleWithMeleeSpeed(ref float launchSpeed, ref float maxSpeed, ref float acceleration)
	{
		float meleeSpeedMultiplier = Owner.GetTotalAttackSpeed(DamageClass.Melee);
		launchSpeed *= meleeSpeedMultiplier;
		maxSpeed *= meleeSpeedMultiplier;
		acceleration *= meleeSpeedMultiplier;
	}

	public virtual void UpdateDamageKB(out float damageMult, out float kbMult)
	{
		damageMult = 1f;
		kbMult = 1f;
		if (CurrentFlailState == FlailState.Spinning)
		{
			damageMult = SpinDamage;
			kbMult = SpinKnockback;
		}
		else if (CurrentFlailState == FlailState.LaunchingForward || CurrentFlailState == FlailState.Retracting)
		{
			damageMult = LaunchDamage;
		}
		else if (CurrentFlailState == FlailState.Dropping)
		{
			kbMult = DropKnockback;
		}
	}

	public virtual void SpinAI(float launchSpeed)
	{
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ownerHitCheck = true;
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 toMouse = Owner.MountedCenter.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			Owner.ChangeDir((toMouse.X > 0f).ToDirectionInt());
			if (!Owner.channel)
			{
				CurrentFlailState = FlailState.LaunchingForward;
				StateTimer = 0f;
				base.Projectile.Center = Owner.MountedCenter;
				base.Projectile.velocity = toMouse * launchSpeed;
				base.Projectile.netUpdate = true;
				base.Projectile.ResetLocalNPCHitImmunity();
				base.Projectile.localNPCHitCooldown = LaunchIFrames * base.Projectile.MaxUpdates;
				base.Projectile.ownerHitCheck = false;
				EffectBeforeLaunch?.Invoke(base.Projectile);
				return;
			}
		}
		StateTimer++;
		Vector2 spinOffset = Utils.RotatedBy(new Vector2((float)Owner.direction), (double)((float)Math.PI * SpinSpeed * (StateTimer / 60f) * (float)Owner.direction), default(Vector2));
		spinOffset.Y *= SpinVerticalFactor;
		if (spinOffset.Y * Owner.gravDir > 0f)
		{
			spinOffset.Y *= 0.5f;
		}
		base.Projectile.Center = Owner.MountedCenter + spinOffset * SpinVisualRadius + new Vector2(0f, Owner.gfxOffY);
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.localNPCHitCooldown = SpinIFrames * base.Projectile.MaxUpdates;
	}

	public virtual void LaunchAI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		bool shouldRetract = StateTimer++ >= (float)LaunchLifespan;
		shouldRetract |= base.Projectile.Distance(Owner.MountedCenter) >= MaxLaunchRange;
		if (Owner.controlUseItem)
		{
			CurrentFlailState = FlailState.Dropping;
			StateTimer = 0f;
			base.Projectile.netUpdate = true;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.2f;
			return;
		}
		if (shouldRetract)
		{
			CurrentFlailState = FlailState.Retracting;
			StateTimer = 0f;
			base.Projectile.netUpdate = true;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.3f;
			EffectBeforePullback?.Invoke(base.Projectile);
		}
		Owner.ChangeDir((Owner.Center.X < base.Projectile.Center.X).ToDirectionInt());
		base.Projectile.localNPCHitCooldown = LaunchIFrames * base.Projectile.MaxUpdates;
	}

	public virtual void RetractAI(bool forced, float maxSpeed, float acceleration)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		if (forced)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.ignoreWater = true;
		}
		float forceMult = (forced ? 2f : 1f);
		Vector2 toPlayer = base.Projectile.SafeDirectionTo(Owner.MountedCenter);
		Vector2 value = Owner.MountedCenter.DirectionFrom(base.Projectile.Center + base.Projectile.velocity).SafeNormalize(Vector2.Zero);
		if (base.Projectile.Distance(Owner.MountedCenter) <= maxSpeed * forceMult || (forced && Vector2.Dot(toPlayer, value) < 0f))
		{
			base.Projectile.Kill();
		}
		else if (Owner.controlUseItem && !forced)
		{
			CurrentFlailState = FlailState.Dropping;
			StateTimer = 0f;
			base.Projectile.netUpdate = true;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.2f;
		}
		else
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.98f;
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(toPlayer * maxSpeed * forceMult, acceleration * forceMult);
			Owner.ChangeDir((Owner.Center.X < base.Projectile.Center.X).ToDirectionInt());
		}
	}

	public virtual void RicochetAI()
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		if (StateTimer++ >= (float)RicochetLifespan)
		{
			CurrentFlailState = FlailState.Dropping;
			StateTimer = 0f;
			base.Projectile.netUpdate = true;
		}
		else
		{
			base.Projectile.localNPCHitCooldown = LaunchIFrames * base.Projectile.MaxUpdates;
			base.Projectile.velocity.Y += 0.6f;
			base.Projectile.velocity.X *= 0.95f;
			Owner.ChangeDir((Owner.Center.X < base.Projectile.Center.X).ToDirectionInt());
		}
	}

	public virtual void DropAI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.controlUseItem || base.Projectile.Distance(Owner.MountedCenter) > MaxDropRange)
		{
			CurrentFlailState = FlailState.ForcedRetracting;
			StateTimer = 0f;
			base.Projectile.netUpdate = true;
			EffectBeforePullback?.Invoke(base.Projectile);
		}
		else
		{
			base.Projectile.localNPCHitCooldown = BaseIFrames * base.Projectile.MaxUpdates;
			base.Projectile.velocity.Y += 0.8f;
			base.Projectile.velocity.X *= 0.95f;
			Owner.ChangeDir((Owner.Center.X < base.Projectile.Center.X).ToDirectionInt());
		}
	}

	public virtual bool ExtraBehavior()
	{
		return true;
	}

	public virtual void DrawChain()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		Texture2D Chain = ModContent.Request<Texture2D>(ChainTexturePath, (AssetRequestMode)2).Value;
		Vector2 playerArmPosition = Main.GetPlayerArmPosition(base.Projectile);
		playerArmPosition.Y -= Owner.gfxOffY;
		Vector2 chainPos = base.Projectile.Center;
		Vector2 toArms = playerArmPosition.MoveTowards(chainPos, 4f) - chainPos;
		float chainSegmentLength = MathF.Max(1f, Chain.Height);
		float rotation = toArms.ToRotation() + (float)Math.PI / 2f;
		for (float chainsLeft = ((Vector2)(ref toArms)).Length() + chainSegmentLength * 0.5f; chainsLeft > 0f; chainsLeft -= chainSegmentLength)
		{
			Color chainDrawColor = Lighting.GetColor((int)(chainPos.X / 16f), (int)(chainPos.Y / 16f));
			Main.spriteBatch.Draw(Chain, chainPos - Main.screenPosition, (Rectangle?)null, chainDrawColor, rotation, Chain.Size() * 0.5f, 1f, (SpriteEffects)0, 0f);
			chainPos += toArms.SafeNormalize(Vector2.Zero) * chainSegmentLength;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = AfterimageLength;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = BaseIFrames * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.active || Owner.dead || Owner.noItems || Owner.CCed || Vector2.Distance(base.Projectile.Center, Owner.Center) > MaxLaunchRange + 160f)
		{
			base.Projectile.Kill();
			return;
		}
		if (Main.myPlayer == base.Projectile.owner && Main.mapFullscreen)
		{
			base.Projectile.Kill();
			return;
		}
		float launchSpeed = LaunchSpeed;
		float maxRetractSpeed = MaxRetractSpeed;
		float retractAcceleration = RetractAcceleration;
		ScaleWithMeleeSpeed(ref launchSpeed, ref maxRetractSpeed, ref retractAcceleration);
		switch (CurrentFlailState)
		{
		case FlailState.Spinning:
			SpinAI(launchSpeed);
			break;
		case FlailState.LaunchingForward:
			LaunchAI();
			break;
		case FlailState.Retracting:
			RetractAI(forced: false, maxRetractSpeed, retractAcceleration);
			break;
		case FlailState.ForcedRetracting:
			RetractAI(forced: true, maxRetractSpeed, retractAcceleration);
			break;
		case FlailState.Ricochet:
			RicochetAI();
			break;
		case FlailState.Dropping:
			DropAI();
			break;
		}
		if (!ExtraBehavior())
		{
			return;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		if (CurrentFlailState == FlailState.Ricochet || CurrentFlailState == FlailState.Dropping)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 1f)
			{
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI + base.Projectile.velocity.X * 0.1f;
			}
			else
			{
				base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
			}
		}
		else
		{
			Vector2 vectorTowardsPlayer = base.Projectile.SafeDirectionTo(Owner.MountedCenter);
			base.Projectile.rotation = vectorTowardsPlayer.ToRotation() + MathHelper.ToRadians(270f);
		}
		base.Projectile.timeLeft = 2;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.SetDummyItemTime(2);
		Owner.itemRotation = base.Projectile.DirectionFrom(Owner.MountedCenter).ToRotation();
		if (base.Projectile.Center.X < Owner.MountedCenter.X)
		{
			Owner.itemRotation += (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		int impactIntensity = 0;
		Vector2 velocity = base.Projectile.velocity;
		float bounceFactor = 0.2f;
		if (CurrentFlailState == FlailState.LaunchingForward || CurrentFlailState == FlailState.Ricochet)
		{
			bounceFactor = 0.4f;
		}
		if (CurrentFlailState == FlailState.Dropping)
		{
			bounceFactor = 0f;
		}
		if (oldVelocity.X != base.Projectile.velocity.X)
		{
			if (Math.Abs(oldVelocity.X) > 4f)
			{
				impactIntensity = 1;
			}
			base.Projectile.velocity.X = oldVelocity.X * (0f - bounceFactor);
			CollisionCounter++;
		}
		if (oldVelocity.Y != base.Projectile.velocity.Y)
		{
			if (Math.Abs(oldVelocity.Y) > 4f)
			{
				impactIntensity = 1;
			}
			base.Projectile.velocity.Y = oldVelocity.Y * (0f - bounceFactor);
			CollisionCounter++;
		}
		if (CurrentFlailState == FlailState.LaunchingForward)
		{
			CurrentFlailState = FlailState.Ricochet;
			base.Projectile.localNPCHitCooldown = BaseIFrames * base.Projectile.MaxUpdates;
			base.Projectile.netUpdate = true;
			Point scanAreaStart = base.Projectile.TopLeft.ToTileCoordinates();
			Point scanAreaEnd = base.Projectile.BottomRight.ToTileCoordinates();
			impactIntensity = 2;
			base.Projectile.CreateImpactExplosion(2, base.Projectile.Center, ref scanAreaStart, ref scanAreaEnd, base.Projectile.width, out var causedShockwaves);
			base.Projectile.CreateImpactExplosion2_FlailTileCollision(base.Projectile.Center, causedShockwaves, velocity);
			Projectile projectile = base.Projectile;
			projectile.position -= velocity;
		}
		if (impactIntensity > 0)
		{
			base.Projectile.netUpdate = true;
			for (int i = 0; i < impactIntensity; i++)
			{
				Collision.HitTiles(base.Projectile.position, velocity, base.Projectile.width, base.Projectile.height);
			}
			SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		}
		if (CurrentFlailState != FlailState.Spinning && CurrentFlailState != FlailState.Ricochet && CurrentFlailState != FlailState.Dropping && CollisionCounter >= 10f)
		{
			CurrentFlailState = FlailState.ForcedRetracting;
			base.Projectile.netUpdate = true;
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (CurrentFlailState == FlailState.Spinning && StateTimer <= 12f)
		{
			return false;
		}
		return base.CanDamage();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentFlailState == FlailState.Spinning)
		{
			Vector2 distance = targetHitbox.ClosestPointInRect(Owner.MountedCenter) - Owner.MountedCenter;
			distance.Y /= SpinVerticalFactor;
			return ((Vector2)(ref distance)).Length() <= SpinHitboxRadius;
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		UpdateDamageKB(out var damageMult, out var kbMult);
		modifiers.SourceDamage *= damageMult;
		modifiers.Knockback *= kbMult;
		modifiers.HitDirectionOverride = (Owner.Center.X < target.Center.X).ToDirectionInt();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		if (!string.IsNullOrEmpty(ChainTexturePath))
		{
			DrawChain();
		}
		if (CurrentFlailState == FlailState.LaunchingForward && CalamityClientConfig.Instance.Afterimages && AfterimageLength > 0)
		{
			Texture2D maceTex = TextureAssets.Projectile[base.Type].Value;
			Vector2 drawOrigin = default(Vector2);
			((Vector2)(ref drawOrigin))._002Ector((float)maceTex.Width * 0.5f, (float)maceTex.Height * 0.5f);
			SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection != 1);
			for (int k = 0; k < base.Projectile.oldPos.Length && (float)k < StateTimer; k++)
			{
				Vector2 drawPos = base.Projectile.oldPos[k] - Main.screenPosition + drawOrigin * base.Projectile.scale + new Vector2(0f, base.Projectile.gfxOffY);
				Color color = base.Projectile.GetAlpha(lightColor) * ((float)(base.Projectile.oldPos.Length - k) / (float)base.Projectile.oldPos.Length);
				Main.spriteBatch.Draw(maceTex, drawPos, (Rectangle?)null, color, base.Projectile.rotation, drawOrigin, base.Projectile.scale - (float)k / (float)base.Projectile.oldPos.Length / 3f, spriteEffects, 0f);
			}
		}
		return true;
	}
}
