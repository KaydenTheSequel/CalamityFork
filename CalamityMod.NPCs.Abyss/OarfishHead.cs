using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

[LongDistanceNetSync]
public class OarfishHead : ModNPC
{
	private Vector2 patrolSpot;

	public bool detectsPlayer;

	public const int minLength = 40;

	public const int maxLength = 41;

	public float speed;

	public float turnSpeed;

	private bool TailSpawned;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 20f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 20f;
		value.Position.Y += 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 60;
		base.NPC.width = 59;
		base.NPC.height = 38;
		base.NPC.defense = 10;
		base.NPC.lifeMax = 4000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.netAlways = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<OarfishBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[2]
		{
			ModContent.GetInstance<AbyssLayer2Biome>().Type,
			ModContent.GetInstance<AbyssLayer3Biome>().Type
		};
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Oarfish")
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
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
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
			for (int segments = 0; segments < 41; segments++)
			{
				int lol = ((segments < 0 || segments >= 40) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<OarfishTail>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<OarfishBody>(), base.NPC.whoAmI));
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
			targetYDirection += 300f;
			if (Math.Abs(base.NPC.Center.X - targetXDirection) < 250f)
			{
				targetXDirection = ((!(base.NPC.velocity.X > 0f)) ? (targetXDirection - 300f) : (targetXDirection + 300f));
			}
		}
		else
		{
			currentSpeed *= 1.5f;
			currentTurnSpeed *= 1.5f;
		}
		float maxCurrentSpeed = currentSpeed * 1.3f;
		float minCurrentSpeed = currentSpeed * 0.7f;
		float currentSpeedCheck = ((Vector2)(ref base.NPC.velocity)).Length();
		if (currentSpeedCheck > 0f)
		{
			if (currentSpeedCheck > maxCurrentSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC = base.NPC;
				nPC.velocity *= maxCurrentSpeed;
			}
			else if (currentSpeedCheck < minCurrentSpeed)
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

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer2 && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<OarfishHead>()))
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.3f;
		}
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<OarfishHead>()))
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
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 3, 5, 4, 7));
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
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("OarfishHead").Type);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<OarfishBody>()].Value, 11, 10, 0.6f, Vector2.Zero, 3, 10f);
		}
		return true;
	}

	public OarfishHead()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		patrolSpot = Vector2.Zero;
		speed = 3f;
		turnSpeed = 0.05f;
		base._002Ector();
	}
}
