using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class MantisShrimp : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 200;
		base.NPC.width = 40;
		base.NPC.height = 24;
		base.NPC.defense = 10;
		base.NPC.lifeMax = 60;
		base.NPC.aiStyle = 3;
		base.AIType = 67;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<MantisShrimpBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.MantisShrimp")
		});
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.spriteDirection = ((base.NPC.direction <= 0) ? 1 : (-1));
		Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
		float targetDist = ((Vector2)(ref val)).Length();
		targetDist *= 0.0025f;
		if ((double)targetDist > 1.5)
		{
			targetDist = 1.5f;
		}
		float maxSpeed = ((!Main.expertMode) ? (2.5f - targetDist) : (3f - targetDist));
		maxSpeed *= (CalamityWorld.death ? 1.2f : (CalamityWorld.revenge ? 1f : 0.8f));
		if (base.NPC.velocity.X < 0f - maxSpeed || base.NPC.velocity.X > maxSpeed)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.8f;
			}
		}
		else if (base.NPC.velocity.X < maxSpeed && base.NPC.direction == 1)
		{
			base.NPC.velocity.X = base.NPC.velocity.X + 1f;
			if (base.NPC.velocity.X > maxSpeed)
			{
				base.NPC.velocity.X = maxSpeed;
			}
		}
		else if (base.NPC.velocity.X > 0f - maxSpeed && base.NPC.direction == -1)
		{
			base.NPC.velocity.X = base.NPC.velocity.X - 1f;
			if (base.NPC.velocity.X < 0f - maxSpeed)
			{
				base.NPC.velocity.X = 0f - maxSpeed;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), target.Center, Vector2.Zero, 612, 0, 0f, Main.myPlayer);
		}
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(36, 600);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.Calamity().ZoneSulphur)
		{
			return 0f;
		}
		return SpawnCondition.OceanMonster.Chance * 0.2f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.AddIf(() => NPC.downedPlantBoss, ModContent.ItemType<MantisClaws>(), 5);
		npcLoot.Add(ItemDropRule.NormalvsExpert(886, 100, 50));
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("MantisShrimp").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("MantisShrimp2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("MantisShrimp3").Type);
			}
		}
	}
}
