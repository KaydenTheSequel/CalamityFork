using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class Stormfrontspark : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.timeLeft = 5;
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 226, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
		Dust obj = Main.dust[dusty];
		obj.position.X--;
		obj.position.Y--;
		obj.scale += (float)Main.rand.Next(50) * 0.01f;
		obj.noGravity = true;
		obj.velocity.Y++;
		if (Main.rand.NextBool())
		{
			int dusty2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 226, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
			Dust obj2 = Main.dust[dusty2];
			obj2.position.X++;
			obj2.position.Y--;
			obj2.scale += 0.2f + (float)Main.rand.Next(50) * 0.01f;
			obj2.noGravity = true;
			obj2.velocity *= 0.1f;
		}
		if ((double)base.Projectile.velocity.Y < 0.25 && (double)base.Projectile.velocity.Y > 0.15)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.8f;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		base.Projectile.velocity.Y += 0.15f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}
}
