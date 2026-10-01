using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class IceClasperEnemyProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/Melee/DarkIceZero";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.coldDamage = true;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.01f;
		int trailDust = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 172, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.5f);
		Main.dust[trailDust].noGravity = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (timeLeft > 0)
		{
			SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityClientConfig.Instance.Afterimages)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		return true;
	}
}
