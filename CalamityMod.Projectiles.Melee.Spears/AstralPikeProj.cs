using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class AstralPikeProj : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<AstralPike>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 2.4f;

	public override float ForwardSpeed => 0.8f;

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
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
	}

	public override void ExtraBehavior()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int i = 0; i < 3; i++)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile star = CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, target.Center, Main.rand.NextBool(), 800f, 800f, 800f, 800f, 10f, ModContent.ProjectileType<AstralStar>(), (int)((double)base.Projectile.damage * 0.33), 1f, base.Projectile.owner, clamped: true);
				if (star.whoAmI.WithinBounds(Main.maxProjectiles))
				{
					star.DamageType = DamageClass.Melee;
					star.ai[0] = 3f;
				}
			}
		}
	}
}
