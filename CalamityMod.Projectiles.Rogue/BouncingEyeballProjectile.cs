using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BouncingEyeballProjectile : ModProjectile, ILocalizedModType, IModType
{
	private int Bounces = 5;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/BouncingEyeball";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 2;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.NPCHit19 with
		{
			Volume = SoundID.NPCHit19.Volume * 0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Bounces--;
		if (Bounces <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override void AI()
	{
		if (base.Projectile.velocity.Y <= 10f)
		{
			base.Projectile.velocity.Y += 0.15f;
		}
		base.Projectile.rotation += MathHelper.ToRadians(5f) * (float)Math.Sign(base.Projectile.velocity.X);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.NPCHit19 with
		{
			Volume = SoundID.NPCHit19.Volume * 0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int dustCount = Main.rand.Next(8, 16);
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
