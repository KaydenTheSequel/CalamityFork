using System;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.RegularEnemies;

public class SlimeAI : VanillaAIOverride
{
	public static void ChooseRandomItem(out int dropItem)
	{
		dropItem = -1;
		switch (Main.rand.Next(4))
		{
		case 0:
			switch (Main.rand.Next(7))
			{
			case 0:
				dropItem = 290;
				break;
			case 1:
				dropItem = 292;
				break;
			case 2:
				dropItem = 296;
				break;
			case 3:
				dropItem = 2322;
				break;
			default:
				if (Main.netMode != 0 && Main.rand.NextBool())
				{
					dropItem = 2997;
				}
				else
				{
					dropItem = 2350;
				}
				break;
			}
			break;
		case 1:
			switch (Main.rand.Next(4))
			{
			case 0:
				dropItem = 8;
				break;
			case 1:
				dropItem = 166;
				break;
			case 2:
				dropItem = 965;
				break;
			case 3:
				dropItem = 58;
				break;
			}
			break;
		case 2:
			if (Main.rand.NextBool())
			{
				dropItem = Main.rand.Next(11, 15);
			}
			else
			{
				dropItem = Main.rand.Next(699, 703);
			}
			break;
		case 3:
			dropItem = Main.rand.Next(71, 74);
			break;
		}
	}

	public override bool AI(Mod mod)
	{
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		bool isSpikedSlime = base.NPC.type == 535 || base.NPC.type == 184 || base.NPC.type == 204 || base.NPC.type == ModContent.NPCType<CryoSlime>();
		bool isLavaSlime = base.NPC.type == 59 || base.NPC.type == ModContent.NPCType<InfernalCongealment>();
		bool num = base.NPC.type == 184 || base.NPC.type == 535 || base.NPC.type == 204;
		int projectileShootType = -1;
		float projectileShootSpeedFactor = 1f;
		if (base.NPC.type == 184)
		{
			projectileShootType = 174;
		}
		if (base.NPC.type == 535)
		{
			projectileShootType = 605;
		}
		if (base.NPC.type == 204)
		{
			projectileShootType = 176;
			projectileShootSpeedFactor *= 0.6f;
		}
		ref float jumpDelay = ref base.NPC.ai[0];
		ref float dropItemID = ref base.NPC.ai[1];
		ref float targetResetCountdown = ref base.NPC.ai[2];
		ref float projectileShootCountdown = ref base.NPC.localAI[0];
		if (base.NPC.type == 1 && (dropItemID == 1f || dropItemID == 2f || dropItemID == 3f))
		{
			dropItemID = -1f;
		}
		if (base.NPC.type == 1 && dropItemID == 0f && Main.netMode != 1 && base.NPC.value > 0f)
		{
			dropItemID = -1f;
			if (Main.rand.NextBool(20))
			{
				ChooseRandomItem(out var dropItem);
				dropItemID = dropItem;
				base.NPC.netUpdate = true;
			}
		}
		Color newColor;
		if (base.NPC.type == 244)
		{
			Vector2 position = base.NPC.Center / 16f;
			newColor = Main.DiscoColor;
			Lighting.AddLight(position, ((Color)(ref newColor)).ToVector3() * -1f);
			((Color)(ref base.NPC.color)).R = (byte)Main.DiscoR;
			((Color)(ref base.NPC.color)).G = (byte)Main.DiscoG;
			((Color)(ref base.NPC.color)).B = (byte)Main.DiscoB;
			((Color)(ref base.NPC.color)).A = 100;
			base.NPC.alpha = 175;
		}
		if (base.NPC.type == 81 && Main.rand.NextBool(30))
		{
			Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, base.NPC.alpha, base.NPC.color);
			dust.velocity *= 0.3f;
		}
		if ((base.NPC.type == 147 || base.NPC.type == 184) && Main.rand.NextBool(10))
		{
			Vector2 position2 = base.NPC.position;
			int width = base.NPC.width;
			int height = base.NPC.height;
			newColor = default(Color);
			Dust dust2 = Dust.NewDustDirect(position2, width, height, 76, 0f, 0f, 0, newColor);
			dust2.noGravity = true;
			dust2.velocity *= 0.1f;
		}
		if (isLavaSlime)
		{
			Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 1f, 0.3f, 0.1f);
			Vector2 position3 = base.NPC.position;
			int width2 = base.NPC.width;
			int height2 = base.NPC.height;
			float speedX = base.NPC.velocity.X * 0.2f;
			float speedY = base.NPC.velocity.Y * 0.2f;
			newColor = default(Color);
			int idx = Dust.NewDust(position3, width2, height2, 6, speedX, speedY, 100, newColor, 1.7f);
			Main.dust[idx].noGravity = true;
		}
		if (num)
		{
			if (projectileShootCountdown > 0f)
			{
				projectileShootCountdown--;
			}
			float distanceFromTarget = base.NPC.Distance(Main.player[base.NPC.target].Center);
			bool noTilesInWayOfTarget = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			if (((!base.NPC.wet && !Main.player[base.NPC.target].npcTypeNoAggro[base.NPC.type]) & noTilesInWayOfTarget) && distanceFromTarget < 200f && base.NPC.velocity.Y == 0f)
			{
				jumpDelay = -40f;
				base.NPC.velocity.X *= 0.9f;
				if (Main.netMode != 1 && projectileShootCountdown <= 0f)
				{
					IEntitySource source = base.NPC.GetSource_FromAI();
					if (distanceFromTarget < 120f)
					{
						Vector2 spikeShootVelocity = default(Vector2);
						for (int i = 0; i < 5; i++)
						{
							((Vector2)(ref spikeShootVelocity))._002Ector((float)(i - 2), -4f);
							spikeShootVelocity *= Main.rand.NextVector2Square(0.75f, 1.25f);
							((Vector2)(ref spikeShootVelocity)).Normalize();
							spikeShootVelocity *= Main.rand.NextFloat(3.5f, 4.5f) * projectileShootSpeedFactor;
							int proj = Projectile.NewProjectile(source, base.NPC.Center, spikeShootVelocity, projectileShootType, 9, 0f, Main.myPlayer);
							if (CalamityWorld.death)
							{
								Main.projectile[proj].extraUpdates++;
							}
							projectileShootCountdown = 30f;
						}
					}
					else
					{
						Vector2 velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center - Vector2.UnitY * 100f) * projectileShootSpeedFactor * (CalamityWorld.death ? 3.25f : (CalamityWorld.revenge ? 5.5f : 4.5f));
						int proj2 = Projectile.NewProjectile(source, base.NPC.Center, velocity, projectileShootType, 9, 0f, Main.myPlayer);
						if (CalamityWorld.death)
						{
							Main.projectile[proj2].extraUpdates++;
							Main.projectile[proj2].timeLeft = 1200;
						}
						projectileShootCountdown = 50f;
					}
				}
			}
			else
			{
				projectileShootCountdown = 50f;
			}
		}
		if (targetResetCountdown > 1f)
		{
			targetResetCountdown--;
		}
		if (base.NPC.wet)
		{
			DoWaterHoverBehavior(base.NPC, isLavaSlime, ref targetResetCountdown);
		}
		base.NPC.aiAction = 0;
		if (targetResetCountdown == 0f)
		{
			jumpDelay = -100f;
			targetResetCountdown = 1f;
			base.NPC.TargetClosest();
		}
		if (!isSpikedSlime)
		{
			base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
		}
		if (base.NPC.velocity.Y == 0f)
		{
			if (base.NPC.collideY && base.NPC.oldVelocity.Y != 0f && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.position.X -= base.NPC.velocity.X + (float)base.NPC.direction;
			}
			if (base.NPC.ai[3] == base.NPC.position.X)
			{
				base.NPC.direction *= -1;
				targetResetCountdown = 200f;
			}
			base.NPC.ai[3] = 0f;
			base.NPC.velocity.X *= 0.8f;
			if (Math.Abs(base.NPC.velocity.X) < 0.1f)
			{
				base.NPC.velocity.X = 0f;
			}
			jumpDelay += (Main.slimeRain ? 4f : 3f) * (CalamityWorld.death ? 2f : 1f);
			if (base.NPC.type == 304 || base.NPC.type == 667)
			{
				jumpDelay += 10f;
			}
			if (isLavaSlime)
			{
				jumpDelay += 2f;
			}
			if (base.NPC.type == 71 || base.NPC.type == ModContent.NPCType<CryoSlime>() || base.NPC.type == ModContent.NPCType<CrimulanBlightSlime>() || base.NPC.type == ModContent.NPCType<EbonianBlightSlime>())
			{
				jumpDelay += 3f;
			}
			if (base.NPC.type == 244)
			{
				jumpDelay += 2f;
			}
			if (base.NPC.type == 138)
			{
				jumpDelay += 2f;
			}
			if (base.NPC.type == 183)
			{
				jumpDelay++;
			}
			if (base.NPC.type == 81)
			{
				jumpDelay += ((base.NPC.scale >= 0f) ? 4f : 1f);
			}
			int jumpType = 0;
			if (jumpDelay >= 0f)
			{
				jumpType = 1;
			}
			if (jumpDelay >= -1000f && jumpDelay <= -500f)
			{
				jumpType = 2;
			}
			if (jumpDelay >= -2000f && jumpDelay <= -1500f)
			{
				jumpType = 3;
			}
			if (jumpType > 0)
			{
				DoJump(base.NPC, jumpType, isLavaSlime, ref targetResetCountdown, out jumpDelay);
			}
			else if (jumpDelay >= -30f)
			{
				base.NPC.aiAction = 1;
				return false;
			}
		}
		else if (base.NPC.target < 255 && ((base.NPC.direction == 1 && base.NPC.velocity.X < 3f) || (base.NPC.direction == -1 && base.NPC.velocity.X > -3f)))
		{
			if (base.NPC.collideX && Math.Abs(base.NPC.velocity.X) == 0.2f)
			{
				base.NPC.position.X -= 1.4f * (float)base.NPC.direction;
			}
			if (base.NPC.collideY && base.NPC.oldVelocity.Y != 0f && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.position.X -= base.NPC.velocity.X + (float)base.NPC.direction;
			}
			if ((base.NPC.direction == -1 && (double)base.NPC.velocity.X < 0.01) || (base.NPC.direction == 1 && (double)base.NPC.velocity.X > -0.01))
			{
				base.NPC.velocity.X += 0.2f * (float)base.NPC.direction;
				return false;
			}
			base.NPC.velocity.X *= 0.93f;
		}
		return false;
	}

	public static void DoJump(NPC npc, int jumpType, bool isLavaSlime, ref float targetResetCountdown, out float jumpDelay)
	{
		if (targetResetCountdown == 1f)
		{
			npc.TargetClosest();
		}
		float verticalJumpSpeed = 4f;
		float horizontalJumpSpeed = 4f;
		if (Main.slimeRain)
		{
			verticalJumpSpeed = 5f;
			horizontalJumpSpeed = 5f;
		}
		if (jumpType == 3)
		{
			verticalJumpSpeed *= 2.5f;
			horizontalJumpSpeed++;
			if (isLavaSlime)
			{
				verticalJumpSpeed += 2f;
			}
		}
		npc.velocity.Y = 0f - verticalJumpSpeed;
		npc.velocity.X += horizontalJumpSpeed * (float)npc.direction;
		switch (jumpType)
		{
		case 3:
			jumpDelay = -200f;
			npc.ai[3] = npc.position.X;
			break;
		case 1:
			jumpDelay = -1120f;
			break;
		default:
			jumpDelay = -2120f;
			break;
		}
		if (npc.type == 141 || npc.type == ModContent.NPCType<PerennialSlime>() || npc.type == ModContent.NPCType<BloomSlime>() || npc.type == ModContent.NPCType<IrradiatedSlime>())
		{
			npc.velocity.X *= 1.2f;
			npc.velocity.Y *= 1.3f;
		}
		npc.netUpdate = true;
	}

	public static void DoWaterHoverBehavior(NPC npc, bool isLavaSlime, ref float targetResetCountdown)
	{
		if (npc.collideY)
		{
			npc.velocity.Y = 0f - (CalamityWorld.death ? 4f : 3f);
		}
		if (npc.velocity.Y < 0f && npc.ai[3] == npc.position.X)
		{
			npc.direction *= -1;
			targetResetCountdown = 200f;
		}
		if (npc.velocity.Y > 0f)
		{
			npc.ai[3] = npc.position.X;
		}
		float riseSpeed = (CalamityWorld.death ? 0.6f : 0.55f);
		float maxRiseSpeed = (CalamityWorld.death ? 6f : 5f);
		if (isLavaSlime)
		{
			riseSpeed += 0.2f;
			maxRiseSpeed += 10f;
		}
		if (npc.velocity.Y > 2f)
		{
			npc.velocity.Y *= 0.9f;
		}
		else if ((npc.directionY < 0) & isLavaSlime)
		{
			npc.velocity.Y -= 1.2f;
		}
		npc.velocity.Y -= riseSpeed;
		if (npc.velocity.Y < 0f - maxRiseSpeed)
		{
			npc.velocity.Y = 0f - maxRiseSpeed;
		}
		if (targetResetCountdown == 1f)
		{
			npc.TargetClosest();
		}
	}
}
