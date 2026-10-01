using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class EarthenTidesBlastSpawner : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 50;
	}

	public override void AI()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.timeLeft = (int)base.Projectile.ai[0];
			base.Projectile.ai[1]++;
		}
		base.Projectile.Center = Main.player[base.Projectile.owner].Center;
		if (base.Projectile.timeLeft % 6 == 3)
		{
			for (int i = 0; i < 2; i++)
			{
				float randomX = Main.rand.NextFloat(-200f, 200f);
				float randomY = Main.rand.NextFloat(-200f, 200f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), new Vector2(base.Projectile.Center.X + randomX, base.Projectile.Center.Y + randomY), Vector2.Zero, ModContent.ProjectileType<EarthenTidesBlast>(), base.Projectile.damage, base.Projectile.knockBack, Main.myPlayer, Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI), Main.rand.NextFloat(0.96f, 1.04f));
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
