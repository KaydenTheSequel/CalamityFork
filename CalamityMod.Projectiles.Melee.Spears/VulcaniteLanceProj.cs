using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class VulcaniteLanceProj : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<VulcaniteLance>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 2.4f;

	public override float ForwardSpeed => 0.8f;

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
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
	}

	public override void ExtraBehavior()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(3) ? 16 : 127, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		Vector2 goreVec = base.Projectile.Center + base.Projectile.velocity;
		if (Main.rand.NextBool(8) && !Main.dedServ)
		{
			int smoke = Gore.NewGore(base.Projectile.GetSource_FromAI(), goreVec, default(Vector2), Main.rand.Next(375, 378));
			Main.gore[smoke].behindTiles = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, hit.Crit);
		target.AddBuff(323, 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, crit: true);
		target.AddBuff(323, 240);
	}

	private void OnHitEffects(Vector2 targetPos, bool crit)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			int boom = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			if (boom.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[boom].DamageType = DamageClass.Melee;
			}
		}
		if (!crit)
		{
			return;
		}
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int i = 0; i < 2; i++)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, Main.rand.NextBool(), 800f, 800f, 0f, 800f, 10f, ModContent.ProjectileType<TinyFlare>(), (int)((double)base.Projectile.damage * 0.5), 2f, base.Projectile.owner, clamped: true);
			}
		}
	}
}
