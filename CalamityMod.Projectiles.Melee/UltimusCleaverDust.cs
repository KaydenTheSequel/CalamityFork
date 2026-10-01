using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class UltimusCleaverDust : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 90;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.1f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.5f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 1f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y * -0.5f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if ((double)base.Projectile.velocity.X > -0.01 && (double)base.Projectile.velocity.X < 0.01)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.2f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		int coldFire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 100);
		Main.dust[coldFire].position.X -= 2f;
		Main.dust[coldFire].position.Y += 2f;
		Main.dust[coldFire].scale += (float)Main.rand.Next(50) * 0.01f;
		Main.dust[coldFire].noGravity = true;
		Main.dust[coldFire].velocity.Y -= 2f;
		if (Main.rand.NextBool())
		{
			int coldFiery = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 100);
			Main.dust[coldFiery].position.X -= 2f;
			Main.dust[coldFiery].position.Y += 2f;
			Main.dust[coldFiery].scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			Main.dust[coldFiery].noGravity = true;
			Dust obj = Main.dust[coldFiery];
			obj.velocity *= 0.1f;
		}
		if ((double)base.Projectile.velocity.Y < 0.25 && (double)base.Projectile.velocity.Y > 0.15)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.8f;
		}
		base.Projectile.rotation = (0f - base.Projectile.velocity.X) * 0.05f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 150f, 12f, 20f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.DamageType == RogueDamageClass.Instance)
		{
			target.AddBuff(144, 120);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.DamageType == RogueDamageClass.Instance)
		{
			target.AddBuff(144, 120);
		}
	}
}
