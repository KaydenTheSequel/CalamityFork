using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SeethingDischargeBrimstoneHellblast : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/Boss/BrimstoneHellblast";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 255;
		base.Projectile.alpha = 0;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			if (base.Projectile.npcProj)
			{
				base.Projectile.localNPCHitCooldown = 5;
				base.Projectile.penetrate = 10;
			}
			initialized = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 10)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.alpha++;
		Lighting.AddLight(base.Projectile.Center, 0.9f, 0f, 0f);
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
		base.Projectile.velocity.X *= 1.045f;
		base.Projectile.velocity.Y *= 1.045f;
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (float)Math.Atan2(0.0 - (double)base.Projectile.velocity.Y, 0.0 - (double)base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(250, 50, 50, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		for (int dust = 0; dust <= 5; dust++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 235);
		}
	}
}
