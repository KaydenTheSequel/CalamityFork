using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BoneMatter2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 9 == 8)
		{
			base.Projectile.frame++;
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.Kill();
			}
		}
	}
}
