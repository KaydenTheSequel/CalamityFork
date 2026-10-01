using CalamityMod.Graphics.Metaballs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RancorLargeCinder : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float Lifetime => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		if (Lifetime == 0f)
		{
			Lifetime = Main.rand.Next(45, 150);
			base.Projectile.netUpdate = true;
		}
		else
		{
			base.Projectile.scale = Utils.GetLerpValue(0f, 20f, Time, clamped: true) * Utils.GetLerpValue(Lifetime, Lifetime - 20f, Time, clamped: true);
			base.Projectile.scale *= MathHelper.Lerp(0.5f, 1f, (float)base.Projectile.identity % 6f / 6f);
		}
		base.Projectile.velocity.Y += 0.04f;
		RancorLavaMetaball.SpawnParticle(base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f), base.Projectile.scale * 36f);
		Time++;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * base.Projectile.Opacity;
	}
}
