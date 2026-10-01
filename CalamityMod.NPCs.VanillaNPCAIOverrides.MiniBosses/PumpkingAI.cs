using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.MiniBosses;

public class PumpkingAI : VanillaAIOverride
{
	public class BladeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_021d: Unknown result type (might be due to invalid IL or missing references)
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0422: Unknown result type (might be due to invalid IL or missing references)
			//IL_0440: Unknown result type (might be due to invalid IL or missing references)
			//IL_0468: Unknown result type (might be due to invalid IL or missing references)
			//IL_047a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0496: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_081e: Unknown result type (might be due to invalid IL or missing references)
			//IL_051f: Unknown result type (might be due to invalid IL or missing references)
			//IL_053d: Unknown result type (might be due to invalid IL or missing references)
			//IL_055b: Unknown result type (might be due to invalid IL or missing references)
			//IL_056b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a99: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c40: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f84: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0602: Unknown result type (might be due to invalid IL or missing references)
			//IL_0620: Unknown result type (might be due to invalid IL or missing references)
			//IL_0650: Unknown result type (might be due to invalid IL or missing references)
			//IL_066e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.spriteDirection = -(int)base.NPC.ai[0];
			if (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != 58)
			{
				base.NPC.ai[2] += 10f;
				if (base.NPC.ai[2] > 50f || !Main.dedServ)
				{
					base.NPC.life = -1;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
			}
			if (Main.netMode != 1 && Main.npc[(int)base.NPC.ai[1]].ai[3] == 2f)
			{
				base.NPC.localAI[1]++;
				if (base.NPC.localAI[1] > 30f)
				{
					base.NPC.localAI[1] = 0f;
					Vector2 scytheProjSpawn = default(Vector2);
					((Vector2)(ref scytheProjSpawn))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f + 30f);
					float scytheProjTargetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - scytheProjSpawn.X;
					float scytheProjTargetY = Main.player[base.NPC.target].position.Y - scytheProjSpawn.Y;
					float scytheProjTargetDist = (float)Math.Sqrt(scytheProjTargetX * scytheProjTargetX + scytheProjTargetY * scytheProjTargetY);
					scytheProjTargetDist = 0.01f / scytheProjTargetDist;
					scytheProjTargetX *= scytheProjTargetDist;
					scytheProjTargetY *= scytheProjTargetDist;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, scytheProjTargetX, scytheProjTargetY, 329, 70, 0f, Main.myPlayer, base.NPC.rotation, base.NPC.spriteDirection);
				}
			}
			if (Main.dayTime)
			{
				base.NPC.velocity.Y += 0.3f;
				base.NPC.velocity.X *= 0.9f;
				return false;
			}
			if (base.NPC.ai[2] == 0f || base.NPC.ai[2] == 3f)
			{
				base.NPC.damage = 0;
				if (Main.npc[(int)base.NPC.ai[1]].ai[1] == 3f && base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				base.NPC.ai[3]++;
				if (base.NPC.ai[3] >= 180f)
				{
					base.NPC.ai[2]++;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				Vector2 scytheSwipePosition = default(Vector2);
				((Vector2)(ref scytheSwipePosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float scytheSwipeTargetX = (Main.player[base.NPC.target].Center.X + Main.npc[(int)base.NPC.ai[1]].Center.X) / 2f;
				float scytheSwipeTargetY = (Main.player[base.NPC.target].Center.Y + Main.npc[(int)base.NPC.ai[1]].Center.Y) / 2f;
				scytheSwipeTargetX += -170f * base.NPC.ai[0] - scytheSwipePosition.X;
				scytheSwipeTargetY += 90f - scytheSwipePosition.Y;
				if (Math.Abs(Main.player[base.NPC.target].Center.X - Main.npc[(int)base.NPC.ai[1]].Center.X) + Math.Abs(Main.player[base.NPC.target].Center.Y - Main.npc[(int)base.NPC.ai[1]].Center.Y) > 700f)
				{
					scytheSwipeTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - 170f * base.NPC.ai[0] - scytheSwipePosition.X;
					scytheSwipeTargetY = Main.npc[(int)base.NPC.ai[1]].Center.Y + 90f - scytheSwipePosition.Y;
				}
				float scytheSwipeTargetDist = (float)Math.Sqrt(scytheSwipeTargetX * scytheSwipeTargetX + scytheSwipeTargetY * scytheSwipeTargetY);
				float scytheSwipeSpeed = 8f;
				if (scytheSwipeTargetDist > 1000f)
				{
					scytheSwipeSpeed = 23f;
				}
				else if (scytheSwipeTargetDist > 800f)
				{
					scytheSwipeSpeed = 20f;
				}
				else if (scytheSwipeTargetDist > 600f)
				{
					scytheSwipeSpeed = 17f;
				}
				else if (scytheSwipeTargetDist > 400f)
				{
					scytheSwipeSpeed = 14f;
				}
				else if (scytheSwipeTargetDist > 200f)
				{
					scytheSwipeSpeed = 11f;
				}
				if (base.NPC.ai[0] < 0f && base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X)
				{
					scytheSwipeTargetX -= 4f;
				}
				if (base.NPC.ai[0] > 0f && base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X)
				{
					scytheSwipeTargetX += 4f;
				}
				scytheSwipeTargetDist = scytheSwipeSpeed / scytheSwipeTargetDist;
				base.NPC.velocity.X = (base.NPC.velocity.X * 14f + scytheSwipeTargetX * scytheSwipeTargetDist) / 15f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 14f + scytheSwipeTargetY * scytheSwipeTargetDist) / 15f;
				scytheSwipeTargetDist = (float)Math.Sqrt(scytheSwipeTargetX * scytheSwipeTargetX + scytheSwipeTargetY * scytheSwipeTargetY);
				if (scytheSwipeTargetDist > 20f)
				{
					base.NPC.rotation = (float)Math.Atan2(scytheSwipeTargetY, scytheSwipeTargetX) + (float)Math.PI / 2f;
				}
			}
			else if (base.NPC.ai[2] == 1f)
			{
				base.NPC.damage = 0;
				Vector2 scytheReturnPosition = default(Vector2);
				((Vector2)(ref scytheReturnPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float scytheReturnTargetX = Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) - 200f * base.NPC.ai[0] - scytheReturnPosition.X;
				float scytheReturnTargetY = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - scytheReturnPosition.Y;
				float scytheReturnTargetDist = (float)Math.Sqrt(scytheReturnTargetX * scytheReturnTargetX + scytheReturnTargetY * scytheReturnTargetY);
				base.NPC.rotation = (float)Math.Atan2(scytheReturnTargetY, scytheReturnTargetX) + (float)Math.PI / 2f;
				base.NPC.velocity.X *= 0.95f;
				base.NPC.velocity.Y -= 0.3f;
				if (base.NPC.velocity.Y < -18f)
				{
					base.NPC.velocity.Y = -18f;
				}
				if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y - 200f)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.TargetClosest();
					base.NPC.ai[2] = 2f;
					((Vector2)(ref scytheReturnPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
					scytheReturnTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - scytheReturnPosition.X;
					scytheReturnTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - scytheReturnPosition.Y;
					scytheReturnTargetDist = (float)Math.Sqrt(scytheReturnTargetX * scytheReturnTargetX + scytheReturnTargetY * scytheReturnTargetY);
					scytheReturnTargetDist = 24f / scytheReturnTargetDist;
					base.NPC.velocity.X = scytheReturnTargetX * scytheReturnTargetDist;
					base.NPC.velocity.Y = scytheReturnTargetY * scytheReturnTargetDist;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 2f)
			{
				base.NPC.damage = base.NPC.defDamage;
				float scytheReturnDestination = Math.Abs(base.NPC.Center.X - Main.npc[(int)base.NPC.ai[1]].Center.X) + Math.Abs(base.NPC.Center.Y - Main.npc[(int)base.NPC.ai[1]].Center.Y);
				if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y || base.NPC.velocity.Y < 0f || scytheReturnDestination > 800f)
				{
					base.NPC.damage = 0;
					base.NPC.ai[2] = 3f;
				}
			}
			else if (base.NPC.ai[2] == 4f)
			{
				base.NPC.damage = 0;
				Vector2 scytheLesserSwipePos = default(Vector2);
				((Vector2)(ref scytheLesserSwipePos))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float scytheLesserSwipeTargetX = Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) - 200f * base.NPC.ai[0] - scytheLesserSwipePos.X;
				float scytheLesserSwipeTargetY = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - scytheLesserSwipePos.Y;
				float scytheLesserSwipeTargetDist = (float)Math.Sqrt(scytheLesserSwipeTargetX * scytheLesserSwipeTargetX + scytheLesserSwipeTargetY * scytheLesserSwipeTargetY);
				base.NPC.rotation = (float)Math.Atan2(scytheLesserSwipeTargetY, scytheLesserSwipeTargetX) + (float)Math.PI / 2f;
				base.NPC.velocity.Y *= 0.95f;
				base.NPC.velocity.X += 0.3f * (0f - base.NPC.ai[0]);
				if (base.NPC.velocity.X < -18f)
				{
					base.NPC.velocity.X = -18f;
				}
				if (base.NPC.velocity.X > 18f)
				{
					base.NPC.velocity.X = 18f;
				}
				if (base.NPC.position.X + (float)(base.NPC.width / 2) < Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) - 500f || base.NPC.position.X + (float)(base.NPC.width / 2) > Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) + 500f)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.TargetClosest();
					base.NPC.ai[2] = 5f;
					((Vector2)(ref scytheLesserSwipePos))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
					scytheLesserSwipeTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - scytheLesserSwipePos.X;
					scytheLesserSwipeTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - scytheLesserSwipePos.Y;
					scytheLesserSwipeTargetDist = (float)Math.Sqrt(scytheLesserSwipeTargetX * scytheLesserSwipeTargetX + scytheLesserSwipeTargetY * scytheLesserSwipeTargetY);
					scytheLesserSwipeTargetDist = 17f / scytheLesserSwipeTargetDist;
					base.NPC.velocity.X = scytheLesserSwipeTargetX * scytheLesserSwipeTargetDist;
					base.NPC.velocity.Y = scytheLesserSwipeTargetY * scytheLesserSwipeTargetDist;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 5f)
			{
				base.NPC.damage = base.NPC.defDamage;
				float scytheLesserSwipeReturnDest = Math.Abs(base.NPC.Center.X - Main.npc[(int)base.NPC.ai[1]].Center.X) + Math.Abs(base.NPC.Center.Y - Main.npc[(int)base.NPC.ai[1]].Center.Y);
				if ((base.NPC.velocity.X > 0f && base.NPC.position.X + (float)(base.NPC.width / 2) > Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2)) || (base.NPC.velocity.X < 0f && base.NPC.position.X + (float)(base.NPC.width / 2) < Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2)) || scytheLesserSwipeReturnDest > 800f)
				{
					base.NPC.damage = 0;
					base.NPC.ai[2] = 0f;
				}
			}
			return false;
		}
	}

	public override bool AI(Mod mod)
	{
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.localAI[0]++;
		if (base.NPC.localAI[0] > 6f)
		{
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[1]++;
			if (base.NPC.localAI[1] > 4f)
			{
				base.NPC.localAI[1] = 0f;
			}
		}
		if (Main.netMode != 1)
		{
			base.NPC.localAI[2]++;
			if (base.NPC.localAI[2] > 300f)
			{
				base.NPC.ai[3] = Main.rand.Next(3);
				base.NPC.localAI[2] = 0f;
			}
			else if (base.NPC.ai[3] == 0f && base.NPC.localAI[2] % 30f == 0f && base.NPC.localAI[2] > 30f)
			{
				float greekFireSpeed = 10f;
				Vector2 greekFireSpawnPos = default(Vector2);
				((Vector2)(ref greekFireSpawnPos))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f + 30f);
				if (!WorldGen.SolidTile((int)greekFireSpawnPos.X / 16, (int)greekFireSpawnPos.Y / 16))
				{
					float greekFireTargetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - greekFireSpawnPos.X;
					float greekFireTargetY = Main.player[base.NPC.target].position.Y - greekFireSpawnPos.Y;
					greekFireTargetX += (float)Main.rand.Next(-50, 51);
					greekFireTargetY += (float)Main.rand.Next(50, 201);
					greekFireTargetY *= 0.2f;
					float greekFireTargetDist = (float)Math.Sqrt(greekFireTargetX * greekFireTargetX + greekFireTargetY * greekFireTargetY);
					greekFireTargetDist = greekFireSpeed / greekFireTargetDist;
					greekFireTargetX *= greekFireTargetDist;
					greekFireTargetY *= greekFireTargetDist;
					greekFireTargetX *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
					greekFireTargetY *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), greekFireSpawnPos.X, greekFireSpawnPos.Y, greekFireTargetX, greekFireTargetY, 326 + Main.rand.Next(3), 60, 0f, Main.myPlayer);
				}
			}
		}
		if (base.NPC.ai[0] == 0f && Main.netMode != 1)
		{
			base.NPC.TargetClosest();
			base.NPC.ai[0] = 1f;
			int pumpkingBlades = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)base.NPC.position.Y + base.NPC.height / 2, 328, base.NPC.whoAmI);
			Main.npc[pumpkingBlades].ai[0] = -1f;
			Main.npc[pumpkingBlades].ai[1] = base.NPC.whoAmI;
			Main.npc[pumpkingBlades].target = base.NPC.target;
			Main.npc[pumpkingBlades].netUpdate = true;
			pumpkingBlades = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)base.NPC.position.Y + base.NPC.height / 2, 328, base.NPC.whoAmI);
			Main.npc[pumpkingBlades].ai[0] = 1f;
			Main.npc[pumpkingBlades].ai[1] = base.NPC.whoAmI;
			Main.npc[pumpkingBlades].ai[3] = 150f;
			Main.npc[pumpkingBlades].target = base.NPC.target;
			Main.npc[pumpkingBlades].netUpdate = true;
		}
		if (Main.player[base.NPC.target].dead || Math.Abs(base.NPC.position.X - Main.player[base.NPC.target].position.X) > 2000f || Math.Abs(base.NPC.position.Y - Main.player[base.NPC.target].position.Y) > 2000f)
		{
			base.NPC.TargetClosest();
			if (Main.player[base.NPC.target].dead || Math.Abs(base.NPC.position.X - Main.player[base.NPC.target].position.X) > 2000f || Math.Abs(base.NPC.position.Y - Main.player[base.NPC.target].position.Y) > 2000f)
			{
				base.NPC.ai[1] = 2f;
			}
		}
		if (Main.dayTime)
		{
			base.NPC.velocity.Y += 0.3f;
			base.NPC.velocity.X *= 0.9f;
		}
		else if (base.NPC.ai[1] == 0f)
		{
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= 300f)
			{
				if (base.NPC.ai[3] != 1f)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
				}
				else
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[1] = 1f;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
			}
			Vector2 aggressivePosition = default(Vector2);
			((Vector2)(ref aggressivePosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			float aggressiveTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - aggressivePosition.X;
			float aggressiveTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - 200f - aggressivePosition.Y;
			float aggressiveTargetDist = (float)Math.Sqrt(aggressiveTargetX * aggressiveTargetX + aggressiveTargetY * aggressiveTargetY);
			float aggressiveSpeed = 8f;
			if (base.NPC.ai[3] == 1f)
			{
				if (aggressiveTargetDist > 900f)
				{
					aggressiveSpeed = 14f;
				}
				else if (aggressiveTargetDist > 600f)
				{
					aggressiveSpeed = 12f;
				}
				else if (aggressiveTargetDist > 300f)
				{
					aggressiveSpeed = 10f;
				}
			}
			if (aggressiveTargetDist > 50f)
			{
				aggressiveTargetDist = aggressiveSpeed / aggressiveTargetDist;
				base.NPC.velocity.X = (base.NPC.velocity.X * 14f + aggressiveTargetX * aggressiveTargetDist) / 15f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 14f + aggressiveTargetY * aggressiveTargetDist) / 15f;
			}
		}
		else if (base.NPC.ai[1] == 1f)
		{
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= 600f || base.NPC.ai[3] != 1f)
			{
				base.NPC.ai[2] = 0f;
				base.NPC.ai[1] = 0f;
			}
			Vector2 scytheAttackPosition = default(Vector2);
			((Vector2)(ref scytheAttackPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			float scytheAttackTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - scytheAttackPosition.X;
			float scytheAttackTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - scytheAttackPosition.Y;
			float scytheAttackTargetDist = (float)Math.Sqrt(scytheAttackTargetX * scytheAttackTargetX + scytheAttackTargetY * scytheAttackTargetY);
			scytheAttackTargetDist = 20f / scytheAttackTargetDist;
			base.NPC.velocity.X = (base.NPC.velocity.X * 49f + scytheAttackTargetX * scytheAttackTargetDist) / 50f;
			base.NPC.velocity.Y = (base.NPC.velocity.Y * 49f + scytheAttackTargetY * scytheAttackTargetDist) / 50f;
		}
		else if (base.NPC.ai[1] == 2f)
		{
			base.NPC.ai[1] = 3f;
			base.NPC.velocity.Y += 0.1f;
			if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y *= 0.95f;
			}
			base.NPC.velocity.X *= 0.95f;
			if (base.NPC.timeLeft > 500)
			{
				base.NPC.timeLeft = 500;
			}
		}
		base.NPC.rotation = base.NPC.velocity.X * -0.02f;
		return false;
	}
}
