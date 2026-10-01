using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AtaraxiaSplit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
		base.Projectile.timeLeft = 25;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		base.DrawOffsetX = -5;
		base.DrawOriginOffsetY = -1;
		base.DrawOriginOffsetX = 0f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.93f;
		base.Projectile.alpha += 5;
		float lightFactor = (255f - (float)base.Projectile.alpha) / 255f;
		Lighting.AddLight(base.Projectile.Center, 0.3f * lightFactor, 0.05f * lightFactor, 0.2f * lightFactor);
		if (Main.rand.Next(256) > base.Projectile.alpha - 60)
		{
			int idx = Dust.NewDust(base.Projectile.Center, 1, 1, 71);
			Main.dust[idx].position = base.Projectile.Center - base.Projectile.velocity * 0.7f;
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 0.3f;
			Dust obj2 = Main.dust[idx];
			obj2.velocity += base.Projectile.velocity * 0.4f;
			Main.dust[idx].scale = Main.rand.NextFloat(0.5f, 1f);
			Main.dust[idx].alpha = Main.rand.Next(80, 200);
		}
	}
}
