using System;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class TerraSigilSmallRock : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 32;
		base.Projectile.friendly = false;
		base.Projectile.damage = 0;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 100;
		base.Projectile.scale *= 0.6f;
	}

	public override void AI()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
			base.Projectile.ai[0] = 1f;
		}
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.45f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}
}
