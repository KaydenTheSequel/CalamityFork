using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.NPCs.SulphurousSea;

public class AquaticUrchin : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y += 12f;
		value.PortraitPositionYOverride = 32f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = (Main.hardMode ? 50 : 25);
		base.NPC.width = 20;
		base.NPC.height = 20;
		base.NPC.defense = 10;
		base.NPC.lifeMax = (Main.hardMode ? 300 : 100);
		base.NPC.knockBackResist = 0.8f;
		base.NPC.value = Item.buyPrice(0, 0, 3);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath15;
		base.NPC.behindTiles = true;
		base.NPC.npcSlots = 0.3333f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AquaticUrchinBanner>();
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.waterMovementSpeed = 1f;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[2]
		{
			ModContent.GetInstance<SulphurousSeaBiome>().Type,
			ModContent.GetInstance<AbyssLayer1Biome>().Type
		};
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AquaticUrchin")
		});
	}

	public static void DoUrchinAI(NPC npc)
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1 && npc.localAI[0] == 0f)
		{
			GenSearch search = (Main.rand.NextBool() ? ((GenSearch)new Searches.Down(300)) : ((GenSearch)new Searches.Up(300)));
			if (WorldUtils.Find(npc.Center.ToTileCoordinates(), Searches.Chain(search, new Conditions.IsSolid()), out var result))
			{
				npc.Center = result.ToWorldCoordinates();
				npc.netUpdate = true;
			}
			npc.localAI[0] = 1f;
		}
		float speedFactor = 0.018f;
		npc.collideX = MathHelper.Distance(npc.position.X, npc.oldPosition.X) < 0.01f;
		npc.collideY = MathHelper.Distance(npc.position.Y, npc.oldPosition.Y) < 0.01f;
		npc.noGravity = Collision.SolidCollision(npc.TopLeft - Vector2.One * 12f, npc.width + 6, npc.height + 6);
		if (npc.ai[1] == 0f)
		{
			if (npc.direction == 0)
			{
				npc.direction = 1;
			}
			npc.rotation += (float)(npc.direction * npc.directionY) * speedFactor * 0.006f;
			if (npc.collideY)
			{
				npc.ai[0] = 2f;
			}
			if (!npc.collideY && npc.ai[0] == 2f)
			{
				npc.direction = -npc.direction;
				npc.ai[1] = 1f;
				npc.ai[0] = 1f;
			}
			if (npc.collideX)
			{
				npc.directionY = -npc.directionY;
				npc.ai[1] = 1f;
			}
		}
		else
		{
			npc.rotation -= (float)(npc.direction * npc.directionY) * 0.006f;
			if (npc.collideX)
			{
				npc.ai[0] = 2f;
			}
			if (!npc.collideX && npc.ai[0] == 2f)
			{
				npc.directionY = -npc.directionY;
				npc.ai[1] = 0f;
				npc.ai[0] = 1f;
			}
			if (npc.collideY)
			{
				npc.direction = -npc.direction;
				npc.ai[1] = 0f;
			}
		}
		npc.velocity.X = (float)npc.direction * speedFactor;
		if (npc.noGravity)
		{
			npc.velocity.Y = (float)npc.directionY * speedFactor;
		}
		Vector2 position = npc.Center - Vector2.One * 6f;
		npc.velocity = Collision.noSlopeCollision(position, npc.velocity, 12, 12, fallThrough: true, fall2: true);
	}

	public override void AI()
	{
		DoUrchinAI(base.NPC);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe)
		{
			return 0f;
		}
		if ((spawnInfo.Player.Calamity().ZoneSulphur || spawnInfo.Player.Calamity().ZoneAbyssLayer1) && spawnInfo.Water && NPC.CountNPCS(ModContent.NPCType<AquaticUrchin>()) < 12)
		{
			return 1f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<UrchinStinger>(), 15);
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AquaticUrchin").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AquaticUrchin2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AquaticUrchin3").Type);
			}
		}
	}
}
