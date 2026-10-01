using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class InfernalKrisExplosion : ModProjectile, ILocalizedModType, IModType
{
	public static float radius = 64f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (int)radius * 2;
		base.Projectile.height = (int)radius * 2;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 9;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft >= 5)
		{
			int numDust = 40;
			int dustType = 6;
			float minRange = 0f;
			float maxRange = radius / 10f;
			Vector2 circleVelocity = default(Vector2);
			for (int i = 0; i < numDust; i++)
			{
				((Vector2)(ref circleVelocity))._002Ector(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
				((Vector2)(ref circleVelocity)).Normalize();
				circleVelocity *= Main.rand.NextFloat(minRange, maxRange);
				int circle = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, circleVelocity.X, circleVelocity.Y, 0, default(Color), 2f);
				Main.dust[circle].noGravity = true;
				Main.dust[circle].velocity = circleVelocity;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 180);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}
}
