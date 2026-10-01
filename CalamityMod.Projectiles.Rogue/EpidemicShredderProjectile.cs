using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class EpidemicShredderProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/EpidemicShredder";

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 6;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 40;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (float)Math.Sign(base.Projectile.velocity.X) * MathHelper.ToRadians(10f);
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
		}
		if ((float)base.Projectile.timeLeft < 160f)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 18f + base.Projectile.SafeDirectionTo(Main.player[base.Projectile.owner].Center) * 18f) / 19f;
			Rectangle hitbox = Main.player[base.Projectile.owner].Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(base.Projectile.Hitbox))
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.timeLeft % 4 == 0 && base.Projectile.Calamity().stealthStrike)
		{
			int projIndex2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity * -1f).RotatedByRandom(MathHelper.ToRadians(15f)), ModContent.ProjectileType<PlagueSeeker>(), (int)((double)base.Projectile.damage * 0.6), base.Projectile.knockBack * 0.6f, base.Projectile.owner);
			if (projIndex2.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[projIndex2].DamageType = RogueDamageClass.Instance;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.penetrate > 1)
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			SpawnSeeker();
			base.Projectile.penetrate--;
		}
		else
		{
			base.Projectile.tileCollide = false;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		SpawnSeeker();
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		SpawnSeeker();
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}

	public void SpawnSeeker()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			int projectileIndex = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<PlagueSeeker>(), (int)((double)base.Projectile.damage * 0.6), base.Projectile.knockBack * 0.6f, base.Projectile.owner);
			if (projectileIndex.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[projectileIndex].DamageType = RogueDamageClass.Instance;
			}
			base.Projectile.ai[0] = 12f;
		}
	}
}
