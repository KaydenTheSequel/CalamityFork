using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SlimeGod;

public class CorruptSlimeSpawn : ModNPC
{
	public static int ShaderainDamage = 12;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.PortraitPositionYOverride = -32f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 14;
		base.NPC.damage = 28;
		base.NPC.width = 40;
		base.NPC.height = 30;
		base.NPC.defense = 6;
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 10000 : 180);
		base.NPC.knockBackResist = 0.7f;
		base.AnimationType = 121;
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
		int associatedNPCType = ModContent.NPCType<SplitEbonianPaladin>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CorruptSlimeSpawn")
		});
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1 && base.NPC.life <= 0)
		{
			Vector2 spawnAt = base.NPC.Center + new Vector2(0f, (float)base.NPC.height / 2f);
			NPC.NewNPC(base.NPC.GetSource_Loot(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<CorruptSlimeSpawn2>());
		}
		Color dustColor = Color.Lavender;
		((Color)(ref dustColor)).A = 150;
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, dustColor);
		}
	}

	public override void OnKill()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld && Main.netMode != 1)
		{
			int type = ModContent.ProjectileType<ShadeNimbusHostile>();
			Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, Vector2.Zero, type, ShaderainDamage, 0f, Main.myPlayer);
		}
	}
}
