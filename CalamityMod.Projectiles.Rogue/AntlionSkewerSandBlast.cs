using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AntlionSkewerSandBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "Terraria/Images/Projectile_" + (short)42;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 60 * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.1f;
		if (Main.rand.NextBool())
		{
			Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f), 32, Main.rand.NextVector2Circular(2f, 2f)).noGravity = true;
		}
	}
}
