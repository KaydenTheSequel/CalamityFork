using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class IonBlast : ModProjectile, ILocalizedModType, IModType
{
	public bool onSpawn = true;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float time => ref base.Projectile.ai[0];

	public bool chargeShot => base.Projectile.ai[2] == 5f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 7;
		base.Projectile.ignoreWater = false;
		base.Projectile.timeLeft = 280;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 40;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (chargeShot)
		{
			if (onSpawn)
			{
				base.Projectile.penetrate = -1;
				base.Projectile.extraUpdates = 6;
				base.Projectile.timeLeft = 190;
				base.Projectile.tileCollide = false;
				onSpawn = false;
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.975f;
			if (base.Projectile.timeLeft > 45 && time > 3f)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.01f, affectedByGravity: false, 15, 1f, Color.Crimson * 0.6f));
			}
		}
		else
		{
			if (base.Projectile.ai[1] != 0f && time > 18f)
			{
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.003f * (0f - base.Projectile.ai[1]));
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 17f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.005f;
			}
			Vector2 vel = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(0.5f, 4.5f);
			if (time % 3f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f), vel, "CalamityMod/Particles/GlowSquareParticle", affectedByGravity: false, Main.rand.Next(7, 12), Main.rand.NextFloat(0.5f, 0.8f), Main.rand.NextBool() ? Color.Lerp(Color.Crimson, Color.White, 0.5f) : Color.Crimson, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, (float)Math.PI / 4f));
			}
		}
		Vector2 center = base.Projectile.Center;
		Color crimson = Color.Crimson;
		Lighting.AddLight(center, ((Color)(ref crimson)).ToVector3() * 0.5f);
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.25f;
		int hitsToMinMult = 8;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		if (!chargeShot)
		{
			SoundStyle style = SoundID.Item92 with
			{
				Volume = 0.4f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Crimson, "CalamityMod/Particles/BloomRingThinLarge", Vector2.One, 0f, 0.02f, 0.053f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Crimson, Color.White, 0.5f), "CalamityMod/Particles/BloomRingThinLarge", Vector2.One, 0f, 0.02f, 0.06f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int i = 0; i < 15; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(Math.PI) * Main.rand.NextFloat(3f, 7f));
				dust.scale = Main.rand.NextFloat(0.85f, 1f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.Lerp(Color.Crimson, Color.White, 0.5f) : Color.Crimson);
			}
			float blastSize = 60f;
			float minMultiplier = 0.3f;
			int hitsToMinMult = 7;
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)base.Projectile.damage * 0.5f), 0f, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile.timeLeft = 10;
			projectile.DamageType = DamageClass.Magic;
			projectile.ArmorPenetration = 40;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (time <= 1f)
		{
			float _ = float.NaN;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, Owner.Center, base.Projectile.width, ref _);
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation;
		float timeleftFade = Utils.GetLerpValue(0f, 60f, base.Projectile.timeLeft, clamped: true);
		Color val;
		if (chargeShot)
		{
			for (int i = 0; i < 5; i++)
			{
				float iMult = 1f - 0.1f * (float)i;
				val = Color.Lerp(Color.Crimson, Color.White, (float)i * 0.1f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(tex2, drawPosition, null, val * iMult * timeleftFade, drawRotation, tex2.Size() * 0.5f, new Vector2(0.2f + 2f * iMult, (1.2f - 1f * iMult) * timeleftFade) * iMult * 0.03f, (SpriteEffects)0);
			}
		}
		else
		{
			for (int j = 0; j < 5; j++)
			{
				val = Color.Lerp(Color.Crimson, Color.White, (float)j * 0.1f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(tex, drawPosition, null, val * (1f - 0.1f * (float)j), drawRotation, tex.Size() * 0.5f, 1f - 0.1f * (float)j, (SpriteEffects)0);
			}
		}
		return false;
	}
}
