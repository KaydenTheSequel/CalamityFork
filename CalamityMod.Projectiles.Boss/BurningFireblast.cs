using System;
using CalamityMod.Events;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.NPCs.CalClone;
using CalamityMod.Particles;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BurningFireblast : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle ImpactSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneFireblastImpact");

	public int DartDamage;

	public bool withinRange;

	public bool setLifetime;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 36;
		base.Projectile.height = 36;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.Opacity = 0f;
		base.Projectile.timeLeft = 150;
		base.Projectile.tileCollide = false;
		base.CooldownSlot = 1;
	}

	public override void OnSpawn(IEntitySource source)
	{
		DartDamage = CalamitasClone.DartDamage;
	}

	public override void AI()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 5)
		{
			base.Projectile.frame = 0;
		}
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		Lighting.AddLight(base.Projectile.Center, 0.9f * base.Projectile.Opacity, 0f, 0f);
		if (!withinRange)
		{
			if (base.Projectile.ai[2] == 1f)
			{
				base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / 60f, 0f, 1f);
			}
			else
			{
				base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 130) / 20f, 0f, 1f);
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
		int target = (int)base.Projectile.ai[0];
		if (!withinRange)
		{
			float inertia = (revenge ? 80f : 100f);
			float homeSpeed = (revenge ? 13f : 9f);
			float minDist = 40f;
			if (target >= 0 && Main.player[target].active && !Main.player[target].dead)
			{
				if (base.Projectile.Distance(Main.player[target].Center) > minDist)
				{
					Vector2 moveDirection = base.Projectile.SafeDirectionTo(Main.player[target].Center, Vector2.UnitY);
					base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + moveDirection * homeSpeed) / inertia;
				}
			}
			else if (base.Projectile.ai[0] != -1f)
			{
				base.Projectile.ai[0] = -1f;
				base.Projectile.netUpdate = true;
			}
		}
		float targetDist = ((target == -1 || Main.player[target].dead || !Main.player[target].active || Main.player[target] == null) ? 1000f : Vector2.Distance(Main.player[target].Center, base.Projectile.Center));
		if (base.Projectile.ai[1] == 2f && !withinRange && Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity + Main.rand.NextVector2Circular(20f, 20f), -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1f), affectedByGravity: false, 14, Main.rand.NextFloat(0.35f, 0.6f), (Main.rand.NextBool() ? Color.Lerp(Color.Red, Color.Magenta, 0.5f) : Color.Red) * base.Projectile.Opacity));
		}
		if ((base.Projectile.timeLeft == 1 && !withinRange) || (targetDist < 224f && base.Projectile.Opacity == 1f))
		{
			if (!setLifetime)
			{
				base.Projectile.timeLeft = 60;
				setLifetime = true;
			}
			withinRange = true;
		}
		if (withinRange && base.Projectile.ai[2] == 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 60 : 114);
				dust.noGravity = true;
				dust.velocity = Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.5f, 1.3f);
				dust.scale = Main.rand.NextFloat(0.7f, 1.8f);
			}
			if (base.Projectile.timeLeft <= 40 && base.Projectile.Opacity > 0f)
			{
				base.Projectile.Opacity -= 0.05f;
			}
			if (base.Projectile.timeLeft == 30)
			{
				base.Projectile.Opacity = 0f;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0f;
				for (int j = 0; j < 2; j++)
				{
					Particle bloom = new BloomParticle(base.Projectile.Center, Vector2.Zero, new Color(121, 56, 0), 0.1f, 0.7f, 30, fade: false);
					GeneralParticleHandler.SpawnParticle(bloom);
					if (base.Projectile.ai[2] == 1f)
					{
						bloom.Lifetime = 0;
					}
				}
			}
			if (base.Projectile.timeLeft == 15)
			{
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, new Color(255, 106, 0), 0.1f, 0.65f, 15, fade: false));
			}
			if (base.Projectile.timeLeft == 8)
			{
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.White, 0.1f, 0.5f, 8, fade: false));
			}
		}
		SeekersMetaball.Particle particle = SeekersMetaball.SpawnParticle(base.Projectile.Center + base.Projectile.velocity * (float)base.Projectile.MaxUpdates, Vector2.Zero, TextureAssets.Projectile[ModContent.ProjectileType<SCalBrimstoneFireblast>()].Width());
		particle.rotation = base.Projectile.rotation;
		particle.CurrentFrame = base.Projectile.frame;
		particle.MaxFrames = Main.projFrames[base.Type];
		particle.TextureToUse = TextureAssets.Projectile[ModContent.ProjectileType<SCalBrimstoneFireblast>()].Value;
		particle.SizeScaling = 0f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in ImpactSound, base.Projectile.Center);
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (base.Projectile.ai[2] != 0f)
		{
			return;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			int totalProjectiles = (death ? 16 : (revenge ? 14 : (expertMode ? 12 : 8)));
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<BurningBolt>();
			float velocity = 8f;
			Vector2 spinningPoint = default(Vector2);
			((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity);
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, type, DartDamage, 0f, base.Projectile.owner, 0f, base.Projectile.ai[1], velocity * 1.5f);
			}
		}
		if (base.Projectile.ai[1] == 2f)
		{
			for (int i = 0; i < 18; i++)
			{
				Vector2 velocity3 = Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0);
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center + velocity3, velocity3 * Main.rand.NextFloat(0.3f, 1f), affectedByGravity: false, 15, 1.1f, (Main.rand.NextBool() ? Color.Lerp(Color.Red, Color.Magenta, 0.5f) : Color.Red) * 0.6f));
			}
			for (int j = 0; j < 18; j++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 60 : 114);
				dust.noGravity = true;
				dust.velocity = Utils.RotatedByRandom(new Vector2(16f, 16f), 100.0) * Main.rand.NextFloat(0.5f, 1.3f);
				dust.scale = Main.rand.NextFloat(0.75f, 1.3f);
			}
		}
	}
}
