using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class NorfleetComet : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 1200;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float rotationRando = Main.rand.NextFloat(-0.02f, 0.02f);
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(rotationRando);
		base.Projectile.rotation += 0.05f;
		if (base.Projectile.ai[2] == 1f)
		{
			base.Projectile.friendly = false;
			if (time >= 75)
			{
				base.Projectile.hostile = true;
				float distanceFromPlayer = base.Projectile.Distance(player.Center);
				Vector2 idealVelocity = (player.Center - base.Projectile.Center) / distanceFromPlayer * 8f;
				base.Projectile.velocity.X += (float)Math.Sign(idealVelocity.X - base.Projectile.velocity.X) * (0.0005f * (float)time);
				base.Projectile.velocity.Y += (float)Math.Sign(idealVelocity.Y - base.Projectile.velocity.Y) * (0.0005f * (float)time);
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
				{
					base.Projectile.Kill();
				}
				base.Projectile.timeLeft = 5;
			}
		}
		if (time > 25)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + ((float)i * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 30f, (base.Projectile.ai[2] == 1f) ? 219 : (Main.rand.NextBool(3) ? 272 : 86), ((float)i * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2() * 3f);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.6f, 0.9f);
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Main.player[base.Projectile.owner].SetScreenshake(7.5f);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ScorpioNukeHit");
		style.Volume = 0.75f;
		style.Pitch = 0.6f;
		style.PitchVariance = 0.2f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<NorfleetExplosion>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.ai[1], (base.Projectile.ai[2] == 1f) ? 1 : 0);
		if (base.Projectile.ai[1] == 0f)
		{
			Color partColor = Color.OrangeRed;
			int partLifetime = 10;
			float scale = 0.1f;
			Vector2 center = base.Projectile.Center;
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center, Vector2.Zero, partColor, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale, partLifetime, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center, Vector2.Zero, partColor * 0.4f, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale * 4f, partLifetime, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int k = 0; k < 15; k++)
			{
				Vector2 velocity = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velocity, velocity, affectedByGravity: false, 45, Main.rand.NextFloat(0.95f, 1.35f), partColor));
			}
			for (int i = 0; i < 30; i++)
			{
				Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + velocity2, 259, velocity2);
				dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust.noGravity = true;
			}
		}
		if (base.Projectile.ai[1] == 1f)
		{
			Color partColor2 = Color.Cyan;
			int partLifetime2 = 10;
			float scale2 = 0.1f;
			Vector2 center2 = base.Projectile.Center;
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center2, Vector2.Zero, partColor2, "CalamityMod/Particles/PlasmaExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale2, partLifetime2, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center2, Vector2.Zero, partColor2 * 0.4f, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale2 * 4f, partLifetime2, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int j = 0; j < 15; j++)
			{
				Vector2 velocity3 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center + velocity3, velocity3, Color.White, partColor2, Main.rand.NextFloat(0.45f, 0.65f), 45, Main.rand.NextFloat(-2f, 2f), 2f));
			}
			for (int l = 0; l < 30; l++)
			{
				Vector2 velocity4 = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + velocity4, 226, velocity4);
				dust2.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust2.noGravity = false;
			}
		}
		if (base.Projectile.ai[1] == 2f)
		{
			Color partColor3 = Color.GreenYellow;
			int partLifetime3 = 10;
			float scale3 = 0.1f;
			Vector2 center3 = base.Projectile.Center;
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center3, Vector2.Zero, partColor3, "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale3, partLifetime3, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center3, Vector2.Zero, partColor3 * 0.4f, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale3 * 4f, partLifetime3, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int m = 0; m < 15; m++)
			{
				Vector2 velocity5 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + velocity5, velocity5, partColor3, 45, Main.rand.NextFloat(0.9f, 2.3f), 0.7f));
			}
			for (int n = 0; n < 30; n++)
			{
				Vector2 velocity6 = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + velocity6, Main.rand.NextBool() ? 39 : 298, velocity6);
				dust3.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust3.noGravity = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		Texture2D vortexTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SoulVortex", (AssetRequestMode)2).Value;
		for (int i = 0; i < 4; i++)
		{
			float angle = (float)Math.PI * 2f * (float)i / 3f + Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f);
			Color drawColor = Color.Lerp(Color.Purple, Color.MediumOrchid, (float)i * 0.2f);
			((Color)(ref drawColor)).A = 0;
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
			Main.EntitySpriteDraw(vortexTexture, drawPosition, null, drawColor * base.Projectile.Opacity, 0f - angle + (float)Math.PI / 2f, vortexTexture.Size() * 0.5f, base.Projectile.scale * (1f - (float)i * 0.07f) * 0.18f * Utils.GetLerpValue(0f, 25f, time, clamped: true), (SpriteEffects)0);
		}
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float randSize = Main.rand.NextFloat(0.8f, 1.2f);
		Color drawColor2 = ((base.Projectile.ai[1] == 0f) ? Color.OrangeRed : ((base.Projectile.ai[1] == 1f) ? Color.Cyan : Color.GreenYellow));
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color color = drawColor2;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position, null, color, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.25f * Utils.GetLerpValue(0f, 25f, time, clamped: true) * randSize, (SpriteEffects)0);
		Vector2 position2 = base.Projectile.Center - Main.screenPosition;
		color = Color.White;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position2, null, color, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.1f * Utils.GetLerpValue(0f, 25f, time, clamped: true) * randSize, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 75f, targetHitbox);
	}
}
