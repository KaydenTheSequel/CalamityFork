using System;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

[LegacyName(new string[] { "Horse" })]
public class EarthElemental : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.6f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -20f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 28f;
		value.Position.Y -= 56f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.npcSlots = 3f;
		base.NPC.damage = 50;
		base.NPC.width = 230;
		base.NPC.height = 230;
		base.NPC.defense = 20;
		base.NPC.lifeMax = 3800;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0.05f;
		base.NPC.value = Item.buyPrice(0, 1, 50);
		base.NPC.dontTakeDamage = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.rarity = 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<EarthElementalBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.EarthElemental")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.Calamity().ZoneAbyss || spawnInfo.Player.Calamity().ZoneSunkenSea || !spawnInfo.Player.ZoneRockLayerHeight)
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		return SpawnCondition.Cavern.Chance * 0.005f;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.ai[0] != 0f || base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter >= 8.0)
			{
				base.NPC.frame.Y = (base.NPC.frame.Y + frameHeight) % (Main.npcFrameCount[base.Type] * frameHeight);
				base.NPC.frameCounter = 0.0;
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		int[] weapons = new int[2]
		{
			ModContent.ItemType<EarthenPike>(),
			ModContent.ItemType<SlagMagnum>()
		};
		npcLoot.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 31, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 160;
		base.NPC.height = 160;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int earthDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 31, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[earthDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[earthDust].scale = 0.5f;
				Main.dust[earthDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int earthDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[earthDust2].noGravity = true;
			Dust obj2 = Main.dust[earthDust2];
			obj2.velocity *= 5f;
			earthDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[earthDust2];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 goreSource = base.NPC.Center;
		int goreAmt = 3;
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
		for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
		{
			float velocityMult = 0.33f;
			if (goreIndex < goreAmt / 3)
			{
				velocityMult = 0.66f;
			}
			if (goreIndex >= 2 * goreAmt / 3)
			{
				velocityMult = 1f;
			}
			ModContent.GetInstance<CalamityMod>();
			int type = Main.rand.Next(61, 64);
			int smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj4 = Main.gore[smoke];
			obj4.velocity *= velocityMult;
			obj4.velocity.X++;
			obj4.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj5 = Main.gore[smoke];
			obj5.velocity *= velocityMult;
			obj5.velocity.X--;
			obj5.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj6 = Main.gore[smoke];
			obj6.velocity *= velocityMult;
			obj6.velocity.X++;
			obj6.velocity.Y--;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj7 = Main.gore[smoke];
			obj7.velocity *= velocityMult;
			obj7.velocity.X--;
			obj7.velocity.Y--;
		}
	}

	public override bool PreAI()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) < 480f)
		{
			if (base.NPC.ai[0] == 0f)
			{
				if (Main.zenithWorld)
				{
					SoundEngine.PlaySound(in SoundID.ScaryScream, Main.player[base.NPC.target].Center);
				}
				base.NPC.ai[0] = 1f;
				base.NPC.dontTakeDamage = false;
			}
		}
		else
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.ai[0] == 0f)
		{
			return false;
		}
		if (Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			if (base.NPC.velocity.Y < -2f)
			{
				base.NPC.velocity.Y = -2f;
			}
			base.NPC.velocity.Y += 0.1f;
			if (base.NPC.velocity.Y > 12f)
			{
				base.NPC.velocity.Y = 12f;
			}
			if (base.NPC.timeLeft > 60)
			{
				base.NPC.timeLeft = 60;
			}
		}
		base.NPC.localAI[0]++;
		if (base.NPC.localAI[0] >= 300f)
		{
			base.NPC.localAI[0] = 0f;
			SoundEngine.PlaySound(in SoundID.NPCHit43, base.NPC.Center);
			base.NPC.TargetClosest();
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				float rockSpeed = 4f;
				int damage = (Main.masterMode ? 18 : (Main.expertMode ? 22 : 30));
				if (Main.netMode != 1)
				{
					Vector2 projPosition = base.NPC.Center;
					float targetXDist = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - projPosition.X;
					float absoluteTargetX = Math.Abs(targetXDist) * 0.1f;
					float targetYDist = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - projPosition.Y - absoluteTargetX;
					float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
					targetDistance = rockSpeed / targetDistance;
					targetXDist *= targetDistance;
					targetYDist *= targetDistance;
					int rockType = ModContent.ProjectileType<EarthRockSmall>();
					projPosition.X += targetXDist;
					projPosition.Y += targetYDist;
					for (int k = 0; k < 4; k++)
					{
						rockType = (Main.rand.NextBool(4) ? ModContent.ProjectileType<EarthRockBig>() : ModContent.ProjectileType<EarthRockSmall>());
						targetXDist = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - projPosition.X;
						targetYDist = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - projPosition.Y;
						targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
						targetDistance = rockSpeed / targetDistance;
						targetXDist += (float)Main.rand.Next(-40, 41);
						targetYDist += (float)Main.rand.Next(-40, 41);
						targetXDist *= targetDistance;
						targetYDist *= targetDistance;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projPosition.X, projPosition.Y, targetXDist, targetYDist, rockType, damage, 0f, Main.myPlayer);
					}
				}
			}
		}
		float playerLocation = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
		base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
		base.NPC.spriteDirection = base.NPC.direction;
		Vector2 direction = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
		float chargeGateValue = (Main.expertMode ? 300f : 600f);
		base.NPC.ai[1]++;
		if (base.NPC.ai[1] >= chargeGateValue)
		{
			base.NPC.damage = base.NPC.defDamage;
			float chargeVelocity = (Main.expertMode ? 9f : 6f);
			direction *= chargeVelocity;
			base.NPC.velocity = direction;
			base.NPC.ai[1] = -30f;
		}
		if (Math.Sqrt(base.NPC.velocity.X * base.NPC.velocity.X + base.NPC.velocity.Y * base.NPC.velocity.Y) > 1.0 && base.NPC.ai[1] >= 0f)
		{
			base.NPC.damage = 0;
			NPC nPC = base.NPC;
			nPC.velocity *= 0.97f;
		}
		if (Math.Sqrt(base.NPC.velocity.X * base.NPC.velocity.X + base.NPC.velocity.Y * base.NPC.velocity.Y) <= 1.15)
		{
			base.NPC.damage = 0;
			base.NPC.velocity = direction;
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 300);
		}
	}
}
