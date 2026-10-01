using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class MoonLordAI : VanillaAIOverride
{
	public static readonly SoundStyle DeathrayChargeSound = new SoundStyle("CalamityMod/Sounds/Custom/MoonLordLaserCharge");

	public static int BoltDamage = 30;

	public static int EyeDamage = 30;

	public static int SphereDamage = 40;

	public static int DeathrayDamage = 75;

	public static int TrueEyeBoltDamage = 35;

	public static int TrueEyeEyeDamage = 35;

	public static int TrueEyeDeathrayDamage = 50;

	public static int TrueEyeSphereDamage = 55;

	public static int MoonBoulderDamage = 70;

	public override bool AI(Mod mod)
	{
		base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		int aggressionLevel = 4;
		if (base.NPC.type == 398 || base.NPC.type == 397 || base.NPC.type == 396)
		{
			switch (NPC.CountNPCS(400))
			{
			case 1:
				aggressionLevel = 3;
				break;
			case 2:
				aggressionLevel = 2;
				break;
			case 3:
				aggressionLevel = 1;
				break;
			}
		}
		if (death)
		{
			aggressionLevel = 5;
		}
		if (Main.getGoodWorld)
		{
			aggressionLevel = 6;
		}
		if (base.NPC.type == 398)
		{
			BuffedMoonLordCoreAI(aggressionLevel);
		}
		else if (base.NPC.type == 396)
		{
			BuffedMoonLordHeadAI(aggressionLevel);
		}
		else if (base.NPC.type == 397)
		{
			BuffedMoonLordHandAI(aggressionLevel);
		}
		else if (base.NPC.type == 400)
		{
			BuffedTrueEyeAI();
		}
		else if (base.NPC.type == 401)
		{
			BuffedMoonLeechBlobAI();
		}
		return false;
	}

	public void BuffedMoonLordCoreAI(int aggressionLevel)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0934: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1191: Unknown result type (might be due to invalid IL or missing references)
		//IL_1196: Unknown result type (might be due to invalid IL or missing references)
		//IL_119d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1000: Unknown result type (might be due to invalid IL or missing references)
		//IL_100b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_154a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f35: Unknown result type (might be due to invalid IL or missing references)
		//IL_158d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1592: Unknown result type (might be due to invalid IL or missing references)
		//IL_159c: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1600: Unknown result type (might be due to invalid IL or missing references)
		//IL_1605: Unknown result type (might be due to invalid IL or missing references)
		//IL_1607: Unknown result type (might be due to invalid IL or missing references)
		//IL_160c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0faa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_165b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1660: Unknown result type (might be due to invalid IL or missing references)
		//IL_1662: Unknown result type (might be due to invalid IL or missing references)
		//IL_1667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_170d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1712: Unknown result type (might be due to invalid IL or missing references)
		//IL_1714: Unknown result type (might be due to invalid IL or missing references)
		//IL_1719: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3c: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[0] != -1f && base.NPC.ai[0] != 2f && Main.rand.NextBool(200))
		{
			SoundEngine.PlaySound(Utils.SelectRandom<SoundStyle>(Main.rand, SoundID.Zombie93, SoundID.Zombie94, SoundID.Zombie95, SoundID.Zombie96, SoundID.Zombie97, SoundID.Zombie98, SoundID.Zombie99), base.NPC.Center);
		}
		if (base.NPC.localAI[3] == 0f)
		{
			base.NPC.netUpdate = true;
			base.NPC.localAI[3] = 1f;
			base.NPC.ai[0] = -1f;
		}
		if (base.NPC.ai[0] == -2f)
		{
			base.NPC.dontTakeDamage = true;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] == 30f)
			{
				SoundEngine.PlaySound(in SoundID.Zombie92, base.NPC.Center);
			}
			if (base.NPC.ai[1] < 60f)
			{
				MoonlordDeathDrama.RequestLight(base.NPC.ai[1] / 30f, base.NPC.Center);
			}
			if (base.NPC.ai[1] == 60f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 0f;
			}
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.dontTakeDamage = true;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] == 30f)
			{
				SoundEngine.PlaySound(in SoundID.Zombie92, base.NPC.Center);
			}
			if (base.NPC.ai[1] < 60f)
			{
				MoonlordDeathDrama.RequestLight(base.NPC.ai[1] / 30f, base.NPC.Center);
			}
			if (base.NPC.ai[1] == 60f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 0f;
				if (Main.netMode != 1 && base.NPC.type == 398)
				{
					base.NPC.netUpdate = true;
					for (int i = 0; i < 2; i++)
					{
						int handSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + i * 800 - 400, (int)base.NPC.Center.Y - 100, 397, base.NPC.whoAmI);
						Main.npc[handSpawn].ai[2] = i;
						Main.npc[handSpawn].ai[3] = base.NPC.whoAmI;
						Main.npc[handSpawn].netUpdate = true;
						base.NPC.localAI[i] = handSpawn;
					}
					int headSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y - 400, 396, base.NPC.whoAmI);
					Main.npc[headSpawn].ai[3] = base.NPC.whoAmI;
					Main.npc[headSpawn].netUpdate = true;
					base.NPC.localAI[2] = headSpawn;
				}
			}
		}
		int trueEyesThatShouldBeActive = 0;
		if (Main.npc[(int)base.NPC.localAI[0]].Calamity().newAI[0] == 1f)
		{
			trueEyesThatShouldBeActive++;
		}
		if (Main.npc[(int)base.NPC.localAI[1]].Calamity().newAI[0] == 1f)
		{
			trueEyesThatShouldBeActive++;
		}
		if (Main.npc[(int)base.NPC.localAI[2]].Calamity().newAI[0] == 1f)
		{
			trueEyesThatShouldBeActive++;
		}
		if (NPC.CountNPCS(400) < trueEyesThatShouldBeActive && Main.netMode != 1)
		{
			int totalSpawns = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)Main.npc[(int)base.NPC.localAI[2]].Center.X, (int)Main.npc[(int)base.NPC.localAI[2]].Center.Y, 400);
			Main.npc[totalSpawns].ai[3] = base.NPC.whoAmI;
			Main.npc[totalSpawns].netUpdate = true;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.dontTakeDamage = true;
			base.NPC.TargetClosest(faceTarget: false);
			Vector2 targetDistance = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (((Vector2)(ref targetDistance)).Length() > 20f)
			{
				float velocity = 9.25f;
				switch (aggressionLevel)
				{
				case 6:
					velocity += 3f;
					break;
				case 5:
					velocity += 1.5f;
					break;
				case 3:
					velocity -= 0.25f;
					break;
				case 2:
					velocity -= 0.5f;
					break;
				case 1:
					velocity -= 0.75f;
					break;
				}
				if (Main.npc[(int)base.NPC.localAI[2]].ai[0] == 1f)
				{
					velocity -= 2.25f;
				}
				Vector2 desiredVelocity = Vector2.Normalize(targetDistance - base.NPC.velocity) * velocity;
				Vector2 currentVelocity = base.NPC.velocity;
				base.NPC.SimpleFlyMovement(desiredVelocity, 0.5f);
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, currentVelocity, 0.5f);
			}
			if (Main.netMode != 1)
			{
				bool shouldDespawn = false;
				if (base.NPC.localAI[0] < 0f || base.NPC.localAI[1] < 0f || base.NPC.localAI[2] < 0f)
				{
					shouldDespawn = true;
				}
				else if (!Main.npc[(int)base.NPC.localAI[0]].active || Main.npc[(int)base.NPC.localAI[0]].type != 397)
				{
					shouldDespawn = true;
				}
				else if (!Main.npc[(int)base.NPC.localAI[1]].active || Main.npc[(int)base.NPC.localAI[1]].type != 397)
				{
					shouldDespawn = true;
				}
				else if (!Main.npc[(int)base.NPC.localAI[2]].active || Main.npc[(int)base.NPC.localAI[2]].type != 396)
				{
					shouldDespawn = true;
				}
				if (shouldDespawn)
				{
					base.NPC.life = 0;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
				bool coreIsOpen = true;
				if (Main.npc[(int)base.NPC.localAI[0]].Calamity().newAI[0] != 1f)
				{
					coreIsOpen = false;
				}
				if (Main.npc[(int)base.NPC.localAI[1]].Calamity().newAI[0] != 1f)
				{
					coreIsOpen = false;
				}
				if (Main.npc[(int)base.NPC.localAI[2]].Calamity().newAI[0] != 1f)
				{
					coreIsOpen = false;
				}
				if (coreIsOpen)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.dontTakeDamage = false;
					base.NPC.netUpdate = true;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.dontTakeDamage = false;
			base.NPC.TargetClosest(faceTarget: false);
			Vector2 targetDistanceVulnerable = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (((Vector2)(ref targetDistanceVulnerable)).Length() > 20f)
			{
				float velocity2 = 9.25f;
				switch (aggressionLevel)
				{
				case 6:
					velocity2 += 3f;
					break;
				case 5:
					velocity2 += 1.5f;
					break;
				case 3:
					velocity2 -= 0.25f;
					break;
				case 2:
					velocity2 -= 0.5f;
					break;
				case 1:
					velocity2 -= 0.75f;
					break;
				}
				if (Main.npc[(int)base.NPC.localAI[2]].ai[0] == 1f)
				{
					velocity2 -= 2f;
				}
				Vector2 desiredVelocity2 = Vector2.Normalize(targetDistanceVulnerable - base.NPC.velocity) * velocity2;
				Vector2 currentVelocity2 = base.NPC.velocity;
				base.NPC.SimpleFlyMovement(desiredVelocity2, 0.5f);
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, currentVelocity2, 0.5f);
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.dontTakeDamage = true;
			base.NPC.Calamity().ShouldCloseHPBar = true;
			base.NPC.velocity = new Vector2((float)base.NPC.direction, -0.5f);
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] < 60f)
			{
				MoonlordDeathDrama.RequestLight(base.NPC.ai[1] / 60f, base.NPC.Center);
			}
			if (base.NPC.ai[1] == 60f)
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile projectile = enumerator.Current;
					if (projectile.type == 456 || projectile.type == 462 || projectile.type == 455 || projectile.type == 452 || projectile.type == 454)
					{
						projectile.Kill();
					}
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					NPC n = enumerator2.Current;
					if (n.type == 400)
					{
						n.HitEffect(0, 9999.0);
						n.active = false;
					}
				}
			}
			if (base.NPC.ai[1] % 3f == 0f && base.NPC.ai[1] < 580f && base.NPC.ai[1] > 60f)
			{
				Vector2 randPositionOffset = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(20f, 420f);
				Vector2 dustPos = base.NPC.Center + randPositionOffset;
				Point dustPosTileCoords = dustPos.ToTileCoordinates();
				bool num = WorldGen.InWorld(dustPosTileCoords.X, dustPosTileCoords.Y) && !WorldGen.SolidTile(dustPosTileCoords.X, dustPosTileCoords.Y);
				float dustScale = Main.rand.NextFloat(1f, 2f);
				float fadeIn = Main.rand.NextFloat(0.4f, 1.4f);
				if (num)
				{
					float randDustAmt = Main.rand.Next(6, 19);
					for (int j = 0; (float)j < randDustAmt * 2f; j++)
					{
						float dustRotation = Main.rand.NextFloat((float)Math.PI * 2f) + (float)Math.PI * 2f / randDustAmt * (float)j;
						Vector2? velocity3 = Vector2.UnitY.RotatedBy(dustRotation) * Main.rand.NextFloat(1.6f, 9.6f);
						float scale = dustScale;
						Dust dust = Dust.NewDustPerfect(dustPos, 229, velocity3, 0, default(Color), scale);
						dust.noGravity = true;
						dust.fadeIn = fadeIn;
					}
				}
				for (float k = 0f; k < base.NPC.ai[1] / 60f; k++)
				{
					Vector2 randPosOffset = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(20f, 820f);
					Vector2 smokePos = base.NPC.Center + randPosOffset;
					Point smokePosTileCoords = smokePos.ToTileCoordinates();
					if (WorldGen.InWorld(smokePosTileCoords.X, smokePosTileCoords.Y) && !WorldGen.SolidTile(smokePosTileCoords.X, smokePosTileCoords.Y))
					{
						int type = (Main.rand.NextBool() ? 31 : 229);
						Vector2? velocity4 = -Vector2.UnitY * Main.rand.NextFloat(0.9f, 7.5f);
						float scale = dustScale;
						Dust dust2 = Dust.NewDustPerfect(smokePos, type, velocity4, 0, default(Color), scale);
						dust2.noGravity = true;
						dust2.fadeIn = fadeIn;
					}
				}
			}
			if (base.NPC.ai[1] % 15f == 0f && base.NPC.ai[1] < 480f && base.NPC.ai[1] >= 90f && Main.netMode != 1)
			{
				Vector2 randomOffset = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(20f, 420f);
				Vector2 npcOffset = base.NPC.Center + randomOffset;
				Point npcOffsetTileCoords = npcOffset.ToTileCoordinates();
				if (WorldGen.InWorld(npcOffsetTileCoords.X, npcOffsetTileCoords.Y) && !WorldGen.SolidTile(npcOffsetTileCoords.X, npcOffsetTileCoords.Y))
				{
					float smokeRotation = (float)Main.rand.NextBool().ToDirectionInt() * ((float)Math.PI / 8f + Main.rand.NextFloat((float)Math.PI / 4f));
					Vector2 smokeVelocity = -Vector2.UnitY.RotatedBy(smokeRotation) * Main.rand.NextFloat(3f, 6f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), npcOffset, smokeVelocity, 622, 0, 0f, Main.myPlayer);
				}
			}
			if (base.NPC.ai[1] == 1f)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath61, base.NPC.Center);
			}
			if (base.NPC.ai[1] >= 480f)
			{
				MoonlordDeathDrama.RequestLight((base.NPC.ai[1] - 480f) / 120f, base.NPC.Center);
			}
			if (base.NPC.ai[1] >= 600f)
			{
				base.NPC.life = 0;
				base.NPC.HitEffect(0, 1337.0);
				base.NPC.checkDead();
				ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					NPC n2 = enumerator3.Current;
					if (n2.type == 397 || n2.type == 396)
					{
						n2.active = false;
						if (Main.netMode != 1)
						{
							NetMessage.SendData(23, -1, -1, null, n2.whoAmI);
						}
					}
				}
				base.NPC.active = false;
				if (Main.netMode != 1)
				{
					NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
				}
				return;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.dontTakeDamage = true;
			Vector2 despawnVelocityLerp = default(Vector2);
			((Vector2)(ref despawnVelocityLerp))._002Ector((float)base.NPC.direction, -0.5f);
			base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, despawnVelocityLerp, 0.98f);
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] < 60f)
			{
				MoonlordDeathDrama.RequestLight(base.NPC.ai[1] / 40f, base.NPC.Center);
			}
			if (base.NPC.ai[1] == 40f)
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator4 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator4.MoveNext())
				{
					Projectile projectile2 = enumerator4.Current;
					if (projectile2.type == 456 || projectile2.type == 462 || projectile2.type == 455 || projectile2.type == 452 || projectile2.type == 454)
					{
						projectile2.Kill();
					}
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator5 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator5.MoveNext())
				{
					NPC n3 = enumerator5.Current;
					if (n3.type == 400)
					{
						n3.HitEffect(0, 9999.0);
						n3.active = false;
					}
				}
				for (int l = 0; l < 600; l++)
				{
					Gore gore2 = Main.gore[l];
					if (gore2.active && gore2.type >= 619 && gore2.type <= 622)
					{
						gore2.active = false;
					}
				}
			}
			if (base.NPC.ai[1] >= 60f)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator6 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator6.MoveNext())
				{
					NPC n4 = enumerator6.Current;
					if (n4.type == 397 || n4.type == 396)
					{
						n4.active = false;
						if (Main.netMode != 1)
						{
							NetMessage.SendData(23, -1, -1, null, n4.whoAmI);
						}
					}
				}
				base.NPC.active = false;
				if (Main.netMode != 1)
				{
					NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
				}
				NPC.LunarApocalypseIsUp = false;
				if (Main.dedServ)
				{
					NetMessage.SendData(7);
				}
				return;
			}
		}
		bool preventDespawn = base.NPC.ai[0] == -2f || base.NPC.ai[0] == -1f || base.NPC.ai[0] == 2f || base.NPC.ai[0] == 3f || (Main.player[base.NPC.target].active && !Main.player[base.NPC.target].dead);
		if (!preventDespawn)
		{
			ActiveEntityIterator<Player>.Enumerator enumerator7 = Main.ActivePlayers.GetEnumerator();
			while (enumerator7.MoveNext())
			{
				if (!enumerator7.Current.dead)
				{
					preventDespawn = true;
					break;
				}
			}
		}
		if (!preventDespawn)
		{
			base.NPC.ai[0] = 3f;
			base.NPC.ai[1] = 0f;
			base.NPC.netUpdate = true;
		}
		if (!(base.NPC.ai[0] >= 0f) || !(base.NPC.ai[0] < 2f) || Main.netMode == 1 || !(base.NPC.Distance(Main.player[base.NPC.target].Center) > 1800f))
		{
			return;
		}
		base.NPC.ai[0] = -2f;
		base.NPC.netUpdate = true;
		Vector2 teleportOffset = Main.player[base.NPC.target].Center - Vector2.UnitY * 150f - base.NPC.Center;
		NPC nPC = base.NPC;
		nPC.position += teleportOffset;
		if (Main.npc[(int)base.NPC.localAI[0]].active)
		{
			NPC obj = Main.npc[(int)base.NPC.localAI[0]];
			obj.position += teleportOffset;
			Main.npc[(int)base.NPC.localAI[0]].netUpdate = true;
		}
		if (Main.npc[(int)base.NPC.localAI[1]].active)
		{
			NPC obj2 = Main.npc[(int)base.NPC.localAI[1]];
			obj2.position += teleportOffset;
			Main.npc[(int)base.NPC.localAI[1]].netUpdate = true;
		}
		if (Main.npc[(int)base.NPC.localAI[2]].active)
		{
			NPC obj3 = Main.npc[(int)base.NPC.localAI[2]];
			obj3.position += teleportOffset;
			Main.npc[(int)base.NPC.localAI[2]].netUpdate = true;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator8 = Main.ActiveNPCs.GetEnumerator();
		while (enumerator8.MoveNext())
		{
			NPC n5 = enumerator8.Current;
			if (n5.type == 400)
			{
				n5.position += teleportOffset;
				n5.netUpdate = true;
			}
		}
	}

	public void BuffedMoonLordHeadAI(int aggressionLevel)
	{
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dba: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		if (!Main.npc[(int)base.NPC.ai[3]].active || Main.npc[(int)base.NPC.ai[3]].type != 398)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
		}
		if (base.NPC.localAI[3] == 13f && !base.NPC.dontTakeDamage)
		{
			base.NPC.PopAllAttachedProjectilesAndTakeDamageForThem();
		}
		base.NPC.dontTakeDamage = base.NPC.localAI[3] >= 15f;
		if (calamityGlobalNPC.newAI[0] == 1f)
		{
			base.NPC.dontTakeDamage = true;
		}
		base.NPC.velocity = Vector2.Zero;
		base.NPC.Center = Main.npc[(int)base.NPC.ai[3]].Center - Vector2.UnitY * 400f;
		Vector2 eyeSizeVector = default(Vector2);
		((Vector2)(ref eyeSizeVector))._002Ector(27f, 59f);
		float attackTimer = 0f;
		int phaseAttackTime = 0;
		int mouthAnimationCheck = 0;
		int eyeAnimationCheck = 0;
		if (base.NPC.ai[0] >= 0f || base.NPC.ai[0] == -2f)
		{
			if (base.NPC.ai[0] == -2f)
			{
				if (calamityGlobalNPC.newAI[0] != 1f)
				{
					calamityGlobalNPC.newAI[0] = 1f;
				}
				base.NPC.life = base.NPC.lifeMax;
				base.NPC.netUpdate = true;
				base.NPC.dontTakeDamage = true;
			}
			if (Main.npc[(int)base.NPC.ai[3]].ai[0] == 2f)
			{
				base.NPC.ai[0] = -3f;
				return;
			}
			float ai0CrossCheck = base.NPC.ai[0];
			base.NPC.ai[1]++;
			int attackIncrement = 0;
			int totalAttackTimer = 0;
			for (; attackIncrement < 5; attackIncrement++)
			{
				phaseAttackTime = NPC.MoonLordAttacksArray[0, 2, 1, attackIncrement];
				if ((float)(phaseAttackTime + totalAttackTimer) > base.NPC.ai[1])
				{
					break;
				}
				totalAttackTimer += phaseAttackTime;
			}
			if (attackIncrement == 5)
			{
				attackIncrement = 0;
				base.NPC.ai[1] = 0f;
				phaseAttackTime = NPC.MoonLordAttacksArray[0, 2, 1, attackIncrement];
				totalAttackTimer = 0;
			}
			base.NPC.ai[0] = NPC.MoonLordAttacksArray[0, 2, 0, attackIncrement];
			attackTimer = (int)base.NPC.ai[1] - totalAttackTimer;
			if (base.NPC.ai[0] != ai0CrossCheck)
			{
				base.NPC.netUpdate = true;
			}
		}
		if (base.NPC.ai[0] == -3f)
		{
			base.NPC.dontTakeDamage = true;
			base.NPC.rotation = MathHelper.Lerp(base.NPC.rotation, (float)Math.PI / 12f, 0.07f);
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 32f)
			{
				base.NPC.ai[1] = 0f;
			}
			if (base.NPC.ai[1] < 0f)
			{
				base.NPC.ai[1] = 0f;
			}
			if (base.NPC.localAI[2] < 14f)
			{
				base.NPC.localAI[2]++;
			}
		}
		else if (base.NPC.ai[0] == 0f)
		{
			eyeAnimationCheck = 3;
			base.NPC.TargetClosest(faceTarget: false);
			Vector2 targetDist = Main.player[base.NPC.target].Center - base.NPC.Center + Vector2.UnitY * 22f;
			float deathrayTravelDist = ((Vector2)(ref targetDist)).Length() / 500f;
			if (deathrayTravelDist > 1f)
			{
				deathrayTravelDist = 1f;
			}
			deathrayTravelDist = 1f - deathrayTravelDist;
			deathrayTravelDist *= 2f;
			if (deathrayTravelDist > 1f)
			{
				deathrayTravelDist = 1f;
			}
			base.NPC.localAI[0] = targetDist.ToRotation();
			base.NPC.localAI[1] = deathrayTravelDist;
			base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 1f, 0.2f);
		}
		if (base.NPC.ai[0] == 1f)
		{
			if (attackTimer < 180f)
			{
				base.NPC.localAI[1] -= 0.05f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
				if (attackTimer >= 60f)
				{
					if (attackTimer == 60f)
					{
						SoundEngine.PlaySound(in DeathrayChargeSound, Main.player[base.NPC.target].Center);
					}
					int deathrayDustAmt = ((!(attackTimer >= 120f)) ? 1 : 2);
					for (int i = 0; i < deathrayDustAmt; i++)
					{
						float deathrayDustScale = ((i % 2 == 1) ? 1.65f : 0.8f);
						Vector2 deathrayDustRotation = base.NPC.Center + Main.rand.NextFloat((float)Math.PI * 2f).ToRotationVector2() * eyeSizeVector / 2f;
						Vector2 position = deathrayDustRotation - Vector2.One * 8f;
						float scale = deathrayDustScale;
						Dust dust = Dust.NewDustDirect(position, 16, 16, 229, 0f, 0f, 0, default(Color), scale);
						dust.velocity = deathrayDustRotation.DirectionTo(base.NPC.Center) * 0.35f * (10f - (float)deathrayDustAmt * 2f);
						dust.noGravity = true;
						dust.customData = base.NPC;
					}
				}
			}
			else if (attackTimer < (float)phaseAttackTime - 15f)
			{
				if (calamityGlobalNPC.newAI[1] == 0f)
				{
					calamityGlobalNPC.newAI[1] = 420f;
					switch (aggressionLevel)
					{
					case 6:
						calamityGlobalNPC.newAI[1] -= 120f;
						break;
					case 5:
						calamityGlobalNPC.newAI[1] -= 60f;
						break;
					case 3:
						calamityGlobalNPC.newAI[1] += 120f;
						break;
					case 2:
						calamityGlobalNPC.newAI[1] += 240f;
						break;
					case 1:
						calamityGlobalNPC.newAI[1] += 360f;
						break;
					}
				}
				if (attackTimer == 180f && Main.netMode != 1)
				{
					base.NPC.TargetClosest(faceTarget: false);
					Vector2 deathrayRotationSpeed = base.NPC.Center.DirectionTo(Main.player[base.NPC.target].Center);
					int deathrayRotationDirection = (deathrayRotationSpeed.X < 0f).ToDirectionInt();
					deathrayRotationSpeed = deathrayRotationSpeed.RotatedBy((float)(-deathrayRotationDirection) * ((float)Math.PI * 2f) / 6f);
					float angularSpeed = (float)deathrayRotationDirection * ((float)Math.PI * 2f) / calamityGlobalNPC.newAI[1];
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, deathrayRotationSpeed, 455, DeathrayDamage, 0f, Main.myPlayer, angularSpeed, base.NPC.whoAmI);
					base.NPC.ai[2] = (deathrayRotationSpeed.ToRotation() + (float)Math.PI + (float)Math.PI * 2f) * (float)deathrayRotationDirection;
					base.NPC.netUpdate = true;
				}
				base.NPC.localAI[1] += 0.05f;
				if (base.NPC.localAI[1] > 1f)
				{
					base.NPC.localAI[1] = 1f;
				}
				float deathrayFaceDirection = (base.NPC.ai[2] >= 0f).ToDirectionInt();
				float deathrayTimer = base.NPC.ai[2];
				if (deathrayTimer < 0f)
				{
					deathrayTimer *= -1f;
				}
				deathrayTimer += deathrayFaceDirection * ((float)Math.PI * 2f) / calamityGlobalNPC.newAI[1] - (float)Math.PI;
				base.NPC.localAI[0] = deathrayTimer;
				base.NPC.ai[2] = (deathrayTimer + (float)Math.PI) * deathrayFaceDirection;
			}
			else
			{
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.localAI[1] -= 0.07f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
					if (Main.netMode != 1 && Main.zenithWorld)
					{
						for (int k = 0; k < 30; k++)
						{
							if (!WorldGen.SolidTile((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f)))
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (float)Main.rand.Next(-1599, 1600) * 0.01f, (float)Main.rand.Next(-1599, 1) * 0.01f, 1021, 70, 10f);
							}
						}
					}
				}
				eyeAnimationCheck = 3;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			mouthAnimationCheck = 2;
			eyeAnimationCheck = 3;
			Vector2 mouthOffset = Vector2.UnitY * 216f;
			if (attackTimer == 0f && Main.netMode != 1)
			{
				Vector2 leechSpawnPos = base.NPC.Center + mouthOffset;
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player p = enumerator.Current;
					if (!p.dead && Vector2.Distance(p.Center, leechSpawnPos) <= 3000f)
					{
						Vector2 targetLeechDist = (Main.player[base.NPC.target].Center - leechSpawnPos).SafeNormalize(Vector2.Zero);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), leechSpawnPos, targetLeechDist, 456, 0, 0f, Main.myPlayer, base.NPC.whoAmI + 1, p.whoAmI);
					}
				}
			}
			if (attackTimer >= 120f && attackTimer <= 240f && attackTimer % 30f == 0f && Main.netMode != 1)
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Projectile p2 = enumerator2.Current;
					if (p2.type == 456 && Main.player[(int)p2.ai[1]].FindBuffIndex(145) != -1)
					{
						Vector2 targetCenter = Main.player[base.NPC.target].Center;
						int moonLeech = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)targetCenter.X, (int)targetCenter.Y, 401, 0, base.NPC.whoAmI + 1, p2.whoAmI);
						Main.npc[moonLeech].netUpdate = true;
					}
				}
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			if (attackTimer == 1f)
			{
				base.NPC.TargetClosest(faceTarget: false);
				base.NPC.netUpdate = true;
			}
			Vector2 aimDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
			bool shootFirstBolt = attackTimer == (float)phaseAttackTime - 14f;
			bool shootSecondBolt = attackTimer == (float)phaseAttackTime - 7f;
			bool shootThirdBolt = attackTimer == (float)phaseAttackTime;
			switch (aggressionLevel)
			{
			case 2:
			case 3:
				shootSecondBolt = false;
				break;
			case 1:
				shootSecondBolt = false;
				shootThirdBolt = false;
				break;
			}
			base.NPC.localAI[0] = base.NPC.localAI[0].AngleLerp(aimDirection.ToRotation(), 0.5f);
			base.NPC.localAI[1] += 0.05f;
			if (base.NPC.localAI[1] > 1f)
			{
				base.NPC.localAI[1] = 1f;
			}
			if (attackTimer == (float)phaseAttackTime - 35f)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath6, base.NPC.Center);
			}
			if ((shootFirstBolt | shootSecondBolt | shootThirdBolt) && Main.netMode != 1)
			{
				Vector2 boltDirection = Utils.Vector2FromElipse(base.NPC.localAI[0].ToRotationVector2(), eyeSizeVector * base.NPC.localAI[1]);
				float velocity = 6.25f;
				switch (aggressionLevel)
				{
				case 6:
					velocity += 1.5f;
					break;
				case 5:
					velocity += 0.75f;
					break;
				case 3:
					velocity -= 0.25f;
					break;
				case 2:
					velocity -= 0.5f;
					break;
				case 1:
					velocity -= 0.75f;
					break;
				}
				Vector2 boltVelocity = Vector2.Normalize(aimDirection) * velocity;
				int type = 462;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + boltDirection, boltVelocity, type, BoltDamage, 0f, Main.myPlayer);
			}
		}
		int num = mouthAnimationCheck * 7;
		if ((float)num > base.NPC.localAI[2])
		{
			base.NPC.localAI[2]++;
		}
		if ((float)num < base.NPC.localAI[2])
		{
			base.NPC.localAI[2]--;
		}
		if (base.NPC.localAI[2] < 0f)
		{
			base.NPC.localAI[2] = 0f;
		}
		if (base.NPC.localAI[2] > 14f)
		{
			base.NPC.localAI[2] = 14f;
		}
		int num2 = eyeAnimationCheck * 5;
		if ((float)num2 > base.NPC.localAI[3])
		{
			base.NPC.localAI[3]++;
		}
		if ((float)num2 < base.NPC.localAI[3])
		{
			base.NPC.localAI[3]--;
		}
		if (base.NPC.localAI[3] < 0f)
		{
			base.NPC.localAI[2] = 0f;
		}
		if (base.NPC.localAI[3] > 15f)
		{
			base.NPC.localAI[2] = 15f;
		}
	}

	public void BuffedMoonLordHandAI(int aggressionLevel)
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1202: Unknown result type (might be due to invalid IL or missing references)
		//IL_1204: Unknown result type (might be due to invalid IL or missing references)
		//IL_1212: Unknown result type (might be due to invalid IL or missing references)
		//IL_1217: Unknown result type (might be due to invalid IL or missing references)
		//IL_121c: Unknown result type (might be due to invalid IL or missing references)
		//IL_121e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1225: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1241: Unknown result type (might be due to invalid IL or missing references)
		//IL_1248: Unknown result type (might be due to invalid IL or missing references)
		//IL_126a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1275: Unknown result type (might be due to invalid IL or missing references)
		//IL_127a: Unknown result type (might be due to invalid IL or missing references)
		//IL_127f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1281: Unknown result type (might be due to invalid IL or missing references)
		//IL_1283: Unknown result type (might be due to invalid IL or missing references)
		//IL_1288: Unknown result type (might be due to invalid IL or missing references)
		//IL_128a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1292: Unknown result type (might be due to invalid IL or missing references)
		//IL_129d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_105a: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0927: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_094b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c04: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_110c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1111: Unknown result type (might be due to invalid IL or missing references)
		//IL_1116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_117b: Unknown result type (might be due to invalid IL or missing references)
		//IL_117d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1184: Unknown result type (might be due to invalid IL or missing references)
		//IL_1189: Unknown result type (might be due to invalid IL or missing references)
		//IL_119d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbb: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		NPC.InitializeMoonLordAttacks();
		if (!Main.npc[(int)base.NPC.ai[3]].active || Main.npc[(int)base.NPC.ai[3]].type != 398)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
		}
		bool isLeftHand = base.NPC.ai[2] == 0f;
		float handFaceDirection = -isLeftHand.ToDirectionInt();
		base.NPC.spriteDirection = (int)handFaceDirection;
		if (base.NPC.frameCounter == 19.0 && !base.NPC.dontTakeDamage)
		{
			base.NPC.PopAllAttachedProjectilesAndTakeDamageForThem();
		}
		base.NPC.dontTakeDamage = base.NPC.frameCounter >= 21.0;
		if (calamityGlobalNPC.newAI[0] == 1f)
		{
			base.NPC.dontTakeDamage = true;
		}
		Vector2 eyeSizeVector = default(Vector2);
		((Vector2)(ref eyeSizeVector))._002Ector(30f, 66f);
		Vector2 coreCenter = Main.npc[(int)base.NPC.ai[3]].Center;
		float handAttackTimer = 0f;
		float phaseAttackTime = 0f;
		int handFrameCheck = 0;
		if (Main.npc[(int)base.NPC.ai[3]].ai[0] == 2f)
		{
			base.NPC.ai[0] = -2f;
		}
		if (base.NPC.ai[0] != -2f || (base.NPC.ai[0] == -2f && Main.npc[(int)base.NPC.ai[3]].ai[0] != 2f))
		{
			if (base.NPC.ai[0] == -2f && Main.npc[(int)base.NPC.ai[3]].ai[0] != 2f)
			{
				if (calamityGlobalNPC.newAI[0] != 1f)
				{
					calamityGlobalNPC.newAI[0] = 1f;
				}
				base.NPC.life = base.NPC.lifeMax;
				base.NPC.netUpdate = true;
				base.NPC.dontTakeDamage = true;
			}
			float ai0CrossCheck = base.NPC.ai[0];
			base.NPC.ai[1]++;
			int handType = ((!isLeftHand) ? 1 : 0);
			int attackIncrement = 0;
			int totalAttackTimer = 0;
			for (; attackIncrement < 5; attackIncrement++)
			{
				phaseAttackTime = NPC.MoonLordAttacksArray[0, handType, 1, attackIncrement];
				if (phaseAttackTime + (float)totalAttackTimer > base.NPC.ai[1])
				{
					break;
				}
				totalAttackTimer += (int)phaseAttackTime;
			}
			if (attackIncrement == 5)
			{
				attackIncrement = 0;
				base.NPC.ai[1] = 0f;
				phaseAttackTime = NPC.MoonLordAttacksArray[0, handType, 1, attackIncrement];
				totalAttackTimer = 0;
			}
			base.NPC.ai[0] = NPC.MoonLordAttacksArray[0, handType, 0, attackIncrement];
			handAttackTimer = (int)base.NPC.ai[1] - totalAttackTimer;
			if (base.NPC.ai[0] != ai0CrossCheck)
			{
				base.NPC.netUpdate = true;
			}
		}
		if (base.NPC.ai[0] == -2f)
		{
			handFrameCheck = 0;
			base.NPC.dontTakeDamage = true;
			base.NPC.velocity = Main.npc[(int)base.NPC.ai[3]].velocity;
		}
		else if (base.NPC.ai[0] == 0f)
		{
			handFrameCheck = 3;
			base.NPC.localAI[1] -= 0.05f;
			if (base.NPC.localAI[1] < 0f)
			{
				base.NPC.localAI[1] = 0f;
			}
			Vector2 handMovementDirection = coreCenter + new Vector2(350f * handFaceDirection, -100f) - base.NPC.Center;
			if (((Vector2)(ref handMovementDirection)).Length() > 20f)
			{
				((Vector2)(ref handMovementDirection)).Normalize();
				float velocity = 7.5f;
				switch (aggressionLevel)
				{
				case 6:
					velocity += 3f;
					break;
				case 5:
					velocity += 1.5f;
					break;
				case 3:
					velocity -= 0.4f;
					break;
				case 2:
					velocity -= 0.8f;
					break;
				case 1:
					velocity -= 1.2f;
					break;
				}
				handMovementDirection *= velocity;
				Vector2 currentVelocity = base.NPC.velocity;
				if (handMovementDirection != Vector2.Zero)
				{
					base.NPC.SimpleFlyMovement(handMovementDirection, 0.3f);
				}
				base.NPC.velocity = Vector2.Lerp(currentVelocity, base.NPC.velocity, 0.5f);
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			handFrameCheck = 0;
			float divisor = 6f;
			switch (aggressionLevel)
			{
			case 6:
				divisor = 4f;
				break;
			case 5:
				divisor = 5f;
				break;
			case 3:
				divisor = 8f;
				break;
			case 2:
				divisor = 10f;
				break;
			case 1:
				divisor = 12f;
				break;
			}
			if (handAttackTimer >= 56f)
			{
				base.NPC.localAI[1] -= 0.07f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
			}
			else if (handAttackTimer >= 28f)
			{
				base.NPC.localAI[1] += 0.05f;
				if (base.NPC.localAI[1] > 0.75f)
				{
					base.NPC.localAI[1] = 0.75f;
				}
				float handPauseDirection = (float)Math.PI * 2f * (handAttackTimer % 28f) / 28f - (float)Math.PI / 2f;
				base.NPC.localAI[0] = Utils.ToRotation(new Vector2(MathF.Cos(handPauseDirection) * eyeSizeVector.X, MathF.Sin(handPauseDirection) * eyeSizeVector.Y));
				if (handAttackTimer % divisor == 0f)
				{
					float velocity2 = 3f;
					switch (aggressionLevel)
					{
					case 3:
						velocity2 += 0.5f;
						break;
					case 2:
						velocity2++;
						break;
					case 1:
						velocity2 += 1.5f;
						break;
					}
					Vector2 eyeDirection = Utils.Vector2FromElipse(base.NPC.localAI[0].ToRotationVector2(), eyeSizeVector * base.NPC.localAI[1]);
					Vector2 eyeSpawn = base.NPC.Center + Vector2.Normalize(eyeDirection) * ((Vector2)(ref eyeSizeVector)).Length() * 0.4f + new Vector2(0f - handFaceDirection, 3f);
					Vector2 eyeVelocity = Vector2.Normalize(eyeDirection) * velocity2;
					float ai = (Main.rand.NextFloat((float)Math.PI * 2f) - (float)Math.PI) / 30f + (float)Math.PI / 180f * handFaceDirection;
					int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), eyeSpawn, eyeVelocity, 452, EyeDamage, 0f, Main.myPlayer, 0f, ai, aggressionLevel);
					Main.projectile[proj].timeLeft = 1200;
				}
			}
			else
			{
				base.NPC.localAI[1] += 0.02f;
				if (base.NPC.localAI[1] > 0.75f)
				{
					base.NPC.localAI[1] = 0.75f;
				}
				float handPauseDirection2 = (float)Math.PI * 2f * (handAttackTimer % 28f) / 28f - (float)Math.PI / 2f;
				base.NPC.localAI[0] = Utils.ToRotation(new Vector2(MathF.Cos(handPauseDirection2) * eyeSizeVector.X, MathF.Sin(handPauseDirection2) * eyeSizeVector.Y));
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.localAI[1] -= 0.05f;
			if (base.NPC.localAI[1] < 0f)
			{
				base.NPC.localAI[1] = 0f;
			}
			Vector2 sphereHandDirection = coreCenter + new Vector2(320f * handFaceDirection, -110f);
			Vector2 sphereHandDirectionMaxBound = default(Vector2);
			((Vector2)(ref sphereHandDirectionMaxBound))._002Ector(400f * handFaceDirection, -60f);
			float velocityMultiplier = 0.885f;
			switch (aggressionLevel)
			{
			case 6:
				velocityMultiplier -= 0.04f;
				break;
			case 5:
				velocityMultiplier -= 0.02f;
				break;
			case 3:
				velocityMultiplier += 0.004f;
				break;
			case 2:
				velocityMultiplier += 0.008f;
				break;
			case 1:
				velocityMultiplier += 0.012f;
				break;
			}
			if (handAttackTimer < 30f)
			{
				Vector2 sphereHandTravelVelocity = sphereHandDirection - base.NPC.Center;
				if (sphereHandTravelVelocity != Vector2.Zero)
				{
					Vector2 sphereHandTravelDist = Vector2.Normalize(sphereHandTravelVelocity);
					float velocity3 = 10f;
					switch (aggressionLevel)
					{
					case 6:
						velocity3 += 3f;
						break;
					case 5:
						velocity3 += 1.5f;
						break;
					case 3:
						velocity3 -= 0.5f;
						break;
					case 2:
						velocity3--;
						break;
					case 1:
						velocity3 -= 1.5f;
						break;
					}
					base.NPC.velocity = Vector2.SmoothStep(base.NPC.velocity, sphereHandTravelDist * Math.Min(velocity3, ((Vector2)(ref sphereHandTravelVelocity)).Length()), 0.2f);
				}
			}
			else if (handAttackTimer < 210f)
			{
				handFrameCheck = 1;
				int sphereHandSpeed = (int)handAttackTimer - 30;
				int divisor2 = 30;
				switch (aggressionLevel)
				{
				case 6:
					divisor2 = 20;
					break;
				case 5:
					divisor2 = 25;
					break;
				case 3:
					divisor2 = 45;
					break;
				case 2:
					divisor2 = 60;
					break;
				case 1:
					divisor2 = 90;
					break;
				}
				if (sphereHandSpeed % divisor2 == 0 && Main.netMode != 1)
				{
					int finalSphereHandSpeed = sphereHandSpeed / 30;
					Vector2 sphereFireDirection = default(Vector2);
					((Vector2)(ref sphereFireDirection))._002Ector(5f * handFaceDirection, (float)finalSphereHandSpeed - 12.5f);
					sphereFireDirection.X += ((float)finalSphereHandSpeed - 3.5f) * handFaceDirection * 3f;
					sphereFireDirection *= 1.2f;
					int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, sphereFireDirection, 454, SphereDamage, 1f, Main.myPlayer, 0f, base.NPC.whoAmI);
					Main.projectile[proj2].timeLeft = 1200;
				}
				Vector2 handSmoothMovement = Vector2.SmoothStep(sphereHandDirection, sphereHandDirection + sphereHandDirectionMaxBound, (handAttackTimer - 30f) / 180f) - base.NPC.Center;
				if (handSmoothMovement != Vector2.Zero)
				{
					Vector2 handSmoothMoveNormalize = handSmoothMovement;
					((Vector2)(ref handSmoothMoveNormalize)).Normalize();
					float velocity4 = 24f;
					switch (aggressionLevel)
					{
					case 6:
						velocity4 += 3f;
						break;
					case 5:
						velocity4 += 1.5f;
						break;
					case 3:
						velocity4--;
						break;
					case 2:
						velocity4 -= 2f;
						break;
					case 1:
						velocity4 -= 3f;
						break;
					}
					base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, handSmoothMoveNormalize * Math.Min(velocity4, ((Vector2)(ref handSmoothMovement)).Length()), 0.5f);
				}
			}
			else if (handAttackTimer < 282f)
			{
				handFrameCheck = 0;
				NPC nPC = base.NPC;
				nPC.velocity *= velocityMultiplier;
			}
			else if (handAttackTimer < 287f)
			{
				handFrameCheck = 1;
				NPC nPC2 = base.NPC;
				nPC2.velocity *= velocityMultiplier;
			}
			else if (handAttackTimer < 292f)
			{
				handFrameCheck = 2;
				NPC nPC3 = base.NPC;
				nPC3.velocity *= velocityMultiplier;
			}
			else if (handAttackTimer < 300f)
			{
				handFrameCheck = 3;
				if (handAttackTimer == 292f && Main.netMode != 1)
				{
					int closestPlayer = Player.FindClosest(base.NPC.position, base.NPC.width, base.NPC.height);
					Vector2 sphereVelocity = (Main.player[closestPlayer].Center - (base.NPC.Center + Vector2.UnitY * -350f)).SafeNormalize(Vector2.UnitY);
					float velocity5 = 2f;
					switch (aggressionLevel)
					{
					case 6:
						velocity5 += 3f;
						break;
					case 5:
						velocity5 += 1.5f;
						break;
					case 3:
						velocity5 -= 0.25f;
						break;
					case 2:
						velocity5 -= 0.5f;
						break;
					case 1:
						velocity5 -= 0.75f;
						break;
					}
					sphereVelocity *= velocity5;
					ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Projectile sp = enumerator.Current;
						if (sp.type == 454 && sp.ai[1] == (float)base.NPC.whoAmI && sp.ai[0] != -1f)
						{
							sp.ai[0] = -1f;
							sp.velocity = sphereVelocity;
							sp.netUpdate = true;
						}
					}
				}
				Vector2 handPauseSmoothSpeed = Vector2.SmoothStep(sphereHandDirection, sphereHandDirection + sphereHandDirectionMaxBound, 1f - (handAttackTimer - 270f) / 30f) - base.NPC.Center;
				if (handPauseSmoothSpeed != Vector2.Zero)
				{
					Vector2 handPauseDirection3 = handPauseSmoothSpeed;
					((Vector2)(ref handPauseDirection3)).Normalize();
					float velocity6 = 17.5f;
					switch (aggressionLevel)
					{
					case 6:
						velocity6 += 3f;
						break;
					case 5:
						velocity6 += 1.5f;
						break;
					case 3:
						velocity6--;
						break;
					case 2:
						velocity6 -= 2f;
						break;
					case 1:
						velocity6 -= 3f;
						break;
					}
					base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, handPauseDirection3 * Math.Min(velocity6, ((Vector2)(ref handPauseSmoothSpeed)).Length()), 0.1f);
				}
			}
			else
			{
				handFrameCheck = 3;
				Vector2 handReturnSmoothSpeed = sphereHandDirection - base.NPC.Center;
				if (handReturnSmoothSpeed != Vector2.Zero)
				{
					Vector2 handReturnDirection = handReturnSmoothSpeed;
					((Vector2)(ref handReturnDirection)).Normalize();
					float velocity7 = 10f;
					switch (aggressionLevel)
					{
					case 6:
						velocity7 += 3f;
						break;
					case 5:
						velocity7 += 1.5f;
						break;
					case 3:
						velocity7 -= 0.5f;
						break;
					case 2:
						velocity7--;
						break;
					case 1:
						velocity7 -= 1.5f;
						break;
					}
					base.NPC.velocity = Vector2.SmoothStep(base.NPC.velocity, handReturnDirection * Math.Min(velocity7, ((Vector2)(ref handReturnSmoothSpeed)).Length()), 0.2f);
				}
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			if (handAttackTimer == 0f)
			{
				base.NPC.TargetClosest(faceTarget: false);
				base.NPC.netUpdate = true;
			}
			Vector2 aimDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
			bool shootFirstBolt = handAttackTimer == phaseAttackTime - 14f;
			bool shootSecondBolt = handAttackTimer == phaseAttackTime - 7f;
			bool shootThirdBolt = handAttackTimer == phaseAttackTime;
			switch (aggressionLevel)
			{
			case 2:
			case 3:
				shootSecondBolt = false;
				break;
			case 1:
				shootSecondBolt = false;
				shootThirdBolt = false;
				break;
			}
			base.NPC.localAI[0] = base.NPC.localAI[0].AngleLerp(aimDirection.ToRotation(), 0.5f);
			base.NPC.localAI[1] += 0.05f;
			if (base.NPC.localAI[1] > 1f)
			{
				base.NPC.localAI[1] = 1f;
			}
			if (handAttackTimer == phaseAttackTime - 35f)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath6, base.NPC.Center);
			}
			if ((shootFirstBolt | shootSecondBolt | shootThirdBolt) && Main.netMode != 1)
			{
				Vector2 boltShootDirection = Utils.Vector2FromElipse(base.NPC.localAI[0].ToRotationVector2(), eyeSizeVector * base.NPC.localAI[1]);
				float velocity8 = 6.25f;
				switch (aggressionLevel)
				{
				case 6:
					velocity8 += 1.5f;
					break;
				case 5:
					velocity8 += 0.75f;
					break;
				case 3:
					velocity8 -= 0.25f;
					break;
				case 2:
					velocity8 -= 0.5f;
					break;
				case 1:
					velocity8 -= 0.75f;
					break;
				}
				Vector2 boltShootSpeed = Vector2.Normalize(aimDirection) * velocity8;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + boltShootDirection, boltShootSpeed, 462, BoltDamage, 0f, Main.myPlayer);
			}
		}
		Vector2 minHandFaceDirection = coreCenter + new Vector2(220f * handFaceDirection, -60f) + new Vector2(handFaceDirection * 110f, -150f);
		Vector2 maxHandFaceDirection = minHandFaceDirection + new Vector2(handFaceDirection * 370f, 150f);
		if (minHandFaceDirection.X > maxHandFaceDirection.X)
		{
			Utils.Swap(ref minHandFaceDirection.X, ref maxHandFaceDirection.X);
		}
		if (minHandFaceDirection.Y > maxHandFaceDirection.Y)
		{
			Utils.Swap(ref minHandFaceDirection.Y, ref maxHandFaceDirection.Y);
		}
		Vector2 defaultHandVelocity = Vector2.Clamp(base.NPC.Center + base.NPC.velocity, minHandFaceDirection, maxHandFaceDirection);
		if (defaultHandVelocity != base.NPC.Center + base.NPC.velocity)
		{
			base.NPC.Center = defaultHandVelocity - base.NPC.velocity;
		}
		int num = handFrameCheck * 7;
		if ((double)num > base.NPC.frameCounter)
		{
			double handFrameControl = base.NPC.frameCounter;
			base.NPC.frameCounter = handFrameControl + 1.0;
		}
		if ((double)num < base.NPC.frameCounter)
		{
			double handFrameControl2 = base.NPC.frameCounter;
			base.NPC.frameCounter = handFrameControl2 - 1.0;
		}
		if (base.NPC.frameCounter < 0.0)
		{
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frameCounter > 21.0)
		{
			base.NPC.frameCounter = 21.0;
		}
	}

	public void BuffedTrueEyeAI()
	{
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_10db: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1034: Unknown result type (might be due to invalid IL or missing references)
		//IL_1039: Unknown result type (might be due to invalid IL or missing references)
		//IL_1041: Unknown result type (might be due to invalid IL or missing references)
		//IL_1048: Unknown result type (might be due to invalid IL or missing references)
		//IL_104d: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1277: Unknown result type (might be due to invalid IL or missing references)
		//IL_127c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1619: Unknown result type (might be due to invalid IL or missing references)
		//IL_1623: Unknown result type (might be due to invalid IL or missing references)
		//IL_1628: Unknown result type (might be due to invalid IL or missing references)
		//IL_164a: Unknown result type (might be due to invalid IL or missing references)
		//IL_164f: Unknown result type (might be due to invalid IL or missing references)
		//IL_099a: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_140a: Unknown result type (might be due to invalid IL or missing references)
		//IL_140f: Unknown result type (might be due to invalid IL or missing references)
		//IL_143c: Unknown result type (might be due to invalid IL or missing references)
		//IL_143e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1445: Unknown result type (might be due to invalid IL or missing references)
		//IL_144a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_180c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1811: Unknown result type (might be due to invalid IL or missing references)
		//IL_1816: Unknown result type (might be due to invalid IL or missing references)
		//IL_1818: Unknown result type (might be due to invalid IL or missing references)
		//IL_182d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1849: Unknown result type (might be due to invalid IL or missing references)
		//IL_184f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1851: Unknown result type (might be due to invalid IL or missing references)
		//IL_1856: Unknown result type (might be due to invalid IL or missing references)
		//IL_1871: Unknown result type (might be due to invalid IL or missing references)
		//IL_1876: Unknown result type (might be due to invalid IL or missing references)
		//IL_18be: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1527: Unknown result type (might be due to invalid IL or missing references)
		//IL_1529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1692: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1708: Unknown result type (might be due to invalid IL or missing references)
		//IL_170d: Unknown result type (might be due to invalid IL or missing references)
		//IL_170f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1714: Unknown result type (might be due to invalid IL or missing references)
		//IL_171e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1732: Unknown result type (might be due to invalid IL or missing references)
		//IL_1737: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		if (Main.npc[(int)base.NPC.ai[3]].ai[0] == 2f)
		{
			base.NPC.HitEffect(0, 9999.0);
			base.NPC.active = false;
		}
		if (calamityGlobalNPC.newAI[0] == 0f)
		{
			int eyeCount = NPC.CountNPCS(base.NPC.type);
			if (eyeCount > 1)
			{
				int eyesSynced = 1;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type)
					{
						n.ai[0] = 0f;
						n.ai[1] = 0f;
						n.ai[2] = 0f;
						n.localAI[0] = 0f;
						n.localAI[1] = 0f;
						n.localAI[2] = 0f;
						calamityGlobalNPC.newAI[0] = 1f;
						calamityGlobalNPC.newAI[1] = 0f;
						base.NPC.netUpdate = true;
						eyesSynced++;
						if (eyesSynced >= eyeCount)
						{
							break;
						}
					}
				}
			}
			else
			{
				calamityGlobalNPC.newAI[0] = 1f;
			}
		}
		if (Main.rand.NextBool(420))
		{
			SoundEngine.PlaySound(Main.rand.NextBool() ? SoundID.Zombie100 : SoundID.Zombie101, base.NPC.Center);
		}
		if (!Main.npc[(int)base.NPC.ai[3]].active || Main.npc[(int)base.NPC.ai[3]].type != 398)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
		}
		Vector2 eyeSizeVector = default(Vector2);
		((Vector2)(ref eyeSizeVector))._002Ector(30f);
		float phaseAttackTime = 0f;
		float ai0CrossCheck = base.NPC.ai[0];
		base.NPC.ai[1]++;
		int attackIncrement = 0;
		int totalAttackTimer = 0;
		for (; attackIncrement < 10; attackIncrement++)
		{
			phaseAttackTime = NPC.MoonLordAttacksArray2[1, attackIncrement];
			if (phaseAttackTime + (float)totalAttackTimer > base.NPC.ai[1])
			{
				break;
			}
			totalAttackTimer += (int)phaseAttackTime;
		}
		if (attackIncrement == 10)
		{
			attackIncrement = 0;
			base.NPC.ai[1] = 0f;
			phaseAttackTime = NPC.MoonLordAttacksArray2[1, attackIncrement];
			totalAttackTimer = 0;
		}
		base.NPC.ai[0] = NPC.MoonLordAttacksArray2[0, attackIncrement];
		float secondAttackTimer = (int)base.NPC.ai[1] - totalAttackTimer;
		if (base.NPC.ai[0] != ai0CrossCheck)
		{
			base.NPC.netUpdate = true;
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] > 180f)
			{
				base.NPC.ai[1] = 0f;
			}
			float localAI2Lerp;
			if (base.NPC.ai[1] < 60f)
			{
				localAI2Lerp = 0.75f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = (float)Math.Sin(base.NPC.ai[1] * ((float)Math.PI * 2f) / 15f) * 0.35f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[0] = (float)Math.PI;
				}
			}
			else if (base.NPC.ai[1] < 120f)
			{
				localAI2Lerp = 1f;
				if (base.NPC.localAI[1] < 0.5f)
				{
					base.NPC.localAI[1] += 0.025f;
				}
				base.NPC.localAI[0] += (float)Math.PI / 15f;
			}
			else
			{
				localAI2Lerp = 1.15f;
				base.NPC.localAI[1] -= 0.05f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
			}
			base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], localAI2Lerp, 0.3f);
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			Vector2 v7 = Main.player[base.NPC.target].Center - base.NPC.Center;
			base.NPC.localAI[0] = base.NPC.localAI[0].AngleLerp(v7.ToRotation(), 0.5f);
			base.NPC.localAI[1] += 0.05f;
			if (base.NPC.localAI[1] > 0.7f)
			{
				base.NPC.localAI[1] = 0.7f;
			}
			base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 1f, 0.2f);
			float velocity = 36f;
			Vector2 freeEyeTargetCenter = Main.player[base.NPC.target].Center;
			Vector2 freeEyeTargetDistance = (freeEyeTargetCenter - base.NPC.Center).SafeNormalize(Vector2.Zero) * velocity;
			if (Vector2.Distance(base.NPC.Center, freeEyeTargetCenter) > 300f)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * 29f + freeEyeTargetDistance.X) / 30f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 29f + freeEyeTargetDistance.Y) / 30f;
			}
			else
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.8f;
				if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
				{
					base.NPC.velocity = Vector2.Zero;
				}
			}
			float freeEyeAccel = 0.5f;
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				NPC n2 = enumerator2.Current;
				if (n2.whoAmI != base.NPC.whoAmI && n2.type == base.NPC.type && Vector2.Distance(base.NPC.Center, n2.Center) < 150f)
				{
					if (base.NPC.position.X < n2.position.X)
					{
						base.NPC.velocity.X -= freeEyeAccel;
					}
					else
					{
						base.NPC.velocity.X += freeEyeAccel;
					}
					if (base.NPC.position.Y < n2.position.Y)
					{
						base.NPC.velocity.Y -= freeEyeAccel;
					}
					else
					{
						base.NPC.velocity.Y += freeEyeAccel;
					}
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			if (secondAttackTimer == 0f)
			{
				base.NPC.TargetClosest(faceTarget: false);
				base.NPC.netUpdate = true;
			}
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.95f;
			if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
			{
				base.NPC.velocity = Vector2.Zero;
			}
			Vector2 aimDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
			base.NPC.localAI[0] = base.NPC.localAI[0].AngleLerp(aimDirection.ToRotation(), 0.5f);
			base.NPC.localAI[1] += 0.05f;
			if (base.NPC.localAI[1] > 1f)
			{
				base.NPC.localAI[1] = 1f;
			}
			if (secondAttackTimer < 20f)
			{
				base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 1.1f, 0.2f);
			}
			else
			{
				base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 0.4f, 0.2f);
			}
			if (secondAttackTimer == phaseAttackTime - 35f)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath6, base.NPC.Center);
			}
			if ((secondAttackTimer == phaseAttackTime - 14f || secondAttackTimer == phaseAttackTime - 7f || secondAttackTimer == phaseAttackTime) && Main.netMode != 1)
			{
				Vector2 freeEyeBoltDirection = Utils.Vector2FromElipse(base.NPC.localAI[0].ToRotationVector2(), eyeSizeVector * base.NPC.localAI[1]);
				float velocity2 = 8f;
				Vector2 freeEyeBoltVel = Vector2.Normalize(aimDirection) * velocity2;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + freeEyeBoltDirection, freeEyeBoltVel, 462, TrueEyeBoltDamage, 0f, Main.myPlayer);
			}
		}
		else if (base.NPC.ai[0] == 2f || base.NPC.ai[0] == 4f)
		{
			int type = 454;
			if (secondAttackTimer < 15f)
			{
				base.NPC.localAI[1] -= 0.07f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
				base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 0.4f, 0.2f);
				NPC nPC3 = base.NPC;
				nPC3.velocity *= 0.8f;
				if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
				{
					base.NPC.velocity = Vector2.Zero;
				}
			}
			else if (secondAttackTimer < 75f)
			{
				float freeEyeAttackPattern = (secondAttackTimer - 15f) / 10f;
				int freeEyeRotateValue = 0;
				int freeEyeRotateTransition = 0;
				switch ((int)freeEyeAttackPattern)
				{
				case 0:
					freeEyeRotateValue = 0;
					freeEyeRotateTransition = 2;
					break;
				case 1:
					freeEyeRotateValue = 2;
					freeEyeRotateTransition = 5;
					break;
				case 2:
					freeEyeRotateValue = 5;
					freeEyeRotateTransition = 3;
					break;
				case 3:
					freeEyeRotateValue = 3;
					freeEyeRotateTransition = 1;
					break;
				case 4:
					freeEyeRotateValue = 1;
					freeEyeRotateTransition = 4;
					break;
				case 5:
					freeEyeRotateValue = 4;
					freeEyeRotateTransition = 0;
					break;
				}
				Vector2 spinningpoint = -Vector2.UnitY * 30f;
				Vector2 freeEyeRotateLerp = spinningpoint.RotatedBy((float)freeEyeRotateValue * ((float)Math.PI * 2f) / 6f);
				Vector2 freeEyeTransitionLerp = spinningpoint.RotatedBy((float)freeEyeRotateTransition * ((float)Math.PI * 2f) / 6f);
				Vector2 freeEyeRotation = Vector2.Lerp(freeEyeRotateLerp, freeEyeTransitionLerp, freeEyeAttackPattern - (float)(int)freeEyeAttackPattern);
				float freeEyeRotationDist = ((Vector2)(ref freeEyeRotation)).Length() / 30f;
				base.NPC.localAI[0] = freeEyeRotation.ToRotation();
				base.NPC.localAI[1] = MathHelper.Lerp(base.NPC.localAI[1], freeEyeRotationDist, 0.5f);
				for (int k = 0; k < 2; k++)
				{
					Dust dust = Dust.NewDustDirect(base.NPC.Center + freeEyeRotation - Vector2.One * 4f, 0, 0, 229);
					dust.velocity += freeEyeRotation / 15f;
					dust.noGravity = true;
				}
				if ((secondAttackTimer - 15f) % 10f != 0f || Main.netMode == 1)
				{
					return;
				}
				Vector2 trueEyeSphereDirection = freeEyeRotation.SafeNormalize(-Vector2.UnitY) * 4f;
				int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + freeEyeRotation, trueEyeSphereDirection, type, 0, 0f, Main.myPlayer, 30f, base.NPC.whoAmI);
				Main.projectile[proj].timeLeft = 1200;
				if (!Main.zenithWorld)
				{
					return;
				}
				for (int i = 0; i < 3; i++)
				{
					if (!WorldGen.SolidTile((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f)))
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, Main.rand.NextFloat(-16f, 16f), Main.rand.NextFloat(-16f, 0f), 1021, MoonBoulderDamage, 10f);
					}
				}
			}
			else if (secondAttackTimer < 105f)
			{
				base.NPC.localAI[0] = base.NPC.localAI[0].AngleLerp(base.NPC.ai[2] - (float)Math.PI / 2f, 0.2f);
				base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 0.75f, 0.2f);
				if (secondAttackTimer == 75f)
				{
					base.NPC.TargetClosest(faceTarget: false);
					base.NPC.netUpdate = true;
					base.NPC.velocity = -Vector2.UnitY * 7f;
					ActiveEntityIterator<Projectile>.Enumerator enumerator3 = Main.ActiveProjectiles.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						Projectile sp = enumerator3.Current;
						if (sp.type == type && sp.ai[1] == (float)base.NPC.whoAmI && sp.ai[0] != -1f)
						{
							sp.velocity += base.NPC.velocity;
							sp.netUpdate = true;
						}
					}
				}
				base.NPC.velocity.Y = base.NPC.velocity.Y * 0.96f;
				base.NPC.ai[2] = (Main.player[base.NPC.target].Center - base.NPC.Center).ToRotation() + (float)Math.PI / 2f;
				base.NPC.rotation = base.NPC.rotation.AngleTowards(base.NPC.ai[2], (float)Math.PI / 30f);
			}
			else if (secondAttackTimer < 120f)
			{
				SoundEngine.PlaySound(in SoundID.Zombie102, base.NPC.Center);
				if (secondAttackTimer == 105f)
				{
					base.NPC.netUpdate = true;
				}
				float velocity3 = 12f;
				Vector2 trueEyeSphereVelocity = (base.NPC.ai[2] - (float)Math.PI / 2f).ToRotationVector2() * velocity3;
				base.NPC.velocity = trueEyeSphereVelocity * 2f;
				ActiveEntityIterator<Projectile>.Enumerator enumerator4 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator4.MoveNext())
				{
					Projectile sp2 = enumerator4.Current;
					if (sp2.type == type && sp2.ai[1] == (float)base.NPC.whoAmI && sp2.ai[0] != -1f)
					{
						sp2.ai[0] = -1f;
						sp2.damage = TrueEyeSphereDamage;
						sp2.velocity = trueEyeSphereVelocity;
						sp2.netUpdate = true;
					}
				}
			}
			else
			{
				NPC nPC4 = base.NPC;
				nPC4.velocity *= 0.92f;
				base.NPC.rotation = base.NPC.rotation.AngleLerp(0f, 0.2f);
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			if (secondAttackTimer < 15f)
			{
				base.NPC.localAI[1] -= 0.07f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
				base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 0.4f, 0.2f);
				NPC nPC5 = base.NPC;
				nPC5.velocity *= 0.9f;
				if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
				{
					base.NPC.velocity = Vector2.Zero;
				}
				return;
			}
			if (secondAttackTimer < 45f)
			{
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = (float)Math.Sin((secondAttackTimer - 15f) * ((float)Math.PI * 2f) / 15f) * 0.5f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[0] = (float)Math.PI;
				}
				return;
			}
			if (secondAttackTimer >= 185f)
			{
				NPC nPC6 = base.NPC;
				nPC6.velocity *= 0.88f;
				base.NPC.rotation = base.NPC.rotation.AngleLerp(0f, 0.2f);
				base.NPC.localAI[1] -= 0.07f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
				base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 1f, 0.2f);
				return;
			}
			if (secondAttackTimer == 45f)
			{
				base.NPC.ai[2] = (float)Main.rand.NextBool().ToDirectionInt() * ((float)Math.PI * 2f) / 40f;
				base.NPC.netUpdate = true;
			}
			if ((secondAttackTimer - 15f - 30f) % 40f == 0f)
			{
				base.NPC.ai[2] *= 0.95f;
			}
			base.NPC.localAI[0] += base.NPC.ai[2];
			base.NPC.localAI[1] += 0.05f;
			if (base.NPC.localAI[1] > 1f)
			{
				base.NPC.localAI[1] = 1f;
			}
			Vector2 trueEyeDirection = base.NPC.localAI[0].ToRotationVector2() * eyeSizeVector * base.NPC.localAI[1];
			float trueEyeVelScale = MathHelper.Lerp(8f, 20f, (secondAttackTimer - 15f - 30f) / 140f);
			base.NPC.velocity = Vector2.Normalize(trueEyeDirection) * trueEyeVelScale;
			base.NPC.rotation = base.NPC.rotation.AngleLerp(base.NPC.velocity.ToRotation() + (float)Math.PI / 2f, 0.2f);
			if ((secondAttackTimer - 45f) % 10f == 0f && Main.netMode != 1)
			{
				Vector2 trueEyeEyeDirection = base.NPC.Center + Vector2.Normalize(trueEyeDirection) * ((Vector2)(ref eyeSizeVector)).Length() * 0.4f;
				Vector2 trueEyeEyeSpeed = Vector2.Normalize(trueEyeDirection) * 5f;
				float ai1 = (Main.rand.NextFloat((float)Math.PI * 2f) - (float)Math.PI) / 30f + (float)Math.PI / 180f * base.NPC.ai[2];
				int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), trueEyeEyeDirection, trueEyeEyeSpeed, 452, TrueEyeEyeDamage, 0f, Main.myPlayer, 0f, ai1);
				Main.projectile[proj2].timeLeft = 1200;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 4f)
			{
				return;
			}
			if (secondAttackTimer == 0f)
			{
				base.NPC.TargetClosest(faceTarget: false);
				base.NPC.netUpdate = true;
			}
			if (secondAttackTimer < 180f)
			{
				base.NPC.localAI[2] = MathHelper.Lerp(base.NPC.localAI[2], 1f, 0.2f);
				base.NPC.localAI[1] -= 0.05f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
				NPC nPC7 = base.NPC;
				nPC7.velocity *= 0.95f;
				if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
				{
					base.NPC.velocity = Vector2.Zero;
				}
				if (secondAttackTimer >= 60f)
				{
					int dustAmt = ((!(secondAttackTimer >= 120f)) ? 1 : 2);
					for (int j = 0; j < dustAmt; j++)
					{
						float dustScale = ((j % 2 == 1) ? 1.65f : 0.8f);
						Vector2 trueEyeDustDirection = base.NPC.Center + Main.rand.NextFloat((float)Math.PI * 2f).ToRotationVector2() * eyeSizeVector / 2f;
						Vector2 position = trueEyeDustDirection - Vector2.One * 8f;
						float scale = dustScale;
						Dust dust2 = Dust.NewDustDirect(position, 16, 16, 229, 0f, 0f, 0, default(Color), scale);
						dust2.velocity = Vector2.Normalize(base.NPC.Center - trueEyeDustDirection) * 0.35f * (10f - (float)dustAmt * 2f);
						dust2.noGravity = true;
						dust2.customData = base.NPC;
					}
				}
			}
			else if (secondAttackTimer < phaseAttackTime - 15f)
			{
				if (calamityGlobalNPC.newAI[1] == 0f)
				{
					calamityGlobalNPC.newAI[1] = 600f;
				}
				if (secondAttackTimer == 180f && Main.netMode != 1)
				{
					if (Main.npc[(int)Main.npc[(int)base.NPC.ai[3]].localAI[2]].ai[0] == 1f)
					{
						calamityGlobalNPC.newAI[1] *= 1.5f;
					}
					base.NPC.TargetClosest(faceTarget: false);
					Vector2 deathrayTargetDist = base.NPC.Center.DirectionTo(Main.player[base.NPC.target].Center);
					int deathraySweepDirection = (deathrayTargetDist.X < 0f).ToDirectionInt();
					deathrayTargetDist = deathrayTargetDist.RotatedBy((0.0 - (double)deathraySweepDirection) * 6.2831854820251465 / 6.0);
					int type2 = 455;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, deathrayTargetDist, type2, TrueEyeDeathrayDamage, 0f, Main.myPlayer, (float)deathraySweepDirection * ((float)Math.PI * 2f) / calamityGlobalNPC.newAI[1], base.NPC.whoAmI);
					base.NPC.ai[2] = (deathrayTargetDist.ToRotation() + (float)Math.PI + (float)Math.PI * 2f) * (float)deathraySweepDirection;
					base.NPC.netUpdate = true;
				}
				base.NPC.localAI[1] += 0.05f;
				if (base.NPC.localAI[1] > 1f)
				{
					base.NPC.localAI[1] = 1f;
				}
				float deathrayRotationDirection = (base.NPC.ai[2] >= 0f).ToDirectionInt();
				float deathrayRotation = base.NPC.ai[2];
				if (deathrayRotation < 0f)
				{
					deathrayRotation *= -1f;
				}
				deathrayRotation += deathrayRotationDirection * ((float)Math.PI * 2f) / calamityGlobalNPC.newAI[1] - (float)Math.PI;
				base.NPC.localAI[0] = deathrayRotation;
				base.NPC.ai[2] = (deathrayRotation + (float)Math.PI) * deathrayRotationDirection;
			}
			else
			{
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.localAI[1] -= 0.07f;
				if (base.NPC.localAI[1] < 0f)
				{
					base.NPC.localAI[1] = 0f;
				}
			}
		}
	}

	public void BuffedMoonLeechBlobAI()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mouthMovement = Vector2.UnitY * 216f;
		int headIndex = (int)Math.Abs(base.NPC.ai[0]) - 1;
		int leechTongue = (int)base.NPC.ai[1];
		if (!Main.npc[headIndex].active || Main.npc[headIndex].type != 396)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			return;
		}
		base.NPC.ai[2]++;
		if (base.NPC.ai[2] >= 180f)
		{
			if (Main.netMode != 1)
			{
				int coreIndex = (int)Main.npc[headIndex].ai[3];
				int leftHandHeal = -1;
				int rightHandHeal = -1;
				int headHeal = headIndex;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.ai[3] == (float)coreIndex)
					{
						if (leftHandHeal == -1 && n.type == 397 && n.ai[2] == 0f)
						{
							leftHandHeal = n.whoAmI;
						}
						if (rightHandHeal == -1 && n.type == 397 && n.ai[2] == 1f)
						{
							rightHandHeal = n.whoAmI;
						}
						if (leftHandHeal != -1 && rightHandHeal != -1)
						{
							break;
						}
					}
				}
				int maxHealAmt = (CalamityWorld.death ? 1500 : 1250);
				int coreMissingHP = Main.npc[coreIndex].lifeMax - Main.npc[coreIndex].life;
				int leftHandMissingHP = Main.npc[leftHandHeal].lifeMax - Main.npc[leftHandHeal].life;
				int rightHandMissingHP = Main.npc[rightHandHeal].lifeMax - Main.npc[rightHandHeal].life;
				int headMissingHP = Main.npc[headHeal].lifeMax - Main.npc[headHeal].life;
				if (headMissingHP > 0 && maxHealAmt > 0)
				{
					int headHealthFailsafe = headMissingHP - maxHealAmt;
					if (headHealthFailsafe > 0)
					{
						headHealthFailsafe = 0;
					}
					int headHealingAmt = maxHealAmt + headHealthFailsafe;
					maxHealAmt -= headHealingAmt;
					Main.npc[headHeal].life += headHealingAmt;
					NPC.HealEffect(Utils.CenteredRectangle(Main.npc[headHeal].Center, new Vector2(50f)), headHealingAmt);
				}
				if (coreMissingHP > 0 && maxHealAmt > 0)
				{
					int coreHealthFailsafe = coreMissingHP - maxHealAmt;
					if (coreHealthFailsafe > 0)
					{
						coreHealthFailsafe = 0;
					}
					int coreHealingAmt = maxHealAmt + coreHealthFailsafe;
					maxHealAmt -= coreHealingAmt;
					Main.npc[coreIndex].life += coreHealingAmt;
					NPC.HealEffect(Utils.CenteredRectangle(Main.npc[coreIndex].Center, new Vector2(50f)), coreHealingAmt);
				}
				if (leftHandMissingHP > 0 && maxHealAmt > 0)
				{
					int leftHandHealthFailsafe = leftHandMissingHP - maxHealAmt;
					if (leftHandHealthFailsafe > 0)
					{
						leftHandHealthFailsafe = 0;
					}
					int leftHandHealingAmt = maxHealAmt + leftHandHealthFailsafe;
					maxHealAmt -= leftHandHealingAmt;
					Main.npc[leftHandHeal].life += leftHandHealingAmt;
					NPC.HealEffect(Utils.CenteredRectangle(Main.npc[leftHandHeal].Center, new Vector2(50f)), leftHandHealingAmt);
				}
				if (rightHandMissingHP > 0 && maxHealAmt > 0)
				{
					int rightHandHealthFailsafe = rightHandMissingHP - maxHealAmt;
					if (rightHandHealthFailsafe > 0)
					{
						rightHandHealthFailsafe = 0;
					}
					int rightHandHealingAmt = maxHealAmt + rightHandHealthFailsafe;
					Main.npc[rightHandHeal].life += rightHandHealingAmt;
					NPC.HealEffect(Utils.CenteredRectangle(Main.npc[rightHandHeal].Center, new Vector2(50f)), rightHandHealingAmt);
				}
			}
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
		}
		else
		{
			base.NPC.velocity = Vector2.Zero;
			base.NPC.Center = Vector2.Lerp(Main.projectile[leechTongue].Center, Main.npc[(int)Math.Abs(base.NPC.ai[0]) - 1].Center + mouthMovement, base.NPC.ai[2] / 180f);
			Vector2 basePos = -Vector2.UnitY * (float)base.NPC.height / 2f;
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.NPC.Center - Vector2.One * 4f + basePos.RotatedBy((float)i * ((float)Math.PI * 2f) / 6f), 229, -Vector2.UnitY, 0, default(Color), 0.7f);
				dust.noGravity = true;
				dust.customData = base.NPC;
			}
			basePos = -Vector2.UnitY * (float)base.NPC.height / 6f;
			for (int j = 0; j < 2; j++)
			{
				int leechDust2 = Dust.NewDust(base.NPC.Center - Vector2.One * 4f + basePos.RotatedBy((float)j * ((float)Math.PI * 2f) / 6f), 0, 0, 229, 0f, -2f, 0, default(Color), 1.5f);
				Main.dust[leechDust2].noGravity = true;
				Main.dust[leechDust2].customData = base.NPC;
			}
		}
	}
}
