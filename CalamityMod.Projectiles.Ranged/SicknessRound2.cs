using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SicknessRound2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
		base.Projectile.light = 0.15f;
		base.Projectile.extraUpdates = 1;
		base.AIType = 1;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 150 && target.CanBeChasedBy(base.Projectile);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool PreAI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = -base.Projectile.velocity * 0.5f;
		float x2 = base.Projectile.Center.X - base.Projectile.velocity.X / 10f;
		float y2 = base.Projectile.Center.Y - base.Projectile.velocity.Y / 10f;
		int dust = Dust.NewDust(new Vector2(x2, y2), 1, 1, 107);
		Main.dust[dust].alpha = base.Projectile.alpha;
		Main.dust[dust].position.X = x2;
		Main.dust[dust].position.Y = y2;
		Main.dust[dust].velocity = dspeed;
		Main.dust[dust].noGravity = true;
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
		if (base.Projectile.timeLeft < 150)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 450f, 12f, 25f);
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}
}
