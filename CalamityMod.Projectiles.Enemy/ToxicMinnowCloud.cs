using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class ToxicMinnowCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.hostile = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 7;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.alpha = 120;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.1f * base.Projectile.Opacity, 1f * base.Projectile.Opacity, 0f);
		if (Main.rand.NextBool())
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		else if (Main.rand.NextBool())
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.9f;
		}
		else if (Main.rand.NextBool())
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.85f;
		}
		else
		{
			Projectile projectile4 = base.Projectile;
			projectile4.velocity *= 0.8f;
		}
		base.Projectile.ai[0]++;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.ai[0] < 560f && base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.ai[0] > 560f)
		{
			base.Projectile.damage = 0;
		}
		else if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		Projectile projectile5 = base.Projectile;
		projectile5.velocity *= 0.98f;
		if (Math.Abs(base.Projectile.velocity.X) > 0f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 20f, targetHitbox);
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.ai[0] < 560f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(20, 240);
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.ai[0] < 560f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(20, 240);
	}
}
