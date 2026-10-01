using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BarrelShrapnel : ModProjectile, ILocalizedModType, IModType
{
	public bool hitTile;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 160;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (hitTile)
		{
			base.Projectile.velocity.X = 0f;
			base.Projectile.rotation = (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		base.Projectile.velocity.Y += 0.2f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		hitTile = true;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 240);
		base.Projectile.Kill();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(323, 240);
		base.Projectile.Kill();
	}
}
