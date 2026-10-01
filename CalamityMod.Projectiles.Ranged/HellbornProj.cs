using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HellbornProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.frame == 0 || base.Projectile.frame == 3)
		{
			base.Projectile.frameCounter++;
		}
		else
		{
			base.Projectile.frameCounter += 2;
		}
		if (base.Projectile.frameCounter > 20)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.035f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Orange;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (num < 1400f)
		{
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 16f;
			if (Main.rand.NextBool(2))
			{
				Vector2 position = base.Projectile.Center + offset;
				int type = ModContent.DustType<SquashDust>();
				Vector2? velocity = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(3f, 5f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(2.3f, 2.7f);
				dust.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
				dust.fadeIn = 2.5f;
			}
			if (Main.rand.NextBool(2))
			{
				Vector2 position2 = base.Projectile.Center - offset;
				int type2 = ModContent.DustType<SquashDust>();
				Vector2? velocity2 = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(3f, 5f);
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(position2, type2, velocity2, 0, newColor);
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(2.3f, 2.7f);
				dust2.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
				dust2.fadeIn = 2.5f;
			}
		}
		Vector2 position3 = base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f);
		int type3 = ModContent.DustType<DiamondDust>();
		Vector2? velocity3 = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.4f);
		newColor = default(Color);
		Dust dust3 = Dust.NewDustPerfect(position3, type3, velocity3, 0, newColor);
		dust3.noGravity = true;
		dust3.scale = Main.rand.NextFloat(0.62f, 0.82f);
		dust3.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
		dust3.fadeIn = 1f;
		dust3.noLight = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		Vector2 launchVel = base.Projectile.Center.DirectionTo(target.Center);
		target.MoveNPC(launchVel, 6f, ignoreKBImmune: true);
		MakeBlast(target.whoAmI, hitTarget: true);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		for (int l = 0; l < 17; l++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.1f, 0.8f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.62f, 0.82f);
			dust.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.1f, 0.8f));
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(2.1f, 2.4f);
			dust2.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
			dust2.fadeIn = 1.7f;
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed * 0.7f, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1.2f, 1.7f, 16, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.7f, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.7f, 0.9f, 16, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HellbornImpact");
		style.PitchVariance = 0.15f;
		style.Volume = 0.8f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (base.Projectile.numHits == 0)
		{
			MakeBlast(0, hitTarget: false);
		}
	}

	public void MakeBlast(int target, bool hitTarget)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		float blastSize = 70f;
		float minMultiplier = 0.25f;
		int hitsToMinMult = 5;
		int blastDamage = (int)((float)base.Projectile.damage * 0.33f);
		if (hitTarget)
		{
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurstExclusive>(), blastDamage, base.Projectile.knockBack, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile.timeLeft = 2;
			projectile.DamageType = DamageClass.Ranged;
			projectile.localAI[0] = target;
		}
		else
		{
			Projectile projectile2 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), blastDamage, base.Projectile.knockBack, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile2.timeLeft = 2;
			projectile2.DamageType = DamageClass.Ranged;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/HellbornProj", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		_ = base.Projectile.rotation;
		_ = base.Projectile.spriteDirection;
		_ = -1;
		_ = texture.Size() * 0.5f;
		Rectangle frame = texture.Frame(1, 6, 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float randSize = Main.rand.NextFloat(0.8f, 1.2f);
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color val = Color.OrangeRed;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position, null, val, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.5f * randSize, (SpriteEffects)0);
		Vector2 position2 = base.Projectile.Center - Main.screenPosition;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position2, null, val * 0.75f, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.35f * randSize, (SpriteEffects)0);
		for (int i = 0; i < 1; i++)
		{
			Main.EntitySpriteDraw(texture, drawPosition, frame, Color.White, 0f, origin, base.Projectile.scale * ((i == 0) ? 1.15f : 1f), (SpriteEffects)0);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 35f, targetHitbox);
	}
}
