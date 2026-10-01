using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class Orthocera : ModNPC
{
	public ref float FallDelay => ref base.NPC.ai[0];

	public ref float Time => ref base.NPC.ai[1];

	public ref float HorizontalSpeed => ref base.NPC.ai[2];

	public bool PerformingJump
	{
		get
		{
			return base.NPC.ai[3] == 1f;
		}
		set
		{
			if (Main.dedServ && value != PerformingJump)
			{
				base.NPC.netUpdate = true;
			}
			base.NPC.ai[3] = value.ToInt();
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 62;
		base.NPC.height = 34;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.damage = 45;
		base.NPC.lifeMax = 280;
		base.NPC.defense = 15;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.damage = 120;
			base.NPC.lifeMax = 3850;
			base.NPC.defense = 35;
		}
		base.NPC.knockBackResist = 0.6f;
		base.NPC.value = Item.buyPrice(0, 0, 4);
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit41;
		base.NPC.DeathSound = SoundID.NPCDeath13;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<OrthoceraBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Orthocera")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.Calamity().newAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.Calamity().newAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.NPC.TargetClosest(faceTarget: false);
		float maxSpeed = (DownedBossSystem.downedPolterghast ? 12.8f : 10.5f);
		if (!Main.player.IndexInRange(base.NPC.target))
		{
			return;
		}
		Player player = Main.player[base.NPC.target];
		if (Time % 250f < 180f)
		{
			if (base.NPC.wet)
			{
				if (PerformingJump)
				{
					PerformingJump = false;
				}
				if (!base.NPC.WithinRange(player.Center, 150f))
				{
					base.NPC.velocity = (base.NPC.velocity * 17f + base.NPC.SafeDirectionTo(player.Center, -Vector2.UnitY) * maxSpeed) / 18f;
					if (FallDelay != 12f)
					{
						FallDelay = 12f;
						base.NPC.netUpdate = true;
					}
				}
				HorizontalSpeed = base.NPC.velocity.X;
				if (HorizontalSpeed == 0f)
				{
					HorizontalSpeed = 0.1f;
				}
				if (Math.Abs(HorizontalSpeed) < 7f)
				{
					HorizontalSpeed = Math.Abs(HorizontalSpeed) * 7f;
				}
				if (Math.Abs(HorizontalSpeed) > 16f)
				{
					HorizontalSpeed = Math.Abs(HorizontalSpeed) * 16f;
				}
				HorizontalSpeed = Math.Abs(HorizontalSpeed) * (float)(player.Center.X - base.NPC.Center.X > 0f).ToDirectionInt();
			}
			else if (FallDelay <= 0f)
			{
				base.NPC.velocity.Y += 0.2f;
			}
			else
			{
				FallDelay--;
			}
			base.NPC.direction = (base.NPC.spriteDirection = (base.NPC.velocity.X > 0f).ToDirectionInt());
		}
		else if (Time % 220f > 180f)
		{
			float verticalAcceleration = (DownedBossSystem.downedPolterghast ? 0.07f : 0.05f);
			if (Time % 220f < 200f)
			{
				base.NPC.velocity.Y -= verticalAcceleration;
			}
			else
			{
				base.NPC.velocity.Y += verticalAcceleration;
			}
			if (Time % 220f == 219f)
			{
				PerformingJump = true;
			}
			if (!base.NPC.wet)
			{
				base.NPC.velocity.X = HorizontalSpeed;
			}
		}
		if (Time % 220f > 180f && PerformingJump)
		{
			Time = 0f;
			base.NPC.netUpdate = true;
		}
		base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 4f + (float)Math.PI / 2f + (float)Math.PI;
		if (base.NPC.spriteDirection == -1)
		{
			base.NPC.rotation -= (float)Math.PI / 2f;
		}
		if (!base.NPC.wet)
		{
			base.NPC.velocity.X *= 0.92f;
			if (Main.netMode != 1 && Time % 220f == 195f)
			{
				float spitDirection = base.NPC.rotation - 5.4977875f;
				if (base.NPC.spriteDirection == -1)
				{
					spitDirection += (float)Math.PI / 2f;
				}
				int damage = ((!DownedBossSystem.downedPolterghast) ? ((!DownedBossSystem.downedAquaticScourge) ? (Main.masterMode ? 11 : (Main.expertMode ? 14 : 18)) : (Main.masterMode ? 17 : (Main.expertMode ? 21 : 26))) : (Main.masterMode ? 27 : (Main.expertMode ? 32 : 40)));
				if (DownedBossSystem.downedPolterghast)
				{
					for (int i = 0; i < 2; i++)
					{
						float offsetAngle = MathHelper.Lerp(-0.3f, 0.3f, (float)i / 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, (spitDirection + offsetAngle).ToRotationVector2() * 10f, ModContent.ProjectileType<OrthoceraStream>(), damage, 2f);
					}
				}
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, spitDirection.ToRotationVector2() * 12f, ModContent.ProjectileType<OrthoceraStream>(), damage, 2f);
			}
		}
		base.NPC.velocity = Vector2.Clamp(base.NPC.velocity, new Vector2(0f - maxSpeed), new Vector2(maxSpeed));
		if (!Main.zenithWorld || (!base.NPC.wet && base.NPC.collideY))
		{
			return;
		}
		base.NPC.Calamity().newAI[0]++;
		if (base.NPC.Calamity().newAI[0] % 5f == 0f && Main.netMode != 1)
		{
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Bottom, Main.rand.NextVector2Circular(4f, 8f), ModContent.ProjectileType<RancorFog>(), 0, 0f, Main.myPlayer);
			RancorLavaMetaball.SpawnParticle(base.NPC.Bottom + Main.rand.NextVector2Circular(10f, 10f), 135f);
		}
		if (base.NPC.Calamity().newAI[0] % 30f == 0f && Main.netMode != 1)
		{
			int p = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Bottom, Vector2.Zero, ModContent.ProjectileType<RancorArm>(), 20, 0f, Main.myPlayer, 0f, -1f);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].DamageType = DamageClass.Default;
				Main.projectile[p].friendly = false;
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<OrthoceraShell>(), 20);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(() => DownedBossSystem.downedPolterghast);
		mainRule.Add(ModContent.ItemType<CorrodedFossil>(), 15, 1, 3, !DownedBossSystem.downedPolterghast);
		mainRule.AddFail(ModContent.ItemType<CorrodedFossil>(), 3, 1, 3, DownedBossSystem.downedPolterghast);
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 6.0)
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
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("OrthoceraGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("OrthoceraGore2").Type, base.NPC.scale);
			}
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		}
	}
}
