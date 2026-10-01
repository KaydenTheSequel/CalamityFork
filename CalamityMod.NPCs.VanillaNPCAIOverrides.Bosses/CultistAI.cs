using System;
using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class CultistAI : VanillaAIOverride
{
	public class AncientLightAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0168: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02af: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0349: Unknown result type (might be due to invalid IL or missing references)
			//IL_034e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0365: Unknown result type (might be due to invalid IL or missing references)
			//IL_036a: Unknown result type (might be due to invalid IL or missing references)
			//IL_030f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0319: Unknown result type (might be due to invalid IL or missing references)
			//IL_031e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0473: Unknown result type (might be due to invalid IL or missing references)
			//IL_0488: Unknown result type (might be due to invalid IL or missing references)
			//IL_048e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0490: Unknown result type (might be due to invalid IL or missing references)
			//IL_0495: Unknown result type (might be due to invalid IL or missing references)
			//IL_043c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0446: Unknown result type (might be due to invalid IL or missing references)
			//IL_044b: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.dontTakeDamage = true;
			if (base.NPC.ai[0] == -1f)
			{
				if (((Vector2)(ref base.NPC.velocity)).Length() >= 0.2f)
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.96f;
				}
				else
				{
					base.NPC.velocity = Vector2.Zero;
					base.NPC.position = base.NPC.oldPosition;
					base.NPC.ai[1]++;
					if (base.NPC.ai[1] >= 30f)
					{
						base.NPC.HitEffect(0, 9999.0);
						base.NPC.active = false;
					}
				}
				return false;
			}
			base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
			if (base.NPC.localAI[0] == 0f)
			{
				base.NPC.localAI[0] = 1f;
				base.NPC.velocity.X = base.NPC.ai[2];
				base.NPC.velocity.Y = base.NPC.ai[3];
				for (int i = 0; i < 13; i++)
				{
					int ancientLight = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 261, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 2.5f);
					Main.dust[ancientLight].noGravity = true;
					Main.dust[ancientLight].fadeIn = 1f;
					Dust obj = Main.dust[ancientLight];
					obj.velocity *= 4f;
					Main.dust[ancientLight].noLight = true;
				}
			}
			for (int j = 0; j < 2; j++)
			{
				if (Main.rand.Next(10 - (int)Math.Min(7f, ((Vector2)(ref base.NPC.velocity)).Length())) < 1)
				{
					int ancientLight2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 261, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 2.5f);
					Main.dust[ancientLight2].noGravity = true;
					Dust obj2 = Main.dust[ancientLight2];
					obj2.velocity *= 0.2f;
					Main.dust[ancientLight2].fadeIn = 0.4f;
					if (Main.rand.NextBool(6))
					{
						Dust obj3 = Main.dust[ancientLight2];
						obj3.velocity *= 5f;
						Main.dust[ancientLight2].noLight = true;
					}
					else
					{
						Main.dust[ancientLight2].velocity = base.NPC.DirectionFrom(Main.dust[ancientLight2].position) * ((Vector2)(ref Main.dust[ancientLight2].velocity)).Length();
					}
				}
			}
			if (base.NPC.ai[0] >= 0f)
			{
				if (base.NPC.ai[0] == 0f && CalamityGlobalNPC.adultEidolonWyrmHead != -1 && Main.npc[CalamityGlobalNPC.adultEidolonWyrmHead].active)
				{
					base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * PrimordialWyrmHead.LightDamageMult);
				}
				base.NPC.ai[0]++;
				float duration = 120f;
				if (base.NPC.ai[0] < duration - 60f && ((Vector2)(ref base.NPC.velocity)).Length() < 20f)
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 1.03f;
				}
				if (base.NPC.ai[0] >= duration - 60f)
				{
					base.NPC.velocity = base.NPC.velocity.RotatedBy(base.NPC.ai[1]);
				}
				if (base.NPC.ai[0] >= duration)
				{
					base.NPC.ai[0] = -1f;
				}
			}
			return false;
		}
	}

	public class AncientDoomAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_030a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0379: Unknown result type (might be due to invalid IL or missing references)
			//IL_0387: Unknown result type (might be due to invalid IL or missing references)
			//IL_038c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0399: Unknown result type (might be due to invalid IL or missing references)
			//IL_039e: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0407: Unknown result type (might be due to invalid IL or missing references)
			//IL_040c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Unknown result type (might be due to invalid IL or missing references)
			//IL_0417: Unknown result type (might be due to invalid IL or missing references)
			//IL_0424: Unknown result type (might be due to invalid IL or missing references)
			//IL_042a: Unknown result type (might be due to invalid IL or missing references)
			//IL_042c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0436: Unknown result type (might be due to invalid IL or missing references)
			//IL_043b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0470: Unknown result type (might be due to invalid IL or missing references)
			//IL_047e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0483: Unknown result type (might be due to invalid IL or missing references)
			//IL_0490: Unknown result type (might be due to invalid IL or missing references)
			//IL_0495: Unknown result type (might be due to invalid IL or missing references)
			//IL_049c: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04db: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0501: Unknown result type (might be due to invalid IL or missing references)
			//IL_0507: Unknown result type (might be due to invalid IL or missing references)
			//IL_0514: Unknown result type (might be due to invalid IL or missing references)
			//IL_051a: Unknown result type (might be due to invalid IL or missing references)
			//IL_051c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0526: Unknown result type (might be due to invalid IL or missing references)
			//IL_052b: Unknown result type (might be due to invalid IL or missing references)
			//IL_064c: Unknown result type (might be due to invalid IL or missing references)
			//IL_066d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0673: Unknown result type (might be due to invalid IL or missing references)
			//IL_0675: Unknown result type (might be due to invalid IL or missing references)
			//IL_0560: Unknown result type (might be due to invalid IL or missing references)
			//IL_056e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0573: Unknown result type (might be due to invalid IL or missing references)
			//IL_0580: Unknown result type (might be due to invalid IL or missing references)
			//IL_0585: Unknown result type (might be due to invalid IL or missing references)
			//IL_058c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0591: Unknown result type (might be due to invalid IL or missing references)
			//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_071b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0720: Unknown result type (might be due to invalid IL or missing references)
			//IL_0731: Unknown result type (might be due to invalid IL or missing references)
			//IL_074a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0750: Unknown result type (might be due to invalid IL or missing references)
			//IL_0752: Unknown result type (might be due to invalid IL or missing references)
			//IL_0757: Unknown result type (might be due to invalid IL or missing references)
			//IL_0770: Unknown result type (might be due to invalid IL or missing references)
			//IL_0775: Unknown result type (might be due to invalid IL or missing references)
			bool num = CalamityWorld.death || BossRushEvent.BossRushActive;
			base.NPC.damage = (base.NPC.defDamage = 0);
			float duration = 420f;
			float spawnAnimTime = 120f;
			int rateOfChange = 6;
			float splitProjVelocity = (num ? 4.5f : 3f);
			_ = (float)Main.npc[(int)base.NPC.ai[0]].life / (float)Main.npc[(int)base.NPC.ai[0]].lifeMax;
			_ = Main.npc[(int)base.NPC.ai[0]].type;
			ModContent.NPCType<PrimordialWyrmHead>();
			bool kill = base.NPC.ai[1] < 0f || !Main.npc[(int)base.NPC.ai[0]].active;
			int target = 255;
			if (Main.npc[(int)base.NPC.ai[0]].type == 439 || Main.npc[(int)base.NPC.ai[0]].type == ModContent.NPCType<PrimordialWyrmHead>())
			{
				if (target == 255)
				{
					target = Main.npc[(int)base.NPC.ai[0]].target;
				}
				if (Main.npc[(int)base.NPC.ai[0]].type == ModContent.NPCType<PrimordialWyrmHead>())
				{
					base.NPC.dontTakeDamage = true;
				}
			}
			else
			{
				kill = true;
			}
			base.NPC.ai[1] += rateOfChange;
			float growthRate = base.NPC.ai[1] / spawnAnimTime;
			growthRate = MathHelper.Clamp(growthRate, 0f, 1f);
			base.NPC.position = base.NPC.Center;
			base.NPC.scale = MathHelper.Lerp(0f, 1f, growthRate);
			base.NPC.Center = base.NPC.position;
			base.NPC.alpha = (int)(255f - growthRate * 255f);
			if (base.NPC.ai[3] == 0f)
			{
				base.NPC.ai[3] = base.NPC.ai[2];
			}
			double rad = (double)base.NPC.ai[3] * (Math.PI / 180.0);
			double dist = 550.0;
			if (Main.npc[(int)base.NPC.ai[0]].type == ModContent.NPCType<PrimordialWyrmHead>())
			{
				int ancientDoomScale = (int)((Main.npc[(int)base.NPC.ai[0]].Calamity().newAI[2] - 30f) / 120f);
				dist += (double)(ancientDoomScale * 45);
			}
			base.NPC.position.X = Main.player[target].Center.X - (float)(int)(Math.Cos(rad) * dist) - (float)(base.NPC.width / 2);
			base.NPC.position.Y = Main.player[target].Center.Y - (float)(int)(Math.Sin(rad) * dist) - (float)(base.NPC.height / 2);
			float spinVelocity = 8f * (1f - base.NPC.ai[1] / duration);
			base.NPC.ai[3] += spinVelocity;
			if (Main.rand.NextBool(6))
			{
				Vector2 shadowflameDustRotate = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				Dust obj = Main.dust[Dust.NewDust(base.NPC.Center - shadowflameDustRotate * 20f, 0, 0, 27)];
				obj.noGravity = true;
				obj.position = base.NPC.Center - shadowflameDustRotate * (float)Main.rand.Next(10, 21) * base.NPC.scale;
				obj.velocity = shadowflameDustRotate.RotatedBy(1.5707963705062866) * 4f;
				obj.scale = 0.5f + Main.rand.NextFloat();
				obj.fadeIn = 0.5f;
			}
			if (Main.rand.NextBool(6))
			{
				Vector2 darkDustRotate = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				Dust obj2 = Main.dust[Dust.NewDust(base.NPC.Center - darkDustRotate * 30f, 0, 0, 240)];
				obj2.noGravity = true;
				obj2.position = base.NPC.Center - darkDustRotate * 20f * base.NPC.scale;
				obj2.velocity = darkDustRotate.RotatedBy(-1.5707963705062866) * 2f;
				obj2.scale = 0.5f + Main.rand.NextFloat();
				obj2.fadeIn = 0.5f;
			}
			if (Main.rand.NextBool(6))
			{
				Vector2 darkDustRotate2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				Dust obj3 = Main.dust[Dust.NewDust(base.NPC.Center - darkDustRotate2 * 30f, 0, 0, 240)];
				obj3.position = base.NPC.Center - darkDustRotate2 * 20f * base.NPC.scale;
				obj3.velocity = Vector2.Zero;
				obj3.scale = 0.5f + Main.rand.NextFloat();
				obj3.fadeIn = 0.5f;
				obj3.noLight = true;
			}
			base.NPC.localAI[0] += (float)Math.PI / 60f;
			base.NPC.localAI[1] = 0.25f + Vector2.UnitY.RotatedBy(base.NPC.ai[1] * ((float)Math.PI * 2f) / 60f).Y * 0.25f;
			if (base.NPC.ai[1] >= duration)
			{
				int type = 593;
				int damage = DoomDamage;
				if (Main.npc[(int)base.NPC.ai[0]].type == ModContent.NPCType<PrimordialWyrmHead>())
				{
					damage = (int)Math.Round((float)damage * PrimordialWyrmHead.DoomDamageMult);
				}
				kill = true;
				if (Main.netMode != 1)
				{
					int totalProjectiles = (Main.getGoodWorld ? 5 : 3);
					_ = (float)Math.PI * 2f / (float)totalProjectiles;
					Vector2 spinningPoint = default(Vector2);
					((Vector2)(ref spinningPoint))._002Ector(0f, 0f - splitProjVelocity);
					float rotOffset = base.NPC.DirectionTo(Main.player[target].Center).ToRotation();
					for (int k = 0; k < totalProjectiles; k++)
					{
						Vector2 doomProjRotate = spinningPoint.RotatedBy(rotOffset + (float)Math.PI * ((float)(k + 1) / (float)(totalProjectiles + 1)));
						Main.projectile[Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, doomProjRotate, type, damage, 0f, Main.myPlayer)].tileCollide = false;
					}
				}
			}
			if (kill)
			{
				base.NPC.HitEffect(0, 9999.0);
				base.NPC.active = false;
			}
			return false;
		}
	}

	public static int CloneFireballDamage = 18;

	public static int FireballDamage = 20;

	public static int IceMistDamage = 1;

	public static int LightningDamage = 30;

	public static int DoomDamage = 45;

	public override bool AI(Mod mod)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0982: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0991: Unknown result type (might be due to invalid IL or missing references)
		//IL_147c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1487: Unknown result type (might be due to invalid IL or missing references)
		//IL_148c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1491: Unknown result type (might be due to invalid IL or missing references)
		//IL_1496: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_109b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_100c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1017: Unknown result type (might be due to invalid IL or missing references)
		//IL_101c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1021: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c80: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1436: Unknown result type (might be due to invalid IL or missing references)
		//IL_143b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_186a: Unknown result type (might be due to invalid IL or missing references)
		//IL_186f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_26d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_26f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_26f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2693: Unknown result type (might be due to invalid IL or missing references)
		//IL_2698: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1306: Unknown result type (might be due to invalid IL or missing references)
		//IL_1311: Unknown result type (might be due to invalid IL or missing references)
		//IL_1316: Unknown result type (might be due to invalid IL or missing references)
		//IL_131b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1320: Unknown result type (might be due to invalid IL or missing references)
		//IL_1322: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1349: Unknown result type (might be due to invalid IL or missing references)
		//IL_1362: Unknown result type (might be due to invalid IL or missing references)
		//IL_1367: Unknown result type (might be due to invalid IL or missing references)
		//IL_136c: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1372: Unknown result type (might be due to invalid IL or missing references)
		//IL_1377: Unknown result type (might be due to invalid IL or missing references)
		//IL_138a: Unknown result type (might be due to invalid IL or missing references)
		//IL_138c: Unknown result type (might be due to invalid IL or missing references)
		//IL_170c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1711: Unknown result type (might be due to invalid IL or missing references)
		//IL_1716: Unknown result type (might be due to invalid IL or missing references)
		//IL_1718: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d50: Unknown result type (might be due to invalid IL or missing references)
		//IL_2aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_25bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_173f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1758: Unknown result type (might be due to invalid IL or missing references)
		//IL_175d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1762: Unknown result type (might be due to invalid IL or missing references)
		//IL_1764: Unknown result type (might be due to invalid IL or missing references)
		//IL_177a: Unknown result type (might be due to invalid IL or missing references)
		//IL_177f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1781: Unknown result type (might be due to invalid IL or missing references)
		//IL_178c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1791: Unknown result type (might be due to invalid IL or missing references)
		//IL_11eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2485: Unknown result type (might be due to invalid IL or missing references)
		//IL_248a: Unknown result type (might be due to invalid IL or missing references)
		//IL_248c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2494: Unknown result type (might be due to invalid IL or missing references)
		//IL_2499: Unknown result type (might be due to invalid IL or missing references)
		//IL_249e: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2395: Unknown result type (might be due to invalid IL or missing references)
		//IL_239a: Unknown result type (might be due to invalid IL or missing references)
		//IL_239c: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1228: Unknown result type (might be due to invalid IL or missing references)
		//IL_122d: Unknown result type (might be due to invalid IL or missing references)
		//IL_122f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1234: Unknown result type (might be due to invalid IL or missing references)
		//IL_1239: Unknown result type (might be due to invalid IL or missing references)
		//IL_123b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23be: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2210: Unknown result type (might be due to invalid IL or missing references)
		//IL_2215: Unknown result type (might be due to invalid IL or missing references)
		//IL_2217: Unknown result type (might be due to invalid IL or missing references)
		//IL_221f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2224: Unknown result type (might be due to invalid IL or missing references)
		//IL_2229: Unknown result type (might be due to invalid IL or missing references)
		//IL_222b: Unknown result type (might be due to invalid IL or missing references)
		//IL_222d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2120: Unknown result type (might be due to invalid IL or missing references)
		//IL_2125: Unknown result type (might be due to invalid IL or missing references)
		//IL_2127: Unknown result type (might be due to invalid IL or missing references)
		//IL_212f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2134: Unknown result type (might be due to invalid IL or missing references)
		//IL_2139: Unknown result type (might be due to invalid IL or missing references)
		//IL_213b: Unknown result type (might be due to invalid IL or missing references)
		//IL_213d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1974: Unknown result type (might be due to invalid IL or missing references)
		//IL_1979: Unknown result type (might be due to invalid IL or missing references)
		//IL_197d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1987: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_125c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1272: Unknown result type (might be due to invalid IL or missing references)
		//IL_1277: Unknown result type (might be due to invalid IL or missing references)
		//IL_127c: Unknown result type (might be due to invalid IL or missing references)
		//IL_127e: Unknown result type (might be due to invalid IL or missing references)
		//IL_128e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1293: Unknown result type (might be due to invalid IL or missing references)
		//IL_1295: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1255: Unknown result type (might be due to invalid IL or missing references)
		//IL_125a: Unknown result type (might be due to invalid IL or missing references)
		//IL_286b: Unknown result type (might be due to invalid IL or missing references)
		//IL_287b: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_224c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2239: Unknown result type (might be due to invalid IL or missing references)
		//IL_223e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2243: Unknown result type (might be due to invalid IL or missing references)
		//IL_215c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2149: Unknown result type (might be due to invalid IL or missing references)
		//IL_214e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2153: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0def: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e56: Unknown result type (might be due to invalid IL or missing references)
		//IL_251c: Unknown result type (might be due to invalid IL or missing references)
		//IL_242c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2273: Unknown result type (might be due to invalid IL or missing references)
		//IL_2183: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1614: Unknown result type (might be due to invalid IL or missing references)
		//IL_162a: Unknown result type (might be due to invalid IL or missing references)
		//IL_162f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1634: Unknown result type (might be due to invalid IL or missing references)
		//IL_1636: Unknown result type (might be due to invalid IL or missing references)
		//IL_1646: Unknown result type (might be due to invalid IL or missing references)
		//IL_164b: Unknown result type (might be due to invalid IL or missing references)
		//IL_164d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1658: Unknown result type (might be due to invalid IL or missing references)
		//IL_165d: Unknown result type (might be due to invalid IL or missing references)
		//IL_166b: Unknown result type (might be due to invalid IL or missing references)
		//IL_166d: Unknown result type (might be due to invalid IL or missing references)
		//IL_160d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e83: Unknown result type (might be due to invalid IL or missing references)
		//IL_2be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_28bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_28c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_28cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_28fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2903: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_2815: Unknown result type (might be due to invalid IL or missing references)
		//IL_281a: Unknown result type (might be due to invalid IL or missing references)
		//IL_281e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2828: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eae: Unknown result type (might be due to invalid IL or missing references)
		//IL_297d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2984: Unknown result type (might be due to invalid IL or missing references)
		//IL_298a: Unknown result type (might be due to invalid IL or missing references)
		//IL_298c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2993: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_29dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_29fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_206d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2072: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f53: Unknown result type (might be due to invalid IL or missing references)
		//IL_2019: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f76: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		if (base.NPC.ai[0] != -1f && Main.rand.NextBool(1000))
		{
			SoundEngine.PlaySound(Utils.SelectRandom<SoundStyle>(Main.rand, SoundID.Zombie88, SoundID.Zombie89, SoundID.Zombie90, SoundID.Zombie91), base.NPC.Center);
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool num = (lifeRatio < 0.85f) | death;
		bool phase3 = (lifeRatio < 0.7f) | death;
		bool phase4 = lifeRatio < (death ? 0.8f : 0.55f);
		bool phase5 = lifeRatio < (death ? 0.6f : 0.4f);
		bool phase6 = lifeRatio < (death ? 0.4f : 0.25f);
		bool phase7 = death && lifeRatio < 0.2f;
		bool phase8 = death && lifeRatio < 0.1f;
		bool isCultist = base.NPC.type == 439;
		bool dontTakeDamage = false;
		float predictionDistance = 480f;
		float distanceAboveTarget = -240f;
		float moveSpeed = (death ? 300f : 75f);
		int iceMistDamage = (isCultist ? IceMistDamage : 0);
		int fireballDamage = (isCultist ? FireballDamage : CloneFireballDamage);
		int lightningDamage = (isCultist ? LightningDamage : 0);
		int iceMistFireRate = (num ? 50 : 60);
		float iceMistSpeed = 12f + (death ? 4f : 2f) * (1f - lifeRatio);
		int iceMistAmt = ((!phase3) ? 1 : 2);
		int fireballFireRate = (phase5 ? 20 : 24) - (death ? 5 : 0);
		float fireballSpeed = ((phase7 ? 8f : (phase6 ? 7f : 6f)) + (death ? (1f - lifeRatio) : 0f)) * (isCultist ? 1f : 0.5f);
		int fireballAmt = (death ? 8 : 4);
		int lightningOrbPhaseTime = (num ? 90 : 120);
		int ancientLightSpawnRate = (phase7 ? 20 : (phase4 ? 25 : 30));
		int ancientLightAmt = (phase7 ? 4 : (phase4 ? 3 : 2));
		int ancientDoomLimit = 10;
		int idleTime = (phase8 ? 40 : (phase7 ? 45 : (phase3 ? 55 : 60)));
		float timeToFinishRitual = (phase8 ? 180f : (phase7 ? 240f : (phase5 ? 300f : 360f)));
		if (Main.getGoodWorld)
		{
			iceMistFireRate = 40;
			iceMistSpeed = 15f;
			fireballFireRate = 8;
			fireballSpeed *= 1.25f;
			lightningOrbPhaseTime = 60;
			ancientLightSpawnRate = 10;
			ancientLightAmt = 5;
			idleTime = 20;
		}
		Player player = Main.player[base.NPC.target];
		if (base.NPC.target < 0 || base.NPC.target == 255 || player.dead || !player.active || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
		{
			CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
			options.faceTarget = false;
			base.NPC.CalamityTargeting(options);
			player = Main.player[base.NPC.target];
			base.NPC.netUpdate = true;
		}
		if (!Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
		{
			calamityGlobalNPC.newAI[0]++;
			if (calamityGlobalNPC.newAI[0] >= 120f)
			{
				calamityGlobalNPC.newAI[0] = 120f;
				iceMistSpeed = 16f;
				iceMistFireRate = 15;
				lightningOrbPhaseTime = 30;
				ancientLightSpawnRate = 5;
				idleTime = 10;
				timeToFinishRitual = 120f;
			}
		}
		else if (calamityGlobalNPC.newAI[0] > 0f)
		{
			calamityGlobalNPC.newAI[0]--;
		}
		if (!isCultist)
		{
			if (base.NPC.ai[3] < 0f || !Main.npc[(int)base.NPC.ai[3]].active || Main.npc[(int)base.NPC.ai[3]].type != 439)
			{
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.active = false;
				return false;
			}
			base.NPC.ai[0] = Main.npc[(int)base.NPC.ai[3]].ai[0];
			base.NPC.ai[1] = Main.npc[(int)base.NPC.ai[3]].ai[1];
			dontTakeDamage = true;
			if (base.NPC.ai[0] == 5f && base.NPC.ai[1] >= 120f && base.NPC.ai[1] < timeToFinishRitual)
			{
				dontTakeDamage = false;
				if (base.NPC.justHit)
				{
					Main.npc[(int)base.NPC.ai[3]].ai[1] = timeToFinishRitual;
				}
			}
		}
		else if (base.NPC.ai[0] == 5f && base.NPC.ai[1] >= 120f && base.NPC.ai[1] < timeToFinishRitual && base.NPC.justHit)
		{
			base.NPC.ai[0] = 0f;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[3]++;
			base.NPC.velocity = Vector2.Zero;
			CalamityTargetingParameters options2 = CalamityTargetingParameters.BossDefaults;
			options2.faceTarget = false;
			base.NPC.CalamityTargeting(options2);
			base.NPC.netUpdate = true;
			Main.projectile[(int)base.NPC.ai[2]].ai[1] = -1f;
			Main.projectile[(int)base.NPC.ai[2]].netUpdate = true;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC item = enumerator.Current;
				if (item.type == 440 && item.ai[3] == (float)base.NPC.whoAmI)
				{
					item.active = false;
					item.ForceNetUpdate();
				}
			}
		}
		if (player.dead || !player.active || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			if (Main.netMode != 1)
			{
				NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
			}
			for (int j = 0; j < Main.maxNPCs; j++)
			{
				if (Main.npc[j].active && Main.npc[j].type == 440 && Main.npc[j].ai[3] == (float)base.NPC.whoAmI)
				{
					Main.npc[j].life = 0;
					Main.npc[j].HitEffect();
					Main.npc[j].active = false;
					if (Main.netMode != 1)
					{
						NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
					}
				}
			}
		}
		float clonePhase = base.NPC.ai[3];
		if (base.NPC.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Zombie89, base.NPC.Center);
			base.NPC.localAI[0] = 1f;
			base.NPC.alpha = 255;
			base.NPC.rotation = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.netUpdate = true;
			}
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.alpha -= 5;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 420f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			else if (base.NPC.ai[1] > 360f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.95f;
				if (base.NPC.localAI[2] != 13f)
				{
					SoundEngine.PlaySound(in SoundID.Zombie105, base.NPC.Center);
				}
				base.NPC.localAI[2] = 13f;
			}
			else if (base.NPC.ai[1] > 300f)
			{
				base.NPC.velocity = -Vector2.UnitY;
				base.NPC.localAI[2] = 10f;
			}
			else if (base.NPC.ai[1] > 120f)
			{
				base.NPC.localAI[2] = 1f;
			}
			else
			{
				base.NPC.localAI[2] = 0f;
			}
			dontTakeDamage = true;
		}
		Vector2 center;
		if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.ai[1] == 0f)
			{
				CalamityTargetingParameters options3 = CalamityTargetingParameters.BossDefaults;
				options3.faceTarget = false;
				base.NPC.CalamityTargeting(options3);
			}
			base.NPC.localAI[2] = 10f;
			int facePlayerDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (facePlayerDirection != 0)
			{
				base.NPC.direction = (base.NPC.spriteDirection = facePlayerDirection);
			}
			base.NPC.ai[1]++;
			if ((base.NPC.ai[1] >= (float)idleTime) & isCultist)
			{
				int phase9 = 0;
				switch ((int)base.NPC.ai[3])
				{
				case 0:
				case 4:
				case 6:
				case 8:
				case 12:
				case 14:
				case 16:
				case 18:
				case 20:
					phase9 = 0;
					break;
				case 2:
				case 10:
					phase9 = 0;
					distanceAboveTarget *= 1.5f;
					predictionDistance *= 1.5f;
					break;
				case 1:
				case 15:
					phase9 = 1;
					break;
				case 3:
				case 11:
					phase9 = 5;
					break;
				case 5:
				case 13:
					phase9 = 3;
					break;
				case 7:
				case 17:
					phase9 = 2;
					break;
				case 9:
				case 19:
				{
					int[] attackPhases = new int[4] { 1, 2, 3, 5 };
					phase9 = ((NPC.CountNPCS(523) < ancientDoomLimit) ? 6 : attackPhases[Main.rand.Next(attackPhases.Length)]);
					break;
				}
				case 21:
					phase9 = 4;
					base.NPC.ai[3] = -1f;
					break;
				default:
					base.NPC.ai[3] = -1f;
					break;
				}
				switch (phase9)
				{
				case 0:
				{
					Vector2 predictionVector = default(Vector2);
					((Vector2)(ref predictionVector))._002Ector(0f + player.velocity.SafeNormalize(Vector2.Zero).X * predictionDistance, distanceAboveTarget);
					center = player.Center + predictionVector - base.NPC.Center;
					float moveDistance = (float)Math.Ceiling(((Vector2)(ref center)).Length() / moveSpeed);
					if (moveDistance == 0f)
					{
						moveDistance = 1f;
					}
					List<int> list2 = new List<int>();
					int cloneAmt = 0;
					list2.Add(base.NPC.whoAmI);
					for (int k = 0; k < Main.maxNPCs; k++)
					{
						if (Main.npc[k].active && Main.npc[k].type == 440 && Main.npc[k].ai[3] == (float)base.NPC.whoAmI)
						{
							list2.Add(k);
						}
					}
					bool cloneAmtIsEven = list2.Count % 2 == 0;
					foreach (int current2 in list2)
					{
						NPC nPC2 = Main.npc[current2];
						Vector2 center2 = nPC2.Center;
						float cloneOffset = (float)((cloneAmt + cloneAmtIsEven.ToInt() + 1) / 2) * ((float)Math.PI * 2f) * 0.4f / (float)list2.Count;
						if (cloneAmt % 2 == 1)
						{
							cloneOffset *= -1f;
						}
						if (list2.Count == 1)
						{
							cloneOffset = 0f;
						}
						Vector2 spinningpoint = new Vector2(0f, -1f);
						double radians = cloneOffset;
						center = default(Vector2);
						Vector2 cloneRotation = Utils.RotatedBy(spinningpoint, radians, center) * new Vector2(150f, 200f);
						Vector2 finalClonePos = player.Center + Vector2.UnitX * predictionVector.X + cloneRotation - center2;
						nPC2.ai[0] = 1f;
						nPC2.ai[1] = moveDistance;
						nPC2.velocity = finalClonePos / moveDistance * 2f;
						if (base.NPC.whoAmI >= nPC2.whoAmI)
						{
							nPC2.position -= nPC2.velocity;
						}
						nPC2.netUpdate = true;
						cloneAmt++;
					}
					break;
				}
				case 1:
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = 0f;
					break;
				case 2:
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					break;
				case 3:
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
					break;
				case 4:
					base.NPC.ai[0] = 5f;
					base.NPC.ai[1] = 0f;
					break;
				case 5:
					base.NPC.ai[0] = 7f;
					base.NPC.ai[1] = 0f;
					break;
				case 6:
					base.NPC.ai[0] = 8f;
					base.NPC.ai[1] = 0f;
					break;
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.localAI[2] = 10f;
			if (base.NPC.ai[1] % 2f != 0f && base.NPC.ai[1] != 1f)
			{
				NPC nPC3 = base.NPC;
				nPC3.position -= base.NPC.velocity;
			}
			base.NPC.ai[1]--;
			if (base.NPC.ai[1] <= 0f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.localAI[2] = 11f;
			Vector2 vec = Vector2.Normalize(player.Center - base.NPC.Center);
			if (vec.HasNaNs())
			{
				((Vector2)(ref vec))._002Ector((float)base.NPC.direction, 0f);
			}
			if (((base.NPC.ai[1] >= 4f) & isCultist) && (int)(base.NPC.ai[1] - 4f) % iceMistFireRate == 0)
			{
				if (Main.netMode != 1)
				{
					List<int> list3 = new List<int>();
					for (int l = 0; l < Main.maxNPCs; l++)
					{
						if (Main.npc[l].active && Main.npc[l].type == 440 && Main.npc[l].ai[3] == (float)base.NPC.whoAmI)
						{
							list3.Add(l);
						}
					}
					foreach (int current3 in list3)
					{
						NPC nPC4 = Main.npc[current3];
						Vector2 center3 = nPC4.Center;
						int cloneFacePlayerDirection = Math.Sign(player.Center.X - center3.X);
						if (cloneFacePlayerDirection != 0)
						{
							nPC4.direction = (nPC4.spriteDirection = cloneFacePlayerDirection);
						}
						vec = Vector2.Normalize(player.Center - center3);
						if (vec.HasNaNs())
						{
							vec = new Vector2((float)base.NPC.direction, 0f);
						}
						Vector2 shadowFireballDirection = center3 + new Vector2((float)(base.NPC.direction * 30), 12f);
						Vector2 shadowFireballVelocity = vec * (fireballSpeed + (float)Main.rand.NextDouble());
						shadowFireballVelocity = shadowFireballVelocity.RotatedByRandom(Math.PI / 6.0);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shadowFireballDirection, shadowFireballVelocity, 468, fireballDamage, 0f, Main.myPlayer);
					}
				}
				if (Main.netMode != 1)
				{
					vec = Vector2.Normalize(player.Center - base.NPC.Center);
					if (vec.HasNaNs())
					{
						((Vector2)(ref vec))._002Ector((float)base.NPC.direction, 0f);
					}
					Vector2 iceMistDirection = base.NPC.Center + new Vector2((float)(base.NPC.direction * 30), 12f);
					Vector2 iceMistVelocity = vec * iceMistSpeed;
					Main.projectile[Projectile.NewProjectile(base.NPC.GetSource_FromAI(), iceMistDirection, iceMistVelocity, 464, iceMistDamage, 0f, Main.myPlayer, 0f, 1f)].timeLeft = 240;
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (float)(4 + iceMistFireRate * iceMistAmt))
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.localAI[2] = 11f;
			Vector2 playerDirection = Vector2.Normalize(player.Center - base.NPC.Center);
			if (playerDirection.HasNaNs())
			{
				((Vector2)(ref playerDirection))._002Ector((float)base.NPC.direction, 0f);
			}
			if (((base.NPC.ai[1] >= 4f) & isCultist) && (int)(base.NPC.ai[1] - 4f) % fireballFireRate == 0)
			{
				if ((int)(base.NPC.ai[1] - 4f) / fireballFireRate == 2)
				{
					List<int> list4 = new List<int>();
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						if (Main.npc[i].active && Main.npc[i].type == 440 && Main.npc[i].ai[3] == (float)base.NPC.whoAmI)
						{
							list4.Add(i);
						}
					}
					if (Main.netMode != 1)
					{
						foreach (int current4 in list4)
						{
							NPC nPC5 = Main.npc[current4];
							Vector2 center4 = nPC5.Center;
							int cloneFireballFaceDirection = Math.Sign(player.Center.X - center4.X);
							if (cloneFireballFaceDirection != 0)
							{
								nPC5.direction = (nPC5.spriteDirection = cloneFireballFaceDirection);
							}
							playerDirection = Vector2.Normalize(player.Center - center4);
							if (playerDirection.HasNaNs())
							{
								playerDirection = new Vector2((float)base.NPC.direction, 0f);
							}
							Vector2 shadowFireballDirection2 = center4 + new Vector2((float)(base.NPC.direction * 30), 12f);
							Vector2 shadowFireballVelocity2 = playerDirection * (fireballSpeed + (float)Main.rand.NextDouble());
							shadowFireballVelocity2 = shadowFireballVelocity2.RotatedByRandom(Math.PI / 6.0);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shadowFireballDirection2, shadowFireballVelocity2, 468, CloneFireballDamage, 0f, Main.myPlayer);
						}
					}
				}
				int cultistFireballFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
				if (cultistFireballFaceDirection != 0)
				{
					base.NPC.direction = (base.NPC.spriteDirection = cultistFireballFaceDirection);
				}
				if (Main.netMode != 1)
				{
					playerDirection = base.NPC.DirectionTo(player.Center);
					if (playerDirection.HasNaNs())
					{
						((Vector2)(ref playerDirection))._002Ector((float)base.NPC.direction, 0f);
					}
					Vector2 fireballDirection = base.NPC.Center + new Vector2((float)(base.NPC.direction * 30), 12f);
					Vector2 fireballVelocity = playerDirection * (fireballSpeed + (float)Main.rand.NextDouble() * 2f);
					fireballVelocity = fireballVelocity.RotatedByRandom(Math.PI / 6.0);
					for (int m = -1; m <= 1; m++)
					{
						IEntitySource source_FromAI = base.NPC.GetSource_FromAI();
						Vector2 spinningpoint2 = fireballVelocity;
						double radians2 = 1f * (float)m;
						center = default(Vector2);
						Projectile.NewProjectile(source_FromAI, fireballDirection, spinningpoint2.RotatedBy(radians2, center), 467, fireballDamage, 0f, Main.myPlayer);
					}
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (float)(4 + fireballFireRate * fireballAmt))
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			if (isCultist)
			{
				base.NPC.localAI[2] = 12f;
			}
			else
			{
				base.NPC.localAI[2] = 11f;
			}
			if (((base.NPC.ai[1] == 20f) & isCultist) && Main.netMode != 1)
			{
				List<int> list5 = new List<int>();
				for (int n = 0; n < Main.maxNPCs; n++)
				{
					if (Main.npc[n].active && Main.npc[n].type == 440 && Main.npc[n].ai[3] == (float)base.NPC.whoAmI)
					{
						list5.Add(n);
					}
				}
				foreach (int current5 in list5)
				{
					NPC nPC6 = Main.npc[current5];
					Vector2 center5 = nPC6.Center;
					int clonePlayerFaceDirection = Math.Sign(player.Center.X - center5.X);
					if (clonePlayerFaceDirection != 0)
					{
						nPC6.direction = (nPC6.spriteDirection = clonePlayerFaceDirection);
					}
					Vector2 playerDirection2 = Vector2.Normalize(player.Center - center5);
					if (playerDirection2.HasNaNs())
					{
						((Vector2)(ref playerDirection2))._002Ector((float)base.NPC.direction, 0f);
					}
					Vector2 shadowFireballDirection3 = center5 + new Vector2((float)(base.NPC.direction * 30), 12f);
					Vector2 shadowFireballVelocity3 = playerDirection2 * (fireballSpeed + (float)Main.rand.NextDouble());
					shadowFireballVelocity3 = shadowFireballVelocity3.RotatedByRandom(Math.PI / 6.0);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shadowFireballDirection3, shadowFireballVelocity3, 468, fireballDamage, 0f, Main.myPlayer);
				}
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y + (death ? 210f : (-100f)), 0f, 0f, 465, lightningDamage, 0f, Main.myPlayer);
				if (death)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X + 210f, base.NPC.Center.Y - 210f, 0f, 0f, 465, lightningDamage, 0f, Main.myPlayer);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X - 210f, base.NPC.Center.Y - 210f, 0f, 0f, 465, lightningDamage, 0f, Main.myPlayer);
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (float)(20 + lightningOrbPhaseTime))
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.localAI[2] = 10f;
			if (Vector2.Normalize(player.Center - base.NPC.Center).HasNaNs())
			{
				new Vector2((float)base.NPC.direction, 0f);
			}
			if (base.NPC.ai[1] >= 0f && base.NPC.ai[1] < 30f)
			{
				dontTakeDamage = true;
				float cultistAlphaControl = (base.NPC.ai[1] - 0f) / 30f;
				base.NPC.alpha = (int)(cultistAlphaControl * 255f);
			}
			else if (base.NPC.ai[1] >= 30f && base.NPC.ai[1] < 90f)
			{
				if ((base.NPC.ai[1] == 30f && Main.netMode != 1) & isCultist)
				{
					base.NPC.localAI[1]++;
					Vector2 spinningpoint3 = default(Vector2);
					((Vector2)(ref spinningpoint3))._002Ector(180f, 0f);
					List<int> list6 = new List<int>();
					for (int num2 = 0; num2 < Main.maxNPCs; num2++)
					{
						if (Main.npc[num2].active && Main.npc[num2].type == 440 && Main.npc[num2].ai[3] == (float)base.NPC.whoAmI)
						{
							list6.Add(num2);
						}
					}
					int maxClonesSpawned = 2;
					if (death)
					{
						maxClonesSpawned += 2;
					}
					if (lifeRatio < 0.75f)
					{
						maxClonesSpawned++;
					}
					if (lifeRatio < 0.5f)
					{
						maxClonesSpawned++;
					}
					if (lifeRatio < 0.25f)
					{
						maxClonesSpawned++;
					}
					if (lifeRatio < 0.1f)
					{
						maxClonesSpawned++;
					}
					int potentialExtraClones = 8 - list6.Count;
					if (potentialExtraClones > maxClonesSpawned)
					{
						potentialExtraClones = maxClonesSpawned;
					}
					int newCloneAmt = list6.Count + potentialExtraClones + 1;
					float[] array = new float[newCloneAmt];
					for (int cloneInc = 0; cloneInc < array.Length; cloneInc++)
					{
						int num3 = cloneInc;
						Vector2 center6 = base.NPC.Center;
						Vector2 spinningpoint4 = spinningpoint3;
						double radians3 = (float)cloneInc * ((float)Math.PI * 2f) / (float)newCloneAmt - (float)Math.PI / 2f;
						center = default(Vector2);
						array[num3] = Vector2.Distance(center6 + spinningpoint4.RotatedBy(radians3, center), player.Center);
					}
					int rotateDistance = 0;
					for (int num4 = 1; num4 < array.Length; num4++)
					{
						if (array[rotateDistance] > array[num4])
						{
							rotateDistance = num4;
						}
					}
					rotateDistance = ((rotateDistance >= newCloneAmt / 2) ? (rotateDistance - newCloneAmt / 2) : (rotateDistance + newCloneAmt / 2));
					int clonesToSpawn = potentialExtraClones;
					for (int num5 = 0; num5 < array.Length; num5++)
					{
						if (rotateDistance != num5)
						{
							Vector2 center7 = base.NPC.Center;
							Vector2 spinningpoint5 = spinningpoint3;
							double radians4 = (float)num5 * ((float)Math.PI * 2f) / (float)newCloneAmt - (float)Math.PI / 2f;
							center = default(Vector2);
							Vector2 cloneRotation2 = center7 + spinningpoint5.RotatedBy(radians4, center);
							if (clonesToSpawn-- > 0)
							{
								int cloneSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)cloneRotation2.X, (int)cloneRotation2.Y + base.NPC.height / 2, 440, base.NPC.whoAmI);
								Main.npc[cloneSpawn].ai[3] = base.NPC.whoAmI;
								Main.npc[cloneSpawn].netUpdate = true;
								Main.npc[cloneSpawn].localAI[1] = base.NPC.localAI[1];
							}
							else
							{
								int currentClone = list6[-clonesToSpawn - 1];
								Main.npc[currentClone].Center = cloneRotation2;
								NetMessage.SendData(23, -1, -1, null, currentClone);
							}
						}
					}
					base.NPC.ai[2] = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, 490, 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					NPC nPC7 = base.NPC;
					Vector2 center8 = nPC7.Center;
					Vector2 spinningpoint6 = spinningpoint3;
					double radians5 = (float)rotateDistance * ((float)Math.PI * 2f) / (float)newCloneAmt - (float)Math.PI / 2f;
					center = default(Vector2);
					nPC7.Center = center8 + spinningpoint6.RotatedBy(radians5, center);
					base.NPC.netUpdate = true;
					list6.Clear();
				}
				dontTakeDamage = true;
				base.NPC.alpha = 255;
				if (isCultist)
				{
					Vector2 ritualCenterDirection = Main.projectile[(int)base.NPC.ai[2]].Center;
					ritualCenterDirection -= base.NPC.Center;
					if (ritualCenterDirection == Vector2.Zero)
					{
						ritualCenterDirection = -Vector2.UnitY;
					}
					((Vector2)(ref ritualCenterDirection)).Normalize();
					if (Math.Abs(ritualCenterDirection.Y) < 0.77f)
					{
						base.NPC.localAI[2] = 11f;
					}
					else if (ritualCenterDirection.Y < 0f)
					{
						base.NPC.localAI[2] = 12f;
					}
					else
					{
						base.NPC.localAI[2] = 10f;
					}
					int ritualFaceDirection = Math.Sign(ritualCenterDirection.X);
					if (ritualFaceDirection != 0)
					{
						base.NPC.direction = (base.NPC.spriteDirection = ritualFaceDirection);
					}
				}
				else
				{
					Vector2 ritualCenterFailDirection = Main.projectile[(int)Main.npc[(int)base.NPC.ai[3]].ai[2]].Center;
					ritualCenterFailDirection -= base.NPC.Center;
					if (ritualCenterFailDirection == Vector2.Zero)
					{
						ritualCenterFailDirection = -Vector2.UnitY;
					}
					((Vector2)(ref ritualCenterFailDirection)).Normalize();
					if (Math.Abs(ritualCenterFailDirection.Y) < 0.77f)
					{
						base.NPC.localAI[2] = 11f;
					}
					else if (ritualCenterFailDirection.Y < 0f)
					{
						base.NPC.localAI[2] = 12f;
					}
					else
					{
						base.NPC.localAI[2] = 10f;
					}
					int ritualFailFaceDirection = Math.Sign(ritualCenterFailDirection.X);
					if (ritualFailFaceDirection != 0)
					{
						base.NPC.direction = (base.NPC.spriteDirection = ritualFailFaceDirection);
					}
				}
			}
			else if (base.NPC.ai[1] >= 90f && base.NPC.ai[1] < 120f)
			{
				dontTakeDamage = true;
				float ritualAlphaControl = (base.NPC.ai[1] - 90f) / 30f;
				base.NPC.alpha = 255 - (int)(ritualAlphaControl * 255f);
			}
			else if (base.NPC.ai[1] >= 120f && base.NPC.ai[1] < timeToFinishRitual)
			{
				base.NPC.alpha = 0;
				if (isCultist)
				{
					Vector2 ritualTimeAlmostUpCenterDirection = Main.projectile[(int)base.NPC.ai[2]].Center;
					ritualTimeAlmostUpCenterDirection -= base.NPC.Center;
					if (ritualTimeAlmostUpCenterDirection == Vector2.Zero)
					{
						ritualTimeAlmostUpCenterDirection = -Vector2.UnitY;
					}
					((Vector2)(ref ritualTimeAlmostUpCenterDirection)).Normalize();
					if (Math.Abs(ritualTimeAlmostUpCenterDirection.Y) < 0.77f)
					{
						base.NPC.localAI[2] = 11f;
					}
					else if (ritualTimeAlmostUpCenterDirection.Y < 0f)
					{
						base.NPC.localAI[2] = 12f;
					}
					else
					{
						base.NPC.localAI[2] = 10f;
					}
					int ritualTimeAlmostUpFaceDirection = Math.Sign(ritualTimeAlmostUpCenterDirection.X);
					if (ritualTimeAlmostUpFaceDirection != 0)
					{
						base.NPC.direction = (base.NPC.spriteDirection = ritualTimeAlmostUpFaceDirection);
					}
				}
				else
				{
					Vector2 ritualTimeUpCenterDirection = Main.projectile[(int)Main.npc[(int)base.NPC.ai[3]].ai[2]].Center;
					ritualTimeUpCenterDirection -= base.NPC.Center;
					if (ritualTimeUpCenterDirection == Vector2.Zero)
					{
						ritualTimeUpCenterDirection = -Vector2.UnitY;
					}
					((Vector2)(ref ritualTimeUpCenterDirection)).Normalize();
					if (Math.Abs(ritualTimeUpCenterDirection.Y) < 0.77f)
					{
						base.NPC.localAI[2] = 11f;
					}
					else if (ritualTimeUpCenterDirection.Y < 0f)
					{
						base.NPC.localAI[2] = 12f;
					}
					else
					{
						base.NPC.localAI[2] = 10f;
					}
					int ritualTimeUpFaceDirection = Math.Sign(ritualTimeUpCenterDirection.X);
					if (ritualTimeUpFaceDirection != 0)
					{
						base.NPC.direction = (base.NPC.spriteDirection = ritualTimeUpFaceDirection);
					}
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= timeToFinishRitual)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				CalamityTargetingParameters options4 = CalamityTargetingParameters.BossDefaults;
				options4.faceTarget = false;
				base.NPC.CalamityTargeting(options4);
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 6f)
		{
			base.NPC.localAI[2] = 13f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (float)(idleTime * 3))
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 7f)
		{
			base.NPC.localAI[2] = 11f;
			Vector2 playerDirection3 = Vector2.Normalize(player.Center - base.NPC.Center);
			if (playerDirection3.HasNaNs())
			{
				((Vector2)(ref playerDirection3))._002Ector((float)base.NPC.direction, 0f);
			}
			if (((base.NPC.ai[1] >= 4f) & isCultist) && (int)(base.NPC.ai[1] - 4f) % ancientLightSpawnRate == 0 && (int)(base.NPC.ai[1] - 4f) / ancientLightSpawnRate <= (death ? 4 : 3))
			{
				if ((int)(base.NPC.ai[1] - 4f) / ancientLightSpawnRate == 2)
				{
					List<int> list7 = new List<int>();
					for (int num6 = 0; num6 < Main.maxNPCs; num6++)
					{
						if (Main.npc[num6].active && Main.npc[num6].type == 440 && Main.npc[num6].ai[3] == (float)base.NPC.whoAmI)
						{
							list7.Add(num6);
						}
					}
					foreach (int current6 in list7)
					{
						NPC nPC8 = Main.npc[current6];
						Vector2 center9 = nPC8.Center;
						int cloneFaceDirection = Math.Sign(player.Center.X - center9.X);
						if (cloneFaceDirection != 0)
						{
							nPC8.direction = (nPC8.spriteDirection = cloneFaceDirection);
						}
					}
				}
				int cultistFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
				if (cultistFaceDirection != 0)
				{
					base.NPC.direction = (base.NPC.spriteDirection = cultistFaceDirection);
				}
				if (Main.netMode != 1)
				{
					playerDirection3 = Vector2.Normalize(player.Center - base.NPC.Center);
					if (playerDirection3.HasNaNs())
					{
						((Vector2)(ref playerDirection3))._002Ector((float)base.NPC.direction, 0f);
					}
					Vector2 ancientLightShootDirection = base.NPC.Center;
					float scaleFactor = (death ? 5f : 4f);
					float ancientLightSpread = MathHelper.ToRadians(CalamityWorld.death ? 90f : 80f);
					float totalAncientLights = (death ? 9 : 7);
					float adjustTotal = totalAncientLights - 1f;
					for (int num7 = 0; (float)num7 < totalAncientLights; num7++)
					{
						float shotgunscalar = Math.Abs((float)num7 - adjustTotal * 0.5f) / (adjustTotal * 0.5f);
						float angleOffset = ancientLightSpread * ((float)num7 / adjustTotal) - ancientLightSpread * 0.5f;
						Vector2 spinningpoint7 = playerDirection3;
						double radians6 = angleOffset;
						center = default(Vector2);
						Vector2 ancientLightSpeed = spinningpoint7.RotatedBy(radians6, center) * scaleFactor * (0.75f + 1.5f * shotgunscalar);
						float ai = (Main.rand.NextFloat() - 0.5f) * 0.2f * ((float)Math.PI * 2f) / 60f;
						int ancientLightProj = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)ancientLightShootDirection.X, (int)ancientLightShootDirection.Y + 7, 522, 0, 0f, ai, ancientLightSpeed.X, ancientLightSpeed.Y);
						Main.npc[ancientLightProj].velocity = ancientLightSpeed;
					}
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (float)(30 + ancientLightSpawnRate * ancientLightAmt))
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 8f)
		{
			base.NPC.localAI[2] = 13f;
			if (((base.NPC.ai[1] >= 4f) & isCultist) && (float)(int)(base.NPC.ai[1] - 4f) % 20f == 0f && base.NPC.ai[1] <= 64f)
			{
				List<int> list8 = new List<int>();
				for (int num8 = 0; num8 < Main.maxNPCs; num8++)
				{
					if (Main.npc[num8].active && Main.npc[num8].type == 440 && Main.npc[num8].ai[3] == (float)base.NPC.whoAmI)
					{
						list8.Add(num8);
					}
				}
				int ancientDoomAmount = 2;
				if ((lifeRatio < 0.75f) & death)
				{
					ancientDoomAmount++;
				}
				if (lifeRatio < 0.5f)
				{
					ancientDoomAmount++;
				}
				if ((lifeRatio < 0.25f) & death)
				{
					ancientDoomAmount++;
				}
				int ancientDoomFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
				if (ancientDoomFaceDirection != 0)
				{
					base.NPC.direction = (base.NPC.spriteDirection = ancientDoomFaceDirection);
				}
				if (Main.netMode != 1)
				{
					for (int num9 = 0; num9 < ancientDoomAmount; num9++)
					{
						float ai2 = (float)num9 * (360f / (float)ancientDoomAmount);
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.Center.X + (float)(Math.Sin(num9 * 120) * 550.0)), (int)(player.Center.Y + (float)(Math.Cos(num9 * 120) * 550.0)), 523, 0, base.NPC.whoAmI, 0f, ai2);
					}
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 195f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[3]++;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
		}
		if (!isCultist)
		{
			base.NPC.ai[3] = clonePhase;
		}
		base.NPC.dontTakeDamage = dontTakeDamage;
		base.NPC.chaseable = base.NPC.ai[0] != -1f && base.NPC.ai[0] != 5f;
		return false;
	}
}
