using System;
using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Packets;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.NPCs.TownNPCs;

[AutoloadHead]
[LegacyName(new string[] { "THIEF" })]
public class Bandit : ModNPC
{
	public static Asset<Texture2D> AltTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 23;
		NPCID.Sets.ExtraFramesCount[base.Type] = 9;
		NPCID.Sets.AttackFrameCount[base.Type] = 4;
		NPCID.Sets.DangerDetectRange[base.Type] = 500;
		NPCID.Sets.AttackType[base.Type] = 0;
		NPCID.Sets.AttackTime[base.Type] = 60;
		NPCID.Sets.AttackAverageChance[base.Type] = 10;
		NPCID.Sets.ShimmerTownTransform[base.Type] = false;
		base.NPC.Happiness.SetBiomeAffection<DesertBiome>(AffectionLevel.Like).SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike).SetNPCAffection(107, AffectionLevel.Like)
			.SetNPCAffection(20, AffectionLevel.Dislike);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Velocity = 1f;
		NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(base.NPC.type, drawModifiers);
		if (!Main.dedServ)
		{
			AltTexture = ModContent.Request<Texture2D>(Texture + "Alt", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.townNPC = true;
		base.NPC.friendly = true;
		base.NPC.lavaImmune = false;
		base.NPC.width = 18;
		base.NPC.height = 44;
		base.NPC.aiStyle = 7;
		base.NPC.damage = 10;
		base.NPC.defense = 15;
		base.NPC.lifeMax = 250;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.knockBackResist = 0.5f;
		base.AnimationType = 208;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Bandit")
		});
	}

	public override void AI()
	{
		if (!CalamityWorld.spawnedBandit)
		{
			CalamityWorld.spawnedBandit = true;
		}
	}

	public override bool CanTownNPCSpawn(int numTownNPCs)
	{
		if (CalamityWorld.spawnedBandit)
		{
			return true;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (player.InventoryHas(74) || player.PortableStorageHas(74))
			{
				return NPC.downedBoss3;
			}
		}
		return false;
	}

	public override List<string> SetNPCNameList()
	{
		return new List<string>
		{
			"Xplizzy",
			"Freakish",
			"Calder",
			"Hunter Jinx",
			"Goose",
			"Jackson",
			"Altarca",
			"Jackie",
			"Ishmael",
			"Ariallis",
			"Shade",
			"Orion",
			this.GetLocalizedValue("Name.Laura"),
			this.GetLocalizedValue("Name.Mie"),
			this.GetLocalizedValue("Name.Bonnie"),
			this.GetLocalizedValue("Name.Sarah"),
			this.GetLocalizedValue("Name.Diane"),
			this.GetLocalizedValue("Name.Kate"),
			this.GetLocalizedValue("Name.Penelope"),
			this.GetLocalizedValue("Name.Marisa"),
			this.GetLocalizedValue("Name.Maribel"),
			this.GetLocalizedValue("Name.Valerie"),
			this.GetLocalizedValue("Name.Jessica"),
			this.GetLocalizedValue("Name.Rowan"),
			this.GetLocalizedValue("Name.Jessie"),
			this.GetLocalizedValue("Name.Jade"),
			this.GetLocalizedValue("Name.Hearn"),
			this.GetLocalizedValue("Name.Amber"),
			this.GetLocalizedValue("Name.Anne"),
			this.GetLocalizedValue("Name.Indiana")
		};
	}

	public override string GetChat()
	{
		if (Main.bloodMoon)
		{
			return this.GetLocalizedValue("Chat.BloodMoon" + Main.rand.Next(1, 5));
		}
		WeightedRandom<string> dialogue = new WeightedRandom<string>();
		dialogue.Add(this.GetLocalizedValue("Chat.Normal1"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal2"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal3"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal4"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal5"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal6"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal7"));
		if (!Main.dayTime)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Night1"));
			dialogue.Add(this.GetLocalizedValue("Chat.Night2"));
		}
		int witch = NPC.FindFirstNPC(ModContent.NPCType<BrimstoneWitch>());
		if (witch != -1)
		{
			dialogue.Add(this.GetLocalization("Chat.BrimstoneWitch").Format(Main.npc[witch].GivenName));
		}
		int merchantIndex = NPC.FindFirstNPC(17);
		if (merchantIndex != -1)
		{
			dialogue.Add(this.GetLocalization("Chat.Merchant").Format(Main.npc[merchantIndex].GivenName));
		}
		int armsDealerIndex = NPC.FindFirstNPC(19);
		int nurseIndex = NPC.FindFirstNPC(18);
		if (armsDealerIndex != -1 && nurseIndex != -1)
		{
			dialogue.Add(this.GetLocalization("Chat.NurseArmsDealer").Format(Main.npc[nurseIndex].GivenName, Main.npc[armsDealerIndex].GivenName));
		}
		if (base.NPC.GivenName == this.GetLocalizedValue("Name.Laura"))
		{
			dialogue.Add(this.GetLocalizedValue("Chat.NamedLaura"));
		}
		if (base.NPC.GivenName == this.GetLocalizedValue("Name.Penelope"))
		{
			dialogue.Add(this.GetLocalizedValue("Chat.NamedPenelope"));
		}
		if (base.NPC.GivenName == this.GetLocalizedValue("Name.Valerie"))
		{
			dialogue.Add(this.GetLocalizedValue("Chat.NamedValerie"));
		}
		if (base.NPC.GivenName == this.GetLocalizedValue("Name.Rowan"))
		{
			dialogue.Add(this.GetLocalizedValue("Chat.NamedRowan"));
		}
		if (Main.LocalPlayer.ZoneJungle)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Jungle"));
		}
		if (BirthdayParty.PartyIsUp)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Party"));
		}
		if (Main.hardMode)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Hardmode1"));
			dialogue.Add(this.GetLocalizedValue("Chat.Hardmode2"));
			dialogue.Add(this.GetLocalizedValue("Chat.Hardmode3"));
		}
		if (NPC.downedMoonlord)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.MoonLordDefeated1"));
			dialogue.Add(this.GetLocalizedValue("Chat.MoonLordDefeated2"));
			dialogue.Add(this.GetLocalizedValue("Chat.MoonLordDefeated3"));
		}
		if (Main.LocalPlayer.InventoryHas(3245))
		{
			dialogue.Add(this.GetLocalizedValue("Chat.HasBoneGlove"));
		}
		if (Main.LocalPlayer.InventoryHas(ModContent.ItemType<Valediction>()))
		{
			dialogue.Add(this.GetLocalizedValue("Chat.HasValediction"));
		}
		return dialogue;
	}

	public string Refund()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		int goblinIndex = NPC.FindFirstNPC(107);
		if (goblinIndex != -1 && CalamityWorld.Reforges >= 1)
		{
			if (Main.netMode == 0)
			{
				DoRefund(base.NPC);
			}
			else if (Main.netMode == 1)
			{
				WantToRefundReforgesPacket.Send();
			}
			SoundEngine.PlaySound(in SoundID.Coins);
			switch (Main.rand.Next(2))
			{
			case 0:
				return this.GetLocalization("Refund1").Format(Main.npc[goblinIndex].GivenName);
			case 1:
				return this.GetLocalizedValue("Refund2");
			}
		}
		return this.GetLocalizedValue("NoRefund");
	}

	public static void DoRefund(NPC bandit)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (bandit != null && CalamityWorld.Reforges > 0)
		{
			int[] coinCounts = Utils.CoinsSplit(CalamityWorld.MoneyStolenByBandit);
			if (coinCounts[0] > 0)
			{
				Item.NewItem(new EntitySource_Gift(bandit), bandit.Hitbox, 71, coinCounts[0]);
			}
			if (coinCounts[1] > 0)
			{
				Item.NewItem(new EntitySource_Gift(bandit), bandit.Hitbox, 72, coinCounts[1]);
			}
			if (coinCounts[2] > 0)
			{
				Item.NewItem(new EntitySource_Gift(bandit), bandit.Hitbox, 73, coinCounts[2]);
			}
			if (coinCounts[3] > 0)
			{
				Item.NewItem(new EntitySource_Gift(bandit), bandit.Hitbox, 74, coinCounts[3]);
			}
			CalamityWorld.MoneyStolenByBandit = 0;
			CalamityWorld.Reforges = 0;
			CalamityNetcode.SyncWorld();
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.Calamity().trippy)
		{
			return false;
		}
		SpriteEffects something = (SpriteEffects)(base.NPC.direction != -1);
		spriteBatch.Draw(BirthdayParty.PartyIsUp ? AltTexture.Value : TextureAssets.Npc[base.Type].Value, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY) - new Vector2(0f, 6f), (Rectangle?)base.NPC.frame, drawColor, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, something, 0f);
		return false;
	}

	public override void SetChatButtons(ref string button, ref string button2)
	{
		button = Language.GetTextValue("LegacyInterface.28");
		button2 = this.GetLocalizedValue("RefundButton");
	}

	public override void OnChatButtonClicked(bool firstButton, ref string shopName)
	{
		if (firstButton)
		{
			shopName = "Shop";
		}
		else
		{
			Main.npcChatText = Refund();
		}
	}

	public override void AddShops()
	{
		new NPCShop(base.Type).Add<Cinquedea>(Array.Empty<Condition>()).Add<Glaive>(Array.Empty<Condition>()).Add<SlickCane>(Array.Empty<Condition>())
			.Add<OldDie>(Array.Empty<Condition>())
			.Add(976)
			.Add<ThiefsDime>(new Condition[1] { Condition.DownedPirates })
			.Add<MomentumCapacitor>(new Condition[1] { Condition.DownedMechBossAll })
			.Add<DeepWounder>(new Condition[1] { CalamityConditions.DownedCalamitasClone })
			.Add<GloveOfPrecision>(new Condition[1] { Condition.DownedPlantera })
			.Add<GloveOfRecklessness>(new Condition[1] { Condition.DownedPlantera })
			.Add<EtherealExtorter>(new Condition[1] { Condition.DownedGolem })
			.Add<CelestialReaper>(new Condition[1] { Condition.DownedMoonLord })
			.Add<VeneratedLocket>(new Condition[1] { CalamityConditions.DownedDevourerOfGods })
			.Add<DragonScales>(new Condition[1] { CalamityConditions.DownedYharon })
			.Add<BearsEye>(Array.Empty<Condition>())
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
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bandit").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bandit2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bandit3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bandit4").Type);
		}
	}

	public override bool CanGoToStatue(bool toKingStatue)
	{
		return true;
	}

	public override void TownNPCAttackStrength(ref int damage, ref float knockback)
	{
		damage = 50;
		knockback = 2f;
	}

	public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
	{
		cooldown = 90;
		randExtraCooldown = 15;
	}

	public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
	{
		projType = ModContent.ProjectileType<CinquedeaProj>();
		attackDelay = 1;
	}

	public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
	{
		multiplier = 6f;
	}
}
