using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BouncingEyeballProjectileStealthStrike : ModProjectile, ILocalizedModType, IModType
{
	public const float Bounciness = 1.35f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 280;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Math.Abs(base.Projectile.velocity.X) > 23f)
		{
			base.Projectile.velocity.X = (float)Math.Sign(base.Projectile.velocity.X) * 23f;
		}
		if (Math.Abs(base.Projectile.velocity.Y) > 23f)
		{
			base.Projectile.velocity.Y = (float)Math.Sign(base.Projectile.velocity.Y) * 23f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity != oldVelocity)
		{
			base.Projectile.velocity = Main.rand.NextFloat(-1.15f, -0.85f) * oldVelocity * 1.35f;
		}
		SoundEngine.PlaySound(SoundID.NPCHit19 with
		{
			Volume = SoundID.NPCHit19.Volume * 0.7f
		}, base.Projectile.Center);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.NPCHit19 with
		{
			Volume = SoundID.NPCHit19.Volume * 0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int dustCount = Main.rand.Next(15, 26);
		for (int index = 0; index < dustCount; index++)
		{
			Vector2 velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(4f, 9f) + base.Projectile.velocity / 2f;
			Dust.NewDust(base.Projectile.Center, 4, 4, 5, velocity.X, velocity.Y);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
