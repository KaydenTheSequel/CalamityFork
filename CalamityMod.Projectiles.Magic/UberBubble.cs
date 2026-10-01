using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class UberBubble : ModProjectile, ILocalizedModType, IModType
{
	public Color EffectsColor;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 30;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.975f;
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 30;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Vector2 v = base.Projectile.ai[0].ToRotationVector2();
		float projRotation = base.Projectile.velocity.ToRotation();
		double rotationClamp = v.ToRotation() - projRotation;
		if (rotationClamp > 3.1415927410125732)
		{
			rotationClamp -= 6.2831854820251465;
		}
		if (rotationClamp < -3.1415927410125732)
		{
			rotationClamp -= -6.2831854820251465;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (base.Projectile.timeLeft == 26)
		{
			for (int i = 0; i <= 10; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(5) ? 111 : 86, base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.3f, 0.5f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.85f, 1.4f);
			}
			for (int j = 0; j <= 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 0.4f), Main.rand.NextFloat(0.2f, 0.4f), Color.Purple, Main.rand.Next(0, 41), 0.25f, 2f));
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in Effervescence.BurstSound, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.Purple, Color.PeachPuff, Main.rand.NextFloat(0.5f, 0.6f), 30, 0.1f, 3f));
		GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center + base.Projectile.velocity, Vector2.Zero, Color.MediumPurple, Color.LightPink, Main.rand.NextFloat(0.5f, 0.6f), 20, 0.1f, 3f));
		int randDustAmt = Main.rand.Next(4, 6);
		for (int i = 0; i < randDustAmt; i++)
		{
			int purpleDust = Dust.NewDust(base.Projectile.Center, 0, 0, 171, 0f, 0f, 100, default(Color), 1.4f);
			Dust obj = Main.dust[purpleDust];
			obj.velocity *= 0.8f;
			Main.dust[purpleDust].position = Vector2.Lerp(Main.dust[purpleDust].position, base.Projectile.Center, 0.5f);
			Main.dust[purpleDust].noGravity = true;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int numBubbles = 0; numBubbles < 3; numBubbles++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(20f)) * Main.rand.NextFloat(0.5f, 2f), ModContent.ProjectileType<BlueBubble>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}
}
