using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class HellionSpike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.aiStyle = 27;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 2;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 600;
		base.AIType = 228;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.25f, 0f);
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 44, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 44, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, hit.Crit);
		target.AddBuff(70, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, crit: true);
		target.AddBuff(70, 180);
	}

	private void OnHitEffects(Vector2 targetPos, bool crit)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!crit)
		{
			return;
		}
		IEntitySource source = base.Projectile.GetSource_FromThis();
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile petal = CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, Main.rand.NextBool(), 800f, 800f, 0f, 800f, 24f, 221, (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack * 0.5f, base.Projectile.owner, clamped: false, 0f);
			if (petal.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				petal.DamageType = DamageClass.Melee;
				petal.localNPCHitCooldown = -1;
			}
		}
	}
}
