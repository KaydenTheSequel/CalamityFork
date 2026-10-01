using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GhastlySoulMedium : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeLeft = 600;

	private float HomingBuff = 1f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.alpha = 100;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 0;
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		if (HomingBuff > 0f)
		{
			HomingBuff -= 0.01f;
		}
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.2f, 0.9f);
		if (base.Projectile.timeLeft % 2 == 0 && base.Projectile.timeLeft < 590)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f) - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, -base.Projectile.velocity * Main.rand.NextFloat(0.5f, 1.5f), affectedByGravity: false, Main.rand.Next(9, 13), 0.4f, Color.Plum));
		}
		float inertia = MathHelper.Lerp(20f, 90f, HomingBuff) * base.Projectile.ai[1];
		float velocity = 9f * base.Projectile.ai[1];
		if (Main.player[base.Projectile.owner].active && !Main.player[base.Projectile.owner].dead)
		{
			float homingDistance = 750f;
			NPC target = base.Projectile.FindTargetWithinRange(homingDistance);
			if (base.Projectile.timeLeft < 580 && target != null)
			{
				CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, homingDistance, velocity, inertia);
			}
			else if (base.Projectile.Distance(Main.player[base.Projectile.owner].Center) > homingDistance)
			{
				Vector2 moveDirection = base.Projectile.SafeDirectionTo(Main.player[base.Projectile.owner].Center, Vector2.UnitY);
				base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + moveDirection * velocity) / inertia;
			}
		}
		else if (base.Projectile.timeLeft > 30)
		{
			base.Projectile.timeLeft = 30;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 85)
		{
			byte b2 = (byte)(base.Projectile.timeLeft * 3);
			byte a2 = (byte)(100f * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return new Color(255, 255, 255, 100);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.88f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = base.Projectile.damage / 3;
		base.Projectile.penetrate = -1;
		base.Projectile.ExpandHitboxBy(140);
		base.Projectile.Damage();
		SoundEngine.PlaySound(in VoidEdge.ProjectileDeathSound, base.Projectile.Center);
		int points = 20;
		float radians = (float)Math.PI * 2f / (float)points;
		Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
		float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
		for (int k = 0; k < points; k++)
		{
			Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.45f * rotRando);
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center + velocity * 15.5f, velocity * 10f, affectedByGravity: false, 25, 0.65f, Color.Plum));
		}
		for (int i = 0; i < 12; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 66, Utils.RotatedByRandom(new Vector2(9f, 9f), 100.0) * Main.rand.NextFloat(0.3f, 1.8f));
			dust.scale = Main.rand.NextFloat(0.85f, 1.25f);
			dust.noGravity = true;
			dust.color = Color.Plum;
		}
	}
}
