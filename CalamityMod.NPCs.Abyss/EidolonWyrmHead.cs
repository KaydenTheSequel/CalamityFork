using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.Sounds;
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
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

[LongDistanceNetSync]
public class EidolonWyrmHead : ModNPC
{
	private Vector2 patrolSpot;

	public bool detectsPlayer;

	public const int minLength = 40;

	public const int maxLength = 41;

	public float speed;

	public float turnSpeed;

	private bool TailSpawned;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 40f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.npcSlots = 8f;
		base.NPC.damage = 170;
		base.NPC.width = 126;
		base.NPC.height = 76;
		base.NPC.defense = 70;
		base.NPC.lifeMax = 160000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 25);
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath6;
		base.NPC.netAlways = true;
		base.NPC.rarity = 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<EidolonWyrmJuvenileBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer4Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.EidolonWyrm")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(patrolSpot);
		writer.Write(detectsPlayer);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		patrolSpot = reader.ReadVector2();
		detectsPlayer = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		bool adultWyrmAlive = false;
		SoundStyle roar = (Main.zenithWorld ? Sunskater.DeathSound with
		{
			Pitch = Sunskater.DeathSound.Pitch - 0.5f
		} : CommonCalamitySounds.WyrmScreamSound);
		if (CalamityGlobalNPC.adultEidolonWyrmHead != -1 && Main.npc[CalamityGlobalNPC.adultEidolonWyrmHead].active)
		{
			adultWyrmAlive = true;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest();
		}
		if (!((base.NPC.justHit || detectsPlayer || Main.player[base.NPC.target].chaosState) | adultWyrmAlive))
		{
			Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (!(((Vector2)(ref val)).Length() < Main.player[base.NPC.target].Calamity().GetAbyssAggro(160f)))
			{
				base.NPC.damage = 0;
				goto IL_014b;
			}
		}
		detectsPlayer = true;
		base.NPC.damage = (Main.expertMode ? 340 : 170);
		goto IL_014b;
		IL_014b:
		base.NPC.chaseable = detectsPlayer;
		if (detectsPlayer)
		{
			if (base.NPC.soundDelay <= 0)
			{
				base.NPC.soundDelay = 420;
				SoundEngine.PlaySound(in roar, base.NPC.Center);
			}
		}
		else if (Main.rand.NextBool(900))
		{
			SoundEngine.PlaySound(in roar, base.NPC.Center);
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (Main.netMode != 1 && !TailSpawned && base.NPC.ai[0] == 0f)
		{
			int Previous = base.NPC.whoAmI;
			for (int segments = 0; segments < 41; segments++)
			{
				int lol = ((segments < 0 || segments >= 40) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<EidolonWyrmTail>(), base.NPC.whoAmI) : ((segments % 2 != 0) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<EidolonWyrmBodyAlt>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<EidolonWyrmBody>(), base.NPC.whoAmI)));
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = Previous;
				Main.npc[Previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				Previous = lol;
			}
			TailSpawned = true;
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.velocity.Y += 3f;
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				base.NPC.velocity.Y += 3f;
			}
			if ((double)base.NPC.position.Y > (double)Main.UnderworldLayer * 16.0)
			{
				for (int a = 0; a < Main.maxNPCs; a++)
				{
					if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<EidolonWyrmBodyAlt>() || Main.npc[a].type == ModContent.NPCType<EidolonWyrmBody>() || Main.npc[a].type == ModContent.NPCType<EidolonWyrmTail>())
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		base.NPC.alpha -= 42;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 6400f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 6400f)
			{
				base.NPC.active = false;
			}
		}
		float currentSpeed = speed;
		float currentTurnSpeed = turnSpeed;
		Vector2 segmentPosition = base.NPC.Center;
		if (patrolSpot == Vector2.Zero)
		{
			patrolSpot = Main.player[base.NPC.target].Center;
		}
		float targetXDirection = (detectsPlayer ? Main.player[base.NPC.target].Center.X : patrolSpot.X);
		float targetYDirection = (detectsPlayer ? Main.player[base.NPC.target].Center.Y : patrolSpot.Y);
		if (!detectsPlayer)
		{
			targetYDirection += 800f;
			if (Math.Abs(base.NPC.Center.X - targetXDirection) < 400f)
			{
				targetXDirection = ((!(base.NPC.velocity.X > 0f)) ? (targetXDirection - 500f) : (targetXDirection + 500f));
			}
		}
		else
		{
			currentSpeed = 7.5f;
			currentTurnSpeed = 0.125f;
			if (!Main.player[base.NPC.target].wet)
			{
				currentSpeed = 15f;
				currentTurnSpeed = 0.25f;
			}
			if (adultWyrmAlive && !Main.player[base.NPC.target].Calamity().ZoneAbyssLayer4)
			{
				currentSpeed = 22.5f;
				currentTurnSpeed = 0.375f;
			}
		}
		float maxCurrentSpeed = currentSpeed * 1.3f;
		float minCurrentSpeed = currentSpeed * 0.7f;
		float speedComparison = ((Vector2)(ref base.NPC.velocity)).Length();
		if (speedComparison > 0f)
		{
			if (speedComparison > maxCurrentSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC = base.NPC;
				nPC.velocity *= maxCurrentSpeed;
			}
			else if (speedComparison < minCurrentSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC2 = base.NPC;
				nPC2.velocity *= minCurrentSpeed;
			}
		}
		targetXDirection = (int)(targetXDirection / 16f) * 16;
		targetYDirection = (int)(targetYDirection / 16f) * 16;
		segmentPosition.X = (int)(segmentPosition.X / 16f) * 16;
		segmentPosition.Y = (int)(segmentPosition.Y / 16f) * 16;
		targetXDirection -= segmentPosition.X;
		targetYDirection -= segmentPosition.Y;
		float targetDistance = (float)Math.Sqrt(targetXDirection * targetXDirection + targetYDirection * targetYDirection);
		float absolutetargetX = Math.Abs(targetXDirection);
		float absolutetargetY = Math.Abs(targetYDirection);
		float timeToReachTarget = currentSpeed / targetDistance;
		targetXDirection *= timeToReachTarget;
		targetYDirection *= timeToReachTarget;
		if ((base.NPC.velocity.X > 0f && targetXDirection > 0f) || (base.NPC.velocity.X < 0f && targetXDirection < 0f) || (base.NPC.velocity.Y > 0f && targetYDirection > 0f) || (base.NPC.velocity.Y < 0f && targetYDirection < 0f))
		{
			if (base.NPC.velocity.X < targetXDirection)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed;
			}
			else if (base.NPC.velocity.X > targetXDirection)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed;
			}
			if (base.NPC.velocity.Y < targetYDirection)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed;
			}
			else if (base.NPC.velocity.Y > targetYDirection)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed;
			}
			if ((double)Math.Abs(targetYDirection) < (double)currentSpeed * 0.2 && ((base.NPC.velocity.X > 0f && targetXDirection < 0f) || (base.NPC.velocity.X < 0f && targetXDirection > 0f)))
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed * 2f;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed * 2f;
				}
			}
			if ((double)Math.Abs(targetXDirection) < (double)currentSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && targetYDirection < 0f) || (base.NPC.velocity.Y < 0f && targetYDirection > 0f)))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed * 2f;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed * 2f;
				}
			}
		}
		else if (absolutetargetX > absolutetargetY)
		{
			if (base.NPC.velocity.X < targetXDirection)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed * 1.1f;
			}
			else if (base.NPC.velocity.X > targetXDirection)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)currentSpeed * 0.5)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed;
				}
			}
		}
		else
		{
			if (base.NPC.velocity.Y < targetYDirection)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed * 1.1f;
			}
			else if (base.NPC.velocity.Y > targetYDirection)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)currentSpeed * 0.5)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed;
				}
			}
		}
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + 1.57f;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		bool adultWyrmAlive = false;
		if (CalamityGlobalNPC.adultEidolonWyrmHead != -1 && Main.npc[CalamityGlobalNPC.adultEidolonWyrmHead].active)
		{
			adultWyrmAlive = true;
		}
		if (adultWyrmAlive)
		{
			cooldownSlot = 1;
		}
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
		return minDist <= 40f;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return detectsPlayer;
		}
		return null;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<EidolonWyrmBody>()].Value, TextureAssets.Npc[ModContent.NPCType<EidolonWyrmBodyAlt>()].Value, 2, 26, 0.6f, new Vector2(70f, 30f), 2, 20f);
		}
		return true;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (base.NPC.spriteDirection == 1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Vector2 val = new Vector2(base.NPC.Center.X, base.NPC.Center.Y);
			Vector2 halfSizeTexture = default(Vector2);
			((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
			Vector2 vector = val - screenPos;
			vector -= new Vector2((float)GlowTexture.Value.Width, (float)(GlowTexture.Value.Height / Main.npcFrameCount[base.Type])) * 1f / 2f;
			vector += halfSizeTexture * 1f + new Vector2(0f, base.NPC.gfxOffY);
			Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), Color.White);
			Main.spriteBatch.Draw(GlowTexture.Value, vector, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, 1f, spriteEffects, 0f);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer4 && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<EidolonWyrmHead>()))
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 5.4f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(PrimordialWyrmHead.CanMinionsDropThings);
		mainRule.Add(ModContent.ItemType<Voidstone>(), 1, 30, 40);
		mainRule.Add(ModContent.ItemType<AbyssShellFossil>(), 50);
		LeadingConditionRule postPolter = new LeadingConditionRule(DropHelper.If(() => DownedBossSystem.downedPolterghast));
		mainRule.Add(postPolter);
		postPolter.Add(ModContent.ItemType<VoidEdge>(), 3);
		postPolter.Add(ModContent.ItemType<EidolicWail>(), 3);
		postPolter.Add(ModContent.ItemType<EidolonStaff>(), 3);
		LeadingConditionRule postLevi = new LeadingConditionRule(DropHelper.If(() => DownedBossSystem.downedLeviathan));
		mainRule.Add(postLevi);
		postLevi.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<Lumenyl>(), 1, 6, 8, 8, 11));
		mainRule.Add(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), 1508, 1, 8, 12));
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Wyrm").Type);
			}
		}
	}

	public override bool CheckActive()
	{
		if (detectsPlayer && !Main.player[base.NPC.target].dead)
		{
			return false;
		}
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 300);
		}
	}

	public EidolonWyrmHead()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		patrolSpot = Vector2.Zero;
		speed = 5f;
		turnSpeed = 0.1f;
		base._002Ector();
	}
}
