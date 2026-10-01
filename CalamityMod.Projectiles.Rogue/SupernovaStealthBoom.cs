using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SupernovaStealthBoom : ModProjectile, ILocalizedModType, IModType
{
	private float radius;

	public bool damageFrame;

	public bool doDamage;

	public Color variedColor;

	public Color mainColor;

	public Color randomColor;

	public int colorTimer;

	public int time;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 408;
		base.Projectile.height = 410;
		base.Projectile.scale = 1f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09be: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		randomColor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.Red, 
			1 => Color.MediumTurquoise, 
			2 => Color.Orange, 
			_ => Color.LawnGreen, 
		});
		if (time == 0)
		{
			mainColor = randomColor;
		}
		if (time % 20 == 0)
		{
			variedColor = (Color)(colorTimer switch
			{
				0 => Color.Red, 
				1 => Color.MediumTurquoise, 
				2 => Color.Orange, 
				_ => Color.LawnGreen, 
			});
			colorTimer++;
			if (colorTimer >= 4)
			{
				colorTimer = 0;
			}
		}
		mainColor = Color.Lerp(mainColor, variedColor, 0.07f);
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref mainColor)).ToVector3() * 4f);
		bool photosen = CalamityClientConfig.Instance.Photosensitivity;
		if (time <= 72)
		{
			float orbScale = MathHelper.Clamp(Utils.GetLerpValue(85f, 0f, time), 0f, 1f);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 2.5f * orbScale, 2.5f * orbScale, 4, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			if (!photosen)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 2f * orbScale, 2f * orbScale, 4, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			radius = 90f * orbScale;
			float numberOfDusts = 2f;
			float rotFactor = 360f / numberOfDusts;
			for (int i = 0; (float)i < numberOfDusts; i++)
			{
				randomColor = (Color)(Main.rand.Next(4) switch
				{
					0 => Color.Red, 
					1 => Color.MediumTurquoise, 
					2 => Color.Orange, 
					_ => Color.LawnGreen, 
				});
				MathHelper.ToRadians((float)i * rotFactor);
				Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f);
				velOffset *= Main.rand.NextFloat(25f, 45f) * orbScale;
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + velOffset * 2.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f), Main.rand.NextFloat(0.3f, 0.5f) * orbScale, randomColor, 9));
			}
		}
		if (time == 40)
		{
			GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, randomColor * 0.4f, new Vector2(1f, 1f), 0f, 5f, 0f, 40));
		}
		if (time == 50)
		{
			GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, randomColor * 0.4f, new Vector2(1f, 1f), 0f, 5f, 0f, 30));
		}
		if (time == 60)
		{
			GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, randomColor * 0.4f, new Vector2(1f, 1f), 0f, 5f, 0f, 20));
		}
		if (time == 70)
		{
			doDamage = false;
			float rotation = (Main.rand.NextBool() ? 2.5f : (-2.5f));
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.White, randomColor, 3f, 9, rotation, 2f));
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.White, randomColor, 3.5f, 9, rotation, 2f));
		}
		if (time == 80)
		{
			radius = 2500f;
			SoundStyle style = Supernova.StealthExplosionSound with
			{
				Pitch = base.Projectile.ai[2]
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.numHits = 0;
			damageFrame = true;
			doDamage = true;
			Owner.SetScreenshake(14.5f);
			for (int j = 0; j < 55; j++)
			{
				Vector2 randVel = Utils.RotatedByRandom(new Vector2(35f, 35f), 100.0) * Main.rand.NextFloat(0.05f, Main.rand.NextBool(3) ? 1f : 0.5f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, new Color(57, 46, 115) * 0.9f, Main.rand.Next(25, 36), Main.rand.NextFloat(0.9f, 2.3f), 0.5f));
			}
			for (int k = 0; k < 150; k++)
			{
				Vector2 randVel2 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.1f, 1.6f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + randVel2, 303, randVel2);
				dust.scale = Main.rand.NextFloat(1.75f, 2.5f);
				dust.noGravity = true;
				dust.color = new Color(57, 46, 115);
				dust.alpha = Main.rand.Next(40, 101);
			}
			if (!photosen)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 4.5f, 3.5f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 4f, 3f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			Vector2 BurstFXDirectionX = Vector2.UnitX * 5f;
			Vector2 BurstFXDirectionY = Vector2.UnitY * 5f;
			if (!photosen)
			{
				for (int l = 0; l < 8; l++)
				{
					randomColor = RandomizeColor();
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, BurstFXDirectionX * ((float)l + 1f), affectedByGravity: false, 12, (0.09f + (float)l * 0.02f) * 1.5f, randomColor, new Vector2(2.7f, 1.3f), quickShrink: true));
					randomColor = RandomizeColor();
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -BurstFXDirectionX * ((float)l + 1f), affectedByGravity: false, 12, (0.09f + (float)l * 0.02f) * 1.5f, randomColor, new Vector2(2.7f, 1.3f), quickShrink: true));
					randomColor = RandomizeColor();
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, BurstFXDirectionY * ((float)l + 1f), affectedByGravity: false, 12, (0.09f + (float)l * 0.02f) * 1.5f, randomColor, new Vector2(2.7f, 1.3f), quickShrink: true));
					randomColor = RandomizeColor();
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -BurstFXDirectionY * ((float)l + 1f), affectedByGravity: false, 12, (0.09f + (float)l * 0.02f) * 1.5f, randomColor, new Vector2(2.7f, 1.3f), quickShrink: true));
				}
			}
			for (int m = 0; m < 25; m++)
			{
				randomColor = RandomizeColor();
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), BurstFXDirectionX * Main.rand.NextFloat(1f, 20.5f), affectedByGravity: false, Main.rand.Next(40, 51), Main.rand.NextFloat(0.04f, 0.095f), randomColor, new Vector2(0.3f, 1.6f)));
				randomColor = RandomizeColor();
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), -BurstFXDirectionX * Main.rand.NextFloat(1f, 20.5f), affectedByGravity: false, Main.rand.Next(40, 51), Main.rand.NextFloat(0.04f, 0.095f), randomColor, new Vector2(0.3f, 1.6f)));
				randomColor = RandomizeColor();
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), BurstFXDirectionY * Main.rand.NextFloat(1f, 20.5f), affectedByGravity: false, Main.rand.Next(40, 51), Main.rand.NextFloat(0.04f, 0.095f), randomColor, new Vector2(0.3f, 1.6f)));
				randomColor = RandomizeColor();
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), -BurstFXDirectionY * Main.rand.NextFloat(1f, 20.5f), affectedByGravity: false, Main.rand.Next(40, 51), Main.rand.NextFloat(0.04f, 0.095f), randomColor, new Vector2(0.3f, 1.6f)));
			}
			for (int n = 0; n < 10; n++)
			{
				randomColor = RandomizeColor();
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, randomColor * 0.7f, "CalamityMod/Particles/FlameExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-20f, 20f), 0f, 4f - (float)n * 0.28f, 50, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		if (time < 80)
		{
			for (int num = 0; num < Main.maxNPCs; num++)
			{
				NPC target = Main.npc[num];
				if (target.CanBeMoved(ignoreKBImmune: true) && target.CanBeChasedBy(base.Projectile) && target != null && !CalamityPlayer.areThereAnyDamnBosses)
				{
					if (Vector2.Distance(target.Center, base.Projectile.Center) > 40f && Vector2.Distance(target.Center, base.Projectile.Center) < 2000f)
					{
						target.Center += target.Center.DirectionTo(base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 38f;
					}
					target.SyncMotionToServer();
				}
			}
		}
		time++;
		if (time >= 82)
		{
			base.Projectile.Kill();
		}
	}

	public Color RandomizeColor()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Color randomColor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.Red, 
			1 => Color.MediumTurquoise, 
			2 => Color.Orange, 
			_ => Color.LawnGreen, 
		});
		if (CalamityClientConfig.Instance.Photosensitivity)
		{
			randomColor *= 0.5f;
		}
		return randomColor;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (damageFrame)
		{
			target.AddBuff(ModContent.BuffType<MiracleBlight>(), 90);
			for (int i = 0; i <= MathHelper.Clamp(9 - base.Projectile.numHits, 3, 9); i++)
			{
				randomColor = (Color)(Main.rand.Next(4) switch
				{
					0 => Color.Red, 
					1 => Color.MediumTurquoise, 
					2 => Color.Orange, 
					_ => Color.LawnGreen, 
				});
				Vector2 vel = target.Center.DirectionFrom(base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 20f * Main.rand.NextFloat(0.05f, 1.2f);
				Dust dust = Dust.NewDustPerfect(target.Center + Main.rand.NextVector2Circular((float)target.width * 0.5f, (float)target.height * 0.5f), 66, vel);
				dust.scale = Main.rand.NextFloat(1.15f, 2f);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.White, randomColor, 0.9f);
			}
			Vector2 launchVel = base.Projectile.Center.DirectionTo(target.Center);
			target.MoveNPC(launchVel, 60f, ignoreKBImmune: true);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (!damageFrame)
		{
			modifiers.SourceDamage *= 0.0025f;
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override bool? CanDamage()
	{
		if (!doDamage)
		{
			return false;
		}
		return null;
	}

	public SupernovaStealthBoom()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		radius = 90f;
		doDamage = true;
		variedColor = Color.White;
		mainColor = Color.LawnGreen;
		randomColor = Color.White;
		base._002Ector();
	}
}
