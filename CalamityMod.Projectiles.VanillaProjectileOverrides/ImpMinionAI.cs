using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Projectiles.VanillaProjectileOverrides;

public static class ImpMinionAI
{
	private const float MinEnemyDistanceDetection = 640f;

	private const float MaxEnemyDistanceDetection = 1200f;

	private const int FireRate = 60;

	private const float ProjectileVelocity = 20f;

	private static float EnemyDistanceDetection
	{
		get
		{
			if (Target != null)
			{
				return 1200f;
			}
			return 640f;
		}
	}

	private static NPC Target { get; set; }

	public static bool DoImpMinionAI(Projectile proj)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[proj.owner];
		Target = owner.Center.MinionHoming(EnemyDistanceDetection, owner, CalamityPlayer.areThereAnyDamnBosses);
		ref float shootTimer = ref proj.ai[0];
		CheckMinionExistence(proj, owner);
		DoAnimation(proj);
		DecideDirection(proj);
		FollowPlayer(proj, owner);
		proj.MinionAntiClump();
		if (Target != null)
		{
			shootTimer += ((!Main.rand.NextBool(20)) ? 1 : 2);
			if (shootTimer >= 60f && proj.owner == Main.myPlayer)
			{
				Vector2 toTargetDirection = proj.Center.DirectionTo(Target.Center) * 20f;
				Projectile projectile = Projectile.NewProjectileDirect(proj.GetSource_FromThis(), proj.Center, toTargetDirection, 376, proj.damage, proj.knockBack, proj.owner);
				projectile.localNPCHitCooldown = 30;
				projectile.Size *= 2f;
				projectile.tileCollide = false;
				projectile.usesLocalNPCImmunity = true;
				projectile.usesIDStaticNPCImmunity = false;
				proj.velocity -= toTargetDirection.SafeNormalize(Vector2.Zero);
				if (!Main.dedServ)
				{
					for (int i = 0; i < 15; i++)
					{
						Dust dust = Dust.NewDustPerfect(proj.Center + toTargetDirection.SafeNormalize(Vector2.Zero) * proj.Size / 2f, 6, toTargetDirection.SafeNormalize(Vector2.Zero).RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(3f, 5f), 0, default(Color), 2f);
						dust.noGravity = true;
						dust.noLight = true;
						dust.noLightEmittence = true;
					}
				}
				shootTimer = 0f;
				SyncVariables(proj);
			}
		}
		else if (proj.WithinRange(owner.Center, 128f))
		{
			proj.velocity *= 0.875f;
			SyncVariables(proj);
		}
		if (!Main.dedServ)
		{
			Dust dust2 = Dust.NewDustDirect(proj.position, proj.width, proj.height, 6);
			dust2.noGravity = true;
			dust2.noLight = true;
			dust2.noLightEmittence = true;
		}
		return false;
	}

	private static void CheckMinionExistence(Projectile proj, Player owner)
	{
		owner.AddBuff(126, 2);
		if (proj.type == 375)
		{
			if (owner.dead)
			{
				owner.impMinion = false;
			}
			if (owner.impMinion)
			{
				proj.timeLeft = 2;
			}
		}
	}

	private static void DoAnimation(Projectile proj)
	{
		proj.frameCounter++;
		if (proj.frameCounter >= 6)
		{
			proj.frame = (proj.frame + 1) % Main.projFrames[proj.type];
			proj.frameCounter = 0;
		}
	}

	private static void DecideDirection(Projectile proj)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			proj.direction = (proj.spriteDirection = (Target.Center.X - proj.Center.X < 0f).ToDirectionInt());
		}
		else
		{
			proj.direction = (proj.spriteDirection = -proj.velocity.X.DirectionalSign());
		}
	}

	private static void FollowPlayer(Projectile proj, Player owner)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (!proj.WithinRange(owner.Center, 160f))
		{
			proj.velocity = (proj.velocity + proj.SafeDirectionTo(owner.Center)) * 0.9f;
			SyncVariables(proj);
		}
		if (!proj.WithinRange(owner.Center, 1200f))
		{
			proj.Center = owner.Center;
			SyncVariables(proj);
		}
	}

	private static void SyncVariables(Projectile proj)
	{
		proj.ForceNetUpdate(ignoreCurrentNetSpam: false);
	}
}
