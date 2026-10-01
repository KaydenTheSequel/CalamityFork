using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ScarletBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 500;
		base.Projectile.height = 500;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 30;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.55f, 0.25f, 0f);
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
			base.Projectile.localAI[0]++;
		}
		bool xflag = false;
		bool yflag = false;
		if (base.Projectile.velocity.X < 0f && base.Projectile.position.X < base.Projectile.ai[0])
		{
			xflag = true;
		}
		if (base.Projectile.velocity.X > 0f && base.Projectile.position.X > base.Projectile.ai[0])
		{
			xflag = true;
		}
		if (base.Projectile.velocity.Y < 0f && base.Projectile.position.Y < base.Projectile.ai[1])
		{
			yflag = true;
		}
		if (base.Projectile.velocity.Y > 0f && base.Projectile.position.Y > base.Projectile.ai[1])
		{
			yflag = true;
		}
		if (xflag & yflag)
		{
			base.Projectile.Kill();
		}
		float projTimer = 25f;
		if (base.Projectile.ai[0] > 180f)
		{
			projTimer -= (base.Projectile.ai[0] - 180f) / 2f;
		}
		if (projTimer <= 0f)
		{
			projTimer = 0f;
			base.Projectile.Kill();
		}
		projTimer *= 0.7f;
		base.Projectile.ai[0] += 4f;
		for (int timerCompare = 0; (float)timerCompare < projTimer; timerCompare++)
		{
			float random1 = Main.rand.Next(-30, 35);
			float random2 = Main.rand.Next(-30, 35);
			float num = Main.rand.Next(13, 18);
			float randomAdjust = (float)Math.Sqrt(random1 * random1 + random2 * random2);
			randomAdjust = num / randomAdjust;
			random1 *= randomAdjust;
			random2 *= randomAdjust;
			int redDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 130, 0f, 0f, 100, default(Color), 1.25f);
			Dust obj = Main.dust[redDust];
			obj.noGravity = true;
			obj.position.X = base.Projectile.Center.X;
			obj.position.Y = base.Projectile.Center.Y;
			obj.position.X += Main.rand.Next(-10, 11);
			obj.position.Y += Main.rand.Next(-10, 11);
			obj.velocity.X = random1;
			obj.velocity.Y = random2;
		}
	}
}
