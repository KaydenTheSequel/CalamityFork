using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class HolyColliderHolyFire : ModProjectile, ILocalizedModType, IModType
{
	public bool isLaunched;

	public bool setStats = true;

	public static int statMax = 8;

	public int setStatTimer = statMax;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Boss/HolyFire2";

	public ref float time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10 * base.Projectile.MaxUpdates;
	}

	public override bool? CanDamage()
	{
		if ((!(time > 10f) || isLaunched) && (!isLaunched || setStats))
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		int hitTime = 13;
		if (base.Projectile.ai[2] == 5f && time >= (float)hitTime && base.Projectile.ai[0] != 10f)
		{
			isLaunched = true;
		}
		else
		{
			base.Projectile.ai[2] = 0f;
		}
		base.Projectile.scale = Utils.GetLerpValue(0f, 40f, base.Projectile.timeLeft, clamped: true);
		if (isLaunched)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			if (setStats)
			{
				if (setStatTimer == statMax)
				{
					base.Projectile.timeLeft = 300;
					for (int i = 0; i < 2; i++)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashCoreImpact");
						style.Volume = 0.55f;
						style.Pitch = 0.6f;
						style.MaxInstances = 2;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					float starAngle = Main.rand.NextFloat(-0.9f, 0.9f);
					for (int j = 0; j < 4; j++)
					{
						Dust.NewDustPerfect(base.Projectile.Center, 278);
						Vector2 vel = ((float)Math.PI * 2f * (float)j / 4f).ToRotationVector2().RotatedBy(starAngle) * 8f;
						GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, vel, affectedByGravity: false, 10, 0.08f, Color.Orange, new Vector2(3.2f, 0.9f), quickShrink: true, glow: true, 0.9f));
					}
				}
				if (setStatTimer == 0)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastShoot");
					style.Volume = 1f;
					style.Pitch = 0.4f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					base.Projectile.extraUpdates = 5;
					Vector2 vel2 = (base.Projectile.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * -12f;
					base.Projectile.velocity = vel2;
					base.Projectile.penetrate = 1;
					base.Projectile.damage *= 15;
					time = 0f;
					setStats = false;
				}
				else
				{
					setStatTimer -= 2;
				}
			}
			else
			{
				if (Main.rand.NextBool())
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(80f, 80f), ModContent.DustType<LightDust>(), base.Projectile.velocity * 2f * Main.rand.NextFloat(0.1f, 1f));
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(1.85f, 2.45f);
					dust.color = (Main.rand.NextBool() ? Color.OrangeRed : Color.Goldenrod);
					dust.noLightEmittence = true;
				}
				if (Main.rand.NextBool())
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(80f, 80f), -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1f), affectedByGravity: false, 11, 0.9f, Main.rand.NextBool() ? Color.Goldenrod : Color.Orange));
				}
				else
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(80f, 80f), -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 27, Main.rand.NextFloat(1.15f, 1.3f), Main.rand.NextBool(4) ? Color.Khaki : Color.Orange, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.2f)));
				}
			}
		}
		else
		{
			if (Main.rand.NextBool(5))
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Utils.RotatedByRandom(new Vector2(0f, -7f), 0.2) * Main.rand.NextFloat(0.2f, 1f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.85f, 1.45f) * base.Projectile.scale;
				dust2.color = Color.Goldenrod;
				dust2.noLightEmittence = true;
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 8f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.88f;
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.965f;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		time++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, isLaunched ? 100 : 20, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (isLaunched && base.Projectile.scale > 0.2f)
		{
			Owner.SetScreenshake(9f);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HolyColliderProjectileHit");
			style.Volume = 1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int g = 0; g < 3; g++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 2.8f * (float)(g + 1), 1.7f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 2.2f * (float)(g + 1), 1.3f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.OrangeRed, Color.Orange, (float)i * 0.2f), (i == 4) ? "CalamityMod/Particles/ShatteredExplosion" : "CalamityMod/Particles/FlameExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.11f + (float)i * 0.05f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int j = 0; j < 25; j++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(21f, 21f), 100.0) * Main.rand.NextFloat(0.4f, 1f), affectedByGravity: true, 55, 0.85f, Main.rand.NextBool() ? Color.Goldenrod : Color.OrangeRed));
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BurningHolyBlast>(), (int)((double)base.Projectile.damage * 0.47), base.Projectile.knockBack, base.Projectile.owner, 1.8f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D smallTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/HolyColliderHolyFire", (AssetRequestMode)2).Value;
		Texture2D bigTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/HolyColliderHolyFire2", (AssetRequestMode)2).Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		float power = ((!setStats) ? 2.4f : 0.6f);
		float randSize = Main.rand.NextFloat(0.8f, 1.1f);
		Color val = Color.Goldenrod;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bloomTexture, drawPos, null, val, base.Projectile.rotation, bloomTexture.Size() * 0.5f, 0.65f * randSize * power * base.Projectile.scale, (SpriteEffects)0);
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bloomTexture, drawPos, null, val * 0.65f, base.Projectile.rotation, bloomTexture.Size() * 0.5f, 0.45f * randSize * power * base.Projectile.scale, (SpriteEffects)0);
		Texture2D usedTex = ((!setStats) ? bigTexture : smallTexture);
		Rectangle frame = usedTex.Frame(1, 4, 0, base.Projectile.frame);
		Vector2 rotationPoint = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation;
		if (setStatTimer == statMax || !setStats)
		{
			Main.EntitySpriteDraw(usedTex, drawPosition, frame, Color.White, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}
}
