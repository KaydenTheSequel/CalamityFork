using System;
using CalamityMod.Graphics.Metaballs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SmallSpirit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public Projectile ProjectileOwner
	{
		get
		{
			int spiritType = ModContent.ProjectileType<SpiritCongregation>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == spiritType && (float)p.identity == base.Projectile.ai[0] && p.owner == base.Projectile.owner)
				{
					return p;
				}
			}
			return null;
		}
	}

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 28);
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 360;
	}

	public override void AI()
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		float radius = MathHelper.SmoothStep(67f, 32f, (float)Math.Sqrt(1f - (float)base.Projectile.timeLeft / 360f));
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		float maxOpacity = 1f;
		Entity target = Owner;
		float flySpeed = 8f;
		float flyInertia = 54f;
		if (ProjectileOwner != null && (ProjectileOwner.ModProjectile<SpiritCongregation>().CurrentPower > 0.97f || base.Projectile.timeLeft < 95))
		{
			radius += 36f;
			target = ProjectileOwner;
			flySpeed = 29f;
			flyInertia = 5f;
			base.Projectile.Center = base.Projectile.Center.MoveTowards(target.Center, 8.5f);
			if (base.Projectile.WithinRange(target.Center, 80f))
			{
				base.Projectile.Kill();
			}
			maxOpacity = 0.4f;
		}
		if (target == null || ProjectileOwner == null || !ProjectileOwner.active)
		{
			base.Projectile.Kill();
			return;
		}
		if (!base.Projectile.WithinRange(target.Center, 260f))
		{
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(target.Center) * flySpeed;
			base.Projectile.velocity = (base.Projectile.velocity * (flyInertia - 1f) + idealVelocity) / flyInertia;
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.075f, 0f, maxOpacity);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		GruesomeMetaball.SpawnParticle(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f) * radius / 130f, Main.rand.NextVector2Circular(6f, 6f), radius);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * base.Projectile.Opacity;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(12f, 12f), 261);
			dust.color = Color.Lerp(Color.LightPink, Color.Red, Main.rand.NextFloat(0.67f));
			dust.scale = 1.2f;
			dust.fadeIn = 0.55f;
			dust.noGravity = true;
		}
	}
}
