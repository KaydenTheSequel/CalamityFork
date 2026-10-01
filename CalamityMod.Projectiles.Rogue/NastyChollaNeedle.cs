using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class NastyChollaNeedle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 180;
		base.Projectile.aiStyle = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -2;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (base.Projectile.spriteDirection == 1)
		{
			base.Projectile.rotation += MathHelper.ToRadians(45f);
		}
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation -= MathHelper.ToRadians(45f);
		}
		base.Projectile.velocity.X *= 0.9995f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.01f;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return target.type != 548 && !target.immortal && !target.dontTakeDamage;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetMaxDamage(1);
	}
}
