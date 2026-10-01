using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.PlagueEnemies;

public class PlaguebringerMiniboss : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 12;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.7f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.8f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 70;
		base.NPC.npcSlots = 8f;
		base.NPC.width = 66;
		base.NPC.height = 66;
		base.NPC.defense = 24;
		base.NPC.lifeMax = 8750;
		base.NPC.value = Item.buyPrice(0, 1, 50);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.AnimationType = 222;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<PlaguebringerBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		if (Main.zenithWorld)
		{
			base.NPC.scale = 2f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.PlaguebringerMiniboss")
		});
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1001: Unknown result type (might be due to invalid IL or missing references)
		//IL_1008: Unknown result type (might be due to invalid IL or missing references)
		//IL_100d: Unknown result type (might be due to invalid IL or missing references)
		//IL_101b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1022: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Unknown result type (might be due to invalid IL or missing references)
		//IL_1036: Unknown result type (might be due to invalid IL or missing references)
		//IL_103b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0919: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d71: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec8: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.1f, 0.3f, 0f);
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		float distanceFromTarget = Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center);
		if (base.NPC.ai[0] != 6f)
		{
			if (base.NPC.timeLeft < 60)
			{
				base.NPC.timeLeft = 60;
			}
			if (distanceFromTarget > 3000f)
			{
				base.NPC.ai[0] = 4f;
			}
		}
		if (Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].ZoneJungle)
		{
			base.NPC.ai[0] = 6f;
		}
		if (base.NPC.ai[0] == 6f)
		{
			base.NPC.damage = 0;
			base.NPC.velocity.Y *= 0.98f;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			if (base.NPC.position.X < (float)(Main.maxTilesX * 8))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				else
				{
					base.NPC.localAI[0] = 1f;
				}
				base.NPC.velocity.X -= 0.08f;
			}
			else
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				else
				{
					base.NPC.localAI[0] = 1f;
				}
				base.NPC.velocity.X += 0.08f;
			}
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
		}
		else if (base.NPC.ai[0] == -1f)
		{
			if (Main.netMode != 1)
			{
				float currentAttack = base.NPC.ai[1];
				int nextAttack;
				do
				{
					nextAttack = Main.rand.Next(2);
				}
				while ((float)nextAttack == currentAttack);
				base.NPC.ai[0] = nextAttack;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				return;
			}
		}
		else if (base.NPC.ai[0] == 0f)
		{
			int chargeDistanceX = 750 - (CalamityWorld.death ? 100 : (CalamityWorld.revenge ? 75 : (Main.expertMode ? 50 : 0)));
			int chargeDelay = 2;
			if (base.NPC.ai[1] > (float)(2 * chargeDelay) && base.NPC.ai[1] % 2f == 0f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				return;
			}
			if (base.NPC.ai[1] % 2f == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				float chargeDistanceY = 20f;
				float distanceFromTargetX = Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X);
				if (Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) < chargeDistanceY && distanceFromTargetX >= (float)chargeDistanceX)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.localAI[0] = 1f;
					base.NPC.ai[1]++;
					base.NPC.ai[2] = 0f;
					Vector2 chargeBeePos = base.NPC.Center;
					float chargeTargetX = Main.player[base.NPC.target].Center.X - chargeBeePos.X;
					float chargeTargetY = Main.player[base.NPC.target].Center.Y - chargeBeePos.Y;
					float chargeTargetDist = (float)Math.Sqrt(chargeTargetX * chargeTargetX + chargeTargetY * chargeTargetY);
					chargeTargetDist = 16f / chargeTargetDist;
					base.NPC.velocity.X = chargeTargetX * chargeTargetDist;
					base.NPC.velocity.Y = chargeTargetY * chargeTargetDist;
					float playerLocation = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
					base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
					base.NPC.spriteDirection = base.NPC.direction;
					SoundEngine.PlaySound(in SoundID.Roar, base.NPC.Center);
					return;
				}
				base.NPC.localAI[0] = 0f;
				float chargeVelocity = 12f;
				float chargeAcceleration = 0.15f;
				if (base.NPC.Center.Y < Main.player[base.NPC.target].Center.Y - chargeDistanceY)
				{
					base.NPC.velocity.Y += chargeAcceleration;
				}
				else if (base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y + chargeDistanceY)
				{
					base.NPC.velocity.Y -= chargeAcceleration;
				}
				else
				{
					base.NPC.velocity.Y *= 0.7f;
				}
				if (base.NPC.velocity.Y < 0f - chargeVelocity)
				{
					base.NPC.velocity.Y = 0f - chargeVelocity;
				}
				if (base.NPC.velocity.Y > chargeVelocity)
				{
					base.NPC.velocity.Y = chargeVelocity;
				}
				float distanceXMax = 100f;
				float distanceXMin = 20f;
				if (distanceFromTargetX > (float)chargeDistanceX + distanceXMax)
				{
					base.NPC.velocity.X += chargeAcceleration * (float)base.NPC.direction;
				}
				else if (distanceFromTargetX < (float)chargeDistanceX + distanceXMin)
				{
					base.NPC.velocity.X -= chargeAcceleration * (float)base.NPC.direction;
				}
				else
				{
					base.NPC.velocity.X *= 0.7f;
				}
				if (base.NPC.velocity.X < 0f - chargeVelocity)
				{
					base.NPC.velocity.X = 0f - chargeVelocity;
				}
				if (base.NPC.velocity.X > chargeVelocity)
				{
					base.NPC.velocity.X = chargeVelocity;
				}
				float playerLocation2 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
				base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				int chargeDirection = 1;
				if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
				{
					chargeDirection = -1;
				}
				if (base.NPC.direction == chargeDirection && Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) > (float)chargeDistanceX)
				{
					base.NPC.ai[2] = 1f;
				}
				if (Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) > (float)chargeDistanceX * 1.5f)
				{
					base.NPC.ai[2] = 1f;
				}
				if (base.NPC.ai[2] != 1f)
				{
					base.NPC.localAI[0] = 1f;
					return;
				}
				base.NPC.damage = 0;
				float playerLocation3 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
				base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.TargetClosest();
				base.NPC.localAI[0] = 0f;
				NPC nPC = base.NPC;
				nPC.velocity *= 0.92f;
				float chargeDeceleration = 0.08f;
				if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < chargeDeceleration)
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[1]++;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			float playerLocation4 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
			base.NPC.direction = ((playerLocation4 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			float stingerAttackSpeed = 12f;
			float stingerAttackAccel = 0.12f;
			Vector2 stingerSpawnPos = default(Vector2);
			((Vector2)(ref stingerSpawnPos))._002Ector(base.NPC.Center.X + (float)(40 * base.NPC.direction), base.NPC.Bottom.Y + 30f);
			Vector2 stingerAttackPos = base.NPC.Center;
			float num = Main.player[base.NPC.target].Center.X - stingerAttackPos.X;
			float stingerAttackTargetY = Main.player[base.NPC.target].Center.Y - 300f - stingerAttackPos.Y;
			Math.Sqrt(num * num + stingerAttackTargetY * stingerAttackTargetY);
			bool canHitTarget = Collision.CanHit(new Vector2(stingerSpawnPos.X, stingerSpawnPos.Y - 30f), 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			Vector2 hoverDestination = Main.player[base.NPC.target].Center - Vector2.UnitY * ((!canHitTarget) ? 0f : 300f);
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(hoverDestination) * stingerAttackSpeed;
			base.NPC.ai[1]++;
			float stingerGateValue = (CalamityWorld.death ? 15f : (CalamityWorld.revenge ? 20f : (Main.expertMode ? 25f : 35f)));
			bool canFireStinger = false;
			if (base.NPC.ai[1] % stingerGateValue == stingerGateValue - 1f)
			{
				canFireStinger = true;
			}
			if (canFireStinger && base.NPC.position.Y + (float)base.NPC.height < Main.player[base.NPC.target].position.Y && Collision.CanHit(stingerSpawnPos, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				SoundEngine.PlaySound(in SoundID.Item42, stingerSpawnPos);
				if (Main.netMode != 1)
				{
					float stingerTargetX = Main.player[base.NPC.target].Center.X - stingerSpawnPos.X;
					float stingerTargetY = Main.player[base.NPC.target].Center.Y - stingerSpawnPos.Y;
					float stingerTargetDist = (float)Math.Sqrt(stingerTargetX * stingerTargetX + stingerTargetY * stingerTargetY);
					stingerTargetDist = 6f / stingerTargetDist;
					stingerTargetX *= stingerTargetDist;
					stingerTargetY *= stingerTargetDist;
					int rocketChance = (CalamityWorld.death ? 4 : (CalamityWorld.revenge ? 7 : (Main.expertMode ? 10 : 15)));
					bool fireRocket = Main.rand.NextBool(rocketChance);
					int type = (fireRocket ? ModContent.ProjectileType<HiveBombGoliath>() : ModContent.ProjectileType<PlagueStingerGoliathV2>());
					int damage = ((!Main.masterMode) ? ((!Main.expertMode) ? (fireRocket ? 72 : 52) : (fireRocket ? 50 : 35)) : (fireRocket ? 42 : 29));
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), stingerSpawnPos.X, stingerSpawnPos.Y, stingerTargetX, stingerTargetY, type, damage, 0f, Main.myPlayer);
				}
			}
			if (Vector2.Distance(stingerSpawnPos, hoverDestination) > 40f || !canHitTarget)
			{
				base.NPC.SimpleFlyMovement(idealVelocity, stingerAttackAccel);
			}
			float stingerPhaseTime = (CalamityWorld.death ? 180f : (CalamityWorld.revenge ? 240f : (Main.expertMode ? 300f : 600f)));
			if (base.NPC.ai[1] > stingerPhaseTime)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 1f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = 0;
			base.NPC.localAI[0] = 1f;
			float despawnVelMult = 14f;
			Vector2 despawnTargetDist = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
			despawnTargetDist *= 14f;
			base.NPC.velocity = (base.NPC.velocity * despawnVelMult + despawnTargetDist) / (despawnVelMult + 1f);
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			if (distanceFromTarget < 2000f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.localAI[0] = 0f;
			}
		}
		if (Main.dedServ)
		{
			NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		if (CalamityClientConfig.Instance.Afterimages && base.NPC.ai[0] == 0f && base.NPC.localAI[0] == 1f)
		{
			int chargeAfterimageAmount = 6;
			for (int i = 1; i < chargeAfterimageAmount; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(chargeAfterimageAmount - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !NPC.downedGolemBoss || !spawnInfo.Player.ZoneJungle)
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		return SpawnCondition.HardmodeJungle.Chance * 0.02f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (base.NPC.height = 100);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int plagueDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[plagueDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[plagueDust].scale = 0.5f;
				Main.dust[plagueDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int plagueDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, 0f, 0f, 100, default(Color), 3f);
			Main.dust[plagueDust2].noGravity = true;
			Dust obj2 = Main.dust[plagueDust2];
			obj2.velocity *= 5f;
			plagueDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[plagueDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void OnKill()
	{
		int heartAmt = Main.rand.Next(3) + 3;
		for (int i = 0; i < heartAmt; i++)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<PlagueCellCanister>(), 1, 8, 12);
		npcLoot.Add(ModContent.ItemType<PlaguedFuelPack>(), 4);
		npcLoot.Add(ModContent.ItemType<PlagueCaller>(), 50);
		npcLoot.Add(209);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Plague>(), 240);
		}
	}
}
