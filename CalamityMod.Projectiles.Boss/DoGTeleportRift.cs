using System;
using System.IO;
using CalamityMod.Effects;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Particles;
using CalamityMod.Skies;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGTeleportRift : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle CrackSound = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenHit", 3);

	public static readonly SoundStyle BreakSound = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenPhaseTransitionCrack");

	public bool FakeRift;

	public int RiftLifetime;

	public NPC DoGHead => Main.npc[(int)base.Projectile.ai[0]];

	public ref float Timer => ref base.Projectile.ai[1];

	public ref float CrackScale => ref base.Projectile.ai[2];

	public ref float AIState => ref base.Projectile.localAI[0];

	public ref float CrackExposure => ref base.Projectile.localAI[1];

	public ref float MaxExposure => ref base.Projectile.localAI[2];

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.Opacity = 0f;
		base.Projectile.scale = 0f;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = RiftLifetime;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(FakeRift);
		writer.Write(RiftLifetime);
		for (int i = 0; i < base.Projectile.localAI.Length; i++)
		{
			writer.Write(base.Projectile.localAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		FakeRift = reader.ReadBoolean();
		RiftLifetime = reader.ReadInt32();
		for (int i = 0; i < base.Projectile.localAI.Length; i++)
		{
			base.Projectile.localAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		if (DoGHead == null || !DoGHead.active)
		{
			base.Projectile.Kill();
			return;
		}
		if (AIState == 0f)
		{
			int riftCrackInterval = (int)((float)RiftLifetime / 3f);
			if (Timer % (float)riftCrackInterval == 0f)
			{
				float effectsLifetimeInterpolant = MathHelper.Lerp(1f, 2f, Timer / (float)RiftLifetime);
				if (!FakeRift)
				{
					float crackPitch = MathHelper.Lerp(-0.75f, 0f, Timer / (float)RiftLifetime);
					SoundStyle style = CrackSound with
					{
						Pitch = crackPitch
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					for (int i = 0; i < 12; i++)
					{
						Vector2 sparkVelocity = Vector2.UnitX.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(8f, 12f) * effectsLifetimeInterpolant * 0.7f;
						float sparkScale = Main.rand.NextFloat(1.2f, 1.6f) * effectsLifetimeInterpolant * 0.62f;
						Color sparkColor = Color.Lerp(Utils.SelectRandom(Main.rand, (Color[])(object)new Color[3]
						{
							Color.Fuchsia,
							DoGSky.DoGLightBlue,
							DoGSky.DoGTwlight
						}), Color.White, 0.65f);
						GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, Main.rand.Next(30, 45), sparkScale, sparkColor));
					}
					for (int j = 0; j < 8; j++)
					{
						Vector2 lightVelocity = Vector2.UnitX.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(12f, 14f) * effectsLifetimeInterpolant * 0.5f;
						float lightScale = Main.rand.NextFloat(1.2f, 1.6f) * effectsLifetimeInterpolant * 0.62f;
						Color lightColor = Color.Lerp(Utils.SelectRandom(Main.rand, (Color[])(object)new Color[3]
						{
							Color.Fuchsia,
							DoGSky.DoGLightBlue,
							DoGSky.DoGTwlight
						}), Color.White, 0.65f);
						GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, lightVelocity, lightScale, lightColor, Main.rand.Next(30, 45)));
					}
					CalamityUtils.AddScreenshakeAt(base.Projectile.Center, 6f * effectsLifetimeInterpolant * 0.8f);
				}
				float brightnessMultiplier = (FakeRift ? 0.75f : 1f);
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * brightnessMultiplier, "CalamityMod/Particles/ShineExplosion2", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.5f * effectsLifetimeInterpolant, 45, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				CrackScale += 0.525f * effectsLifetimeInterpolant * 0.75f;
				MaxExposure = MathHelper.Clamp(MaxExposure + 0.25f * effectsLifetimeInterpolant, 0f, 0.95f);
			}
			if (Timer >= (float)RiftLifetime)
			{
				SwitchAIStates();
			}
			if (base.Projectile.timeLeft < 45)
			{
				base.Projectile.timeLeft = 45;
			}
			CrackExposure = MathHelper.Lerp(CrackExposure, MaxExposure, 0.075f);
			base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.05f, 0f, 1f);
			base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale + 0.02f, 0f, 1f);
			base.Projectile.ForceNetUpdate();
		}
		if (AIState == 1f)
		{
			base.Projectile.Opacity = MathHelper.Lerp(1f, 0f, Timer / 45f);
			CrackExposure = MathHelper.Lerp(MaxExposure, 0f, Timer / 45f);
		}
		Timer++;
	}

	public void SwitchAIStates()
	{
		AIState = 1f;
		Timer = 0f;
		CrackScale += 3f;
		base.Projectile.timeLeft = 30;
		SpawnExplosionVisuals();
		base.Projectile.netUpdate = true;
	}

	public void SpawnExplosionVisuals()
	{
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		float brightnessMultiplier = (CalamityClientConfig.Instance.Photosensitivity ? 0.6f : 1f);
		if (!FakeRift)
		{
			for (int i = 0; i < 25; i++)
			{
				Vector2 crackOffset = Vector2.UnitX.RotatedBy((float)i * ((float)Math.PI * 2f) / 25f) * Main.rand.NextFloat(20f, 80f);
				Vector2 crackLength = base.Projectile.Center + crackOffset - base.Projectile.Center;
				float riftWidth = Main.rand.NextFloat(20f, 30f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, crackLength, ModContent.ProjectileType<DoGRiftCrack>(), 0, 0f, -1, 0f, riftWidth);
			}
			for (int j = 0; j < 35; j++)
			{
				DoGDistortionMetaball.SpawnParticle(base.Projectile.Center + Main.rand.NextVector2Circular(25f, 25f), Vector2.Zero, 500f, square: true, Main.rand.NextFloat((float)Math.PI * 2f));
			}
			int metaballAmount = Main.rand.Next(15, 21);
			for (int k = 0; k < metaballAmount; k++)
			{
				DoGDistortionMetaball.SpawnParticle(base.Projectile.Center, Main.rand.NextVector2Unit() * Main.rand.NextFloat(25f, 35f), Main.rand.NextFloat(30f, 50f));
			}
			for (int l = 0; l < 25; l++)
			{
				Vector2 sparkVelocity = Vector2.UnitX.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(12f, 16f);
				float sparkScale = Main.rand.NextFloat(1.8f, 2f);
				Color sparkColor = Color.Lerp(Utils.SelectRandom(Main.rand, (Color[])(object)new Color[3]
				{
					Color.Fuchsia,
					DoGSky.DoGLightBlue,
					DoGSky.DoGTwlight
				}), Color.White, 0.65f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, Main.rand.Next(30, 45), sparkScale, sparkColor));
			}
			for (int m = 0; m < 20; m++)
			{
				Vector2 lightVelocity = Vector2.UnitX.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(16f, 20f);
				float lightScale = Main.rand.NextFloat(1.8f, 2f);
				Color lightColor = Color.Lerp(Utils.SelectRandom(Main.rand, (Color[])(object)new Color[3]
				{
					Color.Fuchsia,
					DoGSky.DoGLightBlue,
					DoGSky.DoGTwlight
				}), Color.White, 0.65f);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, lightVelocity, lightScale, lightColor, Main.rand.Next(30, 45)));
			}
			for (int n = 0; n < 3; n++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, DoGSky.DoGTwlight * 0.8f * brightnessMultiplier, "CalamityMod/Particles/PlasmaExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 1.25f + (float)n * 0.05f, 45, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, DoGSky.DoGTwlight * brightnessMultiplier, "CalamityMod/Particles/ShineExplosion1", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.75f, 45, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.6f * brightnessMultiplier, "CalamityMod/Particles/ShineExplosion2", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.5f, 45, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new StrongBloom(base.Projectile.Center, Vector2.Zero, Color.White * 0.8f * brightnessMultiplier, 8f, 30));
			SoundEngine.PlaySound(in BreakSound, base.Projectile.Center);
			CalamityUtils.AddScreenshakeAt(base.Projectile.Center, 14f);
		}
		else
		{
			for (int num = 0; num < 3; num++)
			{
				GeneralParticleHandler.SpawnParticle(new PulseRing(base.Projectile.Center, Vector2.Zero, Color.White * 0.45f * brightnessMultiplier, 0f, 3f + (float)num * 0.1f, 30 + num * 15));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D crackTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/CrackedGlass_Glowing", (AssetRequestMode)2).Value;
		Texture2D starTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/StarProj", (AssetRequestMode)2).Value;
		Texture2D bloomRingTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRing", (AssetRequestMode)2).Value;
		float chargeInterpolant = CalamityUtils.SineInOutEasing(Timer / (float)RiftLifetime, 1);
		float brightnessMultiplier = (FakeRift ? 0.6f : 1f);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 verticalStarPartScale = new Vector2(1f, 8f) * chargeInterpolant * base.Projectile.scale * 3.25f;
		Vector2 horizontalStarPartScale = new Vector2(1f, 5f) * chargeInterpolant * base.Projectile.scale * 3.25f;
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Main.EntitySpriteDraw(starTexture, drawPosition, null, Color.White * 0.7f * brightnessMultiplier * chargeInterpolant, 0f, starTexture.Size() * 0.5f, verticalStarPartScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(starTexture, drawPosition, null, Color.White * 0.7f * brightnessMultiplier * chargeInterpolant, (float)Math.PI / 2f, starTexture.Size() * 0.5f, horizontalStarPartScale, (SpriteEffects)0);
		if (!FakeRift)
		{
			Main.EntitySpriteDraw(starTexture, drawPosition, null, DoGSky.DoGTwlight * 0.9f * brightnessMultiplier * chargeInterpolant, 0f, starTexture.Size() * 0.5f, verticalStarPartScale * 0.8f, (SpriteEffects)0);
			Main.EntitySpriteDraw(starTexture, drawPosition, null, DoGSky.DoGTwlight * 0.9f * brightnessMultiplier * chargeInterpolant, (float)Math.PI / 2f, starTexture.Size() * 0.5f, horizontalStarPartScale * 0.8f, (SpriteEffects)0);
		}
		Effect crackShader = CalamityShaders.DoGRealityCrackShader.Value;
		float crackOpcity = ((AIState == 1f) ? (base.Projectile.Opacity * 0.1f) : 0.1f);
		Color darkerPixelColor = (FakeRift ? Color.White : DoGSky.DoGTwlight);
		crackShader.Parameters["opacityCutoffValue"].SetValue(CrackExposure);
		crackShader.Parameters["fadeoutPower"].SetValue(1f);
		crackShader.Parameters["overallOpacity"].SetValue(crackOpcity * brightnessMultiplier);
		crackShader.Parameters["minBrightnessValue"].SetValue(0.75f);
		crackShader.Parameters["darkerPixelColor"].SetValue(((Color)(ref darkerPixelColor)).ToVector3());
		EffectParameter obj = crackShader.Parameters["brighterPixelColor"];
		Color white = Color.White;
		obj.SetValue(((Color)(ref white)).ToVector3());
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, crackShader, Main.GameViewMatrix.TransformationMatrix);
			int crackCount = (FakeRift ? 1 : 3);
			for (int i = 0; i < crackCount; i++)
			{
				float crackRotation = (float)i * ((float)Math.PI * 2f) / (float)crackCount;
				Main.EntitySpriteDraw(crackTexture, drawPosition + Vector2.UnitY * 8f, null, Color.White * base.Projectile.Opacity, crackRotation, crackTexture.Size() * 0.5f, CrackScale, (SpriteEffects)0);
			}
			Main.spriteBatch.End();
		}
		if (AIState != 1f)
		{
			float ringScale = MathHelper.Lerp(8f, 0f, chargeInterpolant);
			Color ringColor = (FakeRift ? Color.White : Color.Lerp(Color.White, DoGSky.DoGTwlight, chargeInterpolant));
			Main.EntitySpriteDraw(bloomRingTexture, drawPosition, null, ringColor * brightnessMultiplier * chargeInterpolant * 0.6f, 0f, bloomRingTexture.Size() * 0.5f, ringScale, (SpriteEffects)0);
		}
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}
}
