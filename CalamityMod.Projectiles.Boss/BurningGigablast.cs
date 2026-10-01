using System;
using CalamityMod.Events;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.SupremeCalamitas;
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

public class BurningGigablast : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle ImpactSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneGigablastImpact");

	public int DartDamage;

	public bool withinRange;

	public bool setLifetime;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.Opacity = 0f;
		base.Projectile.tileCollide = false;
		base.CooldownSlot = 1;
	}

	public override void OnSpawn(IEntitySource source)
	{
		DartDamage = SupremeCalamitas.DartDamage;
		if (source is EntitySource_Parent { Entity: NPC parent } && parent.type == ModContent.NPCType<CalamitasClone>())
		{
			DartDamage = CalamitasClone.DartDamage;
		}
	}

	public override void AI()
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
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
		Lighting.AddLight(base.Projectile.Center, 0.9f * base.Projectile.Opacity, 0f, 0f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
		int target = Player.FindClosest(base.Projectile.Center, 1, 1);
		if (!withinRange)
		{
			float projSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 playerVec = Main.player[target].Center - base.Projectile.Center;
			((Vector2)(ref playerVec)).Normalize();
			playerVec *= projSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 24f + playerVec) / 25f;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= projSpeed;
		}
		float targetDist = ((target == -1 || Main.player[target].dead || !Main.player[target].active || Main.player[target] == null) ? 1000f : Vector2.Distance(Main.player[target].Center, base.Projectile.Center));
		if (base.Projectile.ai[1] == 2f && !withinRange && Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity + Main.rand.NextVector2Circular(30f, 30f), -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1f), affectedByGravity: false, 14, Main.rand.NextFloat(0.5f, 0.75f), (Main.rand.NextBool() ? Color.Lerp(Color.Red, Color.Magenta, 0.5f) : Color.Red) * base.Projectile.Opacity));
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
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.9f;
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
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0f;
				for (int j = 0; j < 2; j++)
				{
					Particle bloom = new BloomParticle(base.Projectile.Center, Vector2.Zero, new Color(121, 21, 77), 0.1f, 0.85f, 30, fade: false);
					GeneralParticleHandler.SpawnParticle(bloom);
					if (base.Projectile.ai[2] == 1f)
					{
						bloom.Lifetime = 0;
					}
				}
			}
			if (base.Projectile.timeLeft == 15)
			{
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.Red, 0.1f, 0.8f, 15, fade: false));
			}
			if (base.Projectile.timeLeft == 8)
			{
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.White, 0.1f, 0.7f, 8, fade: false));
			}
		}
		SeekersMetaball.Particle particle = SeekersMetaball.SpawnParticle(base.Projectile.Center + base.Projectile.velocity * (float)base.Projectile.MaxUpdates, Vector2.Zero, TextureAssets.Projectile[ModContent.ProjectileType<SCalBrimstoneGigablast>()].Width());
		particle.rotation = base.Projectile.rotation;
		particle.CurrentFrame = base.Projectile.frame;
		particle.MaxFrames = Main.projFrames[base.Type];
		particle.TextureToUse = TextureAssets.Projectile[ModContent.ProjectileType<SCalBrimstoneGigablast>()].Value;
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
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
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
			int totalProjectiles = (death ? 36 : (revenge ? 32 : (expertMode ? 28 : 20)));
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<BurningBolt>();
			float velocity = 6.5f;
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
			for (int i = 0; i < 25; i++)
			{
				Vector2 velocity3 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0);
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center + velocity3, velocity3 * Main.rand.NextFloat(0.3f, 1f), affectedByGravity: false, 15, 1.25f, (Main.rand.NextBool() ? Color.Lerp(Color.Red, Color.Magenta, 0.5f) : Color.Red) * 0.6f));
			}
			for (int j = 0; j < 25; j++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 60 : 114);
				dust.noGravity = true;
				dust.velocity = Utils.RotatedByRandom(new Vector2(20f, 20f), 100.0) * Main.rand.NextFloat(0.5f, 1.3f);
				dust.scale = Main.rand.NextFloat(0.9f, 1.8f);
			}
		}
	}
}
