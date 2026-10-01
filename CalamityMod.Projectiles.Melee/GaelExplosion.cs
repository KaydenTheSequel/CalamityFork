using System;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GaelExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 15;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = GaelsGreatsword.ImmunityFrames;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.75f / 255f, (float)(255 - base.Projectile.alpha) * 0.75f / 255f);
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.Center);
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
		for (int timerCounter = 0; (float)timerCounter < projTimer; timerCounter++)
		{
			float rando1 = Main.rand.Next(-30, 31);
			float rando2 = Main.rand.Next(-30, 31);
			float num = Main.rand.Next(9, 27);
			float randoAdjuster = (float)Math.Sqrt(rando1 * rando1 + rando2 * rando2);
			randoAdjuster = num / randoAdjuster;
			rando1 *= randoAdjuster;
			rando2 *= randoAdjuster;
			int gaelDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 218, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			Main.dust[gaelDust].noGravity = true;
			Main.dust[gaelDust].position.X = base.Projectile.Center.X;
			Main.dust[gaelDust].position.Y = base.Projectile.Center.Y;
			Main.dust[gaelDust].position.X += Main.rand.Next(-10, 11);
			Main.dust[gaelDust].position.Y += Main.rand.Next(-10, 11);
			Main.dust[gaelDust].velocity.X = rando1;
			Main.dust[gaelDust].velocity.Y = rando2;
		}
	}
}
