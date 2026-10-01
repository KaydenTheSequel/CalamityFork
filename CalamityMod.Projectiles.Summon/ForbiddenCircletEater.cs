using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ForbiddenCircletEater : ModProjectile, ILocalizedModType, IModType
{
	private int bounce = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 3;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
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
		int dustType = 159;
		float slowXVel = base.Projectile.velocity.X / 3f;
		float slowYVel = base.Projectile.velocity.Y / 3f;
		int dustID = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
		Dust obj = Main.dust[dustID];
		obj.position.X = base.Projectile.Center.X - slowXVel;
		obj.position.Y = base.Projectile.Center.Y - slowYVel;
		obj.velocity *= 0f;
		obj.scale = 0.5f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		_ = base.Projectile.position;
		_ = base.Projectile.position;
		float attackDistance = 100000f;
		base.Projectile.ai[0]++;
		NPC target = null;
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
						target = npc;
					}
				}
			}
			if (target.IsNullOrInactive())
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
							target = npc2;
						}
					}
				}
			}
		}
		if (!target.IsNullOrInactive())
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.DirectionTo(target.Center) * 15f, 0.1f);
		}
		else
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 15f, 0.1f);
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
