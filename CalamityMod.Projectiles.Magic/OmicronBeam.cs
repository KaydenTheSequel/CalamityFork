using System.IO;
using System.Runtime.CompilerServices;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class OmicronBeam : ModProjectile, ILocalizedModType, IModType
{
	[CompilerGenerated]
	private Color _003CmainColor_003Ek__BackingField;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public ref float isSplit => ref base.Projectile.ai[1];

	public bool splitShot
	{
		get
		{
			return base.Projectile.ai[2] == 1f;
		}
		set
		{
			base.Projectile.ai[2] = (value ? 1f : 0f);
		}
	}

	public bool HitDirect { get; set; }

	public Color mainColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CmainColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CmainColor_003Ek__BackingField = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 75;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 91;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		if (isSplit == 0f)
		{
			base.Projectile.ForceNetUpdate();
			splitShot = true;
		}
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.timeLeft % 2 == 0 && time > 2f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 25, MathHelper.Clamp(0.34f - time * 0.07f, 0.085f, 0.34f), mainColor, new Vector2(0.5f, 1.3f)));
		}
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, scale: Main.rand.NextFloat(1.9f, 2.3f), velocity: base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.9f), affectedByGravity: false, lifetime: Main.rand.Next(40, 51), color: mainColor));
		}
		Vector2 dustVel = Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.1f, 0.8f);
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustVel, Main.rand.NextBool(4) ? 264 : 66, dustVel, 0, default(Color), Main.rand.NextFloat(0.9f, 1.2f));
		dust.noGravity = true;
		dust.color = (Main.rand.NextBool() ? Color.Lerp(mainColor, Color.White, 0.5f) : mainColor);
		time++;
		if (base.Projectile.numUpdates == 1)
		{
			base.Projectile.ForceNetUpdate();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		int numProj = 2;
		float rotation = MathHelper.ToRadians(10f);
		if (splitShot && time < 250f && !HitDirect)
		{
			for (int i = 0; i < numProj; i++)
			{
				Vector2 perturbedSpeed = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)(i / (numProj - 1))));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, ModContent.ProjectileType<OmicronBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f);
				for (int k = 0; k < 3; k++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + base.Projectile.velocity * 2f, Vector2.Zero, mainColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.8f, 0.4f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + base.Projectile.velocity * 2f, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.7f, 0.3f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
			}
			for (int j = 0; j <= 6; j++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, (base.Projectile.velocity * 15f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 0.4f), affectedByGravity: false, 10, Main.rand.NextFloat(0.02f, 0.04f), mainColor, new Vector2(2f, 0.7f), quickShrink: true));
			}
			for (int l = 0; l <= 9; l++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, (base.Projectile.velocity * 5f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 0.4f), affectedByGravity: false, 25, Main.rand.NextFloat(0.7f, 0.9f), mainColor));
			}
		}
		for (int m = 0; m < 28; m++)
		{
			Vector2 dustVel = base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1.5f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustVel + Main.rand.NextVector2Circular(6f, 6f), Main.rand.NextBool(4) ? 264 : 66, dustVel, 0, default(Color), Main.rand.NextFloat(0.9f, 1.2f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.Lerp(mainColor, Color.White, 0.5f) : mainColor);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		if (splitShot && time > 7f && !HitDirect)
		{
			base.Projectile.Kill();
		}
		Player Owner = Main.player[base.Projectile.owner];
		for (int i = 0; i <= 8; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 264 : 66, (base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 15f).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(1.2f, 1.6f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.Lerp(mainColor, Color.White, 0.5f) : mainColor);
		}
		if (!(time <= 7f) || !splitShot)
		{
			return;
		}
		modifiers.SourceDamage *= 5f;
		if (!HitDirect)
		{
			Owner.velocity += -base.Projectile.velocity;
			for (int j = 0; j <= 9; j++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, (base.Projectile.velocity * 15f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 0.4f), affectedByGravity: false, 11, Main.rand.NextFloat(0.05f, 0.07f), mainColor, new Vector2(2f, 0.5f), quickShrink: true));
			}
			for (int k = 0; k <= 13; k++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, (base.Projectile.velocity * 10f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 0.4f), affectedByGravity: false, 25, Main.rand.NextFloat(0.7f, 0.9f), mainColor));
			}
			for (int l = 0; l < 20; l++)
			{
				Vector2 shootVel = (base.Projectile.velocity * 20f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.8f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 267 : 66, shootVel);
				dust2.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust2.noGravity = true;
				dust2.color = (Main.rand.NextBool() ? Color.Lerp(mainColor, Color.White, 0.5f) : mainColor);
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ArtemisApolloDash");
			style.Volume = 1.25f;
			style.Pitch = 0.6f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		HitDirect = true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (time <= 7f) ? 90 : 20, targetHitbox);
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(HitDirect);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		HitDirect = reader.ReadBoolean();
	}

	public OmicronBeam()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		mainColor = Color.MediumVioletRed;
		base._002Ector();
	}
}
