using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class DemonHeal : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Healing";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 640;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.HealingProjectile(10, base.Projectile.owner, 20f, 20f, autoHomes: true, 640);
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 130);
		dust.velocity = Vector2.Zero;
		dust.scale = Main.rand.NextFloat(1f, 1.15f);
		dust.fadeIn = 0.45f;
		dust.noGravity = true;
	}
}
