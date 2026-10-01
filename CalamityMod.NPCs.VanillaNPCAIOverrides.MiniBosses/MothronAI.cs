using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.MiniBosses;

public class MothronAI : VanillaAIOverride
{
	internal enum MothronAIState
	{
		DespawnYeet = -1,
		NewAISelection,
		FlyTowardsPlayer,
		AccelerateTowardsPlayer,
		ChargeRedirect,
		ChargePreparation,
		DoTheFuckingCharge,
		PickSpotToLayEgg,
		FlyToEggSpot,
		LayEgg
	}

	public override bool AI(Mod mod)
	{
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d17: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1303: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1231: Unknown result type (might be due to invalid IL or missing references)
		//IL_123e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1249: Unknown result type (might be due to invalid IL or missing references)
		//IL_124b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1250: Unknown result type (might be due to invalid IL or missing references)
		//IL_125a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1261: Unknown result type (might be due to invalid IL or missing references)
		//IL_1266: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1272: Unknown result type (might be due to invalid IL or missing references)
		//IL_1484: Unknown result type (might be due to invalid IL or missing references)
		//IL_1490: Unknown result type (might be due to invalid IL or missing references)
		//IL_1497: Unknown result type (might be due to invalid IL or missing references)
		//IL_149c: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_14be: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1383: Unknown result type (might be due to invalid IL or missing references)
		//IL_138b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_143f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.noTileCollide = false;
		base.NPC.noGravity = true;
		base.NPC.knockBackResist = 0f;
		ref float aiState = ref base.NPC.ai[0];
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = num < 0.4f;
		bool phase3 = num < 0.1f;
		Player target = Main.player[base.NPC.target];
		if (!Main.eclipse)
		{
			aiState = -1f;
		}
		else if (base.NPC.target < 0 || target.dead || !target.active)
		{
			base.NPC.TargetClosest();
			if (target.dead)
			{
				aiState = -1f;
				if (Main.netMode != 1)
				{
					base.NPC.netUpdate = true;
				}
			}
		}
		float chargeSpeed = 32f;
		switch ((MothronAIState)(int)aiState)
		{
		case MothronAIState.DespawnYeet:
		{
			base.NPC.damage = 0;
			Vector2 idealVelocity = Vector2.UnitY * -34f;
			base.NPC.velocity = (base.NPC.velocity * 4f + idealVelocity) / 5f;
			base.NPC.noTileCollide = true;
			base.NPC.dontTakeDamage = true;
			return false;
		}
		case MothronAIState.NewAISelection:
		{
			base.NPC.damage = 0;
			ref float aiTimer = ref base.NPC.ai[1];
			base.NPC.TargetClosest();
			if (base.NPC.Center.X < target.Center.X - 2f)
			{
				base.NPC.direction = 1;
			}
			if (base.NPC.Center.X > target.Center.X + 2f)
			{
				base.NPC.direction = -1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * 9f + base.NPC.velocity.X * 0.025f) / 10f;
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X *= (0f - base.NPC.oldVelocity.X) * 0.5f;
				base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X, -4f, 4f);
			}
			if (base.NPC.collideY)
			{
				base.NPC.velocity.Y *= (0f - base.NPC.oldVelocity.Y) * 0.5f;
				base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y, -4f, 4f);
			}
			Vector2 destinationAboveTarget = target.Center - Vector2.UnitY * 200f;
			float distanceFromAboveTarget = base.NPC.Distance(destinationAboveTarget);
			if (distanceFromAboveTarget > 3000f)
			{
				aiState = 1f;
				aiTimer = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			else if (distanceFromAboveTarget > 600f)
			{
				float flyInertia = 30f;
				Vector2 idealFlyVelocity = base.NPC.SafeDirectionTo(destinationAboveTarget, -Vector2.UnitY) * 15f;
				base.NPC.velocity = (base.NPC.velocity * (flyInertia - 1f) + idealFlyVelocity) / flyInertia;
			}
			else if (((Vector2)(ref base.NPC.velocity)).Length() > 2f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.95f;
			}
			else if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 1.05f;
			}
			aiTimer++;
			if (!(aiTimer >= 10f) || Main.netMode == 1)
			{
				break;
			}
			aiTimer = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			while ((int)aiState == 0)
			{
				int selection = Main.rand.Next(3);
				if (phase3)
				{
					selection = 1;
				}
				else if (phase2)
				{
					selection = Main.rand.Next(2);
				}
				if (selection == 0 && Collision.CanHit(base.NPC.Center, 1, 1, target.Center, 1, 1))
				{
					aiState = 2f;
					continue;
				}
				switch (selection)
				{
				case 1:
					aiState = 3f;
					break;
				case 2:
					if (NPC.CountNPCS(478) + NPC.CountNPCS(479) < 2)
					{
						aiState = 6f;
					}
					break;
				}
			}
			base.NPC.ForceNetUpdate();
			break;
		}
		case MothronAIState.FlyTowardsPlayer:
		{
			base.NPC.damage = 0;
			base.NPC.collideX = false;
			base.NPC.collideY = false;
			base.NPC.noTileCollide = true;
			if (base.NPC.target < 0 || !target.active || target.dead)
			{
				base.NPC.TargetClosest();
			}
			base.NPC.spriteDirection = (base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt());
			base.NPC.rotation = (base.NPC.rotation * 9f + base.NPC.velocity.X * 0.02f) / 10f;
			if (base.NPC.WithinRange(target.Center, 500f) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				aiState = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			float flySpeed = 18f + base.NPC.Distance(target.Center) / 100f;
			float flyInertia = 25f;
			Vector2 idealFlyVelocity = base.NPC.SafeDirectionTo(target.Center, -Vector2.UnitY) * flySpeed;
			base.NPC.velocity = (base.NPC.velocity * (flyInertia - 1f) + idealFlyVelocity) / flyInertia;
			break;
		}
		case MothronAIState.AccelerateTowardsPlayer:
		{
			base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 0.5);
			ref float aiTimer = ref base.NPC.ai[1];
			ref float flySpeedAdditive = ref base.NPC.ai[2];
			if (base.NPC.target < 0 || !target.active || target.dead)
			{
				base.NPC.TargetClosest();
				aiState = 0f;
				aiTimer = 0f;
				flySpeedAdditive = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			base.NPC.spriteDirection = (base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt());
			base.NPC.rotation = (base.NPC.rotation * 4f + base.NPC.velocity.X * 0.025f) / 5f;
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X *= (0f - base.NPC.oldVelocity.X) * 0.5f;
				base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X, -4f, 4f);
			}
			if (base.NPC.collideY)
			{
				base.NPC.velocity.Y *= (0f - base.NPC.oldVelocity.Y) * 0.5f;
				base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y, -4f, 4f);
			}
			Vector2 destination = target.Center - Vector2.UnitY * 20f;
			flySpeedAdditive += 1f / 45f;
			if (Main.expertMode)
			{
				flySpeedAdditive += 1f / 60f;
			}
			float flySpeed = 12f + flySpeedAdditive + base.NPC.Distance(destination) / 120f;
			float flyInertia = 20f;
			Vector2 idealFlyVelocity = base.NPC.SafeDirectionTo(destination, -Vector2.UnitY) * flySpeed;
			base.NPC.velocity = (base.NPC.velocity * (flyInertia - 1f) + idealFlyVelocity) / flyInertia;
			aiTimer++;
			if (aiTimer >= 120f || !Collision.CanHit(base.NPC.Center, 1, 1, target.Center, 1, 1))
			{
				aiState = 0f;
				aiTimer = 0f;
				flySpeedAdditive = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case MothronAIState.ChargeRedirect:
		{
			base.NPC.damage = 0;
			ref float flySpeedAdditive = ref base.NPC.ai[2];
			base.NPC.noTileCollide = true;
			base.NPC.spriteDirection = (base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt());
			base.NPC.rotation = (base.NPC.rotation * 4f + base.NPC.velocity.X * 0.0175f) / 5f;
			Vector2 destination = target.Center;
			destination -= Vector2.UnitY * 12f;
			float xOffset = 600f;
			if (base.NPC.Center.X > target.Center.X)
			{
				destination.X += xOffset;
			}
			else
			{
				destination.X -= xOffset;
			}
			if (Main.netMode != 1 && Math.Abs(base.NPC.Center.X - target.Center.X) > xOffset - 50f && Math.Abs(base.NPC.Center.Y - target.Center.Y) < 20f)
			{
				aiState = 4f;
				flySpeedAdditive = 0f;
				base.NPC.ForceNetUpdate();
			}
			flySpeedAdditive += 1f / 30f;
			float flySpeed = 24f + flySpeedAdditive;
			float flyInertia = 4f;
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(destination, -Vector2.UnitY) * flySpeed;
			base.NPC.velocity = (base.NPC.velocity * (flyInertia - 1f) + idealVelocity) / flyInertia;
			break;
		}
		case MothronAIState.ChargePreparation:
		{
			base.NPC.damage = 0;
			ref float aiTimer = ref base.NPC.ai[1];
			ref float chargeDirection = ref base.NPC.ai[2];
			base.NPC.noTileCollide = true;
			base.NPC.rotation = (base.NPC.rotation * 4f + base.NPC.velocity.X * 0.0175f) / 5f;
			Vector2 destination = target.Center - Vector2.UnitY * 12f;
			float chargePreperationInertia = 8f;
			Vector2 chargeVelocity = base.NPC.SafeDirectionTo(destination, -Vector2.UnitY) * chargeSpeed;
			base.NPC.velocity = (base.NPC.velocity * (chargePreperationInertia - 1f) + chargeVelocity) / chargePreperationInertia;
			base.NPC.spriteDirection = (base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt());
			if (Main.netMode == 1)
			{
				break;
			}
			aiTimer++;
			if (aiTimer > 10f)
			{
				base.NPC.velocity = chargeVelocity;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				aiState = 5f;
				chargeDirection = base.NPC.direction;
				base.NPC.ForceNetUpdate();
			}
			break;
		}
		case MothronAIState.DoTheFuckingCharge:
		{
			ref float chargeDirection = ref base.NPC.ai[2];
			ref float flySpeedAdditive = ref base.NPC.ai[3];
			base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 1.2);
			base.NPC.collideX = false;
			base.NPC.collideY = false;
			base.NPC.noTileCollide = true;
			flySpeedAdditive += 1f / 30f;
			base.NPC.velocity.X = (chargeSpeed + flySpeedAdditive) * chargeDirection;
			float chargeDistance = 460f;
			if ((Main.netMode != 1 && chargeDirection > 0f && base.NPC.Center.X > target.Center.X + chargeDistance) || (chargeDirection < 0f && base.NPC.Center.X < target.Center.X - chargeDistance))
			{
				if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					aiState = 0f;
					chargeDirection = 0f;
					flySpeedAdditive = 0f;
					base.NPC.ForceNetUpdate();
				}
				else if (Math.Abs(base.NPC.Center.X - target.Center.X) > chargeDistance * 2f - 120f)
				{
					aiState = 1f;
					chargeDirection = 0f;
					flySpeedAdditive = 0f;
					base.NPC.ForceNetUpdate();
				}
			}
			base.NPC.rotation = (base.NPC.rotation * 4f + base.NPC.velocity.X * 0.0175f) / 5f;
			break;
		}
		case MothronAIState.PickSpotToLayEgg:
		{
			base.NPC.damage = 0;
			ref float laySpotPositionX = ref base.NPC.ai[2];
			ref float laySpotPositionY = ref base.NPC.ai[3];
			base.NPC.TargetClosest();
			if (Main.netMode == 1)
			{
				break;
			}
			aiState = 0f;
			laySpotPositionX = (laySpotPositionY = -1f);
			for (int i = 0; i < 1000; i++)
			{
				int potentialSpotX = (int)target.Center.X / 16;
				int potentialSpotY = (int)target.Center.Y / 16;
				int checkAreaX = 30 + i / 50;
				int checkAreaY = 20 + i / 75;
				potentialSpotX += Main.rand.Next(-checkAreaX, checkAreaX + 1);
				potentialSpotY += Main.rand.Next(-checkAreaY, checkAreaY + 1);
				if (!WorldGen.SolidTile(potentialSpotX, potentialSpotY))
				{
					for (; !WorldGen.SolidTile(potentialSpotX, potentialSpotY) && (double)potentialSpotY < Main.worldSurface; potentialSpotY++)
					{
					}
					if (base.NPC.WithinRange(Utils.ToWorldCoordinates(new Vector2((float)potentialSpotX, (float)potentialSpotY), 8f, 8f), 1600f))
					{
						aiState = 7f;
						laySpotPositionX = potentialSpotX;
						laySpotPositionY = potentialSpotY;
						break;
					}
				}
			}
			base.NPC.ForceNetUpdate();
			break;
		}
		case MothronAIState.FlyToEggSpot:
		{
			base.NPC.damage = 0;
			base.NPC.spriteDirection = (base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt());
			base.NPC.rotation = (base.NPC.rotation * 9f + base.NPC.velocity.X * 0.025f) / 10f;
			base.NPC.noTileCollide = true;
			Vector2 spotToLayEgg = Utils.ToWorldCoordinates(new Vector2(base.NPC.ai[2], base.NPC.ai[3]), 8f, -20f);
			float distanceFromSpot = base.NPC.Distance(spotToLayEgg);
			float flySpeed = 12f + distanceFromSpot / 150f;
			if (flySpeed > 20f)
			{
				flySpeed = 20f;
			}
			if (Main.netMode != 1 && distanceFromSpot < 10f)
			{
				aiState = 8f;
				base.NPC.netUpdate = true;
			}
			float flyInertia = 10f;
			base.NPC.velocity = (base.NPC.velocity * (flyInertia - 1f) + base.NPC.SafeDirectionTo(spotToLayEgg, -Vector2.UnitY) * flySpeed) / flyInertia;
			break;
		}
		case MothronAIState.LayEgg:
		{
			base.NPC.damage = 0;
			base.NPC.rotation = (base.NPC.rotation * 9f + base.NPC.velocity.X * 0.025f) / 10f;
			base.NPC.noTileCollide = false;
			Vector2 spotToLayEgg = Utils.ToWorldCoordinates(new Vector2(base.NPC.ai[2], base.NPC.ai[3]), 8f, -28f);
			float distanceFromSpot = base.NPC.Distance(spotToLayEgg);
			float hoverSpeed = 4f;
			float hoverInertia = 2f;
			if (Main.netMode != 1 && distanceFromSpot < 44f)
			{
				ref float attackTimer = ref base.NPC.ai[1];
				int eggLayTime = 20;
				if (Main.expertMode)
				{
					eggLayTime = (int)((double)eggLayTime * 0.75);
				}
				int waitTime = eggLayTime;
				attackTimer++;
				if (attackTimer == (float)eggLayTime)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spotToLayEgg.X, (int)spotToLayEgg.Y + 20, 478, base.NPC.whoAmI);
				}
				else if (attackTimer == (float)(eggLayTime + waitTime))
				{
					aiState = 0f;
					attackTimer = 0f;
					base.NPC.ai[2] = (base.NPC.ai[3] = 0f);
					if (NPC.CountNPCS(478) + NPC.CountNPCS(479) < 3 && !Main.rand.NextBool(3))
					{
						aiState = 6f;
					}
					else if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						aiState = 1f;
					}
					base.NPC.netUpdate = true;
				}
			}
			if (distanceFromSpot < hoverSpeed)
			{
				hoverSpeed = distanceFromSpot;
			}
			Vector2 hoverVelocity = base.NPC.SafeDirectionTo(spotToLayEgg) * hoverSpeed;
			base.NPC.velocity = (base.NPC.velocity * (hoverInertia - 1f) + hoverVelocity) / hoverInertia;
			if (base.NPC.velocity.HasNaNs())
			{
				base.NPC.velocity = Vector2.Zero;
			}
			break;
		}
		}
		return false;
	}
}
