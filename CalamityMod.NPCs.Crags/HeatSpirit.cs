using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crags;

public class HeatSpirit : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = -1;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -32f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 8f;
		value.Position.Y -= 8f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.lavaImmune = true;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 33;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.defense = 8;
		base.NPC.lifeMax = 50;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = SoundID.NPCHit52;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.NPC.noGravity = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<HeatSpiritBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.HeatSpirit")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.Calamity().ZoneCalamity)
		{
			return 0f;
		}
		return 0.125f;
	}

	public override void FindFrame(int frameHeight)
	{
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
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y * (float)base.NPC.direction, base.NPC.velocity.X * (float)base.NPC.direction);
		}
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.5f, 0f, 0.05f);
		CalamityGlobalNPC.DoFlyingAI(base.NPC, CalamityWorld.death ? 12.8f : (CalamityWorld.revenge ? 8.8f : 7.8f), CalamityWorld.death ? 0.1f : (CalamityWorld.revenge ? 0.05f : 0.04f), 350f);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 60);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.DefineConditionalDropSet(DropHelper.Hardmode()).Add(ModContent.ItemType<EssenceofHavoc>(), 2);
	}
}
