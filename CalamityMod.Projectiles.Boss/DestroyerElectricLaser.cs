using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DestroyerElectricLaser : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.ignoreWater = true;
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.Projectile.alpha = 255;
		base.Projectile.scale = 1.1f;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, 0f, (float)(255 - base.Projectile.alpha) * 0.35f / 255f, (float)(255 - base.Projectile.alpha) * 0.35f / 255f);
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 125;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.localAI[1] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item33, base.Projectile.Center);
			base.Projectile.localAI[1] = 1f;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 8f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.0025f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha < 200)
		{
			return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 0);
		}
		return Color.Transparent;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<StaticDischarge>(), 120);
		}
	}
}
