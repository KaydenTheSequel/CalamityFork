using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class GaussEnergy : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 240;
	}

	public override void AI()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), 107);
			dust.scale = Main.rand.NextFloat(1.1f, 1.3f);
			dust.noGravity = true;
		}
	}
}
