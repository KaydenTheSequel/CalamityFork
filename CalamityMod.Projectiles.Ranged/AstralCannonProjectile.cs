using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AstralCannonProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Boss/AstralFlame";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 5;
		}
		else
		{
			base.Projectile.alpha = 0;
			base.Projectile.tileCollide = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
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
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.5f, 0.1f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.075f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 192);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Zombie103, base.Projectile.Center);
		for (int i = 0; i < 2; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 50);
		}
		for (int j = 0; j < 20; j++)
		{
			int orangeDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[orangeDust].noGravity = true;
			Dust obj = Main.dust[orangeDust];
			obj.velocity *= 3f;
			orangeDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 50);
			Dust obj2 = Main.dust[orangeDust];
			obj2.velocity *= 2f;
			Main.dust[orangeDust].noGravity = true;
		}
	}
}
