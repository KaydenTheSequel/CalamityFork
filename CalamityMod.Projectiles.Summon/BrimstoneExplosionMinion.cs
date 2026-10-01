using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BrimstoneExplosionMinion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 130;
		base.Projectile.height = 130;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.9f, 0f, 0f);
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
			base.Projectile.localAI[0]++;
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
			float rand1 = Main.rand.Next(-10, 11);
			float rand2 = Main.rand.Next(-10, 11);
			float num = Main.rand.Next(3, 9);
			float randAdjust = (float)Math.Sqrt(rand1 * rand1 + rand2 * rand2);
			randAdjust = num / randAdjust;
			rand1 *= randAdjust;
			rand2 *= randAdjust;
			int brimDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[brimDust];
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
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
	}
}
