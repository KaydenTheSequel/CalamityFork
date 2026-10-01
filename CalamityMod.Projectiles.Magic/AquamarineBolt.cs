using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AquamarineBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 2;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0.25f / 255f);
		int aquaDust = Dust.NewDust(base.Projectile.Center, base.Projectile.width - 28, base.Projectile.height - 28, 68, 0f, 0f, 100, default(Color), 1.5f);
		Main.dust[aquaDust].noGravity = true;
		Dust obj = Main.dust[aquaDust];
		obj.velocity *= 0.1f;
		Dust obj2 = Main.dust[aquaDust];
		obj2.velocity += base.Projectile.velocity * 0.5f;
		if (Main.rand.NextBool(16))
		{
			int aquaticDust = Dust.NewDust(base.Projectile.Center, base.Projectile.width - 32, base.Projectile.height - 32, 68, 0f, 0f, 100);
			Dust obj3 = Main.dust[aquaticDust];
			obj3.velocity *= 0.25f;
			Dust obj4 = Main.dust[aquaticDust];
			obj4.velocity += base.Projectile.velocity * 0.5f;
		}
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor);
		return false;
	}
}
