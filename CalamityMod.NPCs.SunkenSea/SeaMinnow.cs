using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.NPCs.NormalNPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

public class SeaMinnow : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.NPC.type] = 4;
		Main.npcCatchable[base.NPC.type] = true;
		NPCID.Sets.CountsAsCritter[base.NPC.type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 0.1f;
		base.NPC.noGravity = true;
		base.NPC.damage = 0;
		base.NPC.width = 36;
		base.NPC.height = 22;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 5;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SeaMinnowBanner>();
		base.NPC.chaseable = false;
		base.NPC.catchItem = (short)ModContent.ItemType<SeaMinnowItem>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SunkenSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SeaMinnow")
		});
	}

	public override void AI()
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		CalamityRegularEnemyAI.PassiveSwimmingAI(base.NPC, base.Mod, 3, 150f, 0.25f, 0.15f, 6f, 6f, 0.05f);
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		base.NPC.noGravity = true;
		bool shouldSwimAway = false;
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.wet)
		{
			base.NPC.TargetClosest(faceTarget: false);
			if (Main.player[base.NPC.target].wet && !Main.player[base.NPC.target].dead)
			{
				Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (((Vector2)(ref val)).Length() < 150f)
				{
					shouldSwimAway = true;
				}
			}
			if ((!Main.player[base.NPC.target].wet || Main.player[base.NPC.target].dead) & shouldSwimAway)
			{
				shouldSwimAway = false;
			}
			if (!shouldSwimAway)
			{
				if (base.NPC.collideX || base.NPC.velocity.X == 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * -3f;
					base.NPC.direction *= -1;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.collideY)
				{
					base.NPC.netUpdate = true;
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -3f;
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
			if (shouldSwimAway)
			{
				base.NPC.TargetClosest();
				base.NPC.velocity.X = base.NPC.velocity.X - (float)base.NPC.direction * 0.25f;
				base.NPC.velocity.Y = base.NPC.velocity.Y - (float)base.NPC.directionY * 0.15f;
				if (base.NPC.velocity.X > 6f)
				{
					base.NPC.velocity.X = 6f;
				}
				if (base.NPC.velocity.X < -6f)
				{
					base.NPC.velocity.X = -6f;
				}
				if (base.NPC.velocity.Y > 6f)
				{
					base.NPC.velocity.Y = 6f;
				}
				if (base.NPC.velocity.Y < -6f)
				{
					base.NPC.velocity.Y = -6f;
				}
				base.NPC.direction *= -1;
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
		base.NPC.rotation = base.NPC.velocity.X * 0.05f;
		if ((double)base.NPC.rotation < -0.1)
		{
			base.NPC.rotation = -0.1f;
		}
		if ((double)base.NPC.rotation > 0.1)
		{
			base.NPC.rotation = 0.1f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (!base.NPC.wet && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.frameCounter += 0.07500000298023224;
		base.NPC.frameCounter %= Main.npcFrameCount[base.NPC.type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water && !spawnInfo.Player.Calamity().clamity)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.6f;
		}
		return 0f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 68, hit.HitDirection, -1f);
		}
	}
}
