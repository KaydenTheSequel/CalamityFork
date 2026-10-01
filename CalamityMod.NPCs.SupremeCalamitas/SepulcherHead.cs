using System;
using System.IO;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

[LongDistanceNetSync]
public class SepulcherHead : ModNPC
{
	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/SepulcherDeath");

	private const int minLength = 51;

	private const int maxLength = 52;

	private float passedVar;

	private bool TailSpawned;

	private float AttackCooldown;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 30f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.54f;
		nPCBestiaryDrawModifiers.CustomTexturePath = "CalamityMod/ExtraTextures/Bestiary/Sepulcher_Bestiary";
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 80f;
		value.Position.Y -= 13f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 62;
		base.NPC.height = 64;
		base.NPC.defense = 0;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		calamityGlobalNPC.DR = 0.999999f;
		calamityGlobalNPC.unbreakableDR = true;
		base.NPC.lifeMax = (CalamityWorld.revenge ? 345000 : 300000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.scale *= (Main.expertMode ? 1.35f : 1.2f);
		base.NPC.scale *= 1.25f;
		base.NPC.alpha = 255;
		base.NPC.chaseable = false;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.canGhostHeal = false;
		base.NPC.netAlways = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<SupremeCalamitas>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Sepulcher")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.alpha);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.alpha = reader.ReadInt32();
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ded: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2f: Unknown result type (might be due to invalid IL or missing references)
		if (AttackCooldown > 0f)
		{
			AttackCooldown--;
		}
		CalamityGlobalNPC.SCalWorm = base.NPC.whoAmI;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (Main.netMode != 1)
		{
			if (!TailSpawned && base.NPC.ai[0] == 0f)
			{
				float rotationalOffset = 0f;
				int Previous = base.NPC.whoAmI;
				for (int i = 0; i < 52; i++)
				{
					int lol;
					if (i >= 0 && i < 51 && i % 2 == 1)
					{
						lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<SepulcherBodyEnergyBall>(), base.NPC.whoAmI);
						Main.npc[lol].localAI[0] += passedVar;
						passedVar += 36f;
					}
					else if (i >= 0 && i < 51)
					{
						lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<SepulcherBody>(), base.NPC.whoAmI);
						Main.npc[lol].localAI[3] = i;
					}
					else
					{
						lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<SepulcherTail>(), base.NPC.whoAmI);
					}
					if (i >= 3 && i % 4 == 0)
					{
						NPC segment = Main.npc[lol];
						int arm = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)segment.Center.X, (int)segment.Center.Y, ModContent.NPCType<SepulcherArm>(), lol);
						if (Main.npc.IndexInRange(arm))
						{
							Main.npc[arm].ai[0] = lol;
							Main.npc[arm].direction = 1;
							Main.npc[arm].rotation = rotationalOffset;
						}
						rotationalOffset += (float)Math.PI / 6f;
						arm = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)segment.Center.X, (int)segment.Center.Y, ModContent.NPCType<SepulcherArm>(), lol);
						if (Main.npc.IndexInRange(arm))
						{
							Main.npc[arm].ai[0] = lol;
							Main.npc[arm].direction = -1;
							Main.npc[arm].rotation = rotationalOffset + (float)Math.PI;
						}
						rotationalOffset += (float)Math.PI / 6f;
						rotationalOffset = MathHelper.WrapAngle(rotationalOffset);
					}
					Main.npc[lol].realLife = base.NPC.whoAmI;
					Main.npc[lol].ai[2] = base.NPC.whoAmI;
					Main.npc[lol].ai[1] = Previous;
					Main.npc[Previous].ai[0] = lol;
					Previous = lol;
				}
				TailSpawned = true;
			}
			if (!base.NPC.active && Main.dedServ)
			{
				NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
			}
		}
		if (Main.zenithWorld && !NPC.AnyNPCs(ModContent.NPCType<BrimstoneHeart>()))
		{
			CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
			calamityGlobalNPC.DR = 0.4f;
			calamityGlobalNPC.unbreakableDR = false;
			base.NPC.chaseable = true;
			base.NPC.DeathSound = DeathSound;
		}
		if (Main.player[base.NPC.target].dead || (!NPC.AnyNPCs(ModContent.NPCType<BrimstoneHeart>()) && !Main.zenithWorld) || CalamityGlobalNPC.SCal < 0 || !Main.npc[CalamityGlobalNPC.SCal].active)
		{
			base.NPC.TargetClosest(faceTarget: false);
			SoundEngine.PlaySound(in DeathSound, Main.player[base.NPC.target].Center);
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity + 0.165f, 0f, 1f);
		Vector2 segmentLocation = base.NPC.Center;
		float targetX = ((CalamityGlobalNPC.SCal < 0) ? 0f : (Main.npc[CalamityGlobalNPC.SCal].position.X + (float)(Main.player[base.NPC.target].width / 2)));
		float targetY = ((CalamityGlobalNPC.SCal < 0) ? 0f : (Main.npc[CalamityGlobalNPC.SCal].position.Y + (float)(Main.player[base.NPC.target].height / 2)));
		float sepMaxSpeed = 20f;
		float sepAcceleration = 0.175f + (0.37f - AttackCooldown * 0.0015f);
		float fasterMaxSpeed = sepMaxSpeed * 1.3f;
		float slowerMaxSpeed = sepMaxSpeed * 0.7f;
		float currentSpeed = ((Vector2)(ref base.NPC.velocity)).Length();
		if (currentSpeed > 0f)
		{
			if (currentSpeed > fasterMaxSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC = base.NPC;
				nPC.velocity *= fasterMaxSpeed;
			}
			else if (currentSpeed < slowerMaxSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC2 = base.NPC;
				nPC2.velocity *= slowerMaxSpeed;
			}
		}
		targetX = (int)(targetX / 16f) * 16;
		targetY = (int)(targetY / 16f) * 16;
		segmentLocation.X = (int)(segmentLocation.X / 16f) * 16;
		segmentLocation.Y = (int)(segmentLocation.Y / 16f) * 16;
		targetX -= segmentLocation.X;
		targetY -= segmentLocation.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		float absoluteTargetX = Math.Abs(targetX);
		float absoluteTargetY = Math.Abs(targetY);
		float timeToReachTarget = sepMaxSpeed / targetDistance;
		targetX *= timeToReachTarget;
		targetY *= timeToReachTarget;
		if ((base.NPC.velocity.X > 0f && targetX > 0f) || (base.NPC.velocity.X < 0f && targetX < 0f) || (base.NPC.velocity.Y > 0f && targetY > 0f) || (base.NPC.velocity.Y < 0f && targetY < 0f))
		{
			if (base.NPC.velocity.X < targetX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + sepAcceleration;
			}
			else if (base.NPC.velocity.X > targetX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - sepAcceleration;
			}
			if (base.NPC.velocity.Y < targetY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + sepAcceleration;
			}
			else if (base.NPC.velocity.Y > targetY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - sepAcceleration;
			}
			if ((double)Math.Abs(targetY) < (double)sepMaxSpeed * 0.2 && ((base.NPC.velocity.X > 0f && targetX < 0f) || (base.NPC.velocity.X < 0f && targetX > 0f)))
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + sepAcceleration * 2f;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - sepAcceleration * 2f;
				}
			}
			if ((double)Math.Abs(targetX) < (double)sepMaxSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && targetY < 0f) || (base.NPC.velocity.Y < 0f && targetY > 0f)))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + sepAcceleration * 2f;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - sepAcceleration * 2f;
				}
			}
		}
		else if (absoluteTargetX > absoluteTargetY)
		{
			if (base.NPC.velocity.X < targetX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + sepAcceleration * 1.1f;
			}
			else if (base.NPC.velocity.X > targetX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - sepAcceleration * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)sepMaxSpeed * 0.5)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + sepAcceleration;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - sepAcceleration;
				}
			}
		}
		else
		{
			if (base.NPC.velocity.Y < targetY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + sepAcceleration * 1.1f;
			}
			else if (base.NPC.velocity.Y > targetY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - sepAcceleration * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)sepMaxSpeed * 0.5)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + sepAcceleration;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - sepAcceleration;
				}
			}
		}
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
		if (!(Vector2.Distance(base.NPC.Center, Main.npc[CalamityGlobalNPC.SCal].Center) <= 110f) || !(AttackCooldown <= 0f))
		{
			return;
		}
		AttackCooldown = 150f;
		if (Main.netMode != 1)
		{
			int type = ModContent.ProjectileType<BrimstoneBarrage>();
			int totalProjectiles = 30;
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			float velocity = 1f;
			float projectileVelocityToPass = 15f;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(0f - velocity, 0f - velocity));
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 projectileVelocity = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity, type, SupremeCalamitas.DartDamage, 0f, Main.myPlayer, 0f, 3f, projectileVelocityToPass);
			}
			base.NPC.netUpdate = true;
		}
		GeneralParticleHandler.SpawnParticle(new BloomParticle(base.NPC.Center, Vector2.Zero, Color.Red, 0.1f, 0.9f, 30, fade: false));
		GeneralParticleHandler.SpawnParticle(new BloomParticle(base.NPC.Center, Vector2.Zero, Color.White, 0.1f, 0.8f, 30, fade: false));
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Red, new Vector2(2f, 2f), 0f, 0f, 0.9f, 25));
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DeadSunRicochet");
		style.Pitch = -0.65f;
		style.Volume = 1.8f;
		SoundEngine.PlaySound(in style, base.NPC.Center);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0 && !base.NPC.Calamity().unbreakableDR)
		{
			base.NPC.soundDelay = Main.rand.Next(5, 8);
			SoundEngine.PlaySound(in SoundID.DD2_SkeletonHurt, base.NPC.Center);
		}
		if (base.NPC.life > 0 || Main.dedServ)
		{
			return;
		}
		for (int i = 1; i <= 3; i++)
		{
			Vector2 goreSpawnPosition = base.NPC.Center;
			if (i == 2)
			{
				goreSpawnPosition += base.NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(0.7853981852531433) * 16f;
			}
			if (i == 3)
			{
				goreSpawnPosition += base.NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.7853981852531433) * 16f;
			}
			Gore.NewGorePerfect(base.NPC.GetSource_Death(), goreSpawnPosition, base.NPC.velocity, base.Mod.Find<ModGore>($"SepulcherHead_Gore{i}").Type, base.NPC.scale);
		}
	}
}
