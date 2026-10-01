using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.VanillaProjectileOverrides;

public static class HornetMinionAI
{
	private const float MinEnemyDistanceDetection = 640f;

	private const float MaxEnemyDistanceDetection = 1200f;

	private const int FireRate = 40;

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

	public static bool DoHornetMinionAI(Projectile proj)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[proj.owner];
		Target = owner.Center.MinionHoming(EnemyDistanceDetection, owner, CalamityPlayer.areThereAnyDamnBosses);
		ref float shootTimer = ref proj.ai[0];
		CheckMinionExistence(proj, owner);
		DoAnimation(proj);
		DecideDirection(proj);
		FollowPlayer(proj, owner);
		proj.MinionAntiClump();
		proj.rotation = MathHelper.ToRadians(proj.velocity.X * 2f);
		if (Target != null)
		{
			shootTimer += ((!Main.rand.NextBool(30)) ? 1 : 2);
			if (shootTimer >= (float)(40 - (owner.strongBees ? 10 : 0)) && proj.owner == Main.myPlayer)
			{
				Vector2 toTargetDirection = CalamityUtils.CalculatePredictiveAimToTarget(proj.Center, Target, 20f);
				Projectile.NewProjectile(proj.GetSource_FromThis(), proj.Center, toTargetDirection, ModContent.ProjectileType<BetterHornetStinger>(), proj.damage, proj.knockBack, proj.owner);
				proj.velocity -= toTargetDirection.SafeNormalize(Vector2.Zero);
				if (!Main.dedServ)
				{
					for (int i = 0; i < 15; i++)
					{
						Dust dust = Dust.NewDustPerfect(proj.Center + toTargetDirection.SafeNormalize(Vector2.Zero) * proj.Size / 2f, 39, toTargetDirection.SafeNormalize(Vector2.Zero).RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(3f, 6f));
						dust.noGravity = true;
						dust.noLight = true;
						dust.noLightEmittence = true;
					}
					SoundEngine.PlaySound(in SoundID.Item17, proj.Center);
				}
				shootTimer = 0f;
				proj.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
		return false;
	}

	private static void CheckMinionExistence(Projectile proj, Player owner)
	{
		owner.AddBuff(125, 2);
		if (proj.type == 373)
		{
			if (owner.dead)
			{
				owner.hornetMinion = false;
			}
			if (owner.hornetMinion)
			{
				proj.timeLeft = 2;
			}
		}
	}

	private static void DoAnimation(Projectile proj)
	{
		proj.frameCounter++;
		if (proj.frameCounter >= 4)
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
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (!proj.WithinRange(owner.Center, 160f))
		{
			proj.velocity = (proj.velocity + proj.SafeDirectionTo(owner.Center)) * 0.9f;
			proj.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		if (!proj.WithinRange(owner.Center, 1200f))
		{
			proj.Center = owner.Center;
			proj.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
	}
}
