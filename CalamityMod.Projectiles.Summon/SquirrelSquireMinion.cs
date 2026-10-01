using System;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SquirrelSquireMinion : ModProjectile, ILocalizedModType, IModType
{
	private enum AIState
	{
		Idle,
		Attack
	}

	private Player Owner;

	private NPC Target;

	public new string LocalizationCategory => "Projectiles.Summon";

	private AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
			base.Projectile.ForceNetUpdate();
		}
	}

	private bool OnSpawnCheck
	{
		get
		{
			return base.Projectile.ai[2] == 1f;
		}
		set
		{
			base.Projectile.ai[2] = (value ? 1f : 0f);
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 14;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.width = 40;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.sentry = true;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		if (!OnSpawnCheck)
		{
			ActualOnSpawn();
			OnSpawnCheck = true;
		}
		SetTarget();
		ApplyGravity();
		DoAnimation();
		switch (State)
		{
		case AIState.Idle:
			IdleBehavior();
			break;
		case AIState.Attack:
			AttackBehavior();
			break;
		}
	}

	private void ActualOnSpawn()
	{
		Owner = Main.player[base.Projectile.owner];
		base.Projectile.spriteDirection = Main.rand.NextBool().ToDirectionInt();
	}

	private void SetTarget()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Target = base.Projectile.Center.MinionHoming(960f, Owner, ignoreTiles: false);
	}

	private void ApplyGravity()
	{
		float speedY = base.Projectile.velocity.Y;
		if (speedY < 20f)
		{
			speedY = MathF.Min(speedY + 0.4f, 20f);
		}
		base.Projectile.velocity.Y = speedY;
	}

	private void DoAnimation()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 5)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = (base.Projectile.frame + 1) % 7;
		}
	}

	private void IdleBehavior()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			State = AIState.Attack;
		}
		else if (base.Projectile.Distance(Owner.Center) < 60f)
		{
			base.Projectile.spriteDirection = MathF.Sign(Owner.Center.X - base.Projectile.Center.X);
		}
	}

	private void AttackBehavior()
	{
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		if (Target == null)
		{
			State = AIState.Idle;
			return;
		}
		if (base.Projectile.frame == 0 && base.Projectile.frameCounter == 0 && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 spawnPosition = ((base.Projectile.spriteDirection == -1) ? base.Projectile.Left : base.Projectile.Right);
			Vector2 shootVelocity = CalamityUtils.CalculatePredictiveAimToTarget(spawnPosition, Target, SquirrelSquireStaff.ProjectileVelocity);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, shootVelocity, ModContent.ProjectileType<SquirrelSquireAcorn>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			SoundEngine.PlaySound(SoundID.Item1 with
			{
				Volume = 0.5f,
				Pitch = 0.8f,
				PitchVariance = 0.1f
			}, spawnPosition);
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.spriteDirection = MathF.Sign(Target.Center.X - base.Projectile.Center.X);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Top - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
		Rectangle frame = value.Frame(2, 7, (int)State, base.Projectile.frame);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		Vector2 anchorPoint = default(Vector2);
		((Vector2)(ref anchorPoint))._002Ector(frame.Size().X * 0.5f, 0f);
		Main.EntitySpriteDraw(effects: (SpriteEffects)(base.Projectile.spriteDirection == -1), texture: value, position: drawPosition, sourceRectangle: frame, color: drawColor, rotation: base.Projectile.rotation, origin: anchorPoint, scale: base.Projectile.scale);
		return false;
	}
}
