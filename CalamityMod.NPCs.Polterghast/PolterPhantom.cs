using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Polterghast;

[AutoloadBossHead]
public class PolterPhantom : ModNPC
{
	private int despawnTimer = 600;

	private bool reachedChargingPoint;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Hide = true;
		NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(base.Type, bestiaryData);
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 180;
		base.NPC.width = 90;
		base.NPC.height = 120;
		base.NPC.defense = 45;
		base.NPC.DR_NERD(0.1f);
		base.NPC.LifeMaxNERB(62500, 75000, 60000);
		if (Main.zenithWorld)
		{
			base.NPC.lifeMax *= 4;
		}
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.Opacity = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.HitSound = SoundID.NPCHit36;
		base.NPC.DeathSound = SoundID.NPCDeath39;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.ghostBoss.WithinBounds(Main.maxNPCs) && Main.npc[CalamityGlobalNPC.ghostBoss].active && Main.npc[CalamityGlobalNPC.ghostBoss].HasValidTarget && base.NPC.Calamity().newAI[3] == 0f)
		{
			rotation = (Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center - base.NPC.Center).ToRotation() + (float)Math.PI / 2f;
		}
		else
		{
			rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(despawnTimer);
		writer.Write(reachedChargingPoint);
		CalamityGlobalNPC cgn = base.NPC.Calamity();
		writer.Write(cgn.newAI[0]);
		writer.Write(cgn.newAI[1]);
		writer.Write(cgn.newAI[2]);
		writer.Write(cgn.newAI[3]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		despawnTimer = reader.ReadInt32();
		reachedChargingPoint = reader.ReadBoolean();
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		calamityGlobalNPC.newAI[0] = reader.ReadSingle();
		calamityGlobalNPC.newAI[1] = reader.ReadSingle();
		calamityGlobalNPC.newAI[2] = reader.ReadSingle();
		calamityGlobalNPC.newAI[3] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a80: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1d: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.ghostBossClone = base.NPC.whoAmI;
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
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.5f, 0.25f, 0.75f);
		Player player = Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target];
		float lifeRatio = Main.npc[CalamityGlobalNPC.ghostBoss].life / Main.npc[CalamityGlobalNPC.ghostBoss].lifeMax;
		Vector2 vector = base.NPC.Center;
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		float colorChangeTime = 180f;
		float changeColorGateValue = chargePhaseGateValue - colorChangeTime;
		float tileEnrageMult = Main.npc[CalamityGlobalNPC.ghostBoss].ai[3];
		bool num = Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] >= chargePhaseGateValue - 60f || base.NPC.Calamity().newAI[3] == 1f;
		float chargeVelocity = 24f;
		float chargeAcceleration = 0.6f;
		float chargeDistance = 480f;
		bool speedBoost = false;
		bool despawnBoost = false;
		if (base.NPC.timeLeft < 1500)
		{
			base.NPC.timeLeft = 1500;
		}
		float velocity = 3f;
		float acceleration = 0.03f;
		if (!player.ZoneDungeon && !BossRushEvent.BossRushActive && (double)player.position.Y < Main.worldSurface * 16.0)
		{
			despawnTimer--;
			if (despawnTimer <= 0)
			{
				despawnBoost = true;
				base.NPC.ai[1] = 0f;
				base.NPC.Calamity().newAI[0] = 0f;
				base.NPC.Calamity().newAI[1] = 0f;
				base.NPC.Calamity().newAI[2] = 0f;
				base.NPC.Calamity().newAI[3] = 0f;
			}
			speedBoost = true;
			velocity += 8f;
			acceleration = 0.15f;
		}
		else
		{
			despawnTimer++;
		}
		if (Main.npc[CalamityGlobalNPC.ghostBoss].ai[2] < changeColorGateValue)
		{
			velocity = 21f;
			acceleration = 0.13f;
		}
		if (expertMode)
		{
			chargeVelocity += (revenge ? 4f : 2f);
			velocity += (revenge ? 5f : 3.5f);
			acceleration += (revenge ? 0.035f : 0.025f);
		}
		base.NPC.damage = base.NPC.defDamage;
		Vector2 rotationVector = player.Center - vector;
		if (base.NPC.Calamity().newAI[3] == 0f)
		{
			float playerXDestination = player.Center.X - vector.X;
			float playerYDestination = player.Center.Y - vector.Y;
			base.NPC.rotation = (float)Math.Atan2(playerYDestination, playerXDestination) + (float)Math.PI / 2f;
		}
		else
		{
			base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		if (!num)
		{
			reachedChargingPoint = false;
			base.NPC.ai[0] = 0f;
			base.NPC.Opacity += 0.02f;
			if (base.NPC.Opacity > 0.8f)
			{
				base.NPC.Opacity = 0.8f;
			}
			float movementLimitX = 0f;
			float movementLimitY = 0f;
			int numHooks = 4;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.type == ModContent.NPCType<PolterghastHook>())
				{
					movementLimitX += n.Center.X;
					movementLimitY += n.Center.Y;
				}
			}
			movementLimitX /= (float)numHooks;
			movementLimitY /= (float)numHooks;
			Vector2 movementLimitVector = default(Vector2);
			((Vector2)(ref movementLimitVector))._002Ector(movementLimitX, movementLimitY);
			float movementLimitedXDist = player.Center.X - movementLimitVector.X;
			float movementLimitedYDist = player.Center.Y - movementLimitVector.Y;
			if (despawnBoost)
			{
				movementLimitedYDist *= -1f;
				movementLimitedXDist *= -1f;
				velocity += 8f;
			}
			float movementLimitedDistance = (float)Math.Sqrt(movementLimitedXDist * movementLimitedXDist + movementLimitedYDist * movementLimitedYDist);
			float maxDistanceFromHooks = (expertMode ? 650f : 500f);
			if (speedBoost)
			{
				maxDistanceFromHooks += 250f;
			}
			if (death)
			{
				maxDistanceFromHooks += maxDistanceFromHooks * 0.1f * (1f - lifeRatio);
			}
			velocity *= tileEnrageMult;
			acceleration *= tileEnrageMult;
			if (death)
			{
				velocity += velocity * 0.15f * (1f - lifeRatio);
				acceleration += acceleration * 0.15f * (1f - lifeRatio);
			}
			if (movementLimitedDistance >= maxDistanceFromHooks)
			{
				movementLimitedDistance = maxDistanceFromHooks / movementLimitedDistance;
				movementLimitedXDist *= movementLimitedDistance;
				movementLimitedYDist *= movementLimitedDistance;
			}
			movementLimitX += movementLimitedXDist;
			movementLimitY += movementLimitedYDist;
			movementLimitVector = vector;
			movementLimitedXDist = movementLimitX - movementLimitVector.X;
			movementLimitedYDist = movementLimitY - movementLimitVector.Y;
			movementLimitedDistance = (float)Math.Sqrt(movementLimitedXDist * movementLimitedXDist + movementLimitedYDist * movementLimitedYDist);
			if (movementLimitedDistance < velocity)
			{
				movementLimitedXDist = base.NPC.velocity.X;
				movementLimitedYDist = base.NPC.velocity.Y;
			}
			else
			{
				movementLimitedDistance = velocity / movementLimitedDistance;
				movementLimitedXDist *= movementLimitedDistance;
				movementLimitedYDist *= movementLimitedDistance;
			}
			if (base.NPC.velocity.X < movementLimitedXDist)
			{
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f && movementLimitedXDist > 0f)
				{
					base.NPC.velocity.X += acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.X > movementLimitedXDist)
			{
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > 0f && movementLimitedXDist < 0f)
				{
					base.NPC.velocity.X -= acceleration * 2f;
				}
			}
			if (base.NPC.velocity.Y < movementLimitedYDist)
			{
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f && movementLimitedYDist > 0f)
				{
					base.NPC.velocity.Y += acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.Y > movementLimitedYDist)
			{
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > 0f && movementLimitedYDist < 0f)
				{
					base.NPC.velocity.Y -= acceleration * 2f;
				}
			}
			return;
		}
		if (base.NPC.Calamity().newAI[3] == 1f)
		{
			reachedChargingPoint = false;
			base.NPC.Opacity += 0.06f;
			if (base.NPC.Opacity > 0.8f)
			{
				base.NPC.Opacity = 0.8f;
			}
			if (base.NPC.Calamity().newAI[1] == 0f)
			{
				base.NPC.velocity = Vector2.Normalize(rotationVector) * chargeVelocity;
				base.NPC.Calamity().newAI[1] = 1f;
			}
			else
			{
				base.NPC.Calamity().newAI[2]++;
				float totalChargeTime = chargeDistance * 4f / chargeVelocity;
				float slowDownTime = chargeVelocity;
				if (base.NPC.Calamity().newAI[2] >= totalChargeTime - slowDownTime)
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.9f;
				}
				if (base.NPC.Calamity().newAI[2] >= totalChargeTime)
				{
					base.NPC.Calamity().newAI[1] = 0f;
					base.NPC.Calamity().newAI[2] = 0f;
					base.NPC.Calamity().newAI[3] = 0f;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1]++;
					if (base.NPC.ai[1] >= 3f)
					{
						base.NPC.Calamity().newAI[0] = 0f;
						base.NPC.ai[1] = 0f;
					}
				}
			}
		}
		else
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.velocity = Vector2.Zero;
				base.NPC.ai[0] = Main.rand.Next(2) + 1;
				base.NPC.netUpdate = true;
			}
			if (Main.npc[CalamityGlobalNPC.ghostBoss].Center.X >= player.Center.X)
			{
				base.NPC.Calamity().newAI[1] = ((base.NPC.ai[0] == 1f) ? (player.Center.X - chargeDistance) : Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[1]);
			}
			else
			{
				base.NPC.Calamity().newAI[1] = ((base.NPC.ai[0] == 1f) ? (player.Center.X + chargeDistance) : Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[1]);
			}
			if (Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y >= player.Center.Y)
			{
				base.NPC.Calamity().newAI[2] = ((base.NPC.ai[0] == 2f) ? (player.Center.Y - chargeDistance) : Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[2]);
			}
			else
			{
				base.NPC.Calamity().newAI[2] = ((base.NPC.ai[0] == 2f) ? (player.Center.Y + chargeDistance) : Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[2]);
			}
			Vector2 chargeVector = default(Vector2);
			((Vector2)(ref chargeVector))._002Ector(base.NPC.Calamity().newAI[1], base.NPC.Calamity().newAI[2]);
			Vector2 chargeLocationVelocity = Vector2.Normalize(chargeVector - vector) * chargeVelocity;
			float chargeDistanceGateValue = 32f;
			if (Vector2.Distance(vector, chargeVector) <= chargeDistanceGateValue * 3f)
			{
				base.NPC.Opacity += 0.06f;
				if (base.NPC.Opacity > 0.8f)
				{
					base.NPC.Opacity = 0.8f;
				}
			}
			else
			{
				base.NPC.Opacity -= 0.06f;
				if (base.NPC.Opacity < 0f)
				{
					base.NPC.Opacity = 0f;
				}
			}
			if (Vector2.Distance(vector, chargeVector) <= chargeDistanceGateValue || reachedChargingPoint)
			{
				if (!reachedChargingPoint)
				{
					SoundEngine.PlaySound(in SoundID.Item125, base.NPC.Center);
					for (int i = 0; i < 30; i++)
					{
						int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
						Main.dust[dust].noGravity = true;
						Dust obj = Main.dust[dust];
						obj.velocity *= 5f;
					}
				}
				reachedChargingPoint = true;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.Center = chargeVector;
			}
			else if (Vector2.Distance(vector, chargeVector) > 1200f)
			{
				base.NPC.velocity = chargeLocationVelocity;
			}
			else
			{
				base.NPC.SimpleFlyMovement(chargeLocationVelocity, chargeAcceleration);
			}
		}
		base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		if (Main.dedServ)
		{
			NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 150, 255) * base.NPC.Opacity;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		float timeToReachFullColor = 120f;
		float colorChangeTime = 180f;
		float changeColorGateValue = chargePhaseGateValue - colorChangeTime;
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Color lightRed = new Color(255, 100, 100, 255) * base.NPC.Opacity;
		int afterimageAmt = 7;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				if (!base.NPC.IsABestiaryIconDummy && Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] > changeColorGateValue)
				{
					afterimageColor = Color.Lerp(afterimageColor, lightRed, MathHelper.Clamp((Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] - changeColorGateValue) / timeToReachFullColor, 0f, 1f));
				}
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Color color = base.NPC.GetAlpha(drawColor);
		if (!base.NPC.IsABestiaryIconDummy && Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] > changeColorGateValue)
		{
			color = Color.Lerp(color, lightRed, MathHelper.Clamp((Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] - changeColorGateValue) / timeToReachFullColor, 0f, 1f));
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		Color c = Color.Red;
		if (!base.NPC.IsABestiaryIconDummy && (Main.npc[CalamityGlobalNPC.ghostBoss].ai[2] < changeColorGateValue || Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] > changeColorGateValue))
		{
			c = Color.Black;
		}
		Color blackWhiteLerp = Color.Lerp(Color.White, c, 0.5f);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Vector2 otherAfterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				otherAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				otherAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				Color otherAfterimageColor = blackWhiteLerp;
				otherAfterimageColor = Color.Lerp(otherAfterimageColor, Color.White, 0.5f);
				otherAfterimageColor = base.NPC.GetAlpha(otherAfterimageColor);
				otherAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				spriteBatch.Draw(GlowTexture.Value, otherAfterimagePos, (Rectangle?)base.NPC.frame, otherAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(GlowTexture.Value, drawLocation, (Rectangle?)base.NPC.frame, blackWhiteLerp, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
		}
		if (base.NPC.frame.Y > frameHeight * 3)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(145, 360);
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, hit.HitDirection, -1f);
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 90;
		base.NPC.height = 90;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 10; i++)
		{
			int ghostDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[ghostDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[ghostDust].scale = 0.5f;
				Main.dust[ghostDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 60; j++)
		{
			int ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
			Main.dust[ghostDust2].noGravity = true;
			Dust obj2 = Main.dust[ghostDust2];
			obj2.velocity *= 5f;
			ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[ghostDust2];
			obj3.velocity *= 2f;
		}
	}
}
