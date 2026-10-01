using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SulphurousSea;

public class BelchingCoral : ModNPC
{
	public static readonly SoundStyle SAXOPHONE = new SoundStyle("CalamityMod/Sounds/Item/Saxophone/Sax", 6);

	public const float CheckDistance = 480f;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y += 4f;
		value.PortraitPositionYOverride = 24f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 0;
		base.NPC.width = 54;
		base.NPC.height = 42;
		base.NPC.defense = 25;
		base.NPC.lifeMax = 1000;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.NPC.HitSound = SoundID.NPCHit42;
		base.NPC.DeathSound = SoundID.NPCDeath5;
		base.NPC.knockBackResist = 0f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BelchingCoralBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BelchingCoral")
		});
	}

	public override void AI()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.velocity.Y += 0.25f;
		base.NPC.TargetClosest(faceTarget: false);
		Player player = Main.player[base.NPC.target];
		if (Math.Abs(player.Center.X - base.NPC.Center.X) < 480f && player.Bottom.Y < base.NPC.Top.Y && base.NPC.ai[0]++ % 35f == 34f && Main.netMode != 1)
		{
			int damage = (Main.masterMode ? 17 : (Main.expertMode ? 20 : 27));
			Vector2 velocity = default(Vector2);
			((Vector2)(ref velocity))._002Ector(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-11f, -6f));
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Top + new Vector2(0f, 6f), velocity, ModContent.ProjectileType<BelchingCoralSpike>(), damage, 3f);
		}
		if (Main.zenithWorld)
		{
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] > 27f)
			{
				SoundEngine.PlaySound(in SAXOPHONE, base.NPC.Center);
				base.NPC.ai[1] = 0f;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !spawnInfo.Player.Calamity().ZoneSulphur || !DownedBossSystem.downedAquaticScourge)
		{
			return 0f;
		}
		return 0.085f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<CorrodedFossil>(), 15);
		npcLoot.Add(ModContent.ItemType<BelchingSaxophone>(), 10);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("BelchingCoralGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("BelchingCoralGore2").Type, base.NPC.scale);
			}
		}
	}
}
