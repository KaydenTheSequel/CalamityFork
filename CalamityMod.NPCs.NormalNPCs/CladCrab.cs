using System;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class CladCrab : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.chaseable = false;
		base.NPC.damage = 12;
		base.NPC.defense = 7;
		base.NPC.width = 56;
		base.NPC.height = 54;
		base.NPC.lifeMax = 100;
		base.NPC.aiStyle = -1;
		base.NPC.knockBackResist = 0.4f;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.HitSound = SoundID.NPCHit41 with
		{
			Pitch = 0.6f
		};
		base.NPC.DeathSound = SoundID.NPCDeath36 with
		{
			Pitch = -0.4f
		};
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CladCrabBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void AI()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		float movementSpeed = 1f;
		if (base.NPC.HasPlayerTarget)
		{
			Player target = Main.player[base.NPC.target];
			if (target.Distance(base.NPC.Center) < 320f && target.Bottom.Y > base.NPC.Top.Y - 40f && Collision.CanHitLine(base.NPC.Center, 1, 1, target.Center, 1, 1))
			{
				if (base.NPC.ai[0] != 2f)
				{
					base.NPC.ai[1] = 60f;
					base.NPC.ai[0] = 2f;
				}
				base.NPC.ai[2] = 0f;
			}
			else
			{
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] > 300f)
				{
					base.NPC.ai[0] = Main.rand.Next(0, 2);
					base.NPC.ai[1] = Main.rand.Next(120, 301);
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
				}
			}
		}
		if (base.NPC.direction == 0)
		{
			base.NPC.direction = ((!Main.rand.NextBool()) ? 1 : (-1));
		}
		if (base.NPC.ai[1] <= 0f && base.NPC.ai[0] != 2f)
		{
			base.NPC.ai[0] = ((base.NPC.ai[0] == 0f) ? 1 : 0);
			base.NPC.ai[1] = Main.rand.Next(120, 301);
			base.NPC.direction = ((!Main.rand.NextBool()) ? 1 : (-1));
		}
		if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.ai[3] <= 0f && base.NPC.velocity.X == 0f)
			{
				base.NPC.direction *= -1;
				base.NPC.ai[3] = 30f;
			}
			if (!base.NPC.justHit)
			{
				base.NPC.velocity.X = MathHelper.Lerp(base.NPC.velocity.X, (float)base.NPC.direction * movementSpeed, 0.05f);
			}
			base.NPC.StepUpBlocks();
		}
		else if (base.NPC.ai[0] == 1f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X *= 0f;
			}
			if (base.NPC.justHit)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = Main.rand.Next(120, 301);
			}
		}
		else
		{
			Player target2 = Main.player[base.NPC.target];
			if (base.NPC.ai[3] <= 0f && base.NPC.velocity.X == 0f)
			{
				base.NPC.direction *= -1;
				base.NPC.ai[3] = 30f;
			}
			else if ((base.NPC.ai[3] <= 0f && target2.Distance(base.NPC.Center) > 160f) || base.NPC.ai[3] < -180f)
			{
				int dir = Math.Sign(Main.player[base.NPC.target].Center.X - base.NPC.Center.X);
				base.NPC.direction = dir;
				base.NPC.ai[3] = 120f;
			}
			if (!base.NPC.justHit)
			{
				base.NPC.velocity.X = MathHelper.Lerp(base.NPC.velocity.X, (float)base.NPC.direction * movementSpeed, 0.05f);
			}
			base.NPC.StepUpBlocks();
		}
		if (base.NPC.ai[1] > 0f)
		{
			base.NPC.ai[1]--;
		}
		base.NPC.ai[3]--;
		base.NPC.spriteDirection = -base.NPC.direction;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CladCrab")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSulphur || spawnInfo.Player.Calamity().ZoneSunkenSea || !spawnInfo.Player.InZonePurity())
		{
			return 0f;
		}
		return (Main.remixWorld ? SpawnCondition.Cavern.Chance : SpawnCondition.OverworldDaySlime.Chance) * 0.1f;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.velocity.Y == 0f)
		{
			if (!base.NPC.IsABestiaryIconDummy)
			{
				if (base.NPC.direction == 1)
				{
					base.NPC.spriteDirection = -1;
				}
				if (base.NPC.direction == -1)
				{
					base.NPC.spriteDirection = 1;
				}
				if (base.NPC.velocity.X == 0f)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
					return;
				}
			}
			base.NPC.frameCounter += (base.NPC.IsABestiaryIconDummy ? 0.6f : (Math.Abs(base.NPC.velocity.X) * 0.5f));
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 12.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y / frameHeight >= Main.npcFrameCount[base.Type] - 1)
			{
				base.NPC.frame.Y = frameHeight;
			}
		}
		else
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = frameHeight * (Main.npcFrameCount[base.Type] - 1);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(3, 1, 10, 20);
		npcLoot.Add(313, 1, 1, 4);
		npcLoot.Add(315, 1, 1, 3);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 2; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 1, hit.HitDirection, -1f);
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 2, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 7; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 1, hit.HitDirection, -1f);
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 2, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("CladCrab").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("CladCrab2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("CladCrab3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("CladCrab4").Type);
			}
		}
	}
}
