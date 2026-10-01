using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class EarthenPikeSpear : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<EarthenPike>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 2.4f;

	public override float ForwardSpeed => 0.4f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
	}

	public override void ExtraBehavior()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (!(base.Projectile.localAI[0] >= 6f))
		{
			return;
		}
		base.Projectile.localAI[0] = 0f;
		if (Main.myPlayer == base.Projectile.owner)
		{
			float randomVelocity = Main.rand.NextFloat(1.085f, 1.115f);
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * randomVelocity, ModContent.ProjectileType<FossilShard>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack * 0.2f, base.Projectile.owner, 0f, 1f);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].DamageType = DamageClass.Melee;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 300);
	}
}
