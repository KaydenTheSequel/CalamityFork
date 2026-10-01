using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SevensStrikerPlatinumCoin : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.CloneDefaults(161);
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.aiStyle = 1;
		base.AIType = 161;
		base.Projectile.width = 10;
		base.Projectile.height = 10;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(72, 25200);
	}
}
