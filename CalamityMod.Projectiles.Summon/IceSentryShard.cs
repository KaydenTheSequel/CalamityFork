using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class IceSentryShard : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/IceRain";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.aiStyle = 1;
		base.Projectile.coldDamage = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void AI()
	{
		base.Projectile.velocity.Y += 0.2f;
		if (base.Projectile.localAI[0] == 0f || base.Projectile.localAI[0] == 2f)
		{
			base.Projectile.scale += 0.01f;
			base.Projectile.alpha -= 50;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.localAI[0] = 1f;
				base.Projectile.alpha = 0;
			}
		}
		else if ((double)base.Projectile.localAI[0] == 1.0)
		{
			base.Projectile.scale -= 0.01f;
			base.Projectile.alpha += 50;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.localAI[0] = 2f;
				base.Projectile.alpha = 255;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int index1 = 0; index1 < 3; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 76);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].scale = 0.7f;
		}
	}
}
