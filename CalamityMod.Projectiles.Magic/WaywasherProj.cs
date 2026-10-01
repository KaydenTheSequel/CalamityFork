using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class WaywasherProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.02f * (float)base.Projectile.direction;
		Lighting.AddLight(base.Projectile.Center, 0f, 0.1f, 0.7f);
		for (int i = 0; i < 2; i++)
		{
			float shortXVel = base.Projectile.velocity.X / 3f * (float)i;
			float shortYVel = base.Projectile.velocity.Y / 3f * (float)i;
			int fourConst = 4;
			int waterDust = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)fourConst, base.Projectile.position.Y + (float)fourConst), base.Projectile.width - fourConst * 2, base.Projectile.height - fourConst * 2, 33, 0f, 0f, 0, new Color(64, 224, 208), 1.2f);
			Dust obj = Main.dust[waterDust];
			obj.noGravity = true;
			obj.velocity *= 0.1f;
			obj.velocity += base.Projectile.velocity * 0.1f;
			obj.position.X -= shortXVel;
			obj.position.Y -= shortYVel;
		}
		if (Main.rand.NextBool(5))
		{
			int otherFourConst = 4;
			int otherWaterDust = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)otherFourConst, base.Projectile.position.Y + (float)otherFourConst), base.Projectile.width - otherFourConst * 2, base.Projectile.height - otherFourConst * 2, 33, 0f, 0f, 0, new Color(64, 224, 208), 0.6f);
			Dust obj2 = Main.dust[otherWaterDust];
			obj2.velocity *= 0.25f;
			Dust obj3 = Main.dust[otherWaterDust];
			obj3.velocity += base.Projectile.velocity * 0.5f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int k = 0; k < 10; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 33, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f, 0, new Color(0, 142, 255));
		}
	}
}
