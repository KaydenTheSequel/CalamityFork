using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class FrostBoltProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int wallBounces;

	public float fadeIn;

	public bool launch;

	public Color bColor;

	public ref float time => ref base.Projectile.ai[0];

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 480;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.coldDamage = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref bColor)).ToVector3());
		fadeIn = Utils.GetLerpValue(0f, (float)Owner.itemAnimationMax * 0.5f * (float)base.Projectile.MaxUpdates, time, clamped: true);
		Vector2 velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * Owner.HeldItem.shootSpeed;
		base.Projectile.scale = fadeIn;
		if (fadeIn < 1f)
		{
			base.Projectile.Center = Owner.Center + velocity.SafeNormalize(Vector2.UnitX) * 48f;
		}
		else
		{
			if (launch)
			{
				base.Projectile.tileCollide = true;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenHit", 3);
				style.Volume = 0.45f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 8; i++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(0.5f, 1.5f));
					dust.noGravity = false;
					dust.scale = Main.rand.NextFloat(0.65f, 1f);
					dust.color = bColor;
					dust.noLightEmittence = true;
				}
				base.Projectile.velocity = velocity;
				launch = false;
			}
			if (Main.rand.NextBool(15))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), -base.Projectile.velocity * 0.3f, "CalamityMod/Particles/IceTypeParticle", affectedByGravity: false, 32, 0.9f, Color.Lerp(bColor, Color.White, 0.5f), new Vector2(0.8f, 1f)));
			}
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.3f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 7, 0.2f, bColor * 0.9f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f));
		}
		if (base.Projectile.timeLeft < 260)
		{
			base.Projectile.velocity.X *= 0.9711f;
			if (base.Projectile.velocity.Y < 15f)
			{
				base.Projectile.velocity.Y += 0.19f;
			}
			if (base.Projectile.velocity.Y < 5f)
			{
				base.Projectile.velocity.Y *= 0.977f;
			}
			wallBounces = 2;
		}
		else if (wallBounces > 1)
		{
			base.Projectile.timeLeft--;
		}
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
		time++;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			base.Projectile.localNPCImmunity[i] = 0;
		}
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		float blastSize = 80f;
		float minMultiplier = 0.25f;
		int hitsToMinMult = 4;
		int debuff = 44;
		int debuffTime = 120;
		Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
		projectile.localAI[0] = debuff;
		projectile.localAI[1] = debuffTime;
		projectile.timeLeft = 15;
		projectile.DamageType = DamageClass.Magic;
		SoundEngine.PlaySound(SoundID.Item27 with
		{
			Volume = 0.5f,
			Pitch = 0.3f,
			MaxInstances = -1
		}, base.Projectile.Center);
		SoundEngine.PlaySound(SoundID.Item27 with
		{
			Volume = 0.5f,
			Pitch = -0.3f,
			MaxInstances = -1
		}, base.Projectile.Center);
		float rot = Main.rand.NextFloat(-2f, 2f);
		for (int j = 0; j < 6; j++)
		{
			Vector2 velocity = ((float)Math.PI * 2f * (float)j / 6f).ToRotationVector2().RotatedBy(rot) * 6f;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + velocity * 3f, velocity * 0.5f, "CalamityMod/Particles/IceTypeParticle", affectedByGravity: false, 25, 1.3f, Color.Lerp(bColor, Color.White, 0.5f), new Vector2(1f, 1.8f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.45f));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), velocity * 1.5f);
			dust.noGravity = true;
			dust.scale = 1.3f;
			dust.color = bColor;
			dust.noLightEmittence = true;
		}
		if (wallBounces >= 2)
		{
			base.Projectile.Kill();
		}
		wallBounces++;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(44, 60);
		if (base.Projectile.damage > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.85f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		Color backglowColor = bColor;
		((Color)(ref backglowColor)).A = 0;
		projectile.DrawProjectileWithBackglow(backglowColor, Color.White, 2f * fadeIn, null, null, (SpriteEffects)0);
		return false;
	}

	public FrostBoltProjectile()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		launch = true;
		bColor = Color.DeepSkyBlue;
		base._002Ector();
	}
}
