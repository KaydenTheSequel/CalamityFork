using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AtaraxiaMain : ModProjectile, ILocalizedModType, IModType
{
	private static int NumAnimationFrames = 5;

	private static int AnimationFrameTime = 9;

	public int time;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = NumAnimationFrames;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		base.DrawOffsetX = -40;
		base.DrawOriginOffsetY = -3;
		base.DrawOriginOffsetX = 18f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Lighting.AddLight(base.Projectile.Center, 0.45f, 0.1f, 0.1f);
		if ((float)time > 8f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 1.5f, -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 1.5f), affectedByGravity: false, 8, 0.8f, Color.Lerp(Color.DarkOrchid, Color.IndianRed, Main.rand.NextFloat(0f, 1f)) * 0.7f));
		}
		if ((float)time > 8f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + base.Projectile.velocity * 2f, base.Projectile.velocity, affectedByGravity: false, 2, 1.9f, Color.Lerp(Color.DarkOrchid, Color.IndianRed, Main.rand.NextFloat(0f, 1f)) * 0.85f));
		}
		if (time > 8 && Main.rand.NextBool())
		{
			Vector2 dustvel = -base.Projectile.velocity;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267, dustvel * Main.rand.NextFloat(0.1f, 1.2f), 0, default(Color), Main.rand.NextFloat(0.7f, 0.9f));
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.DarkOrchid, Color.IndianRed, Main.rand.NextFloat(0f, 1f));
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > AnimationFrameTime)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= NumAnimationFrames)
		{
			base.Projectile.frame = 0;
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(153, 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath55, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AtaraxiaBoom>(), base.Projectile.damage / 2, 0f, base.Projectile.owner, 1f);
		}
		for (int k = 0; k < 10; k++)
		{
			Vector2 velocity = Utils.RotatedByRandom(new Vector2(20f, 20f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
			float colorRando = Main.rand.NextFloat(0f, 1f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + velocity, 278, velocity * Main.rand.NextFloat(0.2f, 1f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.3f, 0.65f);
			dust.color = Color.Lerp(Color.DarkOrchid, Color.IndianRed, colorRando);
			dust.noLight = true;
			dust.noLightEmittence = true;
		}
		for (int i = 0; i < 10; i++)
		{
			Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
			float colorRando2 = Main.rand.NextFloat(0f, 1f);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + velocity2, velocity2, affectedByGravity: false, 11, Main.rand.NextFloat(0.015f, 0.025f), Color.Lerp(Color.DarkOrchid, Color.IndianRed, colorRando2), new Vector2(2.2f, 0.9f), quickShrink: true));
		}
		for (float k2 = 0f; k2 < 3f; k2++)
		{
			float colorRando3 = Main.rand.NextFloat(0f, 1f);
			int partLifetime = Main.rand.Next(13, 16);
			float scale = Main.rand.NextFloat(0.12f, 0.18f);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f) * (k2 + 1f), Vector2.Zero, Color.Lerp(Color.DarkOrchid, Color.IndianRed, colorRando3) * 0.6f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale, partLifetime, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 30f, targetHitbox);
	}
}
