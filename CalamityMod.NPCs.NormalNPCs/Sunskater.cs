using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.NPCs.ExoMechs.Ares;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class Sunskater : ModNPC
{
	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/Sunskater")
	{
		Volume = 0.9f
	};

	private bool hasBeenHit;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.lavaImmune = true;
		base.NPC.damage = 20;
		base.NPC.width = 58;
		base.NPC.height = 22;
		base.NPC.defense = 4;
		base.NPC.lifeMax = 160;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 3);
		base.NPC.HitSound = SoundID.NPCHit50;
		base.NPC.DeathSound = (Main.zenithWorld ? AresGaussNuke.NukeExplosionSound : DeathSound);
		base.NPC.knockBackResist = 0.7f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SunskaterBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Sunskater")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		base.NPC.noGravity = true;
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.justHit)
		{
			hasBeenHit = true;
		}
		base.NPC.chaseable = hasBeenHit;
		if (!base.NPC.wet)
		{
			bool canAttack = hasBeenHit;
			base.NPC.TargetClosest(faceTarget: false);
			if ((Main.player[base.NPC.target].wet || Main.player[base.NPC.target].dead) & canAttack)
			{
				canAttack = false;
			}
			if (!canAttack)
			{
				if (base.NPC.collideX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * -1f;
					base.NPC.direction *= -1;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.collideY)
				{
					base.NPC.netUpdate = true;
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
						base.NPC.directionY = -1;
						base.NPC.ai[0] = -1f;
					}
					else if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
						base.NPC.directionY = 1;
						base.NPC.ai[0] = 1f;
					}
				}
			}
			if (canAttack)
			{
				base.NPC.TargetClosest();
				base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.15f;
				base.NPC.velocity.Y = base.NPC.velocity.Y + (float)base.NPC.directionY * 0.15f;
				base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X, -10f, 10f);
				base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y, -10f, 10f);
			}
			else
			{
				base.NPC.velocity.X += (float)base.NPC.direction * 0.1f;
				if (base.NPC.velocity.X < -2f || base.NPC.velocity.X > 2f)
				{
					base.NPC.velocity.X *= 0.95f;
				}
				if (base.NPC.ai[0] == -1f)
				{
					base.NPC.velocity.Y -= 0.01f;
					if (base.NPC.velocity.Y < -0.3f)
					{
						base.NPC.ai[0] = 1f;
					}
				}
				else
				{
					base.NPC.velocity.Y += 0.01f;
					if (base.NPC.velocity.Y > 0.3f)
					{
						base.NPC.ai[0] = -1f;
					}
				}
			}
			int npcTileX = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
			int npcTileY = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
			if (Main.tile[npcTileX, npcTileY - 1].LiquidAmount < 128)
			{
				if (Main.tile[npcTileX, npcTileY + 1].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
				else if (Main.tile[npcTileX, npcTileY + 2].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
			}
			if (base.NPC.velocity.Y > 0.4f || base.NPC.velocity.Y < -0.4f)
			{
				base.NPC.velocity.Y *= 0.95f;
			}
		}
		else
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X *= 0.94f;
				if (base.NPC.velocity.X > -0.2f && base.NPC.velocity.X < 0.2f)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.3f;
			if (base.NPC.velocity.Y > 5f)
			{
				base.NPC.velocity.Y = 5f;
			}
			base.NPC.ai[0] = 1f;
		}
		base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f;
		if ((double)base.NPC.rotation < -0.1)
		{
			base.NPC.rotation = -0.1f;
		}
		if ((double)base.NPC.rotation > 0.1)
		{
			base.NPC.rotation = 0.1f;
		}
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (hasBeenHit ? 0.15f : 0.075f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe)
		{
			return 0f;
		}
		return SpawnCondition.Sky.Chance * 0.15f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage <= 0)
		{
			return;
		}
		if (Main.zenithWorld)
		{
			target.AddBuff(ModContent.BuffType<HolyInferno>(), 150);
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(24, 180);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(323, 120);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(39, 120);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(44, 180);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(324, 120);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(67, 60);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(ModContent.BuffType<Shadowflame>(), 120);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(ModContent.BuffType<Daybroken>(), 120);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(ModContent.BuffType<HolyFlames>(), 120);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 90);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(ModContent.BuffType<Dragonfire>(), 90);
			}
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 90);
			}
		}
		else
		{
			target.AddBuff(24, 180);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.AddIf(() => Main.hardMode, ModContent.ItemType<EssenceofSunlight>(), 2);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 64, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 64, hit.HitDirection, -1f);
			}
			if (Main.zenithWorld)
			{
				float screenShakePower = 16f * Utils.GetLerpValue(1300f, 0f, base.NPC.Distance(Main.LocalPlayer.Center), clamped: true);
				Main.LocalPlayer.SetScreenshake(screenShakePower);
			}
		}
	}
}
