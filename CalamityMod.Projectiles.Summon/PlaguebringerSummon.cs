using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlaguebringerSummon : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = Owner.Calamity();
		if ((float)base.Projectile.frameCounter++ >= 6f)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 3)
		{
			base.Projectile.frame = 0;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<PlaguebringerSummon>();
		Owner.AddBuff(ModContent.BuffType<LilPlaguebringerBuff>(), 3600);
		if (num)
		{
			if (Owner.dead)
			{
				modPlayer.plaguebringerPatronSummon = false;
			}
			if (modPlayer.plaguebringerPatronSummon)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (!modPlayer.plaguebringerPatronSet)
		{
			base.Projectile.Kill();
		}
		base.Projectile.MinionAntiClump();
		base.Projectile.ai[0]++;
		NPC Target = base.Projectile.Center.MinionHoming(800f, Owner, ignoreTiles: false);
		if (base.Projectile.owner == Main.myPlayer && Target != null && base.Projectile.ai[0] % 12f == 11f)
		{
			int beeCount = Main.rand.Next(1, 3);
			if (Owner.strongBees && Main.rand.NextBool(3))
			{
				beeCount++;
			}
			for (int i = 0; i < beeCount; i++)
			{
				int beeType = (Main.rand.NextBool(4) ? ModContent.ProjectileType<PlagueBeeSmall>() : Owner.beeType());
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(0.25f, 0.25f), beeType, Owner.beeDamage(base.Projectile.damage), Owner.beeKB(0f), base.Projectile.owner);
				projectile.usesLocalNPCImmunity = true;
				projectile.localNPCHitCooldown = 10;
				projectile.penetrate = 2;
				projectile.DamageType = DamageClass.Generic;
			}
		}
		float passiveMvtFloat = 0.5f;
		float safeDist = 100f;
		float xDist = Owner.Center.X - base.Projectile.Center.X;
		float yDist = Owner.Center.Y - base.Projectile.Center.Y;
		yDist += Main.rand.NextFloat(-10f, 20f);
		xDist += Main.rand.NextFloat(-10f, 20f);
		yDist -= 70f;
		Vector2 playerVector = default(Vector2);
		((Vector2)(ref playerVector))._002Ector(xDist, yDist);
		float playerDist = ((Vector2)(ref playerVector)).Length();
		float returnSpeed = 18f;
		if (playerDist < safeDist && Owner.velocity.Y == 0f && base.Projectile.position.Y + (float)base.Projectile.height <= Owner.position.Y + (float)Owner.height && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height) && base.Projectile.velocity.Y < -6f)
		{
			base.Projectile.velocity.Y = -6f;
		}
		if (playerDist > 2000f)
		{
			base.Projectile.position.X = Owner.Center.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = Owner.Center.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		if (playerDist < 50f)
		{
			if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.99f;
			}
			passiveMvtFloat = 0.01f;
		}
		else
		{
			if (playerDist < 100f)
			{
				passiveMvtFloat = 0.1f;
			}
			if (playerDist > 300f)
			{
				passiveMvtFloat = 1f;
			}
			playerDist = returnSpeed / playerDist;
			playerVector.X *= playerDist;
			playerVector.Y *= playerDist;
		}
		if (base.Projectile.velocity.X < playerVector.X)
		{
			base.Projectile.velocity.X += passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X += passiveMvtFloat;
			}
		}
		if (base.Projectile.velocity.X > playerVector.X)
		{
			base.Projectile.velocity.X -= passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X -= passiveMvtFloat;
			}
		}
		if (base.Projectile.velocity.Y < playerVector.Y)
		{
			base.Projectile.velocity.Y += passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y += passiveMvtFloat * 2f;
			}
		}
		if (base.Projectile.velocity.Y > playerVector.Y)
		{
			base.Projectile.velocity.Y -= passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= passiveMvtFloat * 2f;
			}
		}
		if (base.Projectile.velocity.X >= 0.25f)
		{
			base.Projectile.direction = -1;
		}
		else if (base.Projectile.velocity.X < -0.25f)
		{
			base.Projectile.direction = 1;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.01f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
