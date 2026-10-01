using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NebulaCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 116;
		base.Projectile.height = 116;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 720;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		int mainProjType = ModContent.ProjectileType<NebulaCloudCore>();
		float projTimer = 720f;
		if (false)
		{
			int mainProj = (int)base.Projectile.ai[1];
			if (!Main.projectile[mainProj].active || Main.projectile[mainProj].type != mainProjType)
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.ai[0]++;
		if (!(base.Projectile.ai[0] < projTimer))
		{
			return;
		}
		bool isActive = true;
		int mainProjID = (int)base.Projectile.ai[1];
		if (Main.projectile[mainProjID].active && Main.projectile[mainProjID].type == mainProjType)
		{
			if (Main.projectile[mainProjID].oldPos[1] != Vector2.Zero)
			{
				Projectile projectile = base.Projectile;
				projectile.position += Main.projectile[mainProjID].position - Main.projectile[mainProjID].oldPos[1];
			}
			if (base.Projectile.Center.HasNaNs())
			{
				base.Projectile.Kill();
				return;
			}
		}
		else
		{
			base.Projectile.ai[0] = projTimer;
			isActive = false;
			base.Projectile.Kill();
		}
		if (isActive)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity += new Vector2((float)Math.Sign(Main.projectile[mainProjID].Center.X - base.Projectile.Center.X), (float)Math.Sign(Main.projectile[mainProjID].Center.Y - base.Projectile.Center.Y)) * new Vector2(0.15f, 0.15f);
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 6f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 6f / ((Vector2)(ref base.Projectile.velocity)).Length();
			}
		}
		if (Main.rand.NextBool())
		{
			int purpleDust = Dust.NewDust(base.Projectile.Center, 8, 8, 86);
			Main.dust[purpleDust].position = base.Projectile.Center;
			Main.dust[purpleDust].velocity = base.Projectile.velocity;
			Main.dust[purpleDust].noGravity = true;
			Main.dust[purpleDust].scale = 1.75f;
			if (isActive)
			{
				Main.dust[purpleDust].customData = Main.projectile[(int)base.Projectile.ai[1]];
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] = 86f;
		for (int i = 0; i < 15; i++)
		{
			int killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, (int)base.Projectile.ai[0], base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 0, default(Color), 0.75f);
			if (Main.rand.NextBool(3))
			{
				Main.dust[killDust].fadeIn = 0.75f + (float)Main.rand.Next(-10, 11) * 0.015f;
				Main.dust[killDust].scale = 0.325f + (float)Main.rand.Next(-10, 11) * 0.0075f;
				Main.dust[killDust].type++;
			}
			else
			{
				Main.dust[killDust].scale = 1.5f + (float)Main.rand.Next(-10, 11) * 0.015f;
			}
			Main.dust[killDust].noGravity = true;
			Dust obj = Main.dust[killDust];
			obj.velocity *= 1.375f;
			Dust obj2 = Main.dust[killDust];
			obj2.velocity -= base.Projectile.oldVelocity / 20f;
		}
	}
}
