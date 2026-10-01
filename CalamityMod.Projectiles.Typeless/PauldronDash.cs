using System;
using System.Collections.Generic;
using CalamityMod.Buffs.StatDebuffs;
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
public class PauldronDash : ModProjectile, ILocalizedModType, IModType
{
	private static float ExplosionRadius = 75f;

	public Color effectsColor;

	public float fxFade;

	public bool isAlive;

	public Vector2 aimVel;

	private bool hasHit;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool visuals => Owner.Calamity().sPauldronVisual;

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
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
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
			fxFade = MathHelper.Lerp(fxFade, 0f, 0.08f);
		}
		float rate = Main.GlobalTimeWrappedHourly * 22f;
		List<Color> eColors = new List<Color>
		{
			Color.OrangeRed,
			Color.Orange,
			Color.DarkOrange
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		effectsColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (visuals)
		{
			int dir = MathF.Sign(aimVel.X);
			if (dir == 0)
			{
				dir = Owner.direction;
			}
			Vector2 safeVel = aimVel.SafeNormalize(Vector2.UnitX * (float)dir);
			float sparkscale2 = 0.35f * Math.Max(fxFade, 0.5f);
			for (int i = -1; i <= 1; i += 2)
			{
				Vector2 sparkVelocity = safeVel.RotatedBy((float)dir * 4.3f * (float)i) - safeVel * 3f;
				Vector2 sparkPlace = Owner.Center + (safeVel * 15f).RotatedBy(2f * (float)dir * (float)i) * 1.5f;
				GeneralParticleHandler.SpawnParticle(new VelChangingSpark(sparkPlace, sparkVelocity, -safeVel * 5f, "CalamityMod/Particles/BloomCircle", 10, sparkscale2, effectsColor * fxFade, new Vector2(0.8f, 2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.4f));
				if (isAlive)
				{
					int dustStyle = ModContent.DustType<SquashDust>();
					Dust dust = Dust.NewDustPerfect(sparkPlace + safeVel * 30f, dustStyle, sparkVelocity.RotatedBy(-0.6f * (float)i * (float)dir).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(3f, 5f));
					dust.scale = Main.rand.NextFloat(0.9f, 1.3f);
					dust.color = (Main.rand.NextBool() ? Color.Orange : Color.OrangeRed);
					dust.noGravity = true;
				}
			}
		}
		if (Owner.dead || (!isAlive && fxFade < 0.1f) || (Owner.velocity == Vector2.Zero && Owner.dashDelay != -1))
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(323, 240);
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 300);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HolyColliderProjectileHit");
		style.Volume = 0.4f;
		style.Pitch = 0.15f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockImpact");
		style.Volume = 0.6f;
		style.Pitch = 0.35f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i <= 12; i++)
		{
			float variance = Main.rand.NextFloat(-0.7f, 0.7f);
			int dustStyle = ModContent.DustType<SquashDustTileTouch>();
			Dust dust = Dust.NewDustPerfect(target.Center, dustStyle, base.Projectile.velocity);
			dust.scale = (Main.rand.NextFloat(1.2f, 1.4f) - Math.Abs(variance)) * 2.5f;
			dust.velocity = (Vector2.UnitY * -25f).RotatedBy(variance) * Main.rand.NextFloat(0.7f, 1f) * (1f - Math.Abs(variance) * 1.3f);
			dust.color = (Main.rand.NextBool() ? Color.Orange : Color.OrangeRed);
			dust.noGravity = false;
			dust.fadeIn = 0.35f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(17f, 17f), 100.0) * Main.rand.NextFloat(0.4f, 1f), affectedByGravity: true, 55, 0.85f, Main.rand.NextBool() ? Color.Orange : Color.OrangeRed));
		}
		if (visuals)
		{
			float baseRot = Main.rand.NextFloat(-9f, 9f);
			for (int j = 0; j < 5; j++)
			{
				float rot = Main.rand.NextFloat(-0.3f, 0.3f);
				for (int b = 0; b < 2; b++)
				{
					Vector2 pulseVel = Utils.RotatedBy(new Vector2(0f, Main.rand.NextFloat(-7f, -9f)), (double)((float)j * (MathHelper.ToRadians(360f) / 5f)), default(Vector2)).RotatedBy(rot + baseRot + 0.9f);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, pulseVel, (Main.rand.NextBool() ? Color.Orange : Color.OrangeRed) * 0.9f, "CalamityMod/Projectiles/Summon/RustyBeaconPulse", Vector2.One, pulseVel.ToRotation(), 0.2f, Main.rand.NextFloat(0.55f, 0.85f) * 3f, Main.rand.Next(14, 20), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, Utils.RotatedBy(new Vector2(0f, -7f), (double)((float)j * (MathHelper.ToRadians(360f) / 5f)), default(Vector2)).RotatedBy(rot + baseRot), "CalamityMod/Particles/BloomLineFade", affectedByGravity: false, 13, 0.095f, Main.rand.NextBool() ? Color.Orange : Color.OrangeRed, new Vector2(2.9f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.9f));
			}
			for (int k = 0; k < 2; k++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 20, 1.2f, Color.OrangeRed, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0f, 0.7f, 0.8f));
			}
		}
		Owner.SetScreenshake(4f);
		if (!hasHit)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PauldronExplosion>(), base.Projectile.damage / 5, 0f, base.Projectile.owner);
			hasHit = true;
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
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		if (effectsColor == Color.White || !visuals)
		{
			return false;
		}
		float sine = MathHelper.Lerp(Math.Abs((float)Math.Sin(Main.GlobalTimeWrappedHourly * 50f / (float)Math.PI)), 0.8f, 0.7f);
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearLarge", (AssetRequestMode)2).Value;
		for (int i = 0; i < 5; i++)
		{
			float bScale2 = 0.75f;
			Vector2 scale = new Vector2((1f - (float)i * 0.15f) * sine, 1f + (float)i * 0.22f + fxFade * 0.2f) * (bScale2 - (float)i * 0.08f) * fxFade * 0.23f;
			Vector2 position = Owner.Center - Main.screenPosition + aimVel.SafeNormalize(Vector2.UnitX) * -15f;
			Color val = Color.Lerp(effectsColor, Color.White, (float)i * 0.15f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(bTexture, position, null, val * fxFade, aimVel.ToRotation() + (float)Math.PI / 2f, bTexture.Size() * 0.5f, scale, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public PauldronDash()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		effectsColor = Color.White;
		isAlive = true;
		base._002Ector();
	}
}
