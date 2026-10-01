using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.PrimordialWyrm;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class Eidolist : ModNPC
{
	public bool hasBeenHit;

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/EidolistDeath");

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.75f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 16f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		base.NPC.width = 60;
		base.NPC.height = 80;
		base.NPC.lifeMax = 5000;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.value = Item.buyPrice(0, 0, 30);
		base.NPC.Opacity = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit13;
		base.NPC.DeathSound = DeathSound;
		base.NPC.timeLeft = NPC.activeTime * 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<EidolistBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[2]
		{
			ModContent.GetInstance<AbyssLayer3Biome>().Type,
			ModContent.GetInstance<AbyssLayer4Biome>().Type
		};
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Eidolist")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0927: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
		bool adultWyrmAlive = false;
		if (CalamityGlobalNPC.adultEidolonWyrmHead != -1 && Main.npc[CalamityGlobalNPC.adultEidolonWyrmHead].active)
		{
			adultWyrmAlive = true;
		}
		base.NPC.Opacity += 0.15f;
		if (base.NPC.Opacity > 1f)
		{
			base.NPC.Opacity = 1f;
		}
		Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0f, 0.4f, 0.5f);
		if (base.NPC.justHit | adultWyrmAlive)
		{
			hasBeenHit = true;
		}
		base.NPC.chaseable = hasBeenHit;
		if (!hasBeenHit)
		{
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * -1f;
				base.NPC.direction *= -1;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.collideY)
			{
				base.NPC.netUpdate = true;
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
					base.NPC.directionY = -1;
					base.NPC.localAI[2] = -1f;
				}
				else if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
					base.NPC.directionY = 1;
					base.NPC.localAI[2] = 1f;
				}
			}
			base.NPC.velocity.X += (float)base.NPC.direction * 0.1f;
			if (base.NPC.velocity.X < -2f || base.NPC.velocity.X > 2f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
			}
			if (base.NPC.localAI[2] == -1f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
				if (base.NPC.velocity.Y < -0.3f)
				{
					base.NPC.localAI[2] = 1f;
				}
			}
			else
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
				if (base.NPC.velocity.Y > 0.3f)
				{
					base.NPC.localAI[2] = -1f;
				}
			}
			if (base.NPC.velocity.Y > 0.4f || base.NPC.velocity.Y < -0.4f)
			{
				base.NPC.velocity.Y *= 0.95f;
			}
			return;
		}
		base.NPC.noTileCollide = true;
		float moveVelocity = (adultWyrmAlive ? 14f : 7f);
		float teleportTimer = 480f;
		if (base.NPC.localAI[1] == 1f)
		{
			base.NPC.localAI[1] = 0f;
			if (Main.rand.NextBool(4))
			{
				base.NPC.ai[0] = teleportTimer;
			}
		}
		base.NPC.TargetClosest();
		base.NPC.rotation = Math.Abs(base.NPC.velocity.X) * (float)base.NPC.direction * 0.1f;
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		Vector2 moveDirection = base.NPC.Center + new Vector2((float)(base.NPC.direction * 20), 6f);
		Vector2 attackDirection = Main.player[base.NPC.target].Center - moveDirection;
		bool canAttackTarget = Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1);
		if (base.NPC.justHit)
		{
			base.NPC.localAI[0] = -90f;
		}
		base.NPC.localAI[0]++;
		if (Main.netMode != 1 && base.NPC.localAI[0] >= 150f)
		{
			base.NPC.localAI[0] = -90f;
			base.NPC.netUpdate = true;
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				float num = (adultWyrmAlive ? 10f : 5f);
				Vector2 vector = default(Vector2);
				((Vector2)(ref vector))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
				_ = Main.player[base.NPC.target].Center;
				Main.rand.NextFloat(-10f, 10f);
				float yDist = Main.player[base.NPC.target].Center.Y - vector.Y + Main.rand.NextFloat(-10f, 10f);
				Vector2 targetVec = default(Vector2);
				((Vector2)(ref targetVec))._002Ector(yDist, yDist);
				float targetDist = ((Vector2)(ref targetVec)).Length();
				targetDist = num / targetDist;
				targetVec.X *= targetDist;
				targetVec.Y *= targetDist;
				int damage = ((!adultWyrmAlive) ? (Main.masterMode ? 25 : (Main.expertMode ? 30 : 40)) : (Main.masterMode ? 127 : (Main.expertMode ? 150 : 200)));
				if (Main.rand.NextBool())
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, targetVec, 465, damage, 0f, Main.myPlayer);
				}
				else
				{
					Vector2 iceMistDirection = Vector2.Normalize(Main.player[base.NPC.target].Center - base.NPC.Center + Main.player[base.NPC.target].velocity * 20f);
					if (iceMistDirection.HasNaNs())
					{
						((Vector2)(ref iceMistDirection))._002Ector((float)base.NPC.direction, 0f);
					}
					for (int n = 0; n < 1; n++)
					{
						Vector2 iceMistVelocity = iceMistDirection * 4f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, iceMistVelocity, 464, damage, 0f, Main.myPlayer, 0f, 1f);
					}
				}
			}
		}
		if (((Vector2)(ref attackDirection)).Length() > 400f || !canAttackTarget)
		{
			Vector2 tooFarMoveVelocity = attackDirection;
			if (((Vector2)(ref tooFarMoveVelocity)).Length() > moveVelocity)
			{
				((Vector2)(ref tooFarMoveVelocity)).Normalize();
				tooFarMoveVelocity *= moveVelocity;
			}
			int tooFarVelMult = 30;
			base.NPC.velocity = (base.NPC.velocity * (float)(tooFarVelMult - 1) + tooFarMoveVelocity) / (float)tooFarVelMult;
		}
		else
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.98f;
		}
		if (base.NPC.ai[2] != 0f && base.NPC.ai[3] != 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
			for (int i = 0; i < 20; i++)
			{
				int eidolistDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 20, 0f, 0f, 100, Color.Transparent);
				Dust obj = Main.dust[eidolistDust];
				obj.velocity *= 3f;
				obj.noGravity = true;
				obj.scale = 2.5f;
			}
			base.NPC.Center = new Vector2(base.NPC.ai[2] * 16f, base.NPC.ai[3] * 16f);
			base.NPC.velocity = Vector2.Zero;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
			for (int j = 0; j < 20; j++)
			{
				int eidolistDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 20, 0f, 0f, 100, Color.Transparent);
				Dust obj2 = Main.dust[eidolistDust2];
				obj2.velocity *= 3f;
				obj2.noGravity = true;
				obj2.scale = 2.5f;
			}
		}
		base.NPC.ai[0]++;
		if (!(base.NPC.ai[0] >= teleportTimer) || Main.netMode == 1)
		{
			return;
		}
		base.NPC.ai[0] = 0f;
		Point npcTileCenter = base.NPC.Center.ToTileCoordinates();
		Point targetTileCenter = Main.player[base.NPC.target].Center.ToTileCoordinates();
		int randTeleportOffset = 20;
		int npcTeleportRadius = 3;
		int targetTeleportRadius = 10;
		int teleportTileCheckRadius = 1;
		int teleportTries = 0;
		bool canTeleport = false;
		if (((Vector2)(ref attackDirection)).Length() > 2000f)
		{
			canTeleport = true;
		}
		while (!canTeleport && teleportTries < 100)
		{
			teleportTries++;
			int teleportTileX = Main.rand.Next(targetTileCenter.X - randTeleportOffset, targetTileCenter.X + randTeleportOffset + 1);
			int teleportTileY = Main.rand.Next(targetTileCenter.Y - randTeleportOffset, targetTileCenter.Y + randTeleportOffset + 1);
			if ((teleportTileY < targetTileCenter.Y - targetTeleportRadius || teleportTileY > targetTileCenter.Y + targetTeleportRadius || teleportTileX < targetTileCenter.X - targetTeleportRadius || teleportTileX > targetTileCenter.X + targetTeleportRadius) && (teleportTileY < npcTileCenter.Y - npcTeleportRadius || teleportTileY > npcTileCenter.Y + npcTeleportRadius || teleportTileX < npcTileCenter.X - npcTeleportRadius || teleportTileX > npcTileCenter.X + npcTeleportRadius) && !Main.tile[teleportTileX, teleportTileY].HasUnactuatedTile)
			{
				bool teleportSuccessful = true;
				if (teleportSuccessful && Main.tile[teleportTileX, teleportTileY].LiquidType == 1)
				{
					teleportSuccessful = false;
				}
				if (teleportSuccessful && Collision.SolidTiles(teleportTileX - teleportTileCheckRadius, teleportTileX + teleportTileCheckRadius, teleportTileY - teleportTileCheckRadius, teleportTileY + teleportTileCheckRadius))
				{
					teleportSuccessful = false;
				}
				if (teleportSuccessful)
				{
					base.NPC.ai[2] = teleportTileX;
					base.NPC.ai[3] = teleportTileY;
					break;
				}
			}
		}
		base.NPC.netUpdate = true;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		base.NPC.frameCounter += (hasBeenHit ? 0.15f : 0.075f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if ((!Main.hardMode && !DownedBossSystem.downedCalamitasClone) || !spawnInfo.Player.InAbyss())
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water)
		{
			if (!Main.remixWorld)
			{
				return 0.25f;
			}
			return 2.25f;
		}
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer4 && spawnInfo.Water)
		{
			if (!Main.remixWorld)
			{
				return 0.5f;
			}
			return 4.5f;
		}
		return 0f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Eidolist").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Eidolist2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Eidolist3").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Eidolist4").Type, base.NPC.scale);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(PrimordialWyrmHead.CanMinionsDropThings);
		LeadingConditionRule notDuringCultistFight = new LeadingConditionRule(DropHelper.If(() => !NPC.LunarApocalypseIsUp));
		notDuringCultistFight.Add(ModContent.ItemType<EidolonTablet>(), 4);
		mainRule.Add(notDuringCultistFight);
		LeadingConditionRule postLevi = new LeadingConditionRule(DropHelper.PostLevi());
		postLevi.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<Lumenyl>(), 1, 8, 10, 10, 14));
		mainRule.Add(postLevi);
		LeadingConditionRule postPlant = new LeadingConditionRule(DropHelper.PostPlant());
		postPlant.Add(1508, 1, 3, 5);
		mainRule.Add(postPlant);
	}
}
