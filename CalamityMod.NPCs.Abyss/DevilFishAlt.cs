using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class DevilFishAlt : ModNPC
{
	public bool brokenMask;

	public int hitCounter;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 16;
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Abyss/DevilFishGlowAlt", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.lavaImmune = true;
		base.NPC.damage = 90;
		base.NPC.width = 136;
		base.NPC.height = 62;
		base.NPC.defense = 999999;
		base.NPC.lifeMax = 800;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0.85f;
		base.Banner = ModContent.NPCType<DevilFish>();
		base.BannerItem = ModContent.ItemType<DevilFishBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer3Biome>().Type };
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(brokenMask);
		writer.Write(hitCounter);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		brokenMask = reader.ReadBoolean();
		hitCounter = reader.ReadInt32();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.NPC.target];
		float speedBoost = (brokenMask ? 1.5f : 1f);
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		base.NPC.noGravity = true;
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		base.NPC.chaseable = brokenMask;
		if (hitCounter >= 5 && !brokenMask)
		{
			brokenMask = true;
			base.NPC.HitSound = SoundID.NPCHit1;
			base.NPC.defense = 25;
			if (!Main.dedServ)
			{
				for (int i = 1; i < 4; i++)
				{
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DevilFishMask" + i + ((i == 3) ? "Alt" : "")).Type);
				}
			}
			SoundEngine.PlaySound(in DevilFish.MaskBreakSound, base.NPC.Center);
		}
		if (base.NPC.wet)
		{
			bool canAttack = brokenMask;
			base.NPC.TargetClosest(faceTarget: false);
			if (player.statLife <= player.statLifeMax2 / 4)
			{
				canAttack = true;
			}
			if ((!player.wet || player.dead || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height)) & canAttack)
			{
				canAttack = false;
			}
			if (!canAttack)
			{
				if (base.NPC.collideX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * -1f;
					base.NPC.direction *= -1;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.collideY)
				{
					base.NPC.netUpdate = true;
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
			if (canAttack)
			{
				base.NPC.TargetClosest();
				base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * (CalamityWorld.death ? 0.5f : (CalamityWorld.revenge ? 0.375f : 0.25f)) * speedBoost;
				base.NPC.velocity.Y = base.NPC.velocity.Y + (float)base.NPC.directionY * (CalamityWorld.death ? 0.3f : (CalamityWorld.revenge ? 0.225f : 0.15f)) * speedBoost;
				float velocity = (CalamityWorld.death ? 12f : (CalamityWorld.revenge ? 9f : 6f));
				if (base.NPC.velocity.X > velocity * speedBoost)
				{
					base.NPC.velocity.X = velocity * speedBoost;
				}
				if (base.NPC.velocity.X < (0f - velocity) * speedBoost)
				{
					base.NPC.velocity.X = (0f - velocity) * speedBoost;
				}
				if (base.NPC.velocity.Y > velocity * speedBoost)
				{
					base.NPC.velocity.Y = velocity * speedBoost;
				}
				if (base.NPC.velocity.Y < (0f - velocity) * speedBoost)
				{
					base.NPC.velocity.Y = (0f - velocity) * speedBoost;
				}
			}
			else
			{
				base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.1f;
				if (base.NPC.velocity.X < -2.5f || base.NPC.velocity.X > 2.5f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
				}
				if (base.NPC.ai[0] == -1f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
					if ((double)base.NPC.velocity.Y < -0.3)
					{
						base.NPC.ai[0] = 1f;
					}
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
					if ((double)base.NPC.velocity.Y > 0.3)
					{
						base.NPC.ai[0] = -1f;
					}
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
			if ((double)base.NPC.velocity.Y > 0.4 || (double)base.NPC.velocity.Y < -0.4)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y * 0.95f;
			}
		}
		else
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.94f;
				if ((double)base.NPC.velocity.X > -0.2 && (double)base.NPC.velocity.X < 0.2)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.3f;
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
			base.NPC.ai[0] = 1f;
		}
		base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f;
		if ((double)base.NPC.rotation < -0.2)
		{
			base.NPC.rotation = -0.2f;
		}
		if ((double)base.NPC.rotation > 0.2)
		{
			base.NPC.rotation = 0.2f;
		}
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return brokenMask;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		if (!base.NPC.wet && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
		}
		if (!brokenMask || base.NPC.IsABestiaryIconDummy)
		{
			if (base.NPC.frame.Y > frameHeight * 7)
			{
				base.NPC.frame.Y = 0;
			}
		}
		else if (base.NPC.frame.Y < frameHeight * 8 || base.NPC.frame.Y > frameHeight * 15)
		{
			base.NPC.frame.Y = frameHeight * 8;
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			SpriteEffects effects = (SpriteEffects)(base.NPC.direction != -1);
			Main.EntitySpriteDraw(GlowTexture.Value, base.NPC.Center - Main.screenPosition + new Vector2(0f, base.NPC.gfxOffY + 4f), base.NPC.frame, Color.White * 0.5f, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, effects);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water)
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.55f;
			}
			return 4.95f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		DevilFish.DefineDevilFishLoot(npcLoot);
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
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DevilfishAlt").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DevilfishAlt2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DevilfishAlt3").Type);
			}
		}
		if (hitCounter <= 5)
		{
			hitCounter++;
		}
	}
}
