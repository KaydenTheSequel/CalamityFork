using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class SausageMakerSpear : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SausageMaker>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 1.1f;

	public override float ForwardSpeed => 0.9f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 44);
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void ExtraBehavior()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 5, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 240);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 1.2f, ModContent.ProjectileType<Blood2>(), base.Projectile.damage / 2, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
		}
	}
}
