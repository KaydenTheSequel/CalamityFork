using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class WulfrumDroid : ModProjectile, ILocalizedModType, IModType
{
	public enum BehaviorState
	{
		Aggressive,
		Idle
	}

	public static readonly SoundStyle HelloSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumDroidSpawnBeep")
	{
		PitchVariance = 0.4f
	};

	public static readonly SoundStyle PewSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumDroidFire")
	{
		PitchVariance = 0.4f,
		Volume = 0.6f,
		MaxInstances = 0
	};

	public static readonly SoundStyle RandomChirpSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumDroidChirp", 4)
	{
		PitchVariance = 0.3f
	};

	public static readonly SoundStyle HurrySound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumDroidHurry", 2)
	{
		PitchVariance = 0.3f
	};

	public static readonly SoundStyle RepairSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumDroidRepair")
	{
		Volume = 0.8f,
		PitchVariance = 0.3f,
		SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
	};

	internal Color PrimColorMult;

	public Player healedPlayer;

	public float Initialized;

	public static float AggroRange = 450f;

	public static float ShootDelay = 110f;

	public float BuffModeBuffer;

	public static Asset<Texture2D> SheenTex;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int NewSoundDelay
	{
		get
		{
			int minionCount = 1;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj = enumerator.Current;
				if (proj.owner == Owner.whoAmI && proj.type == base.Type && proj.whoAmI != base.Projectile.whoAmI)
				{
					minionCount++;
				}
			}
			return Main.rand.Next(340, 1460) * minionCount;
		}
	}

	public BehaviorState State
	{
		get
		{
			return (BehaviorState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public ref float ShootTimer => ref base.Projectile.ai[1];

	public ref float AyeAyeCaptainCooldown => ref base.Projectile.localAI[0];

	public ref float NuzzleFlashTime => ref base.Projectile.localAI[1];

	public NPC Target
	{
		get
		{
			NPC target = null;
			if (Owner.HasMinionAttackTargetNPC)
			{
				target = CheckNPCTargetValidity(Main.npc[Owner.MinionAttackTargetNPC]);
			}
			if (target != null)
			{
				return target;
			}
			for (int npcIndex = 0; npcIndex < Main.npc.Length; npcIndex++)
			{
				target = CheckNPCTargetValidity(Main.npc[npcIndex]);
				if (target != null)
				{
					return target;
				}
			}
			return null;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 24;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 86;
		base.Projectile.height = 44;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
		Initialized = 0f;
		BuffModeBuffer = 15f;
	}

	public NPC CheckNPCTargetValidity(NPC potentialTarget)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (potentialTarget.CanBeChasedBy(this) && Vector2.Distance(potentialTarget.Center, base.Projectile.Center) < AggroRange && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, potentialTarget.position, potentialTarget.width, potentialTarget.height))
		{
			return potentialTarget;
		}
		return null;
	}

	public override void AI()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0822: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_098b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0990: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (Initialized == 0f)
		{
			int dustAmt = Main.rand.Next(10, 16);
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				Vector2 direction = Main.rand.NextVector2CircularEdge(1f, 1f);
				Vector2 position = base.Projectile.Center + direction * Main.rand.NextFloat(1f, 8f);
				float scale = Main.rand.NextFloat(1f, 1.4f);
				Dust dust = Dust.NewDustPerfect(position, 229, null, 100, default(Color), scale);
				dust.noGravity = true;
				dust.noLight = true;
				dust.velocity = direction * Main.rand.NextFloat(2f, 4f);
			}
			SoundEngine.PlaySound(in HelloSound, base.Projectile.Center);
			base.Projectile.soundDelay = NewSoundDelay;
			ShootTimer = ShootDelay;
			Initialized++;
		}
		if (Owner.Calamity().mouseRight && Owner.HeldItem.type == ModContent.ItemType<WulfrumController>())
		{
			if (Owner.itemTime == Owner.itemTimeMax)
			{
				if (BuffModeBuffer == 15f)
				{
					BuffModeBuffer--;
				}
				if (BuffModeBuffer == -15f)
				{
					BuffModeBuffer++;
				}
			}
			if (BuffModeBuffer < 15f && BuffModeBuffer >= 0f)
			{
				BuffModeBuffer--;
				if (BuffModeBuffer == 0f)
				{
					BuffModeBuffer = -15f;
				}
			}
			if (BuffModeBuffer > -15f && BuffModeBuffer <= 0f)
			{
				BuffModeBuffer++;
				if (BuffModeBuffer == 0f)
				{
					BuffModeBuffer = 15f;
				}
			}
			base.Projectile.netUpdate = true;
		}
		else if (BuffModeBuffer > 0f && BuffModeBuffer < 15f)
		{
			BuffModeBuffer++;
		}
		else if (BuffModeBuffer < 0f && BuffModeBuffer > -15f)
		{
			BuffModeBuffer--;
		}
		bool buffMode = BuffModeBuffer <= 0f;
		base.Projectile.frameCounter++;
		if (buffMode)
		{
			base.Projectile.frame = MathHelper.Clamp(base.Projectile.frame, 8, 15);
			if (base.Projectile.frameCounter >= 6)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 15)
			{
				base.Projectile.frame = 8;
			}
		}
		else
		{
			base.Projectile.frame = MathHelper.Clamp(base.Projectile.frame, 0, 7);
			if (base.Projectile.frameCounter >= 6)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 7)
			{
				base.Projectile.frame = 0;
			}
		}
		player.AddBuff(ModContent.BuffType<WulfrumDroidBuff>(), 3600);
		if (player.dead)
		{
			modPlayer.wDroid = false;
		}
		if (modPlayer.wDroid)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.MinionAntiClump();
		NPC targetCache = (buffMode ? null : Target);
		float separationAnxietyDist = ((targetCache != null) ? 1000f : 500f);
		if (AyeAyeCaptainCooldown > 0f)
		{
			AyeAyeCaptainCooldown--;
		}
		else if (buffMode)
		{
			SoundEngine.PlaySound(in RepairSound, base.Projectile.Center);
			AyeAyeCaptainCooldown = 100f;
		}
		if (base.Projectile.soundDelay > 0)
		{
			base.Projectile.soundDelay--;
		}
		else
		{
			SoundStyle style = RandomChirpSound with
			{
				Volume = RandomChirpSound.Volume * Main.rand.NextFloat(0.5f, 1f)
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.soundDelay = NewSoundDelay;
			if (targetCache == null)
			{
				Vector2 emoteDirection = -Vector2.UnitY.RotatedByRandom(1.0995573997497559);
				GeneralParticleHandler.SpawnParticle(new WulfrumDroidEmote(base.Projectile.Center + emoteDirection * 10f, emoteDirection * Main.rand.NextFloat(3f, 5f), Main.rand.Next(30, 65), Main.rand.NextFloat(1.4f, 2f)));
			}
		}
		if (Vector2.Distance(Owner.Center, base.Projectile.Center) > separationAnxietyDist)
		{
			State = BehaviorState.Idle;
			base.Projectile.netUpdate = true;
			if (base.Projectile.soundDelay < 10000)
			{
				SoundEngine.PlaySound(in HurrySound, base.Projectile.Center);
			}
			base.Projectile.soundDelay = 10010;
			if (Main.rand.NextBool(7))
			{
				Vector2 emoteDirection2 = -Vector2.UnitY.RotatedByRandom(1.0995573997497559);
				emoteDirection2.X -= (float)Math.Sign(base.Projectile.velocity.X) * 1f;
				GeneralParticleHandler.SpawnParticle(new WulfrumDroidSweatEmote(base.Projectile.Center + emoteDirection2 * 10f, emoteDirection2 * Main.rand.NextFloat(3f, 5f), Main.rand.Next(20, 35), Main.rand.NextFloat(1.4f, 2f)));
			}
		}
		else if (base.Projectile.soundDelay >= 10000)
		{
			base.Projectile.soundDelay = NewSoundDelay;
		}
		if (targetCache != null && State == BehaviorState.Aggressive)
		{
			Vector2 vectorToTarget = targetCache.Center - base.Projectile.Center;
			float num = ((Vector2)(ref vectorToTarget)).Length();
			vectorToTarget = vectorToTarget.SafeNormalize(Vector2.Zero);
			if (num > 200f)
			{
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, vectorToTarget * 6f, 1f / 41f);
			}
			else if (base.Projectile.velocity.Y > -1f)
			{
				base.Projectile.velocity.Y -= 0.1f;
			}
			if (Math.Abs(base.Projectile.Center.X - targetCache.Center.X) < 10f)
			{
				base.Projectile.velocity.X += 4f * (float)Math.Sign(base.Projectile.Center.X - targetCache.Center.X);
			}
		}
		else
		{
			if (!Collision.CanHitLine(base.Projectile.Center, 1, 1, Owner.Center, 1, 1))
			{
				State = BehaviorState.Idle;
			}
			float returnSpeed = ((State == BehaviorState.Idle) ? 15f : 6f);
			Vector2 playerVec = Owner.Center - base.Projectile.Center - Vector2.UnitY * 60f;
			float playerDist = ((Vector2)(ref playerVec)).Length();
			if (playerDist > 200f && returnSpeed < 9f)
			{
				returnSpeed = 9f;
			}
			if (playerDist < 100f && State == BehaviorState.Idle && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				State = BehaviorState.Aggressive;
				base.Projectile.netUpdate = true;
			}
			if (playerDist > 2000f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
			else if (buffMode)
			{
				AyeAyeCaptainCooldown = 50f;
				Player playerToBuff = Owner;
				Vector2 center = Owner.MountedCenter - Owner.Calamity().mouseWorld;
				float mouseDistanceToOwner = ((Vector2)(ref center)).Length();
				if (Main.netMode == 1 && mouseDistanceToOwner > 120f)
				{
					ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Player p = enumerator.Current;
						if (!p.dead && (p.team == Owner.team || p.team == 0))
						{
							center = p.MountedCenter - Owner.Calamity().mouseWorld;
							float mouseDistanceToPotentialTarget = ((Vector2)(ref center)).Length();
							if (mouseDistanceToPotentialTarget < 120f && mouseDistanceToOwner > mouseDistanceToPotentialTarget)
							{
								playerToBuff = p;
								mouseDistanceToOwner = mouseDistanceToPotentialTarget;
							}
						}
					}
					if (playerToBuff != Owner && Main.rand.NextBool(4))
					{
						Vector2 direction2 = Main.rand.NextVector2CircularEdge(1f, 1f);
						Vector2 position2 = playerToBuff.Center + direction2 * Main.rand.NextFloat(4f, 9f);
						float scale = Main.rand.NextFloat(1f, 1.4f);
						Dust dust2 = Dust.NewDustPerfect(position2, 229, null, 100, default(Color), scale);
						dust2.noGravity = true;
						dust2.noLight = true;
						dust2.velocity = direction2 * Main.rand.NextFloat(2f, 4f);
					}
				}
				healedPlayer = playerToBuff;
				center = healedPlayer.Center - base.Projectile.Center;
				float num2 = ((Vector2)(ref center)).Length();
				Vector2 mountedCenter = playerToBuff.MountedCenter;
				Vector2 unitY = Vector2.UnitY;
				double radians = (float)Math.Sin(Main.GlobalTimeWrappedHourly + (float)base.Projectile.whoAmI) * ((float)Math.PI / 2f) * 0.9f;
				center = default(Vector2);
				Vector2 aimPosition = mountedCenter - unitY.RotatedBy(radians, center) * 60f - Vector2.UnitY * 20f;
				center = aimPosition - base.Projectile.Center;
				float distanceToAim = ((Vector2)(ref center)).Length();
				if (distanceToAim > 50f)
				{
					float speed = MathHelper.Lerp(10f, 30f, Math.Clamp((distanceToAim - 110f) / 400f, 0f, 1f));
					base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, (aimPosition - base.Projectile.Center).SafeNormalize(Vector2.Zero) * speed, 0.05f);
				}
				else
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 0.98f;
					if (base.Projectile.velocity == Vector2.Zero)
					{
						base.Projectile.velocity = (aimPosition - base.Projectile.Center).SafeNormalize(Vector2.Zero) * 5f;
					}
				}
				if (num2 < 200f)
				{
					playerToBuff.GetModPlayer<WulfrumControllerPlayer>().buffingDrones++;
					ShootTimer--;
					if (ShootTimer <= 0f)
					{
						ShootTimer = ShootDelay;
						if (modPlayer.roverDrive && modPlayer.RoverDriveShieldDurability < RoverDrive.ShieldDurabilityMax)
						{
							CalamityPlayer buffedCalPlayer = playerToBuff.Calamity();
							buffedCalPlayer.RoverDriveShieldDurability++;
							if (buffedCalPlayer.cooldowns.TryGetValue(WulfrumRoverDriveDurability.ID, out var cd))
							{
								cd.timeLeft = buffedCalPlayer.RoverDriveShieldDurability;
							}
						}
					}
				}
			}
			else if (playerDist > 70f)
			{
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, playerVec.SafeNormalize(Vector2.Zero) * returnSpeed, 1f / 21f);
			}
			else
			{
				if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
				{
					base.Projectile.velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * -0.15f;
				}
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.01f;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		base.Projectile.spriteDirection = (base.Projectile.direction = Math.Sign(base.Projectile.velocity.X));
		if (!buffMode && targetCache != null)
		{
			ChargeUpAndFire(targetCache);
		}
	}

	public void ChargeUpAndFire(NPC targetCache)
	{
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		if (ShootTimer <= 20f)
		{
			base.Projectile.frame = (int)Utils.Remap(ShootTimer, 20f, 4f, 20f, 23f);
			ShootTimer -= 2f;
			if (ShootTimer <= 0f || targetCache == null || BuffModeBuffer < 15f)
			{
				ShootTimer = ShootDelay;
				base.Projectile.frame = 0;
				base.Projectile.netUpdate = true;
				return;
			}
			base.Projectile.rotation = Utils.Remap(ShootTimer, 18f, 4f, base.Projectile.AngleTo(targetCache.Center) + ((base.Projectile.direction == 1) ? 0f : ((base.Projectile.Center.Y < targetCache.Center.Y) ? (-(float)Math.PI) : ((float)Math.PI))), base.Projectile.rotation);
			if (ShootTimer == 18f)
			{
				NuzzleFlashTime = 20f;
				SoundEngine.PlaySound(in PewSound, base.Projectile.Center);
				Vector2 velocity = targetCache.Center - base.Projectile.Center;
				((Vector2)(ref velocity)).Normalize();
				velocity *= 10f;
				Projectile projectile = base.Projectile;
				projectile.velocity += velocity * -0.3f;
				if (Main.myPlayer == base.Projectile.owner)
				{
					int bolt = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<WulfrumEnergyBurst>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					Main.projectile[bolt].originalDamage = base.Projectile.originalDamage;
					Main.projectile[bolt].netUpdate = true;
					base.Projectile.netUpdate = true;
				}
			}
		}
		else if (ShootTimer <= 36f)
		{
			base.Projectile.frame = (int)Utils.Remap(ShootTimer, 36f, 20f, 16f, 20f);
			ShootTimer -= 2f;
			if (targetCache == null || BuffModeBuffer < 15f)
			{
				ShootTimer = ShootDelay;
				base.Projectile.frame -= 16;
				base.Projectile.netUpdate = true;
				return;
			}
			base.Projectile.rotation = Utils.Remap(ShootTimer, 34f, 20f, base.Projectile.rotation, base.Projectile.AngleTo(targetCache.Center) + ((base.Projectile.direction == 1) ? 0f : ((base.Projectile.Center.Y < targetCache.Center.Y) ? (-(float)Math.PI) : ((float)Math.PI))));
			if (ShootTimer < 20f)
			{
				ShootTimer = 20f;
			}
		}
		else
		{
			ShootTimer -= Main.rand.Next(1, 4);
			if (ShootTimer < 36f)
			{
				ShootTimer = 36f;
			}
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = 0.4f + 0.4f * ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f + completionRatio * -12f) * 0.5f + 0.5f);
		float num = fadeOpacity;
		Vector2 val = healedPlayer.Center - base.Projectile.Center;
		fadeOpacity = num * (1f - MathHelper.Clamp((((Vector2)(ref val)).Length() - 170f) / 70f, 0f, 1f));
		return Color.CornflowerBlue.MultiplyRGB(PrimColorMult) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (3.4f + 4f * ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f + completionRatio * -12f) * 0.5f + 0.5f)) * 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		if (healedPlayer == null)
		{
			healedPlayer = Owner;
		}
		if (BuffModeBuffer <= 0f)
		{
			Vector2 val = healedPlayer.Center - base.Projectile.Center;
			if (((Vector2)(ref val)).Length() < 240f)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
				GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ZapTrail", (AssetRequestMode)2));
				Vector2[] drawPos = (Vector2[])(object)new Vector2[5]
				{
					base.Projectile.Center,
					base.Projectile.Center,
					healedPlayer.Center + (base.Projectile.Center - healedPlayer.Center) * 0.5f + Vector2.UnitY * 40f,
					healedPlayer.Center,
					healedPlayer.Center
				};
				CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 1.8f, delegate(Vector2 offset, Color colorMod)
				{
					//IL_0007: Unknown result type (might be due to invalid IL or missing references)
					//IL_0008: Unknown result type (might be due to invalid IL or missing references)
					//IL_0013: Unknown result type (might be due to invalid IL or missing references)
					//IL_0014: Unknown result type (might be due to invalid IL or missing references)
					PrimColorMult = colorMod;
					PrimitiveRenderer.RenderTrail(drawPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
					{
						//IL_0001: Unknown result type (might be due to invalid IL or missing references)
						return offset;
					}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
				});
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			}
		}
		return true;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		if (NuzzleFlashTime > 0f)
		{
			NuzzleFlashTime--;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float opacity = (float)Math.Pow(NuzzleFlashTime / 20f, 1.7000000476837158);
			if (SheenTex == null)
			{
				SheenTex = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2);
			}
			Texture2D shineTex = SheenTex.Value;
			Vector2 shineScale = default(Vector2);
			((Vector2)(ref shineScale))._002Ector(1.2f, 2f - opacity * 1.33f);
			Main.EntitySpriteDraw(shineTex, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * 2f, null, Color.GreenYellow * opacity, (float)Math.PI / 2f, shineTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(BuffModeBuffer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		BuffModeBuffer = reader.ReadSingle();
	}
}
