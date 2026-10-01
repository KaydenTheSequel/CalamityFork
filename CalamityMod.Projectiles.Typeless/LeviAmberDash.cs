using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class LeviAmberDash : ModProjectile, ILocalizedModType, IModType
{
	public Color effectsColor;

	public float fxFade;

	public bool isAlive;

	public bool onSpawn;

	public Vector2 aimVel;

	private static float ExplosionRadius = 85f;

	public static readonly SoundStyle Slap = new SoundStyle("CalamityMod/Sounds/Custom/WetSlap", 4)
	{
		Volume = 0.8f,
		PitchVariance = 0.3f
	};

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool visuals => Owner.Calamity().lAmbergrisVisual;

	public override void SetDefaults()
	{
		base.Projectile.width = (int)ExplosionRadius;
		base.Projectile.height = (int)ExplosionRadius;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 15;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 18;
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		if (onSpawn && visuals)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center, Owner.velocity.SafeNormalize(Vector2.UnitX) * 9f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 23, 0.095f, Color.MediumTurquoise, new Vector2(1f, 1.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center, Owner.velocity.SafeNormalize(Vector2.UnitX) * 12f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 20, 0.065f, Color.DeepSkyBlue, new Vector2(1f, 1.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.23f));
			onSpawn = false;
		}
		if (Owner.dashDelay != -1)
		{
			isAlive = false;
		}
		if (isAlive)
		{
			base.Projectile.timeLeft++;
		}
		base.Projectile.Center = Owner.Center;
		if (effectsColor == Color.White)
		{
			aimVel = Owner.velocity;
		}
		else
		{
			aimVel = Vector2.Lerp(aimVel, Owner.velocity, 0.2f);
		}
		if (isAlive)
		{
			float goalSize = Utils.GetLerpValue(5f, 15f, Math.Abs(aimVel.X), clamped: true);
			if (goalSize < fxFade)
			{
				fxFade = MathHelper.Lerp(fxFade, goalSize, 0.03f);
			}
			else
			{
				fxFade = goalSize;
			}
		}
		else
		{
			fxFade = MathHelper.Lerp(fxFade, 0f, 0.13f);
		}
		float rate = Main.GlobalTimeWrappedHourly * 22f;
		List<Color> eColors = new List<Color>
		{
			Color.DarkTurquoise,
			Color.DeepSkyBlue,
			Color.MediumTurquoise
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		effectsColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (visuals)
		{
			int dir = MathF.Sign(aimVel.X);
			Vector2 safeVel = aimVel.SafeNormalize(Vector2.UnitX);
			float sparkscale2 = 0.35f * Math.Max(fxFade, 0.5f);
			for (int i = -1; i <= 1; i += 2)
			{
				Vector2 sparkVelocity = safeVel.RotatedBy((float)dir * 1.5f * (float)i) - safeVel * 3f;
				Vector2 sparkPlace = Owner.Center + ((safeVel * 15f).RotatedBy(2f * (float)dir * (float)i) * 1.5f + safeVel.RotatedBy(0.5f * (float)i * (float)dir) * 30f) * fxFade;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(sparkPlace, sparkVelocity.RotatedByRandom(0.10000000149011612), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 6, sparkscale2, effectsColor * fxFade, new Vector2(0.8f, 2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 1.1f));
				if (isAlive)
				{
					int dustStyle = ModContent.DustType<SquashDustHollow>();
					Dust dust = Dust.NewDustPerfect(sparkPlace, dustStyle, sparkVelocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(3f, 5f));
					dust.scale = Main.rand.NextFloat(0.9f, 1.3f);
					dust.color = effectsColor;
					dust.noGravity = true;
					dust.fadeIn = Main.rand.NextFloat(0f, 0.7f);
				}
			}
		}
		if (Owner.dead || (!isAlive && fxFade < 0.1f) || Owner.velocity == Vector2.Zero)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WaterSplash", 2);
		style.Volume = 0.2f;
		style.Pitch = Main.rand.NextFloat(0.35f, 0.5f);
		style.MaxInstances = -1;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		target.AddBuff(103, 300);
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 180);
		Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		target.MoveNPC(launchVel, 20f, ignoreKBImmune: true);
		for (int i = 0; i <= 17; i++)
		{
			float variance = Main.rand.NextFloat(-0.4f, 0.4f);
			int dustStyle = ModContent.DustType<SquashDust>();
			Dust dust = Dust.NewDustPerfect(target.Center, dustStyle);
			dust.scale = (Main.rand.NextFloat(1.2f, 1.4f) - Math.Abs(variance)) * 1.5f;
			dust.velocity = (aimVel.SafeNormalize(Vector2.UnitX).RotatedBy(variance) - Vector2.UnitY * 0.3f) * Main.rand.NextFloat(15f, 35f) * (1f - Math.Abs(variance) * 1.3f);
			dust.color = (Main.rand.NextBool() ? Color.MediumTurquoise : Color.DeepSkyBlue);
			dust.noGravity = false;
			dust.fadeIn = Main.rand.NextFloat(0f, 1.2f);
		}
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.67f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, ExplosionRadius, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (!isAlive)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		if (effectsColor == Color.White || !visuals)
		{
			return false;
		}
		float sine = MathHelper.Lerp(Math.Abs((float)Math.Sin(Main.GlobalTimeWrappedHourly * 50f / (float)Math.PI)), 0.8f, 0.7f);
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearRagged", (AssetRequestMode)2).Value;
		for (int i = 0; i < 10; i++)
		{
			float bScale2 = 0.75f;
			Vector2 scale = new Vector2((1f - (float)i * 0.13f) * sine, 1f + (float)i * 0.012f + fxFade * 0.2f) * (bScale2 + (float)i * 0.08f) * fxFade * 0.27f;
			Vector2 position = Owner.Center - Main.screenPosition + aimVel.SafeNormalize(Vector2.UnitX) * -15f;
			Color val = Color.Lerp(Color.Aquamarine, effectsColor, (float)i * 0.15f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(bTexture, position, null, val * fxFade * 1f, aimVel.ToRotation() + (float)Math.PI / 2f, bTexture.Size() * 0.5f, scale, (SpriteEffects)(i % 2 == 0));
		}
		return false;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public LeviAmberDash()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		effectsColor = Color.White;
		isAlive = true;
		onSpawn = true;
		base._002Ector();
	}
}
