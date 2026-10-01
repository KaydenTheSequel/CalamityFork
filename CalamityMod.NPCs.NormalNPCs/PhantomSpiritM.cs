using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class PhantomSpiritM : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.PhantomSpirit.DisplayName");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.NPC.damage = 90;
		base.NPC.width = 32;
		base.NPC.height = 80;
		base.NPC.scale *= 1.1f;
		base.NPC.defense = 50;
		base.NPC.lifeMax = 2500;
		base.NPC.knockBackResist = 0.15f;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 40);
		base.NPC.HitSound = SoundID.NPCHit36;
		base.NPC.DeathSound = SoundID.NPCDeath39;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.Banner = ModContent.NPCType<PhantomSpirit>();
		base.BannerItem = ModContent.ItemType<PhantomSpiritBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.PhantomSpirit")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		float speed = (CalamityWorld.death ? 18f : (CalamityWorld.revenge ? 15.75f : 13.5f));
		CalamityRegularEnemyAI.DungeonSpiritAI(base.NPC, base.Mod, speed, -(float)Math.PI / 2f);
		int polterDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60);
		Dust obj = Main.dust[polterDust];
		obj.velocity *= 0.1f;
		obj.scale = 1.3f;
		obj.noGravity = true;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				int hitPolterDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, base.NPC.velocity.X, base.NPC.velocity.Y);
				Dust obj = Main.dust[hitPolterDust];
				obj.velocity *= 2f;
				obj.noGravity = true;
				obj.scale = 1.4f;
			}
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 0);
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<Necroplasm>(), 1, 1, 3);
	}
}
