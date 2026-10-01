using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BrimstoneElementalMinion : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 78;
		base.Projectile.height = 126;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_078e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		bool isActive = base.Projectile.type == ModContent.ProjectileType<BrimstoneElementalMinion>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.brimElemental && !modPlayer.allElementals && !modPlayer.brimElementalVanity && !modPlayer.allElementalsVanity)
		{
			base.Projectile.active = false;
			return;
		}
		if (isActive)
		{
			if (player.dead)
			{
				modPlayer.brimEleBuff = false;
			}
			if (modPlayer.brimEleBuff)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		dust--;
		if (dust >= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				int brimDust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 235);
				Dust obj = Main.dust[brimDust];
				obj.velocity *= 2f;
				Main.dust[brimDust].scale *= 1.15f;
			}
		}
		bool passive = modPlayer.brimElementalVanity || modPlayer.allElementalsVanity;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 9)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (!passive)
		{
			float lights = (float)Main.rand.Next(90, 111) * 0.01f;
			lights *= Main.essScale;
			Lighting.AddLight(base.Projectile.Center, 1.25f * lights, 0f * lights, 0.5f * lights);
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		float maxTargetDist = 700f;
		base.Projectile.MinionAntiClump();
		Vector2 targetCenter = base.Projectile.position;
		bool canAttack = false;
		if (base.Projectile.ai[0] != 1f)
		{
			base.Projectile.tileCollide = false;
		}
		if (base.Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16)))
		{
			base.Projectile.tileCollide = false;
		}
		if (player.HasMinionAttackTargetNPC && !passive)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!canAttack && targetDist < maxTargetDist && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
				{
					targetCenter = npc.Center;
					canAttack = true;
				}
			}
		}
		if (!canAttack && !passive)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC nPC2 = enumerator.Current;
				if (nPC2.CanBeChasedBy(base.Projectile))
				{
					float targetDist2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!canAttack && targetDist2 < maxTargetDist && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						maxTargetDist = targetDist2;
						targetCenter = nPC2.Center;
						canAttack = true;
					}
				}
			}
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > 1200f)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.tileCollide = false;
			base.Projectile.netUpdate = true;
		}
		bool isReturning = false;
		if (!isReturning)
		{
			isReturning = base.Projectile.ai[0] == 1f;
		}
		float returnSpeed = 5f;
		if (isReturning)
		{
			returnSpeed = 12f;
		}
		Vector2 center2 = base.Projectile.Center;
		Vector2 playerDirection = player.Center - center2 + new Vector2(-500f, -60f);
		float num = ((Vector2)(ref playerDirection)).Length();
		if (num > 200f && returnSpeed < 6.5f)
		{
			returnSpeed = 6.5f;
		}
		if (((num < 400f) & isReturning) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (num > 2000f)
		{
			base.Projectile.position.X = Main.player[base.Projectile.owner].Center.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = Main.player[base.Projectile.owner].Center.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		if (num > 70f)
		{
			((Vector2)(ref playerDirection)).Normalize();
			playerDirection *= returnSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 40f + playerDirection) / 41f;
		}
		else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
		{
			base.Projectile.velocity.X = -0.18f;
			base.Projectile.velocity.Y = -0.08f;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.ai[1] > 160f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.localAI[0] < 120f)
		{
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			return;
		}
		float fireballShootSpeed = 14f;
		int projID = ModContent.ProjectileType<BrimstoneFireballMinion>();
		if (canAttack && base.Projectile.ai[1] == 0f && base.Projectile.localAI[0] >= 120f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, targetCenter, 0, 0))
			{
				Vector2 fireballshootVelocity = base.Projectile.SafeDirectionTo(targetCenter) * fireballShootSpeed;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, fireballshootVelocity, projID, base.Projectile.damage, 0f, base.Projectile.owner);
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override bool? CanDamage()
	{
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		if (modPlayer.brimElementalVanity || modPlayer.allElementalsVanity)
		{
			return false;
		}
		return null;
	}
}
