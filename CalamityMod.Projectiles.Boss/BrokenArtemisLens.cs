using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrokenArtemisLens : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 480;
	}

	public override void AI()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 5f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		else
		{
			base.Projectile.rotation += (float)(base.Projectile.velocity.X > 0f).ToDirectionInt() * ((Vector2)(ref base.Projectile.velocity)).Length() * 0.018f;
		}
		if (base.Projectile.timeLeft < 90)
		{
			base.Projectile.Opacity = (float)base.Projectile.timeLeft / 90f;
		}
		base.Projectile.velocity.Y = MathHelper.Clamp(base.Projectile.velocity.Y + 0.325f, -25f, 25f);
		Time++;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 3f)
		{
			base.Projectile.velocity = Vector2.Zero;
			return false;
		}
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.75f;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		return false;
	}
}
