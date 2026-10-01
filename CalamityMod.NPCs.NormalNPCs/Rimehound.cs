using System.IO;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class Rimehound : ModNPC
{
	private bool reset;

	public static readonly SoundStyle GrowlSound = new SoundStyle("CalamityMod/Sounds/Custom/RimehoundGrowl");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/RimehoundHit");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 9;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.7f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 10f;
		nPCBestiaryDrawModifiers.Velocity = 1.2f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.NPC.damage = 10;
		base.NPC.width = 56;
		base.NPC.height = 56;
		base.NPC.defense = 4;
		base.NPC.lifeMax = 80;
		if (DownedBossSystem.downedCryogen)
		{
			base.NPC.damage = 60;
			base.NPC.defense = 10;
			base.NPC.lifeMax = 600;
		}
		base.NPC.knockBackResist = 0.3f;
		base.AnimationType = 329;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = SoundID.NPCDeath5;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<RimehoundBanner>();
		base.NPC.coldDamage = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Rimehound")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(reset);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		reset = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(900))
		{
			SoundEngine.PlaySound(in GrowlSound, base.NPC.Center);
		}
		if ((double)base.NPC.life <= (double)base.NPC.lifeMax * (CalamityWorld.death ? 0.9 : (CalamityWorld.revenge ? 0.7 : 0.5)))
		{
			if (!reset)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[3] = 0f;
				reset = true;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[1] < 7f)
			{
				base.NPC.ai[1]++;
			}
			CalamityRegularEnemyAI.UnicornAI(base.NPC, base.Mod, spin: true, CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 6f : 4f), 5f, 0.2f);
		}
		else
		{
			CalamityRegularEnemyAI.UnicornAI(base.NPC, base.Mod, spin: false, CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 6f : 4f), 6f, CalamityWorld.death ? 0.1f : (CalamityWorld.revenge ? 0.085f : 0.07f));
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.ai[1] < 7f && base.NPC.ai[1] > 0f)
		{
			base.NPC.frame.Y = frameHeight * 7;
			base.NPC.frameCounter = 0.0;
			return;
		}
		if (base.NPC.ai[1] >= 7f)
		{
			base.NPC.frame.Y = frameHeight * 8;
			base.NPC.frameCounter = 0.0;
			return;
		}
		if (base.NPC.velocity.Y > 0f || base.NPC.velocity.Y < 0f)
		{
			base.NPC.frame.Y = frameHeight * 5;
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.spriteDirection = base.NPC.direction;
		base.NPC.frameCounter += ((Vector2)(ref base.NPC.velocity)).Length() / 16f;
		if (base.NPC.frameCounter > 12.0)
		{
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y >= frameHeight * 6)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.ZoneSnow || spawnInfo.Player.PillarZone() || spawnInfo.Player.ZoneDungeon || spawnInfo.Player.InSunkenSea() || spawnInfo.PlayerInTown || spawnInfo.Player.ZoneOldOneArmy || Main.snowMoon || Main.pumpkinMoon)
		{
			return 0f;
		}
		return 0.03f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(44, 120);
		}
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
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AngryDog").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AngryDog2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AngryDog3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AngryDog4").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(259, 1, 1, 2);
		npcLoot.DefineConditionalDropSet(DropHelper.PostCryo()).Add(ModContent.ItemType<EssenceofEleum>());
	}
}
