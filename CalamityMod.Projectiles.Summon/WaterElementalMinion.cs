using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class WaterElementalMinion : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 190;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		bool isMinion = base.Projectile.type == ModContent.ProjectileType<WaterElementalMinion>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.waterElemental && !modPlayer.allElementals && !modPlayer.waterElementalVanity && !modPlayer.allElementalsVanity)
		{
			base.Projectile.active = false;
			return;
		}
		if (isMinion)
		{
			if (player.dead)
			{
				modPlayer.waterEleBuff = false;
			}
			if (modPlayer.waterEleBuff)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (dust > 0)
		{
			for (int i = 0; i < 50; i++)
			{
				int spawnDust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 33);
				Dust obj = Main.dust[spawnDust];
				obj.velocity *= 2f;
				Main.dust[spawnDust].scale *= 1.15f;
			}
			dust--;
		}
		bool passive = modPlayer.waterElementalVanity || modPlayer.allElementalsVanity;
		if (!passive)
		{
			Lighting.AddLight(base.Projectile.Center, 0f, 0.25f, 1.5f);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 6)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY - 180f);
		if (player.gravDir == -1f)
		{
			base.Projectile.position.Y += 360f;
			base.Projectile.rotation = (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
		base.Projectile.position.X = (int)base.Projectile.position.X;
		base.Projectile.position.Y = (int)base.Projectile.position.Y;
		if (base.Projectile.owner != Main.myPlayer || passive)
		{
			return;
		}
		if (base.Projectile.localAI[0] < 120f)
		{
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			base.Projectile.ai[0]--;
			return;
		}
		bool canAttack = false;
		float projX = base.Projectile.Center.X;
		float projY = base.Projectile.Center.Y;
		float attackRange = 1000f;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float npcX = npc.position.X + (float)(npc.width / 2);
				float npcY = npc.position.Y + (float)(npc.height / 2);
				if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY) < attackRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
				{
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
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					float targetX = n.position.X + (float)(n.width / 2);
					float targetY = n.position.Y + (float)(n.height / 2);
					float targetDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - targetX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - targetY);
					if (targetDist < attackRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, n.position, n.width, n.height))
					{
						attackRange = targetDist;
						projX = targetX;
						projY = targetY;
						canAttack = true;
					}
				}
			}
		}
		if (canAttack && base.Projectile.localAI[0] >= 120f)
		{
			float projXStore = projX;
			float projYStore = projY;
			projX -= base.Projectile.Center.X;
			projY -= base.Projectile.Center.Y;
			if (projX < 0f)
			{
				base.Projectile.spriteDirection = 1;
			}
			else
			{
				base.Projectile.spriteDirection = -1;
			}
			int projectileType = ModContent.ProjectileType<WaterSpearFriendly>();
			if (Main.rand.NextBool(9))
			{
				projectileType = ModContent.ProjectileType<FrostMistFriendly>();
			}
			else if (Main.rand.NextBool(9))
			{
				projectileType = ModContent.ProjectileType<WaterElementalSong>();
			}
			float num = Main.rand.Next(12, 20);
			Vector2 fireDirection = base.Projectile.Center;
			float fireXVel = projXStore - fireDirection.X;
			float fireYVel = projYStore - fireDirection.Y;
			float fireVelocity = (float)Math.Sqrt(fireXVel * fireXVel + fireYVel * fireYVel);
			fireVelocity = num / fireVelocity;
			fireXVel *= fireVelocity;
			fireYVel *= fireVelocity;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X - 4f, base.Projectile.Center.Y, fireXVel, fireYVel, projectileType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			base.Projectile.ai[0] = 12f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
