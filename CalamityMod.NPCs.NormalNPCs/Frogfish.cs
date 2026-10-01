using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Potions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class Frogfish : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.chaseable = false;
		base.NPC.damage = 25;
		base.NPC.width = 60;
		base.NPC.height = 50;
		base.NPC.defense = 10;
		base.NPC.lifeMax = 100;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0.5f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<FrogfishBanner>();
		base.NPC.chaseable = false;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Frogfish")
		});
	}

	public override void AI()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		int alphaControl = 200;
		if (base.NPC.ai[2] == 0f)
		{
			base.NPC.alpha = alphaControl;
			base.NPC.TargetClosest();
			if (!Main.player[base.NPC.target].dead)
			{
				Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (((Vector2)(ref val)).Length() < 170f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.ai[2] = -16f;
				}
			}
			if (base.NPC.velocity.X != 0f || base.NPC.velocity.Y < 0f || base.NPC.velocity.Y > 2f || base.NPC.justHit)
			{
				base.NPC.ai[2] = -16f;
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
				base.NPC.ai[2] = 1f;
				base.NPC.velocity.X = base.NPC.direction * 2;
			}
		}
		else
		{
			base.NPC.alpha = 0;
			if (base.NPC.ai[2] == 1f)
			{
				base.NPC.chaseable = true;
				CalamityRegularEnemyAI.PassiveSwimmingAI(base.NPC, base.Mod, 0, 0f, 0.15f, 0.15f, 3.5f, 1.5f, 0.1f);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(70, 180);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (!base.NPC.wet && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneSulphur)
		{
			return 0f;
		}
		return SpawnCondition.OceanMonster.Chance * 0.2f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<AnechoicCoating>(), 2);
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Frogfish").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Frogfish2").Type);
			}
		}
	}
}
