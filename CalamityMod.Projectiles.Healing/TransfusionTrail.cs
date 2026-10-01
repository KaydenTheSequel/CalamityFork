using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class TransfusionTrail : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.HealingProjectile((int)base.Projectile.ai[1], (int)base.Projectile.ai[0], 5f, 15f);
		int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 183, 0f, 0f, 100);
		Main.dust[dust].noGravity = true;
		Dust obj = Main.dust[dust];
		obj.velocity *= 0f;
		Main.dust[dust].position.X -= base.Projectile.velocity.X * 0.2f;
		Main.dust[dust].position.Y += base.Projectile.velocity.Y * 0.2f;
	}
}
