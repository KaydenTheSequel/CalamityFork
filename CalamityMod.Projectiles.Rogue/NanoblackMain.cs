using System;
using System.IO;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class NanoblackMain : ModProjectile, ILocalizedModType, IModType
{
	internal const int UpdatesPerFrame = 3;

	private const int Lifetime = 240;

	private const float BoomerangReturnTime = 16f;

	private const int BaseTesselationDelay = 4;

	private const float TesselationSpawnSpeed = 24f;

	internal const float RotationIncrement = 0.22f;

	private static readonly SoundStyle LightspeedMissSound = new SoundStyle("CalamityMod/Sounds/Item/NanoblackReaper/NanoblackReaper_LightspeedMiss")
	{
		Volume = 0.8f,
		PitchVariance = 0.1f,
		MaxInstances = 8
	};

	private static readonly SoundStyle LightspeedPerfectMissSound = new SoundStyle("CalamityMod/Sounds/Item/NanoblackReaper/NanoblackReaper_LightspeedMissPerfect")
	{
		Volume = 0.9f,
		PitchVariance = 0.08f,
		MaxInstances = 8
	};

	private static readonly SoundStyle LightspeedSlashBaseSound = new SoundStyle("CalamityMod/Sounds/Item/NanoblackReaper/NanoblackReaper_LightspeedSlash")
	{
		Volume = 0.85f,
		PitchVariance = 0.08f,
		MaxInstances = 10
	};

	private static readonly SoundStyle LightspeedSlashVariantSound = new SoundStyle("CalamityMod/Sounds/Item/NanoblackReaper/NanoblackReaper_LightspeedSlash", 3)
	{
		Volume = 0.65f,
		PitchVariance = 0.12f,
		MaxInstances = 10
	};

	private static readonly SoundStyle LightspeedPerfectSlashSound = new SoundStyle("CalamityMod/Sounds/Item/NanoblackReaper/NanoblackReaper_PerfectLightspeedSlash")
	{
		Volume = 0.95f,
		PitchVariance = 0.06f,
		MaxInstances = 8
	};

	internal const float LightspeedCarveState_Initial = 0f;

	internal const float LightspeedCarveState_CanImperfect = 1f;

	internal const float LightspeedCarveState_CanPerfect = 2f;

	internal const float LightspeedCarveState_Performed = 3f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/NanoblackReaper";

	private static int InternalLifetime => 720;

	private static int InternalTesselationDelay => 12;

	private Player Owner => Main.player[base.Projectile.owner];

	internal ref float RealFrameCounter => ref base.Projectile.ai[0];

	internal ref float TesselationSpawnCooldown => ref base.Projectile.ai[1];

	internal ref float LightspeedCarveState => ref base.Projectile.ai[2];

	internal bool Returning
	{
		get
		{
			return base.Projectile.localAI[0] != 0f;
		}
		set
		{
			base.Projectile.localAI[0] = (value ? 1f : 0f);
		}
	}

	internal ref float ReboundStartFrame => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		base.DrawOffsetX = -11;
		base.DrawOriginOffsetY = -4;
		base.DrawOriginOffsetX = 0f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = InternalLifetime;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 18;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Returning);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Returning = reader.ReadBoolean();
	}

	public override void AI()
	{
		if (base.Projectile.timeLeft == InternalLifetime)
		{
			FrameOneEffects();
		}
		if (Owner.whoAmI == Main.myPlayer)
		{
			Owner.Calamity().mouseWorldListener = true;
		}
		InFlightVisualEffects();
		UpdateAIVariables();
		if (RealFrameCounter >= 16f && RealFrameCounter < 17f)
		{
			Returning = true;
			ReboundStartFrame = RealFrameCounter;
			if (LightspeedCarveState == 0f)
			{
				LightspeedCarveState = 2f;
			}
			base.Projectile.netUpdate = true;
		}
		if (Returning)
		{
			if (LightspeedCarveState == 2f)
			{
				if (RealFrameCounter > ReboundStartFrame + (float)NanoblackReaper.PerfectLightspeedCarveFrames)
				{
					LightspeedCarveState = 1f;
				}
			}
			else if (LightspeedCarveState == 1f && RealFrameCounter > ReboundStartFrame + (float)NanoblackReaper.PerfectLightspeedCarveFrames + (float)NanoblackReaper.ImperfectLightspeedCarveFrames)
			{
				LightspeedCarveState = 3f;
			}
			BoomerangMovement();
		}
		if ((base.Projectile.Calamity().stealthStrike || true) && TesselationSpawnCooldown <= 0f)
		{
			SpawnTesselation();
			TesselationSpawnCooldown = InternalTesselationDelay;
		}
		RotateScytheInFlight();
	}

	private void FrameOneEffects()
	{
		RealFrameCounter = 0f;
		TesselationSpawnCooldown = InternalTesselationDelay;
		LightspeedCarveState = (base.Projectile.Calamity().stealthStrike ? 3f : 0f);
	}

	private void InFlightVisualEffects()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dustType = (Main.rand.NextBool(5) ? ModContent.DustType<VoidDust>() : ModContent.DustType<VoidDustInverted>());
			Vector2 position = Main.rand.NextVector2FromRectangle(base.Projectile.Hitbox);
			float scale = Main.rand.NextFloat(0.8f, 1.1f);
			float velocityMult = Main.rand.NextFloat(0.3f, 0.6f);
			Vector2? velocity = Vector2.Zero;
			float scale2 = scale;
			Dust d = Dust.NewDustPerfect(position, dustType, velocity, 0, default(Color), scale2);
			if (d != null && d.dustIndex != 6000)
			{
				d.color = NanoblackReaper.NanoblackDustColor1;
				d.noGravity = true;
				d.velocity = velocityMult * base.Projectile.velocity;
			}
		}
	}

	private void UpdateAIVariables()
	{
		if (base.Projectile.FinalExtraUpdate())
		{
			RealFrameCounter++;
		}
		TesselationSpawnCooldown--;
	}

	private void BoomerangMovement()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Owner;
		Vector2 toOwner = base.Projectile.SafeDirectionTo(owner.Center, -Vector2.UnitY);
		float currentReturnSpeed = NanoblackReaper.Speed;
		float returnSpeedIncreaseTime = 32f;
		if (RealFrameCounter >= returnSpeedIncreaseTime)
		{
			currentReturnSpeed *= 1f + 0.05f * (RealFrameCounter - returnSpeedIncreaseTime);
		}
		Vector2 desiredVelocity = currentReturnSpeed * toOwner;
		float returnSharpness = 0.04f;
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, desiredVelocity, returnSharpness);
		if (Main.myPlayer == base.Projectile.owner)
		{
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(owner.Hitbox))
			{
				base.Projectile.Kill();
			}
		}
	}

	private void SpawnTesselation()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		int numTessSpawns = 3;
		float[] zeroPointStrikeDelays = new float[numTessSpawns];
		for (int i = 0; i < numTessSpawns; i++)
		{
			zeroPointStrikeDelays[i] = GetStrikeDelay();
		}
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		int tessID = ModContent.ProjectileType<NanoblackTesselation>();
		int tessDamage = (int)(NanoblackReaper.TesselationDamageRatio * (float)base.Projectile.damage);
		float tessKB = NanoblackReaper.TesselationKnockback;
		Vector2 spawnOffsetDir = (base.Projectile.rotation * (float)base.Projectile.spriteDirection).ToRotationVector2();
		Vector2 tessPos = base.Projectile.Center + spawnOffsetDir * 14f;
		Vector2 tessBaseVel = spawnOffsetDir.RotatedBy(-0.7853981852531433) * 24f;
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int j = 0; j < numTessSpawns; j++)
		{
			Vector2 tessVel = tessBaseVel.RotatedBy((float)j * ((float)Math.PI * 2f / 3f));
			float delay = zeroPointStrikeDelays[j];
			int tessIdx = Projectile.NewProjectile(source, tessPos, tessVel, tessID, tessDamage, tessKB, base.Projectile.owner, delay);
			if (tessIdx.WithinBounds(Main.maxProjectiles))
			{
				Projectile obj = Main.projectile[tessIdx];
				obj.direction = (obj.spriteDirection = base.Projectile.spriteDirection);
				obj.Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
			}
		}
		static float GetStrikeDelay()
		{
			return Main.rand.NextFloat(15f, 45f);
		}
	}

	internal void AttemptLightspeedCarve()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		float lcs = LightspeedCarveState;
		if (lcs == 3f)
		{
			return;
		}
		int projType;
		int damage;
		float kb;
		IEntitySource source;
		int num;
		Vector2 pos;
		if (lcs != 0f && base.Projectile.owner == Main.myPlayer)
		{
			projType = ModContent.ProjectileType<NanoblackLightspeedCarve>();
			damage = base.Projectile.damage * 6;
			kb = NanoblackReaper.LightspeedCarveKnockback;
			source = base.Projectile.GetSource_FromThis();
			pos = base.Projectile.Center;
			NPC target = Owner.ClampedMouseWorld().ClosestNPCAt(600f, ignoreTiles: true, bossPriority: true);
			if (target == null || !target.active)
			{
				target = base.Projectile.Center.ClosestNPCAt(600f, ignoreTiles: true, bossPriority: true);
			}
			if (target != null)
			{
				num = (target.active ? 1 : 0);
				if (num != 0)
				{
					pos = target.Center;
					goto IL_00f0;
				}
			}
			else
			{
				num = 0;
			}
			SoundEngine.PlaySound((lcs == 2f) ? LightspeedPerfectMissSound : LightspeedMissSound, base.Projectile.Center);
			goto IL_00f0;
		}
		goto IL_01a5;
		IL_00f0:
		float fuzz = NanoblackLightspeedCarve.PlacementRandomness;
		pos += Main.rand.NextVector2Circular(fuzz, fuzz);
		if (num != 0)
		{
			PlayLightspeedCarveSounds(lcs == 2f, pos);
		}
		if (lcs == 2f)
		{
			CalamityGlobalProjectile calamityGlobalProjectile = Projectile.NewProjectileDirect(source, pos, Vector2.Zero, projType, damage, kb, base.Projectile.owner, 1f).Calamity();
			calamityGlobalProjectile.supercritHits = -1;
			calamityGlobalProjectile.bonusCritDamage++;
		}
		else if (lcs == 1f)
		{
			Projectile.NewProjectile(source, pos, Vector2.Zero, projType, damage, kb, base.Projectile.owner);
		}
		goto IL_01a5;
		IL_01a5:
		LightspeedCarveState = 3f;
		base.Projectile.netUpdate = true;
	}

	private static void PlayLightspeedCarveSounds(bool perfect, Vector2 position)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (perfect)
		{
			SoundEngine.PlaySound(in LightspeedPerfectSlashSound, position);
		}
		else
		{
			SoundEngine.PlaySound(in LightspeedSlashVariantSound, position);
		}
	}

	private void RotateScytheInFlight()
	{
		float spin = ((base.Projectile.direction <= 0) ? (-1f) : 1f);
		base.Projectile.rotation += spin * 0.22f;
		base.Projectile.spriteDirection = base.Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		Color color = NanoblackReaper.NanoblackSlashColor1;
		Vector2 slashDir = -base.Projectile.velocity.SafeNormalize(-Vector2.UnitY);
		Vector2 vel = 0.01f * slashDir.RotatedByRandom(0.39269909262657166);
		GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(scale: 0.12f / 0.357f, relativePosition: base.Projectile.Center, velocity: vel, affectedByGravity: false, lifetime: 12, color: color));
		float glowScale = 0.12f * 0.333f;
		Vector2 squashStretch = default(Vector2);
		((Vector2)(ref squashStretch))._002Ector(1.3333f, 0.8f);
		GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, vel, affectedByGravity: false, 11, glowScale, color, squashStretch, quickShrink: true));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
