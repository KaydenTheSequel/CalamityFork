using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Placeables.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

public class SeaFloaty : ModNPC
{
	private bool hasBeenHit;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.NPC.type] = 6;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 0.5f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 5;
		base.NPC.width = 72;
		base.NPC.height = 22;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 50;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.value = Item.buyPrice(0, 0, 0, 50);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SeaFloatyBanner>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SunkenSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SeaFloaty")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
		writer.Write(hasBeenHit);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.chaseable = reader.ReadBoolean();
		hasBeenHit = reader.ReadBoolean();
	}

	public override void AI()
	{
		if (base.NPC.velocity.X > 0.25f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X < 0.25f)
		{
			base.NPC.spriteDirection = 1;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.direction = 1;
			base.NPC.ai[0] = 1f;
		}
		base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.1f;
		if (base.NPC.velocity.X < -2.5f || base.NPC.velocity.X > 2.5f)
		{
			base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
		}
		if (base.NPC.collideX)
		{
			base.NPC.velocity.X = base.NPC.velocity.X * -1f;
			base.NPC.direction *= -1;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.justHit && !hasBeenHit)
		{
			hasBeenHit = true;
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
		}
		base.NPC.chaseable = hasBeenHit;
		if (hasBeenHit)
		{
			base.NPC.TargetClosest();
			base.NPC.velocity.X = base.NPC.velocity.X - (float)base.NPC.direction * 0.5f;
			base.NPC.velocity.Y = base.NPC.velocity.Y - (float)base.NPC.directionY * 0.3f;
			if (base.NPC.velocity.X > 10f)
			{
				base.NPC.velocity.X = 10f;
			}
			if (base.NPC.velocity.X < -10f)
			{
				base.NPC.velocity.X = -10f;
			}
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
			if (base.NPC.velocity.Y < -10f)
			{
				base.NPC.velocity.Y = -10f;
			}
			base.NPC.direction *= -1;
			base.NPC.rotation = base.NPC.velocity.X * 0.1f;
			if ((double)base.NPC.rotation < -0.3)
			{
				base.NPC.rotation = -0.3f;
			}
			if ((double)base.NPC.rotation > 0.3)
			{
				base.NPC.rotation = 0.3f;
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (hasBeenHit ? 0.3f : 0.15f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.NPC.type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water && !spawnInfo.Player.Calamity().clamity)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.45f;
		}
		return 0f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 68, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 68, hit.HitDirection, -1f);
			}
			if (Main.netMode != 2)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SeaFloatyGore1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SeaFloatyGore2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SeaFloatyGore3").Type);
			}
		}
	}
}
