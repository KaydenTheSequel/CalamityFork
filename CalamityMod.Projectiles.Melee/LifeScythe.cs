using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LifeScythe : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 72;
		base.Projectile.aiStyle = 18;
		base.Projectile.alpha = 55;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = 240;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.AIType = 274;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
	}

	public override void AI()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		float scaling = (float)(int)Main.mouseTextColor / 200f - 0.35f;
		scaling *= 0.2f;
		base.Projectile.scale = scaling + 0.95f;
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0.5f, 0.15f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
