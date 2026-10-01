using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class FireImplosion : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 pushVelocity;

	public float customKnockback;

	public int time;

	public int boomTime;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool frigidFlash => base.Projectile.ai[2] == 5f;

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 55;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.ArmorPenetration = 20;
	}

	public override void AI()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		if (customKnockback == 0f)
		{
			base.Projectile.scale = 0f;
			base.Projectile.alpha = 0;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/OpalChargedFire");
			style.Volume = 0.35f;
			style.Pitch = 0.7f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/FireImplosion");
			style.Volume = 0.45f;
			style.Pitch = Main.rand.NextFloat(0.2f, 0.35f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			customKnockback = Math.Abs(base.Projectile.knockBack);
			base.Projectile.knockBack = 0f;
			for (int i = 0; i < 3; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 1.1f + (float)i * 0.1f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			float rot = Main.rand.NextFloat(-2f, 2f);
			for (int j = 0; j < 8; j++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)j / 8f).ToRotationVector2().RotatedBy(rot) * 13f;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + velocity * 3f, velocity * 0.5f, "CalamityMod/Particles/FireTypeParticle", affectedByGravity: false, 35, 1.5f, Color.OrangeRed, new Vector2(1f, 1.3f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.15f));
			}
		}
		if (boomTime >= 15)
		{
			base.Projectile.scale = 0.95f * Utils.GetLerpValue(42f, 0f, time, clamped: true) * Utils.GetLerpValue(40f, 25f, time, clamped: true);
			time++;
		}
		else
		{
			base.Projectile.scale = 0.95f * Utils.GetLerpValue(0f, 15f, boomTime, clamped: true);
			boomTime++;
		}
		Color newColor;
		if (base.Projectile.timeLeft <= 20)
		{
			if ((frigidFlash && base.Projectile.timeLeft % 3 == 0) || !frigidFlash)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(0.3f, 6f), "CalamityMod/Particles/FireTypeParticle", affectedByGravity: false, 22, Main.rand.NextFloat(0.7f, 1.2f), Color.OrangeRed, new Vector2(0.8f, 1f)));
			}
			if (frigidFlash)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(0.3f, 6f), "CalamityMod/Particles/IceTypeParticle", affectedByGravity: false, 22, Main.rand.NextFloat(0.7f, 1.2f), Color.DeepSkyBlue, new Vector2(0.8f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.1f));
			}
		}
		else if (boomTime > 5)
		{
			for (int k = 0; k < 2; k++)
			{
				Vector2 vel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(7f, 14f) * base.Projectile.scale;
				Vector2 position = base.Projectile.Center + vel * 3f;
				int type = ModContent.DustType<LightDust>();
				Vector2? velocity2 = vel;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, type, velocity2, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.75f, 1.1f);
				dust.color = Color.OrangeRed;
				dust.noLightEmittence = true;
			}
		}
		Vector2 center = base.Projectile.Center;
		newColor = Color.Orange;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 2f * base.Projectile.scale);
		base.Projectile.rotation += 0.4f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (frigidFlash)
		{
			target.AddBuff(323, 30);
			target.AddBuff(324, 30);
		}
		else
		{
			target.AddBuff(24, 180);
		}
		pushVelocity = base.Projectile.Center.DirectionTo(target.Center) * customKnockback;
		float minMult = 0.4f;
		int hitsToMinMult = 15;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult * (frigidFlash ? 0.5f : 1f);
		target.MoveNPC(-pushVelocity, customKnockback, frigidFlash);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		if (frigidFlash)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianDash");
			style.Volume = 1f;
			style.Pitch = -0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = SoundID.DeerclopsIceAttack with
			{
				Volume = 0.85f,
				Pitch = 0.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			float blastSize = 160f;
			float minMultiplier = 0.25f;
			int hitsToMinMult = 8;
			int debuff1 = 324;
			int debuff2 = 323;
			int debuffTime = 230;
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)base.Projectile.damage * 4f), customKnockback * 2f, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile.localAI[0] = debuff1;
			projectile.localAI[2] = debuff2;
			projectile.localAI[1] = debuffTime;
			projectile.DamageType = DamageClass.Magic;
			float rot = Main.rand.NextFloat(-2f, 2f);
			for (int i = 0; i < 8; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 8f).ToRotationVector2().RotatedBy(rot) * 14f;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + velocity * 3f, velocity * 0.5f, "CalamityMod/Particles/IceTypeParticle", affectedByGravity: false, 32, 1.3f, Color.Lerp(Color.DeepSkyBlue, Color.White, 0.5f), new Vector2(1.2f, 1.8f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.27f));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), velocity.RotatedBy(MathHelper.ToRadians(22.5f)) * 1.2f);
				dust.noGravity = true;
				dust.scale = 1.75f;
				dust.color = Color.OrangeRed;
				dust.noLightEmittence = true;
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), velocity * 1.5f);
				dust2.noGravity = true;
				dust2.scale = 1.4f;
				dust2.color = Color.DeepSkyBlue;
				dust2.noLightEmittence = true;
			}
			for (int j = 0; j < 5; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.3f, 1.1f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.DeepSkyBlue, Color.White, (float)j * 0.15f), "CalamityMod/Particles/BloomCircle", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.6f, 0.8f - (float)j * 0.07f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 110f * base.Projectile.scale + 15f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		if (boomTime < 1)
		{
			return false;
		}
		float fade = Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true);
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRing", (AssetRequestMode)2).Value;
		Color useColor = (frigidFlash ? Color.Lerp(Color.DeepSkyBlue, Color.OrangeRed, fade) : Color.OrangeRed);
		for (int i = 0; i < 15; i++)
		{
			Color val = useColor;
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.4f;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 15f).ToRotationVector2().RotatedBy(Main.GlobalTimeWrappedHourly * 28f - fade * 10f) * 7f * fade;
			Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + drawOffset, null, auraColor, base.Projectile.rotation + (float)i * 2.5f, texture.Size() * 0.5f, new Vector2(1f * fade, 1f + (1f - fade)) * MathHelper.Clamp(base.Projectile.scale - (float)i * 0.02f, 0f, 10f), (SpriteEffects)0);
		}
		return false;
	}
}
