using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class VileFeederProjectile : ModProjectile, ILocalizedModType, IModType
{
	private int bounce = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 3;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 50;
		}
		else
		{
			base.Projectile.extraUpdates = 0;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		for (int i = 0; i < 1; i++)
		{
			int dustType = 7;
			float smallXVel = base.Projectile.velocity.X / 3f * (float)i;
			float smallYVel = base.Projectile.velocity.Y / 3f * (float)i;
			int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
			Dust obj = Main.dust[dusty];
			obj.position.X = base.Projectile.Center.X - smallXVel;
			obj.position.Y = base.Projectile.Center.Y - smallYVel;
			obj.velocity *= 0f;
			obj.scale = 0.5f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		float projX = base.Projectile.position.X;
		float projY = base.Projectile.position.Y;
		float attackDistance = 100000f;
		bool canAttack = false;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 30f)
		{
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(base.Projectile))
				{
					float npcX = npc.position.X + (float)(npc.width / 2);
					float npcY = npc.position.Y + (float)(npc.height / 2);
					float npcDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY);
					if (npcDist < 640f && npcDist < attackDistance && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
					{
						attackDistance = npcDist;
						projX = npcX;
						projY = npcY;
						canAttack = true;
					}
				}
			}
			if (!canAttack)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc2 = enumerator.Current;
					if (npc2.CanBeChasedBy(base.Projectile))
					{
						float npcX2 = npc2.position.X + (float)(npc2.width / 2);
						float npcY2 = npc2.position.Y + (float)(npc2.height / 2);
						float npcDist2 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX2) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY2);
						if (npcDist2 < 640f && npcDist2 < attackDistance && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc2.position, npc2.width, npc2.height))
						{
							attackDistance = npcDist2;
							projX = npcX2;
							projY = npcY2;
							canAttack = true;
						}
					}
				}
			}
		}
		if (!canAttack)
		{
			projX = base.Projectile.position.X + (float)(base.Projectile.width / 2) + base.Projectile.velocity.X * 100f;
			projY = base.Projectile.position.Y + (float)(base.Projectile.height / 2) + base.Projectile.velocity.Y * 100f;
		}
		float XSpeedMod = 0.16f;
		Vector2 fireDirection = base.Projectile.Center;
		float fireXVel = projX - fireDirection.X;
		float fireYVel = projY - fireDirection.Y;
		float fireVelocity = (float)Math.Sqrt(fireXVel * fireXVel + fireYVel * fireYVel);
		fireVelocity = 10f / fireVelocity;
		fireXVel *= fireVelocity;
		fireYVel *= fireVelocity;
		if (base.Projectile.velocity.X < fireXVel)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X + XSpeedMod;
			if (base.Projectile.velocity.X < 0f && fireXVel > 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + XSpeedMod * 2f;
			}
		}
		else if (base.Projectile.velocity.X > fireXVel)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X - XSpeedMod;
			if (base.Projectile.velocity.X > 0f && fireXVel < 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - XSpeedMod * 2f;
			}
		}
		if (base.Projectile.velocity.Y < fireYVel)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + XSpeedMod;
			if (base.Projectile.velocity.Y < 0f && fireYVel > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + XSpeedMod * 2f;
			}
		}
		else if (base.Projectile.velocity.Y > fireYVel)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - XSpeedMod;
			if (base.Projectile.velocity.Y > 0f && fireYVel < 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - XSpeedMod * 2f;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		bounce--;
		if (bounce <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			base.Projectile.ai[0] += 15f;
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.ai[0] >= 30f;
	}
}
