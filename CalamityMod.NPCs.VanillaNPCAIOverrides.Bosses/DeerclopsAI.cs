using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class DeerclopsAI : VanillaAIOverride
{
	public static bool hasTargetBeenInRange = true;

	public const float IncreaseDRTriggerDistance = 750f;

	public const float MaxDRIncreaseDistance = 1200f;

	public static float borderScale = 5f;

	public static string ArenaTexPath = "CalamityMod/Particles/LargeBloom";

	public static int DebrisDamage = 18;

	public static int IceSpikeDamage = 16;

	public static int HandDamage = 13;

	public static Asset<Texture2D> ArenaTex;

	public override bool AI(Mod mod)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_162b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1636: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1031: Unknown result type (might be due to invalid IL or missing references)
		//IL_1046: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13df: Unknown result type (might be due to invalid IL or missing references)
		//IL_1516: Unknown result type (might be due to invalid IL or missing references)
		//IL_152b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1685: Unknown result type (might be due to invalid IL or missing references)
		//IL_169a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b76: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_114f: Unknown result type (might be due to invalid IL or missing references)
		//IL_115a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1160: Unknown result type (might be due to invalid IL or missing references)
		//IL_1162: Unknown result type (might be due to invalid IL or missing references)
		//IL_1167: Unknown result type (might be due to invalid IL or missing references)
		//IL_119a: Unknown result type (might be due to invalid IL or missing references)
		//IL_119f: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_0887: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		NPC.deerclopsBoss = base.NPC.whoAmI;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		int rubble = 962;
		int iceSpike = 961;
		int shadowHand = 965;
		if (base.NPC.target.WithinBounds(Main.player.Length) && Main.player[base.NPC.target].dead)
		{
			hasTargetBeenInRange = false;
		}
		NPCAimedTarget targetData = base.NPC.GetTargetData();
		bool haltMovement = false;
		bool goHome = false;
		float resistDamageAmount = MathHelper.Clamp((base.NPC.Distance(targetData.Center) - 750f) / 450f, 0f, 1f);
		base.NPC.localAI[3] = MathHelper.Lerp(0f, 30f, resistDamageAmount);
		float dustAndDRScalar = Utils.Remap(base.NPC.localAI[3], 0f, 30f, 0f, 1f);
		calamityGlobalNPC.DR = MathHelper.Lerp(0f, 0.9f, dustAndDRScalar);
		if (dustAndDRScalar > 0f)
		{
			float invincibleDustAmount = Main.rand.NextFloat() * dustAndDRScalar * 3f;
			while (invincibleDustAmount > 0f)
			{
				invincibleDustAmount--;
				Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 109, 0f, -3f, 0, default(Color), 1.4f).noGravity = true;
			}
		}
		else if (!hasTargetBeenInRange && base.NPC.target.WithinBounds(Main.player.Length) && !Main.player[base.NPC.target].dead)
		{
			hasTargetBeenInRange = true;
		}
		Main.LocalPlayer.Calamity().lastDeerclopsPosition = base.NPC.Center;
		if (base.NPC.homeTileX == -1 && base.NPC.homeTileY == -1)
		{
			Point point = base.NPC.Bottom.ToTileCoordinates();
			base.NPC.homeTileX = point.X;
			base.NPC.homeTileY = point.Y;
			base.NPC.ai[2] = base.NPC.homeTileX;
			base.NPC.ai[3] = base.NPC.homeTileY;
			base.NPC.netUpdate = true;
			base.NPC.timeLeft = 86400;
		}
		base.NPC.timeLeft -= Main.worldEventUpdates;
		if (base.NPC.timeLeft < 0)
		{
			base.NPC.timeLeft = 0;
		}
		base.NPC.homeTileX = (int)base.NPC.ai[2];
		base.NPC.homeTileY = (int)base.NPC.ai[3];
		if (Main.netMode != 1 && hasTargetBeenInRange)
		{
			SpawnBorderShadowHands(base.NPC, lifeRatio, shadowHand, HandDamage, death);
		}
		Vector2 center;
		switch ((int)base.NPC.ai[0])
		{
		case -1:
			base.NPC.localAI[3] = -10f;
			break;
		case 0:
		{
			base.NPC.TargetClosest();
			targetData = base.NPC.GetTargetData();
			if (ShouldRunAway(base.NPC, ref targetData, isChasing: true))
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.netUpdate = true;
				break;
			}
			if (base.NPC.timeLeft < 86400)
			{
				base.NPC.timeLeft = 86400;
			}
			float attackRate = 1f;
			base.NPC.ai[1] += attackRate;
			Vector2 relativeCenter = base.NPC.Bottom + new Vector2(0f, -32f);
			Vector2 val = targetData.Hitbox.ClosestPointInRect(relativeCenter);
			Vector2 distanceFromTarget2 = val - relativeCenter;
			center = val - base.NPC.Center;
			((Vector2)(ref center)).Length();
			float distanceCheckMultiplier = 0.6f;
			bool useFrontIceSpikeAttack = Math.Abs(distanceFromTarget2.X) >= Math.Abs(distanceFromTarget2.Y) * distanceCheckMultiplier || ((Vector2)(ref distanceFromTarget2)).Length() < 48f;
			bool useEitherIceSpikeAttack = distanceFromTarget2.Y <= (float)(100 + targetData.Height) && distanceFromTarget2.Y >= -200f;
			float iceSpikeAttackLimit = 3f;
			bool doNotUseIceSpikes = calamityGlobalNPC.newAI[1] >= iceSpikeAttackLimit;
			if (!doNotUseIceSpikes)
			{
				float iceSpikesDistanceGateValue = 120f + MathHelper.Lerp(0f, 60f, 1f - lifeRatio);
				if (((Math.Abs(distanceFromTarget2.X) < iceSpikesDistanceGateValue) & useEitherIceSpikeAttack) && base.NPC.velocity.Y == 0f && base.NPC.localAI[1] >= 2f)
				{
					base.NPC.velocity.X = 0f;
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
					base.NPC.localAI[1] = 0f;
					calamityGlobalNPC.newAI[0]--;
					calamityGlobalNPC.newAI[1]++;
					base.NPC.SyncExtraAI();
					base.NPC.netUpdate = true;
					break;
				}
				if ((((Math.Abs(distanceFromTarget2.X) < iceSpikesDistanceGateValue) & useEitherIceSpikeAttack) && base.NPC.velocity.Y == 0f) & useFrontIceSpikeAttack)
				{
					base.NPC.velocity.X = 0f;
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.localAI[1]++;
					calamityGlobalNPC.newAI[0]--;
					calamityGlobalNPC.newAI[1]++;
					base.NPC.SyncExtraAI();
					base.NPC.netUpdate = true;
					break;
				}
			}
			float rubbleAttackLimit = 4f;
			bool doNotUseRubble = calamityGlobalNPC.newAI[1] >= rubbleAttackLimit;
			float rubbleGateValue = (death ? 160f : 200f);
			if (!doNotUseRubble)
			{
				bool useRubbleAttack = base.NPC.ai[1] >= rubbleGateValue;
				if ((base.NPC.velocity.Y == 0f && base.NPC.velocity.X != 0f) & useRubbleAttack)
				{
					base.NPC.velocity.X = 0f;
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.localAI[1] = 0f;
					calamityGlobalNPC.newAI[0]--;
					calamityGlobalNPC.newAI[1]++;
					base.NPC.SyncExtraAI();
					base.NPC.netUpdate = true;
					break;
				}
			}
			float shadowHandGateValue = (death ? 60f : 75f);
			bool useShadowHandAttack = base.NPC.ai[1] >= shadowHandGateValue;
			if ((base.NPC.velocity.Y == 0f && base.NPC.velocity.X == 0f) & useShadowHandAttack)
			{
				base.NPC.velocity.X = 0f;
				base.NPC.ai[0] = ((targetData.Center.Y < base.NPC.Center.Y - 50f) ? 5f : 3f);
				base.NPC.ai[1] = 0f;
				base.NPC.localAI[1] = 0f;
				calamityGlobalNPC.newAI[0]--;
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.SyncExtraAI();
				base.NPC.netUpdate = true;
				break;
			}
			float secondShadowHandAttackCooldown = 4f;
			float secondShadowHandGateValue = (death ? 80f : 100f);
			bool useSecondShadowHandAttack = base.NPC.ai[1] >= secondShadowHandGateValue;
			if (((base.NPC.velocity.Y == 0f) & useSecondShadowHandAttack) && Math.Abs(distanceFromTarget2.X) > 100f && calamityGlobalNPC.newAI[0] <= 0f)
			{
				base.NPC.velocity.X = 0f;
				base.NPC.ai[0] = 3f;
				base.NPC.ai[1] = 0f;
				base.NPC.localAI[1] = 0f;
				calamityGlobalNPC.newAI[0] = secondShadowHandAttackCooldown;
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.SyncExtraAI();
				base.NPC.netUpdate = true;
			}
			float haltMovementGateValue = (doNotUseRubble ? (secondShadowHandGateValue + 20f) : (doNotUseIceSpikes ? (rubbleGateValue + 20f) : 240f));
			if (((Vector2)(ref distanceFromTarget2)).Length() < 750f)
			{
				haltMovement = base.NPC.ai[1] >= haltMovementGateValue;
			}
			break;
		}
		case 1:
		{
			base.NPC.ai[1]++;
			haltMovement = true;
			MakeSpikesForward(base.NPC, 1, targetData, iceSpike, IceSpikeDamage, lifeRatio, death);
			float iceSpikePhaseGateValue = 80f;
			if (base.NPC.ai[1] >= iceSpikePhaseGateValue)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 2:
		{
			int scoopRubbleGateValue = 32;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] == (float)(scoopRubbleGateValue - 20))
			{
				SoundEngine.PlaySound(in SoundID.DeerclopsScream, base.NPC.Center);
			}
			if (base.NPC.ai[1] == (float)scoopRubbleGateValue)
			{
				SoundEngine.PlaySound(in SoundID.DeerclopsRubbleAttack, base.NPC.Center);
			}
			haltMovement = true;
			if (Main.netMode != 1 && base.NPC.ai[1] >= (float)scoopRubbleGateValue)
			{
				Point sourceTileCoords = base.NPC.Top.ToTileCoordinates();
				int numRubble = (death ? 60 : 20);
				int distancedByThisManyTiles = (death ? 3 : 5);
				sourceTileCoords.X += base.NPC.direction * 3;
				sourceTileCoords.Y -= 10;
				int num2 = (int)base.NPC.ai[1] - scoopRubbleGateValue;
				if (num2 == 0)
				{
					PunchCameraModifier modifier6 = new PunchCameraModifier(base.NPC.Center, new Vector2(0f, -1f), 20f, 6f, 30, 1000f, "Deerclops");
					Main.instance.CameraModifiers.Add(modifier6);
				}
				int rubbleStart = num2;
				int rubbleLimit = rubbleStart + 1;
				if (num2 % 1 != 0)
				{
					rubbleLimit = rubbleStart;
				}
				for (int rubbleIndex = rubbleStart; rubbleIndex < rubbleLimit && rubbleIndex < numRubble; rubbleIndex++)
				{
					ShootRubbleUp(base.NPC, ref sourceTileCoords, numRubble, distancedByThisManyTiles, rubbleIndex, rubble, DebrisDamage, lifeRatio, death);
				}
			}
			if (base.NPC.ai[1] >= 60f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 3:
			if (base.NPC.ai[1] == 30f)
			{
				SoundEngine.PlaySound(in SoundID.DeerclopsScream, base.NPC.Center);
			}
			base.NPC.ai[1]++;
			haltMovement = true;
			if ((int)base.NPC.ai[1] % 4 == 0 && base.NPC.ai[1] >= 28f)
			{
				PunchCameraModifier modifier5 = new PunchCameraModifier(base.NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, "Deerclops");
				Main.instance.CameraModifiers.Add(modifier5);
			}
			if (base.NPC.ai[1] == 30f)
			{
				base.NPC.TargetClosest();
				if (Main.netMode != 1)
				{
					int totalProjectiles2 = (death ? 11 : 9) + (int)MathHelper.Lerp(0f, 7f, 1f - lifeRatio);
					float velocityMultIncrement2 = (float)(totalProjectiles2 + 1) / (float)totalProjectiles2 - 1f;
					float randomRadialOffset2 = MathHelper.ToRadians(MathHelper.Lerp(0f, death ? 270f : 180f, 1f - lifeRatio));
					float radians3 = (float)Math.PI * 2f / (float)totalProjectiles2 + randomRadialOffset2;
					float velocity2 = (death ? 9f : 7f) + MathHelper.Lerp(0f, 3.5f, 1f - lifeRatio);
					Vector2 spinningPoint2 = default(Vector2);
					((Vector2)(ref spinningPoint2))._002Ector(0f, 0f - velocity2);
					for (int i = 0; i < totalProjectiles2; i++)
					{
						Vector2 spinningpoint2 = spinningPoint2;
						double radians4 = radians3 * (float)i;
						center = default(Vector2);
						Vector2 actualVelocity2 = spinningpoint2.RotatedBy(radians4, center);
						float velocityMultiplier2 = 1f - (float)i * velocityMultIncrement2;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Main.player[base.NPC.target].Center + Vector2.Normalize(actualVelocity2) * 550f, actualVelocity2 * velocityMultiplier2 * -1f, shadowHand, HandDamage, 0f, Main.myPlayer);
					}
				}
			}
			if (base.NPC.ai[1] >= 60f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		case 4:
		{
			base.NPC.ai[1]++;
			haltMovement = true;
			base.NPC.TargetClosest();
			MakeSpikesBothSides(base.NPC, 1, targetData, iceSpike, IceSpikeDamage, lifeRatio, death);
			float doubleIceSpikePhaseGateValue = 90f;
			if (base.NPC.ai[1] >= doubleIceSpikePhaseGateValue)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 5:
			if (base.NPC.ai[1] == 30f)
			{
				SoundEngine.PlaySound(in SoundID.DeerclopsScream, base.NPC.Center);
			}
			base.NPC.ai[1]++;
			haltMovement = true;
			if ((int)base.NPC.ai[1] % 4 == 0 && base.NPC.ai[1] >= 28f)
			{
				PunchCameraModifier modifier3 = new PunchCameraModifier(base.NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, "Deerclops");
				Main.instance.CameraModifiers.Add(modifier3);
			}
			if (base.NPC.ai[1] == 30f)
			{
				base.NPC.TargetClosest();
				if (Main.netMode != 1)
				{
					int totalProjectiles = (death ? 20 : 16) + (int)MathHelper.Lerp(0f, 7f, 1f - lifeRatio);
					float velocityMultIncrement = (float)(totalProjectiles + 1) / (float)totalProjectiles - 1f;
					float randomRadialOffset = Main.rand.NextFloat(MathHelper.ToRadians(MathHelper.Lerp(0f, death ? 360f : 270f, 1f - lifeRatio)));
					float radians = (float)Math.PI * 2f / (float)totalProjectiles + randomRadialOffset;
					float velocity = 12f + MathHelper.Lerp(0f, 4f, 1f - lifeRatio);
					Vector2 spinningPoint = default(Vector2);
					((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity);
					for (int k = 0; k < totalProjectiles; k++)
					{
						Vector2 spinningpoint = spinningPoint;
						double radians2 = radians * (float)k;
						center = default(Vector2);
						Vector2 actualVelocity = spinningpoint.RotatedBy(radians2, center);
						float velocityMultiplier = 1f - (float)k * velocityMultIncrement * 0.5f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Main.player[base.NPC.target].Center + Vector2.Normalize(actualVelocity) * 550f, actualVelocity * velocityMultiplier * -1f, shadowHand, HandDamage, 0f, Main.myPlayer);
					}
				}
			}
			if (base.NPC.ai[1] >= 60f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		case 6:
		{
			base.NPC.TargetClosest(faceTarget: false);
			targetData = base.NPC.GetTargetData();
			if (base.NPC.timeLeft > 300)
			{
				base.NPC.timeLeft = 300;
			}
			if (Main.netMode != 1)
			{
				if (!ShouldRunAway(base.NPC, ref targetData, isChasing: false))
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.localAI[1] = 0f;
					base.NPC.netUpdate = true;
					break;
				}
				if (base.NPC.timeLeft <= 0)
				{
					base.NPC.ai[0] = 8f;
					base.NPC.ai[1] = 0f;
					base.NPC.localAI[1] = 0f;
					base.NPC.netUpdate = true;
					break;
				}
			}
			if (base.NPC.direction != base.NPC.oldDirection)
			{
				base.NPC.netUpdate = true;
			}
			goHome = true;
			base.NPC.ai[1]++;
			Vector2 homeVector = default(Vector2);
			((Vector2)(ref homeVector))._002Ector((float)(base.NPC.homeTileX * 16), (float)(base.NPC.homeTileY * 16));
			bool farBelowHome = base.NPC.Top.Y > homeVector.Y + 1600f;
			bool num = base.NPC.Distance(homeVector) < 1020f;
			base.NPC.Distance(targetData.Center);
			float stopMovingGateValue = base.NPC.ai[1] % 600f;
			if (num && stopMovingGateValue < 420f)
			{
				haltMovement = true;
			}
			bool returnHome = false;
			int returnHomeDueToBelowHomeGateValue = 300;
			if (farBelowHome && base.NPC.ai[1] >= (float)returnHomeDueToBelowHomeGateValue)
			{
				returnHome = true;
			}
			int returnHomeDueToFarFromHomeGateValue = 1500;
			if (!num && base.NPC.ai[1] >= (float)returnHomeDueToFarFromHomeGateValue)
			{
				returnHome = true;
			}
			if (returnHome)
			{
				base.NPC.ai[0] = 7f;
				base.NPC.ai[1] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 7:
			if (base.NPC.ai[1] == 30f)
			{
				SoundEngine.PlaySound(in SoundID.DeerclopsScream, base.NPC.Center);
			}
			base.NPC.ai[1]++;
			haltMovement = true;
			if ((int)base.NPC.ai[1] % 4 == 0 && base.NPC.ai[1] >= 28f)
			{
				PunchCameraModifier modifier7 = new PunchCameraModifier(base.NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, "Deerclops");
				Main.instance.CameraModifiers.Add(modifier7);
			}
			if (base.NPC.ai[1] == 40f)
			{
				base.NPC.TargetClosest();
				if (Main.netMode != 1)
				{
					base.NPC.netUpdate = true;
					base.NPC.Bottom = new Vector2((float)(base.NPC.homeTileX * 16), (float)(base.NPC.homeTileY * 16));
				}
			}
			if (base.NPC.ai[1] >= 60f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			break;
		case 8:
			if (base.NPC.ai[1] == 30f)
			{
				SoundEngine.PlaySound(in SoundID.DeerclopsScream, base.NPC.Center);
			}
			base.NPC.ai[1]++;
			haltMovement = true;
			if ((int)base.NPC.ai[1] % 4 == 0 && base.NPC.ai[1] >= 28f)
			{
				PunchCameraModifier modifier2 = new PunchCameraModifier(base.NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, "Deerclops");
				Main.instance.CameraModifiers.Add(modifier2);
			}
			if (base.NPC.ai[1] >= 40f)
			{
				base.NPC.life = -1;
				base.NPC.HitEffect();
				base.NPC.active = false;
				if (Main.netMode != 1)
				{
					NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
				}
				return false;
			}
			break;
		}
		Movement(base.NPC, lifeRatio, haltMovement, goHome, death);
		return false;
	}

	private static bool ShouldRunAway(NPC npc, ref NPCAimedTarget targetData, bool isChasing)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (targetData.Type == NPCTargetType.Player)
		{
			Player player = Main.player[npc.target];
			bool zoneSnow = player.ZoneSnow;
			Vector2 other = default(Vector2);
			((Vector2)(ref other))._002Ector((float)(npc.homeTileX * 16), (float)(npc.homeTileY * 16));
			float distanceToTriggerRunAway = 480f;
			zoneSnow |= player.Distance(other) <= distanceToTriggerRunAway;
			return (player.dead || (!isChasing && !zoneSnow)) | (npc.Distance(player.Center) >= 2400f);
		}
		if (targetData.Type == NPCTargetType.None)
		{
			return true;
		}
		return false;
	}

	private static void SpawnBorderShadowHands(NPC npc, float lifeRatio, int shadowHand, int HandDamage, bool death)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		int shadowHandSpawnRate = (death ? 15 : 20);
		npc.localAI[2]++;
		int shadowHandTimer = (int)npc.localAI[2];
		if (shadowHandTimer % shadowHandSpawnRate != 0)
		{
			return;
		}
		_ = shadowHandTimer / shadowHandSpawnRate;
		if (shadowHandTimer / shadowHandSpawnRate >= 3)
		{
			npc.localAI[2] = 0f;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			float minShadowHandSpawnDistanceFromPlayer = 360f;
			float playerDistanceFromDeerclops = Vector2.Distance(npc.Center, player.Center);
			if (playerDistanceFromDeerclops >= 750f && playerDistanceFromDeerclops <= 1200f)
			{
				Vector2 spawnPosition = npc.Center + (player.Center - npc.Center).SafeNormalize(Vector2.UnitY) * (playerDistanceFromDeerclops + minShadowHandSpawnDistanceFromPlayer);
				float shadowHandVelocity = (death ? 6f : 5f);
				Vector2 spawnVelocity = (player.Center - spawnPosition).SafeNormalize(Vector2.UnitY) * shadowHandVelocity;
				Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPosition, spawnVelocity, shadowHand, HandDamage, 0f, Main.myPlayer);
			}
		}
	}

	private static void ShootRubbleUp(NPC npc, ref Point sourceTileCoords, int howMany, int distancedByThisManyTiles, int whichOne, int rubble, int DebrisDamage, float lifeRatio, bool death)
	{
		int rubbleSpawnLocation = whichOne * distancedByThisManyTiles;
		int maxRubbleSpawnAttempts = 35;
		for (int rubbleSpawnAttempts = 0; rubbleSpawnAttempts < maxRubbleSpawnAttempts; rubbleSpawnAttempts++)
		{
			int posX = sourceTileCoords.X + rubbleSpawnLocation * npc.direction;
			int posY = sourceTileCoords.Y + rubbleSpawnAttempts;
			if (WorldGen.ActiveAndWalkableTile(posX, posY))
			{
				SpawnRubble(npc, posX, posY, howMany, whichOne, rubble, DebrisDamage, lifeRatio, death);
				break;
			}
		}
	}

	private static void SpawnRubble(NPC npc, int posX, int posY, int howMany, int whichOne, int rubble, int DebrisDamage, float lifeRatio, bool death)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		Vector2 rubbleVelocity = Utils.RotatedBy(new Vector2(0f, -1f), (double)((float)(whichOne * npc.direction) * 0.7f * ((float)Math.PI / 4f / (float)howMany)), default(Vector2));
		int ai1_FrameToUse = Main.rand.Next(Main.projFrames[rubble] * 4);
		ai1_FrameToUse = 6 + Main.rand.Next(6);
		float delay = (death ? 24f : 30f);
		float ai2_DelayBeforeGoingUp = (float)(whichOne + 1) * delay;
		float velocityMultiplier = MathHelper.Lerp(0.01f, 0.015f, 1f - lifeRatio);
		Projectile.NewProjectile(npc.GetSource_FromAI(), new Vector2((float)(posX * 16 + 8), (float)(posY * 16 - 8)), rubbleVelocity * velocityMultiplier, rubble, DebrisDamage, 0f, Main.myPlayer, 0f, ai1_FrameToUse, ai2_DelayBeforeGoingUp);
	}

	private static void MakeSpikesForward(NPC npc, int AISLOT_PhaseCounter, NPCAimedTarget targetData, int iceSpike, int IceSpikeDamage, float lifeRatio, bool death)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return;
		}
		int iceSpikeGateValue = 36;
		if (!(npc.ai[AISLOT_PhaseCounter] < (float)iceSpikeGateValue))
		{
			Point sourceTileCoords = npc.Bottom.ToTileCoordinates();
			int numIceSpikes = 20;
			int xOffsetMult = 1;
			sourceTileCoords.X += npc.direction * 3;
			int num = (int)npc.ai[AISLOT_PhaseCounter] - iceSpikeGateValue;
			if (num == 0)
			{
				PunchCameraModifier modifier = new PunchCameraModifier(npc.Center, new Vector2(0f, 1f), 20f, 6f, 30, 1000f, "Deerclops");
				Main.instance.CameraModifiers.Add(modifier);
			}
			int iceSpikeStart = num / 4 * 4;
			int iceSpikeLimit = iceSpikeStart + 4;
			if (num % 4 != 0)
			{
				iceSpikeLimit = iceSpikeStart;
			}
			float iceSpikeScaleIncrease = MathHelper.Lerp(1f, 2f, 1f - lifeRatio);
			for (int i = iceSpikeStart; i < iceSpikeLimit && i < numIceSpikes; i++)
			{
				int xOffset = (int)Math.Round((float)(i * xOffsetMult) * iceSpikeScaleIncrease);
				TryMakingSpike(npc, ref sourceTileCoords, npc.direction, numIceSpikes, i, xOffset, iceSpike, IceSpikeDamage, lifeRatio, death);
			}
		}
	}

	private static void MakeSpikesBothSides(NPC npc, int AISLOT_PhaseCounter, NPCAimedTarget targetData, int iceSpike, int IceSpikeDamage, float lifeRatio, bool death)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return;
		}
		int iceSpikeGateValue = 56;
		if (!(npc.ai[AISLOT_PhaseCounter] < (float)iceSpikeGateValue))
		{
			Point sourceTileCoords = npc.Bottom.ToTileCoordinates();
			int numIceSpikes = 15;
			int xOffsetMult = 1;
			int num = (int)npc.ai[AISLOT_PhaseCounter] - iceSpikeGateValue;
			if (num == 0)
			{
				PunchCameraModifier modifier = new PunchCameraModifier(npc.Center, new Vector2(0f, 1f), 20f, 6f, 30, 1000f, "Deerclops");
				Main.instance.CameraModifiers.Add(modifier);
			}
			int iceSpikeStart = num / 2 * 2;
			int iceSpikeLimit = iceSpikeStart + 2;
			if (num % 2 != 0)
			{
				iceSpikeLimit = iceSpikeStart;
			}
			float iceSpikeScaleIncrease = MathHelper.Lerp(1f, 2f, 1f - lifeRatio);
			for (int iceSpikeIndex = iceSpikeStart; iceSpikeIndex >= 0 && iceSpikeIndex < iceSpikeLimit && iceSpikeIndex < numIceSpikes; iceSpikeIndex++)
			{
				int xOffset = (int)Math.Round((float)(iceSpikeIndex * xOffsetMult) * iceSpikeScaleIncrease);
				TryMakingSpike(npc, ref sourceTileCoords, npc.direction, numIceSpikes, -iceSpikeIndex, xOffset, iceSpike, IceSpikeDamage, lifeRatio, death);
				TryMakingSpike(npc, ref sourceTileCoords, -npc.direction, numIceSpikes, -iceSpikeIndex, xOffset, iceSpike, IceSpikeDamage, lifeRatio, death);
			}
		}
	}

	private static void TryMakingSpike(NPC npc, ref Point sourceTileCoords, int dir, int howMany, int whichOne, int xOffset, int iceSpike, int IceSpikeDamage, float lifeRatio, bool death)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		int posX = sourceTileCoords.X + xOffset * dir;
		int posY = FindBestY(npc, ref sourceTileCoords, posX);
		if (WorldGen.ActiveAndWalkableTile(posX, posY))
		{
			Vector2 iceSpikeSpawnPos = default(Vector2);
			((Vector2)(ref iceSpikeSpawnPos))._002Ector((float)(posX * 16 + 8), (float)(posY * 16 - 8));
			Vector2 iceSpikeVelocity = Utils.RotatedBy(new Vector2(0f, -1f), (double)((float)(whichOne * dir) * 0.7f * ((float)Math.PI / 4f / (float)howMany)), default(Vector2));
			float iceSpikeScale = 0.1f + Main.rand.NextFloat() * 0.1f + (float)xOffset * 1.1f / (float)howMany;
			Projectile.NewProjectile(npc.GetSource_FromAI(), iceSpikeSpawnPos, iceSpikeVelocity, iceSpike, IceSpikeDamage, 0f, Main.myPlayer, 0f, iceSpikeScale);
		}
	}

	private static int FindBestY(NPC npc, ref Point sourceTileCoords, int x)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		int bestY = sourceTileCoords.Y;
		NPCAimedTarget targetData = npc.GetTargetData();
		if (!targetData.Invalid)
		{
			Rectangle hitbox = targetData.Hitbox;
			Vector2 vector = default(Vector2);
			((Vector2)(ref vector))._002Ector((float)((Rectangle)(ref hitbox)).Center.X, (float)((Rectangle)(ref hitbox)).Bottom);
			int num = (int)(vector.Y / 16f);
			int sign = Math.Sign(num - bestY);
			int y2 = num + sign * 15;
			int? potentialBestY = null;
			float yLimit = float.PositiveInfinity;
			for (int i = bestY; i != y2; i += sign)
			{
				if (WorldGen.ActiveAndWalkableTile(x, i))
				{
					float newYLimit = Utils.ToWorldCoordinates(new Point(x, i), 8f, 8f).Distance(vector);
					if (!potentialBestY.HasValue || !(newYLimit >= yLimit))
					{
						potentialBestY = i;
						yLimit = newYLimit;
					}
				}
			}
			if (potentialBestY.HasValue)
			{
				bestY = potentialBestY.Value;
			}
		}
		for (int j = 0; j < 20; j++)
		{
			if (bestY < 10)
			{
				break;
			}
			if (!WorldGen.SolidTile(x, bestY))
			{
				break;
			}
			bestY--;
		}
		for (int k = 0; k < 20; k++)
		{
			if (bestY > Main.maxTilesY - 10)
			{
				break;
			}
			if (WorldGen.ActiveAndWalkableTile(x, bestY))
			{
				break;
			}
			bestY++;
		}
		return bestY;
	}

	private static void Movement(NPC npc, float lifeRatio, bool haltMovement, bool goHome, bool death)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		float moveSpeed = MathHelper.Lerp(death ? 4f : 3.5f, death ? 6f : 5f, 1f - lifeRatio);
		float moveSpeedDivisor = 4f;
		float yVelocityIncrease = (death ? (-0.5f) : (-0.4f));
		float yVelocityMin = (death ? (-12f) : (-8f));
		float yVelocityIncrease2 = (death ? 0.5f : 0.4f);
		Rectangle targetHitbox = npc.GetTargetData().Hitbox;
		if (goHome)
		{
			((Rectangle)(ref targetHitbox))._002Ector(npc.homeTileX * 16, npc.homeTileY * 16, 16, 16);
			if (npc.Distance(((Rectangle)(ref targetHitbox)).Center.ToVector2()) < 240f)
			{
				targetHitbox.X = (int)(npc.Center.X + (float)(160 * npc.direction));
			}
		}
		float distanceFromTargetX = (float)((Rectangle)(ref targetHitbox)).Center.X - npc.Center.X;
		float num = Math.Abs(distanceFromTargetX);
		if (goHome && distanceFromTargetX != 0f)
		{
			npc.direction = (npc.spriteDirection = Math.Sign(distanceFromTargetX));
		}
		bool closeToTarget = num < 80f;
		bool stopMoving = closeToTarget | haltMovement;
		if (npc.ai[0] == -1f)
		{
			distanceFromTargetX = 5f;
			moveSpeed = 5.35f;
			stopMoving = false;
		}
		if (stopMoving)
		{
			npc.velocity.X *= 0.8f;
			if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
			{
				npc.velocity.X = 0f;
			}
		}
		else
		{
			int moveDirection = Math.Sign(distanceFromTargetX);
			npc.velocity.X = MathHelper.Lerp(npc.velocity.X, (float)moveDirection * moveSpeed, 1f / moveSpeedDivisor);
		}
		int npcCenterXOffset = 40;
		int npcCenterYOffset = 20;
		int gfxOffsetY = 0;
		Vector2 npcCenter = default(Vector2);
		((Vector2)(ref npcCenter))._002Ector(npc.Center.X - (float)(npcCenterXOffset / 2), npc.position.Y + (float)npc.height - (float)npcCenterYOffset + (float)gfxOffsetY);
		bool num2 = npcCenter.X < (float)targetHitbox.X && npcCenter.X + (float)npc.width > (float)(targetHitbox.X + targetHitbox.Width);
		bool aboveTarget = npcCenter.Y + (float)npcCenterYOffset < (float)(targetHitbox.Y + targetHitbox.Height - 16);
		bool acceptTopSurfaces = npc.Bottom.Y >= (float)((Rectangle)(ref targetHitbox)).Top;
		bool insideTiles = Collision.SolidCollision(npcCenter, npcCenterXOffset, npcCenterYOffset, acceptTopSurfaces);
		bool insideTiles2 = Collision.SolidCollision(npcCenter, npcCenterXOffset, npcCenterYOffset - 4, acceptTopSurfaces);
		bool moveUp = !Collision.SolidCollision(npcCenter + new Vector2((float)(npcCenterXOffset * npc.direction), 0f), 16, 80, acceptTopSurfaces);
		float yVelocity = (death ? (-12f) : (-8f));
		if (insideTiles | insideTiles2)
		{
			npc.localAI[0] = 0f;
		}
		if ((num2 | closeToTarget) & aboveTarget)
		{
			npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y + yVelocityIncrease2 * 2f, 0.001f, 16f);
		}
		else if (insideTiles && !insideTiles2)
		{
			npc.velocity.Y = 0f;
		}
		else if (insideTiles)
		{
			npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y + yVelocityIncrease, yVelocityMin, 0f);
		}
		else if (((npc.velocity.Y == 0f) & moveUp) && npc.localAI[0] == 0f)
		{
			npc.velocity.Y = yVelocity;
			npc.localAI[0] = 1f;
		}
		else
		{
			npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y + yVelocityIncrease2, yVelocity, 16f);
		}
	}
}
