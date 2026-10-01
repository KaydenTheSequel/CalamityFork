using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class M1GarandBulletCasing : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public bool TouchedGrass;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/M1GarandBulletCasing";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = 1f;
		base.Projectile.extraUpdates = 1;
		Time++;
		_ = Main.player[base.Projectile.owner];
		if (!TouchedGrass)
		{
			if (base.Projectile.timeLeft <= 240)
			{
				base.Projectile.rotation += 0.04f * (float)base.Projectile.direction;
			}
			else
			{
				base.Projectile.rotation = (new Vector2(0f, (float)(-5 * base.Projectile.direction)) + base.Projectile.velocity * -1.2f).ToRotation();
			}
			base.Projectile.velocity.Y += 0.087f;
			base.Projectile.velocity.X *= 0.99f;
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity, 303, -base.Projectile.velocity.RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(0.2f, 1f), 140, default(Color), Main.rand.NextFloat(0.35f, 0.5f));
				dust.noGravity = false;
				dust.color = Color.White;
			}
		}
		else
		{
			base.Projectile.velocity = Vector2.Zero;
		}
		base.Projectile.alpha = (int)(255f * Utils.GetLerpValue(60f, 0f, base.Projectile.timeLeft, clamped: true));
		if (Collision.SolidCollision(base.Projectile.Center - base.Projectile.velocity * 1.5f, 2, 2))
		{
			base.Projectile.tileCollide = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = 0;
		TouchedGrass = true;
		base.Projectile.velocity = Vector2.Zero;
		return false;
	}
}
