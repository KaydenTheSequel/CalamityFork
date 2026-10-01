using System;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Signus;

[AutoloadBossHead]
public class Signus : ModNPC
{
	private int spawnX = 750;

	private int spawnY = 120;

	private int lifeToAlpha;

	private int stealthTimer;

	public static Asset<Texture2D> AltTexture;

	public static Asset<Texture2D> AltTexture2;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> AltTexture_Glow;

	public static Asset<Texture2D> AltTexture2_Glow;

	public static int ScytheDamage = 60;

	public static int DustDamage = 60;

	public static int StealthStrikeMult = 2;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 10f;
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.5f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 6f;
		value.Position.Y += 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			AltTexture_Glow = ModContent.Request<Texture2D>(Texture + "AltGlow", (AssetRequestMode)2);
			AltTexture2_Glow = ModContent.Request<Texture2D>(Texture + "Alt2Glow", (AssetRequestMode)2);
			AltTexture = ModContent.Request<Texture2D>(Texture + "Alt", (AssetRequestMode)2);
			AltTexture2 = ModContent.Request<Texture2D>(Texture + "Alt2", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 160;
		base.NPC.npcSlots = 32f;
		base.NPC.width = 130;
		base.NPC.height = 130;
		base.NPC.defense = 60;
		base.NPC.LifeMaxNERB(250000, 375000, 380000);
		base.NPC.value = Item.buyPrice(0, 50);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.boss = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.HitSound = SoundID.NPCHit49;
		base.NPC.DeathSound = SoundID.NPCDeath51;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Signus")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(spawnX);
		writer.Write(spawnY);
		writer.Write(lifeToAlpha);
		writer.Write(stealthTimer);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		spawnX = reader.ReadInt32();
		spawnY = reader.ReadInt32();
		lifeToAlpha = reader.ReadInt32();
		stealthTimer = reader.ReadInt32();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_131b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1933: Unknown result type (might be due to invalid IL or missing references)
		//IL_193e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e74: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_103c: Unknown result type (might be due to invalid IL or missing references)
		//IL_106a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1070: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_109f: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1112: Unknown result type (might be due to invalid IL or missing references)
		//IL_111c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1121: Unknown result type (might be due to invalid IL or missing references)
		//IL_1144: Unknown result type (might be due to invalid IL or missing references)
		//IL_1172: Unknown result type (might be due to invalid IL or missing references)
		//IL_1178: Unknown result type (might be due to invalid IL or missing references)
		//IL_119d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1203: Unknown result type (might be due to invalid IL or missing references)
		//IL_121a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1224: Unknown result type (might be due to invalid IL or missing references)
		//IL_1229: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d22: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1459: Unknown result type (might be due to invalid IL or missing references)
		//IL_1465: Unknown result type (might be due to invalid IL or missing references)
		//IL_146f: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1511: Unknown result type (might be due to invalid IL or missing references)
		//IL_151c: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_213c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2146: Unknown result type (might be due to invalid IL or missing references)
		//IL_214b: Unknown result type (might be due to invalid IL or missing references)
		//IL_215f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2164: Unknown result type (might be due to invalid IL or missing references)
		//IL_2166: Unknown result type (might be due to invalid IL or missing references)
		//IL_216b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2174: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f92: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21da: Unknown result type (might be due to invalid IL or missing references)
		//IL_21df: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1559: Unknown result type (might be due to invalid IL or missing references)
		//IL_1560: Unknown result type (might be due to invalid IL or missing references)
		//IL_1569: Unknown result type (might be due to invalid IL or missing references)
		//IL_1573: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_172e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1737: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_17cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e46: Unknown result type (might be due to invalid IL or missing references)
		//IL_2043: Unknown result type (might be due to invalid IL or missing references)
		//IL_2048: Unknown result type (might be due to invalid IL or missing references)
		//IL_2061: Unknown result type (might be due to invalid IL or missing references)
		//IL_2063: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.signus = base.NPC.whoAmI;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		Vector2 vectorCenter = base.NPC.Center;
		double lifeRatio = (double)base.NPC.life / (double)base.NPC.lifeMax;
		lifeToAlpha = (int)((Main.getGoodWorld ? 200.0 : 100.0) * (1.0 - lifeRatio));
		int maxCharges = (death ? 1 : (revenge ? 2 : (expertMode ? 3 : 4)));
		int maxTeleports = ((death && lifeRatio < 0.9) ? 1 : (revenge ? 2 : (expertMode ? 3 : 4)));
		float inertia = (death ? 10f : (revenge ? 11f : (expertMode ? 12f : 14f)));
		float chargeVelocity = (death ? 14f : (revenge ? 13f : (expertMode ? 12f : 10f)));
		if (Main.getGoodWorld)
		{
			inertia *= 0.5f;
			chargeVelocity *= 1.15f;
		}
		bool phase2 = (lifeRatio < 0.75) & expertMode;
		bool phase3 = lifeRatio < 0.5;
		bool phase4 = lifeRatio < 0.33000001311302185;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, vectorCenter) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (!player.active || player.dead || Vector2.Distance(player.Center, vectorCenter) > 6400f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Vector2.Distance(player.Center, vectorCenter) > 6400f)
			{
				base.NPC.rotation = base.NPC.velocity.X * 0.04f;
				if (base.NPC.velocity.Y > 3f)
				{
					base.NPC.velocity.Y = 3f;
				}
				base.NPC.velocity.Y -= 0.15f;
				if (base.NPC.velocity.Y < -12f)
				{
					base.NPC.velocity.Y = -12f;
				}
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.ai[0] != 0f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					calamityGlobalNPC.newAI[0] = 0f;
					calamityGlobalNPC.newAI[1] = 0f;
					spawnY = 120;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (lifeToAlpha < (Main.getGoodWorld ? 100 : 50) && base.NPC.ai[0] != 1f)
		{
			for (int i = 0; i < 2; i++)
			{
				if (Main.rand.Next(3) < 1)
				{
					int cosmiliteDust = Dust.NewDust(vectorCenter - new Vector2(70f), 140, 140, 173, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[cosmiliteDust].noGravity = true;
					Dust obj = Main.dust[cosmiliteDust];
					obj.velocity *= 0.2f;
					Main.dust[cosmiliteDust].fadeIn = 1f;
				}
			}
		}
		int stealthSoundGate = 300;
		int maxStealth = 360;
		if (Main.zenithWorld)
		{
			if (stealthTimer < maxStealth)
			{
				stealthTimer++;
			}
			if (stealthTimer == stealthSoundGate)
			{
				SoundEngine.PlaySound(in CalamityPlayer.RogueStealthSound, base.NPC.Center);
			}
			if (stealthTimer >= stealthSoundGate && stealthTimer < maxStealth)
			{
				base.NPC.alpha = 0;
				base.NPC.knockBackResist = 0f;
				base.NPC.rotation = base.NPC.rotation.AngleLerp(0f, 0.2f);
				NPC nPC = base.NPC;
				nPC.velocity *= 0.3f;
				return;
			}
		}
		if (base.NPC.ai[0] <= 2f)
		{
			base.NPC.rotation = base.NPC.velocity.X * 0.04f;
			float playerLocation = vectorCenter.X - player.Center.X;
			base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.knockBackResist = 0.05f;
			if (expertMode)
			{
				base.NPC.knockBackResist *= Main.RegisteredGameModes[1].KnockbackToEnemiesMultiplier;
			}
			if (phase3 | revenge)
			{
				base.NPC.knockBackResist = 0f;
			}
			float speed = (revenge ? 15f : (expertMode ? 14f : 12f));
			if (expertMode)
			{
				speed += (death ? (6f * (float)(1.0 - lifeRatio)) : (4f * (float)(1.0 - lifeRatio)));
			}
			float playerXDist = player.Center.X - vectorCenter.X;
			float playerYDist = player.Center.Y - vectorCenter.Y;
			float playerDistance = (float)Math.Sqrt(playerXDist * playerXDist + playerYDist * playerYDist);
			playerDistance = speed / playerDistance;
			playerXDist *= playerDistance;
			playerYDist *= playerDistance;
			float inertia2 = 50f;
			if (Main.getGoodWorld)
			{
				inertia2 *= 0.5f;
			}
			base.NPC.velocity.X = (base.NPC.velocity.X * inertia2 + playerXDist) / (inertia2 + 1f);
			base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia2 + playerYDist) / (inertia2 + 1f);
		}
		else
		{
			base.NPC.knockBackResist = 0f;
		}
		if (base.NPC.ai[0] == -1f)
		{
			if (Main.netMode != 1)
			{
				int phase5;
				do
				{
					phase5 = Main.rand.Next(5);
				}
				while ((float)phase5 == base.NPC.ai[1] || ((phase5 == 0) & phase4) || phase5 == 1 || phase5 == 2);
				base.NPC.ai[0] = phase5;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 0f)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			base.NPC.localAI[1]++;
			if (expertMode)
			{
				base.NPC.localAI[1] += (death ? (3f * (float)(1.0 - lifeRatio)) : (2f * (float)(1.0 - lifeRatio)));
			}
			if (!(base.NPC.localAI[1] >= (Main.getGoodWorld ? 0f : 120f)))
			{
				return;
			}
			base.NPC.localAI[1] = 0f;
			base.NPC.TargetClosest();
			int maxTeleportTries = 0;
			int playerTileX;
			int playerTileY;
			while (true)
			{
				maxTeleportTries++;
				playerTileX = (int)player.Center.X / 16;
				playerTileY = (int)player.Center.Y / 16;
				int min = 14;
				int max = 18;
				playerTileX = ((!Main.rand.NextBool()) ? (playerTileX - Main.rand.Next(min, max)) : (playerTileX + Main.rand.Next(min, max)));
				playerTileY = ((!Main.rand.NextBool()) ? (playerTileY - Main.rand.Next(min, max)) : (playerTileY + Main.rand.Next(min, max)));
				if (!WorldGen.SolidTile(playerTileX, playerTileY))
				{
					break;
				}
				if (maxTeleportTries > 100)
				{
					return;
				}
			}
			base.NPC.ai[0] = 1f;
			base.NPC.ai[1] = playerTileX;
			base.NPC.ai[2] = playerTileY;
			base.NPC.netUpdate = true;
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			Vector2 position = default(Vector2);
			((Vector2)(ref position))._002Ector(base.NPC.ai[1] * 16f - (float)(base.NPC.width / 2), base.NPC.ai[2] * 16f - (float)(base.NPC.height / 2));
			for (int m = 0; m < 5; m++)
			{
				int dust = Dust.NewDust(position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 90, default(Color), 2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].fadeIn = 1f;
			}
			base.NPC.alpha += 2;
			if (expertMode)
			{
				base.NPC.alpha += (death ? ((int)Math.Round(4.5 * (1.0 - lifeRatio))) : ((int)Math.Round(3.0 * (1.0 - lifeRatio))));
			}
			if (base.NPC.alpha >= 255)
			{
				SoundEngine.PlaySound(in SoundID.Item8, vectorCenter);
				base.NPC.alpha = 255;
				base.NPC.position = position;
				for (int n = 0; n < 15; n++)
				{
					int cosmiliteDusty = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 90, default(Color), 3f);
					Main.dust[cosmiliteDusty].noGravity = true;
				}
				base.NPC.ai[0] = 2f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.alpha -= 50;
			if (base.NPC.alpha > lifeToAlpha)
			{
				return;
			}
			base.NPC.damage = base.NPC.defDamage;
			if ((Main.netMode != 1) & revenge)
			{
				SoundEngine.PlaySound(in SoundID.Item122, base.NPC.Center);
				int cosmicMineSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X + 750f), (int)player.position.Y, ModContent.NPCType<CosmicMine>());
				if (Main.dedServ)
				{
					NetMessage.SendData(23, -1, -1, null, cosmicMineSpawn);
				}
				int cosmicMineSpawn2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X - 750f), (int)player.position.Y, ModContent.NPCType<CosmicMine>());
				if (Main.dedServ)
				{
					NetMessage.SendData(23, -1, -1, null, cosmicMineSpawn2);
				}
				if (stealthTimer >= maxStealth)
				{
					int stealthCosmicMine = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X + 950f), (int)player.position.Y, ModContent.NPCType<CosmicMine>());
					if (Main.dedServ)
					{
						NetMessage.SendData(23, -1, -1, null, stealthCosmicMine);
					}
					int stealthCosmicMine2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X - 950f), (int)player.position.Y, ModContent.NPCType<CosmicMine>());
					if (Main.dedServ)
					{
						NetMessage.SendData(23, -1, -1, null, stealthCosmicMine2);
					}
					SoundEngine.PlaySound(in RaidersTalisman.StealthHitSound, base.NPC.Center);
					stealthTimer = 0;
				}
				for (int j = 0; j < 5; j++)
				{
					int teleportDust = Dust.NewDust(new Vector2(player.position.X + 750f, player.position.Y), base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Dust obj2 = Main.dust[teleportDust];
					obj2.velocity *= 3f;
					Main.dust[teleportDust].noGravity = true;
					if (Main.rand.NextBool())
					{
						Main.dust[teleportDust].scale = 0.5f;
						Main.dust[teleportDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
					int j2 = Dust.NewDust(new Vector2(player.position.X - 750f, player.position.Y), base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Dust obj3 = Main.dust[j2];
					obj3.velocity *= 3f;
					Main.dust[j2].noGravity = true;
					if (Main.rand.NextBool())
					{
						Main.dust[j2].scale = 0.5f;
						Main.dust[j2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int k = 0; k < 20; k++)
				{
					int teleportDust2 = Dust.NewDust(new Vector2(player.position.X + 750f, player.position.Y), base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[teleportDust2].noGravity = true;
					Dust obj4 = Main.dust[teleportDust2];
					obj4.velocity *= 5f;
					teleportDust2 = Dust.NewDust(new Vector2(player.position.X + 750f, player.position.Y), base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Dust obj5 = Main.dust[teleportDust2];
					obj5.velocity *= 2f;
					int teleportDusty = Dust.NewDust(new Vector2(player.position.X - 750f, player.position.Y), base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[teleportDusty].noGravity = true;
					Dust obj6 = Main.dust[teleportDusty];
					obj6.velocity *= 5f;
					teleportDusty = Dust.NewDust(new Vector2(player.position.X - 750f, player.position.Y), base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Dust obj7 = Main.dust[teleportDusty];
					obj7.velocity *= 2f;
				}
			}
			base.NPC.ai[3]++;
			base.NPC.alpha = lifeToAlpha;
			if (base.NPC.ai[3] >= (float)maxTeleports)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
			}
			else
			{
				base.NPC.ai[0] = 0f;
			}
			base.NPC.netUpdate = true;
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.rotation = base.NPC.velocity.X * 0.04f;
			float playerLocation2 = vectorCenter.X - player.Center.X;
			base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			float divisor = (expertMode ? ((death ? 12f : (revenge ? 15f : 20f)) - (float)Math.Ceiling(5.0 * (1.0 - lifeRatio))) : 20f);
			float scytheBarrageTime = divisor * 3f;
			float scytheBarrageCooldown = divisor * 3f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[2] > 0f)
			{
				base.NPC.ai[2]--;
			}
			else
			{
				base.NPC.ai[2] = scytheBarrageTime + scytheBarrageCooldown;
			}
			if (base.NPC.ai[2] <= scytheBarrageTime && base.NPC.ai[1] % divisor == divisor - 1f && Main.netMode != 1)
			{
				float scytheXDist = player.Center.X - vectorCenter.X;
				float scytheYDist = player.Center.Y - vectorCenter.Y;
				float scytheDistance = (float)Math.Sqrt(scytheXDist * scytheXDist + scytheYDist * scytheYDist);
				scytheDistance = 15f / scytheDistance;
				scytheXDist *= scytheDistance;
				scytheYDist *= scytheDistance;
				int type = ModContent.ProjectileType<SignusScythe>();
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), vectorCenter.X, vectorCenter.Y, scytheXDist, scytheYDist, type, ScytheDamage, 0f, Main.myPlayer, 0f, base.NPC.target + 1);
				if (stealthTimer >= maxStealth)
				{
					SoundEngine.PlaySound(in RaidersTalisman.StealthHitSound, base.NPC.Center);
					Vector2 offset = default(Vector2);
					for (int l = 0; l < 4; l++)
					{
						((Vector2)(ref offset))._002Ector((float)Main.rand.Next(-5, 6), (float)Main.rand.Next(-5, 6));
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), vectorCenter.X, vectorCenter.Y, scytheXDist + offset.X, scytheYDist + offset.Y, type, ScytheDamage * StealthStrikeMult, 0f, Main.myPlayer, 0f, base.NPC.target + 1);
					}
					stealthTimer = 0;
				}
			}
			float maxVelocityY = (death ? 2.5f : 3f);
			float maxVelocityX = (death ? 7f : 8f);
			if (base.NPC.position.Y > player.position.Y - 250f)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= 0.9f;
				}
				base.NPC.velocity.Y -= (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.Y > maxVelocityY)
				{
					base.NPC.velocity.Y = maxVelocityY;
				}
			}
			else if (base.NPC.position.Y < player.position.Y - 350f)
			{
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y *= 0.9f;
				}
				base.NPC.velocity.Y += (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.Y < 0f - maxVelocityY)
				{
					base.NPC.velocity.Y = 0f - maxVelocityY;
				}
			}
			if (vectorCenter.X > player.Center.X + 600f)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= 0.9f;
				}
				base.NPC.velocity.X -= (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.X > maxVelocityX)
				{
					base.NPC.velocity.X = maxVelocityX;
				}
			}
			if (vectorCenter.X < player.Center.X - 600f)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= 0.9f;
				}
				base.NPC.velocity.X += (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.X < 0f - maxVelocityX)
				{
					base.NPC.velocity.X = 0f - maxVelocityX;
				}
			}
			if (base.NPC.ai[1] >= divisor * 20f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 3f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 4f)
			{
				return;
			}
			if (Main.netMode != 1)
			{
				int totalLamps = ((Main.getGoodWorld && !Main.zenithWorld) ? 10 : 5);
				if (NPC.CountNPCS(ModContent.NPCType<CosmicLantern>()) < totalLamps)
				{
					bool buffed = false;
					if (stealthTimer >= maxStealth)
					{
						SoundEngine.PlaySound(in RaidersTalisman.StealthHitSound, base.NPC.Center);
						buffed = true;
					}
					for (int x = 0; x < totalLamps; x++)
					{
						int type2 = ModContent.NPCType<CosmicLantern>();
						if (Main.rand.NextBool(10) && Main.zenithWorld)
						{
							type2 = ModContent.NPCType<CosmicMine>();
						}
						int cosmicMineSpawn3 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X + (float)spawnX), (int)(player.position.Y + (float)spawnY), type2);
						if (Main.dedServ)
						{
							NetMessage.SendData(23, -1, -1, null, cosmicMineSpawn3);
						}
						int cosmicMineSpawn4 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X - (float)spawnX), (int)(player.position.Y + (float)spawnY), type2);
						if (Main.dedServ)
						{
							NetMessage.SendData(23, -1, -1, null, cosmicMineSpawn4);
						}
						if (buffed)
						{
							int stealthCosmicMine3 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X + (float)spawnX + (float)(spawnX / 2)), (int)(player.position.Y + (float)spawnY), ModContent.NPCType<CosmicLantern>());
							if (Main.dedServ)
							{
								NetMessage.SendData(23, -1, -1, null, stealthCosmicMine3);
							}
							int stealthCosmicMine4 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.position.X - (float)spawnX - (float)(spawnX / 2)), (int)(player.position.Y + (float)spawnY), ModContent.NPCType<CosmicLantern>());
							if (Main.dedServ)
							{
								NetMessage.SendData(23, -1, -1, null, stealthCosmicMine4);
							}
						}
						spawnY -= 60;
					}
					if (buffed)
					{
						stealthTimer = 0;
					}
					spawnY = 120;
				}
			}
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
			if (calamityGlobalNPC.newAI[0] == 0f)
			{
				float velocity = (revenge ? 16f : (expertMode ? 15f : 14f));
				if (expertMode)
				{
					velocity += (death ? (6f * (float)(1.0 - lifeRatio)) : (4f * (float)(1.0 - lifeRatio)));
				}
				Vector2 playerCenterDist = player.Center - vectorCenter;
				Vector2 playerHoverAboveDist = playerCenterDist - Vector2.UnitY * 300f;
				playerCenterDist = Vector2.Normalize(playerCenterDist) * velocity;
				playerHoverAboveDist = Vector2.Normalize(playerHoverAboveDist) * velocity;
				bool canLineUpCharge = (Collision.CanHit(vectorCenter, 1, 1, player.Center, 1, 1) || base.NPC.ai[3] >= 120f) && playerCenterDist.ToRotation() > (float)Math.PI / 8f && playerCenterDist.ToRotation() < (float)Math.PI * 7f / 8f;
				if (((Vector2)(ref playerCenterDist)).Length() > 1400f || !canLineUpCharge)
				{
					base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + playerHoverAboveDist) / inertia;
					if (!canLineUpCharge)
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
					calamityGlobalNPC.newAI[0] = 1f;
					base.NPC.ai[2] = playerCenterDist.X;
					base.NPC.ai[3] = playerCenterDist.Y;
					base.NPC.netUpdate = true;
				}
			}
			else if (calamityGlobalNPC.newAI[0] == 1f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.8f;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= 5f)
				{
					calamityGlobalNPC.newAI[0] = 2f;
					base.NPC.netUpdate = true;
					Vector2 velocity2 = default(Vector2);
					((Vector2)(ref velocity2))._002Ector(base.NPC.ai[2], base.NPC.ai[3]);
					((Vector2)(ref velocity2)).Normalize();
					velocity2 *= chargeVelocity;
					base.NPC.velocity = velocity2;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
				}
			}
			else if (calamityGlobalNPC.newAI[0] == 2f)
			{
				if (Main.netMode != 1)
				{
					bool buffed2 = false;
					if (stealthTimer >= maxStealth && base.NPC.ai[1] == 0f)
					{
						SoundEngine.PlaySound(in RaidersTalisman.StealthHitSound, base.NPC.Center);
						buffed2 = true;
					}
					base.NPC.ai[2]++;
					if ((phase2 | buffed2) && base.NPC.ai[2] % 3f == 0f)
					{
						SoundEngine.PlaySound(in SoundID.Item73, base.NPC.Center);
						int type3 = (Main.zenithWorld ? ModContent.ProjectileType<PeanutRocket>() : ModContent.ProjectileType<EssenceDust>());
						Vector2 velocity3 = Main.rand.NextVector2Circular(Main.zenithWorld ? 10f : 0f, Main.zenithWorld ? 10f : 0f);
						int ai = (buffed2 ? 69 : 0);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), vectorCenter, velocity3, type3, DustDamage, 0f, Main.myPlayer, ai);
					}
				}
				base.NPC.ai[1]++;
				bool shouldEndCharge = vectorCenter.Y + 50f > player.Center.Y;
				if (((base.NPC.ai[1] >= 90f) & shouldEndCharge) || ((Vector2)(ref base.NPC.velocity)).Length() < 8f)
				{
					calamityGlobalNPC.newAI[0] = 3f;
					base.NPC.ai[1] = 30f;
					base.NPC.ai[2] = 0f;
					if (stealthTimer >= maxStealth)
					{
						stealthTimer = 0;
					}
					NPC nPC3 = base.NPC;
					nPC3.velocity /= 2f;
					base.NPC.netUpdate = true;
				}
				else
				{
					Vector2 distFromPlayerCenter = player.Center - vectorCenter;
					((Vector2)(ref distFromPlayerCenter)).Normalize();
					if (distFromPlayerCenter.HasNaNs())
					{
						((Vector2)(ref distFromPlayerCenter))._002Ector((float)base.NPC.direction, 0f);
					}
					base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + distFromPlayerCenter * (((Vector2)(ref base.NPC.velocity)).Length() + 0.11111112f * inertia)) / inertia;
				}
			}
			else
			{
				if (calamityGlobalNPC.newAI[0] != 3f)
				{
					return;
				}
				if (stealthTimer >= maxStealth)
				{
					stealthTimer = 0;
				}
				base.NPC.ai[1]--;
				if (base.NPC.ai[1] <= 0f)
				{
					base.NPC.TargetClosest();
					calamityGlobalNPC.newAI[1]++;
					if (calamityGlobalNPC.newAI[1] >= (float)maxCharges)
					{
						base.NPC.ai[0] = -1f;
						base.NPC.ai[1] = 4f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						calamityGlobalNPC.newAI[1] = 0f;
					}
					else
					{
						base.NPC.ai[1] = 0f;
					}
					calamityGlobalNPC.newAI[0] = 0f;
					base.NPC.netUpdate = true;
				}
				NPC nPC4 = base.NPC;
				nPC4.velocity *= 0.97f;
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (base.NPC.IsABestiaryIconDummy ? 1.65 : 1.0);
		if (base.NPC.ai[0] == 4f)
		{
			if (base.NPC.frameCounter > 72.0)
			{
				base.NPC.frameCounter = 0.0;
			}
			return;
		}
		int frameY = 196;
		if (base.NPC.frameCounter > 72.0)
		{
			base.NPC.frameCounter = 0.0;
		}
		base.NPC.frame.Y = frameY * (int)(base.NPC.frameCounter / 12.0);
		if (base.NPC.frame.Y >= frameHeight * 6)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D NPCTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowMaskTexture = Texture_Glow.Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		int afterimageAmt = 5;
		Rectangle frame = base.NPC.frame;
		int frameCount = Main.npcFrameCount[base.Type];
		if (base.NPC.ai[0] == 4f)
		{
			NPCTexture = AltTexture2.Value;
			glowMaskTexture = AltTexture2_Glow.Value;
			afterimageAmt = 10;
			int frameY = 94 * (int)(base.NPC.frameCounter / 12.0);
			if (frameY >= 564)
			{
				frameY = 0;
			}
			((Rectangle)(ref frame))._002Ector(0, frameY, NPCTexture.Width, NPCTexture.Height / frameCount);
		}
		else if (base.NPC.ai[0] == 3f)
		{
			NPCTexture = AltTexture.Value;
			glowMaskTexture = AltTexture_Glow.Value;
			afterimageAmt = 7;
		}
		else
		{
			NPCTexture = TextureAssets.Npc[base.Type].Value;
			glowMaskTexture = Texture_Glow.Value;
		}
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(NPCTexture.Width / 2), (float)(NPCTexture.Height / frameCount / 2));
		float scale = base.NPC.scale;
		float rotation = base.NPC.rotation;
		float offsetY = base.NPC.gfxOffY;
		float transparency = 1f;
		if (stealthTimer >= 300)
		{
			transparency = (float)(100 - (stealthTimer - 300)) * 0.01f;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)NPCTexture.Width, (float)(NPCTexture.Height / frameCount)) * scale / 2f;
				afterimagePos += halfSizeTexture * scale + new Vector2(0f, 4f + offsetY);
				spriteBatch.Draw(NPCTexture, afterimagePos, (Rectangle?)frame, afterimageColor * transparency, rotation, halfSizeTexture, scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)NPCTexture.Width, (float)(NPCTexture.Height / frameCount)) * scale / 2f;
		drawLocation += halfSizeTexture * scale + new Vector2(0f, 4f + offsetY);
		spriteBatch.Draw(NPCTexture, drawLocation, (Rectangle?)frame, base.NPC.GetAlpha(drawColor) * transparency, rotation, halfSizeTexture, scale, spriteEffects, 0f);
		Color eyeGlowColor = Color.Lerp(Color.White, Color.Fuchsia, 0.5f);
		if (Main.zenithWorld)
		{
			eyeGlowColor = Color.MediumBlue;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Color eyeAfterimageColor = eyeGlowColor;
				eyeAfterimageColor = Color.Lerp(eyeAfterimageColor, Color.White, 0.5f);
				eyeAfterimageColor = base.NPC.GetAlpha(eyeAfterimageColor);
				eyeAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				Vector2 eyeAfterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				eyeAfterimagePos -= new Vector2((float)glowMaskTexture.Width, (float)(glowMaskTexture.Height / frameCount)) * scale / 2f;
				eyeAfterimagePos += halfSizeTexture * scale + new Vector2(0f, 4f + offsetY);
				spriteBatch.Draw(glowMaskTexture, eyeAfterimagePos, (Rectangle?)frame, eyeAfterimageColor, rotation, halfSizeTexture, scale, spriteEffects, 0f);
			}
		}
		if (Main.zenithWorld)
		{
			spriteBatch.EnterShaderRegion();
			Color outlineColor = Color.Lerp(Color.Blue, Color.White, 0.4f);
			Vector3 outlineHSL = Main.rgbToHsl(outlineColor);
			float outlineThickness = MathHelper.Clamp(0.5f, 0f, 1f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(1f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - outlineHSL.X, outlineHSL.Y, outlineHSL.Z));
			GameShaders.Misc["CalamityMod:BasicTint"].Apply();
			for (float i2 = 0f; i2 < 1f; i2 += 0.125f)
			{
				spriteBatch.Draw(glowMaskTexture, drawLocation + (i2 * ((float)Math.PI * 2f)).ToRotationVector2() * outlineThickness, (Rectangle?)frame, outlineColor, rotation, halfSizeTexture, scale, spriteEffects, 0f);
			}
			spriteBatch.ExitShaderRegion();
		}
		spriteBatch.Draw(glowMaskTexture, drawLocation, (Rectangle?)frame, eyeGlowColor, rotation, halfSizeTexture, scale, spriteEffects, 0f);
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<SupremeHealingPotion>();
	}

	public static bool LastSentinelKilled()
	{
		if (!DownedBossSystem.downedSignus && DownedBossSystem.downedStormWeaver)
		{
			return DownedBossSystem.downedCeaselessVoid;
		}
		return false;
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			DownedBossSystem.downedSignus = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SignusBag>()));
		LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
		npcLoot.Add(normalOnly);
		int[] weapons = new int[2]
		{
			ModContent.ItemType<CosmicKunai>(),
			ModContent.ItemType<Cosmilamp>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<TwistingNether>(), 1, 10, 12));
		normalOnly.Add(ModContent.ItemType<SignusMask>(), 7);
		IItemDropRule godSlayerVanity = ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerHelm>(), 20);
		godSlayerVanity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerChestplate>()));
		godSlayerVanity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerLeggings>()));
		normalOnly.Add(godSlayerVanity);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<SignusTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<SignusRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<Nanotech>()), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<EtherealTalisman>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedSignus, ModContent.ItemType<LoreSignus>(), ui: true, DropHelper.FirstKillText);
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 200;
		base.NPC.height = 150;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int teleportDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[teleportDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[teleportDust].scale = 0.5f;
				Main.dust[teleportDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 60; j++)
		{
			int teleportDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
			Main.dust[teleportDust2].noGravity = true;
			Dust obj2 = Main.dust[teleportDust2];
			obj2.velocity *= 5f;
			teleportDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[teleportDust2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Signus").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Signus2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Signus3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Signus4").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Signus5").Type);
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
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
		return minDist <= 60f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 420);
		}
	}
}
