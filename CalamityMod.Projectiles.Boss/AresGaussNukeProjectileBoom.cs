using System;
using System.Collections.Generic;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AresGaussNukeProjectileBoom : BaseMassiveExplosionProjectile, ILocalizedModType, IModType
{
	private List<int> PlayersHit = new List<int> { -1 };

	public new string LocalizationCategory => "Projectiles.Boss";

	public override int Lifetime => 60;

	public override bool UsesScreenshake => true;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override float GetScreenshakePower(float pulseCompletionRatio)
	{
		return CalamityUtils.Convert01To010(pulseCompletionRatio) * 16f;
	}

	public override Color GetCurrentExplosionColor(float pulseCompletionRatio)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Yellow * 1.6f, Color.White, MathHelper.Clamp(pulseCompletionRatio * 2.2f, 0f, 1f));
	}

	public override float Fadeout(float completion)
	{
		float opacity = ((!(completion < 0.8f)) ? ((float)Math.Cos((completion - 0.8f) / 0.2f * (float)Math.PI / 2f)) : 1f);
		return opacity * 0.85f;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime;
		base.CooldownSlot = 1;
	}

	public override void PostAI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.2f, 0.1f, 0f);
	}

	public override bool CanHitPlayer(Player target)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityUtils.CircularHitboxCollision(base.Projectile.Center, base.CurrentRadius * base.Projectile.scale * 0.4f, target.Hitbox) && base.Projectile.timeLeft > 6)
		{
			return !PlayersHit.Contains(target.whoAmI);
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		PlayersHit.Add(target.whoAmI);
	}
}
