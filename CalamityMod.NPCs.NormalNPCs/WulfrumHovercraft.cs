using System;
using System.IO;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class WulfrumHovercraft : ModNPC
{
	internal enum HovercraftAIState
	{
		Searching,
		Hover,
		Slowdown,
		SwoopDownward
	}

	public float StunTime;

	public const float StunTimeMax = 45f;

	public const float SearchXOffset = 345f;

	public const float TotalSubphaseTime = 110f;

	internal HovercraftAIState AIState
	{
		get
		{
			return (HovercraftAIState)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = (float)value;
		}
	}

	public float SubphaseTime
	{
		get
		{
			return base.NPC.ai[1];
		}
		set
		{
			base.NPC.ai[1] = value;
		}
	}

	public float SearchDirection
	{
		get
		{
			return base.NPC.ai[2];
		}
		set
		{
			base.NPC.ai[2] = value;
		}
	}

	public float SuperchargeTimer
	{
		get
		{
			return base.NPC.ai[3];
		}
		set
		{
			base.NPC.ai[3] = value;
		}
	}

	public bool Supercharged => SuperchargeTimer > 0f;

	public ref float FlyAwayTimer => ref base.NPC.localAI[0];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 12;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.AIType = -1;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 15;
		base.NPC.width = 40;
		base.NPC.height = 38;
		base.NPC.defense = 4;
		base.NPC.lifeMax = 25;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = WulfrumAmplifier.Hit;
		base.NPC.DeathSound = CommonCalamitySounds.WulfrumNPCDeathSound;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<WulfrumHovercraftBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.WulfrumHovercraft")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(StunTime);
		writer.Write(FlyAwayTimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		StunTime = reader.ReadSingle();
		FlyAwayTimer = reader.ReadSingle();
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		int frame = (int)(base.NPC.frameCounter / 5.0) % (Main.npcFrameCount[base.Type] / 2);
		if (Supercharged)
		{
			frame += Main.npcFrameCount[base.Type] / 2;
		}
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.knockBackResist = 0.1f;
		Player player = Main.player[base.NPC.target];
		bool farFromPlayer = base.NPC.Distance(player.Center) > 960f;
		if (((base.NPC.target < 0 || base.NPC.target >= 255) | farFromPlayer) || player.dead || !player.active)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			farFromPlayer = base.NPC.Distance(player.Center) > 960f;
			if ((player.dead || !player.active) | farFromPlayer)
			{
				if (FlyAwayTimer > 420f)
				{
					if (Main.zenithWorld && player.active && !farFromPlayer)
					{
						AIState = HovercraftAIState.SwoopDownward;
						SoundStyle style = SoundID.DD2_KoboldFlyerHurt with
						{
							Pitch = SoundID.DD2_KoboldFlyerHurt.Pitch + 0.5f
						};
						SoundEngine.PlaySound(in style, base.NPC.Center);
					}
					else
					{
						base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, Vector2.UnitY * -8f, 0.1f);
						base.NPC.rotation = base.NPC.rotation.AngleTowards(0f, MathHelper.ToRadians(15f));
						base.NPC.noTileCollide = true;
					}
				}
				else
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.96f;
					base.NPC.rotation = base.NPC.rotation.AngleTowards(0f, MathHelper.ToRadians(15f));
					FlyAwayTimer++;
				}
				return;
			}
		}
		FlyAwayTimer = Utils.Clamp(FlyAwayTimer - 3f, 0f, 180f);
		base.NPC.noTileCollide = !farFromPlayer;
		Vector2 position = base.NPC.Center - Vector2.UnitY * 8f;
		Color newColor = Color.Lime;
		Lighting.AddLight(position, ((Color)(ref newColor)).ToVector3() * 1.5f);
		if (StunTime > 0f)
		{
			base.NPC.damage = 0;
			if (!Main.dedServ && Main.rand.NextBool(4))
			{
				for (int i = 0; i < 2; i++)
				{
					Vector2 position2 = base.NPC.Center + Main.rand.NextVector2Circular(8f, 8f);
					newColor = default(Color);
					Dust.NewDustPerfect(position2, 226, null, 0, newColor).scale = 0.7f;
				}
			}
			base.NPC.rotation = base.NPC.rotation.AngleTowards(0f, MathHelper.ToRadians(15f));
			if (StunTime > 30f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.6f;
			}
			else
			{
				base.NPC.knockBackResist = 2.4f;
			}
			StunTime--;
			return;
		}
		if (SearchDirection == 0f)
		{
			if (Math.Abs(player.Center.X + 345f - base.NPC.Center.X) < Math.Abs(player.Center.X - 345f - base.NPC.Center.X))
			{
				SearchDirection = 1f;
			}
			else
			{
				SearchDirection = -1f;
			}
			base.NPC.netUpdate = true;
		}
		if (AIState == HovercraftAIState.Searching || AIState == HovercraftAIState.Hover)
		{
			base.NPC.damage = 0;
			Vector2 destination = player.Center + new Vector2(345f * SearchDirection, -160f);
			base.NPC.velocity = base.NPC.SafeDirectionTo(destination, Vector2.UnitY) * (Supercharged ? 7f : 5f);
			if (AIState == HovercraftAIState.Hover)
			{
				destination = player.Center + new Vector2(345f * (0f - SearchDirection), -160f);
				base.NPC.velocity = base.NPC.SafeDirectionTo(destination, Vector2.UnitY) * (Supercharged ? 5.5f : 4f);
			}
			base.NPC.rotation = base.NPC.velocity.X / 16f;
			if (base.NPC.Distance(destination) < 50f)
			{
				if (AIState == HovercraftAIState.Searching)
				{
					AIState = HovercraftAIState.Slowdown;
				}
				else
				{
					AIState = HovercraftAIState.SwoopDownward;
				}
				base.NPC.netUpdate = true;
			}
		}
		if (AIState == HovercraftAIState.Slowdown)
		{
			base.NPC.damage = 0;
			SubphaseTime++;
			if (SubphaseTime < 30f)
			{
				NPC nPC3 = base.NPC;
				nPC3.velocity *= 0.96f;
			}
			else
			{
				AIState = HovercraftAIState.Hover;
				SubphaseTime = 0f;
				base.NPC.netUpdate = true;
			}
		}
		if (AIState == HovercraftAIState.SwoopDownward)
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.rotation = 0f;
			float swoopType = (Supercharged ? 70f : 110f);
			float swoopSlowdownTime = (Supercharged ? 10f : 45f);
			Vector2 swoopVelocity = Vector2.UnitY.RotatedBy((float)Math.PI * SubphaseTime / swoopType * (0f - SearchDirection)) * (Supercharged ? 11f : 8.5f);
			SubphaseTime++;
			if (SubphaseTime < swoopSlowdownTime)
			{
				swoopVelocity *= MathHelper.Lerp(1f, 0.75f, Utils.GetLerpValue(45f, 0f, SubphaseTime));
			}
			if (SubphaseTime >= swoopType - swoopSlowdownTime)
			{
				swoopVelocity *= MathHelper.Lerp(1f, 0.75f, Utils.GetLerpValue(swoopType - 45f, swoopType, SubphaseTime));
			}
			swoopVelocity.Y *= 0.5f;
			base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, swoopVelocity, 0.2f);
			if (SubphaseTime >= swoopType)
			{
				AIState = HovercraftAIState.Searching;
				SearchDirection = 0f;
				SubphaseTime = 0f;
				base.NPC.netUpdate = true;
			}
			base.NPC.rotation = base.NPC.velocity.X / 12f;
		}
		base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneSulphur || (!spawnInfo.Player.ZoneOverworldHeight && !Main.remixWorld) || (!spawnInfo.Player.ZoneNormalCaverns && spawnInfo.Player.ZoneGlowshroom && Main.remixWorld))
		{
			return 0f;
		}
		return (Main.remixWorld ? SpawnCondition.Cavern.Chance : SpawnCondition.OverworldDaySlime.Chance) * (Main.hardMode ? 0.055f : 0.135f) * (NPC.AnyNPCs(ModContent.NPCType<WulfrumAmplifier>()) ? 5.5f : 1f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumHovercraftGore1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumHovercraftGore2").Type);
				int randomGoreCount = Main.rand.Next(1, 4);
				for (int j = 0; j < randomGoreCount; j++)
				{
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumEnemyGore" + Main.rand.Next(1, 11)).Type);
				}
			}
		}
		if (!Main.getGoodWorld || !Supercharged)
		{
			return;
		}
		for (int Sparks = Main.rand.Next(2, 5); Sparks > 0; Sparks--)
		{
			Vector2 velocity = CalamityUtils.RandomVelocity(50f, 30f, 60f);
			int spark = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitX * 6f * (float)base.NPC.spriteDirection, velocity, ModContent.ProjectileType<EGloveSpark>(), 10, 0f);
			if (spark.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[spark].friendly = false;
				Main.projectile[spark].hostile = true;
				Main.projectile[spark].timeLeft = 90;
			}
		}
	}

	public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
	{
		StunTime = 45f;
		base.NPC.netUpdate = true;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<WulfrumMetalScrap>(), 1, 2, 3);
		npcLoot.Add(ModContent.ItemType<WulfrumBattery>(), new Fraction(7, 100));
		npcLoot.AddIf((DropAttemptInfo info) => info.npc.ModNPC<WulfrumHovercraft>().Supercharged, ModContent.ItemType<EnergyCore>());
	}
}
