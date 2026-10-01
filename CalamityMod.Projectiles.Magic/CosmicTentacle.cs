using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CosmicTentacle : ModProjectile, ILocalizedModType, IModType
{
	public bool preDamage;

	public bool moving;

	public float scaling;

	public int scalingTimer;

	public Color InnerColor;

	public int curveDirection;

	public int curves;

	public int scalingTimerMax;

	public float damageMult;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 90;
		base.Projectile.height = 90;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 8;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8 * base.Projectile.extraUpdates;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0816: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(obj.Center, base.Projectile.Center);
		Vector2 vel = ((obj.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 7f).RotatedBy(0.2f * (float)curveDirection);
		if (curveDirection == 100)
		{
			curveDirection = (Main.rand.NextBool() ? 1 : (-1));
		}
		if (preDamage)
		{
			if (targetDist < 1400f && time > 3f)
			{
				float scaler = Utils.GetLerpValue(-60f, 120f, time, clamped: true);
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/LargeBloom", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.6f * scaler, 0f, 4, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
				CustomPulse customPulse = new CustomPulse(base.Projectile.Center, Vector2.Zero, InnerColor * 0.6f, "CalamityMod/Particles/LargeBloom", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.48f * scaler, 0f, 4, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
				GeneralParticleHandler.SpawnParticle(customPulse);
				customPulse.DrawLayer = GeneralDrawLayer.AfterEverything;
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.96f;
			if (time == 3f)
			{
				for (int i = 0; i < 6; i++)
				{
					float variance = Main.rand.NextFloat(-0.4f, 0.4f);
					int dustStyle = ModContent.DustType<VoidDustInverted>();
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle);
					dust.scale = Main.rand.NextFloat(0.8f, 1.2f) - Math.Abs(variance);
					dust.velocity = base.Projectile.velocity.RotatedBy(variance) * Main.rand.NextFloat(1.2f, 1.5f) * (1f - Math.Abs(variance));
					dust.noGravity = true;
					dust.color = Color.LightGreen;
				}
			}
		}
		else if (moving)
		{
			float curveStrength = Utils.GetLerpValue(scalingTimerMax, 0f, scalingTimer, clamped: true) * 0.04f;
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(curveStrength * (float)curveDirection);
			scaling = Utils.GetLerpValue(0f, scalingTimerMax, scalingTimer, clamped: true);
			float sharpScaling = Utils.GetLerpValue((float)scalingTimerMax * 0.7f, scalingTimerMax, scalingTimer, clamped: true);
			if (targetDist < 1400f && base.Projectile.timeLeft % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/GlowSpark2", affectedByGravity: false, 12, 0.07f * scaling, Color.Black * 0.85f, new Vector2(1.7f - (1f - sharpScaling), 0.9f + (1f - sharpScaling) * 2f), useAddativeBlend: false));
				CustomSpark customSpark = new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 12, 0.035f * scaling, Color.LightGreen * 0.75f, new Vector2(1.7f - (1f - sharpScaling), 0.9f + (1f - sharpScaling) * 2f));
				GeneralParticleHandler.SpawnParticle(customSpark);
				customSpark.DrawLayer = GeneralDrawLayer.AfterEverything;
			}
			if (Main.rand.NextBool(6))
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? 278 : 267, -base.Projectile.velocity);
				dust2.scale = ((dust2.type == 278) ? Main.rand.NextFloat(0.3f, 0.6f) : Main.rand.NextFloat(0.6f, 1.2f));
				dust2.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.7f);
				dust2.noGravity = true;
				dust2.color = InnerColor;
			}
			scalingTimer--;
			if (scalingTimer <= 0)
			{
				if (curves > 1)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.Center += Main.rand.NextVector2Circular(100f, 100f);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/LargeBloom", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.6f, 0f, scalingTimerMax / 2, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
					for (int j = 0; j < 2; j++)
					{
						CustomPulse customPulse2 = new CustomPulse(base.Projectile.Center, Vector2.Zero, InnerColor, "CalamityMod/Particles/LargeBloom", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.36f, 0f, scalingTimerMax / 2, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
						GeneralParticleHandler.SpawnParticle(customPulse2);
						customPulse2.DrawLayer = GeneralDrawLayer.AfterEverything;
					}
					base.Projectile.velocity = Vector2.Zero;
				}
				else
				{
					base.Projectile.Kill();
				}
				moving = false;
			}
		}
		else
		{
			scalingTimer += 2;
			if (scalingTimer >= scalingTimerMax)
			{
				curves--;
				if (curves > 0)
				{
					for (int k = 0; k <= 6; k++)
					{
						int dustStyle2 = ModContent.DustType<VoidDustInverted>();
						Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 191 : dustStyle2, base.Projectile.velocity);
						dust3.scale = Main.rand.NextFloat(0.9f, 1.4f);
						dust3.velocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.2f, 1f);
						dust3.noGravity = true;
						dust3.color = (Color)((dust3.type == dustStyle2) ? InnerColor : default(Color));
					}
					base.Projectile.velocity = vel;
					curveDirection = ((curveDirection != 1) ? 1 : (-1));
					moving = true;
					base.Projectile.numHits = 0;
				}
				else
				{
					base.Projectile.Kill();
				}
			}
		}
		if (time == 120f)
		{
			for (int l = 0; l <= 12; l++)
			{
				int dustStyle3 = (Main.rand.NextBool() ? 66 : 263);
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center, dustStyle3, base.Projectile.velocity);
				dust4.scale = Main.rand.NextFloat(0.5f, 0.8f);
				dust4.velocity = Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.2f, 1f);
				dust4.noGravity = true;
				dust4.color = Color.LightGreen;
			}
			preDamage = false;
			moving = true;
			scalingTimer = scalingTimerMax;
			base.Projectile.velocity = vel;
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits == 0)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldShoot");
			style.Volume = 0.3f;
			style.Pitch = 0.9f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i < 6; i++)
			{
				float variance = Main.rand.NextFloat(-0.6f, 0.6f);
				int dustStyle = ModContent.DustType<VoidDustInverted>();
				Dust dust = Dust.NewDustPerfect(target.Center, dustStyle);
				dust.scale = Main.rand.NextFloat(1.2f, 1.6f) - Math.Abs(variance);
				dust.velocity = (base.Projectile.velocity * 1.5f).RotatedBy(variance) * Main.rand.NextFloat(1.2f, 1.5f) * (1f - Math.Abs(variance));
				dust.noGravity = true;
				dust.color = Color.LightGreen;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		damageMult = MathHelper.Clamp(Utils.GetLerpValue(5f, 1f, base.Projectile.numHits), 0.7f, 1f);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 8; i++)
		{
			int dustStyle = (Main.rand.NextBool() ? 66 : 263);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle, base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(0.5f, 0.8f);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.3f, 3.1f);
			dust.noGravity = true;
			dust.color = InnerColor;
		}
	}

	public override bool? CanDamage()
	{
		if (!preDamage)
		{
			return null;
		}
		return false;
	}

	public CosmicTentacle()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		preDamage = true;
		scaling = 1f;
		InnerColor = Color.LightGreen;
		curveDirection = 100;
		curves = 3;
		scalingTimerMax = 90;
		damageMult = 1f;
		base._002Ector();
	}
}
