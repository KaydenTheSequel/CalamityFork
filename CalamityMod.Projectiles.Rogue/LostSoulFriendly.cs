using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class LostSoulFriendly : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 240;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 150;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return (base.Projectile.timeLeft < 210 || base.Projectile.ai[0] == 2f) && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0.25f, 0.25f);
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (base.Projectile.ai[0] != 1f && (base.Projectile.timeLeft < 210 || base.Projectile.ai[0] == 2f))
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 600f, 12f, 20f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.DamageType == DamageClass.Generic)
		{
			int cap = 10;
			float capDamageFactor = 0.05f;
			int excessCount = Main.player[base.Projectile.owner].ownedProjectileCounts[base.Type] - cap;
			modifiers.SourceDamage *= MathHelper.Clamp(1f - capDamageFactor * (float)excessCount, 0f, 1f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		for (int j = 0; j <= 10; j++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 175, 0f, 0f, 100);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(150, 150, 150, 150);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
