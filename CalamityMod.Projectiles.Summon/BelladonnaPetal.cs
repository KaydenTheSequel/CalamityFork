using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BelladonnaPetal : ModProjectile, ILocalizedModType, IModType
{
	public NPC targetFound;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "Terraria/Images/Projectile_276";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float AITimer => ref base.Projectile.ai[0];

	public ref float CheckForFiring => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = 130;
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1200f, Owner);
		Behaviour(potentialTarget);
		if (AITimer < 60f)
		{
			AITimer++;
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f, 1f, 0.3f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 8)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
		base.Projectile.netUpdate = true;
	}

	public void Behaviour(NPC target)
	{
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		if (target != null && AITimer >= 60f)
		{
			for (int dustIndex = 0; dustIndex < 5; dustIndex++)
			{
				float velModifier = 0.25f;
				Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 2, (0f - base.Projectile.velocity.X) * velModifier, (0f - base.Projectile.velocity.Y) * velModifier, 0, default(Color), 0.5f);
			}
			if (CheckForFiring == 0f)
			{
				base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, targetFound, 20f);
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
				SoundEngine.PlaySound(in SoundID.Grass, base.Projectile.Center);
				CheckForFiring = 1f;
				base.Projectile.netUpdate = true;
			}
			base.Projectile.alpha = 0;
		}
		else if (target != null && AITimer < 60f)
		{
			base.Projectile.rotation = MathHelper.Lerp(base.Projectile.rotation, (target.Center - base.Projectile.Center).ToRotation() + (float)Math.PI / 2f, AITimer / 60f);
			base.Projectile.velocity.Y += 0.2f;
			targetFound = target;
			base.Projectile.netUpdate = true;
		}
		else
		{
			AITimer = 0f;
			base.Projectile.alpha += 2;
			base.Projectile.velocity.Y += 0.2f;
			base.Projectile.rotation += 0.05f;
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		for (int dustIndex = 0; dustIndex < 5; dustIndex++)
		{
			Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 2);
		}
	}

	public override bool? CanDamage()
	{
		if (targetFound != null && AITimer >= 60f)
		{
			return null;
		}
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (CheckForFiring == 1f)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(20, 240);
	}
}
