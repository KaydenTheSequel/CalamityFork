using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RancorSmallCinder : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float Lifetime => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 300;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
			base.Projectile.localAI[0] = 1f;
		}
		if (Lifetime == 0f)
		{
			Lifetime = Main.rand.Next(60, 210);
			base.Projectile.netUpdate = true;
		}
		else
		{
			base.Projectile.scale = Utils.GetLerpValue(0f, 20f, Time, clamped: true) * Utils.GetLerpValue(Lifetime, Lifetime - 20f, Time, clamped: true);
			base.Projectile.scale *= MathHelper.Lerp(0.5f, 1f, (float)base.Projectile.identity % 6f / 6f);
		}
		if (Time >= Lifetime)
		{
			base.Projectile.Kill();
		}
		Time++;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * base.Projectile.Opacity;
	}
}
