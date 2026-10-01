using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod.NPCs.AcidRain;

[AutoloadBossHead]
public class CragmawMire : ModNPC
{
	public enum CragmawAttackState
	{
		ReleaseBurstsOfSpikes,
		AcidExplosionSlam,
		DigAndReleaseLaser,
		CreateVibeCheckTether
	}

	public static Asset<Texture2D> Phase2Texture;

	public Player Target => Main.player[base.NPC.target];

	public CragmawAttackState CurrentAttack
	{
		get
		{
			return (CragmawAttackState)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = (float)value;
		}
	}

	public ref float AttackTimer => ref base.NPC.ai[1];

	public bool HasMadeShellBreakGore
	{
		get
		{
			return base.NPC.localAI[0] == 1f;
		}
		set
		{
			base.NPC.localAI[0] = value.ToInt();
		}
	}

	public bool InPhase2
	{
		get
		{
			float phase2CeilingRatio = (CalamityWorld.revenge ? 0.85f : 0.7f);
			if ((float)base.NPC.life / (float)base.NPC.lifeMax < phase2CeilingRatio)
			{
				return DownedBossSystem.downedPolterghast;
			}
			return false;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		if (!Main.dedServ)
		{
			Phase2Texture = ModContent.Request<Texture2D>(Texture + "2", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.width = 68;
		base.NPC.height = 54;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.damage = 66;
		base.NPC.lifeMax = 4000;
		base.NPC.defense = 25;
		base.NPC.value = Item.buyPrice(0, 2);
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.damage = 160;
			base.NPC.lifeMax = 80630;
			base.NPC.defense = 80;
			base.NPC.value *= 10f;
		}
		base.NPC.behindTiles = true;
		base.NPC.knockBackResist = 0f;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CragmawMire")
		});
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.dontTakeDamage = false;
		if (InPhase2 && !HasMadeShellBreakGore)
		{
			SoundEngine.PlaySound(in SoundID.DD2_ExplosiveTrapExplode, base.NPC.Center);
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, -Vector2.UnitY.RotatedByRandom(0.4000000059604645) * 4f, base.Mod.Find<ModGore>("CragmawMireP1Gore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, -Vector2.UnitY.RotatedByRandom(0.4000000059604645) * 4f, base.Mod.Find<ModGore>("CragmawMireP1Gore2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, -Vector2.UnitY.RotatedByRandom(0.4000000059604645) * 4f, base.Mod.Find<ModGore>("CragmawMireP1Gore3").Type, base.NPC.scale);
			}
			HasMadeShellBreakGore = true;
		}
		switch (CurrentAttack)
		{
		case CragmawAttackState.ReleaseBurstsOfSpikes:
			DoBehavior_ReleaseBurstsOfSpikes();
			break;
		case CragmawAttackState.AcidExplosionSlam:
			DoBehavior_AcidExplosionSlam();
			break;
		case CragmawAttackState.DigAndReleaseLaser:
			DoBehavior_DigAndReleaseLaser();
			break;
		case CragmawAttackState.CreateVibeCheckTether:
			DoBehavior_CreateVibeCheckTether();
			break;
		}
		AttackTimer++;
	}

	public void DoBehavior_ReleaseBurstsOfSpikes()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		int spikesPerBurst = 5;
		float burstSpeed = 8f;
		int burstShootRate = 92;
		if (DownedBossSystem.downedPolterghast)
		{
			spikesPerBurst += 3;
			burstSpeed += 3.25f;
			burstShootRate -= 20;
		}
		if (InPhase2)
		{
			spikesPerBurst++;
			burstShootRate -= 10;
		}
		if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height - 6))
		{
			base.NPC.position.Y -= 4f;
		}
		float wrappedAttackTimer = AttackTimer % (float)burstShootRate;
		if (wrappedAttackTimer > (float)burstShootRate * 0.65f)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust rock = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Circular(75f, 75f), 1);
				rock.color = Color.Yellow;
				rock.velocity = (base.NPC.Center - rock.position) * Main.rand.NextFloat(0.06f, 0.09f);
				rock.scale = Main.rand.NextFloat(1f, 1.3f);
				rock.noGravity = true;
			}
		}
		if (wrappedAttackTimer == (float)burstShootRate - 1f)
		{
			SoundEngine.PlaySound(in SoundID.Item92, base.NPC.Center);
			if (Main.netMode != 1)
			{
				int damage = ((!DownedBossSystem.downedPolterghast) ? (Main.masterMode ? 22 : (Main.expertMode ? 26 : 33)) : (Main.masterMode ? 35 : (Main.expertMode ? 42 : 52)));
				float shootOffsetAngle = Main.rand.NextFloat((float)Math.PI * 2f);
				for (int j = 0; j < spikesPerBurst; j++)
				{
					Vector2 spikeShootVelocity = ((float)Math.PI * 2f * (float)j / (float)spikesPerBurst + shootOffsetAngle).ToRotationVector2() * burstSpeed;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + spikeShootVelocity * 1.6f, spikeShootVelocity, ModContent.ProjectileType<CragmawSpike>(), damage, 0f);
				}
			}
		}
		if (AttackTimer > 240f)
		{
			SelectNextAttack();
		}
	}

	public void DoBehavior_AcidExplosionSlam()
	{
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		float opacityFadeoutIncrement = 0.03f;
		float verticalTeleportOffset = 415f;
		float slamAcceleration = 0.375f;
		float maxSlamSpeed = 19f;
		int slamCount = 2;
		if (DownedBossSystem.downedPolterghast)
		{
			opacityFadeoutIncrement += 0.03f;
			slamAcceleration += 0.07f;
			maxSlamSpeed += 5f;
		}
		if (InPhase2)
		{
			opacityFadeoutIncrement += 0.01f;
			slamAcceleration += 0.03f;
			maxSlamSpeed += 1.15f;
		}
		ref float attackSubstate = ref base.NPC.ai[2];
		ref float slamCounter = ref base.NPC.ai[3];
		switch ((int)attackSubstate)
		{
		case 0:
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			base.NPC.position.Y += 3f;
			base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity - opacityFadeoutIncrement * 0.6f, 0f, 1f);
			base.NPC.dontTakeDamage = true;
			if (base.NPC.Opacity <= 0f)
			{
				attackSubstate = 1f;
				base.NPC.Center = Target.Center - Vector2.UnitY * verticalTeleportOffset;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
			break;
		case 1:
			base.NPC.noGravity = true;
			base.NPC.noTileCollide = true;
			base.NPC.damage = 0;
			base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity + opacityFadeoutIncrement, 0f, 1f);
			if (base.NPC.Opacity >= 1f)
			{
				attackSubstate = 2f;
				AttackTimer = 0f;
				base.NPC.velocity = Vector2.UnitY * maxSlamSpeed * 0.251f;
				base.NPC.netUpdate = true;
			}
			break;
		case 2:
			base.NPC.noGravity = true;
			base.NPC.noTileCollide = true;
			base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y + slamAcceleration, 0f, maxSlamSpeed);
			if ((base.NPC.Bottom.Y > Target.Bottom.Y && Collision.SolidCollision(base.NPC.BottomLeft, base.NPC.width, 1)) || Collision.WetCollision(base.NPC.BottomLeft, base.NPC.width, 1) || AttackTimer > 180f)
			{
				base.NPC.velocity = Vector2.Zero;
				if (AttackTimer == 1f)
				{
					SoundEngine.PlaySound(in SoundID.DD2_ExplosiveTrapExplode, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int nukeDamage = ((!DownedBossSystem.downedPolterghast) ? (Main.masterMode ? 25 : (Main.expertMode ? 30 : 38)) : (Main.masterMode ? 49 : (Main.expertMode ? 58 : 72)));
						int dropletDamage = (int)((float)nukeDamage * 0.6f);
						int explosion = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<CragmawExplosion>(), nukeDamage, 0f);
						if (Main.projectile.IndexInRange(explosion))
						{
							Main.projectile[explosion].Bottom = base.NPC.Bottom + Vector2.UnitY * 4f;
						}
						for (int i = 0; i < 12; i++)
						{
							Vector2 dropletVelocity = -Vector2.UnitY.RotatedByRandom(0.7099999785423279) * Main.rand.NextFloat(8f, 12f);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, dropletVelocity, ModContent.ProjectileType<CragmawAcidDrop>(), dropletDamage, 0f);
						}
					}
				}
				AttackTimer++;
			}
			else
			{
				AttackTimer = 0f;
			}
			if (AttackTimer > 35f)
			{
				slamCounter++;
				if (slamCounter >= (float)slamCount)
				{
					SelectNextAttack();
					break;
				}
				attackSubstate = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
	}

	public void DoBehavior_DigAndReleaseLaser()
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		int digReapperTime = 30;
		float digReapperSpeed = 4f;
		int chargeupTelegraphTime = 60;
		float opacityFadeoutIncrement = 0.025f;
		if (DownedBossSystem.downedPolterghast)
		{
			opacityFadeoutIncrement += 0.025f;
			chargeupTelegraphTime -= 15;
		}
		if (InPhase2)
		{
			opacityFadeoutIncrement += 0.01f;
		}
		ref float attackSubstate = ref base.NPC.ai[2];
		switch ((int)attackSubstate)
		{
		case 0:
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			base.NPC.position.Y += 3f;
			base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity - opacityFadeoutIncrement * 0.6f, 0f, 1f);
			base.NPC.dontTakeDamage = true;
			if (base.NPC.Opacity <= 0f)
			{
				attackSubstate = 1f;
				if (WorldUtils.Find(Target.Center.ToTileCoordinates(), Searches.Chain(new Searches.Down(1000), new CustomConditions.SolidOrPlatform()), out var teleportPosition))
				{
					base.NPC.Bottom = teleportPosition.ToWorldCoordinates() + Vector2.UnitY * (float)digReapperTime * digReapperSpeed;
				}
				else
				{
					SelectNextAttack();
				}
				AttackTimer = 0f;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
			break;
		case 1:
			if (AttackTimer < (float)digReapperTime)
			{
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				base.NPC.position.Y -= digReapperSpeed;
			}
			if (AttackTimer == (float)digReapperTime)
			{
				base.NPC.netUpdate = true;
			}
			base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity + opacityFadeoutIncrement, 0f, 1f);
			if (AttackTimer > (float)digReapperTime && AttackTimer < (float)(digReapperTime + chargeupTelegraphTime))
			{
				Dust dust = Dust.NewDustPerfect(base.NPC.Center, 267);
				dust.position -= Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (float)base.NPC.height * 0.4f;
				dust.velocity = -Vector2.UnitY.RotatedByRandom(0.3100000023841858) * Main.rand.NextFloat(2f, 6f);
				dust.color = Color.Lerp(Color.Green, Color.Yellow, Main.rand.NextFloat());
				dust.scale = Main.rand.NextFloat(0.95f, 1.35f);
				dust.noGravity = true;
			}
			if (AttackTimer == (float)(digReapperTime + chargeupTelegraphTime))
			{
				SoundEngine.PlaySound(in SoundID.Zombie104, base.NPC.Center);
				for (int i = 0; i < 40; i++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Circular(75f, 75f), 267);
					dust2.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 6f);
					dust2.color = Color.Lerp(Color.Green, Color.Yellow, Main.rand.NextFloat());
					dust2.scale = Main.rand.NextFloat(1.1f, 1.45f);
					dust2.noGravity = true;
				}
				if (Main.netMode != 1)
				{
					int laserbeamDamage = ((!DownedBossSystem.downedPolterghast) ? (Main.masterMode ? 27 : (Main.expertMode ? 32 : 40)) : (Main.masterMode ? 81 : (Main.expertMode ? 96 : 120)));
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, -Vector2.UnitY, ModContent.ProjectileType<CragmawBeam>(), laserbeamDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
				}
			}
			if (AttackTimer == (float)(digReapperTime + chargeupTelegraphTime + 120))
			{
				SelectNextAttack();
			}
			break;
		}
	}

	public void DoBehavior_CreateVibeCheckTether()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1 && AttackTimer == 30f)
		{
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, -Vector2.UnitY * 4f, ModContent.ProjectileType<CragmawVibeCheckChain>(), 0, 0f, Main.myPlayer, base.NPC.whoAmI, base.NPC.target);
		}
		if (AttackTimer > 390f)
		{
			SelectNextAttack();
		}
	}

	public void SelectNextAttack()
	{
		AttackTimer = 0f;
		base.NPC.ai[2] = 0f;
		base.NPC.ai[3] = 0f;
		CragmawAttackState oldAttack = CurrentAttack;
		WeightedRandom<CragmawAttackState> attackSelector = new WeightedRandom<CragmawAttackState>(Main.rand);
		attackSelector.Add(CragmawAttackState.ReleaseBurstsOfSpikes);
		attackSelector.Add(CragmawAttackState.AcidExplosionSlam);
		attackSelector.Add(CragmawAttackState.DigAndReleaseLaser);
		if (InPhase2)
		{
			attackSelector.Add(CragmawAttackState.CreateVibeCheckTether);
		}
		do
		{
			CurrentAttack = attackSelector.Get();
		}
		while (oldAttack == CurrentAttack);
		base.NPC.netUpdate = true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.85f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Main.EntitySpriteDraw(InPhase2 ? Phase2Texture.Value : TextureAssets.Npc[base.Type].Value, base.NPC.Center - screenPos, base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter % 6.0 == 5.0)
		{
			base.NPC.frame.Y += frameHeight;
		}
		if (base.NPC.frame.Y >= frameHeight * 2)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, -Vector2.UnitY.RotatedByRandom(0.4000000059604645) * 4f, base.Mod.Find<ModGore>("CragmawMireP2Gore").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, -Vector2.UnitY.RotatedByRandom(0.4000000059604645) * 4f, base.Mod.Find<ModGore>("CragmawMireP2Gore2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, -Vector2.UnitY.RotatedByRandom(0.4000000059604645) * 4f, base.Mod.Find<ModGore>("CragmawMireP2Gore3").Type, base.NPC.scale);
		}
	}

	public override void OnKill()
	{
		DownedBossSystem.downedCragmawMire = true;
		CalamityNetcode.SyncWorld();
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
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(() => DownedBossSystem.downedPolterghast);
		mainRule.Add(ModContent.ItemType<NuclearFuelRod>(), 10, 1, 1, !DownedBossSystem.downedPolterghast);
		mainRule.Add(ModContent.ItemType<SpentFuelContainer>(), 10, 1, 1, !DownedBossSystem.downedPolterghast);
		mainRule.AddFail(ModContent.ItemType<NuclearFuelRod>(), 1, 1, 1, DownedBossSystem.downedPolterghast);
		mainRule.AddFail(ModContent.ItemType<SpentFuelContainer>(), 1, 1, 1, DownedBossSystem.downedPolterghast);
		npcLoot.Add(ModContent.ItemType<CragmawMireTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<CragmawMireRelic>());
	}
}
