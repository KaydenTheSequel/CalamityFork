using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class CragmawAcidDrop : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/Environment/AcidDrop";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = false;
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		float homingSpeed = (DownedBossSystem.downedPolterghast ? 16.5f : 10f);
		Player target = Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)];
		if (base.Projectile.WithinRange(target.Center, 1200f) && base.Projectile.timeLeft < 210)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 59f + base.Projectile.SafeDirectionTo(target.Center) * homingSpeed) / 60f;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 25f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 60);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		lightColor = Color.White;
		((Color)(ref lightColor)).A = 64;
		return lightColor * base.Projectile.Opacity;
	}
}
