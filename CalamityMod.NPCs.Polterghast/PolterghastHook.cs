using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Polterghast;

public class PolterghastHook : ModNPC
{
	private int despawnTimer = 300;

	private bool phase2;

	public static Asset<Texture2D> GlowTexture;

	public static Asset<Texture2D> ChainTexture;

	public static int ShotDamage = 55;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 2;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			ChainTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Polterghast/PolterghastChain", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.lifeMax = 50000;
		base.NPC.dontTakeDamage = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit34;
		base.NPC.DeathSound = SoundID.NPCDeath39;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(phase2);
		writer.Write(despawnTimer);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		phase2 = reader.ReadBoolean();
		despawnTimer = reader.ReadInt32();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0f, 0.3f, 0.3f);
		bool speedBoost = false;
		bool despawnBoost = false;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (CalamityGlobalNPC.ghostBoss < 0 || !Main.npc[CalamityGlobalNPC.ghostBoss].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		Player player = Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target];
		if (!player.active || player.dead)
		{
			speedBoost = true;
			despawnBoost = true;
		}
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		bool chargePhase = Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] >= chargePhaseGateValue - 60f;
		float lifeRatio = (float)Main.npc[CalamityGlobalNPC.ghostBoss].life / (float)Main.npc[CalamityGlobalNPC.ghostBoss].lifeMax;
		float tileEnrageMult = Main.npc[CalamityGlobalNPC.ghostBoss].ai[3];
		if (CalamityGlobalNPC.ghostBoss != -1 && !player.ZoneDungeon && (double)player.position.Y < Main.worldSurface * 16.0 && !BossRushEvent.BossRushActive)
		{
			despawnTimer--;
			if (despawnTimer <= 0)
			{
				despawnBoost = true;
			}
			base.NPC.localAI[0] -= 6f;
			speedBoost = true;
		}
		else
		{
			despawnTimer++;
		}
		bool phase3 = lifeRatio < (death ? 0.6f : (revenge ? 0.5f : (expertMode ? 0.35f : 0.2f)));
		phase2 = lifeRatio < (death ? 0.9f : (revenge ? 0.8f : (expertMode ? 0.65f : 0.5f))) && !phase3;
		if (phase2)
		{
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.TargetClosest();
			}
			if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
			{
				base.NPC.TargetClosest();
			}
			Movement(phase2, expertMode, revenge, death, speedBoost, despawnBoost, lifeRatio, tileEnrageMult, player);
			Vector2 hookPosition = base.NPC.Center;
			float targetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - hookPosition.X;
			float targetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - hookPosition.Y;
			float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
			if (chargePhase)
			{
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				return;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[3] == 0f)
			{
				if (base.NPC.ai[2] > (Main.getGoodWorld ? 40f : 120f))
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 1f;
					base.NPC.netUpdate = true;
				}
				return;
			}
			if (base.NPC.ai[2] > 40f)
			{
				base.NPC.ai[3] = 0f;
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == 20f && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 192f)
			{
				float num = 10f * tileEnrageMult;
				int type = ModContent.ProjectileType<PhantomHookShot>();
				targetDistance = num / targetDistance;
				targetX *= targetDistance;
				targetY *= targetDistance;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), hookPosition.X, hookPosition.Y, targetX, targetY, type, ShotDamage, 0f, Main.myPlayer);
			}
		}
		else
		{
			Movement(phase2, expertMode, revenge, death, speedBoost, despawnBoost, lifeRatio, tileEnrageMult, player);
		}
	}

	private void Movement(bool phase2, bool expertMode, bool revenge, bool death, bool speedBoost, bool despawnBoost, float lifeRatio, float tileEnrageMult, Player player)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		float colorChangeTime = 180f;
		float changeColorGateValue = chargePhaseGateValue - colorChangeTime;
		bool chargePhase = Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] >= chargePhaseGateValue - 60f;
		if (phase2)
		{
			float playerXDirection = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
			float playerYDirection = Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y;
			base.NPC.rotation = (float)Math.Atan2(playerYDirection, playerXDirection) + (float)Math.PI / 2f;
		}
		if (Main.netMode == 1)
		{
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[0] = (int)(base.NPC.Center.X / 16f);
			}
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = (int)(base.NPC.Center.X / 16f);
			}
		}
		if (Main.netMode != 1)
		{
			if (base.NPC.ai[0] == 0f || base.NPC.ai[1] == 0f)
			{
				base.NPC.localAI[0] = 0f;
			}
			if (chargePhase)
			{
				base.NPC.localAI[0] -= 10f;
			}
			else
			{
				float shootBoost = (death ? (4f * (1f - lifeRatio)) : (2f * (1f - lifeRatio)));
				base.NPC.localAI[0] -= 1f + shootBoost * tileEnrageMult;
				if (expertMode)
				{
					base.NPC.localAI[0] -= Vector2.Distance(base.NPC.Center, player.Center) * 0.002f;
				}
				if (Main.npc[CalamityGlobalNPC.ghostBoss].ai[2] >= changeColorGateValue)
				{
					base.NPC.localAI[0] -= 3f;
				}
				if (speedBoost)
				{
					base.NPC.localAI[0] -= 6f;
				}
			}
			if (!despawnBoost && base.NPC.localAI[0] <= 0f && base.NPC.ai[0] != 0f)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && (n.velocity.X != 0f || n.velocity.Y != 0f))
					{
						base.NPC.localAI[0] = 180f;
					}
				}
			}
			if (base.NPC.localAI[0] <= 0f)
			{
				base.NPC.localAI[0] = 450f;
				bool canMoveToTile = false;
				int increment = 0;
				while (!canMoveToTile && increment <= 1000)
				{
					increment++;
					int playerTileX = (int)(player.Center.X / 16f);
					int playerTileY = (int)(player.Center.Y / 16f);
					if (base.NPC.ai[0] == 0f)
					{
						playerTileX = (int)((player.Center.X + Main.npc[CalamityGlobalNPC.ghostBoss].Center.X) / 32f);
						playerTileY = (int)((player.Center.Y + Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y) / 32f);
					}
					if (despawnBoost)
					{
						playerTileX = (int)Main.npc[CalamityGlobalNPC.ghostBoss].position.X / 16;
						playerTileY = (int)(Main.npc[CalamityGlobalNPC.ghostBoss].position.Y + 400f) / 16;
					}
					int randPlayerRadius = 20;
					randPlayerRadius += (int)(100f * ((float)increment / 1000f));
					int randTileX = playerTileX + Main.rand.Next(-randPlayerRadius, randPlayerRadius + 1);
					int randTileY = playerTileY + Main.rand.Next(-randPlayerRadius, randPlayerRadius + 1);
					try
					{
						if ((WorldGen.SolidTile(randTileX, randTileY) || Main.tile[randTileX, randTileY].WallType > 0) | chargePhase)
						{
							canMoveToTile = true;
							base.NPC.ai[0] = randTileX;
							base.NPC.ai[1] = randTileY;
							base.NPC.localAI[1] = Vector2.Distance(base.NPC.Center, player.Center) * 0.01f;
							base.NPC.netUpdate = true;
						}
					}
					catch
					{
					}
				}
			}
		}
		if (base.NPC.ai[0] > 0f && base.NPC.ai[1] > 0f)
		{
			float velocityBoost = (death ? (4f * (1f - lifeRatio)) : (2f * (1f - lifeRatio)));
			float velocity = (8f + velocityBoost) * tileEnrageMult;
			if (expertMode)
			{
				velocity += base.NPC.localAI[1];
			}
			if (revenge)
			{
				velocity++;
			}
			if (speedBoost)
			{
				velocity *= 2f;
			}
			if (despawnBoost)
			{
				velocity *= 2f;
			}
			Vector2 hookCenter = default(Vector2);
			((Vector2)(ref hookCenter))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float hookXDest = base.NPC.ai[0] * 16f - 8f - hookCenter.X;
			float hookYDest = base.NPC.ai[1] * 16f - 8f - hookCenter.Y;
			float hookDestination = (float)Math.Sqrt(hookXDest * hookXDest + hookYDest * hookYDest);
			if (hookDestination < 12f + velocity)
			{
				base.NPC.velocity.X = hookXDest;
				base.NPC.velocity.Y = hookYDest;
			}
			else
			{
				hookDestination = velocity / hookDestination;
				base.NPC.velocity.X = hookXDest * hookDestination;
				base.NPC.velocity.Y = hookYDest * hookDestination;
			}
			if (!phase2)
			{
				Vector2 hookCenterPassive = default(Vector2);
				((Vector2)(ref hookCenterPassive))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
				float polterDirectionX = Main.npc[CalamityGlobalNPC.ghostBoss].Center.X - hookCenterPassive.X;
				float polterDirectionY = Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y - hookCenterPassive.Y;
				base.NPC.rotation = (float)Math.Atan2(polterDirectionY, polterDirectionX) - 1.57f;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.ghostBoss < 0 || !base.NPC.active || base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		Color lightRed = default(Color);
		((Color)(ref lightRed))._002Ector(255, 100, 100, 255);
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		float timeToReachFullColor = 120f;
		float colorChangeTime = 180f;
		float changeColorGateValue = chargePhaseGateValue - colorChangeTime;
		if (Main.npc[CalamityGlobalNPC.ghostBoss].active && !phase2)
		{
			Vector2 center = base.NPC.Center;
			float bossCenterX = Main.npc[CalamityGlobalNPC.ghostBoss].Center.X - center.X;
			float bossCenterY = Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y - center.Y;
			float chainRotation = (float)Math.Atan2(bossCenterY, bossCenterX) - 1.57f;
			bool draw = true;
			while (draw)
			{
				int chainWidth = 20;
				int chainHeight = 52;
				float polterDistance = (float)Math.Sqrt(bossCenterX * bossCenterX + bossCenterY * bossCenterY);
				if (polterDistance < (float)chainHeight)
				{
					chainWidth = (int)polterDistance - chainHeight + chainWidth;
					draw = false;
				}
				polterDistance = (float)chainWidth / polterDistance;
				bossCenterX *= polterDistance;
				bossCenterY *= polterDistance;
				center.X += bossCenterX;
				center.Y += bossCenterY;
				bossCenterX = Main.npc[CalamityGlobalNPC.ghostBoss].Center.X - center.X;
				bossCenterY = Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y - center.Y;
				Color cyanLerpColor = Color.Lerp(Color.White, Color.Cyan, 0.5f);
				if (Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] > changeColorGateValue)
				{
					cyanLerpColor = Color.Lerp(cyanLerpColor, lightRed, MathHelper.Clamp((Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] - changeColorGateValue) / timeToReachFullColor, 0f, 1f));
				}
				Main.spriteBatch.Draw(ChainTexture.Value, new Vector2(center.X - screenPos.X, center.Y - screenPos.Y), (Rectangle?)new Rectangle(0, 0, ChainTexture.Value.Width, chainWidth), cyanLerpColor, chainRotation, new Vector2((float)ChainTexture.Value.Width * 0.5f, (float)ChainTexture.Value.Height * 0.5f), 1f, (SpriteEffects)0, 0f);
			}
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		int afterimageAmt = 5;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color cyanLerp2 = Color.Lerp(Color.White, Color.Cyan, 0.5f);
		if (Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] > changeColorGateValue)
		{
			cyanLerp2 = Color.Lerp(cyanLerp2, lightRed, MathHelper.Clamp((Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] - changeColorGateValue) / timeToReachFullColor, 0f, 1f));
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Color otherAfterimageColor = cyanLerp2;
				otherAfterimageColor = Color.Lerp(otherAfterimageColor, Color.White, 0.5f);
				otherAfterimageColor = base.NPC.GetAlpha(otherAfterimageColor);
				otherAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				Vector2 otherAfterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				otherAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				otherAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, otherAfterimagePos, (Rectangle?)base.NPC.frame, otherAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, cyanLerp2, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		if (phase2)
		{
			if (base.NPC.ai[3] == 0f)
			{
				if (base.NPC.frame.Y < 1)
				{
					base.NPC.frameCounter++;
					if (base.NPC.frameCounter > 4.0)
					{
						base.NPC.frameCounter = 0.0;
						base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
					}
				}
			}
			else if (base.NPC.frame.Y > 0)
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 4.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y = base.NPC.frame.Y - frameHeight;
				}
			}
		}
		else if (base.NPC.velocity.X == 0f && base.NPC.velocity.Y == 0f)
		{
			if (base.NPC.frame.Y < 1)
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 4.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				}
			}
		}
		else if (base.NPC.frame.Y > 0)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 4.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = base.NPC.frame.Y - frameHeight;
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, hit.HitDirection, -1f);
			}
		}
	}
}
