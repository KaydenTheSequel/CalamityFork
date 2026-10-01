using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TotalityFire : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.rotation = (0f - base.Projectile.velocity.X) * 0.05f + (float)Math.PI / 2f;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		base.Projectile.ai[1]--;
		if (!initialized)
		{
			initialized = true;
			base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X *= -0.1f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X *= -0.5f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 1f)
		{
			base.Projectile.velocity.Y *= -0.5f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X *= 0.97f;
				if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.2f;
		}
		if (base.Projectile.velocity.Y < 0.25f && base.Projectile.velocity.Y > 0.15f)
		{
			base.Projectile.velocity.X *= 0.8f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (Main.rand.NextBool(4))
		{
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
			Dust obj = Main.dust[fire];
			obj.position.X -= 2f;
			obj.position.Y += 2f;
			obj.scale += (float)Main.rand.Next(50) * 0.01f;
			obj.noGravity = true;
			obj.velocity.Y -= 2f;
		}
		if (Main.rand.NextBool(10))
		{
			int fire2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
			Dust obj2 = Main.dust[fire2];
			obj2.position.X -= 2f;
			obj2.position.Y += 2f;
			obj2.scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			obj2.noGravity = true;
			obj2.velocity *= 0.1f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.ai[1] = 10f;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 150);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(323, 150);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
