using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

[AutoloadBossHead]
public class NuclearTerror : ModNPC
{
	public enum SpecialAttackState
	{
		DivergingBullets,
		ConeStreamOfBullets,
		ShotgunBurstOfBullets
	}

	public int AttackIndex;

	public int DelayTime;

	public int DeathrayTime;

	public bool Dying;

	public bool Walking;

	public bool hasDoneDeathray;

	public float JumpTimer;

	public Vector2 ShootPosition;

	public static readonly SpecialAttackState[] PhaseArray = new SpecialAttackState[17]
	{
		SpecialAttackState.ShotgunBurstOfBullets,
		SpecialAttackState.DivergingBullets,
		SpecialAttackState.ConeStreamOfBullets,
		SpecialAttackState.ConeStreamOfBullets,
		SpecialAttackState.ShotgunBurstOfBullets,
		SpecialAttackState.ConeStreamOfBullets,
		SpecialAttackState.DivergingBullets,
		SpecialAttackState.ShotgunBurstOfBullets,
		SpecialAttackState.ConeStreamOfBullets,
		SpecialAttackState.ConeStreamOfBullets,
		SpecialAttackState.DivergingBullets,
		SpecialAttackState.ConeStreamOfBullets,
		SpecialAttackState.ShotgunBurstOfBullets,
		SpecialAttackState.ConeStreamOfBullets,
		SpecialAttackState.DivergingBullets,
		SpecialAttackState.ShotgunBurstOfBullets,
		SpecialAttackState.ConeStreamOfBullets
	};

	public const int AttackCycleTime = 520;

	public const int SpecialAttackTime = 240;

	public const float TeleportTime = 60f;

	public const float TeleportFadeinTime = 10f;

	public const float TeleportCooldown = 60f;

	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/NuclearTerrorSpawn");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/NuclearTerrorHit");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/NuclearTerrorDeath");

	public Player Target => Main.player[base.NPC.target];

	public ref float AttackTime => ref base.NPC.ai[0];

	public ref float TeleportCountdown => ref base.NPC.ai[1];

	public Vector2 TeleportLocation
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(base.NPC.ai[2], base.NPC.ai[3]);
		}
		set
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.ai[2] = value.X;
			base.NPC.ai[3] = value.Y;
		}
	}

	public ref float HorizontalCollisionCounterDelay => ref base.NPC.localAI[0];

	public ref float HorizontalCollisionSpamCounter => ref base.NPC.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 14;
		NPCID.Sets.TrailCacheLength[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		nPCBestiaryDrawModifiers.Direction = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 10f;
		value.Position.Y += 50f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.width = 176;
		base.NPC.height = 138;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.lifeMax = 90000;
		base.NPC.defense = 50;
		base.NPC.damage = 135;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 20);
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = null;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.NuclearTerror")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(Dying);
		writer.Write(Walking);
		writer.Write(hasDoneDeathray);
		writer.Write(AttackIndex);
		writer.Write(DelayTime);
		writer.Write(JumpTimer);
		writer.Write(DeathrayTime);
		writer.WriteVector2(ShootPosition);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		Dying = reader.ReadBoolean();
		Walking = reader.ReadBoolean();
		hasDoneDeathray = reader.ReadBoolean();
		AttackIndex = reader.ReadInt32();
		DelayTime = reader.ReadInt32();
		JumpTimer = reader.ReadSingle();
		DeathrayTime = reader.ReadInt32();
		ShootPosition = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.NPC.Center;
		Color val;
		Vector3 val2;
		if (!Dying)
		{
			val = Color.White;
			val2 = ((Color)(ref val)).ToVector3();
		}
		else
		{
			val = Color.Lime;
			val2 = ((Color)(ref val)).ToVector3();
		}
		Lighting.AddLight(center, val2 * 2f);
		if (Dying)
		{
			return;
		}
		bool phase2 = (float)base.NPC.life / (float)base.NPC.lifeMax < 0.5f;
		if (DelayTime > 0)
		{
			DelayTime--;
			base.NPC.velocity.X *= 0.9f;
			if (base.NPC.velocity.Y < 18f)
			{
				base.NPC.velocity.Y += 0.35f;
			}
			return;
		}
		if (base.NPC.target < 0 || base.NPC.target >= 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		if (TeleportCountdown > -60f)
		{
			TeleportEffects();
		}
		base.NPC.defDamage = 170;
		base.NPC.damage = ((!Dying) ? base.NPC.defDamage : 0);
		TeleportCheck();
		if (AttackTime == 0f)
		{
			SoundEngine.PlaySound(in SpawnSound, base.NPC.Center);
		}
		AttackTime++;
		float wrappedAttackTime = AttackTime % 520f;
		Walking = false;
		if (Main.zenithWorld && !hasDoneDeathray && (float)base.NPC.life <= (float)base.NPC.lifeMax * 0.1f)
		{
			DeathrayTime++;
			MasterSpark();
			return;
		}
		if (base.NPC.collideX)
		{
			if (HorizontalCollisionCounterDelay > 0f)
			{
				HorizontalCollisionSpamCounter++;
			}
			HorizontalCollisionCounterDelay = 20f;
		}
		if (HorizontalCollisionCounterDelay > 0f)
		{
			HorizontalCollisionCounterDelay--;
		}
		if (wrappedAttackTime < 240f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				JumpTimer++;
				base.NPC.velocity.X *= 0.8f;
				if (JumpTimer >= 70f || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Target.position, Target.width, Target.height))
				{
					JumpTimer = 0f;
					base.NPC.velocity.Y -= MathHelper.Clamp(Math.Abs(Target.Center.Y - base.NPC.Center.Y) / 12.5f, 8f, 18f);
					base.NPC.velocity.X = base.NPC.SafeDirectionTo(Target.Center).X * 18f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
				else
				{
					if (Walking != Math.Abs(base.NPC.velocity.X) > 4f)
					{
						Walking = Math.Abs(base.NPC.velocity.X) > 4f;
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
					if (base.NPC.collideX)
					{
						JumpTimer = 50f;
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
					else if (Math.Abs(Target.Center.X - base.NPC.Center.X) > 125f)
					{
						base.NPC.velocity.X += (float)Math.Sign(base.NPC.SafeDirectionTo(Target.Center).X) * 3f;
						base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X, -28f, 28f);
					}
					else
					{
						base.NPC.velocity.X *= 0.99f;
					}
				}
			}
			base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
		}
		else
		{
			base.NPC.velocity.X *= 0.96f;
			if (wrappedAttackTime == 255f)
			{
				ShootPosition = Target.Center;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				base.NPC.spriteDirection = (ShootPosition.X - base.NPC.Center.X < 0f).ToDirectionInt();
			}
			PerformSpecialAttack(wrappedAttackTime);
			if (wrappedAttackTime == 519f)
			{
				DelayTime = (phase2 ? 45 : 75);
				AttackIndex++;
				AttackIndex %= PhaseArray.Length;
			}
		}
	}

	public void TeleportCheck()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		float distanceFromTarget = base.NPC.Distance(Target.Center);
		bool targetIsFarOff = distanceFromTarget > 900f;
		bool targetNotInLineOfSight = !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Target.position, Target.width, Target.height);
		if (!(TeleportCountdown <= -60f) || (distanceFromTarget <= 2700f && !(targetIsFarOff & targetNotInLineOfSight) && !StuckOnPlatform() && !base.NPC.wet && HorizontalCollisionSpamCounter <= 5f))
		{
			return;
		}
		Point playerPositionTileCoords = Target.position.ToTileCoordinates();
		Point npcPositionTileCoords = base.NPC.position.ToTileCoordinates();
		for (int tries = 0; tries < 250; tries++)
		{
			int maxTeleportDistance = 30 + tries / 3;
			int x = Main.rand.Next(playerPositionTileCoords.X - maxTeleportDistance, playerPositionTileCoords.X + maxTeleportDistance);
			int yStart = Main.rand.Next(playerPositionTileCoords.Y - maxTeleportDistance, playerPositionTileCoords.Y + maxTeleportDistance);
			if (StuckOnPlatform())
			{
				yStart = Main.rand.Next(playerPositionTileCoords.Y, playerPositionTileCoords.Y + maxTeleportDistance * 4);
			}
			for (int y = yStart; y < playerPositionTileCoords.Y + maxTeleportDistance; y++)
			{
				Tile tileBelow = CalamityUtils.ParanoidTileRetrieval(x, y - 1);
				bool num = Math.Abs(y - playerPositionTileCoords.Y) < 12 || Math.Abs(x - playerPositionTileCoords.X) < 12;
				bool veryCloseToSelf = Math.Abs(y - npcPositionTileCoords.Y) < 12 || Math.Abs(x - npcPositionTileCoords.X) < 12;
				bool solidGround = (Main.tileSolid[tileBelow.TileType] || Main.tileSolidTop[tileBelow.TileType]) && tileBelow.HasTile;
				if (!((!num && !veryCloseToSelf) & solidGround) || CalamityUtils.ParanoidTileRetrieval(x, y - 1).LiquidType != 0 || Collision.SolidTiles(x - 12, x + 12, y - 7, y - 7))
				{
					continue;
				}
				int dy = y - 8;
				while (true)
				{
					if (dy <= y + 8)
					{
						if (CalamityUtils.ParanoidTileRetrieval(x, dy).LiquidAmount > 0)
						{
							break;
						}
						dy++;
						continue;
					}
					TeleportCountdown = 60f;
					TeleportLocation = new Vector2((float)x, (float)y - 6f);
					HorizontalCollisionSpamCounter = 0f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					return;
				}
			}
		}
	}

	public void TeleportEffects()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		if (TeleportCountdown > 60f)
		{
			TeleportCountdown = 60f;
		}
		TeleportCountdown--;
		if (TeleportCountdown >= 0f)
		{
			if (TeleportCountdown == 0f && TeleportLocation != Vector2.Zero)
			{
				base.NPC.position = TeleportLocation.ToWorldCoordinates(8f, 0f) - base.NPC.Size;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				base.NPC.velocity = Vector2.Zero;
				return;
			}
			base.NPC.Opacity = TeleportCountdown / 60f;
			int totalDust = (int)((float)(30 * base.NPC.alpha) / 255f);
			for (int i = 0; i < totalDust; i++)
			{
				Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 75);
				dust.noGravity = true;
				dust.velocity = base.NPC.DirectionFrom(dust.position) * 2f;
				dust.scale = 1.6f;
			}
			base.NPC.velocity.X *= 0.95f;
			if (base.NPC.velocity.Y < 18f)
			{
				base.NPC.velocity.Y += 0.35f;
			}
		}
		else
		{
			if (!(TeleportCountdown >= -10f))
			{
				return;
			}
			base.NPC.Opacity = TeleportCountdown / -10f;
			if (TeleportCountdown == -10f)
			{
				for (int j = 0; j < 48; j++)
				{
					Dust dust2 = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 75);
					dust2.noGravity = true;
					dust2.velocity = base.NPC.DirectionFrom(dust2.position) * Main.rand.NextFloat(2f, 3.6f);
					dust2.scale = 1.8f;
				}
			}
		}
	}

	public bool StuckOnPlatform()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = -12; i < 12; i++)
		{
			Point bottom = (base.NPC.Bottom + Vector2.UnitY * (float)i).ToTileCoordinates();
			if (TileID.Sets.Platforms[CalamityUtils.ParanoidTileRetrieval(bottom.X, bottom.Y).TileType] && Target.Top.Y > base.NPC.Bottom.Y + 48f)
			{
				return true;
			}
		}
		return false;
	}

	public void PerformSpecialAttack(float wrappedAttackTime)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		int damage = (Main.masterMode ? 32 : (Main.expertMode ? 38 : 48));
		Vector2 mouthPosition = base.NPC.Center - Vector2.UnitY * 26f;
		mouthPosition.X += (float)base.NPC.spriteDirection * -54f;
		TeleportCountdown = -60f;
		Vector2 directionToTarget = (Target.Center - mouthPosition).SafeNormalize(Vector2.UnitX * (float)base.NPC.spriteDirection);
		Vector2 directionToShootPosition = (ShootPosition - mouthPosition).SafeNormalize(Vector2.UnitX * (float)base.NPC.spriteDirection);
		switch (PhaseArray[AttackIndex])
		{
		case SpecialAttackState.DivergingBullets:
		{
			base.NPC.velocity.X *= 0.9f;
			float shootAdjustedTime = wrappedAttackTime - 280f;
			if ((shootAdjustedTime >= 20f) & ((shootAdjustedTime - 20f) % 30f < 12f) & (shootAdjustedTime <= 152f) & (shootAdjustedTime % 3f == 0f))
			{
				if (Main.netMode != 1)
				{
					float angle3 = (wrappedAttackTime - 300f) % 12f / 12f * MathHelper.ToRadians(15f) - MathHelper.ToRadians(7.5f);
					int bullet = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition, directionToShootPosition.RotatedBy(angle3) * 14f, ModContent.ProjectileType<NuclearBulletLarge>(), damage, 4f);
					Main.projectile[bullet].localAI[0] = angle3;
				}
				base.NPC.spriteDirection = (ShootPosition.X - base.NPC.Center.X < 0f).ToDirectionInt();
				SoundEngine.PlaySound(in SoundID.NPCDeath13, mouthPosition);
			}
			if (wrappedAttackTime >= 315f && AttackTime % 10f == 9f)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition, directionToTarget * 12f, ModContent.ProjectileType<NuclearBulletLarge>(), damage, 3f);
			}
			break;
		}
		case SpecialAttackState.ConeStreamOfBullets:
			if (wrappedAttackTime >= 315f && AttackTime % 4f == 3f)
			{
				if (Main.netMode != 1)
				{
					float angle2 = MathHelper.Lerp(MathHelper.ToRadians(35f), MathHelper.ToRadians(5f), (wrappedAttackTime - 315f) / 275f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition, directionToTarget.RotatedBy(angle2) * 16f, ModContent.ProjectileType<NuclearBulletLarge>(), damage, 4.5f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition, directionToTarget.RotatedBy(0f - angle2) * 16f, ModContent.ProjectileType<NuclearBulletLarge>(), damage, 4.5f);
				}
				base.NPC.spriteDirection = (Target.Center.X - base.NPC.Center.X < 0f).ToDirectionInt();
			}
			break;
		case SpecialAttackState.ShotgunBurstOfBullets:
			if (!(wrappedAttackTime >= 315f) || AttackTime % 20f != 19f)
			{
				break;
			}
			if (Main.netMode != 1)
			{
				for (int i = 0; i < 3; i++)
				{
					float angle = MathHelper.Lerp(-0.5f, 0.5f, (float)i / 3f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition, directionToShootPosition.RotatedBy(angle) * 13f, ModContent.ProjectileType<NuclearBulletMedium>(), damage, 4f);
				}
			}
			base.NPC.spriteDirection = (ShootPosition.X - base.NPC.Center.X < 0f).ToDirectionInt();
			break;
		}
	}

	public void MasterSpark()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.dontTakeDamage = true;
		base.NPC.velocity.X *= 0.5f;
		base.NPC.alpha = 0;
		if ((float)DeathrayTime < 240f)
		{
			int bigFuckOffDeathrayDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, 0f, 0f, 200, default(Color), 1.5f);
			Main.dust[bigFuckOffDeathrayDust].noGravity = true;
			Dust obj = Main.dust[bigFuckOffDeathrayDust];
			obj.velocity *= 0.75f;
			Main.dust[bigFuckOffDeathrayDust].fadeIn = 1.3f;
			Vector2 vector = default(Vector2);
			((Vector2)(ref vector))._002Ector((float)Main.rand.Next(-200, 201), (float)Main.rand.Next(-200, 201));
			((Vector2)(ref vector)).Normalize();
			vector *= (float)Main.rand.Next(100, 200) * 0.04f;
			Main.dust[bigFuckOffDeathrayDust].velocity = vector;
			((Vector2)(ref vector)).Normalize();
			vector *= 34f;
			Main.dust[bigFuckOffDeathrayDust].position = base.NPC.Center - vector;
		}
		else if ((float)DeathrayTime == 240f)
		{
			if (Main.netMode != 1)
			{
				SoundEngine.PlaySound(in SoundID.Zombie104, base.NPC.Center);
				int p = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<GammaRayBurst>(), 250, 0f, Main.myPlayer, base.NPC.whoAmI);
				if (p.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[p].rotation = (float)base.NPC.spriteDirection * (-(float)Math.PI / 2f);
				}
			}
			float screenShakePower = 20f * Utils.GetLerpValue(1300f, 0f, base.NPC.Distance(Main.LocalPlayer.Center), clamped: true);
			Main.LocalPlayer.SetScreenshake(screenShakePower);
		}
		else if ((float)DeathrayTime >= 630f)
		{
			base.NPC.dontTakeDamage = false;
			hasDoneDeathray = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.85f);
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.frameCounter++;
		int frameChangeRate = (Dying ? 7 : 6);
		if (Walking)
		{
			frameChangeRate = 8 - (int)Math.Ceiling(Math.Abs(base.NPC.velocity.X) / 5f);
		}
		if (base.NPC.frameCounter >= (double)frameChangeRate)
		{
			base.NPC.frame.Y += frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (Dying)
		{
			SoundEngine.PlaySound(in DeathSound, base.NPC.Center);
			if (base.NPC.frame.Y < frameHeight * 8)
			{
				int damage = (Main.masterMode ? 32 : (Main.expertMode ? 38 : 48));
				if (Main.netMode != 1)
				{
					for (int i = 0; i < 16; i++)
					{
						int type = (Main.rand.NextBool(4) ? ModContent.ProjectileType<SulphuricAcidMist>() : ModContent.ProjectileType<NuclearBulletLarge>());
						float angle = (float)Math.PI / 8f * (float)i;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, angle.ToRotationVector2() * Main.rand.NextFloat(4f, 11f), type, damage, 3f);
					}
				}
				for (int j = 0; j < 60; j++)
				{
					Dust dust = Dust.NewDustDirect(base.NPC.Center, 45, 45, 75);
					dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(4f, 15f);
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(2f, 3f);
				}
				base.NPC.frame.Y = frameHeight * 8;
			}
			if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type] && Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
		}
		else if (base.NPC.frame.Y >= (Walking ? 8 : 4) * frameHeight)
		{
			base.NPC.frame.Y = (Walking ? (4 * frameHeight) : 0);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.NPC.velocity)).Length() > 0f)
		{
			Color endColor = Color.DarkOliveGreen;
			Color transparent = Color.Transparent;
			((Color)(ref endColor)).A = ((Color)(ref transparent)).A;
			CalamityGlobalNPC.DrawAfterimage(base.NPC, spriteBatch, drawColor, endColor, null, null, directioning: true, invertedDirection: true);
		}
		CalamityGlobalNPC.DrawGlowmask(base.NPC, spriteBatch, null, invertedDirection: true);
		return false;
	}

	public override bool CheckDead()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (!Dying)
		{
			Dying = true;
			base.NPC.active = true;
			base.NPC.life = 1;
			base.NPC.dontTakeDamage = true;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			return false;
		}
		return Dying;
	}

	public override void OnKill()
	{
		DownedBossSystem.downedNuclearTerror = true;
		CalamityNetcode.SyncWorld();
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 10; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 300);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<GammaHeart>(), 3);
		npcLoot.Add(ModContent.ItemType<PhosphorescentGauntlet>(), 3);
		npcLoot.Add(ModContent.ItemType<NuclearTerrorTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<NuclearTerrorRelic>());
	}
}
