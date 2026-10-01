using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HerringMinion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float MinionOrigin => ref base.Projectile.ai[0];

	public ref float HerringPosition => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.penetrate = -1;
		base.Projectile.width = 42;
		base.Projectile.height = 14;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		NPC target = base.Projectile.Center.MinionHoming(1200f, Owner);
		Projectile minionAI = Main.projectile[(int)MinionOrigin];
		if (target != null)
		{
			TargetPosition(minionAI);
		}
		else
		{
			FollowOrigin(minionAI);
		}
		CheckMinionExistance();
		DoAnimation();
		PointInRightDirection(minionAI, target);
		base.Projectile.MinionAntiClump();
		base.Projectile.netUpdate = true;
	}

	public void CheckMinionExistance()
	{
		Projectile minionAI = Main.projectile[(int)MinionOrigin];
		if (MinionOrigin < 0f || MinionOrigin >= (float)Main.projectile.Length)
		{
			base.Projectile.Kill();
		}
		else if (!minionAI.active || minionAI.type != ModContent.ProjectileType<HerringAI>())
		{
			base.Projectile.Kill();
		}
		else
		{
			base.Projectile.timeLeft = 2;
		}
	}

	public void DoAnimation()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 8 % Main.projFrames[base.Type];
	}

	public void PointInRightDirection(Projectile origin, NPC target)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (target != null)
		{
			base.Projectile.rotation = origin.rotation;
			base.Projectile.spriteDirection = origin.spriteDirection;
		}
		else
		{
			base.Projectile.rotation = ((base.Projectile.spriteDirection == -1) ? (base.Projectile.velocity.ToRotation() + (float)Math.PI) : base.Projectile.velocity.ToRotation());
			base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		}
	}

	public void FollowOrigin(Projectile origin)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.WithinRange(origin.Center, 1200f) && !base.Projectile.WithinRange(origin.Center, 300f))
		{
			base.Projectile.velocity = (origin.Center - base.Projectile.Center) / 30f;
		}
		else if (!base.Projectile.WithinRange(origin.Center, 160f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 37f + base.Projectile.SafeDirectionTo(origin.Center) * 17f) / 40f;
		}
		if (!base.Projectile.WithinRange(origin.Center, 1200f))
		{
			base.Projectile.position = origin.Center;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.3f;
		}
	}

	public void TargetPosition(Projectile origin)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, origin.Center + HerringPosition.ToRotationVector2() * 20f, 0.2f);
		base.Projectile.velocity = Vector2.Zero;
		int trailDust = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 33, 0f - base.Projectile.velocity.X, 0f - base.Projectile.velocity.Y);
		Main.dust[trailDust].noGravity = true;
		Main.dust[trailDust].customData = false;
	}
}
