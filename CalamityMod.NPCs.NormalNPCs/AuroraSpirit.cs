using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class AuroraSpirit : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = -1;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -20f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 4f;
		value.Position.Y -= 4f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 86;
		base.NPC.damage = 40;
		base.NPC.width = 40;
		base.NPC.height = 24;
		base.NPC.defense = 8;
		base.NPC.alpha = 100;
		base.NPC.lifeMax = 200;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.knockBackResist = 0f;
		base.NPC.HitSound = SoundID.NPCHit5;
		base.NPC.DeathSound = SoundID.NPCDeath15;
		base.NPC.coldDamage = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AuroraSpiritBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AuroraSpirit")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		int currentFrame = 1;
		if (!Main.dedServ)
		{
			if (TextureAssets.Npc[base.Type].Value == null)
			{
				return;
			}
			currentFrame = TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type];
		}
		if (!base.NPC.IsABestiaryIconDummy)
		{
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			if (base.NPC.direction == 1)
			{
				base.NPC.spriteDirection = 1;
			}
			if (base.NPC.direction == -1)
			{
				base.NPC.spriteDirection = -1;
			}
			base.NPC.rotation = (float)Math.Atan2((double)base.NPC.velocity.Y * (double)base.NPC.direction, (double)base.NPC.velocity.X * (double)base.NPC.direction);
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 4.0)
		{
			base.NPC.frame.Y += currentFrame;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y / currentFrame >= Main.npcFrameCount[base.Type])
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.ZoneSnow || !spawnInfo.Player.ZoneOverworldHeight || spawnInfo.Player.PillarZone() || spawnInfo.Player.ZoneDungeon || spawnInfo.Player.InSunkenSea() || !Main.hardMode || spawnInfo.PlayerInTown || spawnInfo.Player.ZoneOldOneArmy || Main.snowMoon || Main.pumpkinMoon)
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
			target.AddBuff(46, 60);
		}
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.NPC.Center, 0.02f, 0.7f, 0.7f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 67, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 67, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("CryoSpirit").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<EssenceofEleum>());
	}
}
