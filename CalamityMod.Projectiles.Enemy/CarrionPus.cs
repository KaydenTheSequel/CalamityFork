using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class CarrionPus : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 240;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.5f, 0f);
		for (int i = 0; i < 2; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Main.rand.NextVector2Circular(4f, 4f), 0, Main.rand.NextBool(3) ? Color.Red : Color.Yellow).noLightEmittence = true;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 60);
		}
	}
}
