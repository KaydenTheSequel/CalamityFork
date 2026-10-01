using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BloodBombExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 250;
		base.Projectile.height = 250;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = ((base.Projectile.ai[2] == 1f) ? 30 : 60);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 1f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f);
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.position);
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
			float rand1 = Main.rand.Next(-30, 31);
			float rand2 = Main.rand.Next(-30, 31);
			float num = Main.rand.Next(9, 27);
			float randAdjust = (float)Math.Sqrt(rand1 * rand1 + rand2 * rand2);
			randAdjust = num / randAdjust;
			rand1 *= randAdjust;
			rand2 *= randAdjust;
			int bloody = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 100, default(Color), 1.8f);
			Dust obj = Main.dust[bloody];
			obj.noGravity = true;
			obj.position.X = base.Projectile.Center.X;
			obj.position.Y = base.Projectile.Center.Y;
			obj.position.X += Main.rand.Next(-10, 11);
			obj.position.Y += Main.rand.Next(-10, 11);
			obj.velocity.X = rand1;
			obj.velocity.Y = rand2;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[2] == 1f)
		{
			target.AddBuff(ModContent.BuffType<Laceration>(), 40);
		}
	}
}
