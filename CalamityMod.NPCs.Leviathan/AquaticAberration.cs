using System;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Leviathan;

public class AquaticAberration : ModNPC
{
	public bool WaitingForLeviathan
	{
		get
		{
			if (Main.npc.IndexInRange(CalamityGlobalNPC.leviathan) && (float)Main.npc[CalamityGlobalNPC.leviathan].life / (float)Main.npc[CalamityGlobalNPC.leviathan].lifeMax >= ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 0.7f : 0.4f))
			{
				return true;
			}
			return CalamityUtils.FindFirstProjectile(ModContent.ProjectileType<LeviathanSpawner>()) != -1;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.X += 25f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 50;
		base.NPC.aiStyle = -1;
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.defense = 14;
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 10000 : 600);
		base.NPC.knockBackResist = 0.2f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.AIType = -1;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 1.3f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<Leviathan>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AquaticAberration")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0822: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.leviathan < 0 || !Main.npc[CalamityGlobalNPC.leviathan].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		base.NPC.damage = 0;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		base.NPC.TargetClosest(faceTarget: false);
		base.NPC.rotation = base.NPC.velocity.ToRotation();
		if (Math.Sign(base.NPC.velocity.X) != 0)
		{
			base.NPC.spriteDirection = -Math.Sign(base.NPC.velocity.X);
		}
		if (base.NPC.rotation < -(float)Math.PI / 2f)
		{
			base.NPC.rotation += (float)Math.PI;
		}
		if (base.NPC.rotation > (float)Math.PI / 2f)
		{
			base.NPC.rotation -= (float)Math.PI;
		}
		base.NPC.spriteDirection = Math.Sign(base.NPC.velocity.X);
		bool leviathanInPhase4 = (float)Main.npc[CalamityGlobalNPC.leviathan].life / (float)Main.npc[CalamityGlobalNPC.leviathan].lifeMax < 0.2f;
		bool sirenAlive = false;
		if (CalamityGlobalNPC.siren != -1)
		{
			sirenAlive = Main.npc[CalamityGlobalNPC.siren].active;
		}
		if (sirenAlive && WaitingForLeviathan)
		{
			sirenAlive = false;
		}
		float inertia = (death ? 26f : (revenge ? 27f : (expertMode ? 28f : 30f)));
		if (!sirenAlive | leviathanInPhase4)
		{
			inertia *= 0.75f;
		}
		if (base.NPC.ai[0] == 0f)
		{
			float lungeSpeed = (death ? 12f : (revenge ? 11f : (expertMode ? 10f : 8f)));
			if (!sirenAlive | leviathanInPhase4)
			{
				lungeSpeed *= 1.25f;
			}
			Vector2 npcCenter = base.NPC.Center;
			Vector2 targetDirection = Main.player[base.NPC.target].Center - npcCenter;
			Vector2 beginLungeYDist = targetDirection - Vector2.UnitY * 300f * base.NPC.scale;
			float num = ((Vector2)(ref targetDirection)).Length();
			targetDirection = Vector2.Normalize(targetDirection) * lungeSpeed;
			beginLungeYDist = Vector2.Normalize(beginLungeYDist) * lungeSpeed;
			bool canHitPlayer = Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1);
			if (base.NPC.ai[3] >= 120f)
			{
				canHitPlayer = true;
			}
			canHitPlayer = canHitPlayer && targetDirection.ToRotation() > (float)Math.PI / 8f && targetDirection.ToRotation() < (float)Math.PI * 7f / 8f;
			if (num > 800f * base.NPC.scale || !canHitPlayer)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * (inertia - 1f) + beginLungeYDist.X) / inertia;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * (inertia - 1f) + beginLungeYDist.Y) / inertia;
				if (!canHitPlayer)
				{
					base.NPC.ai[3]++;
					if (base.NPC.ai[3] == 120f)
					{
						base.NPC.netUpdate = true;
					}
				}
				else
				{
					base.NPC.ai[3] = 0f;
				}
			}
			else
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[2] = targetDirection.X;
				base.NPC.ai[3] = targetDirection.Y;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.8f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 5f)
			{
				base.NPC.ai[0] = 2f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
				Vector2 velocity = default(Vector2);
				((Vector2)(ref velocity))._002Ector(base.NPC.ai[2], base.NPC.ai[3]);
				((Vector2)(ref velocity)).Normalize();
				velocity *= ((!sirenAlive | leviathanInPhase4) ? 12f : 10f);
				base.NPC.velocity = velocity;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.ai[1]++;
			bool doLunge = base.NPC.Center.Y + 50f > Main.player[base.NPC.target].Center.Y;
			if (((base.NPC.ai[1] >= 90f) & doLunge) || ((Vector2)(ref base.NPC.velocity)).Length() < ((!sirenAlive | leviathanInPhase4) ? 10f : 8f))
			{
				base.NPC.ai[0] = 3f;
				base.NPC.ai[1] = 45f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				NPC nPC2 = base.NPC;
				nPC2.velocity /= 2f;
				base.NPC.netUpdate = true;
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				Vector2 npcCenterAgain = base.NPC.Center;
				Vector2 vec2 = Main.player[base.NPC.target].Center - npcCenterAgain;
				((Vector2)(ref vec2)).Normalize();
				if (vec2.HasNaNs())
				{
					((Vector2)(ref vec2))._002Ector((float)base.NPC.direction, 0f);
				}
				base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + vec2 * (((Vector2)(ref base.NPC.velocity)).Length() + 0.11111112f * inertia)) / inertia;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.ai[1] -= ((!sirenAlive | leviathanInPhase4) ? 1.5f : 1f);
			if (base.NPC.ai[1] <= 0f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.98f;
		}
		if (!death)
		{
			return;
		}
		float pushVelocity = 0.5f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && Vector2.Distance(base.NPC.Center, n.Center) < 80f * base.NPC.scale)
			{
				if (base.NPC.position.X < n.position.X)
				{
					base.NPC.velocity.X -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.X += pushVelocity;
				}
				if (base.NPC.position.Y < n.position.Y)
				{
					base.NPC.velocity.Y -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.Y += pushVelocity;
				}
			}
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(4) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
		if (Main.zenithWorld && Main.netMode != 1)
		{
			for (int i = 0; i < Main.rand.Next(1, 5); i++)
			{
				int spawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 371);
				Main.npc[spawn].target = base.NPC.target;
				Main.npc[spawn].velocity = new Vector2((float)Main.rand.Next(-6, 7), (float)Main.rand.Next(-6, 7));
				Main.npc[spawn].netUpdate = true;
				Main.npc[spawn].ai[3] = (float)Main.rand.Next(80, 121) / 100f;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}
}
