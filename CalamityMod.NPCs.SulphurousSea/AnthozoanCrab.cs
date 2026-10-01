using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SulphurousSea;

public class AnthozoanCrab : ModNPC
{
	public int boulderIndex;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 16;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 8f;
		value.PortraitPositionYOverride = 28f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 0;
		base.NPC.width = 56;
		base.NPC.height = 42;
		base.NPC.defense = 22;
		base.NPC.lifeMax = 900;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.NPC.HitSound = SoundID.NPCHit38;
		base.NPC.DeathSound = SoundID.NPCDeath46;
		base.NPC.knockBackResist = 0.04f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AnthozoanCrabBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AnthozoanCrab")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(boulderIndex);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		boulderIndex = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[1]++ % 360f < 280f)
		{
			if (base.NPC.ai[2] > 1f)
			{
				base.NPC.ai[2]--;
			}
			base.NPC.aiAction = 0;
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[0] = -90f;
				base.NPC.ai[2] = 1f;
				base.NPC.TargetClosest();
			}
			base.NPC.TargetClosest(faceTarget: false);
			Player player = Main.player[base.NPC.target];
			if (base.NPC.velocity.Y == 0f && !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
			{
				if (base.NPC.collideY && base.NPC.oldVelocity.Y != 0f && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.position.X -= base.NPC.velocity.X + (float)base.NPC.direction;
				}
				if (base.NPC.ai[3] == base.NPC.position.X)
				{
					base.NPC.direction *= -1;
					base.NPC.ai[2] = 200f;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.ai[3] = 0f;
				base.NPC.velocity.X *= 0.8f;
				if (Math.Abs(base.NPC.velocity.X) < 0.1f)
				{
					base.NPC.velocity.X = 0f;
				}
				base.NPC.ai[0] += 5f;
				int state = 0;
				if (base.NPC.ai[0] >= 0f)
				{
					state = 1;
				}
				if (base.NPC.ai[0] >= -1000f && base.NPC.ai[0] <= -500f)
				{
					state = 2;
				}
				if (base.NPC.ai[0] >= -2000f && base.NPC.ai[0] <= -1500f)
				{
					state = 3;
				}
				if (state > 0)
				{
					base.NPC.netUpdate = true;
					if (state == 3)
					{
						base.NPC.velocity.Y -= 9f;
						base.NPC.velocity.X += 8f * (float)base.NPC.direction;
						base.NPC.ai[0] = -120f;
						base.NPC.ai[3] = base.NPC.position.X;
					}
					else
					{
						base.NPC.velocity.Y -= 8f;
						base.NPC.velocity.X += 11f * (float)base.NPC.direction;
						base.NPC.ai[0] = -80f;
						if (state == 1)
						{
							base.NPC.ai[0] -= 1000f;
						}
						else
						{
							base.NPC.ai[0] -= 2000f;
						}
					}
				}
				else if (base.NPC.ai[0] >= -30f)
				{
					base.NPC.aiAction = 1;
					return;
				}
			}
			else if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
			{
				base.NPC.direction = (base.NPC.spriteDirection = (base.NPC.SafeDirectionTo(player.Center).X < 0f).ToDirectionInt());
				if (Math.Abs(base.NPC.velocity.X) < 14f && Math.Abs(player.Center.X - base.NPC.Center.X) > 65f)
				{
					base.NPC.velocity.X += (float)base.NPC.spriteDirection * -0.08f;
				}
			}
		}
		else
		{
			base.NPC.velocity.X *= 0.9f;
			if (base.NPC.ai[1] % 360f == 300f)
			{
				base.NPC.velocity.X = 0f;
				Vector2 rockSpawnPosition = default(Vector2);
				((Vector2)(ref rockSpawnPosition))._002Ector(16f * (float)(-base.NPC.spriteDirection) + base.NPC.Center.X, base.NPC.Bottom.Y - 6f);
				int damage = (Main.masterMode ? 18 : (Main.expertMode ? 21 : 29));
				if (Main.netMode != 1)
				{
					boulderIndex = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), rockSpawnPosition, Vector2.Zero, ModContent.ProjectileType<CrabBoulder>(), damage, 6f);
					base.NPC.netUpdate = true;
				}
			}
			if (base.NPC.ai[1] % 360f == 330f)
			{
				Main.projectile[boulderIndex].velocity = Utils.RotatedBy(new Vector2(0f, -11f), (double)((float)(-base.NPC.spriteDirection) * 0.8f), default(Vector2));
				boulderIndex = -1;
				base.NPC.netUpdate = true;
			}
		}
		base.NPC.velocity.Y += 0.25f;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.ai[1] % 360f < 280f)
		{
			if (base.NPC.frameCounter % 6.0 == 5.0)
			{
				base.NPC.frame.Y += frameHeight;
				if (base.NPC.frame.Y >= 4 * frameHeight)
				{
					base.NPC.frame.Y = frameHeight;
				}
			}
		}
		else
		{
			base.NPC.frame.Y = 3 * frameHeight + frameHeight * (int)MathHelper.Clamp(base.NPC.ai[1] % 280f / 60f * 9f, 0f, 9f);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !spawnInfo.Player.Calamity().ZoneSulphur || !DownedBossSystem.downedAquaticScourge)
		{
			return 0f;
		}
		return 0.135f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<CorrodedFossil>(), 15);
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
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AnthozoanCrabGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AnthozoanCrabGore2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AnthozoanCrabGore3").Type, base.NPC.scale);
			}
		}
	}
}
