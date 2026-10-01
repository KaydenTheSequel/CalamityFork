using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.DataStructures;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class TelekineticEnemyGrab : ModProjectile, ILocalizedModType, IModType
{
	private BezierCurve curve;

	private int throwSign;

	private Vector2 throwPos;

	private Vector2 holdPos;

	private int MyTarget;

	private static Dictionary<int, Texture2D> EnemyGlowTextures = new Dictionary<int, Texture2D>();

	private bool evenRed;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Particles/BloomRing";

	private ref float Time => ref base.Projectile.ai[0];

	private ref float StunTime => ref base.Projectile.ai[1];

	private int enemyID
	{
		get
		{
			return (int)base.Projectile.ai[2];
		}
		set
		{
			base.Projectile.ai[2] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 1f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 1200;
		base.Projectile.damage = 10;
		base.Projectile.hostile = true;
		if (NPC.crimsonBoss != -1)
		{
			MyTarget = Main.npc[NPC.crimsonBoss].target;
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		holdPos = new Vector2(base.Projectile.Center.X, Main.npc[NPC.crimsonBoss].Center.Y - 128f) - Main.npc[NPC.crimsonBoss].Center;
		base.Projectile.rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
		StunTime = -1f;
		enemyID = Main.rand.Next(0, 3);
		int[] enemyIDs = new int[3] { 181, 173, 239 };
		enemyID = enemyIDs[enemyID];
		MyTarget = Main.npc[NPC.crimsonBoss].target;
		base.Projectile.netUpdate = true;
	}

	public override void AI()
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		if (NPC.crimsonBoss == -1)
		{
			base.Projectile.active = false;
			return;
		}
		if (Time <= 90f)
		{
			base.Projectile.hostile = false;
		}
		else
		{
			base.Projectile.hostile = true;
		}
		bool throwing = Time > 180f;
		float throwTime = Time - 180f;
		if (throwing && throwSign == 0)
		{
			throwSign = Math.Sign(base.Projectile.Center.X - Main.npc[NPC.crimsonBoss].Center.X) * -Math.Sign(Main.player[MyTarget].Center.Y - Main.npc[NPC.crimsonBoss].Center.Y);
		}
		if (!(throwTime > 90f))
		{
			Vector2 startPoint = Main.npc[NPC.crimsonBoss].Center;
			Vector2 endPoint = base.Projectile.Center;
			if (StunTime == -1f)
			{
				if (!throwing)
				{
					if (Time >= 150f)
					{
						base.Projectile.velocity = (holdPos + Main.npc[NPC.crimsonBoss].Center - base.Projectile.Center) / 30f;
					}
					else if (Time != 0f)
					{
						if (Time <= 90f)
						{
							if (Time == 90f)
							{
								base.Projectile.velocity = Vector2.UnitY * -24f;
							}
							if (Time % 30f == 0f)
							{
								base.Projectile.velocity = Vector2.UnitY * -16f;
							}
							else
							{
								Projectile projectile = base.Projectile;
								projectile.velocity *= 0.33f;
							}
						}
						else
						{
							Projectile projectile2 = base.Projectile;
							projectile2.velocity *= 0.966f;
						}
					}
				}
				else
				{
					if (throwTime <= 30f)
					{
						throwPos = base.Projectile.Center + base.Projectile.velocity - (Main.npc[NPC.crimsonBoss].Center + Main.npc[NPC.crimsonBoss].velocity);
					}
					Vector2 target = Main.player[MyTarget].Center;
					Vector2 throwDir = (target - (throwPos + (Main.npc[NPC.crimsonBoss].Center + Main.npc[NPC.crimsonBoss].velocity))).SafeNormalize(Vector2.UnitY);
					if (throwTime >= 30f && throwTime <= 90f)
					{
						if (throwTime <= 60f)
						{
							base.Projectile.Center = Vector2.Lerp(throwPos, throwPos - throwDir * 56f, CalamityUtils.SineInOutEasing((throwTime - 30f) / 30f, 1)) + (Main.npc[NPC.crimsonBoss].Center + Main.npc[NPC.crimsonBoss].velocity);
							base.Projectile.velocity = Vector2.Zero;
						}
						else
						{
							Projectile projectile3 = base.Projectile;
							projectile3.velocity += throwDir * 0.9f;
							if (throwTime == 90f)
							{
								float dist = base.Projectile.Center.Distance(target) / 2f;
								dist /= ((Vector2)(ref base.Projectile.velocity)).Length();
								base.Projectile.velocity.Y -= dist * 0.6f;
								base.Projectile.tileCollide = true;
							}
						}
					}
				}
			}
			Vector2 direction = endPoint - startPoint;
			float num = Vector2.Distance(startPoint, endPoint);
			float lerp = CalamityUtils.SineInOutEasing(MathHelper.Clamp(throwTime / 60f, 0f, 1f), 1);
			float xMult = MathHelper.Lerp(0f - Math.Clamp(direction.X / 256f, -1f, 1f), (float)throwSign, lerp);
			float yMult = Math.Clamp(direction.Y / 256f, -1f, 1f);
			float curveIntensity = Math.Clamp(num / 420f, 0f, 0.66f) * xMult * yMult;
			Vector2 perpindicular = direction.RotatedBy(1.5707963705062866);
			Vector2 controlPoint1 = startPoint + direction * 0.25f + perpindicular * curveIntensity;
			Vector2 controlPoint2 = startPoint + direction * 0.75f + perpindicular * curveIntensity;
			curve = new BezierCurve(startPoint, controlPoint1, controlPoint2, endPoint);
		}
		else
		{
			base.Projectile.velocity.Y += 0.6f;
		}
		if (StunTime >= 0f)
		{
			if (StunTime < 30f)
			{
				StunTime++;
				return;
			}
			base.Projectile.velocity.Y += 0.6f;
			base.Projectile.tileCollide = true;
		}
		else
		{
			if (Time > 90f)
			{
				base.Projectile.rotation += base.Projectile.velocity.X / 100f - (float)Math.Sign(Main.npc[NPC.crimsonBoss].Center.X - base.Projectile.Center.X) * 0.025f;
			}
			Time++;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(throwSign);
		writer.WriteVector2(throwPos);
		writer.WriteVector2(holdPos);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		throwSign = reader.ReadInt32();
		throwPos = reader.ReadVector2();
		holdPos = reader.ReadVector2();
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		Vector2 velocity = base.Projectile.velocity.RotatedBy(3.1415927410125732) / 8f;
		Vector2 pos = base.Projectile.Center + base.Projectile.velocity;
		switch (enemyID)
		{
		case 181:
		{
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)), 237);
			for (int k = 0; k < 24; k++)
			{
				Vector2 position3 = pos + Main.rand.NextVector2Circular(16f, 32f);
				float scale = Main.rand.NextFloat(1f, 2f);
				Dust.NewDustPerfect(position3, 5, null, 0, default(Color), scale);
			}
			SoundStyle style = SoundID.NPCDeath1 with
			{
				Volume = 0.25f
			};
			SoundEngine.PlaySound(in style, pos);
			break;
		}
		case 173:
		{
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)), 223);
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)), 224);
			for (int j = 0; j < 24; j++)
			{
				Vector2 position2 = pos + Main.rand.NextVector2Circular(16f, 32f);
				float scale = Main.rand.NextFloat(1f, 2f);
				Dust.NewDustPerfect(position2, 5, null, 0, default(Color), scale);
			}
			SoundStyle style = SoundID.NPCDeath1 with
			{
				Volume = 0.25f
			};
			SoundEngine.PlaySound(in style, pos);
			break;
		}
		case 239:
		{
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)), 351);
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)), 352);
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)), 353);
			for (int i = 0; i < 24; i++)
			{
				Vector2 position = pos + Main.rand.NextVector2Circular(16f, 32f);
				float scale = Main.rand.NextFloat(1f, 2f);
				Dust.NewDustPerfect(position, 5, null, 0, default(Color), scale);
			}
			SoundStyle style = SoundID.NPCDeath1 with
			{
				Volume = 0.25f
			};
			SoundEngine.PlaySound(in style, pos);
			break;
		}
		default:
		{
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)), 42);
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)), 43);
			Gore.NewGore(base.Projectile.GetSource_Death(), pos - base.Projectile.velocity / 2f, velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)), 44);
			SoundStyle style = SoundID.NPCDeath2 with
			{
				Volume = 0.175f
			};
			SoundEngine.PlaySound(in style, pos);
			break;
		}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		if (EnemyGlowTextures.Count == 0 || !EnemyGlowTextures.ContainsKey(enemyID))
		{
			EnemyGlowTextures.Clear();
			int[] array = new int[3] { 181, 173, 239 };
			foreach (int id in array)
			{
				Main.instance.LoadNPC(id);
				Asset<Texture2D> baseTex = TextureAssets.Npc[id];
				Texture2D glow = new Texture2D(Main.graphics.GraphicsDevice, baseTex.Value.Width, baseTex.Value.Height);
				Color[] BaseArray = (Color[])(object)new Color[glow.Width * glow.Height];
				Color[] ColorArray = (Color[])(object)new Color[glow.Width * glow.Height];
				baseTex.Value.GetData<Color>(BaseArray);
				for (int j = 0; j < BaseArray.Length; j++)
				{
					if (((Color)(ref BaseArray[j])).A != 0)
					{
						ColorArray[j] = new Color(255, 255, 255);
					}
				}
				glow.SetData<Color>(ColorArray);
				EnemyGlowTextures.Add(id, glow);
			}
		}
		int type = enemyID;
		int wrapFrame = -1;
		int startFrame = 0;
		float mult = 0.75f;
		int frameCount;
		switch (type)
		{
		case 181:
			frameCount = 16;
			startFrame = 2;
			wrapFrame = 16;
			break;
		case 173:
			frameCount = 2;
			mult = 0.25f;
			break;
		case 239:
			frameCount = 5;
			mult = 0.25f;
			break;
		default:
			frameCount = 15;
			type = 21;
			startFrame = 1;
			break;
		}
		if (wrapFrame == -1)
		{
			wrapFrame = frameCount;
		}
		Texture2D tex = TextureAssets.Npc[type].Value;
		int currentFrame = (int)((float)(1200 - base.Projectile.timeLeft) * mult) % (wrapFrame - startFrame);
		Rectangle frame = tex.Frame(1, frameCount, 0, startFrame + currentFrame);
		float throwTime = Time - 180f;
		float opacity = 1f;
		if (Time < 10f)
		{
			opacity = Time / 10f;
		}
		if (throwTime <= 90f)
		{
			int pCount = 12;
			float wrapTime = 60f;
			float wrappedTime = MathHelper.Clamp(Time % (wrapTime + 1f) / wrapTime, 0f, 1f);
			if (wrappedTime == 0f)
			{
				evenRed = !evenRed;
			}
			float glowOpacity = MathHelper.Clamp(1f - (throwTime - 75f) / 15f, 0f, 1f);
			if (StunTime >= 0f)
			{
				glowOpacity = 1f - StunTime / 30f;
			}
			if (Time < 15f)
			{
				glowOpacity = Time / 15f;
			}
			if (curve != null)
			{
				Main.spriteBatch.End(out var snapshot);
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
				Texture2D ring = TextureAssets.Projectile[base.Type].Value;
				List<Vector2> points = curve.GetPoints(pCount);
				for (int k = 1; k < pCount; k++)
				{
					float num = MathHelper.Lerp(0.1f, 1f, (float)k / (float)(pCount - 1));
					float scale2 = MathHelper.Lerp(0.1f, 1f, (float)(k + 1) / (float)(pCount - 1));
					float scale3 = MathHelper.Lerp(num, scale2, wrappedTime);
					float rot1 = (points[k] - points[k - 1]).ToRotation();
					float rot2 = ((k != pCount - 1) ? (points[k + 1] - points[k]).ToRotation() : points[k].ToRotation());
					float rot3 = ((k != pCount - 1) ? rot1.AngleLerp(rot2, wrappedTime) : rot1);
					Vector2 pos;
					if (k == pCount - 1)
					{
						pos = Vector2.Lerp(points[k], points[k] + rot3.ToRotationVector2() * Vector2.Distance(points[points.Count - 1], points[points.Count - 2]), wrappedTime);
					}
					else
					{
						pos = Vector2.Lerp(points[k], points[k + 1], wrappedTime);
					}
					Color color = ((!evenRed) ? (((k % 2 == 0) ? Color.Magenta : Color.Red) * 0.666f) : (((k % 2 == 0) ? Color.Red : Color.Magenta) * 0.666f));
					if (k == pCount - 1)
					{
						color *= 1f - wrappedTime;
					}
					else if (k == 1)
					{
						color *= wrappedTime;
					}
					Main.spriteBatch.Draw(ring, pos + base.Projectile.velocity - Main.screenPosition, (Rectangle?)null, color * glowOpacity, rot3, ring.Size() * 0.5f, new Vector2(0.5f, 1f) * scale3, (SpriteEffects)0, 0f);
				}
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(in snapshot);
			}
			Vector2[] offsets = (Vector2[])(object)new Vector2[4]
			{
				Vector2.UnitX * 2f,
				Vector2.UnitX * -2f,
				Vector2.UnitY * 2f,
				Vector2.UnitY * -2f
			};
			for (int l = 0; l < 4; l++)
			{
				Main.EntitySpriteDraw(EnemyGlowTextures[enemyID], base.Projectile.Center - Main.screenPosition + offsets[l].RotatedBy(base.Projectile.rotation), frame, Color.Lerp(Color.Red, Color.Magenta, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 5f) / 2f + 0.5f) * glowOpacity, base.Projectile.rotation, frame.Size() * 0.5f, 1f, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, frame, lightColor * opacity, base.Projectile.rotation, frame.Size() * 0.5f, 1f, (SpriteEffects)0);
		return false;
	}

	public TelekineticEnemyGrab()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		throwPos = Vector2.Zero;
		MyTarget = -1;
		base._002Ector();
	}
}
