using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Graphics;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class LuminousCorvina : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public static readonly SoundStyle ScreamSound = new SoundStyle("CalamityMod/Sounds/Custom/CorvinaScream");

	private bool hasBeenHit;

	private int screamTimer;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 10;
		base.NPC.width = 68;
		base.NPC.height = 58;
		base.NPC.defense = 18;
		base.NPC.lifeMax = 800;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0.85f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<LuminousCorvinaBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.LuminousCorvina")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(screamTimer);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		screamTimer = reader.ReadInt32();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource(base.NPC.Center));
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		base.NPC.noGravity = true;
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.justHit)
		{
			hasBeenHit = true;
		}
		base.NPC.chaseable = hasBeenHit;
		if (base.NPC.wet)
		{
			bool canAttack = hasBeenHit;
			base.NPC.TargetClosest(faceTarget: false);
			if ((Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && ((base.NPC.Center.X - 15f < Main.player[base.NPC.target].Center.X && base.NPC.direction == 1) || (base.NPC.Center.X + 15f > Main.player[base.NPC.target].Center.X && base.NPC.direction == -1))) || (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) & canAttack))
			{
				screamTimer++;
				int screamLimit = (CalamityWorld.death ? 60 : (CalamityWorld.revenge ? 120 : 180));
				if (screamTimer >= screamLimit)
				{
					if (screamTimer == screamLimit)
					{
						SoundEngine.PlaySound(in ScreamSound, base.NPC.Center);
						if (!Main.dedServ && !Main.player[base.NPC.target].dead && Main.player[base.NPC.target].active)
						{
							Main.player[base.NPC.target].AddBuff(ModContent.BuffType<FishAlert>(), 360);
						}
					}
					if (screamTimer >= screamLimit + 60)
					{
						screamTimer = 0;
					}
					return;
				}
			}
			_ = (!Main.player[base.NPC.target].wet || Main.player[base.NPC.target].dead) & canAttack;
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
			base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.1f;
			if (base.NPC.velocity.X < -0.2f || base.NPC.velocity.X > 0.2f)
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
			return hasBeenHit;
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
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 6.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = 0;
			}
			return;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
		}
		if (screamTimer <= (CalamityWorld.death ? 60 : (CalamityWorld.revenge ? 120 : 180)))
		{
			if (base.NPC.frame.Y > frameHeight * 5)
			{
				base.NPC.frame.Y = 0;
			}
			return;
		}
		if (base.NPC.frame.Y < frameHeight * 6)
		{
			base.NPC.frame.Y = frameHeight * 6;
		}
		if (base.NPC.frame.Y > frameHeight * 7)
		{
			base.NPC.frame.Y = frameHeight * 6;
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
			target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 180);
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
		npcLoot.Add(ModContent.ItemType<Voidstone>(), 1, 8, 15);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.PostLevi());
		mainRule.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 1, 2, 2, 3));
		mainRule.Add(ModContent.ItemType<Lumenyl>(), 2);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 139, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 139, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LuminousCorvina").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LuminousCorvina2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LuminousCorvina3").Type);
			}
		}
	}
}
