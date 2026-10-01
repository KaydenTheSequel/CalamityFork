using CalamityMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(true)]
public class KeelhaulGeyserTop : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 158;
		base.Projectile.height = 188;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 900;
		base.Projectile.alpha = 255;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		if (base.Projectile.alpha > 90)
		{
			base.Projectile.alpha -= 10;
			if (base.Projectile.alpha < 90)
			{
				base.Projectile.alpha = 90;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
	}
}
