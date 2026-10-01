using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ToxicCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 900;
		base.Projectile.alpha = 125;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0.7f, 0f);
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 5f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		base.Projectile.ai[0]++;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.ai[0] < 860f && base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.ai[0] > 860f)
		{
			base.Projectile.damage = 0;
		}
		else if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		base.Projectile.DrawProjectileWithBackglow(new Color(66, 236, 113), lightColor, 3.75f, null, null, (SpriteEffects)0);
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
		return base.Projectile.ai[0] < 860f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 480);
		}
	}
}
