using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class BabyFlakCrab : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 26;
		base.NPC.height = 32;
		base.NPC.damage = 0;
		base.NPC.lifeMax = 5;
		base.NPC.defense = 5;
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit41;
		base.NPC.DeathSound = SoundID.DD2_WitherBeastDeath;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BabyFlakCrabBanner>();
		base.NPC.dontTakeDamageFromHostiles = true;
		base.NPC.catchItem = (short)ModContent.ItemType<BabyFlakCrabItem>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SulphurousSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BabyFlakCrab")
		});
	}

	public override void AI()
	{
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			if (Main.rand.NextBool(20))
			{
				base.NPC.catchItem = (short)ModContent.ItemType<GeyserShell>();
			}
			base.NPC.localAI[0] = 1f;
			base.NPC.velocity.Y = -3f;
			base.NPC.netUpdate = true;
		}
		if (Main.rand.NextBool(8) && base.NPC.catchItem == (short)ModContent.ItemType<GeyserShell>())
		{
			int dust = Dust.NewDust(base.NPC.position - new Vector2(2f, 2f), base.NPC.width + 4, base.NPC.height + 4, 75, base.NPC.velocity.X * 0.4f, base.NPC.velocity.Y * 0.4f, 200);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 1.1f;
			Main.dust[dust].velocity.Y += 0.25f;
			Main.dust[dust].noLight = true;
			if (Main.rand.NextBool())
			{
				Main.dust[dust].noGravity = false;
				Main.dust[dust].scale *= 0.5f;
			}
		}
		Player closest = Main.player[Player.FindClosest(base.NPC.Top, 0, 0)];
		if (Math.Abs(closest.Center.X - base.NPC.Center.X) > 600f)
		{
			base.NPC.ai[1] = 90f;
		}
		if (base.NPC.ai[1] > 0f)
		{
			base.NPC.velocity.X *= 0.935f;
			base.NPC.ai[1]--;
			return;
		}
		if (base.NPC.velocity.Y == 0f && base.NPC.collideX)
		{
			base.NPC.velocity.Y = -13f;
		}
		else
		{
			base.NPC.velocity.Y += 0.15f;
		}
		base.NPC.spriteDirection = (closest.Center.X - base.NPC.Center.X < 0f).ToDirectionInt();
		if (Math.Abs(base.NPC.velocity.X) < 35f)
		{
			base.NPC.velocity.X += (float)base.NPC.spriteDirection * 0.2f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.ai[1] <= 0f)
		{
			if (base.NPC.ai[0]++ % 4f == 3f)
			{
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
			{
				base.NPC.frame.Y = frameHeight * 2;
			}
		}
		else
		{
			if (base.NPC.ai[0]++ % 6f == 5f)
			{
				base.NPC.frame.Y -= frameHeight;
			}
			if (base.NPC.frame.Y <= 0)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !spawnInfo.Player.Calamity().ZoneSulphur || !DownedBossSystem.downedAquaticScourge)
		{
			return 0f;
		}
		return 0.15f;
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
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("BabyFlakCrabGore").Type);
			}
		}
	}
}
