using System.Collections.Generic;
using CalamityMod.NPCs.Crags;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.RegularEnemies;

public class DemonEyeAI : VanillaAIOverride
{
	public const int FadeThroughWallsDelay = 300;

	public static List<int> NightTimeEnemies => new List<int> { 2, 133, 190, 191, 192, 193, 194, 317, 318 };

	public static List<int> Pigrons => new List<int> { 170, 180, 171 };

	public static void DemonEyeBatMovement(NPC npc, float maxXSpeed = 6f, float maxYSpeed = 3.5f, float xAccel = 0.1f, float xAccelBoost1 = 0.06f, float xAccelBoost2 = 0.25f, float yAccel = 0.12f, float yAccelBoost1 = 0.07f, float yAccelBoost2 = 0.2f)
	{
		if (npc.direction == -1 && npc.velocity.X > 0f - maxXSpeed)
		{
			npc.velocity.X -= xAccel;
			if (npc.velocity.X > maxXSpeed)
			{
				npc.velocity.X -= xAccelBoost1;
			}
			else if (npc.velocity.X > 0f)
			{
				npc.velocity.X -= xAccelBoost2;
			}
			if (npc.velocity.X < 0f - maxXSpeed)
			{
				npc.velocity.X = 0f - maxXSpeed;
			}
		}
		else if (npc.direction == 1 && npc.velocity.X < maxXSpeed)
		{
			npc.velocity.X += xAccel;
			if (npc.velocity.X < 0f - maxXSpeed)
			{
				npc.velocity.X += xAccelBoost1;
			}
			else if (npc.velocity.X < 0f)
			{
				npc.velocity.X += xAccelBoost2;
			}
			if (npc.velocity.X > maxXSpeed)
			{
				npc.velocity.X = maxXSpeed;
			}
		}
		if (npc.directionY == -1 && npc.velocity.Y > 0f - maxYSpeed)
		{
			npc.velocity.Y -= yAccel;
			if (npc.velocity.Y > maxYSpeed)
			{
				npc.velocity.Y -= yAccelBoost1;
			}
			else if (npc.velocity.Y > 0f)
			{
				npc.velocity.Y -= yAccelBoost2;
			}
			if (npc.velocity.Y < 0f - maxYSpeed)
			{
				npc.velocity.Y = 0f - maxYSpeed;
			}
		}
		else if (npc.directionY == 1 && npc.velocity.Y < maxYSpeed)
		{
			npc.velocity.Y += yAccel;
			if (npc.velocity.Y < 0f - maxYSpeed)
			{
				npc.velocity.Y += yAccelBoost1;
			}
			else if (npc.velocity.Y < 0f)
			{
				npc.velocity.Y += yAccelBoost2;
			}
			if (npc.velocity.Y > maxYSpeed)
			{
				npc.velocity.Y = maxYSpeed;
			}
		}
	}

	public override bool AI(Mod mod)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		if (Pigrons.Contains(base.NPC.type) && Main.rand.NextBool(1000))
		{
			SoundEngine.PlaySound(in SoundID.Zombie9, base.NPC.Center);
		}
		base.NPC.noGravity = true;
		if (!base.NPC.noTileCollide)
		{
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X = base.NPC.oldVelocity.X * -0.5f;
				if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
				{
					base.NPC.velocity.X = 2f;
				}
				if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
				{
					base.NPC.velocity.X = -2f;
				}
			}
			if (base.NPC.collideY)
			{
				base.NPC.velocity.Y = base.NPC.oldVelocity.Y * -0.5f;
				if (base.NPC.velocity.Y > 0f && base.NPC.velocity.Y < 1f)
				{
					base.NPC.velocity.Y = 1f;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.velocity.Y > -1f)
				{
					base.NPC.velocity.Y = -1f;
				}
			}
		}
		if (Main.dayTime && (double)base.NPC.position.Y <= Main.worldSurface * 16.0 && NightTimeEnemies.Contains(base.NPC.type))
		{
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
			base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt();
			base.NPC.directionY = (base.NPC.velocity.X > 0f).ToDirectionInt();
		}
		else
		{
			base.NPC.TargetClosest();
		}
		Player target = Main.player[base.NPC.target];
		ref float fadeThroughWallsTimer = ref base.NPC.ai[0];
		ref float fadeThroughWallsFlag = ref base.NPC.ai[1];
		if (Pigrons.Contains(base.NPC.type) || base.NPC.type == ModContent.NPCType<CalamityEye>())
		{
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, target.position, target.width, target.height))
			{
				if (fadeThroughWallsFlag != 0f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					fadeThroughWallsFlag = 0f;
					fadeThroughWallsTimer = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (fadeThroughWallsFlag == 0f)
			{
				fadeThroughWallsTimer++;
			}
			if (fadeThroughWallsTimer >= (float)(CalamityWorld.death ? 150 : 300))
			{
				fadeThroughWallsFlag = 1f;
				fadeThroughWallsTimer = 0f;
				base.NPC.netUpdate = true;
			}
			if (fadeThroughWallsFlag == 0f)
			{
				base.NPC.alpha = 0;
				base.NPC.noTileCollide = false;
			}
			else
			{
				base.NPC.wet = false;
				base.NPC.alpha = 200;
				base.NPC.noTileCollide = true;
			}
			base.NPC.rotation = base.NPC.velocity.Y * 0.1f * (float)base.NPC.direction;
			base.NPC.TargetClosest();
			DemonEyeBatMovement(base.NPC);
		}
		else if (base.NPC.type == 116)
		{
			base.NPC.TargetClosest();
			Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.3f, 0.2f, 0.1f);
			DemonEyeBatMovement(base.NPC, CalamityWorld.death ? 10f : 8f, CalamityWorld.death ? 5f : 3.5f, 0.12f, 0.12f, 0.25f, 0.06f);
			if (Main.rand.NextBool(40))
			{
				Dust dust = Dust.NewDustDirect(new Vector2(base.NPC.position.X, base.NPC.position.Y + (float)base.NPC.height * 0.25f), base.NPC.width, base.NPC.height / 2, 5, base.NPC.velocity.X, 2f);
				dust.velocity.X *= 0.5f;
				dust.velocity.Y *= 0.1f;
			}
		}
		else if (base.NPC.type == 133)
		{
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5)
			{
				DemonEyeBatMovement(base.NPC, CalamityWorld.death ? 10f : 8f, CalamityWorld.death ? 8f : 6f, 0.12f, 0.12f, 0.07f, 0.12f, 0.12f, 0.07f);
			}
			else
			{
				DemonEyeBatMovement(base.NPC, CalamityWorld.death ? 8f : 6f, CalamityWorld.death ? 4f : 2.5f, 0.12f, 0.12f, 0.07f, 0.06f, 0.07f, 0.05f);
			}
		}
		else
		{
			float maxSpeedX = (CalamityWorld.death ? 6f : 5f);
			float maxSpeedY = (CalamityWorld.death ? 2.5f : 2f);
			maxSpeedX *= 1f + (1f - base.NPC.scale);
			maxSpeedY *= 1f + (1f - base.NPC.scale);
			DemonEyeBatMovement(base.NPC, maxSpeedX, maxSpeedY, 0.08f, 0.08f, 0.03f, 0.02f, 0.03f, 0.015f);
		}
		if ((base.NPC.type == 2 || base.NPC.type == 133 || base.NPC.type == ModContent.NPCType<CalamityEye>() || (base.NPC.type >= 190 && base.NPC.type <= 194)) && Main.rand.NextBool(40))
		{
			Dust dust2 = Dust.NewDustDirect(new Vector2(base.NPC.position.X, base.NPC.position.Y + (float)base.NPC.height * 0.25f), base.NPC.width, base.NPC.height / 2, 5, base.NPC.velocity.X, 2f);
			dust2.velocity.X *= 0.5f;
			dust2.velocity.Y *= 0.1f;
		}
		if (base.NPC.wet && !Pigrons.Contains(base.NPC.type))
		{
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y *= 0.95f;
			}
			base.NPC.velocity.Y -= 0.6f;
			if (base.NPC.velocity.Y < -5f)
			{
				base.NPC.velocity.Y = -5f;
			}
			base.NPC.TargetClosest();
		}
		return false;
	}
}
