using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class Atlas : ModNPC
{
	public static Asset<Texture2D> glowmask;

	private const int sheetHeight = 852;

	private const int startWalkFrameY = 710;

	private const float idle_walkMaxSpeed = 0.8f;

	private const float idle_walkAcceleration = 0.1f;

	private float target_walkMaxSpeed = 1.6f;

	private float target_walkAcceleration = 0.12f;

	private const float swing_minXDistance = 96f;

	private const float swing_minYDistance = 48f;

	private const int swing_minCounterHit = 16;

	private const int swing_maxCounterHit = 25;

	private const int swing_playSoundOnFrame = 15;

	private bool idling;

	private bool idle_impulseWalk;

	private bool idle_walkLeft;

	private bool idle_blocked;

	private bool swinging;

	private bool swingYeet;

	private byte swing_counter;

	private short swing_untilNext;

	public static readonly SoundStyle HurtSound = new SoundStyle("CalamityMod/Sounds/NPCHit/AtlasHurt", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/AtlasDeath")
	{
		Volume = 0.65f
	};

	public static readonly SoundStyle AggroSound = new SoundStyle("CalamityMod/Sounds/Custom/AtlasSadAggro")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle UnaggroSound = new SoundStyle("CalamityMod/Sounds/Custom/AtlasSadUnaggro")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle SwingSound = new SoundStyle("CalamityMod/Sounds/Custom/AtlasSwing")
	{
		Volume = 0.65f
	};

	public static readonly SoundStyle IdleSound = new SoundStyle("CalamityMod/Sounds/Custom/AtlasIdle", 2)
	{
		Volume = 0.6f
	};

	private float idle_counter
	{
		get
		{
			return base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = value;
		}
	}

	private float target_counter
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

	private float grounded_counter
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

	private Player Target
	{
		get
		{
			if (base.NPC.HasValidTarget)
			{
				return Main.player[base.NPC.target];
			}
			return null;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/AtlasGlow", (AssetRequestMode)2);
		}
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -5f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.lavaImmune = true;
		base.NPC.width = 78;
		base.NPC.height = 88;
		base.NPC.damage = 70;
		base.NPC.defense = 40;
		base.NPC.lifeMax = 1200;
		base.NPC.knockBackResist = 0.08f;
		base.NPC.value = Item.buyPrice(0, 0, 50);
		base.NPC.aiStyle = -1;
		base.NPC.DeathSound = DeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AtlasBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 100;
			base.NPC.defense = 50;
			base.NPC.knockBackResist = 0.04f;
			base.NPC.lifeMax = 1800;
		}
		if (CalamityWorld.revenge)
		{
			target_walkAcceleration = 0.16f;
			target_walkMaxSpeed = 2.4f;
		}
		if (CalamityWorld.death)
		{
			target_walkAcceleration = 0.2f;
			target_walkMaxSpeed = 3.2f;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Atlas")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		BitsByte bb = new BitsByte(idling, idle_impulseWalk, idle_walkLeft, idle_blocked, swinging, swingYeet);
		writer.Write(bb);
		writer.Write(swing_counter);
		writer.Write(swing_untilNext);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		BitsByte bb = reader.ReadByte();
		idling = bb[0];
		idle_impulseWalk = bb[1];
		idle_walkLeft = bb[2];
		idle_blocked = bb[3];
		swinging = bb[4];
		swingYeet = bb[5];
		swing_counter = reader.ReadByte();
		swing_untilNext = reader.ReadInt16();
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		if (!base.NPC.HasValidTarget || idling)
		{
			base.NPC.TargetClosest(faceTarget: false);
			idling = false;
			if (!base.NPC.HasValidTarget)
			{
				idling = true;
			}
			else if (!Collision.CanHit(base.NPC.position + Vector2.UnitY * 8f, 15, 15, Target.position, Target.width, Target.height))
			{
				idling = true;
			}
			else
			{
				SoundEngine.PlaySound(in AggroSound, base.NPC.Center);
			}
		}
		if (base.NPC.velocity.Y == 0f)
		{
			grounded_counter++;
		}
		else
		{
			grounded_counter = 0f;
		}
		swing_untilNext--;
		if (idling)
		{
			DoIdleAI();
		}
		else if (swinging)
		{
			DoSwingUpdate();
		}
		else
		{
			DoTargetAI();
		}
		base.NPC.stepSpeed = 0.75f;
		if (base.NPC.velocity.Y >= 0f)
		{
			Collision.StepUp(ref base.NPC.position, ref base.NPC.velocity, base.NPC.width, base.NPC.height, ref base.NPC.stepSpeed, ref base.NPC.gfxOffY, 1, holdsMatching: false, 1);
		}
	}

	private void DoSwingUpdate()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.velocity.X *= 0.84f;
		swing_counter++;
		if (swing_counter == 15)
		{
			SoundEngine.PlaySound(in SwingSound, base.NPC.Center);
		}
		if (swing_counter < 16 || swing_counter > 25)
		{
			return;
		}
		int centerX = (int)base.NPC.Center.X;
		int centerY = (int)base.NPC.Center.Y;
		int width = 96;
		int height = 142;
		Rectangle hitbox = default(Rectangle);
		((Rectangle)(ref hitbox))._002Ector(centerX + ((base.NPC.direction == -1) ? (-width) : 0), centerY - height / 2, width, height);
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (player.dead)
			{
				continue;
			}
			Rectangle rect = player.getRect();
			if (((Rectangle)(ref rect)).Intersects(hitbox))
			{
				Vector2 before = player.velocity;
				player.Hurt(PlayerDeathReason.ByNPC(base.NPC.whoAmI), base.NPC.defDamage, base.NPC.direction);
				Vector2 difference = player.velocity - before;
				float horMult = 3.5f;
				float verMult = 1.2f;
				swingYeet = false;
				if (Main.rand.NextBool(1000) || Main.zenithWorld)
				{
					horMult = 12f;
					verMult = 2.3f;
					swingYeet = true;
				}
				if (player.noKnockback)
				{
					horMult *= 0.5f;
					verMult *= 0.5f;
				}
				difference *= new Vector2(horMult, verMult);
				player.velocity = before + difference;
			}
		}
	}

	private void DoIdleAI()
	{
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		if (HoleBelow() || (base.NPC.collideX && base.NPC.oldPosition.X == base.NPC.position.X))
		{
			idle_impulseWalk = false;
			idle_blocked = true;
		}
		idle_counter++;
		if (idle_impulseWalk)
		{
			if (idle_counter == 4f)
			{
				base.NPC.frame.Y = 710;
			}
			base.NPC.velocity.X += 0.1f * (float)((!idle_walkLeft) ? 1 : (-1));
			if (Math.Abs(base.NPC.velocity.X) > 0.8f)
			{
				base.NPC.velocity.X = (idle_walkLeft ? (-0.8f) : 0.8f);
			}
			idle_impulseWalk = idle_impulseWalk && idle_counter > 20f && !Main.rand.NextBool(150);
			if (!idle_impulseWalk)
			{
				idle_counter = 0f;
			}
			base.NPC.direction = ((!idle_walkLeft) ? 1 : (-1));
			return;
		}
		idle_impulseWalk = idle_counter > 100f && Main.rand.NextBool(400);
		if (idle_impulseWalk)
		{
			idle_counter = 0f;
			if (idle_blocked)
			{
				idle_blocked = false;
				idle_walkLeft = !idle_walkLeft;
			}
			else
			{
				idle_walkLeft = Main.rand.NextBool();
			}
			base.NPC.frameCounter = 0.0;
			SoundEngine.PlaySound(in IdleSound, base.NPC.Center);
		}
		else
		{
			base.NPC.velocity.X *= 0.9f;
			if (Math.Abs(base.NPC.velocity.X) < 0.1f)
			{
				base.NPC.velocity.X = 0f;
			}
		}
	}

	private void DoTargetAI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		int mult = ((!(Target.Center.X < base.NPC.Center.X)) ? 1 : (-1));
		base.NPC.velocity.X += target_walkAcceleration * (float)mult;
		if (Math.Abs(base.NPC.velocity.X) > target_walkMaxSpeed)
		{
			base.NPC.velocity.X = target_walkMaxSpeed * (float)mult;
		}
		base.NPC.direction = ((!(base.NPC.velocity.X < 0f)) ? 1 : (-1));
		if (grounded_counter > 90f && (HoleBelow() || (base.NPC.collideX && base.NPC.position.X == base.NPC.oldPosition.X)))
		{
			base.NPC.velocity.Y = -6f;
			target_counter++;
		}
		bool canHitTarget = Collision.CanHit(base.NPC.position + Vector2.UnitY * 8f, 15, 15, Target.position, Target.width, Target.height);
		if (target_counter > 0f && !canHitTarget)
		{
			target_counter++;
		}
		else
		{
			target_counter = 0f;
		}
		if (target_counter > 180f)
		{
			idling = true;
			target_counter = 0f;
			SoundEngine.PlaySound(in UnaggroSound, base.NPC.Center);
		}
		else if (target_counter == 0f && base.NPC.velocity.Y == 0f)
		{
			Vector2 distance = base.NPC.Center - Target.Center;
			if (((swing_untilNext < 0) & canHitTarget) && Math.Abs(distance.X) < 96f && Math.Abs(distance.Y) < 48f)
			{
				StartSwing();
			}
		}
	}

	private bool HoleBelow()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		int tileWidth = 5;
		int tileX = (int)(base.NPC.Center.X / 16f) - tileWidth;
		if (base.NPC.velocity.X > 0f)
		{
			tileX += tileWidth;
		}
		int tileY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f);
		for (int y = tileY; y < tileY + 2; y++)
		{
			for (int x = tileX; x < tileX + tileWidth; x++)
			{
				if (Main.tile[x, y].HasTile)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void StartSwing()
	{
		swinging = true;
		swing_counter = 0;
		swing_untilNext = 300;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in HurtSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3, 30);
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Top, base.NPC.velocity * 0.5f, base.Mod.Find<ModGore>("AtlasGore4").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), (base.NPC.direction == 1) ? base.NPC.Right : base.NPC.Left, base.NPC.velocity * 0.5f, base.Mod.Find<ModGore>("AtlasGore2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.5f, base.Mod.Find<ModGore>("AtlasGore0").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.5f, base.Mod.Find<ModGore>("AtlasGore1").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.5f, base.Mod.Find<ModGore>("AtlasGore3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.5f, base.Mod.Find<ModGore>("AtlasGore5").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.5f, base.Mod.Find<ModGore>("AtlasGore6").Type);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		int width = 142;
		int height = 142;
		base.NPC.frame.Width = width;
		base.NPC.frame.Height = height;
		if (idling)
		{
			base.NPC.frameCounter++;
			if (!idle_impulseWalk)
			{
				base.NPC.frame.X = 0;
				if (base.NPC.frameCounter > 9.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y += height;
					if (base.NPC.frame.Y >= 852)
					{
						base.NPC.frame.Y = 0;
					}
				}
			}
			else
			{
				base.NPC.frame.X = width;
				if (base.NPC.frameCounter > 8.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y += height;
					if (base.NPC.frame.Y >= 852)
					{
						base.NPC.frame.Y = 0;
					}
				}
			}
		}
		else if (swinging)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.X = width * 2;
			if (swing_counter < 8)
			{
				base.NPC.frame.Y = 0;
			}
			else if (swing_counter < 16)
			{
				base.NPC.frame.Y = height;
			}
			else if (swing_counter < 23)
			{
				base.NPC.frame.Y = height * 2;
			}
			else if (swing_counter < 30)
			{
				base.NPC.frame.Y = height * 3;
			}
			else if (swing_counter < 38)
			{
				base.NPC.frame.Y = height * 4;
			}
			else if (swing_counter < 46)
			{
				base.NPC.frame.Y = height * 5;
			}
			else
			{
				swinging = false;
				swing_counter = 0;
				idling = false;
			}
		}
		else
		{
			base.NPC.frameCounter++;
			base.NPC.frame.X = width;
			if (base.NPC.frameCounter > 7.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += height;
				if (base.NPC.frame.Y >= 852)
				{
					base.NPC.frame.Y = 0;
				}
			}
		}
		if (base.NPC.velocity.Y != 0f)
		{
			base.NPC.frame.X = width;
			base.NPC.frame.Y = height * 4;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = base.NPC.position - new Vector2(30f, 48f) - screenPos;
		SpriteEffects effect = (SpriteEffects)(base.NPC.direction == 1);
		spriteBatch.Draw(TextureAssets.Npc[base.Type].Value, position, (Rectangle?)base.NPC.frame, drawColor, 0f, default(Vector2), 1f, effect, 0f);
		spriteBatch.Draw(glowmask.Value, position, (Rectangle?)base.NPC.frame, Color.White * 0.65f, 0f, default(Vector2), 1f, effect, 0f);
		return false;
	}

	private Vector2 GetEyePos()
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		int x = 0;
		int y = 0;
		switch (base.NPC.frame.Y)
		{
		case 0:
			x = 45;
			y = 79;
			break;
		case 142:
			x = 41;
			y = 79;
			break;
		case 284:
			x = 35;
			y = 75;
			break;
		case 426:
			x = 25;
			y = 73;
			break;
		case 568:
			x = 31;
			y = 77;
			break;
		case 710:
			x = 43;
			y = 81;
			break;
		}
		if (base.NPC.direction == 1)
		{
			x = 142 - x;
		}
		return new Vector2((float)x, (float)y);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(1))
		{
			return 0.09f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<TitanHeart>());
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 6, 8, 7, 9));
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<TitanArm>(), 10);
	}
}
