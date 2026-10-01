using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PrismaticWave : ModProjectile, ILocalizedModType, IModType
{
	private int alpha;

	public Color[] colors;

	public Particle starEffect;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 36;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = MeleeRangedHybridDamageClass.Instance;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 360;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		base.Projectile.alpha -= 16;
		if (base.Projectile.alpha < 64)
		{
			base.Projectile.alpha = 64;
		}
		Lighting.AddLight(base.Projectile.Center, (float)Main.DiscoR * 0.5f / 255f, (float)Main.DiscoG * 0.5f / 255f, (float)Main.DiscoB * 0.5f / 255f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Main.rand.NextBool())
		{
			int rainbow = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 267, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, alpha, Main.rand.Next(colors));
			Main.dust[rainbow].noGravity = true;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = Main.rand.NextFloat((float)Math.PI / 60f, (float)Math.PI / 12f) * (float)Main.rand.NextBool().ToDirectionInt();
		}
		if (starEffect == null)
		{
			Color projColor = Color.Lerp(Color.White, colors[(int)base.Projectile.ai[1]], 0.4f);
			starEffect = new GenericSparkle(base.Projectile.Center + base.Projectile.velocity * 1.5f, Vector2.Zero, projColor, colors[(int)base.Projectile.ai[1]], base.Projectile.scale * 2.5f, 2, Timer * base.Projectile.localAI[0]);
			GeneralParticleHandler.SpawnParticle(starEffect);
		}
		else
		{
			starEffect.Time = 0;
			starEffect.Position = base.Projectile.Center + base.Projectile.velocity * 1.5f;
		}
		if (base.Projectile.ai[2] == 1f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 400f, 18f, 20f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return colors[(int)base.Projectile.ai[1]];
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.RemoveParticle(starEffect);
		for (int k = 0; k < 3; k++)
		{
			int rainbow = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 267, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f, alpha, Main.rand.Next(colors));
			Main.dust[rainbow].noGravity = true;
		}
	}

	public PrismaticWave()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		alpha = 50;
		colors = (Color[])(object)new Color[12]
		{
			new Color(255, 0, 0, 50),
			new Color(255, 128, 0, 50),
			new Color(255, 255, 0, 50),
			new Color(128, 255, 0, 50),
			new Color(0, 255, 0, 50),
			new Color(0, 255, 128, 50),
			new Color(0, 255, 255, 50),
			new Color(0, 128, 255, 50),
			new Color(0, 0, 255, 50),
			new Color(128, 0, 255, 50),
			new Color(255, 0, 255, 50),
			new Color(255, 0, 128, 50)
		};
		base._002Ector();
	}
}
