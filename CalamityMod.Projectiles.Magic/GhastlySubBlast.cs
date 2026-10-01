using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GhastlySubBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 420;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
	}

	public override void AI()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		int projType = ModContent.ProjectileType<GhastlyBlast>();
		float x3 = 0.15f;
		float y3 = 0.15f;
		if (false)
		{
			int projID = (int)base.Projectile.ai[1];
			if (!Main.projectile[projID].active || Main.projectile[projID].type != projType)
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.ai[0]++;
		if (!(base.Projectile.ai[0] < 420f))
		{
			return;
		}
		bool isActive = true;
		int preHomingProjID = (int)base.Projectile.ai[1];
		if (Main.projectile[preHomingProjID].active && Main.projectile[preHomingProjID].type == projType)
		{
			if (Main.projectile[preHomingProjID].oldPos[1] != Vector2.Zero)
			{
				Projectile projectile = base.Projectile;
				projectile.position += Main.projectile[preHomingProjID].position - Main.projectile[preHomingProjID].oldPos[1];
			}
			if (base.Projectile.Center.HasNaNs())
			{
				base.Projectile.Kill();
				return;
			}
		}
		else
		{
			base.Projectile.ai[0] = 420f;
			isActive = false;
			base.Projectile.Kill();
		}
		if (isActive)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity += new Vector2((float)Math.Sign(Main.projectile[preHomingProjID].Center.X - base.Projectile.Center.X), (float)Math.Sign(Main.projectile[preHomingProjID].Center.Y - base.Projectile.Center.Y)) * new Vector2(x3, y3);
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 6f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 6f / ((Vector2)(ref base.Projectile.velocity)).Length();
			}
		}
		if (Main.rand.NextBool())
		{
			int ghostlyRed = Dust.NewDust(base.Projectile.Center, 8, 8, 60);
			Main.dust[ghostlyRed].position = base.Projectile.Center;
			Main.dust[ghostlyRed].velocity = base.Projectile.velocity;
			Main.dust[ghostlyRed].noGravity = true;
			Main.dust[ghostlyRed].scale = 1.5f;
			if (isActive)
			{
				Main.dust[ghostlyRed].customData = Main.projectile[(int)base.Projectile.ai[1]];
			}
		}
		base.Projectile.alpha = 255;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] = 60f;
		for (int i = 0; i < 10; i++)
		{
			int killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, (int)base.Projectile.ai[0], base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
			Main.dust[killDust].scale = 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
			Main.dust[killDust].noGravity = true;
			Dust obj = Main.dust[killDust];
			obj.velocity *= 1.25f;
			Dust obj2 = Main.dust[killDust];
			obj2.velocity -= base.Projectile.oldVelocity / 10f;
		}
	}
}
