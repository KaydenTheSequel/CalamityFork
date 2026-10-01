using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ShadeNimbusRain : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.alpha = 50;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Center.Y > Main.player[base.Projectile.owner].Center.Y)
		{
			base.Projectile.tileCollide = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		int shadeDust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + (float)base.Projectile.height - 2f), 2, 2, 14);
		Dust obj = Main.dust[shadeDust];
		obj.position.X -= 2f;
		obj.alpha = 38;
		obj.velocity *= 0.1f;
		obj.velocity += -base.Projectile.oldVelocity * 0.25f;
		obj.scale = 0.95f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(100, 255, 100, base.Projectile.alpha);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo info, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrainRot>(), 60);
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
			if (base.Projectile.damage < 1)
			{
				base.Projectile.damage = 1;
			}
		}
	}
}
