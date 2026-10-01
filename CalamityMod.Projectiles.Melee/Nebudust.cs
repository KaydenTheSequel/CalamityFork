using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Nebudust : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.5f, 0f);
		float projTimer = 25f;
		if (base.Projectile.ai[0] > 60f)
		{
			projTimer -= (base.Projectile.ai[0] - 60f) / 2f;
		}
		if (projTimer <= 0f)
		{
			projTimer = 0f;
			base.Projectile.Kill();
		}
		projTimer *= 0.7f;
		if (base.Projectile.ai[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item105, base.Projectile.position);
		}
		base.Projectile.ai[0] += 4f;
		for (int timerCounter = 0; (float)timerCounter < projTimer; timerCounter++)
		{
			float rando1 = Main.rand.Next(-6, 7);
			float rando2 = Main.rand.Next(-6, 7);
			float num = Main.rand.Next(2, 5);
			float randoAdjuster = (float)Math.Sqrt(rando1 * rando1 + rando2 * rando2);
			randoAdjuster = num / randoAdjuster;
			rando1 *= randoAdjuster;
			rando2 *= randoAdjuster;
			int astra = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 269, 0f, 0f, 100);
			Main.dust[astra].noGravity = true;
			Main.dust[astra].position.X = base.Projectile.Center.X;
			Main.dust[astra].position.Y = base.Projectile.Center.Y;
			Main.dust[astra].position.X += Main.rand.Next(-10, 11);
			Main.dust[astra].position.Y += Main.rand.Next(-10, 11);
			Main.dust[astra].velocity.X = rando1;
			Main.dust[astra].velocity.Y = rando2;
		}
	}
}
