using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class NebulaDust : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.Opacity = 0f;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 3600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 3;
	}

	public override void AI()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += base.Projectile.velocity.X * 0.02f;
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.rotation -= Math.Abs(base.Projectile.velocity.Y) * 0.02f;
		}
		else
		{
			base.Projectile.rotation += Math.Abs(base.Projectile.velocity.Y) * 0.02f;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 60f)
		{
			if (base.Projectile.Opacity > 0f)
			{
				base.Projectile.Opacity -= 0.02f;
				if (base.Projectile.Opacity < 0f)
				{
					base.Projectile.Opacity = 0f;
				}
			}
			else if (base.Projectile.owner == Main.myPlayer)
			{
				base.Projectile.Kill();
			}
		}
		else if (base.Projectile.Opacity < 0.7f)
		{
			base.Projectile.Opacity += 0.1f;
			if (base.Projectile.Opacity > 0.7f)
			{
				base.Projectile.Opacity = 0.7f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 90);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
