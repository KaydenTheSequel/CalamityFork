using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ExoMechs.Thanatos;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(ThanatosHead))]
public class ThanatosBody2 : ModNPC
{
	public static int normalIconIndex;

	public static int vulnerableIconIndex;

	private bool vulnerable;

	public ThanatosSmokeParticleSet SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);

	private const float defaultLifeRatio = 5f;

	private int noContactDamageTimer;

	private const float timeToOpenAndFireLasers = 36f;

	private const float segmentCloseTimerDecrement = 0.2f;

	public static Asset<Texture2D> GlowTexture;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.ThanatosHead.DisplayName");

	public override void Load()
	{
		string normalIconPath = "CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosNormalBody2";
		string vulnerableIconPath = "CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosVulnerableBody2";
		normalIconIndex = CalamityMod.Instance.AddBossHeadTexture(normalIconPath);
		vulnerableIconIndex = CalamityMod.Instance.AddBossHeadTexture(vulnerableIconPath);
	}

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 5;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 150;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 90;
		base.NPC.height = 90;
		base.NPC.defense = 100;
		base.NPC.DR_NERD(0.9999f);
		base.NPC.Calamity().unbreakableDR = true;
		base.NPC.LifeMaxNERB(960000, 1150000, 600000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = CommonCalamitySounds.ExoDeathSound;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		base.NPC.chaseable = false;
		base.NPC.boss = true;
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void BossHeadSlot(ref int index)
	{
		if (Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[1] == 2f)
		{
			index = -1;
		}
		else if (vulnerable)
		{
			index = vulnerableIconIndex;
		}
		else
		{
			index = normalIconIndex;
		}
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = base.NPC.rotation;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(noContactDamageTimer);
		writer.Write(vulnerable);
		writer.Write(base.NPC.localAI[0]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.chaseable = reader.ReadBoolean();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		noContactDamageTimer = reader.ReadInt32();
		vulnerable = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_0f38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool shouldDespawn = !NPC.AnyNPCs(ModContent.NPCType<ThanatosHead>());
		if (!shouldDespawn)
		{
			if (base.NPC.ai[1] <= 0f)
			{
				shouldDespawn = true;
			}
			else if (Main.npc[(int)base.NPC.ai[1]].life <= 0)
			{
				shouldDespawn = true;
			}
		}
		if (shouldDespawn)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
			return;
		}
		vulnerable = false;
		NPC head = Main.npc[(int)base.NPC.ai[2]];
		CalamityGlobalNPC calamityGlobalNPC_Head = head.Calamity();
		bool invisiblePhase = calamityGlobalNPC_Head.newAI[1] == 2f;
		base.NPC.dontTakeDamage = Main.npc[(int)base.NPC.ai[2]].dontTakeDamage;
		if (!invisiblePhase)
		{
			if (Main.npc[(int)base.NPC.ai[1]].Opacity > 0.5f)
			{
				if (noContactDamageTimer > 0)
				{
					noContactDamageTimer--;
				}
				base.NPC.Opacity += 0.2f;
				if (base.NPC.Opacity > 1f)
				{
					base.NPC.Opacity = 1f;
				}
			}
			else
			{
				noContactDamageTimer = 185;
			}
		}
		else
		{
			noContactDamageTimer = 185;
			base.NPC.Opacity -= 0.05f;
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
		}
		int numSegments = 100;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		int otherExoMechsAlive = 0;
		bool exoPrimeAlive = false;
		bool exoTwinsAlive = false;
		if (CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].active)
		{
			otherExoMechsAlive++;
			exoPrimeAlive = true;
		}
		if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
		{
			otherExoMechsAlive++;
			exoTwinsAlive = true;
		}
		int num;
		int num2;
		if (!(lifeRatio < 0.4f))
		{
			if (otherExoMechsAlive == 0)
			{
				num = ((lifeRatio < 0.7f) ? 1 : 0);
				if (num != 0)
				{
					goto IL_02e4;
				}
			}
			else
			{
				num = 0;
			}
			num2 = 0;
			goto IL_02ec;
		}
		num = 1;
		goto IL_02e4;
		IL_02e4:
		num2 = ((otherExoMechsAlive == 0) ? 1 : 0);
		goto IL_02ec;
		IL_02ec:
		bool lastMechAlive = (byte)num2 != 0;
		float exoPrimeLifeRatio = 5f;
		float exoTwinsLifeRatio = 5f;
		if (exoPrimeAlive)
		{
			exoPrimeLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].lifeMax;
		}
		if (exoTwinsAlive)
		{
			exoTwinsLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].lifeMax;
		}
		bool otherMechIsBerserk = exoPrimeLifeRatio < 0.4f || exoTwinsLifeRatio < 0.4f;
		bool shouldGetBuffedByBerserkPhase = num != 0 && !otherMechIsBerserk;
		float fasterSegmentClosingVar = (lastMechAlive ? 0.2f : (shouldGetBuffedByBerserkPhase ? 0.1f : 0f));
		if ((calamityGlobalNPC_Head.newAI[0] == 0f || calamityGlobalNPC_Head.newAI[0] == 1f) && calamityGlobalNPC_Head.newAI[2] > 0f && !invisiblePhase)
		{
			if (base.NPC.Calamity().newAI[0] == 0f)
			{
				base.NPC.ai[3]++;
			}
			double numSegmentsAbleToFire = (death ? 36.0 : (revenge ? 34.0 : (expertMode ? 30.0 : 24.0)));
			if (shouldGetBuffedByBerserkPhase)
			{
				numSegmentsAbleToFire *= 1.25;
			}
			float segmentDivisor = (float)Math.Round((double)numSegments / numSegmentsAbleToFire);
			if (calamityGlobalNPC_Head.newAI[0] == 0f)
			{
				float divisor = (lastMechAlive ? 45f : (shouldGetBuffedByBerserkPhase ? 60f : 75f));
				if ((base.NPC.ai[3] % divisor == 0f && base.NPC.ai[0] % segmentDivisor == 0f) || base.NPC.Calamity().newAI[0] > 0f)
				{
					vulnerable = true;
					if (base.NPC.Calamity().newAI[1] == 0f)
					{
						base.NPC.Calamity().newAI[0]++;
						if (base.NPC.Calamity().newAI[0] >= 36f)
						{
							base.NPC.ai[3] = 0f;
							base.NPC.Calamity().newAI[1] = 1f;
							int[] whoAmIArray = new int[3];
							Vector2[] targetCenterArray = (Vector2[])(object)new Vector2[3];
							int numProjectiles = 0;
							float maxDistance = 2400f;
							ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
							while (enumerator.MoveNext())
							{
								Player plr = enumerator.Current;
								if (plr.dead)
								{
									continue;
								}
								Vector2 playerCenter = plr.Center;
								if (Vector2.Distance(playerCenter, base.NPC.Center) < maxDistance)
								{
									whoAmIArray[numProjectiles] = plr.whoAmI;
									targetCenterArray[numProjectiles] = playerCenter;
									if (++numProjectiles >= targetCenterArray.Length)
									{
										break;
									}
								}
							}
							SoundStyle style = CommonCalamitySounds.ExoLaserShootSound with
							{
								Volume = 0.1f * CommonCalamitySounds.ExoLaserShootSound.Volume
							};
							SoundEngine.PlaySound(in style, base.NPC.Center);
							for (int i = 0; i < numProjectiles; i++)
							{
								int type = ModContent.ProjectileType<ThanatosLaser>();
								if (Main.netMode != 1)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, targetCenterArray[i], type, ThanatosHead.LaserDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
								}
							}
						}
					}
					else
					{
						base.NPC.Calamity().newAI[0] -= 0.2f + fasterSegmentClosingVar;
						if (base.NPC.Calamity().newAI[0] <= 0f)
						{
							base.NPC.Calamity().newAI[0] = 0f;
							base.NPC.Calamity().newAI[1] = 0f;
						}
					}
				}
			}
			else
			{
				float divisor2 = base.NPC.ai[0] * (lastMechAlive ? 1f : (shouldGetBuffedByBerserkPhase ? 2f : 3f));
				if ((base.NPC.ai[3] == divisor2 && base.NPC.ai[0] % segmentDivisor == 0f) || base.NPC.Calamity().newAI[0] > 0f)
				{
					vulnerable = true;
					if (base.NPC.Calamity().newAI[1] == 0f)
					{
						base.NPC.Calamity().newAI[0]++;
						if (base.NPC.Calamity().newAI[0] >= 36f)
						{
							base.NPC.ai[3] = 0f;
							base.NPC.Calamity().newAI[1] = 1f;
							int[] whoAmIArray2 = new int[3];
							Vector2[] targetCenterArray2 = (Vector2[])(object)new Vector2[3];
							int numProjectiles2 = 0;
							float maxDistance2 = 2400f;
							ActiveEntityIterator<Player>.Enumerator enumerator2 = Main.ActivePlayers.GetEnumerator();
							while (enumerator2.MoveNext())
							{
								Player plr2 = enumerator2.Current;
								if (plr2.dead)
								{
									continue;
								}
								Vector2 playerCenter2 = plr2.Center;
								if (Vector2.Distance(playerCenter2, base.NPC.Center) < maxDistance2)
								{
									whoAmIArray2[numProjectiles2] = plr2.whoAmI;
									targetCenterArray2[numProjectiles2] = playerCenter2;
									if (++numProjectiles2 >= targetCenterArray2.Length)
									{
										break;
									}
								}
							}
							float predictionAmt = (death ? 20f : (revenge ? 18f : (expertMode ? 16f : 12f)));
							if (base.NPC.ai[0] % 3f == 0f)
							{
								predictionAmt *= 0.5f;
							}
							int type2 = ModContent.ProjectileType<ThanatosLaser>();
							SoundEngine.PlaySound(in CommonCalamitySounds.ExoLaserShootSound, base.NPC.Center);
							for (int j = 0; j < numProjectiles2; j++)
							{
								if (calamityGlobalNPC_Head.newAI[1] == 1f)
								{
									if (Main.netMode != 1)
									{
										Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, targetCenterArray2[j], type2, ThanatosHead.LaserDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
									}
									continue;
								}
								if (shouldGetBuffedByBerserkPhase && base.NPC.ai[0] % 3f == 0f && Main.netMode != 1)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, targetCenterArray2[j], type2, ThanatosHead.LaserDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
								}
								Vector2 projectileDestination = targetCenterArray2[j] + Main.player[whoAmIArray2[j]].velocity * predictionAmt;
								if (Main.netMode != 1)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileDestination, type2, ThanatosHead.LaserDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
								}
								projectileDestination = targetCenterArray2[j] - Main.player[whoAmIArray2[j]].velocity * predictionAmt;
								if (Main.netMode != 1)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileDestination, type2, ThanatosHead.LaserDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
								}
							}
						}
					}
					else
					{
						base.NPC.Calamity().newAI[0] -= 0.2f + fasterSegmentClosingVar;
						if (base.NPC.Calamity().newAI[0] <= 0f)
						{
							base.NPC.Calamity().newAI[0] = 0f;
							base.NPC.Calamity().newAI[1] = 0f;
						}
					}
				}
			}
		}
		else
		{
			if (base.NPC.ai[3] > 0f)
			{
				base.NPC.ai[3] = 0f;
			}
			base.NPC.Calamity().newAI[0] -= 0.2f + fasterSegmentClosingVar;
			if (base.NPC.Calamity().newAI[0] <= 0f)
			{
				base.NPC.Calamity().newAI[0] = 0f;
				base.NPC.Calamity().newAI[1] = 0f;
			}
			else
			{
				vulnerable = true;
			}
		}
		if (base.NPC.Calamity().newAI[2] == 0f)
		{
			noContactDamageTimer = 300;
		}
		if (base.NPC.Calamity().newAI[2] < 600f)
		{
			base.NPC.Calamity().newAI[2]++;
		}
		base.NPC.chaseable = vulnerable;
		base.NPC.Calamity().DR = (vulnerable ? 0.1f : 0.9999f);
		base.NPC.Calamity().unbreakableDR = !vulnerable;
		SmokeDrawer.ParticleSpawnRate = 9999999;
		if (vulnerable)
		{
			float volume = ((calamityGlobalNPC_Head.newAI[0] == 0f) ? 0.1f : 1f);
			if (base.NPC.localAI[0] == 0f)
			{
				SoundStyle style = ThanatosHead.VentSound with
				{
					Volume = volume * ThanatosHead.VentSound.Volume
				};
				SoundEngine.PlaySound(in style, base.NPC.Center);
			}
			base.NPC.localAI[0]++;
			float actualVentDuration = (lastMechAlive ? 90f : (shouldGetBuffedByBerserkPhase ? 120f : 180f));
			if (base.NPC.localAI[0] < actualVentDuration)
			{
				SmokeDrawer.BaseMoveRotation = base.NPC.rotation - (float)Math.PI / 2f;
				SmokeDrawer.ParticleSpawnRate = 10;
			}
		}
		else
		{
			base.NPC.localAI[0] = 0f;
		}
		SmokeDrawer.Update();
		Player player = Main.player[head.target];
		Vector2 npcCenter = base.NPC.Center;
		float targetCenterX = player.Center.X;
		float targetCenterY = player.Center.Y;
		targetCenterX = (int)(targetCenterX / 16f) * 16;
		targetCenterY = (int)(targetCenterY / 16f) * 16;
		npcCenter.X = (int)(npcCenter.X / 16f) * 16;
		npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
		targetCenterX -= npcCenter.X;
		targetCenterY -= npcCenter.Y;
		float newPosition = (float)Math.Sqrt(targetCenterX * targetCenterX + targetCenterY * targetCenterY);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				npcCenter = base.NPC.Center;
				targetCenterX = Main.npc[(int)base.NPC.ai[1]].Center.X - npcCenter.X;
				targetCenterY = Main.npc[(int)base.NPC.ai[1]].Center.Y - npcCenter.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(targetCenterY, targetCenterX) + (float)Math.PI / 2f;
			newPosition = (float)Math.Sqrt(targetCenterX * targetCenterX + targetCenterY * targetCenterY);
			newPosition = (newPosition - (float)base.NPC.width) / newPosition;
			targetCenterX *= newPosition;
			targetCenterY *= newPosition;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X += targetCenterX;
			base.NPC.position.Y += targetCenterY;
			if (targetCenterX < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (targetCenterX > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
		}
		bool speedUp = head.localAI[3] < 180f;
		float distanceFromTarget = Vector2.Distance(head.Center, speedUp ? (player.Center + new Vector2(0f, 2400f)) : player.Center);
		float increaseSpeedMult = 1f;
		float increaseSpeedGateValue = 600f;
		if (distanceFromTarget > increaseSpeedGateValue)
		{
			float distanceAmount = MathHelper.Clamp((distanceFromTarget - increaseSpeedGateValue) / (5600f - increaseSpeedGateValue), 0f, 1f);
			increaseSpeedMult = MathHelper.Lerp(1f, 3.5f, distanceAmount);
		}
		float baseVelocityMult = (shouldGetBuffedByBerserkPhase ? 0.15f : 0f) + (death ? 1.2f : (revenge ? 1.175f : (expertMode ? 1.15f : 1.1f)));
		float baseVelocity = 10f * baseVelocityMult;
		baseVelocity = ((!(player.dead | speedUp)) ? (baseVelocity * increaseSpeedMult) : (baseVelocity * 4f));
		if (Main.getGoodWorld)
		{
			baseVelocity *= 1.15f;
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		cooldownSlot = 1;
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.NPC.Center, targetHitbox.TopLeft());
		float hitboxTopRight = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float hitboxBotLeft = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float hitboxBotRight = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (hitboxTopRight < minDist)
		{
			minDist = hitboxTopRight;
		}
		if (hitboxBotLeft < minDist)
		{
			minDist = hitboxBotLeft;
		}
		if (hitboxBotRight < minDist)
		{
			minDist = hitboxBotRight;
		}
		if (minDist <= 50f && base.NPC.Opacity == 1f)
		{
			return noContactDamageTimer <= 0;
		}
		return false;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		if (base.NPC.Calamity().newAI[2] < 600f)
		{
			modifiers.SourceDamage *= 0.01f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (!Main.npc[(int)base.NPC.ai[2]].active || Main.npc[(int)base.NPC.ai[2]].life <= 0)
		{
			return;
		}
		CalamityGlobalNPC calamityGlobalNPC_Head = Main.npc[(int)base.NPC.ai[2]].Calamity();
		_ = calamityGlobalNPC_Head.newAI[1];
		if (calamityGlobalNPC_Head.newAI[0] == 0f || calamityGlobalNPC_Head.newAI[0] == 1f)
		{
			_ = calamityGlobalNPC_Head.newAI[2] > 0f;
		}
		else
			_ = 0;
		base.NPC.frameCounter++;
		if (base.NPC.Calamity().newAI[0] > 0f)
		{
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			int finalFrame = Main.npcFrameCount[base.Type] - 1;
			if (base.NPC.frame.Y > frameHeight * finalFrame)
			{
				base.NPC.frame.Y = frameHeight * finalFrame;
			}
		}
		else
		{
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frame.Y -= frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y < 0)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 center = base.NPC.Center - screenPos;
		center -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		center += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, vector, base.NPC.scale, (SpriteEffects)0, 0f);
		texture = GlowTexture.Value;
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, Color.White * base.NPC.Opacity, base.NPC.rotation, vector, base.NPC.scale, (SpriteEffects)0, 0f);
		SmokeDrawer.DrawSet(base.NPC.Center);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void ModifyTypeName(ref string typeName)
	{
		int index = (int)base.NPC.ai[2];
		if (index >= 0 && index < Main.maxNPCs && Main.npc[index] != null && Main.npc[index].type == ModContent.NPCType<ThanatosHead>() && Main.npc[index].ModNPC<ThanatosHead>().exoMechdusa)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.ThanatosHead.HekateName");
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			if (vulnerable)
			{
				base.NPC.soundDelay = 8;
				SoundEngine.PlaySound(in ThanatosHead.ThanatosHitSoundOpen, base.NPC.Center);
			}
			else
			{
				base.NPC.soundDelay = 3;
				SoundEngine.PlaySound(in ThanatosHead.ThanatosHitSoundClosed, base.NPC.Center);
			}
		}
		int baseDust = ((!vulnerable) ? 1 : 3);
		for (int k = 0; k < baseDust; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255));
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			}
			for (int j = 0; j < 20; j++)
			{
				int plasmaDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
				Main.dust[plasmaDust].noGravity = true;
				Dust obj = Main.dust[plasmaDust];
				obj.velocity *= 3f;
				plasmaDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
				Dust obj2 = Main.dust[plasmaDust];
				obj2.velocity *= 2f;
				Main.dust[plasmaDust].noGravity = true;
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ThanatosBody2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ThanatosBody2_2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ThanatosBody2_3").Type);
			}
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
