using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HermitCrabMinion : ModProjectile, ILocalizedModType, IModType
{
	private int playerStill;

	private bool fly;

	private bool spawnDust = true;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 9;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 36;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override void AI()
	{
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0887: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0932: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (spawnDust)
		{
			for (int i = 0; i < 20; i++)
			{
				int dust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 33);
				Dust obj = Main.dust[dust];
				obj.velocity *= 2f;
				Main.dust[dust].scale *= 1.15f;
			}
			spawnDust = false;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<HermitCrabMinion>();
		player.AddBuff(ModContent.BuffType<HermitCrab>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.hCrab = false;
			}
			if (modPlayer.hCrab)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		_ = base.Projectile.position;
		if (!fly)
		{
			Vector2 center2 = base.Projectile.Center;
			Vector2 destination = player.Center - center2;
			float playerDistance = ((Vector2)(ref destination)).Length();
			if (base.Projectile.velocity.Y == 0f && (HoleBelow() || (playerDistance > 205f && base.Projectile.position.X == base.Projectile.oldPosition.X)))
			{
				base.Projectile.velocity.Y = -10f;
			}
			base.Projectile.velocity.Y += 0.6f;
			if (base.Projectile.velocity.X != 0f)
			{
				base.Projectile.frameCounter++;
			}
			else
			{
				base.Projectile.frame = 0;
			}
			if (base.Projectile.frameCounter > 4)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame >= 5)
			{
				base.Projectile.frame = 1;
			}
			float attackDistance = 600f;
			bool chaseNPC = false;
			float npcPositionX = 0f;
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(base.Projectile))
				{
					float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
					if (!chaseNPC && targetDist < attackDistance)
					{
						attackDistance = targetDist;
						_ = npc.Center;
						npcPositionX = npc.position.X;
						chaseNPC = true;
					}
				}
			}
			if (!chaseNPC)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npcTarget = enumerator.Current;
					if (npcTarget.CanBeChasedBy(base.Projectile))
					{
						float targetDist2 = Vector2.Distance(npcTarget.Center, base.Projectile.Center);
						if (!chaseNPC && targetDist2 < attackDistance)
						{
							attackDistance = targetDist2;
							_ = npcTarget.Center;
							npcPositionX = npcTarget.position.X;
							chaseNPC = true;
						}
					}
				}
			}
			if (chaseNPC)
			{
				if (npcPositionX - base.Projectile.position.X > 0f)
				{
					switch (Main.rand.Next(1, 2))
					{
					case 1:
						base.Projectile.velocity.X += 0.1f;
						break;
					case 2:
						base.Projectile.velocity.X += 0.15f;
						break;
					}
					if (base.Projectile.velocity.X > 9f)
					{
						base.Projectile.velocity.X = 9f;
					}
				}
				else
				{
					switch (Main.rand.Next(1, 2))
					{
					case 1:
						base.Projectile.velocity.X -= 0.1f;
						break;
					case 2:
						base.Projectile.velocity.X -= 0.15f;
						break;
					}
					if (base.Projectile.velocity.X < -9f)
					{
						base.Projectile.velocity.X = -9f;
					}
				}
				if (playerDistance > 1000f)
				{
					fly = true;
					chaseNPC = false;
					base.Projectile.velocity.X = 0f;
					base.Projectile.velocity.Y = 0f;
					base.Projectile.tileCollide = false;
				}
			}
			else
			{
				if (playerDistance > 600f)
				{
					fly = true;
					base.Projectile.velocity.X = 0f;
					base.Projectile.velocity.Y = 0f;
					base.Projectile.tileCollide = false;
				}
				if (playerDistance > 200f)
				{
					if (player.position.X - base.Projectile.position.X > 0f)
					{
						switch (Main.rand.Next(1, 3))
						{
						case 1:
							base.Projectile.velocity.X += 0.05f;
							break;
						case 2:
							base.Projectile.velocity.X += 0.1f;
							break;
						case 3:
							base.Projectile.velocity.X += 0.15f;
							break;
						}
						if (base.Projectile.velocity.X > 9f)
						{
							base.Projectile.velocity.X = 9f;
						}
					}
					else
					{
						switch (Main.rand.Next(1, 3))
						{
						case 1:
							base.Projectile.velocity.X -= 0.05f;
							break;
						case 2:
							base.Projectile.velocity.X -= 0.1f;
							break;
						case 3:
							base.Projectile.velocity.X -= 0.15f;
							break;
						}
						if (base.Projectile.velocity.X < -9f)
						{
							base.Projectile.velocity.X = -9f;
						}
					}
				}
				if (playerDistance < 200f && base.Projectile.velocity.X != 0f)
				{
					if (base.Projectile.velocity.X > 0.5f)
					{
						switch (Main.rand.Next(1, 3))
						{
						case 1:
							base.Projectile.velocity.X -= 0.05f;
							break;
						case 2:
							base.Projectile.velocity.X -= 0.1f;
							break;
						case 3:
							base.Projectile.velocity.X -= 0.15f;
							break;
						}
					}
					else if (base.Projectile.velocity.X < -0.5f)
					{
						switch (Main.rand.Next(1, 3))
						{
						case 1:
							base.Projectile.velocity.X += 0.05f;
							break;
						case 2:
							base.Projectile.velocity.X += 0.1f;
							break;
						case 3:
							base.Projectile.velocity.X += 0.15f;
							break;
						}
					}
					else if (base.Projectile.velocity.X < 0.5f && base.Projectile.velocity.X > -0.5f)
					{
						base.Projectile.velocity.X = 0f;
					}
				}
			}
		}
		else if (fly)
		{
			Vector2 center3 = base.Projectile.Center;
			Vector2 destination2 = player.Center - center3 + new Vector2(0f, 0f);
			float num2 = ((Vector2)(ref destination2)).Length();
			((Vector2)(ref destination2)).Normalize();
			destination2 *= 8f;
			base.Projectile.velocity = (base.Projectile.velocity * 40f + destination2) / 41f;
			base.Projectile.rotation = base.Projectile.velocity.X * 0.03f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 3)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame >= 9)
			{
				base.Projectile.frame = 5;
			}
			if (num2 > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num2 < 100f)
			{
				if (player.velocity.Y == 0f)
				{
					playerStill++;
				}
				else
				{
					playerStill = 0;
				}
				if (playerStill > 30 && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
				{
					fly = false;
					base.Projectile.tileCollide = true;
					base.Projectile.rotation = 0f;
				}
			}
		}
		if (base.Projectile.velocity.X > 0.25f)
		{
			base.Projectile.spriteDirection = 1;
		}
		else if (base.Projectile.velocity.X < -0.25f)
		{
			base.Projectile.spriteDirection = -1;
		}
	}

	private bool HoleBelow()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		int tileWidth = 4;
		int tileX = (int)(base.Projectile.Center.X / 16f) - tileWidth;
		if (base.Projectile.velocity.X > 0f)
		{
			tileX += tileWidth;
		}
		int tileY = (int)((base.Projectile.position.Y + (float)base.Projectile.height) / 16f);
		for (int y = tileY; y < tileY + 2; y++)
		{
			for (int x = tileX; x < tileX + tileWidth; x++)
			{
				if (Main.tile[x, y].HasTile)
				{
					return false;
				}
			}
		}
		return true;
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
