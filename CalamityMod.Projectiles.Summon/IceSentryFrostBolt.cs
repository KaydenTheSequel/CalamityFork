using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class IceSentryFrostBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Magic/FrostBoltProjectile";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 480;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.01f / 255f, (float)(255 - base.Projectile.alpha) * 0.3f / 255f, (float)(255 - base.Projectile.alpha) * 0.45f / 255f);
		for (int i = 0; i < 2; i++)
		{
			float slowVelX = base.Projectile.velocity.X / 3f * (float)i;
			float slowVelY = base.Projectile.velocity.Y / 3f * (float)i;
			int four = 4;
			int dusty = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)four, base.Projectile.position.Y + (float)four), base.Projectile.width - four * 2, base.Projectile.height - four * 2, 92, 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[dusty];
			obj.noGravity = true;
			obj.velocity *= 0.1f;
			obj.velocity += base.Projectile.velocity * 0.1f;
			obj.position.X -= slowVelX;
			obj.position.Y -= slowVelY;
		}
		if (Main.rand.NextBool(10))
		{
			int otherFour = 4;
			int dustier = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)otherFour, base.Projectile.position.Y + (float)otherFour), base.Projectile.width - otherFour * 2, base.Projectile.height - otherFour * 2, 92, 0f, 0f, 100, default(Color), 0.6f);
			Dust obj2 = Main.dust[dustier];
			obj2.velocity *= 0.25f;
			Dust obj3 = Main.dust[dustier];
			obj3.velocity += base.Projectile.velocity * 0.5f;
		}
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
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
			SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 92, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 60);
	}
}
