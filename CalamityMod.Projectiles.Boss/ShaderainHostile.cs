using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ShaderainHostile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 40;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
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

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.timeLeft >= 85;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 85)
		{
			byte b2 = (byte)(base.Projectile.timeLeft * 3);
			byte a2 = (byte)((float)base.Projectile.alpha * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrainRot>(), 120);
		}
	}
}
