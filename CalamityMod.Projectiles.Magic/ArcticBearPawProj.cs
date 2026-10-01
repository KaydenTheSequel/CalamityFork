using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ArcticBearPawProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.coldDamage = true;
		base.Projectile.penetrate = 5;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.coldDamage = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 88);
		Main.dust[index2].noGravity = true;
		if (base.Projectile.velocity.X > -0.05f && ((base.Projectile.velocity.X < 0.05f) & (base.Projectile.velocity.Y > -0.05f)) && base.Projectile.velocity.Y < 0.05f)
		{
			base.Projectile.Kill();
			return;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.968f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}
}
