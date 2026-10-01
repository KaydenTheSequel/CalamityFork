using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class HideOfAstrumDeusExplosion : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.timeLeft = 60;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0f, 0.75f);
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
		int timerCounter = 0;
		float scale = 0.7f;
		int dustType = (Main.rand.NextBool() ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>());
		if (base.Projectile.ai[1] == 0f)
		{
			scale = 1.5f;
			dustType = 173;
		}
		for (; (float)timerCounter < projTimer; timerCounter++)
		{
			float rand1 = Main.rand.Next(-30, 31);
			float rand2 = Main.rand.Next(-30, 31);
			float num = Main.rand.Next(9, 27);
			float randAdjust = (float)Math.Sqrt(rand1 * rand1 + rand2 * rand2);
			randAdjust = num / randAdjust;
			rand1 *= randAdjust;
			rand2 *= randAdjust;
			int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, default(Color), scale);
			Dust dust = Main.dust[dusty];
			if (dustType != 173)
			{
				dust.color = new Color(255, 255, 255, 0);
			}
			dust.noGravity = true;
			dust.position.X = base.Projectile.Center.X;
			dust.position.Y = base.Projectile.Center.Y;
			dust.position.X += Main.rand.Next(-10, 11);
			dust.position.Y += Main.rand.Next(-10, 11);
			dust.velocity.X = rand1;
			dust.velocity.Y = rand2;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
	}
}
