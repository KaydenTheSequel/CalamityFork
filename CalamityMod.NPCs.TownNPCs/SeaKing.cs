using System;
using System.Collections.Generic;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Rogue;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.NPCs.TownNPCs;

[AutoloadHead]
[LegacyName(new string[] { "SEAHOE" })]
public class SeaKing : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 25;
		NPCID.Sets.ExtraFramesCount[base.Type] = 5;
		NPCID.Sets.AttackFrameCount[base.Type] = 4;
		NPCID.Sets.DangerDetectRange[base.Type] = 700;
		NPCID.Sets.AttackType[base.Type] = 0;
		NPCID.Sets.AttackTime[base.Type] = 90;
		NPCID.Sets.AttackAverageChance[base.Type] = 30;
		NPCID.Sets.HatOffsetY[base.Type] = 16;
		NPCID.Sets.ShimmerTownTransform[base.Type] = false;
		base.NPC.Happiness.SetBiomeAffection<OceanBiome>(AffectionLevel.Like).SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike).SetNPCAffection(229, AffectionLevel.Like)
			.SetNPCAffection(38, AffectionLevel.Dislike)
			.SetNPCAffection(369, AffectionLevel.Hate);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Velocity = 1f;
		NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(base.NPC.type, drawModifiers);
	}

	public override void SetDefaults()
	{
		base.NPC.townNPC = true;
		base.NPC.friendly = true;
		base.NPC.width = 30;
		base.NPC.height = 58;
		base.NPC.aiStyle = 7;
		base.NPC.damage = 10;
		base.NPC.defense = 25;
		base.NPC.lifeMax = 7500;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0.65f;
		base.AnimationType = 22;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SeaKing")
		});
	}

	public override bool CanTownNPCSpawn(int numTownNPCs)
	{
		if (DownedBossSystem.downedCLAM)
		{
			return DownedBossSystem.downedDesertScourge;
		}
		return false;
	}

	public override List<string> SetNPCNameList()
	{
		return new List<string> { this.GetLocalizedValue("Name.Amidias") };
	}

	public override void AI()
	{
		base.NPC.breath += 2;
	}

	public override string GetChat()
	{
		WeightedRandom<string> dialogue = new WeightedRandom<string>();
		_ = Main.LocalPlayer;
		if (base.NPC.homeless)
		{
			return this.GetLocalizedValue("Chat.Homeless" + Main.rand.Next(1, 3));
		}
		if (Main.dayTime)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Day1"));
			dialogue.Add(this.GetLocalizedValue("Chat.Day2"));
			dialogue.Add(this.GetLocalizedValue("Chat.Day3"));
			dialogue.Add(this.GetLocalizedValue("Chat.Day4"));
		}
		else
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Night1"));
			dialogue.Add(this.GetLocalizedValue("Chat.Night2"));
			dialogue.Add(this.GetLocalizedValue("Chat.Night3"));
		}
		int angler = NPC.FindFirstNPC(369);
		if (angler != -1)
		{
			dialogue.Add(this.GetLocalization("Chat.Angler").Format(Main.npc[angler].GivenName));
		}
		if (NPC.FindFirstNPC(ModContent.NPCType<BrimstoneWitch>()) != -1)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.BrimstoneWitch"));
		}
		int partyGirl = NPC.FindFirstNPC(208);
		if (partyGirl != -1)
		{
			dialogue.Add(this.GetLocalization("Chat.PartyGirl").Format(Main.npc[partyGirl].GivenName));
		}
		if (Main.bloodMoon)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.BloodMoon1"));
			dialogue.Add(this.GetLocalizedValue("Chat.BloodMoon2"));
		}
		if (Main.hardMode)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Hardmode1"));
			dialogue.Add(this.GetLocalizedValue("Chat.Hardmode2"));
		}
		if (NPC.downedMoonlord)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.MoonLordDefeated1"));
			dialogue.Add(this.GetLocalizedValue("Chat.MoonLordDefeated2"));
		}
		if (DownedBossSystem.downedDoG)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.DoGDefeated"));
		}
		return dialogue;
	}

	public string Lore()
	{
		int selector = (int)base.NPC.Calamity().newAI[0];
		if (DownedBossSystem.downedYharon)
		{
			return this.GetLocalizedValue("Help.YharonDefeated" + (1 + selector % 3));
		}
		if (DownedBossSystem.downedDoG)
		{
			return this.GetLocalizedValue("Help.DoGDefeated" + (1 + selector % 3));
		}
		if (DownedBossSystem.downedPolterghast)
		{
			return this.GetLocalizedValue("Help.PolterghastDefeated" + (1 + selector % 2));
		}
		if (DownedBossSystem.downedProvidence)
		{
			return this.GetLocalizedValue("Help.ProvidenceDefeated" + (1 + selector % 2));
		}
		if (NPC.downedMoonlord)
		{
			return this.GetLocalizedValue("Help.MoonLordDefeated" + (1 + selector % 5));
		}
		if (NPC.downedGolemBoss)
		{
			return this.GetLocalizedValue("Help.GolemDefeated" + (1 + selector % 3));
		}
		if (Main.hardMode)
		{
			return this.GetLocalizedValue("Help.Hardmode" + (1 + selector % (DownedBossSystem.downedCryogen ? 6 : 7)));
		}
		int chosen = 1 + selector % 10;
		string worldEvil = Language.GetTextValue("LegacyMisc." + (WorldGen.crimson ? 102 : 101));
		if (chosen == 6 || chosen == 9)
		{
			return this.GetLocalization("Help.PreHardmode" + chosen).Format(worldEvil);
		}
		return this.GetLocalizedValue("Help.PreHardmode" + chosen);
	}

	public override void SetChatButtons(ref string button, ref string button2)
	{
		button = Language.GetTextValue("LegacyInterface.28");
		button2 = Language.GetTextValue("LegacyInterface.51");
	}

	public override void OnChatButtonClicked(bool firstButton, ref string shopName)
	{
		if (firstButton)
		{
			shopName = "Shop";
			return;
		}
		Main.npcChatText = Lore();
		base.NPC.Calamity().newAI[0]++;
		Main.LocalPlayer.AddBuff(ModContent.BuffType<AmidiasBlessing>(), 36000);
	}

	public override void AddShops()
	{
		new NPCShop(base.Type).Add<Shellshooter>(Array.Empty<Condition>()).Add<SnapClam>(Array.Empty<Condition>()).Add<SandDollar>(Array.Empty<Condition>())
			.Add<Waywasher>(Array.Empty<Condition>())
			.Add<AmidiasTrident>(Array.Empty<Condition>())
			.Add<EnchantedConch>(Array.Empty<Condition>())
			.Add<PolypLauncher>(Array.Empty<Condition>())
			.AddWithCustomValue(2673, Item.buyPrice(0, 25), Condition.Hardmode)
			.AddWithCustomValue<BloodwormItem>(Item.buyPrice(1), new Condition[1] { CalamityConditions.DownedOldDuke })
			.Register();
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Amidias").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Amidias2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Amidias3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Amidias4").Type);
		}
	}

	public override bool CanGoToStatue(bool toKingStatue)
	{
		return toKingStatue;
	}

	public override void TownNPCAttackStrength(ref int damage, ref float knockback)
	{
		damage = 30;
		knockback = 2f;
	}

	public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
	{
		cooldown = 5;
	}

	public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
	{
		projType = ModContent.ProjectileType<SnapClamProj>();
		attackDelay = 1;
	}

	public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
	{
		multiplier = 16f;
		gravityCorrection = 10f;
	}
}
