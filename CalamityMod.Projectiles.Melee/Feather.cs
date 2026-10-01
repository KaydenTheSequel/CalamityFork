using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Feather : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeLeft = 150;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Magic/TradewindsProjectile";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 150;
		base.Projectile.aiStyle = 1;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (base.Projectile.timeLeft > 145)
		{
			return false;
		}
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 64, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.5f;
		}
	}
}
