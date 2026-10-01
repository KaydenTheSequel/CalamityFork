using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class SandPoisonCloudOldDuke : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 1800;
		base.Projectile.alpha = 80;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = 1;
		if (base.Projectile.Center.X < Main.LocalPlayer.Center.X)
		{
			base.Projectile.spriteDirection = -1;
		}
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0.7f, 0f);
		base.Projectile.ai[0]++;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.ai[0] < 1620f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Top + new Vector2(Main.rand.NextFloat(-12f, 12f), 6f), new Vector2(0f, 0f - Main.rand.NextFloat(2f)), affectedByGravity: false, 20, Main.rand.NextFloat(0.5f, 1.2f), new Color(100, 255, 0)));
			if (base.Projectile.frame >= 4)
			{
				base.Projectile.frame = 0;
			}
		}
		if (base.Projectile.ai[0] > 1620f)
		{
			base.Projectile.damage = 0;
		}
		else if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.995f;
		if (Math.Abs(base.Projectile.velocity.X) > 0f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(100f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(155f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(55f * base.Projectile.Opacity);
		((Color)(ref lightColor)).A = 0;
		base.Projectile.DrawProjectileWithBackglow(new Color(20, 60, 26, 0), Color.White, 2f, null, null, (SpriteEffects)0);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor);
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
		return base.Projectile.ai[0] < 1620f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 480);
		}
	}
}
