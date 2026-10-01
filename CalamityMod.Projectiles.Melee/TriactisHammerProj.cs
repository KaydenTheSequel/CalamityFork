using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TriactisHammerProj : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/PwnagehammerSound")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle HitSoundGFB = new SoundStyle("CalamityMod/Sounds/Item/CalamityBell");

	public static readonly SoundStyle WindUpSound = new SoundStyle("CalamityMod/Sounds/Item/GalaxySmasherClone")
	{
		Volume = 0.9f
	};

	public static readonly SoundStyle SmashSound = new SoundStyle("CalamityMod/Sounds/Item/GalaxySmasherSmash")
	{
		Volume = 0.7f
	};

	public static readonly SoundStyle SmashSoundGFB = new SoundStyle("CalamityMod/Sounds/Item/TF2PanHit");

	public static float ExplosionDamageKBMult = 2f;

	public static float SuperHammerDamageMult = 3f;

	public static float SmashHomingRange = 800f;

	public static float WindUpTime = 216f;

	public static float ConvergeTime = 18f;

	public float OrbitRadius;

	public static Asset<Texture2D> EchoHammer;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/TriactisTruePaladinianMageHammerofMight";

	public ref float AirTime => ref base.Projectile.ai[0];

	public ref float HammerState => ref base.Projectile.ai[1];

	public ref float SmashTarget => ref base.Projectile.ai[2];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void Load()
	{
		EchoHammer = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TriactisHammerEcho", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 168);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		Rectangle hitbox;
		if (HammerState > 0f)
		{
			if (HammerState == 4f)
			{
				NPC target = Main.npc[(int)SmashTarget];
				if (target == null || target.life <= 0 || !target.active || target.dontTakeDamage || target.immortal)
				{
					target = base.Projectile.Center.ClosestNPCAt(SmashHomingRange, ignoreTiles: true, bossPriority: true);
					if (target != null)
					{
						SmashTarget = target.whoAmI;
					}
				}
				if (target != null)
				{
					base.Projectile.Center = target.Center;
				}
				if (AirTime == -1f)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<TriactisHammerExplosion>(), (int)((float)base.Projectile.damage * ExplosionDamageKBMult), base.Projectile.knockBack * ExplosionDamageKBMult, base.Projectile.owner);
					SoundEngine.PlaySound(Main.zenithWorld ? SmashSoundGFB : SmashSound, base.Projectile.Center);
					base.Projectile.Kill();
				}
				return;
			}
			float rotation = Main.GlobalTimeWrappedHourly * 2f + MathHelper.ToRadians(120f) * HammerState;
			Color currentColor = TriactisHammerFlare.GetColor(HammerState);
			Projectile target2 = Main.projectile[(int)SmashTarget];
			if (target2 == null || !target2.active)
			{
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, currentColor, Vector2.One, 0f, 0.2f, 4f, 20));
				base.Projectile.Kill();
				return;
			}
			if (AirTime == 0f)
			{
				OrbitRadius = MathHelper.Clamp(Vector2.Distance(base.Projectile.Center, target2.Center), 64f, 400f);
			}
			AirTime++;
			if (AirTime < WindUpTime)
			{
				OrbitRadius = MathHelper.Lerp(OrbitRadius, 800f, 0.04f);
				base.Projectile.Center = target2.Center + Vector2.UnitX.RotatedBy(rotation) * OrbitRadius;
				base.Projectile.rotation = base.Projectile.AngleFrom(target2.Center) + (float)Math.PI / 4f;
				base.Projectile.scale = Utils.GetLerpValue(0f, WindUpTime * 0.1f, AirTime, clamped: true);
				if (AirTime == 1f)
				{
					for (int i = 0; i < 6; i++)
					{
						Vector2 velocity = Vector2.UnitX.RotatedBy(rotation).RotatedByRandom(MathHelper.ToRadians(36f)) * Main.rand.NextFloat(10f, 30f);
						float scale = Main.rand.NextFloat(0.8f, 1.5f);
						GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, velocity, Color.White, currentColor, 1f * scale, 24, 0.1f, 2.5f * scale));
					}
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, currentColor, Vector2.One, 0f, 0.1f, 0.8f, 10));
				}
				if (OrbitRadius > 792f)
				{
					GeneralParticleHandler.SpawnParticle(new ManaDrainStreak(Owner, Main.rand.NextFloat(0.6f, 1f), Main.rand.NextVector2Unit() * Main.rand.NextFloat(160f, 320f), 0f, currentColor, base.Projectile.GetAlpha(Color.White), Main.rand.Next(10, 20), base.Projectile.Center));
					if (AirTime % 18f == 0f)
					{
						GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, base.Projectile.GetAlpha(Color.White), Vector2.One, 0f, 0.2f, 2.5f, 12));
					}
				}
			}
			else
			{
				OrbitRadius = MathHelper.Lerp(800f, 0f, (AirTime - WindUpTime) / ConvergeTime);
				base.Projectile.Center = target2.Center + Vector2.UnitX.RotatedBy(rotation) * OrbitRadius;
				base.Projectile.rotation = base.Projectile.AngleTo(target2.Center) + (float)Math.PI / 4f;
				hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(target2.Hitbox) || AirTime > WindUpTime + ConvergeTime)
				{
					target2.ai[0] = -1f;
				}
			}
			return;
		}
		if (HammerState == 0f)
		{
			AirTime++;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f + (float)base.Projectile.direction * MathHelper.ToRadians(2f) * AirTime;
			base.Projectile.velocity.X *= 0.99f;
			if (base.Projectile.velocity.Y < 20f)
			{
				base.Projectile.velocity.Y += 0.2f;
			}
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 228, base.Projectile.velocity * 0.3f);
			dust.position += Main.rand.NextVector2Unit() * Main.rand.NextFloat(0f, 64f);
			dust.noGravity = true;
			if (Vector2.Distance(base.Projectile.Center, Owner.MountedCenter) >= 2000f)
			{
				base.Projectile.Kill();
			}
			return;
		}
		if (AirTime < 30f)
		{
			AirTime++;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
			float idealRotation = base.Projectile.AngleTo(Owner.MountedCenter) + (float)Math.PI / 4f;
			base.Projectile.rotation = MathHelper.Lerp(base.Projectile.rotation, idealRotation, AirTime / 30f);
		}
		else
		{
			base.Projectile.rotation = base.Projectile.AngleTo(Owner.MountedCenter) + (float)Math.PI / 4f;
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(Owner.MountedCenter) * 25f;
			hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox) || Vector2.Distance(base.Projectile.Center, Owner.MountedCenter) >= 2000f)
			{
				base.Projectile.Kill();
			}
		}
		if (!Main.rand.NextBool(4))
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 228, base.Projectile.velocity * 0.2f);
			dust2.position += Main.rand.NextVector2Unit() * Main.rand.NextFloat(0f, 64f);
			dust2.noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		float hammerState = HammerState;
		if (hammerState != 1f)
		{
			if (hammerState != 2f)
			{
				if (hammerState == 3f)
				{
					return new Color(117, 170, 239);
				}
				return Color.White;
			}
			return new Color(132, 225, 26);
		}
		return Color.Red;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits > 0 || HammerState > 0f)
		{
			return;
		}
		int FlareCount = Owner.ownedProjectileCounts[ModContent.ProjectileType<TriactisHammerFlare>()] + 1;
		if (Main.zenithWorld)
		{
			SoundStyle style = HitSoundGFB with
			{
				Pitch = (float)FlareCount * 0.15f - 0.15f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else
		{
			SoundStyle style = HitSound with
			{
				Pitch = (float)FlareCount * 0.15f - 0.15f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		for (int i = 0; i < 32; i++)
		{
			Vector2 velocity = Vector2.UnitX.RotatedBy((float)Math.PI * 2f * (float)i / 32f) * (8f + 2f * (float)FlareCount) * ((i % 2 == 0) ? 1.2f : 1f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 228, velocity);
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale = ((i % 2 == 0) ? 1.6f : 2.4f) * (0.7f + 0.3f * (float)FlareCount);
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type != ModContent.ProjectileType<TriactisHammerFlare>() || p.owner != Owner.whoAmI)
			{
				continue;
			}
			if (p.ai[1] != (float)target.whoAmI)
			{
				p.ai[1] = target.whoAmI;
				for (int j = 0; j < 5; j++)
				{
					Vector2 velocity2 = Main.rand.NextVector2Unit() * Main.rand.NextFloat(6f, 10f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(p.Center, velocity2, Color.White, TriactisHammerFlare.GetColor(p.ai[0]), 1f, 24, 0.1f, 2.4f));
				}
			}
			if (FlareCount > 3)
			{
				p.ai[1] = -2f;
				p.ai[2] = base.Projectile.whoAmI;
			}
			p.netUpdate = true;
		}
		if (FlareCount > 3)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Gold, Color.White, 0.5f), Vector2.One, 0f, 0.2f, 2.5f, 24));
			SoundEngine.PlaySound(in WindUpSound, base.Projectile.Center);
			HammerState = 4f;
			SmashTarget = target.whoAmI;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.ExpandHitboxBy(4);
			base.Projectile.netUpdate = true;
		}
		else
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<TriactisHammerFlare>(), (int)((float)base.Projectile.damage * SuperHammerDamageMult), 0f, base.Projectile.owner, FlareCount, target.whoAmI);
			HammerState = -1f;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		if (HammerState == 4f)
		{
			return false;
		}
		if (HammerState > 0f)
		{
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
			Texture2D echoTex = EchoHammer.Value;
			Main.EntitySpriteDraw(echoTex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, echoTex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.ExitShaderRegion();
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? CanDamage()
	{
		if (HammerState == 4f)
		{
			return false;
		}
		if (HammerState > 0f && AirTime < WindUpTime)
		{
			return false;
		}
		return base.CanDamage();
	}
}
