using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SulphurousSea;

public class Gnasher : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 25;
		base.NPC.width = 50;
		base.NPC.height = 36;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 50;
		base.NPC.aiStyle = 3;
		base.AIType = 67;
		base.NPC.value = Item.buyPrice(0, 0, 0, 60);
		base.NPC.HitSound = SoundID.NPCHit50;
		base.NPC.DeathSound = SoundID.NPCDeath54;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<GnasherBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SulphurousSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Gnasher")
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
		float maxVelocity = ((!Main.expertMode) ? (2.25f - targetDist) : (2.5f - targetDist));
		maxVelocity *= (CalamityWorld.death ? 1.2f : (CalamityWorld.revenge ? 1f : 0.8f));
		if (base.NPC.velocity.X < 0f - maxVelocity || base.NPC.velocity.X > maxVelocity)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.8f;
			}
		}
		else if (base.NPC.velocity.X < maxVelocity && base.NPC.direction == 1)
		{
			base.NPC.velocity.X = base.NPC.velocity.X + 1f;
			if (base.NPC.velocity.X > maxVelocity)
			{
				base.NPC.velocity.X = maxVelocity;
			}
		}
		else if (base.NPC.velocity.X > 0f - maxVelocity && base.NPC.direction == -1)
		{
			base.NPC.velocity.X = base.NPC.velocity.X - 1f;
			if (base.NPC.velocity.X < 0f - maxVelocity)
			{
				base.NPC.velocity.X = 0f - maxVelocity;
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.NPC.type];
		base.NPC.frameCounter %= Main.npcFrameCount[base.NPC.type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
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
		if (spawnInfo.Player.Calamity().ZoneSulphur)
		{
			return 0.1f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<ContaminatedBile>(), 5);
		npcLoot.AddIf(() => Main.hardMode, 1328, 10);
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Gnasher").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Gnasher2").Type);
			}
		}
	}
}
