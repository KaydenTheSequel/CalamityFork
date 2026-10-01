using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BrimlashProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.aiStyle = 27;
		base.AIType = 156;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.99f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 3f)
		{
			base.Projectile.Kill();
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.5f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f);
		int brimDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, 0f, 100);
		Main.dust[brimDust].noGravity = true;
		Dust obj = Main.dust[brimDust];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[brimDust];
		obj2.velocity += base.Projectile.velocity * 0.1f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 295)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int i = 4; i < 31; i++)
		{
			float dustX = base.Projectile.oldVelocity.X * (30f / (float)i);
			float dustY = base.Projectile.oldVelocity.Y * (30f / (float)i);
			int deathDust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - dustX, base.Projectile.oldPosition.Y - dustY), 8, 8, 235, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.8f);
			Main.dust[deathDust].noGravity = true;
			Dust obj = Main.dust[deathDust];
			obj.velocity *= 0.5f;
			deathDust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - dustX, base.Projectile.oldPosition.Y - dustY), 8, 8, 235, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
			Dust obj2 = Main.dust[deathDust];
			obj2.velocity *= 0.05f;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int j = 0; j < 3; j++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)j / 3f + base.Projectile.velocity.ToRotation()).ToRotationVector2() * 4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<Brimlash2>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
	}
}
