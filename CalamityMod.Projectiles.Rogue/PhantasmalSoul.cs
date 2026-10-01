using System;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PhantasmalSoul : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 300;

	private const int NoHomingFrames = 35;

	private const int NoHitFrames = 10;

	private const int NoDrawFrames = 5;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.alpha = 100;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 265 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft < 265)
		{
			base.Projectile.ai[0] = 1f;
		}
		if (Main.rand.NextBool(4))
		{
			int dustID = 173;
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID);
			dust.velocity *= 0.1f;
			dust.scale = 1.3f;
			dust.noGravity = true;
			dust.noLight = true;
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.2f, 0.9f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		float inertia = 40f * base.Projectile.ai[1];
		float homingSpeed = 8f * base.Projectile.ai[1];
		float playerHomingRange = 900f;
		Player owner = Main.player[base.Projectile.owner];
		if (owner.active && !owner.dead)
		{
			if (base.Projectile.Distance(Main.player[base.Projectile.owner].Center) > playerHomingRange)
			{
				Vector2 moveDirection = base.Projectile.SafeDirectionTo(Main.player[base.Projectile.owner].Center, Vector2.UnitY);
				base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + moveDirection * homingSpeed) / inertia;
			}
			else if (base.Projectile.ai[0] == 1f)
			{
				CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 10f, 20f);
			}
		}
		else if (base.Projectile.timeLeft > 30)
		{
			base.Projectile.timeLeft = 30;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 295)
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

	public override void OnKill(int timeLeft)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		int dustCount = 36;
		int dustID = 173;
		for (int i = 0; i < dustCount; i++)
		{
			Vector2 velocity = Main.rand.NextVector2Circular(1f, 1f);
			velocity = 12f * velocity + base.Projectile.velocity * 0.1f;
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, dustID);
			dust.velocity = velocity;
			dust.noGravity = true;
			dust.noLight = true;
		}
		SoundEngine.PlaySound(in VoidEdge.ProjectileDeathSound, base.Projectile.Center);
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.timeLeft < 290;
	}
}
