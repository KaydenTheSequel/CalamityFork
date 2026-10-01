using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class ProfanedEnergyBody : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
		nPCBestiaryDrawModifiers.CustomTexturePath = "CalamityMod/ExtraTextures/Bestiary/ProfanedEnergy_Bestiary";
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 30f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.NPC.damage = 0;
		base.NPC.npcSlots = 3f;
		base.NPC.width = 72;
		base.NPC.height = 36;
		base.NPC.defense = 50;
		base.NPC.lifeMax = 2500;
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 50);
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = true;
		base.NPC.HitSound = SoundID.NPCHit52;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ProfanedEnergyBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ProfanedEnergyBody")
		});
	}

	public override void AI()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.energyFlame = base.NPC.whoAmI;
		if (Main.netMode != 1 && base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			for (int num723 = 0; num723 < 2; num723++)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<ProfanedEnergyLantern>(), base.NPC.whoAmI);
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !NPC.downedMoonlord || spawnInfo.Player.Calamity().ZoneCalamity || (!spawnInfo.Player.ZoneUnderworldHeight && !spawnInfo.Player.ZoneHallow) || Main.pumpkinMoon || Main.snowMoon || Main.eclipse)
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		if (SpawnCondition.Underworld.Chance > 0f)
		{
			return SpawnCondition.Underworld.Chance / 6f;
		}
		return SpawnCondition.OverworldHallow.Chance / 6f;
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
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedEnergyBody").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedEnergyBody2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedEnergyBody3").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<UnholyEssence>(), 1, 2, 4);
	}
}
