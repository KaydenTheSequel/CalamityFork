using System;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SlimeGod;

public class CrimsonSlimeSpawn : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 1;
		base.NPC.damage = 20;
		base.NPC.width = 40;
		base.NPC.height = 30;
		base.NPC.defense = 4;
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 10000 : 110);
		base.NPC.knockBackResist = 0.8f;
		base.AnimationType = 81;
		base.NPC.Opacity = 0.8f;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<SplitCrimulanPaladin>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CrimsonSlimeSpawn")
		});
	}

	public override void AI()
	{
		base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		Color dustColor = Color.Crimson;
		((Color)(ref dustColor)).A = 150;
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, dustColor);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, dustColor);
			}
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(8) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
		if (Main.zenithWorld && Main.rand.NextBool(5) && Main.netMode != 1)
		{
			Vector2 valueBoom = default(Vector2);
			((Vector2)(ref valueBoom))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			float spreadBoom = 0.261f;
			double startAngleBoom = Math.Atan2(base.NPC.velocity.X, base.NPC.velocity.Y) - (double)(spreadBoom / 2f);
			double deltaAngleBoom = spreadBoom / 8f;
			int damageBoom = 30;
			for (int iBoom = 0; iBoom < 5; iBoom++)
			{
				int projectileType = ModContent.ProjectileType<UnstableCrimulanGlob>();
				double offsetAngleBoom = startAngleBoom + deltaAngleBoom * (double)(iBoom + iBoom * iBoom) / 2.0 + (double)(32f * (float)iBoom);
				float velocityfactor = 0.3f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), valueBoom.X, valueBoom.Y, (float)(Math.Sin(offsetAngleBoom) * (double)velocityfactor), (float)(Math.Cos(offsetAngleBoom) * (double)velocityfactor), projectileType, damageBoom, 0f, Main.myPlayer);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), valueBoom.X, valueBoom.Y, (float)((0.0 - Math.Sin(offsetAngleBoom)) * (double)velocityfactor), (float)((0.0 - Math.Cos(offsetAngleBoom)) * (double)velocityfactor), projectileType, damageBoom, 0f, Main.myPlayer);
			}
		}
	}
}
