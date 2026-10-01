using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PurityProjection : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/BrokenBiomeBlade_PurityProjection";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.aiStyle = 27;
		base.AIType = 156;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 45;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 35)
		{
			base.Projectile.tileCollide = true;
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f, 1f, 0.24f);
		int dustParticle = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 100, default(Color), 0.9f);
		Main.dust[dustParticle].noGravity = true;
		Dust obj = Main.dust[dustParticle];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[dustParticle];
		obj2.velocity += base.Projectile.velocity * 0.1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 35)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 15; i++)
		{
			Vector2 displace = (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() * (-0.5f + (float)i / 15f) * 88f;
			int dustParticle = Dust.NewDust(base.Projectile.Center + displace, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 100, default(Color), 2f);
			Main.dust[dustParticle].noGravity = true;
			Main.dust[dustParticle].velocity = base.Projectile.oldVelocity;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Crumbling>(), 90);
	}
}
