using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class BrimlanceProj : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Brimlance>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 2.4f;

	public override float ForwardSpeed => 0.7f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.hide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
	}

	public override void ExtraBehavior()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			int idx = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 235, base.Projectile.direction * 2, 0f, 150);
			Main.dust[idx].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<BrimlanceHellfireExplosion>()] < 3)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<BrimlanceHellfireExplosion>(), (int)((double)base.Projectile.damage * 0.35), hit.Knockback, Main.myPlayer);
			}
			for (int i = 0; i < 2; i++)
			{
				Vector2 fireVelocity = Utils.RotatedByRandom(new Vector2(0f, Main.rand.NextFloat(7f, 10f)), 6.2831854820251465);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, fireVelocity, ModContent.ProjectileType<BrimlanceStandingFire>(), (int)((double)base.Projectile.damage * 0.25), 0f, base.Projectile.owner);
			}
		}
	}
}
