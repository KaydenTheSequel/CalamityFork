using System;
using CalamityMod.Buffs.Pets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class DaawnlightSpiritOriginMinion : ModProjectile, ILocalizedModType, IModType
{
	public enum AnimationState
	{
		Idle,
		Pointing
	}

	private int _animationFrames = 5;

	private int _delayPerAnimationFrame = 9;

	private Vector2 _smoothedBobble;

	public new string LocalizationCategory => "Projectiles.Pets";

	private Player Owner => Main.player[base.Projectile.owner];

	public AnimationState CurrentAnimation
	{
		get
		{
			return (AnimationState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
			switch (value)
			{
			case AnimationState.Idle:
				_animationFrames = 5;
				_delayPerAnimationFrame = 9;
				base.Projectile.frame = 0;
				base.Projectile.frameCounter = 0;
				break;
			case AnimationState.Pointing:
				_animationFrames = 9;
				_delayPerAnimationFrame = 5;
				base.Projectile.frame = 0;
				base.Projectile.frameCounter = 0;
				break;
			}
			base.Projectile.netUpdate = true;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 9;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 138;
		base.Projectile.height = 218;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		_smoothedBobble = base.Projectile.Center - Main.screenPosition;
	}

	public override void AI()
	{
		ShouldPetExist();
		DoMovement();
		DoAnimation();
	}

	private void ShouldPetExist()
	{
		if (!Owner.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (Owner.dead || (!Owner.Calamity().spiritOrigin && !Owner.Calamity().spiritOriginVanity))
		{
			Owner.Calamity().spiritOriginPet = false;
		}
		if (Owner.Calamity().spiritOriginPet)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	private void DoMovement()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.WithinRange(Owner.Center, 100f))
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.975f;
		}
		else
		{
			float flySpeed = MathHelper.Clamp(11f + base.Projectile.Distance(Owner.Center) * 0.015f, 11f, 25f);
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(base.Projectile.SafeDirectionTo(Owner.Center) * flySpeed, flySpeed * 0.02f);
			if (!base.Projectile.WithinRange(Owner.Center, 2200f))
			{
				base.Projectile.Center = Owner.Center;
				base.Projectile.velocity = -Vector2.UnitY * 4f;
			}
		}
		if (MathHelper.Distance(base.Projectile.Center.X, Owner.Center.X) > 80f)
		{
			base.Projectile.spriteDirection = (base.Projectile.Center.X > Owner.Center.X).ToDirectionInt();
		}
	}

	public void DoAnimation()
	{
		if (CurrentAnimation == AnimationState.Idle)
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter == _delayPerAnimationFrame)
			{
				base.Projectile.frame = (base.Projectile.frame + 1) % _animationFrames;
				base.Projectile.frameCounter = 0;
			}
			return;
		}
		if (base.Projectile.frame == 6 && base.Projectile.frameCounter != 8)
		{
			base.Projectile.frameCounter++;
			return;
		}
		if (Owner.miscCounter % ((base.Projectile.frame > 5) ? 8 : 4) == 0)
		{
			base.Projectile.frame = Math.Min(base.Projectile.frame + 1, _animationFrames - 1);
		}
		if (base.Projectile.frame == _animationFrames - 1)
		{
			CurrentAnimation = AnimationState.Idle;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		if (Owner.FindBuffIndex(ModContent.BuffType<ArcherofLunamoon>()) != -1)
		{
			Owner.ClearBuff(ModContent.BuffType<ArcherofLunamoon>());
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(2, 9, (int)CurrentAnimation, base.Projectile.frame);
		drawPosition += Vector2.UnitY * CalamityUtils.Convert01To010(Utils.GetLerpValue(0f, _animationFrames - 1, base.Projectile.frame, clamped: true)) * 12f;
		_smoothedBobble = Vector2.Lerp(_smoothedBobble, drawPosition, 0.05f);
		Main.EntitySpriteDraw(value, _smoothedBobble, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
		return false;
	}
}
