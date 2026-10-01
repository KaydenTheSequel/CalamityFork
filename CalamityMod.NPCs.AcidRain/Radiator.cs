using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class Radiator : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 67;
		base.NPC.damage = 10;
		base.NPC.width = 24;
		base.NPC.height = 24;
		base.NPC.defense = 5;
		base.NPC.lifeMax = 50;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.damage = 60;
			base.NPC.lifeMax = 3250;
			base.NPC.defense = 20;
		}
		else if (DownedBossSystem.downedAquaticScourge)
		{
			base.NPC.damage = 30;
			base.NPC.lifeMax = 130;
			base.NPC.defense = 10;
		}
		base.NPC.knockBackResist = 0.8f;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.AIType = 360;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<RadiatorBanner>();
		base.NPC.catchItem = (short)ModContent.ItemType<RadiatingCrystal>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Radiator")
		});
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.NPC.Center, 0.3f, 1.5f, 0.3f);
		if (Main.dedServ)
		{
			return;
		}
		int auraSize = 200;
		Player player = Main.LocalPlayer;
		if (player.dead || !player.active)
		{
			return;
		}
		Vector2 val = player.Center - base.NPC.Center;
		if (((Vector2)(ref val)).Length() < (float)auraSize && !player.creativeGodMode)
		{
			player.AddBuff(ModContent.BuffType<Irradiated>(), 3, quiet: false);
			player.AddBuff(20, 2, quiet: false);
			if (DownedBossSystem.downedPolterghast)
			{
				player.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 3, quiet: false);
				player.AddBuff(70, 2, quiet: false);
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 8.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y > frameHeight * 2)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<SulphuricScale>(), 2, 1, 3);
		npcLoot.Add(ItemDropRule.NormalvsExpert(887, 100, 50));
	}
}
