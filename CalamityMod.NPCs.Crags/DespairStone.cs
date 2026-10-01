using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Particles;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crags;

public class DespairStone : ModNPC
{
	public SlotId ChainsawSoundSlot;

	public static readonly SoundStyle ChainsawStartSound = new SoundStyle("CalamityMod/Sounds/Custom/ChainsawStart")
	{
		Volume = 0.15f
	};

	public static readonly SoundStyle ChainsawEndSound = new SoundStyle("CalamityMod/Sounds/Custom/ChainsawEnd")
	{
		Volume = 0.15f
	};

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 40;
		base.NPC.width = 66;
		base.NPC.height = 64;
		base.NPC.defense = 38;
		base.NPC.lifeMax = 180;
		base.NPC.knockBackResist = 0.1f;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.HitSound = SoundID.NPCHit41;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.behindTiles = true;
		base.NPC.lavaImmune = true;
		if (DownedBossSystem.downedProvidence)
		{
			base.NPC.damage = 80;
			base.NPC.defense = 50;
			base.NPC.lifeMax = 3000;
		}
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<DespairStoneBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.DespairStone")
		});
	}

	public override void AI()
	{
		float buzzsawStartTime = 480f;
		base.NPC.ai[1]++;
		if (base.NPC.ai[2] != 0f)
		{
			BuzzsawMode();
		}
		if (base.NPC.ai[1] > buzzsawStartTime)
		{
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[1] = buzzsawStartTime + 1f;
				base.NPC.rotation += base.NPC.velocity.X * 0.01f;
				base.NPC.spriteDirection = -base.NPC.direction;
				if (base.NPC.velocity.Y == 0f || base.NPC.lavaWet)
				{
					BuzzsawMode();
				}
			}
		}
		else
		{
			base.NPC.ai[2] = 0f;
			CalamityRegularEnemyAI.UnicornAI(base.NPC, base.Mod, spin: true, CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 6f : 4f), 5f, 0.2f);
		}
		if (base.NPC.lavaWet)
		{
			base.NPC.velocity.Y += -0.8f;
		}
	}

	public void BuzzsawMode()
	{
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		float speedCap = 10f;
		if (base.NPC.ai[2] == 0f)
		{
			ChainsawSoundSlot = SoundEngine.PlaySound(in ChainsawStartSound, base.NPC.Center);
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.ai[2] = -1f;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.ai[2] = 1f;
			}
			else
			{
				float distance = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
				if (distance != 0f)
				{
					base.NPC.ai[2] = distance / Math.Abs(distance);
				}
				else
				{
					base.NPC.ai[2] = 1f;
				}
			}
		}
		if (SoundEngine.TryGetActiveSound(ChainsawSoundSlot, out ActiveSound chainsawSound) && chainsawSound.IsPlaying)
		{
			chainsawSound.Position = base.NPC.Center;
			chainsawSound.Update();
		}
		if (base.NPC.velocity.X == 0f)
		{
			if (base.NPC.velocity.Y > 0f - speedCap)
			{
				base.NPC.velocity.Y += -1.66f;
			}
			if (base.NPC.velocity.Y < 0f - speedCap)
			{
				base.NPC.velocity.Y = 0f - speedCap;
			}
		}
		if (base.NPC.velocity.X == 0f || base.NPC.velocity.Y == 0f)
		{
			SpawnSparks();
		}
		else if (Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y < 0f && base.NPC.velocity.Y < 0f && !base.NPC.lavaWet)
		{
			base.NPC.velocity.Y += -0.03f;
		}
		if (Math.Abs(base.NPC.velocity.X) < speedCap)
		{
			base.NPC.velocity.X += 1.66f * base.NPC.ai[2];
		}
		if (Math.Abs(base.NPC.velocity.X) > speedCap)
		{
			base.NPC.velocity.X = speedCap * base.NPC.ai[2];
		}
		base.NPC.rotation += speedCap * 0.03f * base.NPC.ai[2];
		base.NPC.spriteDirection = -base.NPC.direction;
		if (base.NPC.ai[1] > (float)Main.rand.Next(670, 740))
		{
			base.NPC.ai[1] = 0f;
			base.NPC.velocity.Y += -3f;
			chainsawSound?.Stop();
			SoundEngine.PlaySound(in ChainsawEndSound, base.NPC.Center);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.Calamity().ZoneCalamity)
		{
			return 0f;
		}
		return 0.25f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<BrimstoneSlag>(), 5, 10, 30);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.Hardmode());
		LeadingConditionRule postProv = npcLoot.DefineConditionalDropSet(DropHelper.PostProv());
		mainRule.Add(ModContent.ItemType<EssenceofHavoc>(), 2);
		postProv.Add(ModContent.ItemType<Bloodstone>(), 4);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DespairStone").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DespairStone2").Type, base.NPC.scale);
			}
		}
	}

	public override bool PreKill()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(ChainsawSoundSlot, out ActiveSound chainsawSound) && chainsawSound.IsPlaying)
		{
			chainsawSound?.Stop();
		}
		return true;
	}

	public void SpawnSparks()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		Vector2 particleSpawnDisplacement = default(Vector2);
		Vector2 splatterDirection = default(Vector2);
		if (base.NPC.velocity.X == 0f)
		{
			((Vector2)(ref particleSpawnDisplacement))._002Ector(24f * base.NPC.ai[2], 20f);
			((Vector2)(ref splatterDirection))._002Ector(0f, 1f);
		}
		else
		{
			((Vector2)(ref particleSpawnDisplacement))._002Ector(20f * (0f - base.NPC.ai[2]), 24f);
			((Vector2)(ref splatterDirection))._002Ector(0f - base.NPC.ai[2], 0f);
		}
		Vector2 bloodSpawnPosition = base.NPC.Center + particleSpawnDisplacement;
		if (base.NPC.ai[1] % 4f != 0f)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			int sparkLifetime = Main.rand.Next(14, 21);
			float sparkScale = Main.rand.NextFloat(0.8f, 1f) + 0.05f;
			Color sparkColor = Color.Lerp(Color.DarkGray, Color.DarkRed, Main.rand.NextFloat(0.7f));
			sparkColor = Color.Lerp(sparkColor, Color.OrangeRed, Main.rand.NextFloat());
			if (Main.rand.NextBool(10))
			{
				sparkScale *= 1.4f;
			}
			Vector2 sparkVelocity = splatterDirection.RotatedByRandom(0.44999998807907104) * Main.rand.NextFloat(6f, 13f);
			sparkVelocity.Y -= 6f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(bloodSpawnPosition, sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
		}
	}
}
