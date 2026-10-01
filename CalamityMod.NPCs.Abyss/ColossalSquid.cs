using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class ColossalSquid : ModNPC
{
	public bool hasBeenHit;

	public bool clone;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 11;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 9f;
		base.NPC.noGravity = true;
		base.NPC.damage = 150;
		base.NPC.width = 180;
		base.NPC.height = 180;
		base.NPC.defense = 50;
		base.NPC.lifeMax = 130000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.timeLeft = NPC.activeTime * 30;
		base.NPC.value = Item.buyPrice(0, 25);
		base.NPC.HitSound = SoundID.NPCHit20;
		base.NPC.DeathSound = SoundID.NPCDeath23;
		base.NPC.rarity = 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ColossalSquidBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.NPC.Calamity().canBreakPlayerDefense = true;
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ColossalSquid")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(clone);
		writer.Write(base.NPC.chaseable);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		clone = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Unknown result type (might be due to invalid IL or missing references)
		//IL_1043: Unknown result type (might be due to invalid IL or missing references)
		//IL_1048: Unknown result type (might be due to invalid IL or missing references)
		//IL_104d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0856: Unknown result type (might be due to invalid IL or missing references)
		//IL_085b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_1218: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1243: Unknown result type (might be due to invalid IL or missing references)
		//IL_126f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a33: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1506: Unknown result type (might be due to invalid IL or missing references)
		//IL_1511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1531: Unknown result type (might be due to invalid IL or missing references)
		//IL_1541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1734: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val;
		if (base.NPC.localAI[1] == 1f)
		{
			base.NPC.localAI[3]++;
			if (base.NPC.localAI[3] >= 180f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				base.NPC.localAI[3] = 0f;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			if (Main.rand.NextBool(300))
			{
				SoundEngine.PlaySound(in SoundID.Zombie34, base.NPC.Center);
			}
			base.NPC.noTileCollide = false;
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				if (Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 1f;
				}
				else
				{
					Vector2 targetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
					targetDirection.Y -= Main.player[base.NPC.target].height / 4;
					if (((Vector2)(ref targetDirection)).Length() > 800f)
					{
						base.NPC.ai[0] = 2f;
					}
					else
					{
						Vector2 squidCenter = base.NPC.Center;
						squidCenter.X = Main.player[base.NPC.target].Center.X;
						Vector2 squidDirection = squidCenter - base.NPC.Center;
						if (((Vector2)(ref squidDirection)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, squidCenter, 1, 1))
						{
							base.NPC.ai[0] = 3f;
							base.NPC.ai[1] = squidCenter.X;
							base.NPC.ai[2] = squidCenter.Y;
							Vector2 squidCenterAgain = base.NPC.Center;
							squidCenterAgain.Y = Main.player[base.NPC.target].Center.Y;
							if (((Vector2)(ref squidDirection)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, squidCenterAgain, 1, 1) && Collision.CanHit(squidCenterAgain, 1, 1, Main.player[base.NPC.target].position, 1, 1))
							{
								base.NPC.ai[0] = 3f;
								base.NPC.ai[1] = squidCenterAgain.X;
								base.NPC.ai[2] = squidCenterAgain.Y;
							}
						}
						else
						{
							squidCenter = base.NPC.Center;
							squidCenter.Y = Main.player[base.NPC.target].Center.Y;
							val = squidCenter - base.NPC.Center;
							if (((Vector2)(ref val)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, squidCenter, 1, 1))
							{
								base.NPC.ai[0] = 3f;
								base.NPC.ai[1] = squidCenter.X;
								base.NPC.ai[2] = squidCenter.Y;
							}
						}
						if (base.NPC.ai[0] == 0f)
						{
							base.NPC.localAI[0] = 0f;
							((Vector2)(ref targetDirection)).Normalize();
							targetDirection *= 0.5f;
							NPC nPC = base.NPC;
							nPC.velocity += targetDirection;
							base.NPC.ai[0] = 4f;
							base.NPC.ai[1] = 0f;
						}
					}
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.damage = 0;
				base.NPC.rotation += (float)base.NPC.direction * 0.1f;
				Vector2 latchPosition = Main.player[base.NPC.target].Top - base.NPC.Center;
				float latchDistance = ((Vector2)(ref latchPosition)).Length();
				float latchLockSpeed = 5f;
				latchLockSpeed += latchDistance / 100f;
				int latchVelocity = 50;
				((Vector2)(ref latchPosition)).Normalize();
				latchPosition *= latchLockSpeed;
				base.NPC.velocity = (base.NPC.velocity * (float)(latchVelocity - 1) + latchPosition) / (float)latchVelocity;
				if (!Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
				}
				if (latchDistance < 160f && Main.player[base.NPC.target].active && !Main.player[base.NPC.target].dead && !clone)
				{
					base.NPC.Center = Main.player[base.NPC.target].Top;
					base.NPC.velocity = Vector2.Zero;
					base.NPC.ai[0] = 5f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				base.NPC.damage = 0;
				base.NPC.rotation = base.NPC.velocity.X * 0.05f;
				base.NPC.noTileCollide = true;
				Vector2 lungeDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
				float num = ((Vector2)(ref lungeDirection)).Length();
				float lungeSpeed = 3f;
				int lungeVelocity = 3;
				((Vector2)(ref lungeDirection)).Normalize();
				lungeDirection *= lungeSpeed;
				base.NPC.velocity = (base.NPC.velocity * (float)(lungeVelocity - 1) + lungeDirection) / (float)lungeVelocity;
				if (num < 600f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.ai[0] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 3f)
			{
				base.NPC.damage = 0;
				base.NPC.rotation = base.NPC.velocity.X * 0.05f;
				Vector2 otherLungeDirection = new Vector2(base.NPC.ai[1], base.NPC.ai[2]) - base.NPC.Center;
				float otherLungeDistance = ((Vector2)(ref otherLungeDirection)).Length();
				float otherLungeSpeed = 2f;
				float otherLungeVelocity = 3f;
				((Vector2)(ref otherLungeDirection)).Normalize();
				otherLungeDirection *= otherLungeSpeed;
				base.NPC.velocity = (base.NPC.velocity * (otherLungeVelocity - 1f) + otherLungeDirection) / otherLungeVelocity;
				if (base.NPC.collideX || base.NPC.collideY)
				{
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
				}
				if (otherLungeDistance < otherLungeSpeed || otherLungeDistance > 800f || Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 4f)
			{
				base.NPC.damage = 0;
				base.NPC.rotation = base.NPC.velocity.X * 0.05f;
				if (base.NPC.collideX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * -0.8f;
				}
				if (base.NPC.collideY)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y * -0.8f;
				}
				Vector2 slowDownDirection;
				if (base.NPC.velocity.X == 0f && base.NPC.velocity.Y == 0f)
				{
					slowDownDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
					slowDownDirection.Y -= Main.player[base.NPC.target].height / 4;
					((Vector2)(ref slowDownDirection)).Normalize();
					base.NPC.velocity = slowDownDirection * 0.1f;
				}
				float slowDownVelocity = 20f;
				slowDownDirection = base.NPC.velocity;
				((Vector2)(ref slowDownDirection)).Normalize();
				slowDownDirection *= 2f;
				base.NPC.velocity = (base.NPC.velocity * (slowDownVelocity - 1f) + slowDownDirection) / slowDownVelocity;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] > 180f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
				}
				if (Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= 5f && !Collision.SolidCollision(base.NPC.position - new Vector2(10f, 10f), base.NPC.width + 20, base.NPC.height + 20))
				{
					base.NPC.localAI[0] = 0f;
					Vector2 slowDownCenter = base.NPC.Center;
					slowDownCenter.X = Main.player[base.NPC.target].Center.X;
					if (Collision.CanHit(base.NPC.Center, 1, 1, slowDownCenter, 1, 1) && Collision.CanHit(base.NPC.Center, 1, 1, slowDownCenter, 1, 1) && Collision.CanHit(Main.player[base.NPC.target].Center, 1, 1, slowDownCenter, 1, 1))
					{
						base.NPC.ai[0] = 3f;
						base.NPC.ai[1] = slowDownCenter.X;
						base.NPC.ai[2] = slowDownCenter.Y;
					}
					else
					{
						slowDownCenter = base.NPC.Center;
						slowDownCenter.Y = Main.player[base.NPC.target].Center.Y;
						if (Collision.CanHit(base.NPC.Center, 1, 1, slowDownCenter, 1, 1) && Collision.CanHit(Main.player[base.NPC.target].Center, 1, 1, slowDownCenter, 1, 1))
						{
							base.NPC.ai[0] = 3f;
							base.NPC.ai[1] = slowDownCenter.X;
							base.NPC.ai[2] = slowDownCenter.Y;
						}
					}
				}
			}
			else if (base.NPC.ai[0] == 5f)
			{
				base.NPC.damage = base.NPC.defDamage;
				Player latchedTarget = Main.player[base.NPC.target];
				if (!latchedTarget.active || latchedTarget.dead || clone)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
				else
				{
					base.NPC.Center = ((latchedTarget.gravDir == 1f) ? latchedTarget.Top : latchedTarget.Bottom) + new Vector2((float)(latchedTarget.direction * 4), 0f);
					base.NPC.gfxOffY = latchedTarget.gfxOffY;
					base.NPC.velocity = Vector2.Zero;
					latchedTarget.AddBuff(163, 59);
				}
			}
			base.NPC.rotation = base.NPC.velocity.X * 0.05f;
			goto IL_1a6d;
		}
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (!base.NPC.noTileCollide)
		{
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * -1f;
				base.NPC.direction *= -1;
			}
			if (base.NPC.collideY)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
					base.NPC.directionY = -1;
					base.NPC.ai[0] = -1f;
				}
				else if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
					base.NPC.directionY = 1;
					base.NPC.ai[0] = 1f;
				}
			}
		}
		base.NPC.TargetClosest(faceTarget: false);
		if (Main.player[base.NPC.target].wet && !Main.player[base.NPC.target].dead && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
		{
			val = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (((Vector2)(ref val)).Length() < Main.player[base.NPC.target].Calamity().GetAbyssAggro(240f))
			{
				goto IL_1088;
			}
		}
		if (base.NPC.justHit)
		{
			goto IL_1088;
		}
		goto IL_11b2;
		IL_11b2:
		base.NPC.chaseable = hasBeenHit;
		if (hasBeenHit)
		{
			if (Main.rand.NextBool(300))
			{
				SoundEngine.PlaySound(in SoundID.Zombie34, base.NPC.Center);
			}
			if (base.NPC.ai[3] > 0f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.ai[3] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[3] == 0f)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] >= 120f)
			{
				base.NPC.ai[3] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[3] == 0f)
			{
				base.NPC.noTileCollide = false;
			}
			else
			{
				base.NPC.noTileCollide = true;
			}
			base.NPC.localAI[3]++;
			if (base.NPC.localAI[3] >= 420f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 1f;
				base.NPC.localAI[2] = 0f;
				base.NPC.localAI[3] = 0f;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
				return;
			}
			base.NPC.localAI[2] = 1f;
			base.NPC.localAI[0]++;
			if (base.NPC.localAI[0] >= 150f)
			{
				base.NPC.localAI[0] = 0f;
				base.NPC.netUpdate = true;
				int damage = (Main.masterMode ? 46 : (Main.expertMode ? 55 : 70));
				if (clone)
				{
					damage /= 4;
				}
				SoundEngine.PlaySound(in SoundID.Item111, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y + 60f, 0f, 2f, ModContent.ProjectileType<InkBombHostile>(), damage, 0f, Main.myPlayer);
				}
			}
			base.NPC.rotation = base.NPC.velocity.X * 0.05f;
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.975f;
			float hitLungeThreshold = 2.5f;
			float lungeVelocity2 = 20f;
			if (((Vector2)(ref base.NPC.velocity)).Length() > lungeVelocity2 * 0.4f)
			{
				base.NPC.damage = base.NPC.defDamage;
			}
			else
			{
				base.NPC.damage = 0;
			}
			if (base.NPC.velocity.X > 0f - hitLungeThreshold && base.NPC.velocity.X < hitLungeThreshold && base.NPC.velocity.Y > 0f - hitLungeThreshold && base.NPC.velocity.Y < hitLungeThreshold)
			{
				base.NPC.TargetClosest();
				Vector2 hitLungePos = default(Vector2);
				((Vector2)(ref hitLungePos))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float hitLungeTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - hitLungePos.X;
				float hitLungeTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - hitLungePos.Y;
				float hitLungeTargetDist = (float)Math.Sqrt(hitLungeTargetX * hitLungeTargetX + hitLungeTargetY * hitLungeTargetY);
				hitLungeTargetDist = lungeVelocity2 / hitLungeTargetDist;
				hitLungeTargetX *= hitLungeTargetDist;
				hitLungeTargetY *= hitLungeTargetDist;
				base.NPC.velocity.X = hitLungeTargetX;
				base.NPC.velocity.Y = hitLungeTargetY;
				return;
			}
		}
		else
		{
			base.NPC.damage = 0;
			if (Main.rand.NextBool(300))
			{
				SoundEngine.PlaySound(in SoundID.Zombie35, base.NPC.Center);
			}
			base.NPC.localAI[2] = 0f;
			base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.02f;
			base.NPC.rotation = base.NPC.velocity.X * 0.2f;
			if (base.NPC.velocity.X < -1f || base.NPC.velocity.X > 1f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
			}
			if (base.NPC.ai[0] == -1f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
				if (base.NPC.velocity.Y < -1f)
				{
					base.NPC.ai[0] = 1f;
				}
			}
			else
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
				if (base.NPC.velocity.Y > 1f)
				{
					base.NPC.ai[0] = -1f;
				}
			}
			int npcTileX = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
			int npcTileY = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
			if (Main.tile[npcTileX, npcTileY - 1].LiquidAmount > 128)
			{
				if (Main.tile[npcTileX, npcTileY + 1].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
				else if (Main.tile[npcTileX, npcTileY + 2].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
			}
			else
			{
				base.NPC.ai[0] = 1f;
			}
			if ((double)base.NPC.velocity.Y > 1.2 || (double)base.NPC.velocity.Y < -1.2)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y * 0.99f;
				return;
			}
		}
		goto IL_1a6d;
		IL_1a6d:
		float pushVelocity = 0.05f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC otherNPC = enumerator.Current;
			if (otherNPC.whoAmI != base.NPC.whoAmI && otherNPC.type == base.NPC.type && Vector2.Distance(base.NPC.Center, otherNPC.Center) < 160f)
			{
				if (base.NPC.position.X < otherNPC.position.X)
				{
					base.NPC.velocity.X -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.X += pushVelocity;
				}
				if (base.NPC.position.Y < otherNPC.position.Y)
				{
					base.NPC.velocity.Y -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.Y += pushVelocity;
				}
			}
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 6400f)
		{
			base.NPC.active = false;
		}
		return;
		IL_1088:
		if (Main.zenithWorld && Main.netMode != 1 && !clone && !hasBeenHit)
		{
			for (int i = 0; i < 3; i++)
			{
				int squib = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + Main.rand.Next(-20, 20), (int)base.NPC.Center.Y + Main.rand.Next(-20, 20), ModContent.NPCType<ColossalSquid>());
				if (squib.WithinBounds(Main.maxNPCs))
				{
					Main.npc[squib].ModNPC<ColossalSquid>().clone = true;
					Main.npc[squib].ModNPC<ColossalSquid>().hasBeenHit = true;
					Main.npc[squib].scale = 0.25f;
					Main.npc[squib].lifeMax /= 5;
					Main.npc[squib].life /= 5;
				}
			}
		}
		hasBeenHit = true;
		goto IL_11b2;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (base.NPC.ai[0] == 5f)
		{
			return false;
		}
		return true;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[0] == 5f)
		{
			Color color = Lighting.GetColor((int)((double)base.NPC.position.X + (double)base.NPC.width * 0.5) / 16, (int)(((double)base.NPC.position.Y + (double)base.NPC.height * 0.5) / 16.0));
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (base.NPC.spriteDirection == 1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Player player = Main.player[base.NPC.target];
			player.invis = true;
			player.aggro = -750;
			if (player.gravDir == -1f)
			{
				spriteEffects = (SpriteEffects)(spriteEffects | 2);
			}
			Main.spriteBatch.Draw(TextureAssets.Npc[base.Type].Value, new Vector2((float)(player.direction * 4), player.gfxOffY) + ((player.gravDir == 1f) ? player.Top : player.Bottom) - screenPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(color), base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, spriteEffects, 0f);
		}
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
		base.NPC.frameCounter += (hasBeenHit ? 0.15f : 0.075f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if ((spawnInfo.Player.Calamity().ZoneAbyssLayer3 || spawnInfo.Player.Calamity().ZoneAbyssLayer4) && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<ColossalSquid>()))
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 5.4f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 300);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(1119, 1, 12, 16);
		npcLoot.Add(ModContent.ItemType<InkBomb>(), 3);
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 26, 38, 31, 45));
		npcLoot.AddIf(DropHelper.PostPolter(), ModContent.ItemType<CalamarisLament>(), 3);
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
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 30; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ColossalSquid").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ColossalSquid2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ColossalSquid3").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ColossalSquid4").Type, base.NPC.scale);
			}
		}
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (Main.zenithWorld && clone)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.TinySquid");
		}
	}
}
