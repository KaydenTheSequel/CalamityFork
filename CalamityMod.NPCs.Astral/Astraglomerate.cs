using System;
using CalamityMod.BiomeManagers.BestiaryCategories;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class Astraglomerate : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/AstraglomerateGlow", (AssetRequestMode)2);
		}
		Main.npcFrameCount[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 38;
		base.NPC.height = 62;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 0;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 600;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AstraglomerateBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 90;
			base.NPC.defense = 40;
			base.NPC.lifeMax = 900;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralUnderground>().Type };
		if (Main.zenithWorld)
		{
			base.NPC.scale = 3f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Astraglomerate")
		});
	}

	public override void AI()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.ai[0]++;
		if (base.NPC.ai[0] > (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 120f : 180f)) && Main.rand.NextBool(100) && NPC.CountNPCS(ModContent.NPCType<Glomerling>()) < 10)
		{
			base.NPC.ai[0] = 0f;
			int n = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<Glomerling>(), 0, base.NPC.whoAmI);
			Main.npc[n].velocity.X = Main.rand.NextFloat(-0.4f, 0.4f);
			Main.npc[n].velocity.Y = Main.rand.NextFloat(-0.5f, -0.05f);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 10.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y > frameHeight * 4)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos, (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, new Vector2(19f, 30f), 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3);
		if (base.NPC.life > 0)
		{
			return;
		}
		int type = ModContent.NPCType<Glomerling>();
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			if (Main.npc[i].type == type)
			{
				Main.npc[i].ai[0] = -1f;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player) || !spawnInfo.Player.InAstral())
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(2))
		{
			return 0.17f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 1, 3, 1, 4));
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<HivePod>(), 10);
	}
}
