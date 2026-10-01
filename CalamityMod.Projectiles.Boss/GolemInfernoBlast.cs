using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class GolemInfernoBlast : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 900;

	private const float Radius = 200f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 170);
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 900;
		base.Projectile.Calamity().DealsDefenseDamage = true;
	}

	public override void AI()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HealingPlus(base.Projectile.Center + Main.rand.NextVector2Circular(200f, 200f), 1f, Vector2.Zero, Color.Orange, Color.OrangeRed, 2)
			{
				Rotation = Main.rand.NextFloat((float)Math.PI * 2f)
			});
		}
		if (base.Projectile.timeLeft % 3 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new FlameExplosion(base.Projectile.Center, Vector2.Zero, Color.Orange, Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.1f, 0.25f, 15, 0.7f));
		}
		for (int j = 0; j < 3; j++)
		{
			float randomMult = Main.rand.NextFloat(0.35f, 0.9f);
			Vector2 dustSpawn = base.Projectile.Center + Main.rand.NextVector2CircularEdge(200f, 200f) * randomMult;
			Vector2 dustVel = base.Projectile.Center.DirectionTo(dustSpawn) * Utils.Remap(randomMult, 0.35f, 0.9f, 6f, 1f);
			Dust.NewDustPerfect(dustSpawn, 174, dustVel, 0, default(Color), 1.25f).noGravity = true;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 200f, targetHitbox);
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.timeLeft < 870;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 360);
	}
}
