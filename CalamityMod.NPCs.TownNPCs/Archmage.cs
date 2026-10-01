using System;
using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.NPCs.TownNPCs;

[AutoloadHead]
[LegacyName(new string[] { "DILF" })]
public class Archmage : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 25;
		NPCID.Sets.ExtraFramesCount[base.Type] = 9;
		NPCID.Sets.AttackFrameCount[base.Type] = 4;
		NPCID.Sets.DangerDetectRange[base.Type] = 700;
		NPCID.Sets.AttackType[base.Type] = 0;
		NPCID.Sets.AttackTime[base.Type] = 90;
		NPCID.Sets.AttackAverageChance[base.Type] = 30;
		NPCID.Sets.ShimmerTownTransform[base.Type] = false;
		base.NPC.Happiness.SetBiomeAffection<SnowBiome>(AffectionLevel.Like).SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike).SetNPCAffection(108, AffectionLevel.Like)
			.SetNPCAffection(209, AffectionLevel.Dislike);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Velocity = 1f;
		NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(base.NPC.type, drawModifiers);
	}

	public override void SetDefaults()
	{
		base.NPC.townNPC = true;
		base.NPC.friendly = true;
		base.NPC.lavaImmune = true;
		base.NPC.width = 18;
		base.NPC.height = 40;
		base.NPC.aiStyle = 7;
		base.NPC.damage = 10;
		base.NPC.defense = 15;
		base.NPC.lifeMax = 20000;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0.8f;
		base.AnimationType = 22;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Archmage")
		});
	}

	public override void AI()
	{
		if (!CalamityWorld.foundHomePermafrost && !base.NPC.homeless)
		{
			CalamityWorld.foundHomePermafrost = true;
		}
	}

	public override bool CanTownNPCSpawn(int numTownNPCs)
	{
		if (NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>()) && Main.zenithWorld)
		{
			return false;
		}
		return DownedBossSystem.downedCryogen;
	}

	public override List<string> SetNPCNameList()
	{
		return new List<string> { this.GetLocalizedValue("Name.Permafrost") };
	}

	public override string GetChat()
	{
		if (base.NPC.homeless && !CalamityWorld.foundHomePermafrost)
		{
			return this.GetLocalizedValue("Chat.Homeless" + Main.rand.Next(1, 3));
		}
		WeightedRandom<string> dialogue = new WeightedRandom<string>();
		dialogue.Add(this.GetLocalizedValue("Chat.Normal1"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal2"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal3"));
		if (Main.dayTime && !Main.LocalPlayer.ZoneSnow)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Day1"));
			dialogue.Add(this.GetLocalizedValue("Chat.Day2"));
		}
		else if (!Main.dayTime)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Night1"));
			dialogue.Add(this.GetLocalizedValue("Chat.Night2"));
		}
		if (BirthdayParty.PartyIsUp)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Party"));
		}
		if (Main.bloodMoon)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.BloodMoon1"));
			dialogue.Add(this.GetLocalizedValue("Chat.BloodMoon2"));
		}
		if (NPC.downedMoonlord)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.MoonLordDefeated1"));
			dialogue.Add(this.GetLocalizedValue("Chat.MoonLordDefeated2"));
		}
		return dialogue;
	}

	public override void PartyHatPosition(ref Vector2 position, ref SpriteEffects spriteEffects)
	{
		position.X -= 5f * (float)base.NPC.direction;
	}

	public override void SetChatButtons(ref string button, ref string button2)
	{
		button = Language.GetTextValue("LegacyInterface.28");
	}

	public override void OnChatButtonClicked(bool firstButton, ref string shopName)
	{
		if (firstButton)
		{
			shopName = "Shop";
		}
	}

	public override void AddShops()
	{
		new NPCShop(base.Type).Add<FrostbiteBlaster>(Array.Empty<Condition>()).Add<IcicleTrident>(Array.Empty<Condition>()).Add<IceStar>(Array.Empty<Condition>())
			.Add<ArcticBearPaw>(new Condition[1] { Condition.DownedMechBossAll })
			.Add<CryogenicStaff>(new Condition[1] { Condition.DownedMechBossAll })
			.Add<FrostyFlare>(new Condition[1] { Condition.DownedMechBossAll })
			.Add<Cryophobia>(new Condition[1] { Condition.DownedMechBossAll })
			.Add<AbsoluteZero>(new Condition[3]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			})
			.Add<EternalBlizzard>(new Condition[3]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			})
			.Add<WintersFury>(new Condition[3]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			})
			.Add<HailstormBullet>(new Condition[3]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			})
			.Add<IcicleArrow>(new Condition[3]
			{
				Condition.DownedEverscream,
				Condition.DownedSantaNK1,
				Condition.DownedIceQueen
			})
			.Add<PermafrostsConcoction>(Array.Empty<Condition>())
			.Add(2209)
			.Add<DeliciousMeat>(Array.Empty<Condition>())
			.Add<Popo>(Array.Empty<Condition>())
			.Add<FrigidMonolith>(Array.Empty<Condition>())
			.Add<BloodRune>(new Condition[1] { Condition.PlayerCarriesItem(ModContent.ItemType<IceBarrage>()) })
			.Register();
	}

	public override bool CanGoToStatue(bool toKingStatue)
	{
		return toKingStatue;
	}

	public override void TownNPCAttackStrength(ref int damage, ref float knockback)
	{
		damage = 150;
		knockback = 9f;
	}

	public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
	{
		cooldown = 30;
		randExtraCooldown = 15;
	}

	public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
	{
		projType = ModContent.ProjectileType<DarkIce>();
		attackDelay = 1;
	}

	public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
	{
		multiplier = 2f;
	}
}
