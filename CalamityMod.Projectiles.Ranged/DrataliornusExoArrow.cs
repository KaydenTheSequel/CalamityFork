using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DrataliornusExoArrow : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 4;
		base.Projectile.timeLeft = 60;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 0;
	}

	public override void AI()
	{
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		float inc = 3f;
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

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return new Color(250, 25, 0, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.DrawBeam(100f, 3f, lightColor);
	}
}
