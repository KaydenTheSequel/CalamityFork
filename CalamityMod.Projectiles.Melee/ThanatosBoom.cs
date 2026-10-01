using System;
using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ThanatosBoom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 54);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 45;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			CreateExplosionDust();
			base.Projectile.localAI[0] = 1f;
		}
	}

	public void CreateExplosionDust()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (float speed = 2f; speed <= 6f; speed += 0.7f)
		{
			float lifePersistance = Main.rand.NextFloat(0.8f, 1.7f);
			for (int i = 0; i < 60; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.velocity = ((float)Math.PI * 2f * (float)i / 60f).ToRotationVector2() * speed;
				dust.noGravity = true;
				dust.color = Main.hslToRgb(Main.rand.NextFloat(), 0.7f, 0.625f);
				dust.fadeIn = lifePersistance;
				dust.scale = 1.4f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}
}
