using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class UnstableEbonianGlob : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.Projectile.Opacity = 0.8f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = (CalamityWorld.death ? 600 : (CalamityWorld.revenge ? 540 : (Main.expertMode ? 480 : 300)));
		if (Main.zenithWorld)
		{
			base.Projectile.extraUpdates = 1;
		}
	}

	public override void AI()
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 12f && (Main.expertMode || BossRushEvent.BossRushActive))
		{
			float velocityMult = (CalamityWorld.death ? 1.015f : (CalamityWorld.revenge ? 1.0125f : (Main.expertMode ? 1.01f : 1.005f)));
			Projectile projectile = base.Projectile;
			projectile.velocity *= velocityMult;
		}
		if (base.Projectile.timeLeft < 60)
		{
			base.Projectile.Opacity = MathHelper.Lerp(0f, 0.8f, (float)base.Projectile.timeLeft / 60f);
		}
		if (Main.rand.NextBool())
		{
			Color dustColor = Color.Lavender;
			((Color)(ref dustColor)).A = 150;
			int dust = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 4, 0f, 0f, base.Projectile.alpha, dustColor);
			Main.dust[dust].noGravity = true;
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.05f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 12f, targetHitbox);
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.timeLeft >= 60;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor);
		return false;
	}
}
