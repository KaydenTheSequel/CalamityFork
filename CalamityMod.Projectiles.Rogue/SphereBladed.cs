using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SphereBladed : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 15;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f, 0f, 0f);
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 229, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 100);
		}
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 8;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.position);
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 30f)
			{
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
		}
		else
		{
			float acceleration = 3.2f;
			Vector2 vector2 = base.Projectile.Center;
			float xdistance = Main.player[base.Projectile.owner].position.X + (float)(Main.player[base.Projectile.owner].width / 2) - vector2.X;
			float ydistance = Main.player[base.Projectile.owner].position.Y + (float)(Main.player[base.Projectile.owner].height / 2) - vector2.Y;
			float totalDist = (float)Math.Sqrt(xdistance * xdistance + ydistance * ydistance);
			if (totalDist > 3000f)
			{
				base.Projectile.Kill();
			}
			totalDist = 16f / totalDist;
			xdistance *= totalDist;
			ydistance *= totalDist;
			if (base.Projectile.velocity.X < xdistance)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
				if (base.Projectile.velocity.X < 0f && xdistance > 0f)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
				}
			}
			else if (base.Projectile.velocity.X > xdistance)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
				if (base.Projectile.velocity.X > 0f && xdistance < 0f)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
				}
			}
			if (base.Projectile.velocity.Y < ydistance)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
				if (base.Projectile.velocity.Y < 0f && ydistance > 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
				}
			}
			else if (base.Projectile.velocity.Y > ydistance)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
				if (base.Projectile.velocity.Y > 0f && ydistance < 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
				}
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle rectangle = default(Rectangle);
				((Rectangle)(ref rectangle))._002Ector((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height);
				Rectangle playerPos = default(Rectangle);
				((Rectangle)(ref playerPos))._002Ector((int)Main.player[base.Projectile.owner].position.X, (int)Main.player[base.Projectile.owner].position.Y, Main.player[base.Projectile.owner].width, Main.player[base.Projectile.owner].height);
				if (((Rectangle)(ref rectangle)).Intersects(playerPos))
				{
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.rotation += 0.5f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit34, base.Projectile.position);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
