using System;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class StormjawBaby : ModProjectile, ILocalizedModType, IModType
{
	public float dust;

	private int sparkCounter;

	private int targetIndex;

	private Vector2 idlePos;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player player => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 56;
		base.Projectile.height = 38;
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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		OnSpawn();
		SummonChecks();
		idlePos = player.Center;
		idlePos.X -= (15f + (float)player.width / 2f) * (float)player.direction;
		idlePos.X -= (float)base.Projectile.minionPos * 40f * (float)player.direction;
		FindTarget();
		FlyBackToPlayer();
		AttackTarget();
		GoToTarget();
		IdleBehavior();
	}

	private void OnSpawn()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		if (dust == 0f)
		{
			int dustAmt = 36;
			for (int d = 0; d < dustAmt; d++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(d - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 dustVel = val - base.Projectile.Center;
				int spark = Dust.NewDust(val + dustVel, 0, 0, 132, dustVel.X * 1.1f, dustVel.Y * 1.1f, 100, default(Color), 1.4f);
				Main.dust[spark].noGravity = true;
				Main.dust[spark].noLight = true;
				Main.dust[spark].velocity = dustVel;
			}
			dust++;
		}
	}

	private void SummonChecks()
	{
		bool num = base.Projectile.type == ModContent.ProjectileType<StormjawBaby>();
		player.AddBuff(ModContent.BuffType<BabyStormlionBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				player.Calamity().stormjaw = false;
			}
			if (player.Calamity().stormjaw)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	private void FindTarget()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		targetIndex = -1;
		float maxDistance = 800f;
		if (base.Projectile.ai[0] != 0f)
		{
			return;
		}
		NPC targetedNPC = base.Projectile.OwnerMinionAttackTargetNPC;
		Vector2 val;
		if (targetedNPC != null && targetedNPC.CanBeChasedBy(base.Projectile))
		{
			val = targetedNPC.Center - base.Projectile.Center;
			float num1 = ((Vector2)(ref val)).Length();
			if (num1 < maxDistance)
			{
				targetIndex = targetedNPC.whoAmI;
				maxDistance = num1;
			}
		}
		if (targetIndex >= 0)
		{
			return;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.CanBeChasedBy(base.Projectile))
			{
				val = npc.Center - base.Projectile.Center;
				float num2 = ((Vector2)(ref val)).Length();
				if (num2 < maxDistance)
				{
					targetIndex = npc.whoAmI;
					maxDistance = num2;
				}
			}
		}
	}

	private void FlyBackToPlayer()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] != 1f)
		{
			return;
		}
		base.Projectile.tileCollide = false;
		Vector2 returnPos = player.Center - base.Projectile.Center;
		returnPos.X -= 40 * player.direction;
		returnPos.X -= 40 * base.Projectile.minionPos * player.direction;
		returnPos.Y -= 60f;
		float playerDist = ((Vector2)(ref returnPos)).Length();
		float returnSpeed = 12f;
		float acceleration = 0.4f;
		if (returnSpeed < ((Vector2)(ref base.Projectile.velocity)).Length())
		{
			returnSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		if (playerDist < 100f && player.velocity.Y == 0f && base.Projectile.Bottom.Y <= player.Bottom.Y && !Collision.SolidCollision(base.Projectile.Center, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			if (base.Projectile.velocity.Y < -6f)
			{
				base.Projectile.velocity.Y = -6f;
			}
		}
		if (playerDist > 2000f)
		{
			base.Projectile.position = player.Center - base.Projectile.Size / 2f;
			base.Projectile.netUpdate = true;
		}
		if (playerDist < 50f)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 2f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.99f;
			}
			acceleration = 0.01f;
		}
		else
		{
			if (playerDist < 100f)
			{
				acceleration = 0.1f;
			}
			if (playerDist > 300f)
			{
				acceleration = 1f;
			}
			playerDist = returnSpeed / playerDist;
			returnPos *= playerDist;
		}
		if (base.Projectile.velocity.X < returnPos.X)
		{
			base.Projectile.velocity.X += acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X += acceleration;
			}
		}
		if (base.Projectile.velocity.X > returnPos.X)
		{
			base.Projectile.velocity.X -= acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X -= acceleration;
			}
		}
		if (base.Projectile.velocity.Y < returnPos.Y)
		{
			base.Projectile.velocity.Y += acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y += acceleration * 2f;
			}
		}
		if (base.Projectile.velocity.Y > returnPos.Y)
		{
			base.Projectile.velocity.Y -= acceleration;
			if (acceleration > 0.05f && base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= acceleration * 2f;
			}
		}
		if (base.Projectile.frame < 6 || base.Projectile.frame > 9)
		{
			base.Projectile.frame = 6;
		}
		else
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 3)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame >= 10)
			{
				base.Projectile.frame = 6;
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
		base.Projectile.rotation = ((base.Projectile.velocity.ToRotation() + (float)base.Projectile.spriteDirection != 1f) ? ((float)Math.PI) : 0f);
	}

	private void AttackTarget()
	{
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
			base.Projectile.rotation = 0f;
			if (base.Projectile.velocity.Y == 0f)
			{
				if (base.Projectile.velocity.X == 0f)
				{
					base.Projectile.frame = 0;
					base.Projectile.frameCounter = 0;
				}
				else if (Math.Abs(base.Projectile.velocity.X) >= 0.5f)
				{
					base.Projectile.frameCounter += (int)Math.Abs(base.Projectile.velocity.X);
					base.Projectile.frameCounter++;
					if (base.Projectile.frameCounter > 10)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
					if (base.Projectile.frame >= 6)
					{
						base.Projectile.frame = 0;
					}
				}
				else
				{
					base.Projectile.frame = 0;
					base.Projectile.frameCounter = 0;
				}
			}
			else if (base.Projectile.velocity.Y != 0f)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame = 0;
			}
			base.Projectile.velocity.Y += 0.4f;
			if (base.Projectile.velocity.Y > 10f)
			{
				base.Projectile.velocity.Y = 10f;
			}
			sparkCounter += Main.rand.Next(1, 4);
			if (sparkCounter >= 20 && Main.myPlayer == base.Projectile.owner)
			{
				Vector2 sparkS = default(Vector2);
				for (int i = 0; i < Main.rand.Next(1, 4); i++)
				{
					((Vector2)(ref sparkS))._002Ector(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sparkS, ModContent.ProjectileType<StormjawSpark>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				sparkCounter = 0;
			}
			base.Projectile.ai[1]--;
			if (base.Projectile.ai[1] <= 0f)
			{
				base.Projectile.ai[1] = 0f;
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector((int)(base.Projectile.position.X + base.Projectile.velocity.X * 0.5f - 4f), (int)(base.Projectile.position.Y + base.Projectile.velocity.Y * 0.5f - 4f), base.Projectile.width + 8, base.Projectile.height + 8);
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		Vector2 sparkS2 = default(Vector2);
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.CanBeChasedBy(base.Projectile) || npc.immune[base.Projectile.owner] > 0)
			{
				continue;
			}
			Rectangle rect = npc.getRect();
			if (!((Rectangle)(ref rectangle)).Intersects(rect) || (!npc.noTileCollide && !player.CanHit(npc)))
			{
				continue;
			}
			sparkCounter += Main.rand.Next(1, 3);
			if (sparkCounter >= 20 && Main.myPlayer == base.Projectile.owner)
			{
				for (int j = 0; j < Main.rand.Next(1, 4); j++)
				{
					((Vector2)(ref sparkS2))._002Ector(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sparkS2, ModContent.ProjectileType<StormjawSpark>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				sparkCounter = 0;
			}
		}
	}

	private void GoToTarget()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		if (targetIndex < 0)
		{
			return;
		}
		float rangeofSight = 700f;
		float attackZone = 20f;
		if ((double)base.Projectile.position.Y > Main.worldSurface * 16.0)
		{
			rangeofSight *= 0.7f;
		}
		NPC npc = Main.npc[targetIndex];
		Vector2 val = npc.Center - base.Projectile.Center;
		float num = ((Vector2)(ref val)).Length();
		Collision.CanHit(base.Projectile.Center, base.Projectile.width, base.Projectile.height, npc.Center, npc.width, npc.height);
		if (num < rangeofSight)
		{
			idlePos = npc.Center;
			if (npc.Center.Y < base.Projectile.Center.Y - 30f && base.Projectile.velocity.Y == 0f)
			{
				float targetYDist = Math.Abs(npc.Center.Y - base.Projectile.Center.Y);
				if (targetYDist < 120f)
				{
					base.Projectile.velocity.Y = -10f;
				}
				else if (targetYDist < 210f)
				{
					base.Projectile.velocity.Y = -13f;
				}
				else if (targetYDist < 270f)
				{
					base.Projectile.velocity.Y = -15f;
				}
				else if (targetYDist < 310f)
				{
					base.Projectile.velocity.Y = -17f;
				}
				else if (targetYDist < 380f)
				{
					base.Projectile.velocity.Y = -18f;
				}
			}
		}
		if (num < attackZone)
		{
			base.Projectile.ai[0] = 2f;
			base.Projectile.ai[1] = 15f;
			base.Projectile.netUpdate = true;
		}
	}

	private void IdleBehavior()
	{
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f && targetIndex < 0)
		{
			if (sparkCounter > 0)
			{
				sparkCounter--;
			}
			if (sparkCounter < 0)
			{
				sparkCounter = 0;
			}
			float sepAnxietyDist = 1000f;
			Vector2 playerDist = player.Center - base.Projectile.Center;
			if (((Vector2)(ref playerDist)).Length() > 2000f)
			{
				base.Projectile.position = player.Center - base.Projectile.Size / 2f;
			}
			else if (((Vector2)(ref playerDist)).Length() > sepAnxietyDist || Math.Abs(playerDist.Y) > 300f)
			{
				base.Projectile.ai[0] = 1f;
				base.Projectile.netUpdate = true;
				if (base.Projectile.velocity.Y > 0f && playerDist.Y < 0f)
				{
					base.Projectile.velocity.Y = 0f;
				}
				if (base.Projectile.velocity.Y < 0f && playerDist.Y > 0f)
				{
					base.Projectile.velocity.Y = 0f;
				}
			}
		}
		if (base.Projectile.ai[0] != 0f)
		{
			return;
		}
		base.Projectile.tileCollide = true;
		float accelFast = 1f;
		float maxSpeed = 8f;
		float xVel = 8f;
		float accelSlow = 0.2f;
		if (xVel < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
		{
			xVel = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
			accelFast = 1.4f;
		}
		int direction = 0;
		bool flag3 = false;
		float idleDist = idlePos.X - base.Projectile.Center.X;
		if (Math.Abs(idleDist) > 5f)
		{
			if (idleDist < 0f)
			{
				direction = -1;
				if (base.Projectile.velocity.X > 0f - maxSpeed)
				{
					base.Projectile.velocity.X -= accelFast;
				}
				else
				{
					base.Projectile.velocity.X -= accelSlow;
				}
			}
			else
			{
				direction = 1;
				if (base.Projectile.velocity.X < maxSpeed)
				{
					base.Projectile.velocity.X += accelFast;
				}
				else
				{
					base.Projectile.velocity.X += accelSlow;
				}
			}
		}
		else
		{
			base.Projectile.velocity.X *= 0.9f;
			if (Math.Abs(base.Projectile.velocity.X) < accelFast * 2f)
			{
				base.Projectile.velocity.X = 0f;
			}
		}
		if (direction != 0)
		{
			int num = (int)base.Projectile.Center.X / 16;
			int yPos = (int)base.Projectile.position.Y / 16;
			int x = num + direction + (int)base.Projectile.velocity.X;
			for (int y = yPos; y < yPos + base.Projectile.height / 16 + 1; y++)
			{
				if (WorldGen.InWorld(x, y) && WorldGen.SolidTile(x, y))
				{
					flag3 = true;
				}
			}
		}
		Collision.StepUp(ref base.Projectile.position, ref base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ref base.Projectile.stepSpeed, ref base.Projectile.gfxOffY);
		if ((base.Projectile.velocity.Y == 0f) & flag3)
		{
			for (int i = 0; i < 3; i++)
			{
				int x2 = (int)base.Projectile.Center.X / 16;
				if (i == 0)
				{
					x2 = (int)base.Projectile.Left.X / 16;
				}
				if (i == 2)
				{
					x2 = (int)base.Projectile.Right.X / 16;
				}
				int y2 = (int)base.Projectile.Bottom.Y / 16;
				Tile tile = Main.tile[x2, y2];
				if ((!WorldGen.InWorld(x2, y2) || !WorldGen.SolidTile(x2, y2)) && !tile.IsHalfBlock && tile.Slope <= SlopeType.Solid && (!TileID.Sets.Platforms[tile.TileType] || !tile.HasTile || tile.HasActuator))
				{
					continue;
				}
				try
				{
					int num2 = (int)base.Projectile.Center.X / 16;
					int yPos2 = (int)base.Projectile.Center.Y / 16;
					int i2 = num2 + direction + (int)base.Projectile.velocity.X;
					if (!WorldGen.SolidTile(i2, yPos2 - 1) && !WorldGen.SolidTile(i2, yPos2 - 2))
					{
						base.Projectile.velocity.Y = -5.1f;
					}
					else if (!WorldGen.SolidTile(i2, yPos2 - 2))
					{
						base.Projectile.velocity.Y = -7.1f;
					}
					else if (WorldGen.SolidTile(i2, yPos2 - 5))
					{
						base.Projectile.velocity.Y = -11.1f;
					}
					else if (WorldGen.SolidTile(i2, yPos2 - 4))
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
		if (base.Projectile.velocity.X > xVel)
		{
			base.Projectile.velocity.X = xVel;
		}
		if (base.Projectile.velocity.X < 0f - xVel)
		{
			base.Projectile.velocity.X = 0f - xVel;
		}
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.direction = -1;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		if (base.Projectile.velocity.X > accelFast && direction == 1)
		{
			base.Projectile.direction = 1;
		}
		if (base.Projectile.velocity.X < 0f - accelFast && direction == -1)
		{
			base.Projectile.direction = -1;
		}
		base.Projectile.spriteDirection = -base.Projectile.direction;
		base.Projectile.rotation = 0f;
		if (base.Projectile.velocity.Y == 0f)
		{
			if (base.Projectile.velocity.X == 0f)
			{
				base.Projectile.frame = 0;
				base.Projectile.frameCounter = 0;
				if (player.Center.X - base.Projectile.Center.X - 40f * (float)player.direction - 40f * (float)base.Projectile.minionPos * (float)player.direction > 0f)
				{
					base.Projectile.spriteDirection = base.Projectile.direction;
				}
			}
			else if (Math.Abs(base.Projectile.velocity.X) >= 0.5f)
			{
				base.Projectile.frameCounter += (int)Math.Abs(base.Projectile.velocity.X);
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 10)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame >= 6)
				{
					base.Projectile.frame = 0;
				}
			}
			else
			{
				base.Projectile.frame = 0;
				base.Projectile.frameCounter = 0;
			}
		}
		else if (base.Projectile.velocity.Y != 0f)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = 0;
		}
		base.Projectile.velocity.Y += 0.4f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (targetIndex < 0)
		{
			fallThrough = base.Projectile.Bottom.Y < player.Top.Y;
		}
		else
		{
			fallThrough = base.Projectile.Bottom.Y < Main.npc[targetIndex].Top.Y;
		}
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			int index = Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, new Vector2(0f, 0f), Main.rand.Next(61, 64), base.Projectile.scale);
			Gore obj = Main.gore[index];
			obj.velocity *= 0.1f;
		}
	}

	public StormjawBaby()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		targetIndex = -1;
		idlePos = Vector2.Zero;
		base._002Ector();
	}
}
