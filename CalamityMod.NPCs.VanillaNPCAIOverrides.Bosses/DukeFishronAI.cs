using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class DukeFishronAI : VanillaAIOverride
{
	public class DetonatingBubbleAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_0426: Unknown result type (might be due to invalid IL or missing references)
			//IL_042b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0370: Unknown result type (might be due to invalid IL or missing references)
			//IL_037b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0380: Unknown result type (might be due to invalid IL or missing references)
			//IL_0385: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_03af: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05de: Unknown result type (might be due to invalid IL or missing references)
			//IL_0644: Unknown result type (might be due to invalid IL or missing references)
			//IL_0649: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
			bool driftUpward = base.NPC.ai[1] < 0f;
			base.NPC.damage = ((!driftUpward) ? base.NPC.defDamage : 0);
			if (driftUpward)
			{
				base.NPC.ai[1]++;
				if (base.NPC.velocity.Y > -2f)
				{
					base.NPC.velocity.Y -= 0.04f;
				}
				return false;
			}
			if (base.NPC.target == 255)
			{
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
				base.NPC.ai[3] = (float)Main.rand.Next(100, 151) / 100f;
				float startingVelocity = (float)Main.rand.Next(250, 351) / 15f;
				base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center + new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101))).SafeNormalize(Vector2.UnitY) * startingVelocity;
				base.NPC.netUpdate = true;
			}
			bool pop = base.NPC.ai[0] == 1f;
			Vector2 velocityVector = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
			float inertia = 30f;
			float velocity = 20f;
			base.NPC.velocity = (base.NPC.velocity * inertia + velocityVector * velocity) / (inertia + 1f);
			base.NPC.scale = base.NPC.ai[3];
			base.NPC.alpha -= 30;
			if (base.NPC.alpha < 50)
			{
				base.NPC.alpha = 50;
			}
			base.NPC.alpha = 50;
			float inertia2 = inertia + 10f;
			base.NPC.velocity.X = (base.NPC.velocity.X * inertia2 + (float)Main.rand.Next(-10, 11) * 0.1f) / (inertia2 + 1f);
			base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia2 + -0.25f + (float)Main.rand.Next(-10, 11) * 0.2f) / (inertia2 + 1f);
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y -= 0.04f;
			}
			float spreadOutStrength = ((CalamityWorld.death || BossRushEvent.BossRushActive) ? (-0.08f) : (-0.06f));
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (i != base.NPC.whoAmI && Main.npc[i].active && Main.npc[i].type == base.NPC.type)
				{
					Vector2 otherBubbleDist = Main.npc[i].Center - base.NPC.Center;
					if (((Vector2)(ref otherBubbleDist)).Length() < (float)(base.NPC.width + base.NPC.height))
					{
						otherBubbleDist = otherBubbleDist.SafeNormalize(Vector2.UnitY);
						otherBubbleDist *= spreadOutStrength;
						NPC nPC = base.NPC;
						nPC.velocity += otherBubbleDist;
						NPC obj = Main.npc[i];
						obj.velocity -= otherBubbleDist;
					}
				}
			}
			if (base.NPC.ai[0] == 0f)
			{
				int size = 40;
				Rectangle rect = base.NPC.getRect();
				rect.X -= size + base.NPC.width / 2;
				rect.Y -= size + base.NPC.height / 2;
				rect.Width += size * 2;
				rect.Height += size * 2;
				for (int j = 0; j < 255; j++)
				{
					Player player = Main.player[j];
					if (player.active && !player.dead && ((Rectangle)(ref rect)).Intersects(player.getRect()))
					{
						base.NPC.ai[0] = 1f;
						base.NPC.ai[1] = 4f;
						base.NPC.netUpdate = true;
						break;
					}
				}
			}
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[1]++;
				float timeBeforePopping = 300f;
				if (base.NPC.ai[1] >= timeBeforePopping)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 4f;
				}
			}
			if (pop)
			{
				base.NPC.ai[1]--;
				if (base.NPC.ai[1] <= 0f)
				{
					base.NPC.life = 0;
					base.NPC.HitEffect();
					base.NPC.active = false;
					return false;
				}
			}
			if (pop)
			{
				base.NPC.position = base.NPC.Center;
				base.NPC.width = (base.NPC.height = 100);
				base.NPC.position = new Vector2(base.NPC.position.X - (float)(base.NPC.width / 2), base.NPC.position.Y - (float)(base.NPC.height / 2));
				base.NPC.EncourageDespawn(3);
			}
			return false;
		}
	}

	public static float Phase2ContactDamageMult = 1.436f;

	public static float Phase3ContactDamageMult = 1.315f;

	public override bool AI(Mod mod)
	{
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_1074: Unknown result type (might be due to invalid IL or missing references)
		//IL_107e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1083: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_154d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_155c: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1304: Unknown result type (might be due to invalid IL or missing references)
		//IL_1309: Unknown result type (might be due to invalid IL or missing references)
		//IL_1314: Unknown result type (might be due to invalid IL or missing references)
		//IL_1319: Unknown result type (might be due to invalid IL or missing references)
		//IL_131e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1325: Unknown result type (might be due to invalid IL or missing references)
		//IL_132a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1332: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_109e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1102: Unknown result type (might be due to invalid IL or missing references)
		//IL_1107: Unknown result type (might be due to invalid IL or missing references)
		//IL_1127: Unknown result type (might be due to invalid IL or missing references)
		//IL_1139: Unknown result type (might be due to invalid IL or missing references)
		//IL_113e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1140: Unknown result type (might be due to invalid IL or missing references)
		//IL_1142: Unknown result type (might be due to invalid IL or missing references)
		//IL_114e: Unknown result type (might be due to invalid IL or missing references)
		//IL_115b: Unknown result type (might be due to invalid IL or missing references)
		//IL_116c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1172: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11af: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1800: Unknown result type (might be due to invalid IL or missing references)
		//IL_180a: Unknown result type (might be due to invalid IL or missing references)
		//IL_180f: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_135a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1863: Unknown result type (might be due to invalid IL or missing references)
		//IL_186e: Unknown result type (might be due to invalid IL or missing references)
		//IL_143f: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1391: Unknown result type (might be due to invalid IL or missing references)
		//IL_139c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1605: Unknown result type (might be due to invalid IL or missing references)
		//IL_160a: Unknown result type (might be due to invalid IL or missing references)
		//IL_161e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1628: Unknown result type (might be due to invalid IL or missing references)
		//IL_1633: Unknown result type (might be due to invalid IL or missing references)
		//IL_1638: Unknown result type (might be due to invalid IL or missing references)
		//IL_163d: Unknown result type (might be due to invalid IL or missing references)
		//IL_13af: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1405: Unknown result type (might be due to invalid IL or missing references)
		//IL_140d: Unknown result type (might be due to invalid IL or missing references)
		//IL_196a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1981: Unknown result type (might be due to invalid IL or missing references)
		//IL_1986: Unknown result type (might be due to invalid IL or missing references)
		//IL_1991: Unknown result type (might be due to invalid IL or missing references)
		//IL_1996: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_19bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_19da: Unknown result type (might be due to invalid IL or missing references)
		//IL_194a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1951: Unknown result type (might be due to invalid IL or missing references)
		//IL_1956: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2007: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e14: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e84: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_2349: Unknown result type (might be due to invalid IL or missing references)
		//IL_2354: Unknown result type (might be due to invalid IL or missing references)
		//IL_2206: Unknown result type (might be due to invalid IL or missing references)
		//IL_221f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2225: Unknown result type (might be due to invalid IL or missing references)
		//IL_2227: Unknown result type (might be due to invalid IL or missing references)
		//IL_222c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2033: Unknown result type (might be due to invalid IL or missing references)
		//IL_203e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2472: Unknown result type (might be due to invalid IL or missing references)
		//IL_2055: Unknown result type (might be due to invalid IL or missing references)
		//IL_205a: Unknown result type (might be due to invalid IL or missing references)
		//IL_206e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2078: Unknown result type (might be due to invalid IL or missing references)
		//IL_2083: Unknown result type (might be due to invalid IL or missing references)
		//IL_2088: Unknown result type (might be due to invalid IL or missing references)
		//IL_208d: Unknown result type (might be due to invalid IL or missing references)
		//IL_209b: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_20fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2119: Unknown result type (might be due to invalid IL or missing references)
		//IL_211f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2121: Unknown result type (might be due to invalid IL or missing references)
		//IL_2128: Unknown result type (might be due to invalid IL or missing references)
		//IL_1677: Unknown result type (might be due to invalid IL or missing references)
		//IL_167e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_254a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2554: Unknown result type (might be due to invalid IL or missing references)
		//IL_2559: Unknown result type (might be due to invalid IL or missing references)
		//IL_2389: Unknown result type (might be due to invalid IL or missing references)
		//IL_238e: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_214b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2150: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_26fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2713: Unknown result type (might be due to invalid IL or missing references)
		//IL_2718: Unknown result type (might be due to invalid IL or missing references)
		//IL_2723: Unknown result type (might be due to invalid IL or missing references)
		//IL_2728: Unknown result type (might be due to invalid IL or missing references)
		//IL_2733: Unknown result type (might be due to invalid IL or missing references)
		//IL_2738: Unknown result type (might be due to invalid IL or missing references)
		//IL_273d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2744: Unknown result type (might be due to invalid IL or missing references)
		//IL_2749: Unknown result type (might be due to invalid IL or missing references)
		//IL_2751: Unknown result type (might be due to invalid IL or missing references)
		//IL_275c: Unknown result type (might be due to invalid IL or missing references)
		//IL_276c: Unknown result type (might be due to invalid IL or missing references)
		//IL_26dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c29: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c39: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1735: Unknown result type (might be due to invalid IL or missing references)
		//IL_173c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e53: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e27: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_295b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2966: Unknown result type (might be due to invalid IL or missing references)
		//IL_296b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2970: Unknown result type (might be due to invalid IL or missing references)
		//IL_2977: Unknown result type (might be due to invalid IL or missing references)
		//IL_297c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ee3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ee8: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool phase2 = num < (death ? 0.6f : 0.7f);
		bool phase3 = num < (death ? 0.3f : 0.4f);
		bool phase4 = num < (death ? 0.1f : 0.2f);
		bool phase2AI = base.NPC.ai[0] > 4f;
		bool num2 = base.NPC.ai[0] > 9f;
		bool charging = base.NPC.ai[3] < 10f;
		base.NPC.damage = base.NPC.defDamage;
		if (num2)
		{
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * Phase3ContactDamageMult);
			base.NPC.defense = 0;
		}
		else if (phase2AI)
		{
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * Phase2ContactDamageMult);
			base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 0.8);
		}
		else
		{
			base.NPC.defense = base.NPC.defDefense;
		}
		int idlePhaseTimer = 30;
		float idlePhaseAcceleration = 0.55f;
		float idlePhaseVelocity = 8.5f;
		if (num2)
		{
			idlePhaseAcceleration = 0.7f;
			idlePhaseVelocity = 12f;
		}
		else if (phase2AI & charging)
		{
			idlePhaseAcceleration = 0.6f;
			idlePhaseVelocity = 10f;
		}
		if (Main.getGoodWorld)
		{
			idlePhaseAcceleration *= 1.15f;
			idlePhaseVelocity *= 1.15f;
		}
		int chargeTime = 28;
		float chargeVelocity = 17f;
		if (num2)
		{
			chargeTime = 25;
			chargeVelocity = 27f;
		}
		else if (charging & phase2AI)
		{
			chargeTime = 27;
			chargeVelocity = 21f;
		}
		if (death)
		{
			idlePhaseTimer = 28;
			idlePhaseAcceleration *= 1.05f;
			idlePhaseVelocity *= 1.08f;
			chargeTime--;
			chargeVelocity *= 1.1f;
		}
		if (Main.getGoodWorld)
		{
			chargeVelocity *= 1.15f;
		}
		int bubbleBelchPhaseTimer = (death ? 60 : 80);
		int bubbleBelchPhaseDivisor = (death ? 3 : 4);
		float bubbleBelchPhaseAcceleration = (death ? 0.35f : 0.3f);
		float bubbleBelchPhaseVelocity = (death ? 5.5f : 5f);
		if (Main.getGoodWorld)
		{
			bubbleBelchPhaseAcceleration *= 1.5f;
			bubbleBelchPhaseVelocity *= 1.5f;
		}
		int sharknadoPhaseTimer = 90;
		int phaseTransitionTimer = 180;
		int teleportPhaseTimer = 30;
		int bubbleSpinPhaseTimer = (death ? 90 : 120);
		int bubbleSpinPhaseDivisor = (death ? 3 : 4);
		float bubbleSpinBubbleVelocity = (death ? 8f : 7f);
		float bubbleSpinPhaseVelocity = 20f;
		float bubbleSpinPhaseRotation = (float)Math.PI * 2f / (float)(bubbleSpinPhaseTimer / 2);
		if (Main.getGoodWorld)
		{
			bubbleSpinBubbleVelocity *= 1.5f;
		}
		int spawnEffectPhaseTimer = 75;
		Player player = Main.player[base.NPC.target];
		if (base.NPC.target < 0 || base.NPC.target == 255 || player.dead || !player.active || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			player = Main.player[base.NPC.target];
			base.NPC.netUpdate = true;
		}
		if (player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
		{
			base.NPC.velocity.Y -= 0.4f;
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
			if (base.NPC.ai[0] > 4f)
			{
				base.NPC.ai[0] = 5f;
			}
			else
			{
				base.NPC.ai[0] = 0f;
			}
			base.NPC.ai[2] = 0f;
		}
		bool enrage = (calamityGlobalNPC.CurrentlyEnraged = !BossRushEvent.BossRushActive && (player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400))));
		base.NPC.dontTakeDamage = false;
		calamityGlobalNPC.DR = ((base.NPC.ai[0] == -1f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f) ? 0.625f : 0.15f);
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = base.NPC.ai[0] == -1f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f;
		if (enrage)
		{
			bubbleBelchPhaseTimer = 20;
			bubbleBelchPhaseDivisor = 1;
			bubbleBelchPhaseAcceleration = 0.65f;
			bubbleBelchPhaseVelocity = 10f;
			idlePhaseTimer = 20;
			idlePhaseAcceleration = 1f;
			idlePhaseVelocity = 15f;
			chargeTime = 24;
			chargeVelocity += 5f;
			bubbleSpinPhaseDivisor = 1;
			bubbleSpinBubbleVelocity = 15f;
			base.NPC.damage *= 2;
			base.NPC.defense = base.NPC.defDefense * 3;
		}
		if (death)
		{
			chargeTime -= 2;
			chargeVelocity++;
		}
		if (Main.getGoodWorld)
		{
			chargeTime += Main.rand.Next(5, 66);
		}
		if (num2 && (!phase4 || Main.getGoodWorld))
		{
			calamityGlobalNPC.newAI[0]++;
			float timeGateValue = 600f;
			if (calamityGlobalNPC.newAI[0] >= timeGateValue)
			{
				calamityGlobalNPC.newAI[0] = 0f;
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, 385, 0, 0f, Main.myPlayer, 1f, base.NPC.target + 1, (enrage | death) ? 1 : 0);
				}
				base.NPC.netUpdate = true;
			}
		}
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			base.NPC.alpha = 255;
			base.NPC.rotation = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.netUpdate = true;
			}
		}
		float rateOfRotation = 0.04f;
		if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 6f || base.NPC.ai[0] == 7f)
		{
			rateOfRotation = 0f;
		}
		if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 8f)
		{
			rateOfRotation = 0.01f;
		}
		Vector2 rotationVector = player.Center - base.NPC.Center;
		float rotationSpeed = (float)Math.Atan2(rotationVector.Y, rotationVector.X);
		if (base.NPC.spriteDirection == 1)
		{
			rotationSpeed += (float)Math.PI;
		}
		if (rotationSpeed < 0f)
		{
			rotationSpeed += (float)Math.PI * 2f;
		}
		if (rotationSpeed > (float)Math.PI * 2f)
		{
			rotationSpeed -= (float)Math.PI * 2f;
		}
		if (base.NPC.ai[0] == -1f || base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 8f)
		{
			rotationSpeed = 0f;
		}
		if (rateOfRotation != 0f)
		{
			base.NPC.rotation = base.NPC.rotation.AngleTowards(rotationSpeed, rateOfRotation);
		}
		if (base.NPC.ai[0] != -1f && base.NPC.ai[0] < 9f)
		{
			if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.alpha += 15;
			}
			else
			{
				base.NPC.alpha -= 15;
			}
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			if (base.NPC.alpha > 150)
			{
				base.NPC.alpha = 150;
			}
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.damage = 0;
			NPC nPC = base.NPC;
			nPC.velocity *= 0.98f;
			int faceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (faceDirection != 0)
			{
				base.NPC.direction = faceDirection;
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			if (base.NPC.ai[2] > 20f)
			{
				base.NPC.velocity.Y = -2f;
				base.NPC.alpha -= 5;
				if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.alpha += 15;
				}
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
				if (base.NPC.alpha > 150)
				{
					base.NPC.alpha = 150;
				}
			}
			if (base.NPC.ai[2] == (float)(sharknadoPhaseTimer - 30))
			{
				int dustAmt = 36;
				for (int i = 0; i < dustAmt; i++)
				{
					Vector2 val = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)base.NPC.width / 2f, (float)base.NPC.height) * 0.75f * 0.5f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.NPC.Center;
					Vector2 sharknadoDustDirection = val - base.NPC.Center;
					int sharknadoDust = Dust.NewDust(val + sharknadoDustDirection, 0, 0, 172, sharknadoDustDirection.X * 2f, sharknadoDustDirection.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[sharknadoDust].noGravity = true;
					Main.dust[sharknadoDust].noLight = true;
					Main.dust[sharknadoDust].velocity = Vector2.Normalize(sharknadoDustDirection) * 3f;
				}
				SoundEngine.PlaySound(in SoundID.Zombie20, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)spawnEffectPhaseTimer)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 0f && !player.dead)
		{
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 300 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 idlePhaseDirection = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -200f) - base.NPC.Center - base.NPC.velocity) * idlePhaseVelocity;
			base.NPC.SimpleFlyMovement(idlePhaseDirection, idlePhaseAcceleration);
			int playerFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (playerFaceDirection != 0)
			{
				if (base.NPC.ai[2] == 0f && playerFaceDirection != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.direction = playerFaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)idlePhaseTimer || Main.zenithWorld)
			{
				int attackPicker = 0;
				switch ((int)base.NPC.ai[3])
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					attackPicker = 1;
					break;
				case 10:
					base.NPC.ai[3] = 1f;
					attackPicker = 2;
					break;
				case 11:
					base.NPC.ai[3] = 0f;
					attackPicker = 3;
					break;
				}
				if (enrage && attackPicker == 2)
				{
					attackPicker = 3;
				}
				if (phase2)
				{
					attackPicker = 4;
				}
				switch (attackPicker)
				{
				case 1:
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeVelocity;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (playerFaceDirection != 0)
					{
						base.NPC.direction = playerFaceDirection;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
					break;
				case 2:
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					break;
				case 3:
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					if (enrage)
					{
						base.NPC.ai[2] = sharknadoPhaseTimer - 40;
					}
					else if (death)
					{
						base.NPC.ai[2] = sharknadoPhaseTimer - 40;
					}
					break;
				case 4:
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					break;
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 1.01f;
			int chargeDustAmt = 7;
			for (int j = 0; j < chargeDustAmt; j++)
			{
				Vector2 val2 = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f).RotatedBy((float)(j - (chargeDustAmt / 2 - 1)) * (float)Math.PI / (float)chargeDustAmt) + base.NPC.Center;
				Vector2 chargeDustDirection = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
				int chargeDust = Dust.NewDust(val2 + chargeDustDirection, 0, 0, 172, chargeDustDirection.X * 2f, chargeDustDirection.Y * 2f, 100, default(Color), 1.4f);
				Main.dust[chargeDust].noGravity = true;
				Main.dust[chargeDust].noLight = true;
				Dust obj = Main.dust[chargeDust];
				obj.velocity /= 4f;
				Dust obj2 = Main.dust[chargeDust];
				obj2.velocity -= base.NPC.velocity;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 300 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 bubbleAttackDirection = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -200f) - base.NPC.Center - base.NPC.velocity) * bubbleBelchPhaseVelocity;
			base.NPC.SimpleFlyMovement(bubbleAttackDirection, bubbleBelchPhaseAcceleration);
			if (base.NPC.ai[2] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Zombie20, base.NPC.Center);
			}
			if (base.NPC.ai[2] % (float)bubbleBelchPhaseDivisor == 0f)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath19, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Vector2 bubbleSpawnDirection = Vector2.Normalize(player.Center - base.NPC.Center) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)bubbleSpawnDirection.X, (int)bubbleSpawnDirection.Y + 45, 371);
				}
			}
			int bubbleSpriteFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (bubbleSpriteFaceDirection != 0)
			{
				base.NPC.direction = bubbleSpriteFaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)bubbleBelchPhaseTimer)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(sharknadoPhaseTimer - 30))
			{
				SoundEngine.PlaySound(in SoundID.Zombie9, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == (float)(sharknadoPhaseTimer - 30))
			{
				Vector2 sharknadoSpawnerDirection = base.NPC.rotation.ToRotationVector2() * (Vector2.UnitX * (float)base.NPC.direction) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
				bool num3 = Main.rand.NextBool();
				float velocityY = (num3 ? 8f : (-4f));
				float ai1 = (num3 ? 0f : (-1f));
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), sharknadoSpawnerDirection.X, sharknadoSpawnerDirection.Y, base.NPC.direction * 3, velocityY, 385, 0, 0f, Main.myPlayer, 0f, ai1);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), sharknadoSpawnerDirection.X, sharknadoSpawnerDirection.Y, (0f - (float)base.NPC.direction) * 3f, velocityY, 385, 0, 0f, Main.myPlayer, 0f, ai1);
				velocityY = (num3 ? (-4f) : 8f);
				ai1 = (num3 ? (-1f) : 0f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), sharknadoSpawnerDirection.X, sharknadoSpawnerDirection.Y, 0f, velocityY, 385, 0, 0f, Main.myPlayer, 0f, ai1);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)sharknadoPhaseTimer)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			NPC nPC4 = base.NPC;
			nPC4.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(phaseTransitionTimer - 60))
			{
				SoundEngine.PlaySound(in SoundID.Zombie20, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)phaseTransitionTimer)
			{
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 5f && !player.dead)
		{
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 300 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 phase2IdleDirection = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -200f) - base.NPC.Center - base.NPC.velocity) * idlePhaseVelocity;
			base.NPC.SimpleFlyMovement(phase2IdleDirection, idlePhaseAcceleration);
			int phase2SpriteFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (phase2SpriteFaceDirection != 0)
			{
				if (base.NPC.ai[2] == 0f && phase2SpriteFaceDirection != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.direction = phase2SpriteFaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)idlePhaseTimer || Main.zenithWorld)
			{
				int phase2AttackPicker = 0;
				switch ((int)base.NPC.ai[3])
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					phase2AttackPicker = 1;
					break;
				case 6:
					base.NPC.ai[3] = 1f;
					phase2AttackPicker = 2;
					break;
				case 7:
					base.NPC.ai[3] = 0f;
					phase2AttackPicker = 3;
					break;
				}
				if (enrage && phase2AttackPicker == 2)
				{
					phase2AttackPicker = 3;
				}
				if (phase3)
				{
					phase2AttackPicker = 4;
				}
				switch (phase2AttackPicker)
				{
				case 1:
					base.NPC.ai[0] = 6f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeVelocity;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (phase2SpriteFaceDirection != 0)
					{
						base.NPC.direction = phase2SpriteFaceDirection;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
					break;
				case 2:
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * bubbleSpinPhaseVelocity;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (phase2SpriteFaceDirection != 0)
					{
						base.NPC.direction = phase2SpriteFaceDirection;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
					base.NPC.ai[0] = 7f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					break;
				case 3:
					base.NPC.ai[0] = 8f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					break;
				case 4:
					base.NPC.ai[0] = 9f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					break;
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 6f)
		{
			NPC nPC5 = base.NPC;
			nPC5.velocity *= 1.01f;
			int phase2ChargeDustAmt = 7;
			for (int k = 0; k < phase2ChargeDustAmt; k++)
			{
				Vector2 val3 = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f).RotatedBy((float)(k - (phase2ChargeDustAmt / 2 - 1)) * (float)Math.PI / (float)phase2ChargeDustAmt) + base.NPC.Center;
				Vector2 phase2ChargeDustDirection = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
				int phase2ChargeDust = Dust.NewDust(val3 + phase2ChargeDustDirection, 0, 0, 172, phase2ChargeDustDirection.X * 2f, phase2ChargeDustDirection.Y * 2f, 100, default(Color), 1.4f);
				Main.dust[phase2ChargeDust].noGravity = true;
				Main.dust[phase2ChargeDust].noLight = true;
				Dust obj3 = Main.dust[phase2ChargeDust];
				obj3.velocity /= 4f;
				Dust obj4 = Main.dust[phase2ChargeDust];
				obj4.velocity -= base.NPC.velocity;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 7f)
		{
			if (base.NPC.ai[2] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Zombie20, base.NPC.Center);
			}
			if (base.NPC.ai[2] % (float)bubbleSpinPhaseDivisor == 0f)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath19, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Vector2 phase2BubbleSharkronDirection = Vector2.Normalize(base.NPC.velocity) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
					int phase2Bubbles = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)phase2BubbleSharkronDirection.X, (int)phase2BubbleSharkronDirection.Y + 45, 371);
					Main.npc[phase2Bubbles].target = base.NPC.target;
					Main.npc[phase2Bubbles].velocity = Vector2.Normalize(base.NPC.velocity).RotatedBy((float)Math.PI / 2f * (float)base.NPC.direction) * bubbleSpinBubbleVelocity * (Main.getGoodWorld ? (Main.rand.NextFloat() + 0.5f) : 1f);
					Main.npc[phase2Bubbles].netUpdate = true;
					Main.npc[phase2Bubbles].ai[3] = (float)Main.rand.Next(80, 121) / 100f;
					if (base.NPC.ai[2] % (float)(bubbleSpinPhaseDivisor * 5) == 0f)
					{
						int phase2BubbleSharkrons = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)phase2BubbleSharkronDirection.X, (int)phase2BubbleSharkronDirection.Y + 45, 373);
						Main.npc[phase2BubbleSharkrons].ai[1] = 89f;
					}
				}
			}
			base.NPC.velocity = base.NPC.velocity.RotatedBy((0.0 - (double)bubbleSpinPhaseRotation) * (double)(float)base.NPC.direction);
			base.NPC.rotation -= bubbleSpinPhaseRotation * (float)base.NPC.direction;
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)bubbleSpinPhaseTimer)
			{
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 8f)
		{
			NPC nPC6 = base.NPC;
			nPC6.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(sharknadoPhaseTimer - 30))
			{
				SoundEngine.PlaySound(in SoundID.Zombie20, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == (float)(sharknadoPhaseTimer - 30))
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, 385, 0, 0f, Main.myPlayer, 1f, base.NPC.target + 1, (enrage | death) ? 1 : 0);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)sharknadoPhaseTimer)
			{
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 9f)
		{
			if (base.NPC.ai[2] < (float)(phaseTransitionTimer - 90))
			{
				if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.alpha += 15;
				}
				else
				{
					base.NPC.alpha -= 15;
				}
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
				if (base.NPC.alpha > 150)
				{
					base.NPC.alpha = 150;
				}
			}
			else if (base.NPC.alpha < 255)
			{
				base.NPC.alpha += 4;
				if (base.NPC.alpha > 255)
				{
					base.NPC.alpha = 255;
				}
			}
			NPC nPC7 = base.NPC;
			nPC7.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(phaseTransitionTimer - 60))
			{
				SoundEngine.PlaySound(in SoundID.Zombie20, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)phaseTransitionTimer)
			{
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 10f && !player.dead)
		{
			if (base.NPC.alpha < 255)
			{
				base.NPC.alpha += 25;
				if (base.NPC.alpha > 255)
				{
					base.NPC.alpha = 255;
				}
			}
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 360 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 desiredVelocity = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -200f) - base.NPC.Center - base.NPC.velocity) * idlePhaseVelocity;
			base.NPC.SimpleFlyMovement(desiredVelocity, idlePhaseAcceleration);
			int phase3SpriteFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (phase3SpriteFaceDirection != 0)
			{
				if (base.NPC.ai[2] == 0f && phase3SpriteFaceDirection != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
					for (int l = 0; l < base.NPC.oldPos.Length; l++)
					{
						base.NPC.oldPos[l] = Vector2.Zero;
					}
				}
				base.NPC.direction = phase3SpriteFaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)idlePhaseTimer || Main.zenithWorld)
			{
				int phase3AttackPicker = 0;
				if (phase4)
				{
					switch ((int)base.NPC.ai[3])
					{
					case 0:
					case 1:
					case 2:
					case 4:
					case 5:
					case 6:
					case 7:
						phase3AttackPicker = 1;
						break;
					case 3:
					case 8:
						phase3AttackPicker = 2;
						break;
					}
					if (death)
					{
						phase3AttackPicker = 1;
					}
				}
				else
				{
					switch ((int)base.NPC.ai[3])
					{
					case 0:
					case 2:
					case 3:
					case 5:
					case 6:
					case 7:
						phase3AttackPicker = 1;
						break;
					case 1:
					case 4:
					case 8:
						phase3AttackPicker = 2;
						break;
					}
				}
				switch (phase3AttackPicker)
				{
				case 1:
					base.NPC.ai[0] = 11f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeVelocity;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (phase3SpriteFaceDirection != 0)
					{
						base.NPC.direction = phase3SpriteFaceDirection;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
					break;
				case 2:
					base.NPC.ai[0] = 12f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					break;
				case 3:
					base.NPC.ai[0] = -1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					break;
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 11f)
		{
			NPC nPC8 = base.NPC;
			nPC8.velocity *= 1.01f;
			base.NPC.alpha -= 25;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			int phase3ChargeDustAmt = 7;
			for (int m = 0; m < phase3ChargeDustAmt; m++)
			{
				Vector2 val4 = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f).RotatedBy((float)(m - (phase3ChargeDustAmt / 2 - 1)) * (float)Math.PI / (float)phase3ChargeDustAmt) + base.NPC.Center;
				Vector2 phase3ChargeDustDirection = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
				int phase3ChargeDust = Dust.NewDust(val4 + phase3ChargeDustDirection, 0, 0, 172, phase3ChargeDustDirection.X * 2f, phase3ChargeDustDirection.Y * 2f, 100, default(Color), 1.4f);
				Main.dust[phase3ChargeDust].noGravity = true;
				Main.dust[phase3ChargeDust].noLight = true;
				Dust obj5 = Main.dust[phase3ChargeDust];
				obj5.velocity /= 4f;
				Dust obj6 = Main.dust[phase3ChargeDust];
				obj6.velocity -= base.NPC.velocity;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				if (!phase4 || !death)
				{
					base.NPC.ai[3]++;
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 12f)
		{
			base.NPC.damage = 0;
			if (base.NPC.alpha < 255)
			{
				base.NPC.alpha += 17;
				if (base.NPC.alpha > 255)
				{
					base.NPC.alpha = 255;
				}
			}
			NPC nPC9 = base.NPC;
			nPC9.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(teleportPhaseTimer / 2))
			{
				SoundEngine.PlaySound(in SoundID.Zombie20, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == (float)(teleportPhaseTimer / 2))
			{
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.ai[1] = 300 * Math.Sign((base.NPC.Center - player.Center).X);
				}
				Vector2 center = player.Center + new Vector2(0f - base.NPC.ai[1], -200f);
				base.NPC.Center = center;
				int phase3PlayerDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
				if (phase3PlayerDirection != 0)
				{
					if (base.NPC.ai[2] == 0f && phase3PlayerDirection != base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
						for (int n = 0; n < base.NPC.oldPos.Length; n++)
						{
							base.NPC.oldPos[n] = Vector2.Zero;
						}
					}
					base.NPC.direction = phase3PlayerDirection;
					if (base.NPC.spriteDirection != -base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)teleportPhaseTimer)
			{
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3]++;
				if (base.NPC.ai[3] >= 9f)
				{
					base.NPC.ai[3] = 0f;
				}
				base.NPC.netUpdate = true;
			}
		}
		return false;
	}
}
