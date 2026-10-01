using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AllianceTriangle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 154;
		base.Projectile.height = 134;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 254;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Lighting.AddLight(base.Projectile.Center, new Vector3(240f, 185f, 7f) * (1f / 85f));
		base.Projectile.Center = player.Center;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] <= 5f)
		{
			base.Projectile.alpha -= 75;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		else
		{
			base.Projectile.scale *= 1.06f;
			base.Projectile.alpha += 10;
		}
		if (base.Projectile.alpha >= 255 || player == null || player.dead)
		{
			base.Projectile.Kill();
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha <= 0)
		{
			return new Color(200, 200, 200, 200);
		}
		return null;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
