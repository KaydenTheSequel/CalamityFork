using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class NuclearToad : ModNPC
{
	public const float ExplosionTelegraphTime = 120f;

	public static Asset<Texture2D> GlowTexture;

	public Player Target => Main.player[base.NPC.target];

	public static float ExplosionStartRadius
	{
		get
		{
			float explodeDistance = (DownedBossSystem.downedAquaticScourge ? 360f : 270f);
			if (DownedBossSystem.downedPolterghast)
			{
				explodeDistance = 560f;
			}
			return explodeDistance;
		}
	}

	public ref float ExplosionTimer => ref base.NPC.ai[0];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y += 8f;
		value.PortraitPositionYOverride = 28f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.width = 62;
		base.NPC.height = 34;
		base.NPC.defense = 4;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.damage = 15;
		base.NPC.lifeMax = 60;
		base.NPC.defense = 3;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.damage = 80;
			base.NPC.lifeMax = 2750;
			base.NPC.defense = 15;
		}
		else if (DownedBossSystem.downedAquaticScourge)
		{
			base.NPC.damage = 35;
			base.NPC.lifeMax = 200;
		}
		base.NPC.knockBackResist = 0.7f;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<NuclearToadBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.NuclearToad")
		});
	}

	public override void AI()
	{
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		base.NPC.velocity.X *= 0.96f;
		if (base.NPC.wet)
		{
			if (base.NPC.velocity.Y > 2f)
			{
				base.NPC.velocity.Y *= 0.9f;
			}
			base.NPC.velocity.Y -= 0.16f;
			if (base.NPC.velocity.Y < -4f)
			{
				base.NPC.velocity.Y = -4f;
			}
		}
		else
		{
			if (base.NPC.velocity.Y < -2f)
			{
				base.NPC.velocity.Y *= 0.9f;
			}
			base.NPC.velocity.Y += 0.16f;
			if (base.NPC.velocity.Y > 3f)
			{
				base.NPC.velocity.Y = 3f;
			}
		}
		if (Main.rand.NextBool(480))
		{
			SoundEngine.PlaySound(in SoundID.Zombie13, base.NPC.Center);
		}
		if (base.NPC.WithinRange(Target.Center, ExplosionStartRadius) || ExplosionTimer >= 1f)
		{
			ExplosionTimer++;
		}
		if (!(ExplosionTimer >= 120f))
		{
			return;
		}
		if (Main.netMode != 1)
		{
			int damage = ((!Main.masterMode) ? ((!Main.expertMode) ? (DownedBossSystem.downedAquaticScourge ? 27 : 10) : (DownedBossSystem.downedAquaticScourge ? 21 : 8)) : (DownedBossSystem.downedAquaticScourge ? 17 : 7));
			float speed = Main.rand.NextFloat(8f, 12f);
			if (DownedBossSystem.downedPolterghast)
			{
				speed *= 1.8f;
				damage = (Main.masterMode ? 30 : (Main.expertMode ? 36 : 45));
			}
			for (int i = 0; i < 7; i++)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, -Vector2.UnitY.RotatedByRandom(0.7900000214576721) * speed * Main.rand.NextFloat(0.8f, 1f), ModContent.ProjectileType<NuclearToadGoo>(), damage, 1f);
			}
		}
		SoundEngine.PlaySound(in SoundID.DD2_KoboldExplosion, base.NPC.Center);
		base.NPC.life = 0;
		base.NPC.HitEffect();
		base.NPC.active = false;
		base.NPC.netUpdate = true;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		float explosionInterpolant = ExplosionTimer / 120f;
		float fadeToRed = (float)Math.Abs(Math.Sin(3.1415927410125732 * Math.Pow(explosionInterpolant, 3.0) * 6.0));
		return Color.Lerp(drawColor, new Color(232, 40, 12, 0), fadeToRed * 0.75f) * base.NPC.Opacity;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 4.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= Main.npcFrameCount[base.Type] * frameHeight)
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
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 8; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("NuclearToadGore1").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("NuclearToadGore2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("NuclearToadGore3").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("NuclearToadGore4").Type, base.NPC.scale);
			}
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, Main.rand.NextFloat(-2f, 2f), -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<SulphuricScale>(), 2, 1, 3);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(() => DownedBossSystem.downedAquaticScourge);
		mainRule.Add(ModContent.ItemType<CausticCroakerStaff>(), 100, 1, 1, !DownedBossSystem.downedAquaticScourge);
		mainRule.AddFail(ModContent.ItemType<CausticCroakerStaff>(), 20, 1, 1, DownedBossSystem.downedAquaticScourge);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.DrawGlowmask(base.NPC, spriteBatch, GlowTexture.Value, invertedDirection: true);
	}
}
