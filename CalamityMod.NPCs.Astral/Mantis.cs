using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class Mantis : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 14;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/MantisGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 55;
		base.NPC.width = 60;
		base.NPC.height = 58;
		base.NPC.aiStyle = -1;
		base.NPC.defense = 22;
		base.NPC.lifeMax = 400;
		base.NPC.knockBackResist = 0.2f;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<MantisBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 85;
			base.NPC.defense = 32;
			base.NPC.knockBackResist = 0.1f;
			base.NPC.lifeMax = 600;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Mantis")
		});
	}

	public override void AI()
	{
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		Player target = Main.player[base.NPC.target];
		if (base.NPC.ai[0] == 0f)
		{
			float acceleration = (CalamityWorld.death ? 0.075f : (CalamityWorld.revenge ? 0.06f : 0.045f));
			float maxSpeed = (CalamityWorld.death ? 10.8f : (CalamityWorld.revenge ? 8.8f : 6.8f));
			if (base.NPC.Center.X > target.Center.X)
			{
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X -= acceleration;
				}
				if (base.NPC.velocity.X < 0f - maxSpeed)
				{
					base.NPC.velocity.X = 0f - maxSpeed;
				}
			}
			else
			{
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X += acceleration;
				}
				if (base.NPC.velocity.X > maxSpeed)
				{
					base.NPC.velocity.X = maxSpeed;
				}
			}
			if (base.NPC.velocity.Y == 0f && (HoleBelow() || (base.NPC.collideX && base.NPC.position.X == base.NPC.oldPosition.X)))
			{
				base.NPC.velocity.Y = (CalamityWorld.death ? (-8f) : (CalamityWorld.revenge ? (-6.5f) : (-5f)));
			}
			Vector2 vector = base.NPC.Center - target.Center;
			if (((Vector2)(ref vector)).Length() < 480f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, target.position, target.width, target.height))
			{
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 120f)))
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = (base.NPC.ai[2] = 0f);
					base.NPC.frame.Y = 400;
					base.NPC.frameCounter = 0.0;
				}
			}
			else
			{
				base.NPC.ai[1] -= 0.5f;
			}
			if (base.NPC.justHit)
			{
				base.NPC.ai[1] -= 60f;
			}
			if (base.NPC.ai[1] < 0f)
			{
				base.NPC.ai[1] = 0f;
			}
		}
		else
		{
			base.NPC.ai[2]++;
			base.NPC.velocity.X *= 0.95f;
			if (base.NPC.ai[2] == 20f)
			{
				SoundEngine.PlaySound(in SoundID.Item71, base.NPC.Center);
				Vector2 vector2 = Main.player[base.NPC.target].Center - base.NPC.Center;
				((Vector2)(ref vector2)).Normalize();
				int damage = ((!DownedBossSystem.downedAstrumAureus) ? (Main.masterMode ? 30 : (Main.expertMode ? 35 : 45)) : (Main.masterMode ? 34 : (Main.expertMode ? 40 : 55)));
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + ((base.NPC.Center.X < target.Center.X) ? (-14f) : 14f) * Vector2.UnitX, vector2 * 7f, ModContent.ProjectileType<MantisRing>(), damage, 0f);
			}
		}
		base.NPC.direction = ((!(base.NPC.Center.X > target.Center.X)) ? 1 : 0);
		base.NPC.spriteDirection = base.NPC.direction;
	}

	private bool HoleBelow()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		int tileWidth = 4;
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

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.velocity.Y != 0f)
			{
				base.NPC.frame.Y = frameHeight * 13;
				base.NPC.frameCounter = 20.0;
				return;
			}
			base.NPC.frameCounter += 0.8f + Math.Abs(base.NPC.velocity.X) * 0.5f;
			if (base.NPC.frameCounter > 10.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
				if (base.NPC.frame.Y > frameHeight * 5)
				{
					base.NPC.frame.Y = 0;
				}
			}
			return;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 4.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= frameHeight * 13)
			{
				base.NPC.frame.Y = 0;
				base.NPC.frameCounter = 0.0;
				base.NPC.ai[0] = 0f;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, ModContent.DustType<AstralOrange>(), 1f, 4, 24);
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos - new Vector2(0f, 8f), (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, new Vector2(70f, 40f), 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(1))
		{
			return 0.16f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 1, 2, 1, 3));
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<AstralScythe>(), 10);
	}
}
