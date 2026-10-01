using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GleamingBolt2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 120;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 90 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft < 90)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 8f, 20f);
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.985f;
		}
		int randomDust = Utils.SelectRandom<int>(Main.rand, 64, 204);
		Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, randomDust, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(Type: (Main.rand.Next(2) != 0) ? 204 : 64, Position: base.Projectile.position + base.Projectile.velocity, Width: base.Projectile.width, Height: base.Projectile.height, SpeedX: base.Projectile.oldVelocity.X * 0.5f, SpeedY: base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
