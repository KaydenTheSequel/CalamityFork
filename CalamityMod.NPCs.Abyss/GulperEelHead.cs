using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

[LongDistanceNetSync]
public class GulperEelHead : ModNPC
{
	private Vector2 patrolSpot;

	public bool detectsPlayer;

	public const int minLength = 20;

	public const int maxLength = 21;

	public float speed;

	public float turnSpeed;

	private bool TailSpawned;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.75f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 20f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 50f;
		value.Position.Y += 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 135;
		base.NPC.width = 40;
		base.NPC.height = 84;
		base.NPC.defense = 10;
		base.NPC.lifeMax = 48000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 1);
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit9;
		base.NPC.DeathSound = SoundID.NPCDeath13;
		base.NPC.netAlways = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<GulperEelBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.GulperEel")
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		if (Framing.GetTileSafely(base.NPC.Center.ToTileCoordinates()).HasUnactuatedTile && base.NPC.Distance(Main.player[base.NPC.target].Center) < 800f && Main.rand.NextBool())
		{
			Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 204, 0f, 0f, 150, default(Color), 0.3f);
			dust.fadeIn = 0.75f;
			dust.velocity *= 0.1f;
			dust.noLight = true;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest();
		}
		Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
		if (((Vector2)(ref val)).Length() < Main.player[base.NPC.target].Calamity().GetAbyssAggro(160f) || base.NPC.justHit)
		{
			detectsPlayer = true;
		}
		base.NPC.chaseable = detectsPlayer;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (Main.netMode != 1 && !TailSpawned && base.NPC.ai[0] == 0f)
		{
			int Previous = base.NPC.whoAmI;
			for (int segments = 0; segments < 21; segments++)
			{
				int lol;
				switch (segments)
				{
				case 0:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<GulperEelBody>(), base.NPC.whoAmI);
					break;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<GulperEelBodyAlt>(), base.NPC.whoAmI);
					break;
				default:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<GulperEelTail>(), base.NPC.whoAmI);
					break;
				}
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
		}
		base.NPC.alpha -= 42;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 5600f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 5600f)
			{
				base.NPC.active = false;
			}
		}
		float currentSpeed = speed;
		float currentTurnSpeed = turnSpeed;
		Vector2 segmentPosition = default(Vector2);
		((Vector2)(ref segmentPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
		if (patrolSpot == Vector2.Zero)
		{
			patrolSpot = Main.player[base.NPC.target].Center;
		}
		float targetXDirection = (detectsPlayer ? Main.player[base.NPC.target].Center.X : patrolSpot.X);
		float targetYDirection = (detectsPlayer ? Main.player[base.NPC.target].Center.Y : patrolSpot.Y);
		if (!detectsPlayer)
		{
			targetYDirection += 500f;
			if (Math.Abs(base.NPC.Center.X - targetXDirection) < 300f)
			{
				targetXDirection = ((!(base.NPC.velocity.X > 0f)) ? (targetXDirection - 400f) : (targetXDirection + 400f));
			}
		}
		else
		{
			currentSpeed *= 1.5f;
			currentTurnSpeed *= 1.5f;
		}
		float maxCurrentSpeed = currentSpeed * 1.3f;
		float minCurrentSpeed = currentSpeed * 0.7f;
		float speedCompare = ((Vector2)(ref base.NPC.velocity)).Length();
		if (speedCompare > 0f)
		{
			if (speedCompare > maxCurrentSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC = base.NPC;
				nPC.velocity *= maxCurrentSpeed;
			}
			else if (speedCompare < minCurrentSpeed)
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
		float absoluteTargetX = Math.Abs(targetXDirection);
		float absoluteTargetY = Math.Abs(targetYDirection);
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
		else if (absoluteTargetX > absoluteTargetY)
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
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			Texture2D mainBody = TextureAssets.Npc[ModContent.NPCType<GulperEelBodyAlt>()].Value;
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, (Texture2D[])(object)new Texture2D[3]
			{
				TextureAssets.Npc[ModContent.NPCType<GulperEelBody>()].Value,
				mainBody,
				mainBody
			}, 3, 26, 0.3f, new Vector2(0f, 20f), 3, 10f, 0f, 0.1f);
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
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
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
			vector += halfSizeTexture * 1f + new Vector2(0f, 4f + base.NPC.gfxOffY);
			Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), Color.LightYellow);
			Main.spriteBatch.Draw(GlowTexture.Value, vector, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, 1f, spriteEffects, 0f);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<GulperEelHead>()))
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.3f;
			}
			return 2.7f;
		}
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer4 && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<GulperEelHead>()))
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
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 6, 8, 8, 11));
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
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GulperEel").Type);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<CrushDepth>(), 300);
		}
	}

	public GulperEelHead()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		patrolSpot = Vector2.Zero;
		speed = 5f;
		turnSpeed = 0.075f;
		base._002Ector();
	}
}
