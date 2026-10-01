using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Environment;

public class WaterAlgae : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public ref float Direction => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.CanDistortWater[base.Type] = false;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 720;
		base.Projectile.penetrate = 1;
		base.Projectile.scale = Main.rand?.NextFloat(0.4f, 0.75f) ?? 1f;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 120f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(720f, 660f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.hide = base.Projectile.Opacity < 0.2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
			base.Projectile.localAI[0] = 1f;
		}
		if (Collision.WetCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			if (Direction == 0f)
			{
				Direction = base.Projectile.identity % 2;
			}
			if (Collision.SolidCollision(base.Projectile.Center + Vector2.UnitX * Direction * 100f, 1, 1))
			{
				Direction *= -1f;
			}
			base.Projectile.velocity.X = MathHelper.Lerp(base.Projectile.velocity.X, Direction * MathHelper.Lerp(0.3f, 0.7f, (float)base.Projectile.identity % 9f / 9f), 0.025f);
			base.Projectile.velocity.Y = MathHelper.Clamp(base.Projectile.velocity.Y - 0.008f, -0.4f, 0.4f);
		}
		else
		{
			base.Projectile.velocity.X *= 0.985f;
			base.Projectile.velocity.Y = MathHelper.Clamp(base.Projectile.velocity.Y + 0.1f, -1f, 5f);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.velocity.X *= 0.5f;
		if (base.Projectile.timeLeft > 210)
		{
			base.Projectile.timeLeft = 210;
		}
		return false;
	}
}
