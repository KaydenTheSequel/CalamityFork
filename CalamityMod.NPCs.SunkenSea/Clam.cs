using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.SunkenSea;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

public class Clam : ModNPC
{
	private int hitAmount;

	private bool hasBeenHit;

	private bool statChange;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.NPC.type] = 5;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = (Main.hardMode ? 60 : 30);
		base.NPC.width = 56;
		base.NPC.height = 38;
		base.NPC.defense = 9999;
		base.NPC.lifeMax = (Main.hardMode ? 300 : 150);
		if (Main.expertMode)
		{
			base.NPC.lifeMax *= 2;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = (Main.hardMode ? Item.buyPrice(0, 0, 5) : Item.buyPrice(0, 0, 1));
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.knockBackResist = 0.05f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ClamBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SunkenSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Clam")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hitAmount);
		writer.Write(base.NPC.chaseable);
		writer.Write(hasBeenHit);
		writer.Write(statChange);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hitAmount = reader.ReadInt32();
		base.NPC.chaseable = reader.ReadBoolean();
		hasBeenHit = reader.ReadBoolean();
		statChange = reader.ReadBoolean();
	}

	public override void AI()
	{
		base.NPC.TargetClosest();
		if (Main.player[base.NPC.target].Calamity().clamity)
		{
			hitAmount = 3;
			hasBeenHit = true;
		}
		if (base.NPC.justHit && hitAmount < 3)
		{
			hitAmount++;
			hasBeenHit = true;
		}
		base.NPC.chaseable = hasBeenHit;
		if (hitAmount == 3)
		{
			if (!statChange)
			{
				base.NPC.defense = (Main.hardMode ? 15 : 6);
				base.NPC.damage = base.NPC.defDamage;
				statChange = true;
			}
			if (base.NPC.ai[0] == 0f)
			{
				if (Main.netMode != 1)
				{
					if (base.NPC.velocity.X == 0f && !(base.NPC.velocity.Y < 0f))
					{
						_ = (double)base.NPC.velocity.Y;
						_ = 0.9;
					}
					base.NPC.ai[0] = 1f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.ai[2]++;
				int decelerationTimer = 20;
				if (base.NPC.ai[1] == 0f)
				{
					decelerationTimer = 12;
				}
				if (base.NPC.ai[2] < (float)decelerationTimer)
				{
					base.NPC.velocity.X *= 0.9f;
					return;
				}
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				if (base.NPC.direction == 0)
				{
					base.NPC.direction = -1;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
				base.NPC.ai[1]++;
				base.NPC.ai[3]++;
				if (base.NPC.ai[3] >= 4f)
				{
					base.NPC.ai[3] = 0f;
					if (base.NPC.ai[1] == 2f)
					{
						float multiplierX = Main.rand.Next(3, 7);
						base.NPC.velocity.X = (float)base.NPC.direction * multiplierX;
						base.NPC.velocity.Y = -8f;
						base.NPC.ai[1] = 0f;
					}
					else
					{
						float multiplierX2 = Main.rand.Next(5, 9);
						base.NPC.velocity.X = (float)base.NPC.direction * multiplierX2;
						base.NPC.velocity.Y = -4f;
					}
				}
				base.NPC.netUpdate = true;
			}
			else if (base.NPC.direction == 1 && base.NPC.velocity.X < 1f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + 0.1f;
			}
			else if (base.NPC.direction == -1 && base.NPC.velocity.X > -1f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - 0.1f;
			}
		}
		else
		{
			base.NPC.damage = 0;
		}
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 4.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
		}
		if (hitAmount < 3)
		{
			base.NPC.frame.Y = frameHeight * 4;
		}
		else if (base.NPC.frame.Y > frameHeight * 3)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water)
		{
			return SpawnCondition.CaveJellyfish.Chance * 1.2f;
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
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 37, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 37, hit.HitDirection, -1f);
			}
			if (Main.netMode != 2)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Clam1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Clam2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Clam3").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<Navystone>(), 1, 8, 12);
		npcLoot.Add(4412, 8);
		npcLoot.Add(4413, 16);
		npcLoot.Add(4414, 40);
		npcLoot.AddIf(() => Main.hardMode, ModContent.ItemType<MolluskHusk>(), 2);
	}
}
