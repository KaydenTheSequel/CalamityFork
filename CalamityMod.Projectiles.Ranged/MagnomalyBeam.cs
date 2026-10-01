using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MagnomalyBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.ai[1] = 0f;
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Lighting.AddLight(base.Projectile.Center, (float)Main.DiscoR * 0.25f / 255f, (float)Main.DiscoG * 0.25f / 255f, (float)Main.DiscoB * 0.25f / 255f);
		float inc = 2f;
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.localAI[0] += inc;
			if (base.Projectile.localAI[0] > 100f)
			{
				base.Projectile.localAI[0] = 100f;
			}
		}
		else
		{
			base.Projectile.localAI[0] -= inc;
			if (base.Projectile.localAI[0] <= 0f)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.DrawBeam(100f, 2f, lightColor);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return new Color(0, 250, 75, base.Projectile.alpha);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}
}
