using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Potions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class Cuttlefish : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.chaseable = false;
		base.NPC.damage = 34;
		base.NPC.width = 58;
		base.NPC.height = 30;
		base.NPC.defense = 8;
		base.NPC.lifeMax = 160;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.alpha = 150;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.HitSound = SoundID.NPCHit33;
		base.NPC.DeathSound = SoundID.NPCDeath28;
		base.NPC.knockBackResist = 0.3f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CuttlefishBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer2Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Cuttlefish")
		});
	}

	public override void AI()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		int alphaControl = 150;
		if (base.NPC.ai[2] == 0f)
		{
			base.NPC.alpha = alphaControl;
			base.NPC.TargetClosest();
			if (!Main.player[base.NPC.target].dead)
			{
				Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (((Vector2)(ref val)).Length() < Main.player[base.NPC.target].Calamity().GetAbyssAggro(160f) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.ai[2] = -16f;
				}
			}
			if (base.NPC.justHit)
			{
				base.NPC.ai[2] = -16f;
			}
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
			base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.02f;
			base.NPC.rotation = base.NPC.velocity.X * 0.4f;
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
			}
		}
		else if (base.NPC.ai[2] < 0f)
		{
			if (base.NPC.alpha > 0)
			{
				base.NPC.alpha -= alphaControl / 16;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[2] = 1f;
				base.NPC.velocity.X = base.NPC.direction * 2;
			}
		}
		else
		{
			if (base.NPC.ai[2] != 1f)
			{
				return;
			}
			base.NPC.chaseable = true;
			if (base.NPC.direction == 0)
			{
				base.NPC.TargetClosest();
			}
			if (base.NPC.wet || base.NPC.noTileCollide)
			{
				bool canAttack = false;
				base.NPC.TargetClosest(faceTarget: false);
				if (Main.player[base.NPC.target].wet && !Main.player[base.NPC.target].dead)
				{
					canAttack = true;
				}
				if (!canAttack)
				{
					if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.noTileCollide = false;
					}
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
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						if (base.NPC.ai[3] > 0f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
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
					if (base.NPC.ai[1] >= 150f)
					{
						base.NPC.ai[3] = 1f;
						base.NPC.ai[1] = 0f;
						base.NPC.netUpdate = true;
					}
					if (base.NPC.ai[3] == 0f)
					{
						base.NPC.alpha = 0;
						base.NPC.noTileCollide = false;
					}
					else
					{
						base.NPC.alpha = 150;
						base.NPC.noTileCollide = true;
					}
					base.NPC.TargetClosest();
					base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.2f;
					base.NPC.velocity.Y = base.NPC.velocity.Y + (float)base.NPC.directionY * 0.2f;
					if (base.NPC.velocity.X > 9f)
					{
						base.NPC.velocity.X = 9f;
					}
					if (base.NPC.velocity.X < -9f)
					{
						base.NPC.velocity.X = -9f;
					}
					if (base.NPC.velocity.Y > 7f)
					{
						base.NPC.velocity.Y = 7f;
					}
					if (base.NPC.velocity.Y < -7f)
					{
						base.NPC.velocity.Y = -7f;
					}
				}
				else
				{
					if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.noTileCollide = false;
					}
					base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.1f;
					if (base.NPC.velocity.X < -1f || base.NPC.velocity.X > 1f)
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
				int npcTileXAgain = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
				int npcTileYAgain = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
				if (Main.tile[npcTileXAgain, npcTileYAgain - 1].LiquidAmount > 128)
				{
					if (Main.tile[npcTileXAgain, npcTileYAgain + 1].HasTile)
					{
						base.NPC.ai[0] = -1f;
					}
					else if (Main.tile[npcTileXAgain, npcTileYAgain + 2].HasTile)
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
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.25f;
				if (base.NPC.velocity.Y > 7f)
				{
					base.NPC.velocity.Y = 7f;
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
	}

	public override void FindFrame(int frameHeight)
	{
		if (!base.NPC.wet && !base.NPC.noTileCollide && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
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
			target.AddBuff(22, 300);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer2 && spawnInfo.Water)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.6f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<AnechoicCoating>(), 2);
		npcLoot.Add(ItemDropRule.NormalvsExpert(888, 100, 50));
		npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<InkBomb>(), 10, 5));
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
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Cuttlefish").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Cuttlefish2").Type);
			}
		}
	}
}
