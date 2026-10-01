using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseCustomUseStyleProjectile : ModProjectile
{
	public bool whenSpawned;

	public Vector2 Offset;

	public int NumberOfAnimations;

	public float Animation;

	public bool FlipAsSword;

	public bool IgnoreActiveAnimation;

	public float RotationOffset;

	public float ArmRotationOffset;

	public float ArmRotationOffsetBack;

	public int Frame;

	public SpriteEffects spriteEffects;

	public bool CanHit;

	public Vector2 AbsolutePosition;

	public bool DrawUnconditionally;

	public float AnimationProgress;

	public virtual int AssignedItemID => 0;

	public virtual Player Owner => Main.player[base.Projectile.owner];

	public virtual float HitboxOutset => 30f;

	public virtual Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(30f, 30f);
		}
	}

	public virtual int FrameCount => 1;

	public virtual Vector2 SpriteOrigin
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size / 2f;
		}
	}

	public float FinalRotation => base.Projectile.rotation + RotationOffset;

	public virtual float HitboxRotationOffset => 0f;

	public override void SetDefaults()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.width = (int)Math.Max(HitboxSize.X, 1f);
		base.Projectile.height = (int)Math.Max(HitboxSize.Y, 1f);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.Projectile.timeLeft = Owner.HeldItem.useAnimation + 1;
	}

	public virtual void ResetStyle()
	{
	}

	public virtual void WhenSpawned()
	{
	}

	public virtual void UseStyle()
	{
	}

	public virtual void OnBeginUse()
	{
	}

	public virtual void OnEndUse()
	{
	}

	public override void AI()
	{
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		if (whenSpawned)
		{
			WhenSpawned();
			whenSpawned = false;
			base.Projectile.timeLeft = Owner.HeldItem.useAnimation + 1;
			base.Projectile.netUpdate = true;
		}
		bool itemAnimationActive = Owner.ItemAnimationActive;
		if (Owner.HeldItem.type != AssignedItemID || Owner.dead)
		{
			base.Projectile.Kill();
		}
		Owner.Calamity().mouseWorldListener = true;
		Owner.Calamity().rightClickListener = true;
		if (itemAnimationActive || IgnoreActiveAnimation)
		{
			Animation++;
			UseStyle();
			Owner.heldProj = base.Projectile.whoAmI;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.rotation + RotationOffset + ArmRotationOffset);
			Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.rotation + RotationOffset + ArmRotationOffsetBack);
		}
		else
		{
			Animation = 0f;
			if (DrawUnconditionally)
			{
				Owner.heldProj = base.Projectile.whoAmI;
				Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.rotation + RotationOffset + ArmRotationOffset);
				Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.rotation + RotationOffset + ArmRotationOffsetBack);
			}
			NumberOfAnimations = 0;
			ResetStyle();
		}
		AnimationProgress = Animation % (float)Owner.itemAnimationMax;
		if (AbsolutePosition == Vector2.Zero)
		{
			base.Projectile.position = Owner.position + Owner.Size / 2f - base.Projectile.Size / 2f + Offset;
		}
		else
		{
			AbsolutePosition += base.Projectile.velocity;
			base.Projectile.position = AbsolutePosition - base.Projectile.Size / 2f + Offset;
		}
		if (AnimationProgress == (float)(Owner.itemAnimationMax - 1))
		{
			OnEndUse();
			NumberOfAnimations++;
		}
		if (Owner.itemAnimation == Owner.itemAnimationMax - 1)
		{
			base.Projectile.timeLeft = Owner.HeldItem.useAnimation + 1;
			OnBeginUse();
		}
		if (DrawUnconditionally)
		{
			base.Projectile.timeLeft = Math.Max(base.Projectile.timeLeft, 2);
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		return target.immune[0] <= 0 && !target.friendly && !target.dontTakeDamage;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = Owner.direction;
		base.ModifyHitNPC(target, ref modifiers);
	}

	public override bool? CanDamage()
	{
		if (!CanHit)
		{
			return false;
		}
		return base.CanDamage();
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 cen = base.Projectile.Center + Utils.RotatedBy(new Vector2(HitboxOutset, 0f), (double)(FinalRotation + HitboxRotationOffset), default(Vector2));
		hitbox = new Rectangle((int)cen.X - (int)(HitboxSize.X / 2f), (int)cen.Y - (int)(HitboxSize.Y / 2f), (int)HitboxSize.X, (int)HitboxSize.Y);
		base.ModifyDamageHitbox(ref hitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.itemAnimation > 0 || DrawUnconditionally)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		return false;
	}

	protected BaseCustomUseStyleProjectile()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		whenSpawned = true;
		Offset = Vector2.Zero;
		CanHit = true;
		AbsolutePosition = Vector2.Zero;
		base._002Ector();
	}
}
