using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HauntedDishes : ModProjectile, ILocalizedModType, IModType
{
	public float dust;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 19;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a58: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		base.Projectile.Calamity();
		if (dust == 0f)
		{
			int dustAmt = 36;
			for (int i = 0; i < dustAmt; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int dusty = Dust.NewDust(val + faceDirection, 0, 0, 7, faceDirection.X * 1.1f, faceDirection.Y * 1.1f, 100, default(Color), 1.4f);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].noLight = true;
				Main.dust[dusty].velocity = faceDirection;
			}
			dust++;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<HauntedDishes>();
		player.AddBuff(ModContent.BuffType<HauntedDishesBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.hauntedDishes = false;
			}
			if (modPlayer.hauntedDishes)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		bool minionMovingLeft = false;
		bool minionMovingRight = false;
		bool minionBelowPlayer = false;
		bool minionShouldJump = false;
		if (base.Projectile.lavaWet)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.ai[1] = 0f;
		}
		int idlePos = 40 * (base.Projectile.minionPos + 1) * player.direction;
		if (player.position.X + (float)(player.width / 2) < base.Projectile.position.X + (float)(base.Projectile.width / 2) - 10f + (float)idlePos)
		{
			minionMovingLeft = true;
		}
		else if (player.position.X + (float)(player.width / 2) > base.Projectile.position.X + (float)(base.Projectile.width / 2) + 10f + (float)idlePos)
		{
			minionMovingRight = true;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			int conflict1 = 500;
			conflict1 += 40 * base.Projectile.minionPos;
			if (base.Projectile.localAI[0] > 0f)
			{
				conflict1 += 500;
			}
			Vector2 idleMinionPos = base.Projectile.Center;
			float num2 = player.position.X + (float)(player.width / 2) - idleMinionPos.X;
			float playerY = player.position.Y + (float)(player.height / 2) - idleMinionPos.Y;
			float num3 = (float)Math.Sqrt(num2 * num2 + playerY * playerY);
			if (num3 > 1500f)
			{
				base.Projectile.ai[0] = 1f;
			}
			if (num3 > 2000f)
			{
				base.Projectile.position.X = player.position.X + (float)(player.width / 2) - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.position.Y + (float)(player.height / 2) - (float)(base.Projectile.height / 2);
			}
		}
		if (base.Projectile.ai[0] != 0f)
		{
			base.Projectile.tileCollide = false;
			float npcDetectRange = 1200f;
			bool npcFound = false;
			int targetIndex = -1;
			for (int index = 0; index < Main.maxNPCs; index++)
			{
				NPC npc2 = Main.npc[index];
				if (!npc2.CanBeChasedBy(base.Projectile))
				{
					continue;
				}
				float npcX = npc2.position.X + (float)(npc2.width / 2);
				float npcY = npc2.position.Y + (float)(npc2.height / 2);
				if (Math.Abs(player.position.X + (float)(player.width / 2) - npcX) + Math.Abs(player.position.Y + (float)(player.height / 2) - npcY) < npcDetectRange)
				{
					if (Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc2.position, npc2.width, npc2.height))
					{
						targetIndex = index;
					}
					npcFound = true;
					break;
				}
			}
			if (npcFound && targetIndex >= 0)
			{
				base.Projectile.ai[0] = 0f;
			}
			Vector2 returningMinionPos = base.Projectile.Center;
			float xDist = player.position.X + (float)(player.width / 2) - returningMinionPos.X;
			xDist -= (float)(40 * player.direction);
			if (!npcFound)
			{
				xDist -= (float)(40 * base.Projectile.minionPos * player.direction);
			}
			float yDist = player.position.Y + (float)(player.height / 2) - returningMinionPos.Y;
			yDist -= 60f;
			float playerDist2 = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
			float minionReturnSpeed = playerDist2;
			float minionReturnAccel = 0.4f;
			if (12f < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
			{
				Math.Abs(player.velocity.X);
				Math.Abs(player.velocity.Y);
			}
			if (playerDist2 < 100f && player.velocity.Y == 0f && base.Projectile.position.Y + (float)base.Projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				if (base.Projectile.velocity.Y < -6f)
				{
					base.Projectile.velocity.Y = -6f;
				}
			}
			if (playerDist2 > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (playerDist2 < 50f)
			{
				if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 0.99f;
				}
				minionReturnAccel = 0.01f;
			}
			else
			{
				if (playerDist2 < 100f)
				{
					minionReturnAccel = 0.1f;
				}
				if (playerDist2 > 300f)
				{
					minionReturnAccel = 1f;
				}
				playerDist2 = minionReturnSpeed / playerDist2;
				xDist *= playerDist2;
				yDist *= playerDist2;
			}
			if (base.Projectile.velocity.X < xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + minionReturnAccel;
				if (minionReturnAccel > 0.05f && base.Projectile.velocity.X < 0f)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X + minionReturnAccel;
				}
			}
			if (base.Projectile.velocity.X > xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - minionReturnAccel;
				if (minionReturnAccel > 0.05f && base.Projectile.velocity.X > 0f)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X - minionReturnAccel;
				}
			}
			if (base.Projectile.velocity.Y < yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + minionReturnAccel;
				if (minionReturnAccel > 0.05f && base.Projectile.velocity.Y < 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y + minionReturnAccel * 2f;
				}
			}
			if (base.Projectile.velocity.Y > yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - minionReturnAccel;
				if (minionReturnAccel > 0.05f && base.Projectile.velocity.Y > 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y - minionReturnAccel * 2f;
				}
			}
			if (base.Projectile.frame < 15)
			{
				base.Projectile.frame = 15;
			}
			else
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 3)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame >= 19)
				{
					base.Projectile.frame = 15;
				}
			}
			if (base.Projectile.velocity.X > 0.5f)
			{
				base.Projectile.spriteDirection = 1;
			}
			else if (base.Projectile.velocity.X < -0.5f)
			{
				base.Projectile.spriteDirection = -1;
			}
			base.Projectile.rotation = ((base.Projectile.spriteDirection != 1) ? (base.Projectile.velocity.ToRotation() + (float)Math.PI) : base.Projectile.velocity.ToRotation());
			return;
		}
		float exaggeratedMinionPos = 40 * base.Projectile.minionPos;
		float attackCooldown = 30f;
		base.Projectile.localAI[0]--;
		if (base.Projectile.localAI[0] < 0f)
		{
			base.Projectile.localAI[0] = 0f;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]--;
		}
		else
		{
			float minionXTarget = base.Projectile.position.X;
			float minionYTarget = base.Projectile.position.Y;
			float minionAttackMaxDist = 100000f;
			float minionAttackDistance = minionAttackMaxDist;
			int minionAttackIndex = -1;
			NPC minionAttackTargetNpc = base.Projectile.OwnerMinionAttackTargetNPC;
			if (minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy(base.Projectile))
			{
				float minionTargetXDist = minionAttackTargetNpc.position.X + (float)(minionAttackTargetNpc.width / 2);
				float minionTargetYDist = minionAttackTargetNpc.position.Y + (float)(minionAttackTargetNpc.height / 2);
				float minionTargetDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - minionTargetXDist) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - minionTargetYDist);
				if (minionTargetDist < minionAttackMaxDist)
				{
					if (minionAttackIndex == -1 && minionTargetDist <= minionAttackDistance)
					{
						minionAttackDistance = minionTargetDist;
						minionXTarget = minionTargetXDist;
						minionYTarget = minionTargetYDist;
					}
					if (Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, minionAttackTargetNpc.position, minionAttackTargetNpc.width, minionAttackTargetNpc.height))
					{
						minionAttackMaxDist = minionTargetDist;
						minionXTarget = minionTargetXDist;
						minionYTarget = minionTargetYDist;
						minionAttackIndex = minionAttackTargetNpc.whoAmI;
					}
				}
			}
			if (minionAttackIndex == -1)
			{
				for (int j = 0; j < Main.maxNPCs; j++)
				{
					if (!Main.npc[j].CanBeChasedBy(base.Projectile))
					{
						continue;
					}
					float minionTargetXDist2 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
					float minionTargetYDist2 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
					float minionTargetDist2 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - minionTargetXDist2) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - minionTargetYDist2);
					if (minionTargetDist2 < minionAttackMaxDist)
					{
						if (minionAttackIndex == -1 && minionTargetDist2 <= minionAttackDistance)
						{
							minionAttackDistance = minionTargetDist2;
							minionXTarget = minionTargetXDist2;
							minionYTarget = minionTargetYDist2;
						}
						if (Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
						{
							minionAttackMaxDist = minionTargetDist2;
							minionXTarget = minionTargetXDist2;
							minionYTarget = minionTargetYDist2;
							minionAttackIndex = j;
						}
					}
				}
			}
			if (minionAttackIndex == -1 && minionAttackDistance < minionAttackMaxDist)
			{
				minionAttackMaxDist = minionAttackDistance;
			}
			float yDependentTargeting = 400f;
			if ((double)base.Projectile.position.Y > Main.worldSurface * 16.0)
			{
				yDependentTargeting = 200f;
			}
			if (minionAttackMaxDist < yDependentTargeting + exaggeratedMinionPos && minionAttackIndex == -1)
			{
				float minionTargetXDist3 = minionXTarget - (base.Projectile.position.X + (float)(base.Projectile.width / 2));
				if (minionTargetXDist3 < -5f)
				{
					minionMovingLeft = true;
					minionMovingRight = false;
				}
				else if (minionTargetXDist3 > 5f)
				{
					minionMovingRight = true;
					minionMovingLeft = false;
				}
			}
			else if (minionAttackIndex >= 0 && minionAttackMaxDist < 800f + exaggeratedMinionPos)
			{
				base.Projectile.localAI[0] = 60f;
				float minionTargetXDist4 = minionXTarget - (base.Projectile.position.X + (float)(base.Projectile.width / 2));
				if (minionTargetXDist4 > 300f || minionTargetXDist4 < -300f)
				{
					if (minionTargetXDist4 < -50f)
					{
						minionMovingLeft = true;
						minionMovingRight = false;
					}
					else if (minionTargetXDist4 > 50f)
					{
						minionMovingRight = true;
						minionMovingLeft = false;
					}
				}
				else if (base.Projectile.owner == Main.myPlayer)
				{
					base.Projectile.ai[1] = attackCooldown;
					Vector2 projMinionPos = default(Vector2);
					((Vector2)(ref projMinionPos))._002Ector(base.Projectile.Center.X, base.Projectile.Center.Y - 8f);
					float plateTargetX = minionXTarget - projMinionPos.X + Main.rand.NextFloat(-6f, 6f);
					float randomPlateYOffset = (float)((double)(Math.Abs(plateTargetX) * 0.1f) * (double)Main.rand.Next(0, 100) * 0.001);
					float plateTargetY = minionYTarget - projMinionPos.Y + Main.rand.NextFloat(-6f, 6f) - randomPlateYOffset;
					double plateTargetDist = Math.Sqrt((double)plateTargetX * (double)plateTargetX + (double)plateTargetY * (double)plateTargetY);
					float plateVelocity = (float)(12.0 / plateTargetDist);
					float SpeedX = plateTargetX * plateVelocity;
					float SpeedY = plateTargetY * plateVelocity;
					int damage = base.Projectile.damage;
					int Type = ModContent.ProjectileType<PlateProjectile>();
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projMinionPos.X, projMinionPos.Y, SpeedX * 2f, SpeedY * 2f, Type, damage, base.Projectile.knockBack, base.Projectile.owner);
					if (SpeedX < 0f)
					{
						base.Projectile.direction = -1;
					}
					if (SpeedX > 0f)
					{
						base.Projectile.direction = 1;
					}
					base.Projectile.netUpdate = true;
				}
			}
		}
		if (base.Projectile.ai[1] != 0f)
		{
			minionMovingLeft = false;
			minionMovingRight = false;
		}
		else if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.direction = player.direction;
		}
		base.Projectile.rotation = 0f;
		base.Projectile.tileCollide = true;
		float groundMinionAccel = 0.08f;
		float groundMinionMaxVel = 6.5f;
		groundMinionMaxVel = 6f;
		groundMinionAccel = 0.2f;
		if (groundMinionMaxVel < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
		{
			groundMinionMaxVel = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
			groundMinionAccel = 0.3f;
		}
		if (minionMovingLeft)
		{
			if (base.Projectile.velocity.X > -3.5f)
			{
				base.Projectile.velocity.X -= groundMinionAccel;
			}
			else
			{
				base.Projectile.velocity.X -= groundMinionAccel * 0.25f;
			}
		}
		else if (minionMovingRight)
		{
			if (base.Projectile.velocity.X < 3.5f)
			{
				base.Projectile.velocity.X += groundMinionAccel;
			}
			else
			{
				base.Projectile.velocity.X += groundMinionAccel * 0.25f;
			}
		}
		else
		{
			base.Projectile.velocity.X *= 0.9f;
			if (base.Projectile.velocity.X >= 0f - groundMinionAccel && base.Projectile.velocity.X <= groundMinionAccel)
			{
				base.Projectile.velocity.X = 0f;
			}
		}
		if (minionMovingLeft | minionMovingRight)
		{
			int minionTileX = (int)((double)base.Projectile.position.X + (double)(base.Projectile.width / 2)) / 16;
			int j2 = (int)((double)base.Projectile.position.Y + (double)(base.Projectile.height / 2)) / 16;
			if (minionMovingLeft)
			{
				minionTileX--;
			}
			if (minionMovingRight)
			{
				minionTileX++;
			}
			if (WorldGen.SolidTile(minionTileX + (int)base.Projectile.velocity.X, j2))
			{
				minionShouldJump = true;
			}
		}
		if (player.position.Y + (float)player.height - 8f > base.Projectile.position.Y + (float)base.Projectile.height)
		{
			minionBelowPlayer = true;
		}
		Collision.StepUp(ref base.Projectile.position, ref base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ref base.Projectile.stepSpeed, ref base.Projectile.gfxOffY);
		if (base.Projectile.velocity.Y == 0f)
		{
			if (!minionBelowPlayer && ((double)base.Projectile.velocity.X < 0.0 || (double)base.Projectile.velocity.X > 0.0))
			{
				int i2 = (int)((double)base.Projectile.position.X + (double)(base.Projectile.width / 2)) / 16;
				int j3 = (int)((double)base.Projectile.position.Y + (double)(base.Projectile.height / 2)) / 16 + 1;
				if (minionMovingLeft)
				{
					i2--;
				}
				if (minionMovingRight)
				{
					i2++;
				}
				WorldGen.SolidTile(i2, j3);
			}
			if (minionShouldJump)
			{
				int i3 = (int)((double)base.Projectile.position.X + (double)(base.Projectile.width / 2)) / 16;
				int j4 = (int)((double)base.Projectile.position.Y + (double)base.Projectile.height) / 16 + 1;
				if (WorldGen.SolidTile(i3, j4) || Main.tile[i3, j4].IsHalfBlock || Main.tile[i3, j4].Slope > SlopeType.Solid || base.Projectile.type == 200)
				{
					if (base.Projectile.type == 200)
					{
						base.Projectile.velocity.Y = -3.1f;
					}
					else
					{
						try
						{
							int minionJumpTileX = (int)((double)base.Projectile.position.X + (double)(base.Projectile.width / 2)) / 16;
							int minionJumpTileY = (int)((double)base.Projectile.position.Y + (double)(base.Projectile.height / 2)) / 16;
							if (minionMovingLeft)
							{
								minionJumpTileX--;
							}
							if (minionMovingRight)
							{
								minionJumpTileX++;
							}
							int i4 = minionJumpTileX + (int)base.Projectile.velocity.X;
							if (!WorldGen.SolidTile(i4, minionJumpTileY - 1) && !WorldGen.SolidTile(i4, minionJumpTileY - 2))
							{
								base.Projectile.velocity.Y = -5.1f;
							}
							else if (!WorldGen.SolidTile(i4, minionJumpTileY - 2))
							{
								base.Projectile.velocity.Y = -7.1f;
							}
							else if (WorldGen.SolidTile(i4, minionJumpTileY - 5))
							{
								base.Projectile.velocity.Y = -11.1f;
							}
							else if (WorldGen.SolidTile(i4, minionJumpTileY - 4))
							{
								base.Projectile.velocity.Y = -10.1f;
							}
							else
							{
								base.Projectile.velocity.Y = -9.1f;
							}
						}
						catch
						{
							base.Projectile.velocity.Y = -9.1f;
						}
					}
				}
			}
			else if (base.Projectile.type == 266 && (minionMovingLeft | minionMovingRight))
			{
				base.Projectile.velocity.Y -= 6f;
			}
		}
		if (base.Projectile.velocity.X > groundMinionMaxVel)
		{
			base.Projectile.velocity.X = groundMinionMaxVel;
		}
		if (base.Projectile.velocity.X < 0f - groundMinionMaxVel)
		{
			base.Projectile.velocity.X = 0f - groundMinionMaxVel;
		}
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.direction = -1;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		if ((base.Projectile.velocity.X > groundMinionAccel) & minionMovingRight)
		{
			base.Projectile.direction = 1;
		}
		if ((base.Projectile.velocity.X < 0f - groundMinionAccel) & minionMovingLeft)
		{
			base.Projectile.direction = -1;
		}
		if (base.Projectile.direction == -1)
		{
			base.Projectile.spriteDirection = -1;
		}
		if (base.Projectile.direction == 1)
		{
			base.Projectile.spriteDirection = 1;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			if (base.Projectile.localAI[1] == 0f)
			{
				base.Projectile.localAI[1] = 1f;
				base.Projectile.frame = 9;
			}
			if (base.Projectile.frame >= 9 && base.Projectile.frame <= 14)
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 8)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame == 14)
				{
					base.Projectile.frame = 9;
				}
			}
		}
		else if (base.Projectile.velocity.Y == 0f)
		{
			base.Projectile.localAI[1] = 0f;
			if (base.Projectile.velocity.X == 0f)
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 4)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame >= 3)
				{
					base.Projectile.frame = 0;
				}
			}
			else if (base.Projectile.velocity.X < -0.8f || base.Projectile.velocity.X > 0.8f)
			{
				base.Projectile.frameCounter = base.Projectile.frameCounter + (int)Math.Abs(base.Projectile.velocity.X);
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 20)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame < 4)
				{
					base.Projectile.frame = 3;
				}
				if (base.Projectile.frame >= 9)
				{
					base.Projectile.frame = 3;
				}
			}
			else
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 4)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame >= 3)
				{
					base.Projectile.frame = 0;
				}
			}
		}
		else if (base.Projectile.velocity.Y < 0f)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = 3;
		}
		else if (base.Projectile.velocity.Y > 0f)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = 3;
		}
		base.Projectile.velocity.Y += 0.4f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
		_ = base.Projectile.velocity;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			int index = Gore.NewGore(base.Projectile.GetSource_Death(), new Vector2(base.Projectile.position.X - (float)(base.Projectile.width / 2), base.Projectile.position.Y - (float)(base.Projectile.height / 2)), new Vector2(0f, 0f), Main.rand.Next(61, 64), base.Projectile.scale);
			Gore obj = Main.gore[index];
			obj.velocity *= 0.1f;
		}
	}
}
