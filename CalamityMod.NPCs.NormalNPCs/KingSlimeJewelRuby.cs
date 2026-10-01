using System;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class KingSlimeJewelRuby : ModNPC
{
	public static readonly SoundStyle ShatterSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/CrownJewelShatter");

	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Custom/RedJewelFire");

	public static readonly SoundStyle ModeShiftSound = new SoundStyle("CalamityMod/Sounds/Custom/RedJewelModeShift");

	private const int BoltShootGateValue = 60;

	private const int BoltShootGateValue_Death = 60;

	private const float RubyLightTelegraphDuration = 45f;

	public static int JewelBoltDamage = 10;

	private const int EmeraldChargePhaseGateValue = 120;

	private const int EmeraldChargeGateValue = 60;

	private const int EmeraldChargeGateValue_Death = 40;

	private const float EmeraldLightTelegraphDuration = 30f;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Hide = true;
		NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(base.Type, bestiaryData);
		NPCID.Sets.TrailingMode[base.NPC.type] = 7;
		NPCID.Sets.TrailCacheLength[base.NPC.type] = 10;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 24;
		base.NPC.width = 32;
		base.NPC.height = 32;
		base.NPC.defense = 10;
		base.NPC.DR_NERD(0.1f);
		base.NPC.lifeMax = 120;
		base.NPC.knockBackResist = 0.8f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit5;
		base.NPC.DeathSound = SoundID.NPCDeath15;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void AI()
	{
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a19: Unknown result type (might be due to invalid IL or missing references)
		//IL_134a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1437: Unknown result type (might be due to invalid IL or missing references)
		//IL_143c: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1459: Unknown result type (might be due to invalid IL or missing references)
		//IL_1474: Unknown result type (might be due to invalid IL or missing references)
		//IL_147e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d39: Unknown result type (might be due to invalid IL or missing references)
		//IL_14df: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1506: Unknown result type (might be due to invalid IL or missing references)
		//IL_1522: Unknown result type (might be due to invalid IL or missing references)
		//IL_1544: Unknown result type (might be due to invalid IL or missing references)
		//IL_155d: Unknown result type (might be due to invalid IL or missing references)
		//IL_156b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1587: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1707: Unknown result type (might be due to invalid IL or missing references)
		//IL_1729: Unknown result type (might be due to invalid IL or missing references)
		//IL_1742: Unknown result type (might be due to invalid IL or missing references)
		//IL_1750: Unknown result type (might be due to invalid IL or missing references)
		//IL_176c: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (!CalamityPlayer.areThereAnyDamnBosses)
		{
			base.NPC.life = 0;
			OnKill();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		NPC kingSlime = null;
		NPC[] npc = Main.npc;
		foreach (NPC n in npc)
		{
			if (n.active && n.type == 50)
			{
				kingSlime = n;
				break;
			}
		}
		if (death && kingSlime != null && kingSlime.active)
		{
			base.NPC.dontTakeDamage = true;
		}
		else
		{
			base.NPC.dontTakeDamage = false;
		}
		float kingSlimeLifeRatio = ((kingSlime != null && kingSlime.active) ? ((float)kingSlime.life / (float)kingSlime.lifeMax) : 1f);
		bool num = death && kingSlimeLifeRatio < 0.55f && kingSlimeLifeRatio >= 0.35f;
		bool isAlternatingPhase = death && kingSlimeLifeRatio < 0.35f;
		if (num)
		{
			if (base.NPC.localAI[3] != 1f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.localAI[0] = 1f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				base.NPC.localAI[3] = 1f;
				SoundStyle style = ModeShiftSound with
				{
					Volume = 0.5f
				};
				SoundEngine.PlaySound(in style);
				base.NPC.netUpdate = true;
				for (int j = 0; j < 6; j++)
				{
					GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Red));
					GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Pink));
				}
			}
		}
		else if (isAlternatingPhase)
		{
			if (base.NPC.localAI[3] != 2f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				base.NPC.localAI[3] = 2f;
				SoundStyle style = ModeShiftSound with
				{
					Volume = 0.4f
				};
				SoundEngine.PlaySound(in style);
				base.NPC.netUpdate = true;
				for (int k = 0; k < 6; k++)
				{
					GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Red));
					GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Pink));
				}
			}
		}
		else if (base.NPC.localAI[3] != 0f)
		{
			base.NPC.ai[0] = 0f;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[1] = 0f;
			base.NPC.localAI[2] = 0f;
			base.NPC.localAI[3] = 0f;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.localAI[0] == 1f)
		{
			Lighting.AddLight(base.NPC.Center, 0f, 0.8f, 0f);
			if (base.NPC.ai[3] == 1f)
			{
				base.NPC.knockBackResist = 0f;
				if (base.NPC.ai[0] == 0f)
				{
					base.NPC.damage = 0;
					NPC nPC = base.NPC;
					nPC.velocity *= 0.925f;
					if (Main.rand.NextBool(3))
					{
						Vector2 dustVel2 = Vector2.UnitX.RotatedByRandom(100.0) * Main.rand.NextFloat(9.5f, 13f);
						Dust dust = Dust.NewDustPerfect(base.NPC.Center + dustVel2.SafeNormalize(Vector2.UnitX) * 150f, ModContent.DustType<SquashDust>(), -dustVel2 * 1.15f, 0, default(Color), Main.rand.NextFloat(0.9f, 1.2f));
						dust.noGravity = true;
						dust.fadeIn = 0.66f;
						dust.color = new Color(0, 200, 0);
					}
					base.NPC.ai[1]++;
					float anglularSpeed = base.NPC.ai[1] / 40f;
					anglularSpeed = 0.1f + anglularSpeed * 0.4f;
					base.NPC.rotation += anglularSpeed * (float)base.NPC.direction;
					if (!(base.NPC.ai[1] >= 40f))
					{
						return;
					}
					for (int dusty = 0; dusty < 10; dusty++)
					{
						Vector2 dustVel3 = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center + Main.player[base.NPC.target].velocity * 20f, -Vector2.UnitY) * Main.rand.NextFloat(-4f, -1f);
						int emerald = Dust.NewDust(base.NPC.Center, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
						Main.dust[emerald].velocity = dustVel3 * Main.rand.NextFloat(1f, 2f);
						Main.dust[emerald].noGravity = true;
						if (Main.rand.NextBool())
						{
							Main.dust[emerald].scale = 0.5f;
							Main.dust[emerald].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					SoundEngine.PlaySound(in SoundID.Item38, base.NPC.Center);
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
				else if (base.NPC.ai[0] == 1f)
				{
					base.NPC.damage = base.NPC.defDamage;
					float chargeSpeed = 28f;
					base.NPC.velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * chargeSpeed;
					base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.ForceNetUpdate();
				}
				else
				{
					if (base.NPC.ai[0] != 2f)
					{
						return;
					}
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.rotation += MathHelper.ToRadians((float)(Math.Sign(base.NPC.velocity.X) * 15));
					GeneralParticleHandler.SpawnParticle(new CustomSprite(base.NPC.Center - base.NPC.velocity, base.NPC.velocity * 0.8f, 10, "CalamityMod/NPCs/NormalNPCs/KingSlimeJewelEmerald", 1.2f, Color.DarkGreen.MultiplyRGBA(new Color(1f, 1f, 1f, 0f)))
					{
						Rotation = base.NPC.rotation
					});
					base.NPC.ai[1]++;
					if (!(base.NPC.ai[1] >= 40f))
					{
						return;
					}
					base.NPC.damage = 0;
					for (int l = 0; l < 10; l++)
					{
						Vector2 dustVel4 = Main.rand.NextVector2CircularEdge(5f, 5f);
						int emerald2 = Dust.NewDust(base.NPC.Center, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
						Main.dust[emerald2].velocity = dustVel4 * Main.rand.NextFloat(1f, 2f);
						Main.dust[emerald2].noGravity = true;
						if (Main.rand.NextBool())
						{
							Main.dust[emerald2].scale = 0.5f;
							Main.dust[emerald2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
					base.NPC.velocity = Vector2.Zero;
					if (!isAlternatingPhase)
					{
						return;
					}
					base.NPC.localAI[2]++;
					if (base.NPC.localAI[2] >= 2f)
					{
						base.NPC.localAI[0] = 0f;
						base.NPC.localAI[1] = 0f;
						base.NPC.localAI[2] = 0f;
						SoundStyle style = ModeShiftSound with
						{
							Volume = 0.5f
						};
						SoundEngine.PlaySound(in style);
						base.NPC.netUpdate = true;
						for (int m = 0; m < 6; m++)
						{
							GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Red));
							GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Pink));
						}
					}
				}
				return;
			}
			base.NPC.damage = 0;
			base.NPC.knockBackResist = 0.7f;
			base.NPC.rotation = base.NPC.velocity.X / 15f;
			float velocity = 5f;
			float acceleration = 0.2f;
			if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y - 200f)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= 0.98f;
				}
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > velocity)
				{
					base.NPC.velocity.Y = velocity;
				}
			}
			else if (base.NPC.position.Y < Main.player[base.NPC.target].position.Y - 300f)
			{
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y *= 0.98f;
				}
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f - velocity)
				{
					base.NPC.velocity.Y = 0f - velocity;
				}
			}
			if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X + 200f)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > 8f)
				{
					base.NPC.velocity.X = 8f;
				}
			}
			if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X - 200f)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < -8f)
				{
					base.NPC.velocity.X = -8f;
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= 120f)
			{
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 1f;
				base.NPC.netUpdate = true;
			}
			return;
		}
		Lighting.AddLight(base.NPC.Center, 0.8f, 0f, 0f);
		base.NPC.rotation = base.NPC.velocity.X / 15f;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
		}
		float velocity2 = 5f;
		float acceleration2 = 0.1f;
		if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y - 350f)
		{
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y *= 0.98f;
			}
			base.NPC.velocity.Y -= acceleration2;
			if (base.NPC.velocity.Y > velocity2)
			{
				base.NPC.velocity.Y = velocity2;
			}
		}
		else if (base.NPC.position.Y < Main.player[base.NPC.target].position.Y - 450f)
		{
			if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y *= 0.98f;
			}
			base.NPC.velocity.Y += acceleration2;
			if (base.NPC.velocity.Y < 0f - velocity2)
			{
				base.NPC.velocity.Y = 0f - velocity2;
			}
		}
		if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X + 100f)
		{
			if (base.NPC.velocity.X > 0f)
			{
				base.NPC.velocity.X *= 0.98f;
			}
			base.NPC.velocity.X -= acceleration2;
			if (base.NPC.velocity.X > 8f)
			{
				base.NPC.velocity.X = 8f;
			}
		}
		if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X - 100f)
		{
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.velocity.X *= 0.98f;
			}
			base.NPC.velocity.X += acceleration2;
			if (base.NPC.velocity.X < -8f)
			{
				base.NPC.velocity.X = -8f;
			}
		}
		base.NPC.ai[0]++;
		if (!(base.NPC.ai[0] >= (float)(death ? 60 : 60)))
		{
			return;
		}
		base.NPC.ai[0] = 0f;
		Vector2 npcPos = base.NPC.Center;
		float xDist = Main.player[base.NPC.target].Center.X - npcPos.X;
		float yDist = Main.player[base.NPC.target].Center.Y - npcPos.Y;
		Vector2 projVector = default(Vector2);
		((Vector2)(ref projVector))._002Ector(xDist, yDist);
		float projLength = ((Vector2)(ref projVector)).Length();
		float num2 = (death ? 12f : 10f);
		int type = ModContent.ProjectileType<JewelProjectile>();
		projLength = num2 / projLength;
		projVector.X *= projLength;
		projVector.Y *= projLength;
		for (int num3 = 0; num3 < 6; num3++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Red));
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Pink));
		}
		SoundEngine.PlaySound(in ShootSound, base.NPC.Center);
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), npcPos, projVector, type, JewelBoltDamage, 0f, Main.myPlayer);
		}
		base.NPC.netUpdate = true;
		if (!isAlternatingPhase)
		{
			return;
		}
		base.NPC.localAI[1]++;
		if (base.NPC.localAI[1] >= 4f)
		{
			base.NPC.localAI[0] = 1f;
			base.NPC.localAI[1] = 0f;
			base.NPC.localAI[2] = 0f;
			SoundStyle style = ModeShiftSound with
			{
				Volume = 0.5f
			};
			SoundEngine.PlaySound(in style);
			base.NPC.netUpdate = true;
			for (int num4 = 0; num4 < 6; num4++)
			{
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Red));
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Pink));
			}
		}
	}

	public override void OnKill()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Red));
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Pink));
		}
		float start = Main.rand.NextFloat((float)Math.PI * 2f);
		for (int j = 0; j < 3; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSprite(base.NPC.Center, Utils.RotatedByRandom(new Vector2(0f, -2f), start + MathHelper.ToRadians(20f)).RotatedBy(MathHelper.ToRadians((float)(j * 125))), 120, "CalamityMod/Particles/KingSlimeRubyShards", 1f, new Color(255, 255, 255), Main.rand.NextFloat(0.2f, 0.6f), AddativeBlend: true, needed: false, 3, j));
		}
		SoundEngine.PlaySound(in ShatterSound, base.NPC.Center);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		bool num = base.NPC.localAI[0] == 1f;
		Color col = (num ? Color.DarkOliveGreen : Color.Red);
		Color flashCol = (num ? Color.Lime : Color.Pink);
		float alph = 0f;
		float currentLightTelegraphDuration = (num ? 30f : 45f);
		float currentColorTelegraphGateValue = (float)(num ? 40 : 60) - currentLightTelegraphDuration;
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(num ? "CalamityMod/NPCs/NormalNPCs/KingSlimeJewelEmerald" : Texture, (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/NPCs/NormalNPCs/KingSlimeJewelFlash", (AssetRequestMode)2);
		if (num)
		{
			if (base.NPC.ai[0] == 0f && base.NPC.ai[1] > currentColorTelegraphGateValue)
			{
				alph = MathHelper.Lerp(0f, 1f, (base.NPC.ai[1] - currentColorTelegraphGateValue) / currentLightTelegraphDuration);
			}
			if (base.NPC.ai[0] == 2f && CalamityClientConfig.Instance.Afterimages)
			{
				for (int i = 1; i < base.NPC.oldPos.Length; i++)
				{
					Vector2 trailDrawPos = base.NPC.oldPos[i] + base.NPC.Size * 0.5f - screenPos;
					Color trailColor = Color.Lime * (1f - (float)i / (float)base.NPC.oldPos.Length) * 0.3f;
					spriteBatch.Draw(tex.Value, trailDrawPos, (Rectangle?)null, trailColor, base.NPC.rotation, tex.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0, 0f);
				}
			}
		}
		else if (base.NPC.ai[0] > currentColorTelegraphGateValue)
		{
			alph = MathHelper.Lerp(0f, 1f, (base.NPC.ai[0] - currentColorTelegraphGateValue) / currentLightTelegraphDuration);
		}
		Main.EntitySpriteDraw(tex.Value, base.NPC.Center - screenPos, tex.Frame(), Color.White, base.NPC.rotation, tex.Frame().Center(), 1f, (SpriteEffects)0);
		Main.EntitySpriteDraw(tex2.Value, base.NPC.Center - screenPos, tex2.Frame(), Color.Lerp(col, flashCol, alph).MultiplyRGBA(new Color(alph, alph, alph, 0f)), base.NPC.rotation, tex2.Frame().Center(), alph * 1.2f, (SpriteEffects)0);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.localAI[0] == 1f)
		{
			base.NPC.frameCounter = 2.0;
		}
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * balance);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1.5f), Color.Red));
		}
		if (base.NPC.localAI[0] != 1f)
		{
			return;
		}
		int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 89, hit.HitDirection, -1f);
		Main.dust[dust].noGravity = true;
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (base.NPC.height = 45);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int j = 0; j < 2; j++)
		{
			int emeraldDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
			Main.dust[emeraldDust].noGravity = true;
			Dust obj = Main.dust[emeraldDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[emeraldDust].scale = 0.5f;
				Main.dust[emeraldDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int k = 0; k < 10; k++)
		{
			int emeraldDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 3f);
			Main.dust[emeraldDust2].noGravity = true;
			Dust obj2 = Main.dust[emeraldDust2];
			obj2.velocity *= 5f;
			emeraldDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
			Main.dust[emeraldDust2].noGravity = true;
			Dust obj3 = Main.dust[emeraldDust2];
			obj3.velocity *= 2f;
		}
	}
}
