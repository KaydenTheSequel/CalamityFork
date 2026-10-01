using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace CalamityMod.Projectiles.VanillaProjectileOverrides;

public static class RavenMinionAI
{
	private const float MinEnemyDistanceDetection = 960f;

	private const float MaxEnemyDistanceDetection = 2400f;

	private const float DashSpeed = 35f;

	private static SoundStyle CrowNoises = new SoundStyle("CalamityMod/Sounds/Custom/Crow", 3);

	private static float EnemyDistanceDetection
	{
		get
		{
			if (Target != null)
			{
				return 2400f;
			}
			return 960f;
		}
	}

	private static NPC Target { get; set; }

	public static bool DoRavenMinionAI(Projectile proj)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[proj.owner];
		Target = owner.Center.MinionHoming(EnemyDistanceDetection, owner);
		bool hasTarget = Target != null;
		CheckMinionExistence(proj, owner);
		DoAnimation(proj, hasTarget);
		proj.localNPCHitCooldown = 10;
		proj.friendly = true;
		proj.tileCollide = false;
		proj.ignoreWater = true;
		proj.usesLocalNPCImmunity = true;
		proj.usesIDStaticNPCImmunity = false;
		proj.rotation = MathHelper.ToRadians(proj.velocity.X);
		proj.MinionAntiClump(0.5f);
		if (hasTarget)
		{
			Vector2 dashDirection = proj.SafeDirectionTo(Target.Center);
			if (!proj.WithinRange(Target.Center, 240f))
			{
				float inertia = 5f;
				proj.velocity = (proj.velocity * inertia + dashDirection * 35f) / (inertia + 1f);
				SyncVariables(proj);
			}
			else if (((Vector2)(ref proj.velocity)).Length() < 20f)
			{
				proj.velocity = dashDirection * 25f;
				SyncVariables(proj);
			}
		}
		else
		{
			if (!proj.WithinRange(owner.Center, 320f))
			{
				proj.velocity = (proj.velocity + proj.SafeDirectionTo(owner.Center)) * 0.9f;
				SyncVariables(proj);
			}
			if (!proj.WithinRange(owner.Center, 2400f))
			{
				proj.Center = owner.Center;
				SyncVariables(proj);
			}
			if (proj.velocity == Vector2.Zero)
			{
				proj.velocity = Main.rand.NextVector2Circular(5f, 5f);
			}
		}
		if (!Main.dedServ && Main.rand.NextBool(6000))
		{
			SoundEngine.PlaySound(in CrowNoises, proj.Center);
		}
		return false;
	}

	public static void DoRavenMinionDrawing(Projectile proj, ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[proj.type].Value;
		Vector2 drawPosition = proj.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[proj.type], 0, proj.frame);
		Vector2 origin = frame.Size() * 0.5f;
		SpriteEffects effects = (SpriteEffects)((float)MathF.Sign(0f - proj.velocity.X) != 1f);
		ProjectileID.Sets.TrailingMode[proj.type] = 2;
		ProjectileID.Sets.TrailCacheLength[proj.type] = 5;
		if (Target != null)
		{
			Color mediumPurple = Color.MediumPurple;
			((Color)(ref mediumPurple)).A = 50;
			CalamityUtils.DrawAfterimagesCentered(proj, 0, mediumPurple);
		}
		Main.EntitySpriteDraw(value, drawPosition, frame, proj.GetAlpha(lightColor), proj.rotation, origin, proj.scale, effects);
	}

	private static void CheckMinionExistence(Projectile proj, Player owner)
	{
		owner.AddBuff(83, 2);
		if (proj.type == 317)
		{
			if (owner.dead)
			{
				owner.raven = false;
			}
			if (owner.raven)
			{
				proj.timeLeft = 2;
			}
		}
	}

	private static void DoAnimation(Projectile proj, bool charging)
	{
		proj.frameCounter++;
		if (proj.frameCounter >= 5)
		{
			int maxFrames = Main.projFrames[proj.type] / 2;
			proj.frame = (proj.frame + 1) % maxFrames;
			if (charging)
			{
				proj.frame += maxFrames;
			}
			proj.frameCounter = 0;
		}
	}

	private static void SyncVariables(Projectile proj)
	{
		proj.ForceNetUpdate(ignoreCurrentNetSpam: false);
	}
}
