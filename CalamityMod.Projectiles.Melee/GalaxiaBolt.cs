using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GalaxiaBolt : ModProjectile, ILocalizedModType, IModType
{
	public NPC target;

	public bool StrongerHoming;

	private Particle Head;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Hue => ref base.Projectile.ai[0];

	public ref float HomingStrength => ref base.Projectile.ai[1];

	public ref float ShouldDelayHoming => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 80;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (Head == null)
		{
			Head = new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.White, Main.hslToRgb(Hue, 100f, 50f), 1.2f, 2, 0.06f, 3f);
			GeneralParticleHandler.SpawnParticle(Head);
		}
		else
		{
			Head.Position = base.Projectile.Center + base.Projectile.velocity * 0.5f;
			Head.Time = 0;
			Head.Scale += (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f) * 0.02f * base.Projectile.scale;
		}
		if (target == null)
		{
			target = base.Projectile.Center.ClosestNPCAt(812f);
		}
		else if ((base.Projectile.velocity.AngleBetween(target.Center - base.Projectile.Center) < (float)Math.PI / 2f || StrongerHoming) && (ShouldDelayHoming != 1f || base.Projectile.timeLeft < 60))
		{
			float idealDirection = base.Projectile.AngleTo(target.Center);
			float updatedDirection = base.Projectile.velocity.ToRotation().AngleTowards(idealDirection, HomingStrength);
			base.Projectile.velocity = updatedDirection.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length() * 0.995f;
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f, 1f, 0.24f);
		if (Main.rand.NextBool(2))
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, Color.Lerp(Color.MidnightBlue, Color.Indigo, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f)), 30, Main.rand.NextFloat(0.6f, 1.2f) * base.Projectile.scale, 0.8f, 0f, glowing: false, 0f, required: true));
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, Main.hslToRgb(Hue, 1f, 0.7f), 20, Main.rand.NextFloat(0.4f, 0.7f) * base.Projectile.scale, 0.8f, 0f, glowing: true, 0.005f, required: true));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
