using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class WaterElementalSong : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 26);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.X *= 0.985f;
		base.Projectile.velocity.Y *= 0.985f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale += 0.02f;
			if (base.Projectile.scale >= 1.25f)
			{
				base.Projectile.localAI[0] = 1f;
			}
		}
		else if (base.Projectile.localAI[0] == 1f)
		{
			base.Projectile.scale -= 0.02f;
			if (base.Projectile.scale <= 0.75f)
			{
				base.Projectile.localAI[0] = 0f;
			}
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			Main.musicPitch = (Main.rand.NextFloat() - 0.5f) * 0.5f;
			SoundEngine.PlaySound(in SoundID.Item26, base.Projectile.position);
		}
		Lighting.AddLight(base.Projectile.Center, 0f, 0f, 1.2f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, 0);
	}
}
